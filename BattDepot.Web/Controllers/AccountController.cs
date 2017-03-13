using System;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Concrete;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Infrastructure.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using Davisoft_BDSProject.Web.Models;
using System.Web.Security;
using Resources;

namespace Davisoft_BDSProject.Web.Controllers
{
    [ExcludeFilters(typeof(RequestAuthorizeAttribute))]
    public class AccountController : Controller
    {
        private readonly ILoginTracker _loginTracker;
        private readonly IMembershipService _membership;

        public AccountController(IMembershipService membership, ILoginTracker loginTracker, IUnitRepository unitRepo)
        {
            _membership = membership;
            _loginTracker = loginTracker;
        }

        public ActionResult Index()
        {
            return View();
        }


        private RedirectResult RedirectToLocal(string returnUrl)
        {
            return Redirect(Url.IsLocalUrl(returnUrl)
                                ? returnUrl
                                : Url.Action("Index", "Home", new { area = "" }));
        }

        public ActionResult Login(string returnUrl)
        {
            //Session["DbName"] = null;

            CurrentUser.Logout();

            ViewBag.returnUrl = returnUrl;
            var model = new LoginViewModel()
                        {
                            RememberMe = true,
                        };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (!CurrentUser.Login(model.Email, model.Password, model.RememberMe))
                    {
                        ModelState.AddModelError("", Resource.InvalidEmailOrPassword);
                    }
                    else
                    {
                        MoneyHelper.SetDefaultCurrency();
                        return RedirectToLocal(returnUrl);
                    }
                }
                catch (ConcurrentLoginLimitException ex)
                {
                    ModelState.AddModelError("", "Your account is currently logged in somewhere else. Please try again later!");
                }
            }
            return View(model);
        }

        public ActionResult Logout(string returnUrl = "/")
        {
            //Session["DbName"] = null;
            //if (Request.Cookies["DatabaseName"] != null)
            //{
            //    var cookie = Request.Cookies["DatabaseName"];
            //    cookie.Value = null;
            //    Response.Cookies.Set(cookie);
            //}

            CurrentUser.Logout();
            //var cookie = Request.Cookies["DatabaseName"];
            //cookie.Expires = DateTime.Now.AddDays(-1);
            //Response.Cookies.Set(cookie);
            return RedirectToAction("Login", "Account", new { returnUrl });
        }

        [ActionName("Profile")]
        [DisplayName("Edit Profile")]
        public ActionResult EditProfile()
        {
            var model = new AccountModel(CurrentUser.Identity);
            return View(model);
        }

        [HttpPost]
        [ActionName("Profile")]
        [DisplayName("Edit Profile")]
        public ActionResult EditProfile(AccountModel model)
        {
            var oldPassword = EncryptHelper.EncryptPassword(model.OldPassword);
            if (oldPassword != CurrentUser.Identity.Password)
            {
                ModelState.AddModelError("OldPassword", Resource.TheOldPasswordDoNotMatch);
            }
            if (ModelState.IsValid)
            {
                // Update user profile picture
                if (model.Picture != null && model.Picture.ContentLength > 0)
                {
                    UserPicture.Delete(CurrentUser.Identity.ID, CurrentUser.Identity.Picture);
                    string pictureFileName = UserPicture.Upload(CurrentUser.Identity.ID, model.Picture);
                    CurrentUser.Identity.Picture = pictureFileName;
                }

                // Update user primitive info
                //CurrentUser.Identity.Password = model.Password;
                CurrentUser.Identity.DisplayName = model.Username;
                CurrentUser.Identity.Email = model.Email;
                CurrentUser.Identity.Phone = model.Phone;
                CurrentUser.Identity.MobilePhone = model.MobilePhone;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    CurrentUser.Identity.Password = EncryptHelper.EncryptPassword(model.Password);
                }
                if (_membership.UpdateUser(CurrentUser.Identity))
                    FormsAuthentication.SetAuthCookie(CurrentUser.Identity.Email, false);

                _loginTracker.ReloadUser(CurrentUser.Identity.Email, CurrentUser.Identity);


                ViewBag.Success = true;
                ViewBag.Message = Resource.YourProfileHasBeenUpdated;
                return EditProfile();
            }

            return View(model);
        }

        public ActionResult BypassLogin(string token)
        {
            return Redirect(BypassAuth.Decrypt(token));
        }
    }
}

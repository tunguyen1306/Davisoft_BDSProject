using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebBDS_Project.Models;

namespace WebBDS_Project.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        davisoft_bdsprojectEntities1 db = new davisoft_bdsprojectEntities1();
        public ActionResult Login(BdsAcountModel acountModel)
        {
            Session["IdUser"] = null;
            Session["EmailUser"] = null;
            Session["IdTypeUser"] = null;
            Session["FullName"] = null;
          
            var data = db.BDSAccounts.FirstOrDefault(x => x.Email.ToLower() == acountModel.tblBDSAccount.Email.ToLower().Trim() && x.PassWord == acountModel.tblBDSAccount.PassWord.Trim() && x.Active == 1 && x.MailActive==1);
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.Email, false);
                String returnUrl = Request.Params["ReturnUrl"];
                var dataEmployee = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == data.ID && x.Active == 1);
                var dataPer= db.BDSPersonalInformations.FirstOrDefault(x => x.IdAccount == data.ID && x.Active == 1);
                if (dataEmployee != null)
                {
                    Session["IdUser"] = null;
                    Session["EmailUser"] = null;
                    Session["IdUserEmployee"] = null;
                    Session.Remove("IdUser");
                    Session.Remove("EmailUser");
                    Session.Remove("IdUserPer");
                    Session["FullName"] = dataEmployee.Name;
                    Session["EmailUser"] = data.Email;
                    Session["IdUser"] = data.ID;
                    Session["IdUserEmployee"] = dataEmployee.ID;
                }
                else
                {
                    if (dataPer != null)
                    {
                        Session["IdUser"] = null;
                        Session["EmailUser"] = null;
                        Session["IdUserEmployee"] = null;
                        Session.Remove("IdUser");
                        Session.Remove("EmailUser");
                        Session.Remove("IdUserEmployee");
                        Session["IdUser"] = data.ID;
                        Session["EmailUser"] = data.Email;
                        Session["FullName"] = dataPer.Name;
                        Session["IdUserPer"] = dataPer.ID;
                    }
                    else
                    {
                        return RedirectToAction("LoginForm", new { authen = false });
                    }
                }
                if (String.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    return Redirect(Server.UrlDecode(returnUrl));
                }

            }
            return RedirectToAction("LoginForm", "Login", acountModel);

        }
        public ActionResult LoginForm()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session["IdUser"] = null;
            Session["EmailUser"] = null;
            Session.Remove("IdUser");
            Session.Remove("EmailUser");

            return RedirectToAction("Index","Default");
        }
        [HttpPost]
        public ActionResult Checkuser(int id)
        {
            var result = 0;
            if (db.BDSPersonalInformations.Select(x => x.IdAccount).ToList().Contains(id))
            {
                result = 1;
            }

            return Json(new { result = result });
        }
        [HttpPost]
        public ActionResult CheckuserEm(int id)
        {
            var result = 0;
            if (db.BDSEmployerInformations.Select(x => x.IdAccount).ToList().Contains(id))
            {
                result = 1;
            }
            return Json(new { result = result });
        }
    }
}

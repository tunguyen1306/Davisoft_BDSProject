using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf.qrcode;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Infrastructure.Helpers;
using MultiLanguage;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Concrete;
using Davisoft_BDSProject.Web.Infrastructure;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using Davisoft_BDSProject.Web.Models;
using NS.Mail;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using StringHelper = Davisoft_BDSProject.Domain.Helpers.StringHelper;
using UrlHelper = Davisoft_BDSProject.Web.Infrastructure.Helpers.UrlHelper;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class HomeController : Controller
    {

        [DisplayName(@"Dashboard")]
        public ActionResult Index()
        {
            //ViewBag.DefaultCountry = MoneyHelper.DefaultCountry;
            //ViewBag.DefaultCurrency = MoneyHelper.DefaultCurrency;
            var model = new HomeViewModel();
            var salesmanId = PermissionHelper.SalesmanId();
            var branchId = PermissionHelper.BranchId();
            if (salesmanId == 0 && branchId == 0)
            {
                model.Prepare();
            }
            else
            {
                //model.PrepareForSalesman();
            }
            return View(model);
        }

        [DisplayName(@"Search")]
        public ActionResult Search(string query)
        {
            //ViewBag.DefaultCountry = MoneyHelper.DefaultCountry;
            //ViewBag.DefaultCurrency = MoneyHelper.DefaultCurrency;
            var model = new HomeSearchModel();
            model.Search(query);
            return View(model);
        }

        [DisplayName(@"Set language")]
        public ActionResult SetLanguage(string cult, string returnUrl)
        {
            var accountService = DependencyHelper.GetService<IMembershipService>();
            var unitService = DependencyHelper.GetService<IUnitRepository>();
            var language = unitService.GetLanguage(cult);
            CurrentUser.Identity.LanguageID = language.ID;
            CurrentUser.Identity.Language = language;
            accountService.UpdateUserLanguage(CurrentUser.Identity.ID, language.ID);
            //if (HttpContext.Request.Url != null)
            //{
            //    var url = HttpContext.Request.Url.LocalPath;
            //    return RedirectToLocal(url);
            //}
            return RedirectToLocal(returnUrl);
        }

        [AjaxOnly]
        private RedirectResult RedirectToLocal(string returnUrl)
        {
            return Redirect(Url.IsLocalUrl(returnUrl)
                                ? returnUrl
                                : Url.Action("Index", "Home", new { area = "" }));
        }

        [AjaxOnly]
        public ActionResult GetOnOffSession()
        {
            var r = System.Web.HttpContext.Current.Session.SessionID + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return Json(r, JsonRequestBehavior.AllowGet);
        }
        [AjaxOnly]
        public ActionResult GetToken(string user, string pass, string session)
        {
            var token = StringHelper.GetToken(session, pass, user);
            return Json(token, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public ActionResult TestMail()
        {
            string subject = "Test mail " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
            var obj = new object();
            var message = new RazorMailMessage("Logs/Exception", obj).Render();
            EmailHelper.SendEmail(new[] { "linh@netrunnersystems.com" }, subject, message);
            return Json("Successfully!", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Blank()
        {
            return View("Blank");
        }

    }
}

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
        davisoft_bdsprojectEntities db = new davisoft_bdsprojectEntities();
        public ActionResult Login(BdsAcountModel acountModel)
        {
            Session["IdUser"] = null;
            Session["EmailUser"] = null;
            Session["IdTypeUser"] = null;
            Session["FullName"] = null;
            var data = db.BDSAccounts.FirstOrDefault(x => x.Email == acountModel.tblBDSAccount.Email && x.PassWord == acountModel.tblBDSAccount.PassWord && x.Active == 1);
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.Email, false);
                String returnUrl = Request.Params["ReturnUrl"];


              
                var dataEmployee = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == data.ID && x.Active == 1);
                var dataPer= db.BDSPersonalInformations.FirstOrDefault(x => x.IdAccount == data.ID && x.Active == 1);
                if (dataEmployee != null)
                {
                    Session["FullName"] = dataEmployee.Name;
                    Session["EmailUser"] = data.Email;
                    Session["IdUser"] = data.ID;
                }
                else
                {
                    if (dataPer != null)
                    {
                            Session["IdUser"] = data.ID;
                            Session["EmailUser"] = data.Email;
                            Session["FullName"] = dataPer.Name;
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
            return RedirectToAction("Index", "Default", acountModel);

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

    }
}

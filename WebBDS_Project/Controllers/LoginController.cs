using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var data = db.bdsaccounts.Where(x => x.Email == acountModel.tblbdsaccount.Email && x.PassWord == acountModel.tblbdsaccount.PassWord && x.Active == 1).FirstOrDefault();
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.Email, false);
                String returnUrl = Request.Params["ReturnUrl"];


                Session["IdUser"] = data.Id;
                Session["EmailUser"] = data.Email;
                var dataEmployee = db.bdsemployerinformations.Where(x => x.IdAccount == data.Id).Select(x=>x.Name).FirstOrDefault();
            
                    //Session["FullName"] = dataEmployee.Name;
               
           
                //Session["IdTypeUser"] = data.IdTypeUser;
                //Session["FullName"] = data.LastNameUser + data.FirtNameUser;
                if (String.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    return Redirect(Server.UrlDecode(returnUrl));
                }

            }
            return View();
        }
        public ActionResult LoginForm()
        {
            return View();
        }

    }
}

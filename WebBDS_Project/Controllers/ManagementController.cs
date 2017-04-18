﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebBDS_Project.Models;

namespace WebBDS_Project.Controllers
{
    public class ManagementController : Controller
    {
        //
        // GET: /Management/
        davisoft_bdsprojectEntities db = new davisoft_bdsprojectEntities();
         [ActionName("ManagementCompany")]
        public ActionResult ManagementCompany()
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            var idAcount = int.Parse(Session["IdUser"].ToString());
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            var IdYourSave = db.BDSEmpers.Where(x => x.IdAccountEm == idAcount).Select(x=>x.IdAccountPer).ToList();
             var register = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.ToList(),
                ListMarriea = db.BDSMarriages.ToList(),
                ListSalary = db.BDSSalaries.ToList(),
                ListDucation = db.BDSEducations.ToList(),
                ListBDSCareer = db.BDSCareers.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                TblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == idAcount),//!IdYourSave.Contains(x.ID)
                ListBDSPersonalInformation = db.BDSPersonalInformations.Where(x => x.Active == 1 && !IdYourSave.Contains(x.ID)).ToList(),
                TblBdsAdcount = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount),
                ListBDSEmper = db.BDSEmpers.ToList()
            };
            return View(register);
        }
         [ActionName("ManagementPersonal")]
        public ActionResult ManagementPersonal()
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
           
            return View();
        }
          [ActionName("YourArchive")]
        public ActionResult YourArchive()
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            var idAcount = int.Parse(Session["IdUser"].ToString());
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            var IdYourSave = db.BDSEmpers.Where(x => x.IdAccountEm == idAcount).Select(x => x.IdAccountPer).ToList();
            var register = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.ToList(),
                ListMarriea = db.BDSMarriages.ToList(),
                ListSalary = db.BDSSalaries.ToList(),
                ListDucation = db.BDSEducations.ToList(),
                ListBDSCareer = db.BDSCareers.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                TblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                ListBDSPersonalInformation = db.BDSPersonalInformations.Where(x => IdYourSave.Contains(x.ID) && x.Active==1).ToList(),
                TblBdsAdcount = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount),
                ListBDSEmper = db.BDSEmpers.ToList()
            };
            return View(register);
        }
        [ActionName("ApplyArchive")]
        public ActionResult ApplyArchive()
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            var idAcount = int.Parse(Session["IdUser"].ToString());
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            var IdYourSave = db.BDSApplies.Where(x => x.IdAccountEm == idAcount).Select(x => x.IdAccountPer).ToList();
            var register = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.ToList(),
                ListMarriea = db.BDSMarriages.ToList(),
                ListSalary = db.BDSSalaries.ToList(),
                ListDucation = db.BDSEducations.ToList(),
                ListBDSCareer = db.BDSCareers.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                TblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                ListBDSPersonalInformation = db.BDSPersonalInformations.Where(x => x.IdAccount == idAcount && x.Active == 1).ToList(),
                TblBdsAdcount = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount),
                ListBDSEmper = db.BDSEmpers.ToList(),
                ListBDSApply = db.BDSApplies.Where(x => x.IdAccountEm == idAcount).ToList()
            };
            return View(register);
        }
        [ActionName("Apply")]
        public ActionResult Apply(int id)
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            if (id==0)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            var idAcount = int.Parse(Session["IdUser"].ToString());
            var IdAccByNewId = db.BDSNews.Find(id).IdAcount;

            if (IdAccByNewId != null)
            {
                var tblApply = new BDSApply
                {
                    IdAccountEm = (int) IdAccByNewId,
                    IdAccountPer = idAcount,
                    Active = 1,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreateUser = 1,
                    ModifiedUser = 1,
                    IdNews = id
                };
                db.BDSApplies.Add(tblApply);
                db.SaveChanges();
            }
            return RedirectToAction("ThanksApply");
        }
        [ActionName("ThanksApply")]
        public ActionResult ThanksApply()
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            return View();
        }
        [ActionName("ManagementAcountEmployee")]
        public ActionResult ManagementAcountEmployee()
        {
          
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
               
                return RedirectToAction("LoginForm", "Login");

            }
            var idAcount = int.Parse(Session["IdUser"].ToString());
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            CaptCha cap = new CaptCha();
            BDSNew BDSNew = new BDSNew();
            var register = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.ToList(),
                ListMarriea = db.BDSMarriages.ToList(),
                ListSalary = db.BDSSalaries.ToList(),
                ListDucation = db.BDSEducations.ToList(),
                ListBDSCareer = db.BDSCareers.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,

                tblBDSNew = BDSNew,
                TblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                TblBdsAdcount = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount),
                ListBDSEmper = db.BDSEmpers.ToList()
            };
            return View(register);
        }
        [HttpPost,ActionName("ManagementAcountEmployee")]
        public ActionResult ManagementAcountEmployee(RegisterInformationModel register)
        {
          
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }  var idAcount = int.Parse(Session["IdUser"].ToString());
            BDSAccount TblBdsAdcount  = db.BDSAccounts.Find(register.TblBdsAdcount.ID);
            TblBdsAdcount.Email = register.TblBdsAdcount.Email;
            db.Entry(TblBdsAdcount).State = EntityState.Modified;
            db.SaveChanges();
            var idEmployee = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == register.TblBdsAdcount.ID);
            if (idEmployee != null)
            {
                BDSEmployerInformation TblBDSEmployerInformation = db.BDSEmployerInformations.Find(idEmployee.ID);
                TblBDSEmployerInformation.Name = register.TblBDSEmployerInformation.Name;
                TblBDSEmployerInformation.Address = register.TblBDSEmployerInformation.Address;
                TblBDSEmployerInformation.Phone = register.TblBDSEmployerInformation.Phone;
                TblBDSEmployerInformation.City = register.TblBDSEmployerInformation.City;
                TblBDSEmployerInformation.Scope = register.TblBDSEmployerInformation.Scope;
                TblBDSEmployerInformation.Description = register.TblBDSEmployerInformation.Description;
                TblBDSEmployerInformation.UrlImage = register.TblBDSEmployerInformation.UrlImage;
                TblBDSEmployerInformation.Fax = register.TblBDSEmployerInformation.Fax;
                TblBDSEmployerInformation.WebSite = register.TblBDSEmployerInformation.WebSite;
                TblBDSEmployerInformation.NameContact = register.TblBDSEmployerInformation.NameContact;
                TblBDSEmployerInformation.EmailContact = register.TblBDSEmployerInformation.EmailContact;
                TblBDSEmployerInformation.AddressContact = register.TblBDSEmployerInformation.AddressContact;
                TblBDSEmployerInformation.PhoneContact = register.TblBDSEmployerInformation.PhoneContact;
                TblBDSEmployerInformation.TypeContact = register.TblBDSEmployerInformation.TypeContact;

                db.Entry(TblBDSEmployerInformation).State = EntityState.Modified;
                db.SaveChanges();
            }
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            CaptCha cap = new CaptCha();
            BDSNew BDSNew = new BDSNew();
          
            var register1 = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.ToList(),
                ListMarriea = db.BDSMarriages.ToList(),
                ListSalary = db.BDSSalaries.ToList(),
                ListDucation = db.BDSEducations.ToList(),
                ListBDSCareer = db.BDSCareers.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,

                tblBDSNew = BDSNew,
                TblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                TblBdsAdcount = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount),
                ListBDSEmper = db.BDSEmpers.ToList()
            };
            return View(register1);
        }
        [HttpPost, ActionName("SaveYourArchive")]
        public ActionResult SaveYourArchive()
        {
            return Json("");
        }
        [HttpPost, ActionName("CheckEmail")]
        public ActionResult CheckEmail(string Email)
        {
            var dataAcount = db.BDSAccounts.FirstOrDefault(x => x.Email == Email);
            var sesIdAccount =int.Parse( Session["IdUser"].ToString());
            if (dataAcount != null && dataAcount.ID == sesIdAccount)
            {
                
                    return Json(new{result=1});
                
            }
            else
            {
                return Json(new { result = 0 });
            }

        }
         [HttpPost, ActionName("ChangePass")]
        public ActionResult ChangePass(string Email,string oldPass,string newpass)
         {
             var dataAcount = db.BDSAccounts.FirstOrDefault(x => x.Email == Email && x.PassWord == oldPass);
             if (dataAcount!=null )
             {
                var tblAcount= db.BDSAccounts.Find(dataAcount.ID);
                 tblAcount.PassWord = newpass;
                 db.Entry(tblAcount).State = EntityState.Modified;
                 db.SaveChanges();
                 return Json(new { result = 1 });
             }
             else
             {
                 return Json(new { result = 0 });
             }
            
        }

        public ActionResult ForgetPass()
        {
            return View();

        }
        [HttpPost, ActionName("ForgetPass1")]
        public ActionResult ForgetPass1(string email)
        {
            var augen = Guid.NewGuid();
            var newpass = augen.ToString().Substring(0, 8);
            var idAcount = db.BDSAccounts.FirstOrDefault(x=>x.Email==email);
            if (idAcount!=null)
            {
                var tblAcount = db.BDSAccounts.Find(idAcount.ID);
                tblAcount.PassWord = newpass;
                db.Entry(tblAcount).State = EntityState.Modified;
                db.SaveChanges();
                var smtp = new SmtpClient();

                var message = new MailMessage()
                {
                    BodyEncoding = new UTF8Encoding(),
                    Subject = "Mail nhận mật khẩu mới",
                    IsBodyHtml = true
                };
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential("tien131091@gmail.com", "Doilanhuthe1");

                message.Body = newpass;
                message.To.Add(email);

                smtp.Send(message);



                return Json(new { result=1});
            }
            else
            {
                return Json(new { result = 0 });
            }
           

        }
        public ActionResult Payment(string email)
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            return View();

        }

        public JsonResult AjaxPayment(double? money)
        {
            double M = money.Value, P = 0, ME = 0;
            DateTime dateNow = DateTime.Now;
            var ev= db.BDSEvents.Where(T => T.Active == 1 && T.FromDate <= dateNow && dateNow <= T.ToDate)
                .OrderByDescending(T => T.FromDate)
                .FirstOrDefault();
            if (ev!=null)
            {
                ME = money.Value*(double) ev.DisPercent/100;
            }
            return Json(new {M = M, P = P, ME = ME},JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("DetailPersonal")]
        public ActionResult DetailPersonal(int id)
        {
            var idAcount = int.Parse(Session["IdUser"].ToString());
            var point = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount).Point;
            if (point>3)
            {
                var tblBDSAccounts = db.BDSAccounts.Find(idAcount);
                point = point - 3;
                tblBDSAccounts.Point = point;
                db.Entry(tblBDSAccounts).State = EntityState.Modified;
                db.SaveChanges();
                var tblBDSEmpers = new BDSEmper();
               
                tblBDSEmpers.IdAccountEm = idAcount;
                tblBDSEmpers.IdAccountPer = id;
                tblBDSEmpers.Active = 1;
                tblBDSEmpers.CreateDate = DateTime.Now;
                tblBDSEmpers.ModifiedDate = DateTime.Now;
                tblBDSEmpers.CreateUser = 1;
                tblBDSEmpers.ModifiedUser = 1;
                db.BDSEmpers.Add(tblBDSEmpers);
                db.SaveChanges();
                var namePeson = db.BDSPersonalInformations.Find(id);
                return Json(new { result = 1, name = namePeson.Name.UrlFrendly()+"-"+id });
               
            }
            else
            {
                return Json(new { result=0});
            }
           
        }
        public ActionResult DetailPer(string id)
        {
            var _id = int.Parse(id.Split('-').Last());
            return View();
        }
    }
}

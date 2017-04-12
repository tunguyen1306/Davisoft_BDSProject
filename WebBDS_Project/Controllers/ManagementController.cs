using System;
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
            var dataCity = from data in db.states
                           join datatext in db.statetexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            var register = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListMarriea = db.bdsmarriages.ToList(),
                ListSalary = db.bdssalaries.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListBdscareer = db.bdscareers.ToList(),
                ListTimework = db.bdstimeworks.ToList(),
                Listbdslanguage = db.bdslanguages.ToList(),
                Listbdsnewstype = db.bdsnewstypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                TblBdsemployerinformation = db.bdsemployerinformations.FirstOrDefault(x => x.IdAccount == idAcount),
                Listbdspersonalinformation = db.bdspersonalinformations.ToList(),
                TblBdsAdcount = db.bdsaccounts.FirstOrDefault(x => x.Id == idAcount),
                Listbdsemper = db.bdsempers.ToList()
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
            var dataCity = from data in db.states
                           join datatext in db.statetexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            var register = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListMarriea = db.bdsmarriages.ToList(),
                ListSalary = db.bdssalaries.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListBdscareer = db.bdscareers.ToList(),
                ListTimework = db.bdstimeworks.ToList(),
                Listbdslanguage = db.bdslanguages.ToList(),
                Listbdsnewstype = db.bdsnewstypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                TblBdsemployerinformation = db.bdsemployerinformations.FirstOrDefault(x => x.IdAccount == idAcount),
                Listbdspersonalinformation = db.bdspersonalinformations.Where(x => x.IdAccount == idAcount).ToList(),
                TblBdsAdcount = db.bdsaccounts.FirstOrDefault(x => x.Id == idAcount),
                Listbdsemper = db.bdsempers.ToList()
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
            var dataCity = from data in db.states
                           join datatext in db.statetexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            CaptCha cap = new CaptCha();
            bdsnew bdsNew = new bdsnew();
            var register = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListMarriea = db.bdsmarriages.ToList(),
                ListSalary = db.bdssalaries.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListBdscareer = db.bdscareers.ToList(),
                ListTimework = db.bdstimeworks.ToList(),
                Listbdslanguage = db.bdslanguages.ToList(),
                Listbdsnewstype = db.bdsnewstypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,

                tblbdsnew = bdsNew,
                TblBdsemployerinformation = db.bdsemployerinformations.FirstOrDefault(x => x.IdAccount == idAcount),
                TblBdsAdcount = db.bdsaccounts.FirstOrDefault(x => x.Id == idAcount),
                Listbdsemper = db.bdsempers.ToList()
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
            bdsaccount TblBdsAdcount  = db.bdsaccounts.Find(register.TblBdsAdcount.Id);
            TblBdsAdcount.Email = register.TblBdsAdcount.Email;
            db.Entry(TblBdsAdcount).State = EntityState.Modified;
            db.SaveChanges();
            var idEmployee = db.bdsemployerinformations.FirstOrDefault(x => x.IdAccount == register.TblBdsAdcount.Id);
            if (idEmployee != null)
            {
                bdsemployerinformation Tblbdsemployerinformation = db.bdsemployerinformations.Find(idEmployee.Id);
                Tblbdsemployerinformation.Name = register.TblBdsemployerinformation.Name;
                Tblbdsemployerinformation.Address = register.TblBdsemployerinformation.Address;
                Tblbdsemployerinformation.Phone = register.TblBdsemployerinformation.Phone;
                Tblbdsemployerinformation.City = register.TblBdsemployerinformation.City;
                Tblbdsemployerinformation.Scope = register.TblBdsemployerinformation.Scope;
                Tblbdsemployerinformation.Description = register.TblBdsemployerinformation.Description;
                Tblbdsemployerinformation.UrlImage = register.TblBdsemployerinformation.UrlImage;
                Tblbdsemployerinformation.Fax = register.TblBdsemployerinformation.Fax;
                Tblbdsemployerinformation.WebSite = register.TblBdsemployerinformation.WebSite;
                Tblbdsemployerinformation.NameContact = register.TblBdsemployerinformation.NameContact;
                Tblbdsemployerinformation.EmailContact = register.TblBdsemployerinformation.EmailContact;
                Tblbdsemployerinformation.AddressContact = register.TblBdsemployerinformation.AddressContact;
                Tblbdsemployerinformation.PhoneContact = register.TblBdsemployerinformation.PhoneContact;
                Tblbdsemployerinformation.TypeContact = register.TblBdsemployerinformation.TypeContact;

                db.Entry(Tblbdsemployerinformation).State = EntityState.Modified;
                db.SaveChanges();
            }
            var dataCity = from data in db.states
                           join datatext in db.statetexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            CaptCha cap = new CaptCha();
            bdsnew bdsNew = new bdsnew();
            var register1 = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListMarriea = db.bdsmarriages.ToList(),
                ListSalary = db.bdssalaries.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListBdscareer = db.bdscareers.ToList(),
                ListTimework = db.bdstimeworks.ToList(),
                Listbdslanguage = db.bdslanguages.ToList(),
                Listbdsnewstype = db.bdsnewstypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,

                tblbdsnew = bdsNew,
                TblBdsemployerinformation = db.bdsemployerinformations.FirstOrDefault(x => x.IdAccount == idAcount),
                TblBdsAdcount = db.bdsaccounts.FirstOrDefault(x => x.Id == idAcount),
                Listbdsemper = db.bdsempers.ToList()
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
            var dataAcount = db.bdsaccounts.FirstOrDefault(x => x.Email == Email);
            var sesIdAccount =int.Parse( Session["IdUser"].ToString());
            if (dataAcount != null && dataAcount.Id == sesIdAccount)
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
             var dataAcount = db.bdsaccounts.FirstOrDefault(x => x.Email == Email && x.PassWord == oldPass);
             if (dataAcount!=null )
             {
                var tblAcount= db.bdsaccounts.Find(dataAcount.Id);
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
            var idAcount = db.bdsaccounts.FirstOrDefault(x=>x.Email==email);
            if (idAcount!=null)
            {
                var tblAcount = db.bdsaccounts.Find(idAcount.Id);
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
        public ActionResult Paymen(string email)
        {
            return View();

        }
    }
}

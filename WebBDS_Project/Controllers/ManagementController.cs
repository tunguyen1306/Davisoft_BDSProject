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
        davisoft_bdsprojectEntities1 db = new davisoft_bdsprojectEntities1();
        RegisterController registerController=new RegisterController();
         [ActionName("ManagementCompany")]
        public ActionResult ManagementCompany()
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            var idAcount = int.Parse(Session["IdUser"].ToString());
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
            var dataDist = (from data in db.Districts
                            join datatext in db.DistrictTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi"
                            select new GeoModel { DistId = data.district_id, DistName = datatext.text }).ToList();
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
                 ListGeoDisModel = dataDist.ToList(),
                TblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == idAcount),//!IdYourSave.Contains(x.ID)
                ListBDSPersonalInformation = db.BDSPersonalInformations.Where(x => x.Active == 1 && !IdYourSave.Contains(x.ID)).ToList(),
                TblBdsAdcount = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount),
                ListBDSEmper = db.BDSEmpers.ToList()
            };
            return View(register);
        }
         public PartialViewResult PListNewsOfUser(string tt, int from = 0, int view = 6, int page = 1)
         {
             ViewBag.from = from;
             ViewBag.view = view;
             ViewBag.page = page;
             ViewBag.tt = tt;

             return PartialView();
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
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
            var IdYourSave = db.BDSEmpers.Where(x => x.IdAccountEm == idAcount).Select(x => x.IdAccountPer).ToList();
            var IdYourSavePer = db.BDSEmpers.Where(x => x.IdAccountPer == idAcount).Select(x => x.IdAccountEm).ToList();
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
                ListBDSEmployerInformation = db.BDSEmployerInformations.Where(x => IdYourSavePer.Contains(x.ID)).ToList(),
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
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
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
                    IdNews = id,
                    TypeProfile=1,
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
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
            var dataDist  =( from data in db.Districts
                           join datatext in db.DistrictTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { DistId = data.district_id, DistName = datatext.text }).ToList();
            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataDist.Insert(0, new GeoModel { DistId = 0, DistName = "Chọn quận/huyện" });
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
                ListGeoDisModel= dataDist.ToList(),
                tblCaptCha = cap,
                tblBDSNew = BDSNew,
                TblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                TblBDSPersonalInformation = db.BDSPersonalInformations.FirstOrDefault(x => x.IdAccount == idAcount),
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
            }
            if (Session["IdUserEmployee"] != null)
            {
                var idAcount = int.Parse(Session["IdUser"].ToString());
                BDSAccount TblBdsAdcount = db.BDSAccounts.Find(register.TblBdsAdcount.ID);
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
                var dataCity = (from data in db.States
                                join datatext in db.StateTexts on data.name_id equals datatext.id
                                where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                                select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

                dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
                dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
                dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
                var dataDist = (from data in db.Districts
                                join datatext in db.DistrictTexts on data.name_id equals datatext.id
                                where datatext.language_id == "vi"
                                select new GeoModel { DistId = data.district_id, DistName = datatext.text }).ToList();
                dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
                dataDist.Insert(0, new GeoModel { DistId = 0, DistName = "Chọn quận/huyện" });
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
                    ListGeoDisModel = dataDist.ToList(),
                    tblCaptCha = cap,
                    tblBDSNew = BDSNew,
                    TblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                    TblBDSPersonalInformation = db.BDSPersonalInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                    TblBdsAdcount = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount),
                    ListBDSEmper = db.BDSEmpers.ToList()
                };
                return View(register1);

            }
            if (Session["IdUserPer"] != null)
            {
                var idAcount = int.Parse(Session["IdUser"].ToString());
                BDSAccount TblBdsAdcount = db.BDSAccounts.Find(idAcount);
                TblBdsAdcount.Email = register.TblBdsAdcount.Email;
                db.Entry(TblBdsAdcount).State = EntityState.Modified;
                db.SaveChanges();
                var idEmployee = db.BDSPersonalInformations.FirstOrDefault(x => x.IdAccount == idAcount);
                if (idEmployee != null)
                {
                    BDSPersonalInformation BDSPersonalInformation = db.BDSPersonalInformations.Find(idEmployee.ID);
                    BDSPersonalInformation.Name = register.TblBDSPersonalInformation.Name;
                    BDSPersonalInformation.TemporaryAddress = register.TblBDSPersonalInformation.TemporaryAddress;
                    BDSPersonalInformation.Province = register.TblBDSPersonalInformation.Province;
                    BDSPersonalInformation.PermanentAddress = register.TblBDSPersonalInformation.PermanentAddress;
                    BDSPersonalInformation.Phone = register.TblBDSPersonalInformation.Phone;
                   // BDSPersonalInformation.City = register.TblBDSPersonalInformation.City;
                    BDSPersonalInformation.Description = register.TblBDSPersonalInformation.Description;
                    BDSPersonalInformation.UrlImage = register.TblBDSPersonalInformation.UrlImage;
                    BDSPersonalInformation.Birthday = register.TblBDSPersonalInformation.Birthday;
                    BDSPersonalInformation.Sex = register.TblBDSPersonalInformation.Sex;
                  //  BDSPersonalInformation.City= register.TblBDSPersonalInformation.City;
                 //   BDSPersonalInformation.District = register.TblBDSPersonalInformation.District;
                    BDSPersonalInformation.MaritalStatus = register.TblBDSPersonalInformation.MaritalStatus;
                  //  BDSPersonalInformation.Salary = register.TblBDSPersonalInformation.Salary;
                  //  BDSPersonalInformation.Experience = register.TblBDSPersonalInformation.Experience;
                  //  BDSPersonalInformation.Education = register.TblBDSPersonalInformation.Education;
                  //  BDSPersonalInformation.IdLoaiNghe = register.TblBDSPersonalInformation.IdLoaiNghe;
                  //  BDSPersonalInformation.ProfessionalExperience = register.TblBDSPersonalInformation.ProfessionalExperience;
                    db.Entry(BDSPersonalInformation).State = EntityState.Modified;
                    db.SaveChanges();
                }
                var dataCity = (from data in db.States
                                join datatext in db.StateTexts on data.name_id equals datatext.id
                                where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                                select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

                dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
                dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
                dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
                var dataDist = (from data in db.Districts
                                join datatext in db.DistrictTexts on data.name_id equals datatext.id
                                where datatext.language_id == "vi"
                                select new GeoModel { DistId = data.district_id, DistName = datatext.text }).ToList();
        
                dataDist.Insert(0, new GeoModel { DistId = 0, DistName = "Chọn quận/huyện" });
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
                    ListGeoDisModel = dataDist.ToList(),
                    tblCaptCha = cap,
                    tblBDSNew = BDSNew,
                    TblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                    TblBDSPersonalInformation = db.BDSPersonalInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                    TblBdsAdcount = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount),
                    ListBDSEmper = db.BDSEmpers.ToList()
                };
                return View(register1);
            }
            return null;
        }

  [HttpPost,ActionName("ManagementAcountPersonal")]
        public ActionResult ManagementAcountPersonal(RegisterInformationModel register)
        {
          
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }

            var idAcount = int.Parse(Session["IdUser"].ToString());
            BDSAccount TblBdsAdcount  = db.BDSAccounts.Find(idAcount);
            TblBdsAdcount.Email = register.TblBdsAdcount.Email;
            db.Entry(TblBdsAdcount).State = EntityState.Modified;
            db.SaveChanges();
            var idEmployee = db.BDSPersonalInformations.FirstOrDefault(x => x.IdAccount == idAcount);
            if (idEmployee != null)
            {
                BDSPersonalInformation BDSPersonalInformation = db.BDSPersonalInformations.Find(idEmployee.ID);
                BDSPersonalInformation.Name = register.TblBDSPersonalInformation.Name;
                BDSPersonalInformation.TemporaryAddress = register.TblBDSPersonalInformation.TemporaryAddress;
                BDSPersonalInformation.Province = register.TblBDSPersonalInformation.Province;
                BDSPersonalInformation.PermanentAddress = register.TblBDSPersonalInformation.PermanentAddress;
                BDSPersonalInformation.Phone = register.TblBDSPersonalInformation.Phone;
              //  BDSPersonalInformation.City = register.TblBDSPersonalInformation.City;
                BDSPersonalInformation.Description = register.TblBDSPersonalInformation.Description;
                BDSPersonalInformation.UrlImage = register.TblBDSPersonalInformation.UrlImage;
                db.Entry(BDSPersonalInformation).State = EntityState.Modified;
                db.SaveChanges();
            }
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
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
                TblBDSPersonalInformation = db.BDSPersonalInformations.FirstOrDefault(x => x.IdAccount == idAcount),
                TblBdsAdcount = db.BDSAccounts.FirstOrDefault(x => x.ID == idAcount),
                ListBDSEmper = db.BDSEmpers.ToList()
            };
            return View(register1);
        }



        [HttpPost, ActionName("SaveYourArchive")]
        public JsonResult SaveYourArchive(int? id)
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return Json(new {Status=false,Message="Vui lòng đăng nhập tài khoản nhà tuyển dụng!"}); ;
            } 
            var idAcount = int.Parse(Session["IdUser"].ToString());
            var emp= db.BDSEmployerInformations.FirstOrDefault(T => T.IdAccount == idAcount);
            if (emp==null)
            {
                return Json(new { Status = false, Message = "Vui lòng đăng nhập tài khoản nhà tuyển dụng!" }); ;
            }

            if (db.BDSEmpers.Where(T => T.IdAccountEm == emp.ID && T.IdAccountPer == id && T.Active == 1).Count()>0)
            {
                return Json(new { Status = false, Message = "Hồ sơ đã được lưu!" }); ;
            }



            BDSEmper em = new BDSEmper();
            em.Active = 1;
            em.CreateDate = DateTime.Now;
            em.CreateUser = 1;
            em.Name = "Lưu hồ sơ PER" + id.Value.ToString("000000") + "";
            em.IdAccountEm = emp.ID;
            em.IdAccountPer = id.Value;
            em.Description = em.Name;
          
            em.KeySearch = "";
            em.Point = 3;
            var account = db.BDSAccounts.FirstOrDefault(T => T.ID == idAcount);
            if (account.Point < em.Point)
            {
                return Json(new { Status = false, Message = "Tài khoản không đủ điểm!" });
            }
          
            String Fname = "Lưu hồ sơ '{A}' điểm phải trả {B}";
            string name = Fname.Replace("{A}", "PER" + id.Value.ToString("000000") + "").Replace("{B}", em.Point.ToString());
            BDSTransactionHistory tranhist = new BDSTransactionHistory
            {
                Name = name,
                Description = name,
                KeySearch = name.NormalizeD(),
                Active = 1,
                CreateUser = 1,
                CreateDate = DateTime.Now,
                TypeTran = 2,
                PointTran = em.Point.Value,
                MoneyTran =0,
                DateTran = DateTime.Now,
                Status = 0
                
            };
            db.Entry(tranhist).State = EntityState.Added;

            db.SaveChanges();

            em.RefTranHis = tranhist.ID;

            db.BDSEmpers.Add(em);

           
            account.Point = account.Point - em.Point;
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { Status = true, Message = "Lưu hồ sơ thành công!" });
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
           
            var idAcount = db.BDSAccounts.FirstOrDefault(x=>x.Email==email);
            if (idAcount!=null)
            {
               
                registerController.SendTemplateEmail(email, idAcount.Email, idAcount.Token,"Láy lại mật khẩu",2);
                return Json(new { result=1});
            }
            else
            {
                return Json(new { result = 0 });
            }
           

        }
        public ActionResult Payment()
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
            var ev = db.BDSEvents.Where(T => T.Active == 1 && T.FromDate <= dateNow && dateNow <= T.ToDate)
                .OrderByDescending(T => T.FromDate)
                .FirstOrDefault();
            if (ev != null)
            {
                ME = money.Value * (double)ev.DisPercent / 100;
            }
            return Json(new { M = M, P = P, ME = ME }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MoneyToPoint()
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }

            ViewBag.Error = false;
            return View();

        }
        [HttpPost]
        public ActionResult MoneyToPoint(double sotien)
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            int id = (int)Session["IdUser"];
            var account=db.BDSAccounts.First(T => T.ID == id);
            if (account.Money.Value >= sotien)
            {
                var P = 0.0;
                 var setting=  db.Settings.FirstOrDefault(T => T.Name == "MoneyToPoint");
                if (setting==null)
                {
                    P =sotien;
                }
                else
                {
                    P =   Math.Ceiling( sotien/10000 * double.Parse(setting.Value));
                }
              
                String Fname = "Đổi '{A}' VND thành '{B}' điểm";
                string name = Fname.Replace("{A}", sotien.ToString("n2")).Replace("{B}",P.ToString("n2") );
                BDSTransactionHistory tran = new BDSTransactionHistory()
                {
                    Name = name,
                    Description = name,
                    KeySearch = name.NormalizeD(),
                    Active = 1,
                    CreateUser = 1,
                    CreateDate = DateTime.Now,
                    TypeTran = 3,
                    PointTran = (int)P,
                    MoneyTran = sotien,
                    DateTran = DateTime.Now,
                    Status =1

                   
                   
                };
                db.BDSTransactionHistories.Add(tran);
                account.Money = account.Money - sotien;
                account.Point = account.Point + (int) P;
                db.Entry(account).State=EntityState.Modified;
                db.SaveChanges();
                ViewBag.Error = false;
                return RedirectToAction("ManagementAcountEmployee");
            }
            else
            {
                ViewBag.Error = true;
            }

            return View();

        }
        public JsonResult AjaxMoneyToPoint(double? money)
        {
            double M = money.Value, P = 0, ME = 0;
            var setting=  db.Settings.FirstOrDefault(T => T.Name == "MoneyToPoint");
            if (setting==null)
            {
                P = money.Value;
            }
            else
            {
                P =   Math.Ceiling( money.Value/10000 * double.Parse(setting.Value));
            }
            return Json(new { M = M, P = P, ME = ME }, JsonRequestBehavior.AllowGet);
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
            var id_ = int.Parse(id.Split('-').Last());
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new ListCityNew { Id = data.state_id, Name = datatext.text }).ToList();

            dataCity.Insert(0, new ListCityNew { Id = 0, Name = "Chọn thành/phố" });
            dataCity.Insert(0, new ListCityNew { Id = 59, Name = "TP.Hồ Chí Minh" });
            dataCity.Insert(1, new ListCityNew { Id = 28, Name = "TP.Hà Nội" });

            var dataDist = (from data in db.Districts
                            join datatext in db.DistrictTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi"
                            select new ListCityNew { Id = data.district_id, Name = datatext.text }).ToList();

            var Model = new NewsModel
            {
                tblBDSPersonalInformation = db.BDSPersonalInformations.FirstOrDefault(x => x.ID == id_),
                ListBDSAccount = db.BDSAccounts.ToList(),
                ListCityText = dataCity.ToList(),
                ListDisText = dataDist.ToList(),
                ListDucation = db.BDSEducations.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSCareer=db.BDSCareers.ToList(),
                ListBDSSalary = db.BDSSalaries.ToList()


            };
           
            return View(Model);
        }
        public ActionResult ListNewOfUser()
        {
            
            return View();
        }
        public PartialViewResult PManaTinDacBiet(string tt="", int from = 0, int view = 12, int page = 1)
        {
            ViewBag.from = from;
            ViewBag.view = view;
            ViewBag.page = page;
            ViewBag.tt = tt;

            return PartialView();
        }
        public PartialViewResult PManaTinNoiBat(string tt = "", int from = 0, int view = 6, int page = 1)
        {
            ViewBag.from = from;
            ViewBag.view = view;
            ViewBag.page = page;
            ViewBag.tt = tt;

            return PartialView();
        }

        public PartialViewResult PManaTinMoiNhat(string tt = "", int from = 0, int view = 5, int page = 1)
        {
            ViewBag.from = from;
            ViewBag.view = view;
            ViewBag.page = page;
            ViewBag.tt = tt;

            return PartialView();
        }
        public PartialViewResult PManaTinNhaTuyenDung(string tt = "", int from = 0, int view = 5, int page = 1)
        {
            ViewBag.from = from;
            ViewBag.view = view;
            ViewBag.page = page;
            ViewBag.tt = tt;

            return PartialView();
        }
    }
}

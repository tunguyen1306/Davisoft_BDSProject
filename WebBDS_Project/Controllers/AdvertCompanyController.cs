using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageResizer;
using WebBDS_Project.Models;

namespace WebBDS_Project.Controllers
{
    public class AdvertCompanyController : Controller
    {
        davisoft_bdsprojectEntities1 db = new davisoft_bdsprojectEntities1();
        //
        // GET: /AdvertCompany/

        public ActionResult CreateAdvertCompany()
        {
            int id = 26;
            var tblAccount = db.BDSAccounts.FirstOrDefault(T => T.ID == id);
            var tblPer = db.BDSPersonalInformations.FirstOrDefault(T => T.IdAccount == tblAccount.ID);
            var cuurentINews = db.BDSPerNews.FirstOrDefault(T => T.PerId == tblPer.ID && T.Active==1);
            List<BDSPerNews_Degrees> ListPerNewDegrees=new List<BDSPerNews_Degrees>();
            List<BDSPerNews_Experiences> ListPerNewExperiences=new List<BDSPerNews_Experiences>();
            List<BDSPerNews_LangDegrees> ListPerNewLangDegrees=new List<BDSPerNews_LangDegrees>();
            List<BDSPerNews_References> ListPerNewReferences=new List<BDSPerNews_References>();
            if (cuurentINews==null)
            {
                cuurentINews = new BDSPerNew { Active = 1, CreateDate = DateTime.Now, CreateUser = 1, PerId = tblPer.ID};
                db.Entry(cuurentINews).State=EntityState.Added;
                db.SaveChanges();
                ListPerNewDegrees.Add(new BDSPerNews_Degrees { ID_BDSPerNews = cuurentINews .ID});
                ListPerNewExperiences.Add(new BDSPerNews_Experiences { ID_BDSPerNews = cuurentINews.ID });
                ListPerNewLangDegrees.Add(new BDSPerNews_LangDegrees { ID_BDSPerNews = cuurentINews.ID });
                ListPerNewReferences.Add(new BDSPerNews_References { ID_BDSPerNews = cuurentINews.ID });
            }
            else
            {
                ListPerNewDegrees = db.BDSPerNews_Degrees.Where(T=>T.ID_BDSPerNews==cuurentINews.ID).ToList();
                ListPerNewExperiences = db.BDSPerNews_Experiences.Where(T => T.ID_BDSPerNews == cuurentINews.ID).ToList();
                ListPerNewLangDegrees = db.BDSPerNews_LangDegrees.Where(T => T.ID_BDSPerNews == cuurentINews.ID).ToList();
                ListPerNewReferences = db.BDSPerNews_References.Where(T => T.ID_BDSPerNews == cuurentINews.ID).ToList();
                if (ListPerNewDegrees.Count==0)
                {
                    ListPerNewDegrees.Add(new BDSPerNews_Degrees { ID_BDSPerNews = cuurentINews.ID });
                }
                if (ListPerNewExperiences.Count == 0)
                {
                    ListPerNewExperiences.Add(new BDSPerNews_Experiences { ID_BDSPerNews = cuurentINews.ID });
                }
                if (ListPerNewLangDegrees.Count == 0)
                {
                    ListPerNewLangDegrees.Add(new BDSPerNews_LangDegrees { ID_BDSPerNews = cuurentINews.ID });
                }
                if (ListPerNewReferences.Count == 0)
                {
                    ListPerNewReferences.Add(new BDSPerNews_References { ID_BDSPerNews = cuurentINews.ID });
                }
            }
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
       
        
       
            var registerModel = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.Where(T=>T.Active==1).ToList(),
                ListMarriea = db.BDSMarriages.Where(T => T.Active == 1).ToList(),
                ListSalary = db.BDSSalaries.Where(T => T.Active == 1).ToList(),
                ListDucation = db.BDSEducations.Where(T => T.Active == 1).ToList(),
                ListBDSCareer = db.BDSCareers.Where(T => T.Active == 1).ToList(),
                ListTimework = db.BDSTimeWorks.Where(T => T.Active == 1).ToList(),
                ListBDSLanguage = db.BDSLanguages.Where(T => T.Active == 1).ToList(),
                ListBDSNewsType = db.BDSNewsTypes.Where(T => T.Active == 1).OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity,

                ListBdsAdcount = db.BDSAccounts.Where(T => T.Active == 1).ToList(),
                ListBDSEmper = db.BDSEmpers.Where(T => T.Active == 1).ToList(),
                ListPerNewDegrees = ListPerNewDegrees,
                ListPerNewExperiences = ListPerNewExperiences,
                ListPerNewLangDegrees=ListPerNewLangDegrees,
                ListPerNewReferences=ListPerNewReferences,
                tblBDSPerNew = cuurentINews,
                TblBDSPersonalInformation = tblPer,
                TblBdsAdcount = tblAccount
            };
            return View(registerModel);
        }

        [HttpPost]
        public ActionResult CreateAdvertCompany(RegisterInformationModel model,int? type)
        {
            model.Msg = "Cập nhật dữ liệu thành công!";
            model.Status = true;
            try
            {
                switch (type)
                {
                    case 1:
                        String path = SaveFile(Request.Files["TblBDSPersonalInformation.File"]);
                        if (path != "")
                        {
                            model.TblBDSPersonalInformation.UrlImage = path;
                        }
                        var tblPer = db.BDSPersonalInformations.FirstOrDefault(T => T.ID == model.TblBDSPersonalInformation.ID);
                        tblPer.Sex = model.TblBDSPersonalInformation.Sex;
                        tblPer.Name = model.TblBDSPersonalInformation.Name;
                        tblPer.Phone = model.TblBDSPersonalInformation.Phone;
                        tblPer.Birthday = model.TblBDSPersonalInformation.Birthday;
                        tblPer.Province = model.TblBDSPersonalInformation.Province;
                        tblPer.TemporaryAddress = model.TblBDSPersonalInformation.TemporaryAddress;
                        tblPer.PermanentAddress = model.TblBDSPersonalInformation.PermanentAddress;
                        tblPer.UrlImageCheck = model.TblBDSPersonalInformation.UrlImageCheck;
                        tblPer.UrlImage = model.TblBDSPersonalInformation.UrlImage;
                        db.Entry(tblPer).State = EntityState.Modified;
                        db.SaveChanges();
                        model.TblBDSPersonalInformation = tblPer;
                        break;
                    case 2:
                        var cuurentINews = db.BDSPerNews.FirstOrDefault(T => T.ID == model.tblBDSPerNew.ID);
                        cuurentINews.TitleProfile = model.tblBDSPerNew.TitleProfile;
                        cuurentINews.CareerGoalProfile = model.tblBDSPerNew.CareerGoalProfile;
                        cuurentINews.CareerProfile = model.tblBDSPerNew.CareerProfile;
                        cuurentINews.EducationProfile = model.tblBDSPerNew.EducationProfile;
                        cuurentINews.ExperienceProfile = model.tblBDSPerNew.ExperienceProfile;
                        cuurentINews.LanguageProfile = model.tblBDSPerNew.LanguageProfile;
                        cuurentINews.LevelProfile = model.tblBDSPerNew.LevelProfile;
                        cuurentINews.ProvinceProfile = model.tblBDSPerNew.ProvinceProfile;
                        cuurentINews.ModifiedDate = DateTime.Now;
                        cuurentINews.ModifiedUser = 1;
                        db.Entry(cuurentINews).State = EntityState.Modified;
                        db.SaveChanges();
                        model.tblBDSPerNew = cuurentINews;
                        break;
                    case 3:
                        cuurentINews = db.BDSPerNews.FirstOrDefault(T => T.ID == model.tblBDSPerNew.ID);
                        cuurentINews.Skills = model.tblBDSPerNew.Skills;
                        cuurentINews.Hobby = model.tblBDSPerNew.Hobby;
                        cuurentINews.MSExcelOffice = model.tblBDSPerNew.MSExcelOffice;
                        cuurentINews.MSWordOffice = model.tblBDSPerNew.MSWordOffice;
                        cuurentINews.MSPowerPointOffice = model.tblBDSPerNew.MSPowerPointOffice;
                        cuurentINews.OutlookOffice = model.tblBDSPerNew.OutlookOffice;
                        cuurentINews.OrderOffice = model.tblBDSPerNew.OrderOffice;
                        cuurentINews.ModifiedDate = DateTime.Now;
                        cuurentINews.ModifiedUser = 1;
                        db.Entry(cuurentINews).State = EntityState.Modified;
                        db.SaveChanges();
                        model.tblBDSPerNew = cuurentINews;
                        for (int i = 0; i < model.ListPerNewDegrees.Count; i++)
                        {
                            var itemD = model.ListPerNewDegrees[i];
                            path = SaveFile(Request.Files["ListPerNewDegrees[" + i + "].File"]);
                            if (path != "")
                            {
                                itemD.ImageUrl = path;
                            }
                            var ccItem = db.BDSPerNews_Degrees.FirstOrDefault(T => T.ID == itemD.ID);
                            if (ccItem == null)
                            {
                                ccItem = new BDSPerNews_Degrees { Active = 1, CreateDate = DateTime.Now, CreateUser = 1, Name = itemD.Name, ID_BDSPerNews = cuurentINews.ID, Career = itemD.Career, FromYear = itemD.FromYear, ToYear = itemD.ToYear, ImageUrl = itemD.ImageUrl, Level = itemD.Level, Rating = itemD.Level };
                                db.Entry(ccItem).State = EntityState.Added;
                                db.SaveChanges();
                                model.ListPerNewDegrees[i].ID = ccItem.ID;
                            }
                            else
                            {
                                ccItem.Level = itemD.Level;
                                ccItem.Name = itemD.Name;
                                ccItem.Career = itemD.Career;
                                ccItem.FromYear = itemD.FromYear;
                                ccItem.ToYear = itemD.ToYear;
                                ccItem.Rating = itemD.Rating;
                                ccItem.ImageUrl = itemD.ImageUrl;
                                ccItem.ModifiedDate = DateTime.Now;
                                ccItem.ModifiedUser = 1;
                                ccItem.Active = itemD.Active;
                                db.Entry(ccItem).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        for (int i = 0; i < model.ListPerNewLangDegrees.Count; i++)
                        {
                            var itemD = model.ListPerNewLangDegrees[i];
                            var ccItem = db.BDSPerNews_LangDegrees.FirstOrDefault(T => T.ID == itemD.ID);
                            if (ccItem == null)
                            {
                                ccItem = new BDSPerNews_LangDegrees { Active = 1, CreateUser = 1, CreateDate = DateTime.Now, ID_BDSPerNews = cuurentINews.ID, Language = itemD.Language, Name = itemD.Name, Listening = itemD.Listening, Reading = itemD.Reading, Speaking = itemD.Speaking, Writing = itemD.Writing, Decription = itemD.Decription };
                                db.Entry(ccItem).State = EntityState.Added;
                                db.SaveChanges();
                                model.ListPerNewLangDegrees[i].ID = ccItem.ID;
                            }
                            else
                            {
                                ccItem.Language = itemD.Language;
                                ccItem.Name = itemD.Name;
                                ccItem.Listening = itemD.Listening;
                                ccItem.Speaking = itemD.Speaking;
                                ccItem.Reading = itemD.Reading;
                                ccItem.Writing = itemD.Writing;
                                ccItem.Decription = itemD.Decription;
                                ccItem.ModifiedDate = DateTime.Now;
                                ccItem.ModifiedUser = 1;
                                ccItem.Active = itemD.Active;
                                db.Entry(ccItem).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        break;
                    case 4:
                        for (int i = 0; i < model.ListPerNewExperiences.Count; i++)
                        {
                            var itemD = model.ListPerNewExperiences[i];

                            var ccItem = db.BDSPerNews_Experiences.FirstOrDefault(T => T.ID == itemD.ID);
                            if (ccItem == null)
                            {
                                ccItem = new BDSPerNews_Experiences
                                {
                                    Active = 1,
                                    CreateUser = 1,
                                    CreateDate = DateTime.Now,
                                    ID_BDSPerNews = model.tblBDSPerNew.ID,
                                    Name = itemD.Name,
                                    Level = itemD.Level,
                                    Decription = itemD.Decription,
                                    Perfix = itemD.Perfix,
                                    Salary = itemD.Salary,
                                    FromDate = itemD.FromDate,
                                    ToDate = itemD.ToDate,

                                };
                                db.Entry(ccItem).State = EntityState.Added;
                                db.SaveChanges();
                                model.ListPerNewExperiences[i].ID = ccItem.ID;
                            }
                            else
                            {
                                ccItem.Name = itemD.Name;
                                ccItem.Level = itemD.Level;
                                ccItem.Decription = itemD.Decription;
                                ccItem.Perfix = itemD.Perfix;
                                ccItem.Salary = itemD.Salary;
                                ccItem.FromDate = itemD.FromDate;
                                ccItem.ToDate = itemD.ToDate;
                                ccItem.ModifiedDate = DateTime.Now;
                                ccItem.ModifiedUser = 1;
                                ccItem.Active = itemD.Active;
                                db.Entry(ccItem).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        break;
                    case 5:
                        for (int i = 0; i < model.ListPerNewReferences.Count; i++)
                        {
                            var itemD = model.ListPerNewReferences[i];

                            var ccItem = db.BDSPerNews_References.FirstOrDefault(T => T.ID == itemD.ID);
                            if (ccItem == null)
                            {
                                ccItem = new BDSPerNews_References
                                {
                                    Active = 1,
                                    CreateUser = 1,
                                    CreateDate = DateTime.Now,
                                    ID_BDSPerNews = model.tblBDSPerNew.ID,
                                    Name = itemD.Name,
                                    NameCompany = itemD.NameCompany,
                                    Decription = itemD.Decription,
                                    Phone = itemD.Phone,


                                };
                                db.Entry(ccItem).State = EntityState.Added;
                                db.SaveChanges();
                                model.ListPerNewReferences[i].ID = ccItem.ID;
                            }
                            else
                            {
                                ccItem.Name = itemD.Name;
                                ccItem.NameCompany = itemD.NameCompany;
                                ccItem.Decription = itemD.Decription;
                                ccItem.Phone = itemD.Phone;
                                ccItem.ModifiedDate = DateTime.Now;
                                ccItem.ModifiedUser = 1;
                                ccItem.Active = itemD.Active;
                                db.Entry(ccItem).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        break;
                    case 6:
                        cuurentINews = db.BDSPerNews.FirstOrDefault(T => T.ID == model.tblBDSPerNew.ID);
                        path = SaveFile(Request.Files["tblBDSPerNew.File"]);
                        if (path != "")
                        {
                            model.tblBDSPerNew.FileUrl = path;
                        }
                        cuurentINews.FileUrl = model.tblBDSPerNew.FileUrl;
                        cuurentINews.ModifiedDate = DateTime.Now;
                        cuurentINews.ModifiedUser = 1;
                        db.Entry(cuurentINews).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                }
            }
            catch (Exception)
            {
                model.Msg = "Cập nhật dữ liệu thất bại!";
                model.Status = false;
            }
            
            return Json(model);
        }

        public string SaveFile(HttpPostedFileBase file)
        {
         

           
      
          
            if (file != null && file.ContentLength > 0)
            {
                var fileNameFull = file.FileName;
                var fileName = Path.GetFileName(file.FileName);
                string newFileNmae = Path.GetFileNameWithoutExtension(fileName);
                var fortmatName = Path.GetExtension(fileName);
                var  NewPath = newFileNmae.Replace(newFileNmae, (DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()).ToString());
                fileNameFull = DateTime.Now.Day + "" + DateTime.Now.Month + "_" + NewPath + fortmatName;
                var path = Server.MapPath("~/UploadImg/") + DateTime.Now.Day + DateTime.Now.Month + "/";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
               var path1 = Path.Combine(path, fileNameFull);
                file.SaveAs(path1);
                return fileNameFull;
            }
            return "";

        }
        public PartialViewResult BaseProfile()
        {
            return PartialView();
        }
        public PartialViewResult Degrees()
        {
          
            
            var ListPerNewDegrees = new List<BDSPerNews_Degrees>();
            ListPerNewDegrees.Add(new BDSPerNews_Degrees());
            var registerModel = new RegisterInformationModel
            {
             
                ListSalary = db.BDSSalaries.Where(T => T.Active == 1).ToList(),
                ListDucation = db.BDSEducations.Where(T => T.Active == 1).ToList(),
                ListBDSCareer = db.BDSCareers.Where(T => T.Active == 1).ToList(),
                ListTimework = db.BDSTimeWorks.Where(T => T.Active == 1).ToList(),
                ListBDSLanguage = db.BDSLanguages.Where(T => T.Active == 1).ToList(),
                ListPerNewDegrees = ListPerNewDegrees,
              
            };

            return PartialView(registerModel);
        }
        public PartialViewResult Experiences()
        {
            List<BDSPerNews_Experiences> ListPerNewExperiences = new List<BDSPerNews_Experiences>();

            ListPerNewExperiences.Add(new BDSPerNews_Experiences());
            var registerModel = new RegisterInformationModel
            {

                ListSalary = db.BDSSalaries.Where(T => T.Active == 1).ToList(),
                ListDucation = db.BDSEducations.Where(T => T.Active == 1).ToList(),
                ListBDSCareer = db.BDSCareers.Where(T => T.Active == 1).ToList(),
                ListTimework = db.BDSTimeWorks.Where(T => T.Active == 1).ToList(),
                ListBDSLanguage = db.BDSLanguages.Where(T => T.Active == 1).ToList(),
                ListPerNewExperiences = ListPerNewExperiences,

            };
            return PartialView(registerModel);
        }
        public PartialViewResult LangDegrees()
        {
            List<BDSPerNews_LangDegrees> ListPerNewLangDegrees = new List<BDSPerNews_LangDegrees>();

            ListPerNewLangDegrees.Add(new BDSPerNews_LangDegrees());
            var registerModel = new RegisterInformationModel
            {

                ListSalary = db.BDSSalaries.Where(T => T.Active == 1).ToList(),
                ListDucation = db.BDSEducations.Where(T => T.Active == 1).ToList(),
                ListBDSCareer = db.BDSCareers.Where(T => T.Active == 1).ToList(),
                ListTimework = db.BDSTimeWorks.Where(T => T.Active == 1).ToList(),
                ListBDSLanguage = db.BDSLanguages.Where(T => T.Active == 1).ToList(),
                ListPerNewLangDegrees = ListPerNewLangDegrees,

            };

            return PartialView(registerModel);
        }
        public PartialViewResult References()
        {

            List<BDSPerNews_References> ListPerNewReferences = new List<BDSPerNews_References>();
            ListPerNewReferences.Add(new BDSPerNews_References());
            var registerModel = new RegisterInformationModel
            {

                ListSalary = db.BDSSalaries.Where(T => T.Active == 1).ToList(),
                ListDucation = db.BDSEducations.Where(T => T.Active == 1).ToList(),
                ListBDSCareer = db.BDSCareers.Where(T => T.Active == 1).ToList(),
                ListTimework = db.BDSTimeWorks.Where(T => T.Active == 1).ToList(),
                ListBDSLanguage = db.BDSLanguages.Where(T => T.Active == 1).ToList(),
                ListPerNewReferences = ListPerNewReferences,

            };

            return PartialView(registerModel);
           
        }

        public ActionResult SaveJob()
        {
            if (Session["IdUser"] == null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
           
            return View();
        }
        public ActionResult JobApply()
        {
            return View();
        }

    }
}

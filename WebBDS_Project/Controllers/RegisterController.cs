using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBDS_Project.Models;

namespace WebBDS_Project.Controllers
{
    public class RegisterController : Controller
    {
       davisoft_bdsprojectEntities db = new davisoft_bdsprojectEntities();
        //
        // GET: /Register/

        public ActionResult RegisterCompany()
        {
            var dataCity = from data in db.states
                           join datatext in db.statetexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new ListCity { Id = data.state_id, Name = datatext.text };
            var registerModel = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListCityText = dataCity.ToList()
                
            };
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult RegisterCompany(RegisterInformationModel bdsInformationModel)
        {
            try
            {
                bdsInformationModel.TblBdsAdcount.CreateDate = DateTime.Now;
                bdsInformationModel.TblBdsAdcount.ModifiedDate = DateTime.Now;
                db.bdsaccounts.Add(bdsInformationModel.TblBdsAdcount);
                db.SaveChanges();
                bdsemployerinformation tblemployee = new bdsemployerinformation();
                bdsInformationModel.TblBdsemployerinformation.IdAccount = bdsInformationModel.TblBdsAdcount.Id;
              
                bdsInformationModel.TblBdsemployerinformation.Active = 1;
                bdsInformationModel.TblBdsemployerinformation.CreateDate = DateTime.Now;
                bdsInformationModel.TblBdsemployerinformation.CreateUser = 1;
                bdsInformationModel.TblBdsemployerinformation.ModifiedDate = DateTime.Now;
                bdsInformationModel.TblBdsemployerinformation.ModifiedUser = 1;
                bdsInformationModel.TblBdsemployerinformation.Featured = 1;
                
                db.bdsemployerinformations.Add(bdsInformationModel.TblBdsemployerinformation);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
           
            return RedirectToAction("Thanks","Register");
        }
        public ActionResult RegisterPersonal()
        {

            var registerModel = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListMarriea = db.bdsmarriages.ToList(),
                ListSalary = db.bdssalaries.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListBdscareer = db.bdscareers.ToList(),
                ListTimework = db.bdstimeworks.ToList()

            };
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult CheckEmail(string Email)
        {
            var countEmail = db.bdsaccounts.Where(x => x.Email == Email).Count();

            return Json(countEmail );
        }
        [HttpPost]
        public ActionResult RegisterPersonal(RegisterInformationModel bdsInformationModel)
        {
            bdsInformationModel.TblBdsAdcount.CreateDate = DateTime.Now;
            bdsInformationModel.TblBdsAdcount.ModifiedDate = DateTime.Now;
            db.bdsaccounts.Add(bdsInformationModel.TblBdsAdcount);
            db.SaveChanges();
            bdsInformationModel.TblBdspersonalinformation.IdAccount = bdsInformationModel.TblBdsAdcount.Id;
            db.bdspersonalinformations.Add(bdsInformationModel.TblBdspersonalinformation);
            db.SaveChanges();
            return RedirectToAction("Index", "Default");
        }
        [HttpPost]
        public ActionResult GetCity()
        {
            var dataCity = from data in db.states
                           join datatext in db.statetexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            return Json(dataCity.ToList());
        }
        [HttpPost]
        public ActionResult GetDistrict(int id)
        {
            var dataDistrict = from data in db.districts
                               join datatext in db.districttexts on data.name_id equals datatext.id
                               where datatext.language_id == "vi" && data.state_id == id
                               select new GeoModel { DistId = data.district_id, DistName = datatext.text };
            return Json(dataDistrict.ToList());
        }

        public ActionResult UploadImg()
        {
            var path = string.Empty; var path1 = string.Empty;
            var NewPath = string.Empty;
            var fortmatName = string.Empty;
            var fileNameFull = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var file = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (file != null && file.ContentLength > 0)
                {

                    var fileName = Path.GetFileName(file.FileName);
                    string newFileNmae = Path.GetFileNameWithoutExtension(fileName);
                    fortmatName = Path.GetExtension(fileName);

                    NewPath = newFileNmae.Replace(newFileNmae, (DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()).ToString());
                    fileNameFull = DateTime.Now.Day + "" + DateTime.Now.Month + "_" + NewPath + fortmatName;
                    path = Server.MapPath("~/fileUpload/") + DateTime.Now.Day + DateTime.Now.Month + "/";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    path1 = Path.Combine(path, fileNameFull);

                    file.SaveAs(path1);
                }
            }

            if (HttpContext.Request.Url != null && HttpContext.Request.Url.Host.Contains("localhost"))
           
                path = ConfigurationManager.AppSettings["UrlImage"] + DateTime.Now.Day + DateTime.Now.Month + "/";
            
            var _fullUrl = path + fileNameFull;
            return Json(new
            {
                fullurl = _fullUrl,
                shorurl = "/fileUpload/" + DateTime.Now.Day + DateTime.Now.Month + "/" + fileNameFull,
                imgName = fileNameFull
            });
            
        }
        public ActionResult Thanks()
        {
           
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
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
           
            var registerModel = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList()
                
            };
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult RegisterCompany(RegisterInformationModel bdsInformationModel)
        {
            bdsInformationModel.TbBdsAdcount.CreateDate = DateTime.Now;
            bdsInformationModel.TbBdsAdcount.ModifiedDate = DateTime.Now;
            db.bdsaccounts.Add(bdsInformationModel.TbBdsAdcount);
            db.SaveChanges();
            bdsInformationModel.TblBdsemployerinformation.Id = bdsInformationModel.TbBdsAdcount.Id;
            db.bdsemployerinformations.Add(bdsInformationModel.TblBdsemployerinformation);
            db.SaveChanges();
            return RedirectToAction("Index","Default");
        }
        public ActionResult RegisterPersonal()
        {

            var registerModel = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListMarriea = db.bdsmarriages.ToList(),
                ListSalary = db.bdssalaries.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListBdscareer = db.bdscareers.ToList()

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
            bdsInformationModel.TbBdsAdcount.CreateDate = DateTime.Now;
            bdsInformationModel.TbBdsAdcount.ModifiedDate = DateTime.Now;
            db.bdsaccounts.Add(bdsInformationModel.TbBdsAdcount);
            db.SaveChanges();
            bdsInformationModel.TblBdspersonalinformation.Id = bdsInformationModel.TbBdsAdcount.Id;
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
           
                path = ConfigurationManager.AppSettings["domain"] + DateTime.Now.Day + DateTime.Now.Month + "/";
            
            var _fullUrl = path + fileNameFull;
            return Json(new
            {
                fullurl = _fullUrl,
                shorurl = "/fileUpload/" + DateTime.Now.Day + DateTime.Now.Month + "/" + fileNameFull,
                imgName = fileNameFull
            });
            
        }

    }
}

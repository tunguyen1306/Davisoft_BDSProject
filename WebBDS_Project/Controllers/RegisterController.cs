using System;
using System.Collections.Generic;
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
        public ActionResult RegisterCompany(RegisterInformationModel bdsPersonalInformationModel)
        {
            bdsPersonalInformationModel.TbBdsAdcount.CreateDate = DateTime.Now;
            bdsPersonalInformationModel.TbBdsAdcount.ModifiedDate = DateTime.Now;
            db.bdsaccounts.Add(bdsPersonalInformationModel.TbBdsAdcount);
            db.SaveChanges();
            bdsPersonalInformationModel.TblBdsemployerinformation.Id = bdsPersonalInformationModel.TbBdsAdcount.Id;
            db.bdsemployerinformations.Add(bdsPersonalInformationModel.TblBdsemployerinformation);
            db.SaveChanges();
            return View();
        }

        public ActionResult RegisterEmployee()
        {
            return View();
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

    }
}

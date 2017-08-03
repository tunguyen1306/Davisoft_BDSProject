using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
            var ListSalary = db.BDSSalaries.ToList();
            var registerModel = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.ToList(),
                ListMarriea = db.BDSMarriages.ToList(),
                ListSalary = ListSalary,
                ListDucation = db.BDSEducations.ToList(),
                ListBDSCareer = db.BDSCareers.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                ListBDSEmployerInformation = db.BDSEmployerInformations.ToList(),
                ListBdsAdcount = db.BDSAccounts.ToList(),
                ListBDSEmper = db.BDSEmpers.ToList(),




            };
            return View(registerModel);
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

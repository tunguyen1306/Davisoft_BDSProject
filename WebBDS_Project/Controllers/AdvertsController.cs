using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBDS_Project.Models;

namespace WebBDS_Project.Controllers
{
    public class AdvertsController : Controller
    {
        davisoft_bdsprojectEntities db = new davisoft_bdsprojectEntities();
        //
        // GET: /Adverts/

        public ActionResult AdvertCompany()
        {
            var dataCity = from data in db.states
                           join datatext in db.statetexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            var registerModel = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListMarriea = db.bdsmarriages.ToList(),
                ListSalary = db.bdssalaries.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListBdscareer = db.bdscareers.ToList(),
                ListTimework = db.bdstimeworks.ToList(),
                Listbdslanguage = db.bdslanguages.ToList(),
                Listbdsnewstype = db.bdsnewstypes.ToList(),
                ListGeoModel= dataCity.ToList()

            };
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult AdvertCompany(RegisterInformationModel create)
        {
            create.tblbdsnew.FromDeadline = DateTime.Now;
            create.tblbdsnew.FromCreateNews = DateTime.Now;
            db.bdsnews.Add(create.tblbdsnew);
            db.SaveChanges();
            return RedirectToAction("Index", "Default");
        }

    }
}

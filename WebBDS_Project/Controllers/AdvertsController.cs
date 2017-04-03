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
        public ActionResult AdvertCompany(RegisterInformationModel create)
        {
            return View();
        }

    }
}

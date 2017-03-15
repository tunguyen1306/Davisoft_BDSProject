using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Davisoft_BDSProject.Web.Models;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class AdminCityController : Controller
    {
        //
        // GET: /AdminCity/
        private Davisoft_BDSProjectEntities db = new Davisoft_BDSProjectEntities();
        public ActionResult Index()
        {
            var queryData = from city in db.states
                join cityTex in db.statetexts on city.name_id equals cityTex.id
                where cityTex.language_id.Equals("vi")
                            select new CityModel { City = city,CityText = cityTex};

            return View(queryData);
        }
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(state state)
        {
            if (ModelState.IsValid)
            {
                db.states.Add(state);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(state);
        }

    }
}

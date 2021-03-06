﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Davisoft_BDSProject.Web.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using ServiceStack;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class AdminCityController : Controller
    {
        //
        // GET: /AdminCity/
        private Entities db = new Entities();
        public ActionResult Index()
        {
            var queryData = from city in db.States
                join cityTex in db.StateTexts on city.name_id equals cityTex.id
                where cityTex.language_id.Equals("vi")
                            select new CityModel { City = city,CityText = cityTex};

            return View(queryData);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit( int id)
        {
            var queryData = from city in db.States
                            join cityTex in db.StateTexts on city.name_id equals cityTex.id
                            where cityTex.language_id.Equals("vi") || city.state_id==id
                            select new CityModel { City = city, CityText = cityTex };

            return View(queryData.ToList().FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(CityModel  cityModel)
        {
            if (ModelState.IsValid)
            {

                State sta = db.States.Find(cityModel.City.state_id);
                db.Entry(sta).State = EntityState.Modified;
                sta.Status = cityModel.City.Status;
                sta.stateCode = cityModel.City.stateCode;
                StateText stateText = (from data in db.StateTexts where data.id==cityModel.CityText.id && data.language_id=="vi" select data).ToList().FirstOrDefault();
                db.Entry(stateText).State = EntityState.Modified;
                stateText.text = cityModel.CityText.text;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cityModel);
        }
        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityModel cityModel)
        {
            if (ModelState.IsValid)
            {
                
                cityModel.City.CreateDate = DateTime.Now;
                cityModel.City.ModifyDate = DateTime.Now;
                cityModel.City.countryCode = "VN";
                cityModel.City.name_id = 1234;
                db.States.Add(cityModel.City);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}

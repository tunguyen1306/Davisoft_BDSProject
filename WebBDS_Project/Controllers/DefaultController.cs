using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Antlr.Runtime.Tree;
using WebBDS_Project.Models;

namespace WebBDS_Project.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
        davisoft_bdsprojectEntities db = new davisoft_bdsprojectEntities();
        public ActionResult Index()
        {
         
            return View();
          
        }

        public PartialViewResult TinDacBiet(string tt,int from=0,int view=12,int page=1)
        {
            ViewBag.from = from;
            ViewBag.view = view;
            ViewBag.page = page;
            ViewBag.tt = tt;

            return PartialView();
        }
        public PartialViewResult TinNoiBat(string tt, int from = 0, int view = 6, int page = 1)
        {
            ViewBag.from = from;
            ViewBag.view = view;
            ViewBag.page = page;
            ViewBag.tt = tt;

            return PartialView();
        }
        public PartialViewResult TinMoiNhat(string tt, int from = 0, int view = 5, int page = 1)
        {
            ViewBag.from = from;
            ViewBag.view = view;
            ViewBag.page = page;
            ViewBag.tt = tt;

            return PartialView();
        }
        public PartialViewResult TinNhaTuyenDung(string tt, int from = 0, int view = 5, int page = 1)
        {
            ViewBag.from = from;
            ViewBag.view = view;
            ViewBag.page = page;
            ViewBag.tt = tt;

            return PartialView();
        }
        public ActionResult Blank()
        {
            return View();
        }
        public ActionResult Detail(string  id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new ListCityNew { Id = data.state_id, Name = datatext.text };
            var idEmployee = db.BDSNews.FirstOrDefault(x=>x.ID== id_).IdAcount;
            var Model = new NewsModel
            {
                tblBDSNew = db.BDSNews.FirstOrDefault(x => x.ID == id_),
                ListPicture = db.BDSPictures.Where(x => x.advert_id == id_).ToList(),
                tblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x=>x.IdAccount== idEmployee),
                ListBDSAccount = db.BDSAccounts.ToList(),
                ListCityText = dataCity.ToList(),
                ListDucation = db.BDSEducations.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList()


            };

            return View(Model);
        }
        public ActionResult Search()
        {

            return View();
        }
        public ActionResult SearchForEmployee()
        {

            return View();
        }
        public ActionResult DetailNews(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var data = db.BDSExtNews.Where(x => x.Active == 1 && x.ApproveStatus == 1).ToList();
            return View(data);
        }
        public ActionResult DetailEmployee(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var data = db.BDSEmployerInformations.Where(x => x.ID== id_).ToList();

            return View(data);
        }
    }
}

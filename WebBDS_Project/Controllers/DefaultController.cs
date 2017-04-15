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
            var dataCity = from data in db.states
                           join datatext in db.stateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new ListCityNew { Id = data.state_id, Name = datatext.text };
            var Model = new NewsModel
            {
                tblbdsnew = db.bdsnews.FirstOrDefault(x => x.Id == id_),
                ListPicture = db.bdspictures.Where(x => x.advert_id == id_).ToList(),
                Listbdsemployerinformation = db.bdsemployerinformations.ToList(),
                Listbdsaccount = db.bdsaccounts.ToList(),
                ListCityText = dataCity.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListTimework = db.bdstimeworks.ToList(),
                Listbdslanguage = db.bdslanguages.ToList()


            };

            return View(Model);
        }
        public ActionResult Search()
        {

            return View();
        }
        public ActionResult DetailNews(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var data = db.bdsextnews.Where(x => x.Active == 1 && x.ApproveStatus == 1).ToList();
            return View(data);
        }
    }
}

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
        public class CareerSearch
        {
            public int ID_News { get; set; }
            public string GString { get; set; }
        }
        public ActionResult Search(int?[] filterWorkingPlace, int[] filterCareer, int? filterSalary, int? filterTimeWorking, int page = 1, int view = 25)
        {
            int[] arrayIDNEWS=new int[]{};
            if (filterCareer != null && filterCareer.Length>0)
            {
                var str = String.Join(",", filterCareer.OrderBy(T=>T));
                String qStr = @"select 
	                        [ID_News],
                                   (select originalFilepath = STUFF((
                                  SELECT ',' + CAST( [dbo].[BDSNews_Career].[ID_Career] AS VARCHAR(10))
                                  FROM  [dbo].[BDSNews_Career]
                                  WHERE [dbo].[BDSNews_Career].[ID_News]=t1.ID_News    and ID_Career in (" + String.Join(",", filterCareer) + @")              
                                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')
                                  ) AS GString
                         from
                        [dbo].[BDSNews_Career] t1 where ID_Career in (" + String.Join(",", filterCareer) + @")
                        group by [ID_News]";
                var qCareer = db.Database.SqlQuery<CareerSearch>( qStr);
               arrayIDNEWS= qCareer.Where(T => T.GString.StartsWith(str)).Select(T => T.ID_News).ToArray();
               if (arrayIDNEWS.Length==0)
               {
                   arrayIDNEWS = new[] {-1};
               }
            }
         
            int fromS = 0;
            int toS = int.MaxValue;
            var salary = db.BDSSalaries.FirstOrDefault(T => T.ID == filterSalary);
            if (salary != null)
            {
                switch (salary.Type)
                {
                    case 1:
                        toS = salary.FromSalary;
                        break;
                    case 2:
                        fromS = salary.FromSalary;
                        toS = salary.ToSalary;
                        break;
                    case 3:
                        fromS = salary.FromSalary;
                        break;
                }
            }

            var q = (from a in db.BDSNews
                     join b in db.BDSNewsTypes on a.IdTypeNewsCuurent equals b.ID
                     where a.Active == 1 && a.Status == 1 && ((a.FromSalary >= fromS && a.ToSalary <= toS) || (a.FromSalary <= fromS && fromS <= a.ToSalary)||(a.FromSalary <= toS && toS <= a.ToSalary))
                     orderby b.Order ascending, a.FromCreateNews descending
                     select a);
            if (filterWorkingPlace != null && filterWorkingPlace.Length>0)
            {
               
               q= q.Where(T => filterWorkingPlace.Contains(T.AddressWork));
            }
            if (filterTimeWorking.HasValue)
            {
                q = q.Where(a => a.IdTimeWork == filterTimeWorking);
            }
            if (arrayIDNEWS.Length > 0)
            {

                q = q.Where(a =>arrayIDNEWS.Contains(a.ID));
            }
            var total = q.Count();
            var data = q.Skip(page * view - view).Take(view).ToList();
            ViewBag.Total = total;
            ViewBag.From = page * view - view;
            return View(data);
        }
        
        
        public ActionResult SearchForEmployee()
        {

            return View();
        }
        public ActionResult DetailNews(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var data = db.BDSExtNews.FirstOrDefault(x => x.Active == 1 && x.ApproveStatus == 1 && x.ID==id_);
            return View(data);
        }
        public ActionResult DetailEmployee(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var data = db.BDSEmployerInformations.FirstOrDefault(x => x.ID== id_);

            return View(data);
        }
    }
}

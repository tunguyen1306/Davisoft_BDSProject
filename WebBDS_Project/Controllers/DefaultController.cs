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
        public static Dictionary<string, object> listStaticData = new Dictionary<string, object>();
        
        //
        // GET: /Default/
        davisoft_bdsprojectEntities1 db = new davisoft_bdsprojectEntities1();
        public ActionResult Index()
        {
            Session["IdMenu"] = 0;
            return View();
          
        }

        public PartialViewResult Employee(string tt, int from = 0, int view = 12, int page = 1)
        {
            ViewBag.from = from;
            ViewBag.view = view;
            ViewBag.page = page;
            ViewBag.tt = tt;

            return PartialView();
        }
        public PartialViewResult TinDacBiet(string tt, int from = 0, int view = 12, int page = 1)
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
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new ListCityNew { Id = data.state_id, Name = datatext.text }).ToList();

            dataCity.Insert(0, new ListCityNew { Id = 0, Name = "Chọn thành/phố" });
            dataCity.Insert(0, new ListCityNew { Id = 59, Name = "TP.Hồ Chí Minh" });
            dataCity.Insert(1, new ListCityNew { Id = 28, Name = "TP.Hà Nội" });
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
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSCareer = db.BDSCareers.ToList()

            };

            return View(Model);
        }
        public class CareerSearch
        {
            public int ID_News { get; set; }
            public string GString { get; set; }
        }
        public ActionResult Search(int?[] filterWorkingPlace, String[] filterCareer, String filterSalary, String filterTimeWorking, int? typenews, int page = 1, int view = 25)
        {
            var t = 0;
            if (filterCareer!=null)
            {
                Session["IdMenu"] = filterCareer[0];
            }
            else
            {
                Session["IdMenu"] = 0;
            }
           
            int[] arrayIDNEWS=new int[]{};
            if (filterCareer != null && filterCareer.Length>0)
            {
                int[] filterCareerID=  db.BDSCareers.Where(T => filterCareer.Contains(T.KeyUrl)).Select(T => T.ID).ToArray();
                var str = String.Join(",", filterCareerID.OrderBy(T => T));
                String qStr = @"select 
	                        [ID_News],
                                   (select originalFilepath = STUFF((
                                  SELECT ',' + CAST( [dbo].[BDSNews_Career].[ID_Career] AS VARCHAR(10))
                                  FROM  [dbo].[BDSNews_Career]
                                  WHERE [dbo].[BDSNews_Career].[ID_News]=t1.ID_News    and ID_Career in (" + String.Join(",", filterCareerID) + @")              
                                  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')
                                  ) AS GString
                         from
                        [dbo].[BDSNews_Career] t1 where ID_Career in (" + String.Join(",", filterCareerID) + @")
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
            var salary = db.BDSSalaries.FirstOrDefault(T => T.KeyUrl == filterSalary);
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
                     where a.Active == 1 &&(typenews==null|| typenews==b.ID) && a.Status == 1 && ((a.FromSalary >= fromS && a.ToSalary <= toS) || (a.FromSalary <= fromS && fromS <= a.ToSalary)||(a.FromSalary <= toS && toS <= a.ToSalary))
                     orderby a.FromCreateNews descending
                     select a);
            if (filterWorkingPlace != null && filterWorkingPlace.Length>0)
            {
               
               q= q.Where(T => filterWorkingPlace.Contains(T.AddressWork));
            }
            if (!String.IsNullOrWhiteSpace(filterTimeWorking))
            {
                var dbsTimeWork= db.BDSTimeWorks.Where(T => T.KeyUrl == filterTimeWorking).FirstOrDefault();
                if (dbsTimeWork!=null)
                {
                    q = q.Where(a => a.IdTimeWork == dbsTimeWork.ID);
                }
               
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


        public ActionResult SearchForEmployee(String filterWorkingPlaces, String filterCareers, String filterSalary, String filterTimeWorking, int page = 1, int view = 25)
        {


            int[] filterWorkingPlace = null;
            String[] filterCareer = null;
            if (filterCareers!="nganh-nghe")
            {
                filterCareer = filterCareers.Split(',');
            }
            if (filterWorkingPlaces!="tinh-thanh")
            {
                filterWorkingPlace = filterWorkingPlaces.Split(',').Select(T=>int.Parse(T)).ToArray();
            }
            if (filterSalary=="muc-luong")
            {
                filterSalary = null;
            }
            if (filterTimeWorking != "kinh-nghiem")
            {
                filterTimeWorking = null;
            }
            var q = (from a in db.BDSPersonalInformations join b in db.BDSPerNews on a.ID equals b.PerId
                     join c in db.BDSEducations on b.EducationProfile equals c.ID
                     where a.Active == 1 && b.Status == 1 && b.SearchCheck == 1
                     orderby a.DateReup ascending 
                     select b);
            if (filterWorkingPlace != null && filterWorkingPlace.Length > 0)
            {

                q = q.Where(T => filterWorkingPlace.Contains(T.ProvinceProfile.Value));
            }
            if (!String.IsNullOrWhiteSpace(filterSalary))
            {
                var salary = db.BDSSalaries.FirstOrDefault(T => T.KeyUrl == filterSalary);
                if (salary != null)
                {
                    q = q.Where(a => a.SalaryProfile == salary.ID);
                }
            }
            if (!String.IsNullOrWhiteSpace(filterTimeWorking))
            {
                var dbsTimeWork = db.BDSTimeWorks.Where(T => T.KeyUrl == filterTimeWorking).FirstOrDefault();
                if (dbsTimeWork != null)
                {
                    q = q.Where(a => a.ExperienceProfile == dbsTimeWork.ID);
                }
               
            }
            if (filterCareer!=null && filterCareer.Length > 0)
            {
                int[] filterCareerID = db.BDSCareers.Where(T => filterCareer.Contains(T.KeyUrl)).Select(T => T.ID).ToArray();
               

                q = q.Where(a => filterCareerID.Contains(a.CareerProfile.Value));
            }
            var total = q.Count();
            var data = q.Skip(page * view - view).Take(view).ToList();
            ViewBag.Total = total;
            ViewBag.From = page * view - view;
            return View(data);
        }
      
        public ActionResult DetailNews(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var data = db.BDSExtNews.FirstOrDefault(x => x.Active == 1  && x.ID==id_);
            return View(data);
        }
       
        public ActionResult DetailCompany(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new ListCityNew { Id = data.state_id, Name = datatext.text }).ToList();

            dataCity.Insert(0, new ListCityNew { Id = 0, Name = "Chọn thành/phố" });
            dataCity.Insert(0, new ListCityNew { Id = 59, Name = "TP.Hồ Chí Minh" });
            dataCity.Insert(1, new ListCityNew { Id = 28, Name = "TP.Hà Nội" });
           
            var Model = new NewsModel
            {
               
                tblBDSEmployerInformation = db.BDSEmployerInformations.FirstOrDefault(x => x.ID == id_),
                ListBDSAccount = db.BDSAccounts.ToList(),
                ListCityText = dataCity.ToList(),
                ListDucation = db.BDSEducations.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSCareer = db.BDSCareers.ToList()

            };

            return View(Model);
        }

    }
}

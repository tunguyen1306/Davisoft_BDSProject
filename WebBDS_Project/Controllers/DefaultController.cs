using System;
using System.Collections.Generic;
using System.Data;
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
        public ActionResult Search(String filter, int? typenews, int page = 1, int view = 25)
        {
          /*  var t = 0;
            if (filterCareer!=null)
            {
                Session["IdMenu"] = filterCareer[0];
            }
            else
            {
                Session["IdMenu"] = 0;
            }*/

            List<String> filterWorkingPlaces = new List<string>();
            List<String> filterCareers = new List<string>();
            String filterSalary = "";
            String filterWorkTime = "";
            filterWorkingPlaces = extractParams(filter, "tt").Split(',').ToList().Where(T => T.Trim().Length > 0).ToList(); ;
            filterCareers = extractParams(filter, "nn").Split(',').ToList().Where(T => T.Trim().Length > 0).ToList(); ;
            filterSalary = extractParams(filter, "ml");
            filterWorkTime = extractParams(filter, "kn");
            String bQuery = "";
            if (filterWorkingPlaces.Count > 0)
            {
                foreach (var item in filterWorkingPlaces)
                {

                    bQuery += "filterWorkingPlace=" + item + "&";

                }
                ViewData["filterWorkingPlace"] = String.Join(",", filterWorkingPlaces);
            }
            if (filterCareers.Count > 0)
            {
                foreach (var item in filterCareers)
                {

                    bQuery += "filterCareer=" + item + "&";

                }
                ViewData["filterCareer"] = String.Join(",", filterCareers);
            }
            if (!String.IsNullOrWhiteSpace(filterSalary))
            {
                bQuery += "filterSalary=" + filterSalary + "&";
                ViewData["filterSalary"] = filterSalary;
            }

            if (!String.IsNullOrWhiteSpace(filterWorkTime))
            {
                bQuery += "filterTimeWorking=" + filterWorkTime + "&";
                ViewData["filterTimeWorking"] = filterWorkTime;
            }
            if (bQuery.Length > 0)
            {
                bQuery = bQuery.Substring(0, bQuery.Length - 1);

            }
            ViewBag.Query = bQuery;


            int[] arrayIDNEWS=new int[]{};
            if (filterCareers != null && filterCareers.Count > 0)
            {
                int[] filterCareerID = db.BDSCareers.Where(T => filterCareers.Contains(T.KeyUrl)).Select(T => T.ID).ToArray();
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
            if (filterWorkingPlaces != null && filterWorkingPlaces.Count > 0)
            {
            
                    int[] listP =db.StateTexts.Where(T => filterWorkingPlaces.Contains(T.keyurl) && T.language_id == "vi")
                        .Select(T => T.id)
                        .ToArray();
                    q = q.Where(T => listP.Contains(T.AddressWork.Value));
               
               
            }
            if (!String.IsNullOrWhiteSpace(filterWorkTime))
            {
                var dbsTimeWork = db.BDSTimeWorks.Where(T => T.KeyUrl == filterWorkTime).FirstOrDefault();
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

        String extractParams(String filter, String key)
        {
            String result = "";
            if (!String.IsNullOrWhiteSpace(filter))
            {
                if (filter.Substring(0, 1) == "-")
                {
                    filter = filter.Substring(1);
                }
                if (filter.Split('-').Where(T => T == key).Count() > 0)
                {
                    string bString = "";
                    for (int i = 0; i < filter.Split('-').Length; i++)
                    {
                        bString += filter.Split('-')[i] + "-";
                        if (filter.Split('-')[i] == key)
                        {
                            break;
                        }
                    }
                    if (bString.Length > 0)
                    {
                        String rString = "";
                        String newFilter = filter.Substring(bString.Length);
                        for (int i = 0; i < newFilter.Split('-').Length; i++)
                        {
                            if (newFilter.Split('-')[i] == "tt")
                            {
                                break; ;
                            }
                            if (newFilter.Split('-')[i] == "nn")
                            {
                                break; ;
                            }
                            if (newFilter.Split('-')[i] == "ml")
                            {
                                break; ;
                            }
                            if (newFilter.Split('-')[i] == "kn")
                            {
                                break; ;
                            }
                            rString += newFilter.Split('-')[i] + "-";

                        }
                        if (rString.Length > 0)
                        {
                            rString = rString.Substring(0, rString.Length - 1);
                            result += "," + rString;
                            newFilter = newFilter.Substring(rString.Length);
                            String rs = extractParams(newFilter, key);
                            if (rs.Length>0)
                            {
                                result += "," + extractParams(newFilter, key);
                            }
                           
                        }

                    }
                }

            }
            return result.IndexOf(",")==0?result.Substring(1):"";
        }
        public ActionResult SearchForEmployee(String filter,int page = 1, int view = 25)
        {

            List<String>  filterWorkingPlaces=new List<string>();
            List<String> filterCareers = new List<string>();
            String filterSalary = "";
            String filterWorkTime = "";
            filterWorkingPlaces = extractParams(filter, "tt").Split(',').ToList().Where(T=>T.Trim().Length>0).ToList();
            filterCareers = extractParams(filter, "nn").Split(',').ToList().Where(T => T.Trim().Length > 0).ToList();
            filterSalary=extractParams(filter, "ml");
            filterWorkTime=extractParams(filter, "kn");
            String bQuery = "";
            if (filterWorkingPlaces.Count > 0)
            {
                foreach (var item in filterWorkingPlaces)
                {

                    bQuery += "filterWorkingPlace=" + item + "&";

                }
                ViewData["filterWorkingPlace"] = String.Join(",", filterWorkingPlaces);
            }
            if (filterCareers.Count > 0)
            {
                foreach (var item in filterCareers)
                {

                    bQuery += "filterCareer=" + item + "&";

                }
                ViewData["filterCareer"] = String.Join(",", filterCareers);
            }
            if (!String.IsNullOrWhiteSpace(filterSalary))
            {
                bQuery += "filterSalary=" + filterSalary + "&";
                ViewData["filterSalary"] = filterSalary;
            }
            
            if (!String.IsNullOrWhiteSpace(filterWorkTime))
            {
                bQuery += "filterTimeWorking=" + filterWorkTime + "&";
                ViewData["filterTimeWorking"] = filterWorkTime;
            }
            if (bQuery.Length>0)
            {
                bQuery = bQuery.Substring(0, bQuery.Length - 1);
              
            }
            ViewBag.Query = bQuery;

            var q = (from a in db.BDSPersonalInformations join b in db.BDSPerNews on a.ID equals b.PerId
                     join c in db.BDSEducations on b.EducationProfile equals c.ID
                     where a.Active == 1 && b.Status == 1 && b.SearchCheck == 1
                     orderby a.DateReup ascending 
                     select b);
            if (filterWorkingPlaces.Count > 0)
            {
                try
                {
                    int[] listP = db.StateTexts.Where(T => filterWorkingPlaces.Contains(T.keyurl) && T.language_id == "vi")
                         .Select(T => T.id)
                         .ToArray();
                    q = q.Where(T => listP.Contains(T.ProvinceProfile.Value));
                }
                catch (Exception)
                {
                    
                   
                }
              
            }
            if (filterCareers.Count > 0)
            {
                int[] filterCareerID = db.BDSCareers.Where(T => filterCareers.Contains(T.KeyUrl)).Select(T => T.ID).ToArray();
                if (filterCareerID.Length>0)
                {
                    q = q.Where(a => filterCareerID.Contains(a.CareerProfile.Value));
                }
              
            }
            if (!String.IsNullOrWhiteSpace(filterSalary))
            {

                var salary = db.BDSSalaries.FirstOrDefault(T => filterSalary.Contains(T.KeyUrl));
                if (salary != null)
                {
                    q = q.Where(a => a.SalaryProfile == salary.ID);
                }
            }
            if (!String.IsNullOrWhiteSpace(filterWorkTime))
            {

                var dbsTimeWork = db.BDSTimeWorks.Where(T => filterWorkTime.Contains(T.KeyUrl)).FirstOrDefault();
                if (dbsTimeWork != null)
                {
                    q = q.Where(a => a.ExperienceProfile == dbsTimeWork.ID);
                }
               
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CsQuery.EquationParser.Implementation;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Models;
using ImageResizer;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Resources;
using System.Net;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class BDSNewController : Controller
    {
        Davisoft_BDSProjectEntities db = new Davisoft_BDSProjectEntities();
        private readonly IBDSNewService _service;
        private readonly IBDSAccountService _serviceAccount;
        private readonly IBDSEmployerInformationService _serviceEmployerInformation;
        private readonly IBDSEducationService _serviceEducation;
        private readonly IBDSTimeWorkService _serviceTimeWork;
        private readonly IBDSCareerService _serviceCareer;
        private readonly IBDSNewsTypeService _serviceNewsType;
        private readonly IBDSNewsTypePriceService _serviceNewsTypePrice;
        private readonly IBDSLanguageService _serviceLanguage;
        private readonly IBDSPictureService _servicePicture;
        public BDSNewController(IBDSNewService service,
            IBDSAccountService serviceAccount,
            IBDSEducationService serviceEducation,
            IBDSTimeWorkService serviceTimeWork,
            IBDSCareerService serviceCareer,
            IBDSNewsTypeService serviceNewsType,
            IBDSNewsTypePriceService serviceNewsTypePrice,
              IBDSLanguageService serviceLanguage,
            IBDSEmployerInformationService serviceEmployerInformation,
            IBDSPictureService servicePicture
            )
        {
            _service = service;
            _serviceAccount = serviceAccount;
            _serviceEducation = serviceEducation;
            _serviceTimeWork = serviceTimeWork;
            _serviceCareer = serviceCareer;
            _serviceNewsType = serviceNewsType;
            _serviceNewsTypePrice = serviceNewsTypePrice;
            _serviceLanguage = serviceLanguage;
            _serviceEmployerInformation = serviceEmployerInformation;
            _servicePicture = servicePicture;
        }

        void LoadDataList()
        {
            var Cities = (from a in db.states
                          join b in db.statetexts on a.name_id equals b.id
                          where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();


            var Educations = _serviceEducation.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var NewsTypes = _serviceNewsType.GetIQueryableItems().Where(T => T.Active == 1).OrderBy(T=>T.Order).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var NewsTypePrices = _serviceNewsTypePrice.GetIQueryableItems().Where(T => T.Active == 1).ToList();
            var TimeWorks = _serviceTimeWork.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Careers = _serviceCareer.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Languages = _serviceLanguage.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var EmployerInformations = _serviceEmployerInformation.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.BDSAccount.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
        
            Cities.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Educations.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            NewsTypes.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            TimeWorks.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            EmployerInformations.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Languages.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
         
            ViewBag.Cities = Cities;
            ViewBag.Educations = Educations;
            ViewBag.NewsTypes = NewsTypes;
            ViewBag.NewsTypePrices = NewsTypePrices;
            ViewBag.TimeWorks = TimeWorks;
            ViewBag.Careers = Careers;
            ViewBag.Languages = Languages;
            ViewBag.EmployerInformations = EmployerInformations;
        }
       
        [DisplayName(@"News management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"News management")]
        [AjaxOnly]
        public JsonResult IndexAjax(DataTableJS data)
        {
          
            String search = null;
            if (data.search != null && data.search["value"] != null)
            {
                search = data.search["value"];
            }
            var column = data.order[0]["column"];
            var dir = data.order[0]["dir"];
            string columnName = ((String[])data.columns[int.Parse(column)]["data"])[0];
            var queryFilter =
              _service.GetIQueryableItems()
                  .Where(
                      T => T.Active == 1 &&
                          search != null &&
                          (T.KeySearch.ToLower().Contains(search.ToLower())));
            if (dir == "asc")
            {
                queryFilter = queryFilter.OrderByField(columnName, true);
            }
            else
            {
                queryFilter = queryFilter.OrderByField(columnName, false);
            }
            data.recordsTotal = _service.GetIQueryableItems().Where(T => T.Active == 1).Count();
            data.recordsFiltered = queryFilter.Count();
            data.data = queryFilter.Skip(data.start)
                    .Take(data.length == -1 ? data.recordsTotal : data.length)
                    .ToList().Select(T => new { 
                        ID = T.ID,
                        Title = T.Title, 
                        BDSNewsType = new BDSNewsType{ ID = T.BDSNewsType.ID,Name=T.BDSNewsType.Name} ,
                        NameCompany = T.NameCompany, 
                        BDSAccount = new BDSAccount { ID = T.BDSAccount.ID, Email = T.BDSAccount.Email }, 
                        DesCompany = T.DesCompany,
                        FromCreateNews=T.FromCreateNews.ToString(MvcApplication.DateTimeFormat.ShortDatePattern),
                        ToCreateNews = T.ToCreateNews.ToString(MvcApplication.DateTimeFormat.ShortDatePattern),
                        Status = T.Status
                    });
         
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Create()
        {
            var tblNews = new BDSNew();
            _service.CreateItem(tblNews);
            LoadDataList();
            return View(new BDSNew { CreateDate = DateTime.Now, FromDateToDateString = DateTime.Now.ToString(MvcApplication.DateTimeFormat.ShortDatePattern) + " - " + DateTime.Now.AddDays(3).ToString(MvcApplication.DateTimeFormat.ShortDatePattern), CreateUser = 1, ID = tblNews.ID, IdPictrure = _servicePicture.GetIQueryableItems().Count(x => x.advert_id == tblNews.ID) });
        }

        [HttpPost]
        public ActionResult Create(BDSNew model)
        {



       
            if (!ModelState.IsValid)
            {
                LoadDataList();
                model.BDSPictures=   _servicePicture.GetIQueryableItems().Where(T => T.Active == 1 && T.advert_id==model.ID).ToList();
                return View(model);
            }
            var fromDate = model.FromDateToDateString.Split('-')[0];
            var toDate = model.FromDateToDateString.Split('-')[1];
            model.FromCreateNews = DateTime.Parse(fromDate.Trim(), MvcApplication.CultureInfo, DateTimeStyles.None);
            model.ToCreateNews = DateTime.Parse(toDate.Trim(), MvcApplication.CultureInfo, DateTimeStyles.None);
            model.FromDeadline = DateTime.Now;
            model.IdTypeNewsCuurent = model.IdTypeNewsCuurent;
            model.KeySearch = model.Title.NormalizeD() + " " + _serviceAccount.GetItem(model.IdAcount).Email + " " + _serviceNewsType.GetItem(model.IdTypeNews).Name.NormalizeD() + " " + _serviceEmployerInformation.GetIQueryableItems().Where(T => T.IdAccount == model.IdAcount).FirstOrDefault().Name + " " + model.DesCompany.NormalizeD();
            _service.CreateItem(model);

            var type = _serviceNewsType.GetItem(model.IdTypeNewsCuurent);

            String Fname = "Đăng tin '{A}' trong vòng '{B}' ngày tổng giá phải trả '{C}' VNĐ";
            string name = Fname.Replace("{A}", type.Name).Replace("{B}", ((int)Math.Ceiling(model.ToCreateNews.Subtract(model.FromCreateNews).TotalDays)) + "").Replace("{C}", model.TotalMoney.ToString("n2"));
            bdstransactionhistory tran = new bdstransactionhistory
            {
                Name = name,
                Description = name,
                KeySearch = name.NormalizeD(),
                Active = 1,
                CreateUser = 1,
                CreateDate = DateTime.Now,
                TypeTran = 2,
                PointTran = 0,
                MoneyTran = (decimal) model.TotalMoney,
                DateTran = DateTime.Now
            };
            db.Entry(tran).State = EntityState.Added;
            db.SaveChanges();
            model.RefTranHis = tran.Id;
            _service.UpdateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            LoadDataList();
            BDSNew model =
                _service.GetIQueryableItems().Where(T => T.ID == id).Include(T => T.BDSPictures).FirstOrDefault();
            model.IdPictrure = _servicePicture.GetIQueryableItems().Count(x => x.advert_id == id);
            model.FromDateToDateString = model.FromCreateNews.ToString(MvcApplication.DateTimeFormat.ShortDatePattern) +
                                         " - " +
                                         model.ToCreateNews.ToString(MvcApplication.DateTimeFormat.ShortDatePattern);
            return View(model);
        }

        [HttpPost]
        public ActionResult Review(BDSNew model)
        {
            if (!ModelState.IsValid)
            {
                LoadDataList();
                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }
            var fromDate = model.FromDateToDateString.Split('-')[0];
            var toDate = model.FromDateToDateString.Split('-')[1];
            model.FromCreateNews = DateTime.Parse(fromDate.Trim(), MvcApplication.CultureInfo, DateTimeStyles.None);
            model.ToCreateNews = DateTime.Parse(toDate.Trim(), MvcApplication.CultureInfo, DateTimeStyles.None);
            model.FromDeadline = DateTime.Now;
               model.KeySearch = model.Title.NormalizeD() + " " + _serviceAccount.GetItem(model.IdAcount).Email + " " + _serviceNewsType.GetItem(model.IdTypeNews).Name.NormalizeD() + " " + _serviceEmployerInformation.GetIQueryableItems().Where(T => T.IdAccount == model.IdAcount).FirstOrDefault().Name + " " + model.DesCompany.NormalizeD();
            _service.UpdateItem(model);
            ViewBag.Success = true;
            ViewBag.Message = Resource.SaveSuccessful;
            return    Redirect("/");;
        }

        public ActionResult Review(int id)
        {
            LoadDataList();
            BDSNew model =
                _service.GetIQueryableItems().Where(T => T.ID == id).Include(T => T.BDSPictures).FirstOrDefault();
            model.IdPictrure = _servicePicture.GetIQueryableItems().Count(x => x.advert_id == id);
            model.FromDateToDateString = model.FromCreateNews.ToString(MvcApplication.DateTimeFormat.ShortDatePattern) +
                                         " - " +
                                         model.ToCreateNews.ToString(MvcApplication.DateTimeFormat.ShortDatePattern);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSNew model)
        {
            if (!ModelState.IsValid)
            {
                LoadDataList();
                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }
            var fromDate = model.FromDateToDateString.Split('-')[0];
            var toDate = model.FromDateToDateString.Split('-')[1];
            model.FromCreateNews = DateTime.Parse(fromDate.Trim(), MvcApplication.CultureInfo, DateTimeStyles.None);
            model.ToCreateNews = DateTime.Parse(toDate.Trim(), MvcApplication.CultureInfo, DateTimeStyles.None);
            model.FromDeadline = DateTime.Now;
            model.KeySearch = model.Title.NormalizeD() + " " + _serviceAccount.GetItem(model.IdAcount).Email + " " + _serviceNewsType.GetItem(model.IdTypeNews).Name.NormalizeD() + " " + _serviceEmployerInformation.GetIQueryableItems().Where(T => T.IdAccount == model.IdAcount).FirstOrDefault().Name + " " + model.DesCompany.NormalizeD();
            _service.UpdateItem(model);
            ViewBag.Success = true;
            ViewBag.Message = Resource.SaveSuccessful;
            return Edit(model.ID);
        }

        [HttpPost]
        [ActionName("Delete"), DisplayName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteItem(id);
            return RedirectToAction("Index");
        }  

        
        [HttpPost]
        public void SaveImg(NewsPictures newsPicture)
        {
            var t = newsPicture.cfile == null ? "" : newsPicture.cfile;
            var file = t.Replace("data:image/png;base64,", "");
            var photoBytes = Convert.FromBase64String(file);
            string format = "jpg";
            if (newsPicture.isactive == 1)
            {
                newsPicture.isactive = 1;
            }
            else
            {
                newsPicture.isactive = 2;
            }
            var picture = new NewsPictures
            {
                tblPicture = new bdspicture() { advert_id = newsPicture.idProducts, angleType = 0, cms_sql_id = 0, converted = DateTime.Now, tocheck = true, type_id = 1, title = newsPicture.nameImg, position = newsPicture.isactive }
            };
            if (newsPicture.idpicture == 0)
            {
                var settings = new ResizeSettings();
                settings.Scale = ScaleMode.DownscaleOnly;
                settings.Format = format;

                //string uploadFolder = picture.DirectoryPhysical;

                string path = Server.MapPath("~/fileUpload/").Replace("adminbds.vangia.net", "webtuyendung.vangia.net") + DateTime.Now.Day + DateTime.Now.Month + "/";
                // check for directory
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // filename with placeholder for size
                if (picture.GetConvertedFileName() == null || string.IsNullOrWhiteSpace(picture.GetConvertedFileName()))
                    picture.SetFileName(DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + "_" + picture.CreateFilename() + "_{0}." + format);

                if (picture.GetFilePathPhysical(NewsPictures.PictureSize.Large) != null)
                {
                    string dest = path + picture.FileName(NewsPictures.PictureSize.Large);
                    settings.MaxWidth = 720;
                    settings.MaxHeight = 405;
                    if (picture.WaterMarkLarge == NewsPictures.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPictures.PictureSize.Large);
                }

                if (picture.GetFilePathPhysical(NewsPictures.PictureSize.Medium) != null)
                {
                    string dest = path + picture.FileName(NewsPictures.PictureSize.Medium);
                    settings.MaxWidth = 320;
                    settings.MaxHeight = 180;
                    if (picture.WaterMarkLarge == NewsPictures.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPictures.PictureSize.Medium);
                }
                if (picture.GetFilePathPhysical(NewsPictures.PictureSize.Tiny) != null)
                {
                    string dest = path + picture.FileName(NewsPictures.PictureSize.Tiny);
                    settings.MaxWidth = 220;
                    settings.MaxHeight = 123;
                    if (picture.WaterMarkLarge == NewsPictures.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPictures.PictureSize.Tiny);
                }

                if (picture.GetFilePathPhysical(NewsPictures.PictureSize.Small) != null)
                {
                    string dest = path + picture.FileName(NewsPictures.PictureSize.Small);
                    settings.MaxWidth = 120;
                    settings.MaxHeight = 67;
                    if (picture.WaterMarkLarge == NewsPictures.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPictures.PictureSize.Small);
                }


                var BDSPicture =new  BDSPicture(); 
                BDSPicture.advert_id = picture.tblPicture.advert_id;
                BDSPicture.position = picture.tblPicture.position;
                BDSPicture.originalFilepath = picture.tblPicture.originalFilepath;
                
                _servicePicture.CreateItem(BDSPicture);
            }
            if (newsPicture.idpicture > 0)
            {
                BDSPicture modelPIc = _servicePicture.GetItem(newsPicture.idpicture);
                _servicePicture.UpdateItem(modelPIc);
                RedirectToAction("Index", "BDSNew");
            }


        }


        public ActionResult Cancel(int id)
        {
           
            BDSNew model = _service.GetItem(id);
            _service.DeleteItem(model.ID);
           var modelPicture = _servicePicture.GetIQueryableItems().Where(x=>x.advert_id==id).ToList();
            if (modelPicture.Count>0)
            {
                foreach (var item in modelPicture)
                {
                    _servicePicture.DeleteItem(item.ID);
                }
            }
          

            return RedirectToAction("Index", "BDSNew");
        }
        [HttpPost]
        public ActionResult DeleteImg(int idpicture)
        {

          
            try
            {
                _servicePicture.DeleteItem(idpicture);
                var Pic = _servicePicture.GetItem(idpicture);
                DeleteIMG(Pic.originalFilepath);
                return Json(Pic);
            }
            catch (Exception)
            {
                
             
            }
            return Json(null);


        }
        public void DeleteIMG(string picture)
        {
            if (picture == null)
                return;
            var fo = picture.Substring(0, 3);
            string dir = Server.MapPath("~/fileUpload/" + fo + "/" + picture);

            System.IO.File.Delete(dir);



        }
        [AjaxOnly]
        public JsonResult CalcuMoney(int idType,string rDate)
        {
           var price= _serviceNewsTypePrice.GetIQueryableItems()
                .Where(T => T.IdNewsType == idType && T.Active == 1 && T.ApplyPrice <= DateTime.Now)
                .OrderByDescending(T => T.ApplyPrice)
                .FirstOrDefault();

           var fromDate = rDate.Split('-')[0];
           var toDate = rDate.Split('-')[1];
           var  FromDate = DateTime.Parse(fromDate.Trim(), MvcApplication.CultureInfo, DateTimeStyles.None);
           var ToDate = DateTime.Parse(toDate.Trim(), MvcApplication.CultureInfo, DateTimeStyles.None);
            var totalDay = ToDate.Subtract(FromDate).TotalDays;
            if (totalDay<1)
            {
                totalDay = 1;
            }
            else
            {
                totalDay = Math.Ceiling(totalDay);
            }
            var pInDay = 0.0;
            var pAll = 0.0;
           if (price!=null)
           {
               pInDay = price.Price;
               pAll = totalDay*pInDay;
           }
            return Json(new{PriceInDay=pInDay,TotalPrice=pAll,TotalDay=totalDay}, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult EmployerInformation(int idAccount)
        {
           return   Json(
                _serviceEmployerInformation.GetIQueryableItems().Where(T => T.IdAccount == idAccount).ToList().Select(T => new
                {
                    Name = T.Name,
                    AddressContact = T.AddressContact + ", " + (from a in db.districts
                                                                join b in db.districttexts on a.name_id equals b.id
                                                                where b.language_id == "vi" && a.state_id == T.DistrictContact
                                                                select new { ID = a.state_id, Name = b.text }).FirstOrDefault().Name +", "+

                                                                (from a in db.states
                                                                 join b in db.statetexts on a.name_id equals b.id
                                                                 where b.language_id == "vi" && a.Status == 1 && a.state_id == T.CityContact
                                                                 select new { ID = a.state_id, Name = b.text }).FirstOrDefault().Name
                                                                ,
                    NameContact = T.NameContact,
                    PhoneContact = T.PhoneContact,
                    EmailContact = T.EmailContact,
                    WebSite = T.WebSite
                }).FirstOrDefault(),
                JsonRequestBehavior.AllowGet);
        }
        
        [ActionName("DeActive"), DisplayName("Delete")]
        public JsonResult DeActiveConfirmed(int id)
        {
            var model = _service.GetItem(id);
            model.Active = 0;
            _service.UpdateItem(model);
            return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
        }
    }
}

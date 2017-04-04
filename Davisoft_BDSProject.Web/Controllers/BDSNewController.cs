using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Models;
using ImageResizer;
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
            var NewsTypes = _serviceNewsType.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
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
        [DisplayName(@"New management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"New management")]
        [AjaxOnly]
        public JsonResult IndexAjax(DataTableJS data)
        {
            var itmes = _service.GetIQueryableItems().ToList();
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
                        T =>
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
            data.recordsTotal = _service.GetIQueryableItems().Count();
            data.recordsFiltered = queryFilter.Count();
            data.data = queryFilter.Skip(data.start)
                    .Take(data.length == -1 ? data.recordsTotal : data.length)
                    .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Create()
        {
            var tblNews = new BDSNew();
            _service.CreateItem(tblNews);
            LoadDataList();
            return View(new BDSNew{CreateDate = DateTime.Now,CreateUser = 1,ID = tblNews.ID });
        }

        [HttpPost]
        public ActionResult Create(BDSNew model)
        {



            LoadDataList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
          
            _service.CreateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            LoadDataList();
            BDSNew model = _service.GetItem(id);
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

                string path = Server.MapPath("~/fileUpload/") + DateTime.Now.Day + DateTime.Now.Month + "/";
                // check for directory
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // filename with placeholder for size
                if (picture.GetConvertedFileName() == null || string.IsNullOrWhiteSpace(picture.GetConvertedFileName()))
                    picture.SetFileName(DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + "_" + picture.CreateFilename() + "_{0}." + format);

                if (picture.GetFilePathPhysical(NewsPictures.PictureSize.Large) != null)
                {
                    string dest = path + picture.FileName(NewsPictures.PictureSize.Large);
                    settings.MaxWidth = 800;
                    settings.MaxHeight = 800;
                    if (picture.WaterMarkLarge == NewsPictures.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPictures.PictureSize.Large);
                }

                if (picture.GetFilePathPhysical(NewsPictures.PictureSize.Medium) != null)
                {
                    string dest = path + picture.FileName(NewsPictures.PictureSize.Medium);
                    settings.MaxWidth = 300;
                    settings.MaxHeight = 300;
                    if (picture.WaterMarkLarge == NewsPictures.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPictures.PictureSize.Medium);
                }


                var BDSPicture =new  BDSPicture(); 
                BDSPicture.advert_id = picture.tblPicture.advert_id;
                BDSPicture.position = picture.tblPicture.position;
                BDSPicture.originalFilepath = picture.tblPicture.originalFilepath;
                //BDSPicture.advert_id = newsPicture.idProducts;
                //BDSPicture.position = newsPicture.isactive;
                //BDSPicture.title = newsPicture.nameImg;
                //BDSPicture.convertedFilename = newsPicture.;
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
                    _servicePicture.DeleteItem(item.id);
                }
            }
          
            _service.DeleteItem(model.ID);
            return RedirectToAction("Index", "BDSNew");
        }
    }
}

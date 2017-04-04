using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Resources;

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
        public BDSNewController(IBDSNewService service,
            IBDSAccountService serviceAccount,
            IBDSEducationService serviceEducation,
            IBDSTimeWorkService serviceTimeWork,
            IBDSCareerService serviceCareer,
            IBDSNewsTypeService serviceNewsType,
            IBDSNewsTypePriceService serviceNewsTypePrice,
              IBDSLanguageService serviceLanguage,
            IBDSEmployerInformationService serviceEmployerInformation
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
            LoadDataList();
            return View(new BDSNew{CreateDate = DateTime.Now,CreateUser = 1,ID = 0});
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

    }
}

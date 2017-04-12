using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using Davisoft_BDSProject.Web.Models;
using iTextSharp.text.pdf.qrcode;
using Resources;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class BDSPersonalInformationController : Controller
    {
        Davisoft_BDSProjectEntities db = new Davisoft_BDSProjectEntities();
        private readonly IBDSPersonalInformationService _service;
        private readonly IBDSAccountService _serviceAccount;
        private readonly IBDSEducationService _serviceEducation;
        private readonly IBDSMarriageService _serviceMarriage;
        private readonly IBDSSalaryService _serviceSalary;
        private readonly IBDSTimeWorkService _serviceTimeWork;
        private readonly IBDSCareerService _serviceCareer;
        public BDSPersonalInformationController(IBDSPersonalInformationService service,
            IBDSAccountService serviceAccount,
            IBDSEducationService serviceEducation,
            IBDSMarriageService serviceMarriage,
            IBDSSalaryService serviceSalary,
            IBDSTimeWorkService serviceTimeWork,
            IBDSCareerService serviceCareer
            )
        {
            _service = service;
            _serviceAccount = serviceAccount;
            _serviceEducation = serviceEducation;
            _serviceMarriage = serviceMarriage;
            _serviceSalary = serviceSalary;
            _serviceTimeWork = serviceTimeWork;
            _serviceCareer = serviceCareer;
        }
        [DisplayName(@"BDS Account Per management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"BDS Account Per management")]
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
                    .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Create()
        {
          
            var Cities = (from a in db.states
                join b in db.statetexts on a.name_id equals b.id
                where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Districts = (from a in db.districts
                          join b in db.districttexts on a.name_id equals b.id
                          where b.language_id == "vi"
                          select new  { ID = a.state_id, Name = b.text }).ToList().Select(T=>new SelectListItem{Value = T.ID.ToString(),Text = T.Name.ToString(),Selected = false}).ToList();

         

            Cities.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Districts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });

            var Educations = _serviceEducation.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Marriages = _serviceMarriage.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Salaries = _serviceSalary.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var TimeWorks = _serviceTimeWork.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Careers = _serviceCareer.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            

            Educations.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Marriages.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Salaries.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            TimeWorks.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Careers.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });








            ViewBag.Educations = Educations;
            ViewBag.Marriages = Marriages;
            ViewBag.Salaries = Salaries;
            ViewBag.TimeWorks = TimeWorks;
            ViewBag.Careers = Careers;
            ViewBag.Cities = Cities;
            ViewBag.Districts = new List<SelectListItem>();
            return View(new BDSPersonalInformation { CreateUser = 1, ID = 0,Active = 1, BDSAccount = new BDSAccount { CreateDate = DateTime.Now, Money = 0, Point = 0, Token = Guid.NewGuid().ToString(), MailActive = 1, Active = 1, PassWord = "", CreateUser = 1, ID = 0 } });
        }

        [HttpPost]
        public ActionResult Create(BDSPersonalInformation model)
        {
            var Cities = (from a in db.states
                          join b in db.statetexts on a.name_id equals b.id
                          where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();

            var Districts = (from a in db.districts
                             join b in db.districttexts on a.name_id equals b.id
                             where b.language_id == "vi"
                             select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                
            if (!ModelState.IsValid)
            {


                var Educations = _serviceEducation.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var Marriages = _serviceMarriage.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var Salaries = _serviceSalary.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var TimeWorks = _serviceTimeWork.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var Careers = _serviceCareer.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();


                Educations.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                Marriages.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                Salaries.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                TimeWorks.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                Careers.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });








                ViewBag.Educations = Educations;
                ViewBag.Marriages = Marriages;
                ViewBag.Salaries = Salaries;
                ViewBag.TimeWorks = TimeWorks;
                ViewBag.Careers = Careers;
                ViewBag.Cities = Cities;;
                ViewBag.Districts = Districts;
                return View(model);
            }
        
            model.BDSAccount.PassWord = model.BDSAccount.PassWord;
            model.BDSAccount.KeySearch = model.BDSAccount.Email.NormalizeD() + " " + model.BDSAccount.Money.Value.ToString("n2") + " " +
                              model.BDSAccount.Point.Value.ToString("n2");
            model.BDSAccount= _serviceAccount.CreateItem(model.BDSAccount);

            model.IdAccount = model.BDSAccount.ID;
            model.BDSCareer = _serviceCareer.GetItem(model.IdLoaiNghe);
            model.BDSEducation = _serviceEducation.GetItem(model.Education);
            model.BDSMarriage = _serviceMarriage.GetItem(model.MaritalStatus);
            model.BDSSalary = _serviceSalary.GetItem(model.Salary);
            model.BDSTimeWork = _serviceTimeWork.GetItem(model.Experience);
            model.FullAddress = model.Address.NormalizeD() + ", " + Districts.Where(T => T.Value == model.District.ToString()).FirstOrDefault().Text.NormalizeD() +
            ", " + Cities.Where(T => T.Value == model.City.ToString()).FirstOrDefault().Text.NormalizeD();
            model.KeySearch = model.Name.NormalizeD() + " " + model.Birthday.ToString("dd/MM/yyyy").NormalizeD() + " " + model.Phone.NormalizeD() + " " + (model.Sex == 1 ? "Nam" : "Nữ") + " " + model.FullAddress + " " + model.BDSMarriage.Name.NormalizeD() + " " + model.BDSSalary.Name.NormalizeD() + " " + model.BDSCareer.Name.NormalizeD() + " " + model.BDSTimeWork.Name.NormalizeD() + " " + (model.ProfessionalExperience == 1 ? "Có kinh nghiệm bất động sản".NormalizeD() : "Không có kinh nghiệm bất động sản".NormalizeD()) + " " + model.Description.NormalizeD();
            String path = ImageUpload.GetImagePath(ImageUpload.Upload(Guid.NewGuid().ToString(), Request.Files["UrlImageFile"], 400, 400));
            model.UrlImage = path;
            _service.CreateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            BDSPersonalInformation model = _service.GetItem(id);
     
            var Cities = (from a in db.states
                          join b in db.statetexts on a.name_id equals b.id
                          where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
           
            var Districts = (from a in db.districts
                             join b in db.districttexts on a.name_id equals b.id
                             where b.language_id == "vi"
                             select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            Districts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Cities.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            var Educations = _serviceEducation.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Marriages = _serviceMarriage.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Salaries = _serviceSalary.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var TimeWorks = _serviceTimeWork.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Careers = _serviceCareer.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();


            Educations.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Marriages.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Salaries.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            TimeWorks.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Careers.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });








            ViewBag.Educations = Educations;
            ViewBag.Marriages = Marriages;
            ViewBag.Salaries = Salaries;
            ViewBag.TimeWorks = TimeWorks;
            ViewBag.Careers = Careers;
            ViewBag.Cities = Cities;
            ViewBag.Districts = Districts;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSPersonalInformation model)
        {
            var Cities = (from a in db.states
                          join b in db.statetexts on a.name_id equals b.id
                          where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Districts = (from a in db.districts
                             join b in db.districttexts on a.name_id equals b.id
                             where b.language_id == "vi"
                             select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            if (!ModelState.IsValid)
            {
               
               
                Cities.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                Districts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                var Educations = _serviceEducation.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var Marriages = _serviceMarriage.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var Salaries = _serviceSalary.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var TimeWorks = _serviceTimeWork.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var Careers = _serviceCareer.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();


                Educations.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                Marriages.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                Salaries.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                TimeWorks.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                Careers.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });








                ViewBag.Educations = Educations;
                ViewBag.Marriages = Marriages;
                ViewBag.Salaries = Salaries;
                ViewBag.TimeWorks = TimeWorks;
                ViewBag.Careers = Careers;
                ViewBag.Cities = Cities;
                ViewBag.Districts = Districts;
                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }

            if (_service.GetItem(model.ID).BDSAccount.PassWord != model.BDSAccount.PassWord)
            {
                model.BDSAccount.PassWord = _service.GetItem(model.ID).BDSAccount.PassWord;
            }
            if (Request.Files.Count > 0 && Request.Files["UrlImageFile"] != null && Request.Files["UrlImageFile"].ContentLength>0)
            {
                String path = ImageUpload.GetImagePath(ImageUpload.Upload(Guid.NewGuid().ToString(), Request.Files["UrlImageFile"], 400, 400));
                model.UrlImage = path;
            }
            model.BDSAccount.KeySearch = model.BDSAccount.Email.NormalizeD() + " " + model.BDSAccount.Money.Value.ToString("n2") + " " +
                            model.BDSAccount.Point.Value.ToString("n2");
            _serviceAccount.UpdateItem(model.BDSAccount);

            model.BDSCareer = _serviceCareer.GetItem(model.IdLoaiNghe);
            model.BDSEducation = _serviceEducation.GetItem(model.Education);
            model.BDSMarriage = _serviceMarriage.GetItem(model.MaritalStatus);
            model.BDSSalary = _serviceSalary.GetItem(model.Salary);
            model.BDSTimeWork = _serviceTimeWork.GetItem(model.Experience);
            model.FullAddress = model.Address.NormalizeD() + ", " + Districts.Where(T => T.Value == model.District.ToString()).FirstOrDefault().Text.NormalizeD() +
            ", " + Cities.Where(T => T.Value == model.City.ToString()).FirstOrDefault().Text.NormalizeD();
            model.KeySearch = model.Name.NormalizeD() + " " + model.Birthday.ToString("dd/MM/yyyy").NormalizeD() + " " + model.Phone.NormalizeD() + " " + (model.Sex == 1 ? "Nam" : "Nữ") + " " + model.FullAddress + " " + model.BDSMarriage.Name.NormalizeD() + " " + model.BDSSalary.Name.NormalizeD() + " " + model.BDSCareer.Name.NormalizeD() + " " + model.BDSTimeWork.Name.NormalizeD() + " " + (model.ProfessionalExperience == 1 ? "Có kinh nghiệm bất động sản".NormalizeD() : "Không có kinh nghiệm bất động sản".NormalizeD()) + " " + model.Description.NormalizeD();
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

        public JsonResult LoadDistricts()
        {
            
            var Districts = (from a in db.districts
                join b in db.districttexts on a.name_id equals b.id
                where b.language_id == "vi"
                select new {ID = a.state_id, Name = b.text,State_ID=a.state_id}).ToList();
            return Json(Districts, JsonRequestBehavior.AllowGet);

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

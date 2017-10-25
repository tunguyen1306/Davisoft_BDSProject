using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
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
        Entities db = new Entities();
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
                         (search == "" || (search != null &&
                           (T.KeySearch.ToLower().Contains(search.ToLower())))));
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
        [AjaxOnly]
        public JsonResult ApplyAjax(DataTableJS data,int? id)
        {
            if (id.HasValue && id>0)
            {
                var q = (from a in db.BDSApplies
                         join b in db.BDSEmployerInformationEns on a.IdAccountEm equals b.IdAccount
                         join c in db.BDSPersonalInformationEns on a.IdAccountPer equals c.IdAccount
                         join d in db.BDSNewEns on a.IdNews equals d.ID
                         where a.TypeProfile == 1 && c.ID == id orderby a.CreateDate
                         select new { Date = a.CreateDate, IdNew = a.IdNews, IdCompany = b.ID, IdAccount = b.IdAccount, TitleNew = d.Title, FromDateNew = d.FromCreateNews, ToDateNew = d.ToCreateNews, NameCompany = b.Name ,Carrers=d.Career});
                var dataCarrer = _serviceCareer.GetIQueryableItems().ToList().Select(T=>new {ID=T.ID.ToString(),Name=T.Name}).ToList();
                data.recordsTotal = q.Count();
                data.recordsFiltered = q.Count();
                var dataEx = q.Skip(data.start)
                    .Take(data.length == -1 ? data.recordsTotal : data.length)
                    .ToList().Select(T => new { Carrers = String.Join(",", dataCarrer.Where(X => T.Carrers.Split(',').Contains(X.ID)).Select(X => X.Name).ToArray()), Date = T.Date, IdNew = T.IdNew, IdCompany = T.IdCompany, IdAccount = T.IdAccount, TitleNew = T.TitleNew, FromDateNew = T.FromDateNew, ToDateNew = T.ToDateNew, NameCompany = T.NameCompany });
                data.data = dataEx;
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public PartialViewResult ViewApply(int? id)
        {
            
            return PartialView();
        }
        public ActionResult Create()
        {

          
            var Cities = (from a in db.States
                join b in db.StateTexts on a.name_id equals b.id
                where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Districts = (from a in db.Districts
                          join b in db.DistrictTexts on a.name_id equals b.id
                          where b.language_id == "vi"
                          select new  { ID = a.state_id, Name = b.text }).ToList().Select(T=>new SelectListItem{Value = T.ID.ToString(),Text = T.Name.ToString(),Selected = false}).ToList();

         

            Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });
            Districts.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });

           
            var Marriages = _serviceMarriage.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            Marriages.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });
            ViewBag.Marriages = Marriages;
         
            ViewBag.Cities = Cities;
            ViewBag.Districts = new List<SelectListItem>();
            return View(new BDSPersonalInformation { CreateUser = 1, ID = 0,Active = 1, BDSAccount = new BDSAccount { CreateDate = DateTime.Now, Money = 0, Point = 0, Token = Guid.NewGuid().ToString(), MailActive = 1, Active = 1, PassWord = "", CreateUser = 1, ID = 0 } });
        }

        [HttpPost]
        public ActionResult Create(BDSPersonalInformation model)
        {
            var Cities = (from a in db.States
                          join b in db.StateTexts on a.name_id equals b.id
                          where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();

            var Districts = (from a in db.Districts
                             join b in db.DistrictTexts on a.name_id equals b.id
                             where b.language_id == "vi"
                             select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                
            if (!ModelState.IsValid)
            {


             
                var Marriages = _serviceMarriage.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            
            
                Marriages.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });
           







        
                ViewBag.Marriages = Marriages;
         
                ViewBag.Cities = Cities;;
                ViewBag.Districts = Districts;
                return View(model);
            }
        
            model.BDSAccount.PassWord = model.BDSAccount.PassWord;
            model.BDSAccount.MailActive = model.BDSAccount.MailActive;
            model.BDSAccount.KeySearch = model.BDSAccount.Email.NormalizeD() + " " + model.BDSAccount.Money.Value.ToString("n2") + " " +
                              model.BDSAccount.Point.Value.ToString("n2");
            model.BDSAccount= _serviceAccount.CreateItem(model.BDSAccount);

            model.IdAccount = model.BDSAccount.ID;
         
            model.BDSMarriage = _serviceMarriage.GetItem(model.MaritalStatus);
       
            model.KeySearch = model.Name.NormalizeD() + " " + model.Birthday.ToString("dd/MM/yyyy").NormalizeD() + " " + model.Phone.NormalizeD() + " " + (model.Sex == 1 ? "Nam" : "Nữ") + " " + model.TemporaryAddress + " " + model.BDSMarriage.Name.NormalizeD() ;
            var path = string.Empty;
            var path1 = string.Empty;
            var NewPath = string.Empty;
            var fortmatName = string.Empty;
            var fileNameFull = string.Empty;




            var file = System.Web.HttpContext.Current.Request.Files["UrlImageFile"];
            if (file != null && file.ContentLength > 0)
            {

                var fileName = Path.GetFileName(file.FileName);
                string newFileNmae = Path.GetFileNameWithoutExtension(fileName);
                fortmatName = Path.GetExtension(fileName);

                NewPath = newFileNmae.Replace(newFileNmae, (DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()).ToString());
                fileNameFull = DateTime.Now.Day + "" + DateTime.Now.Month + "_" + NewPath + fortmatName;
                if (Server.MapPath("~/UploadImg/").Contains(ConfigurationManager.AppSettings["HostAdmin"]))
                {
                    path = Server.MapPath("~/UploadImg/").Replace(ConfigurationManager.AppSettings["HostAdmin"], ConfigurationManager.AppSettings["HostWeb"]) + DateTime.Now.Day + DateTime.Now.Month + "/";
                }
                else
                {
                    path = Server.MapPath("~/UploadImg/").Replace("Davisoft_BDSProject.Web", "WebBDS_Project") + DateTime.Now.Day + DateTime.Now.Month + "/";
                }
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path1 = Path.Combine(path, fileNameFull);
                file.SaveAs(path1);
            }
            model.UrlImage = fileNameFull;
            _service.CreateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            BDSPersonalInformation model = _service.GetItem(id);
     
            var Cities = (from a in db.States
                          join b in db.StateTexts on a.name_id equals b.id
                          where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
           
            var Districts = (from a in db.Districts
                             join b in db.DistrictTexts on a.name_id equals b.id
                             where b.language_id == "vi"
                             select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            Districts.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });
            Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });
          
            var Marriages = _serviceMarriage.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
          

        
            Marriages.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });
      








        
            ViewBag.Marriages = Marriages;
          
            ViewBag.Cities = Cities;
            ViewBag.Districts = Districts;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSPersonalInformation model)
        {
            var Cities = (from a in db.States
                          join b in db.StateTexts on a.name_id equals b.id
                          where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Districts = (from a in db.Districts
                             join b in db.DistrictTexts on a.name_id equals b.id
                             where b.language_id == "vi"
                             select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            if (!ModelState.IsValid)
            {
               
               
                Cities.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });
                Districts.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });
                
                var Marriages = _serviceMarriage.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
               
                Marriages.Insert(0, new SelectListItem { Value = "", Text = "Chọn", Selected = true });
              








             
                ViewBag.Marriages = Marriages;
            
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
            var path = string.Empty;
            var path1 = string.Empty;
            var NewPath = string.Empty;
            var fortmatName = string.Empty;
            var fileNameFull = string.Empty;

            model.BDSAccount.MailActive = model.BDSAccount.MailActive;


            var file = System.Web.HttpContext.Current.Request.Files["UrlImageFile"];
            if (file != null && file.ContentLength > 0)
            {

                var fileName = Path.GetFileName(file.FileName);
                string newFileNmae = Path.GetFileNameWithoutExtension(fileName);
                fortmatName = Path.GetExtension(fileName);

                NewPath = newFileNmae.Replace(newFileNmae, (DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()).ToString());
                fileNameFull = DateTime.Now.Day + "" + DateTime.Now.Month + "_" + NewPath + fortmatName;
                if (Server.MapPath("~/UploadImg/").Contains(ConfigurationManager.AppSettings["HostAdmin"]))
                {
                    path = Server.MapPath("~/UploadImg/").Replace(ConfigurationManager.AppSettings["HostAdmin"], ConfigurationManager.AppSettings["HostWeb"]) + DateTime.Now.Day + DateTime.Now.Month + "/";
                }
                else
                {
                    path = Server.MapPath("~/UploadImg/").Replace("Davisoft_BDSProject.Web", "WebBDS_Project") + DateTime.Now.Day + DateTime.Now.Month + "/";
                }
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path1 = Path.Combine(path, fileNameFull);
                file.SaveAs(path1);
            }
            model.UrlImage = fileNameFull;
            model.BDSAccount.KeySearch = model.BDSAccount.Email.NormalizeD() + " " + model.BDSAccount.Money.Value.ToString("n2") + " " +
                            model.BDSAccount.Point.Value.ToString("n2");
            _serviceAccount.UpdateItem(model.BDSAccount);

       
            model.BDSMarriage = _serviceMarriage.GetItem(model.MaritalStatus);
        
            model.KeySearch = model.Name.NormalizeD() + " " + model.Birthday.ToString("dd/MM/yyyy").NormalizeD() + " " + model.Phone.NormalizeD() + " " + (model.Sex == 1 ? "Nam" : "Nữ") + " " + model.TemporaryAddress + " " + model.BDSMarriage.Name.NormalizeD() + " " + model.Description.NormalizeD();
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
            
            var Districts = (from a in db.Districts
                join b in db.DistrictTexts on a.name_id equals b.id
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

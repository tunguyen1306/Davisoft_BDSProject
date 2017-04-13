using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class BDSEmployerInformationController : Controller
    {
        davisoft_bdsprojectEntities db = new davisoft_bdsprojectEntities();
        private readonly IBDSEmployerInformationService _service;
        private readonly IBDSAccountService _serviceAccount;
        private readonly IBDSTypeContactService _serviceTypeContact;
        private readonly IBDSScopeService _serviceScope;
        public BDSEmployerInformationController(IBDSEmployerInformationService service, IBDSTypeContactService serviceTypeContact, IBDSScopeService serviceScope, IBDSAccountService serviceAccount)
        {
            _service = service;
            _serviceAccount = serviceAccount;
            _serviceTypeContact = serviceTypeContact;
            _serviceScope = serviceScope;
        }
        [DisplayName(@"BDS Account Em management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"BDS Account Em management")]
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
                join b in db.stateTexts on a.name_id equals b.id
                where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Districts = (from a in db.districts
                          join b in db.districttexts on a.name_id equals b.id
                          where b.language_id == "vi"
                          select new  { ID = a.state_id, Name = b.text }).ToList().Select(T=>new SelectListItem{Value = T.ID.ToString(),Text = T.Name.ToString(),Selected = false}).ToList();

         

            Cities.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Districts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            var Scopes = _serviceScope.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var TypeContacts = _serviceTypeContact.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            Scopes.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            TypeContacts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            ViewBag.Scopes = Scopes;
            ViewBag.TypeContacts = TypeContacts;
            ViewBag.Cities = Cities;
            ViewBag.Districts = new List<SelectListItem>();
            return View(new BDSEmployerInformation { CreateUser = 1, ID = 0,Active = 1, BDSAccount = new BDSAccount { CreateDate = DateTime.Now, Money = 0, Point = 0, Token = Guid.NewGuid().ToString(), MailActive = 1, Active = 1, PassWord = "", CreateUser = 1, ID = 0 } });
        }

        [HttpPost]
        public ActionResult Create(BDSEmployerInformation model)
        {
            var Cities = (from a in db.states
                          join b in db.stateTexts on a.name_id equals b.id
                          where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();

            var Districts = (from a in db.districts
                             join b in db.districttexts on a.name_id equals b.id
                             where b.language_id == "vi"
                             select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                
            if (!ModelState.IsValid)
            {
              
              
                Districts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                Cities.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                var Scopes = _serviceScope.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var TypeContacts = _serviceTypeContact.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                Scopes.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                TypeContacts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                ViewBag.Scopes = Scopes;
                ViewBag.TypeContacts = TypeContacts;
                ViewBag.Cities = Cities;;
                ViewBag.Districts = Districts;
                return View(model);
            }
        
            model.BDSAccount.PassWord = model.BDSAccount.PassWord;
            model.BDSAccount.KeySearch = model.BDSAccount.Email.NormalizeD() + " " + model.BDSAccount.Money.Value.ToString("n2") + " " +
                              model.BDSAccount.Point.Value.ToString("n2");
            model.BDSAccount= _serviceAccount.CreateItem(model.BDSAccount);

            model.IdAccount = model.BDSAccount.ID;
            model.BDSScope = _serviceScope.GetItem(model.Scope);
            model.FullAddress = model.Address.NormalizeD() + ", " + Districts.Where(T => T.Value == model.District.ToString()).FirstOrDefault().Text.NormalizeD() +
            ", " + Cities.Where(T => T.Value == model.City.ToString()).FirstOrDefault().Text.NormalizeD();
            model.KeySearch = model.Name.NormalizeD() + " " + model.BDSScope.Name.NormalizeD() + " " + model.FullAddress.NormalizeD()+" "+model.Phone+" "+model.Fax+" "+model.WebSite+" "+model.Description.NormalizeD()+" "+model.NameContact.NormalizeD()+" "+model.Phone+" "+model.Fax+" " ;
           // String path = ImageUpload.GetImagePath(ImageUpload.Upload(Guid.NewGuid().ToString(), Request.Files["UrlImageFile"], 400, 400));
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
                path = Server.MapPath("~/fileUpload/").Replace("adminbds.vangia.net", "webtuyendung.vangia.net") + DateTime.Now.Day + DateTime.Now.Month + "/";
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
            BDSEmployerInformation model = _service.GetItem(id);
     
            var Cities = (from a in db.states
                          join b in db.stateTexts on a.name_id equals b.id
                          where b.language_id == "vi" && a.Status == 1
                          select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
           
            var Districts = (from a in db.districts
                             join b in db.districttexts on a.name_id equals b.id
                             where b.language_id == "vi"
                             select new { ID = a.state_id, Name = b.text }).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            Districts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Cities.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            var Scopes = _serviceScope.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var TypeContacts = _serviceTypeContact.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            Scopes.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            TypeContacts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            ViewBag.Scopes = Scopes;
            ViewBag.TypeContacts = TypeContacts;
            ViewBag.Cities = Cities;
            ViewBag.Districts = Districts;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSEmployerInformation model)
        {
            var Cities = (from a in db.states
                          join b in db.stateTexts on a.name_id equals b.id
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
                var Scopes = _serviceScope.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                var TypeContacts = _serviceTypeContact.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
                Scopes.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                TypeContacts.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
                ViewBag.Scopes = Scopes;
                ViewBag.TypeContacts = TypeContacts;
                ViewBag.Cities = Cities;
                ViewBag.Districts = Districts;
                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }

            if (_service.GetItem(model.ID).BDSAccount.PassWord != model.BDSAccount.PassWord)
            {
                model.BDSAccount.PassWord =  _service.GetItem(model.ID).BDSAccount.PassWord;
            }
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
                path = Server.MapPath("~/fileUpload/").Replace("adminbds.vangia.net", "webtuyendung.vangia.net") + DateTime.Now.Day + DateTime.Now.Month + "/";
              
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path1 = Path.Combine(path, fileNameFull);
                file.SaveAs(path1);
            }
            model.UrlImage = fileNameFull;
            model.BDSAccount.KeySearch = model.BDSAccount.Email.NormalizeD() + " " + model.BDSAccount.Money.Value.ToString("n2") + " " +
                            model.BDSAccount.Point.Value.ToString("n2");
            _serviceAccount.UpdateItem(model.BDSAccount);

            model.BDSScope = _serviceScope.GetItem(model.Scope);
            model.FullAddress = model.Address.NormalizeD() + ", " + Districts.Where(T => T.Value == model.District.ToString()).FirstOrDefault().Text.NormalizeD() +
            ", " + Cities.Where(T => T.Value == model.City.ToString()).FirstOrDefault().Text.NormalizeD();
            model.KeySearch = model.Name.NormalizeD() + " " + model.BDSScope.Name.NormalizeD() + " " + model.FullAddress.NormalizeD() + " " + model.Phone + " " + model.Fax + " " + model.WebSite + " " + model.Description.NormalizeD() + " " + model.NameContact.NormalizeD() + " " + model.Phone + " " + model.Fax + " ";
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

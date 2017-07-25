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
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using Davisoft_BDSProject.Web.Models;
using Resources;
using BDSBank = Davisoft_BDSProject.Domain.Entities.BDSBank;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class BDSBankController : Controller
    {
        private readonly IBDSBankService _service;
   
        public BDSBankController(IBDSBankService service)
        {
            _service = service;
      
        }
        [DisplayName(@"Bank management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"Bank management")]
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
        public ActionResult Create()
        {
         
            return View(new BDSBank { ID = 0, CreateDate = DateTime.Now, CreateUser = 1 });
        }

        [HttpPost]
        public ActionResult Create(BDSBank model)
        {
            if (!ModelState.IsValid)
            {
            
                return View(model);
            }
            model.KeySearch = model.Name.NormalizeD() + " " + 
                model.AccountName.NormalizeD() + " " +
                model.AccountNumber.NormalizeD() + " " + 
                model.Branch.NormalizeD() + " " +
                (String.IsNullOrEmpty(model.Address)
                                ? ""
                                : model.Address.NormalizeD()) + " " +
                (String.IsNullOrEmpty(model.Description)
                                ? ""
                                : model.Description.NormalizeD());
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
          
            BDSBank model = _service.GetItem(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSBank model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }
            BDSBank modelDB = _service.GetItem(model.ID);
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
            model.KeySearch = model.Name.NormalizeD() + " " +
               model.AccountName.NormalizeD() + " " +
               model.AccountNumber.NormalizeD() + " " +
               model.Branch.NormalizeD() + " " +
               (String.IsNullOrEmpty(model.Address)
                               ? ""
                               : model.Address.NormalizeD()) + " " +
               (String.IsNullOrEmpty(model.Description)
                               ? ""
                               : model.Description.NormalizeD());
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

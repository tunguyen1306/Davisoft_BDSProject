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
using Resources;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class BDSNewsTypeController : Controller
    {
        private readonly IBDSNewsTypeService _service;

        public BDSNewsTypeController(IBDSNewsTypeService service)
        {
            _service = service;
        }
        [DisplayName(@"Marriage management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"Marriage management")]
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
            return View(new BDSNewsType { CreateDate = DateTime.Now, CreateUser = 1, ID = 0 });
        }

        [HttpPost]
        public ActionResult Create(BDSNewsType model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
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
            model.UrlIcon = fileNameFull;
            model.KeySearch = model.Name.NormalizeD() + " " + model.CodeNewsType.NormalizeD() + " " +
                            (String.IsNullOrEmpty(model.Description)
                                ? ""
                                : model.Description.NormalizeD());
            _service.CreateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            BDSNewsType model = _service.GetItem(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSNewsType model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }
            model.KeySearch = model.Name.NormalizeD() + " " + model.CodeNewsType.NormalizeD() + " " +
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
                path = Server.MapPath("~/fileUpload/").Replace("adminbds.vangia.net", "webtuyendung.vangia.net") + DateTime.Now.Day + DateTime.Now.Month + "/";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path1 = Path.Combine(path, fileNameFull);
                file.SaveAs(path1);
            }
            model.UrlIcon = fileNameFull;
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

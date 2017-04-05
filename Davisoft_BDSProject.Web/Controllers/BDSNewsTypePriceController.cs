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
using Resources;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class BDSNewsTypePriceController : Controller
    {
        private readonly IBDSNewsTypeService _serviceNewsType;

        private readonly IBDSNewsTypePriceService _service;

        public BDSNewsTypePriceController(IBDSNewsTypePriceService service, IBDSNewsTypeService serviceNewsType)
        {
            _service = service;

            _serviceNewsType = serviceNewsType;
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
            ViewBag.ListNewsType = _serviceNewsType.GetIQueryableItems().Where(T => T.Active == 1).ToList();
            return View(new BDSNewsTypePrice { CreateDate = DateTime.Now, CreateUser = 1, ID = 0 ,Price = 20000,ApplyPrice = DateTime.Now,Perfix = 1});
        }

        [HttpPost]
        public ActionResult Create(BDSNewsTypePrice model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ListNewsType = _serviceNewsType.GetIQueryableItems().Where(T => T.Active == 1).ToList();
                return View(model);
            }

           
            model.KeySearch = model.Name.NormalizeD() + " " +
                            (String.IsNullOrEmpty(model.Description)
                                ? ""
                                : model.Description.NormalizeD());
            _service.CreateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListNewsType = _serviceNewsType.GetIQueryableItems().Where(T => T.Active == 1).ToList();
            BDSNewsTypePrice model = _service.GetItem(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSNewsTypePrice model)
        {
          
            if (!ModelState.IsValid)
            {
                ViewBag.ListNewsType = _serviceNewsType.GetIQueryableItems().Where(T => T.Active == 1).ToList();
                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }
            model.KeySearch = model.Name.NormalizeD() + " " +
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

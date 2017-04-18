using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Models;
using Resources;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class BDSEventController : Controller
    {
        private readonly IBDSEventService _service;

        public BDSEventController(IBDSEventService service)
        {
            _service = service;
        }
        [DisplayName(@"Event management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"Event management")]
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
                    .ToList().Select(T => new { ID = T.ID, DisPercent = T.DisPercent, Description = T.Description, FromDateToDate = T.FromDate.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern) + " - " + T.ToDate.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern), Name = T.Name, TypeApply = T.TypeApply, Active = T.Active });
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Create()
        {
            return View(new BDSEvent{CreateDate = DateTime.Now,CreateUser = 1,ID = 0});
        }

        [HttpPost]
        public ActionResult Create(BDSEvent model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var fromDate = model.FromDateToDate.Split('-')[0];
            var toDate = model.FromDateToDate.Split('-')[1];



            model.FromDate = DateTime.Parse(fromDate, MvcApplication.CultureInfo, DateTimeStyles.None);
            model.ToDate = DateTime.Parse(toDate, MvcApplication.CultureInfo, DateTimeStyles.None);
            model.KeySearch = model.Name.NormalizeD() + " " + model.FromDate.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern).NormalizeD() + " " + model.ToDate.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern).NormalizeD() + " " +
                       (String.IsNullOrEmpty(model.Description)
                           ? ""
                           : model.Description.NormalizeD());
            _service.CreateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            BDSEvent model = _service.GetItem(id);
            model.FromDateToDate = model.FromDate.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern) + " - " + model.ToDate.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSEvent model)
        {
            var fromDate = model.FromDateToDate.Split('-')[0];
            var toDate = model.FromDateToDate.Split('-')[1];  
            model.FromDate = DateTime.Parse(fromDate,MvcApplication.CultureInfo, DateTimeStyles.None);
            model.ToDate = DateTime.Parse(toDate, MvcApplication.CultureInfo, DateTimeStyles.None);
          
            if (!ModelState.IsValid)
            {

                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }

            model.KeySearch = model.Name.NormalizeD() + " " + model.FromDate.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern).NormalizeD() + " " + model.ToDate.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern).NormalizeD() + " " +
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

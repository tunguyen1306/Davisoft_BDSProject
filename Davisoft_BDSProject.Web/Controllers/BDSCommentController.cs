using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Models;
using Resources;
using BDSComment = Davisoft_BDSProject.Domain.Entities.BDSComment;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class BDSCommentController : Controller
    {
        private readonly IBDSCommentService _service;

        public BDSCommentController(IBDSCommentService service)
        {
            _service = service;
        }
        [DisplayName(@"Comment management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"Comment management")]
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
            return View(new BDSComment{CreateDate = DateTime.Now,CreateUser = 1,ID = 0,DateComment=DateTime.Now,Status = 0});
        }

        [HttpPost]
        public ActionResult Create(BDSComment model)
        {
            model.Name = model.NameComment;
            model.Description = model.Description;
            model.Status = 0;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.KeySearch = model.Name.NormalizeD() + " " + model.TextComment.NormalizeD() + " " + model.DateComment.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern).NormalizeD() + " " +
                            (String.IsNullOrEmpty(model.Description)
                                ? ""
                                : model.Description.NormalizeD());
            _service.CreateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            BDSComment model = _service.GetItem(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSComment model)
        {
            model.Name = model.NameComment;
            model.Description = model.Description;
            if (!ModelState.IsValid)
            {

                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }
            model.KeySearch = model.Name.NormalizeD() + " " + model.TextComment.NormalizeD() + " " + model.DateComment.Value.ToString(MvcApplication.DateTimeFormat.ShortDatePattern).NormalizeD() + " " +
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

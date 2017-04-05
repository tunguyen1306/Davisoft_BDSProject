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
using Davisoft_BDSProject.Web.Models;
using Resources;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class BDSBranchController : Controller
    {
        private readonly IBDSBranchService _service;
        private readonly IBDSAreaService _areaservice;
        public BDSBranchController(IBDSBranchService service, IBDSAreaService areaservice)
        {
            _service = service;
            _areaservice = areaservice;
        }
        [DisplayName(@"BDSBranch management")]
        public ActionResult Index()
        {
            return View();
        }

        private void LoadDataList()
        {
            var Areas = _areaservice.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();

            Areas.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            ViewBag.Areas = Areas;
           
        }

        [DisplayName(@"BDSBranch management")]
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
            LoadDataList();
            return View(new BDSBranch{CreateDate = DateTime.Now,CreateUser = 1,ID = 0});
        }

        [HttpPost]
        public ActionResult Create(BDSBranch model)
        {
            if (!ModelState.IsValid)
            {
                LoadDataList();
                return View(model);
            }
            model.BDSArea = _areaservice.GetItem(model.IdArea);
            model.KeySearch = model.Name.NormalizeD() + " " + model.BDSArea.Name.NormalizeD() + " " + model.Tel.NormalizeD() + " " + model.Phone.NormalizeD() + " " + model.Address.NormalizeD() + " " +
                            (String.IsNullOrEmpty(model.Description)
                                ? ""
                                : model.Description.NormalizeD());
            _service.CreateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            LoadDataList();
            BDSBranch model = _service.GetItem(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSBranch model)
        {
            if (!ModelState.IsValid)
            {
                LoadDataList();
                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }
            model.BDSArea = _areaservice.GetItem(model.IdArea);
            model.KeySearch = model.Name.NormalizeD() + " " + model.BDSArea.Name.NormalizeD() + " " + model.Tel.NormalizeD() + " " + model.Phone.NormalizeD() + " " + model.Address.NormalizeD() + " " +
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

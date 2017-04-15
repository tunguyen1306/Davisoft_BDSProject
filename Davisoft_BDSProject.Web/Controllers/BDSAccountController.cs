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
    public class BDSAccountController : Controller
    {
        private readonly IBDSAccountService _service;

        public BDSAccountController(IBDSAccountService service)
        {
            _service = service;
        }
        [DisplayName(@"BDS Account management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"BDS Account management")]
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
                    .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Create()
        {
            return View(new BDSAccount { CreateDate = DateTime.Now, Money = 0, Point = 0, Token = Guid.NewGuid().ToString(), MailActive = 1, Active = 1, PassWord = "123456", CreateUser = 1, ID = 0 });
        }

        [HttpPost]
        public ActionResult Create(BDSAccount model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.KeySearch = model.Email.NormalizeD() + " " + model.Money.Value.ToString("n2")+" "+
                           model.Point.Value.ToString("n2");
            model.PassWord = EncryptHelper.EncryptPassword(model.PassWord);
            _service.CreateItem(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            BDSAccount model = _service.GetItem(id);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSAccount model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }
           
            model.KeySearch = model.Email.NormalizeD() + " " + model.Money.Value.ToString("n2") + " " +
                              model.Point.Value.ToString("n2");
            model.PassWord = _service.GetItem(model.ID).PassWord;
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

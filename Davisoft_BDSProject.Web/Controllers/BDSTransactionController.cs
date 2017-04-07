﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
    public class BDSTransactionController : Controller
    {
        private readonly IBDSEventService _serviceEvent;
        private readonly IBDSTransactionService _service;
        private readonly IBDSAccountService _serviceAccount;
        private readonly IBDSBankService _serviceBank;
        private readonly IBDSEmployerInformationService _serviceEmployerInformation;
        private readonly IBDSBranchService _serviceBranch;
        public BDSTransactionController(IBDSTransactionService service,
            IBDSAccountService serviceAccount,
            IBDSBankService serviceBank,
            IBDSEmployerInformationService serviceEmployerInformation,
            IBDSBranchService serviceBranch,
            IBDSEventService serviceEvent
            )
        {
            _service = service;
            _serviceAccount = serviceAccount;
            _serviceBank = serviceBank;
            _serviceEmployerInformation = serviceEmployerInformation;
            _serviceBranch = serviceBranch;
            _serviceEvent = serviceEvent;
        }

        void LoadDataList()
        {
            var EmployerInformations = _serviceEmployerInformation.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.BDSAccount.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Banks = _serviceBank.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            var Branchs = _serviceBranch.GetIQueryableItems().Where(T => T.Active == 1).ToList().Select(T => new SelectListItem { Value = T.ID.ToString(), Text = T.Name.ToString(), Selected = false }).ToList();
            EmployerInformations.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Banks.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            Branchs.Insert(0, new SelectListItem { Value = "", Text = "Please Select", Selected = true });
            ViewBag.Banks = Banks;
            ViewBag.Branchs = Branchs;
            ViewBag.EmployerInformations = EmployerInformations;
        }
        [DisplayName(@"Transaction management")]
        public ActionResult Index()
        {
            return View();
        }
        [DisplayName(@"Transaction management")]
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
            return View(new BDSTransaction{CreateDate = DateTime.Now,CreateUser = 1,ID = 0});
        }

        [HttpPost]
        public ActionResult Create(BDSTransaction model)
        {
            if (!ModelState.IsValid)
            {
                LoadDataList();
                return View(model);
            }
            model.TranDate = DateTime.Now;
            model.BDSAccount = _serviceAccount.GetItem(model.IdAccount);
            if (model.IdBank.HasValue)
            {
                model.BDSBank = _serviceBank.GetItem(model.IdBank.Value);
            }
            if (model.IdBranch.HasValue)
            {
                model.BDSBranch = _serviceBranch.GetItem(model.IdBranch.Value);
            }

            model.KeySearch = model.Name.NormalizeD() + " " + model.TranDate.ToString("dd/MM/yyyy HH:mm:ss").NormalizeD() + " " +
                              (String.IsNullOrEmpty(model.Description)
                                  ? ""
                                  : model.Description.NormalizeD());
            _service.CreateItem(model);
            var account = _serviceAccount.GetItem(model.IdAccount);
            account.Money += model.Money + model.MoneyEventAdd;
            account.Point += model.Point;
            _serviceAccount.UpdateItem(account);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            LoadDataList();
            BDSTransaction model = _service.GetItem(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BDSTransaction model)
        {
            if (!ModelState.IsValid)
            {
                LoadDataList();
                ViewBag.Success = false;
                ViewBag.Message = Resource.SaveFailed;
                return View(model);
            }
            var modelDB = _service.GetItem(model.ID);
            model.BDSAccount = _serviceAccount.GetItem(model.IdAccount);
            if (model.IdBank.HasValue)
            {
                model.BDSBank = _serviceBank.GetItem(model.IdBank.Value);
            }
            if (model.IdBranch.HasValue)
            {
                model.BDSBranch = _serviceBranch.GetItem(model.IdBranch.Value);
            }
            model.KeySearch = model.Name.NormalizeD() + " " + model.TranDate.ToString("dd/MM/yyyy HH:mm:ss").NormalizeD() + " " +
                              (String.IsNullOrEmpty(model.Description)
                                  ? ""
                                  : model.Description.NormalizeD());
            _service.UpdateItem(model);


            var account = _serviceAccount.GetItem(model.IdAccount);
            account.Money += model.Money + model.MoneyEventAdd - modelDB.Money-model.MoneyEventAdd;
            account.Point += model.Point - modelDB.Point;
            _serviceAccount.UpdateItem(account);


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

        [AjaxOnly]
        public JsonResult CheckEventAjax(double money)
        {
            double ME = 0;
            double DE = 0;
            int P = 0;
            var dateNow = DateTime.Now;
            BDSEvent ev= _serviceEvent.GetIQueryableItems().Where(T => T.FromDate <= dateNow && dateNow <= T.ToDate).OrderBy(T=>T.FromDate).FirstOrDefault();
            if (ev!=null)
            {
               ME= money*ev.DisPercent/100;
                DE = ev.DisPercent;
            }

            return Json(new { M = money, ME = ME, P = P, DE = DE }, JsonRequestBehavior.AllowGet);

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
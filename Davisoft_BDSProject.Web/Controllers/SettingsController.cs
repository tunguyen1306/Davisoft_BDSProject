using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Web.Infrastructure;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using Davisoft_BDSProject.Web.Models;
using Resources;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingRepository _settingRepo;
        public static readonly List<string> LoginRequired = new List<string> { "Home" };
        public SettingsController(ISettingRepository settingRepo)
        {
            _settingRepo = settingRepo;
        }

        //
        // GET: /Settings/
        [DisplayName(@"Setting management")]
        public ViewResult Index()
        {
            return View(_settingRepo.GetSettings());
        }

        [HttpPost]
        [DisplayName(@"Setting management")]
        public ActionResult Index(SettingModel setting)
        {
            if (ModelState.IsValid)
            {
                var lstId = Request["Id"].Split(',');
                foreach (var id in lstId)
                {
                    var item = _settingRepo.GetSetting(Convert.ToInt32(id));
                    //if (item.Name.ToLower().Contains(Setting.ModuleKey.SystemConfig.ApiUsername.ToString().ToLower()))
                    //{
                    //    item.Value = Request[item.Name].Split(',')[0];
                    //}
                    //else if (item.Name.ToLower().Contains(Setting.ModuleKey.SystemConfig.ApiPassword.ToString().ToLower()))
                    //{
                    //    item.Value = Request[item.Name].Split(',')[0];
                    //}
                    //else if (
                    //   item.Name.ToLower()
                    //       .Contains(Setting.ModuleKey.Booking.ContractNumberFormat.ToString().ToLower()))
                    //    item.Value = Request[item.Name];
                    //else if (
                    //    item.Name.ToLower()
                    //        .Contains(Setting.ModuleKey.Booking.ContractRunningNumberLenght.ToString().ToLower()))
                    //{
                    //    item.Value = Request[item.Name].Split(',')[0];

                    //}
                    //else
                    item.Value = Request[item.Name].Split(',')[0];
                    _settingRepo.UpdateSetting(item);
                }
            }

            return View(_settingRepo.GetSettings());
        }
    }
}

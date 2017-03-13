using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Models;
using Resources;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class HolidayController : Controller
    {
        //
        // GET: /Holiday/
        public readonly IUnitRepository _repo;

        public HolidayController(IUnitRepository repo)
        {
            _repo = repo;
        }
        [DisplayName(@"Holiday management")]
        public ActionResult Index()
        {
            var model = new HolidayModelView();
            var thisYear = new DateTime(DateTime.Today.Year, 1, 1);
            var holidays = _repo.GetAllHolidays().Where(m => m.Date.Date >= thisYear).ToList();
            if (!holidays.Any())
            {
                holidays.Add(new Holiday { Date = default(DateTime) });
            }
            model.Holidays = holidays;
            return View(model);
        }
        [AjaxOnly]
        public ActionResult Submit(HolidayModelView model, string typesubmit)
        {
            if (typesubmit == "save")
            {
                var holiday = new Holiday { Date = DateTime.Parse(model.HolidayModel.DateSelected), Description = model.HolidayModel.Description, IsFullDay = model.HolidayModel.IsFullDay };
                holiday = _repo.CreateHoliday(holiday);
                typesubmit = model.HolidayModel.ID > 0 ? "update" : "create";
                return Json(new
                {
                    TypeSubmit = typesubmit,
                    Holiday = new
                    {
                        model.HolidayModel.Year,
                        model.HolidayModel.Month,
                        model.HolidayModel.Day,
                        model.HolidayModel.DateSelected,
                        model.HolidayModel.Description,
                        model.HolidayModel.IsFullDay,
                        holiday.ID,
                    },
                    Status = 1,
                }, JsonRequestBehavior.AllowGet);
            }
            else if (typesubmit == "delete")
            {
                _repo.DeleteHoliday(DateTime.Parse(model.HolidayModel.DateSelected));
                return Json(new
                {
                    TypeSubmit = typesubmit,
                    Holiday = new
                    {
                        model.HolidayModel.Year,
                        model.HolidayModel.Month,
                        model.HolidayModel.Day,
                        model.HolidayModel.DateSelected,
                        model.HolidayModel.Description,
                        model.HolidayModel.IsFullDay,
                        model.HolidayModel.ID,
                    },
                    Status = 1,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                TypeSubmit = typesubmit,
                Holiday = new
                {
                    model.HolidayModel.Year,
                    model.HolidayModel.Month,
                    model.HolidayModel.Day,
                    model.HolidayModel.DateSelected,
                    model.HolidayModel.Description,
                    model.HolidayModel.IsFullDay,
                    model.HolidayModel.ID,
                },
                Status = 0,
            }, JsonRequestBehavior.AllowGet);
        }
        [AjaxOnly]
        public ActionResult GetFullDates()
        {
            var holidays = _repo.GetAllHolidays(DateTime.Now.Year);
            return Json(holidays, JsonRequestBehavior.AllowGet);
        }
        [AjaxOnly]
        public ActionResult GetDatesByMonth(int id)
        {
            var holidays = _repo.GetAllHolidays(DateTime.Now.Year)
                                .Where(m => m.Date.Month == id)
                                .Select(m => new
                                             {
                                                 m.Date.Day,
                                                 m.Date.Month,
                                                 m.Description,
                                                 m.IsFullDay
                                             });
            return Json(holidays, JsonRequestBehavior.AllowGet);
        }
    }
}

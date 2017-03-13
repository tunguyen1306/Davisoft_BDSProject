using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Infrastructure.Helpers;
using ServiceStack.Text;

namespace Davisoft_BDSProject.Web.Controllers
{
    public class AuditController : Controller
    {
        private readonly IAuditTracker _tracker;

        public AuditController(IAuditTracker tracker)
        {
            _tracker = tracker;
        }

        [Infrastructure.Utility.ApplyAuthorize(true)]
        public ActionResult Index(string Username = null)
        {
            IEnumerable<Audit> records;
            //if (!string.IsNullOrEmpty(Request["DisplayName"]))
            //{
            //    var username = Request["DisplayName"];
            //    records = _tracker.GetAuditRecords(username);
            //}
            //else
            //{
            records = _tracker.GetAuditRecords().DistinctBy(m => m.Username);
            ViewBag.Username = Username;
            //}
            //records = _tracker.GetAuditRecords();
            //records = _tracker.GetAuditRecords(m => m.TimeAccessed.Month == UserDateTime.Today.Month && m.TimeAccessed.Year == UserDateTime.Today.Year);
            return View(records);
        }

        public ActionResult Detail(int id)
        {
            Audit record = _tracker.GetRecord(id);
            return View(record);
        }
        public ActionResult Display(string model, string id)
        {
            IEnumerable<Audit> audits = _tracker.GetManualAuditRecord(model, Convert.ToInt32(id));
            return View(audits);
        }

        [AjaxOnly]
        public ActionResult AjaxGetAudits()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            // ReSharper disable once AssignNullToNotNullAttribute
            var start = Request.Form.GetValues("start").FirstOrDefault();
            // ReSharper disable once AssignNullToNotNullAttribute
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            // ReSharper disable once AssignNullToNotNullAttribute
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = "";
            // ReSharper disable once AssignNullToNotNullAttribute
            if (Request.Form.GetValues("search[value]").FirstOrDefault() != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                searchValue = Request.Form.GetValues("search[value]").FirstOrDefault().Trim().ToLower();
            }


            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            IEnumerable<Audit> audits = new HashSet<Audit>();
            if (!string.IsNullOrEmpty(searchValue))
            {
                if (!string.IsNullOrEmpty(searchValue))
                    audits = _tracker.GetAuditRecords(m =>
                        m.Username.ToLower().Contains(searchValue) ||
                        m.Message.ToLower().Contains(searchValue) ||
                        m.UrlAccessed.ToLower().Contains(searchValue) ||
                        m.TimeAccessed.ToString("dd/MM/yyyy HH:mm:ss").Contains(searchValue));
            }
            else
            {
                audits = _tracker.GetAuditRecords();
            }
            // ReSharper disable once AssignNullToNotNullAttribute
            if (Request.Form.GetValues("selectUser").FirstOrDefault() != null)
            {
                var selectUser = "";
                // ReSharper disable once PossibleNullReferenceException
                selectUser = Request.Form.GetValues("selectUser").FirstOrDefault().Trim().ToLower();
                if (!string.IsNullOrEmpty(selectUser))
                    audits = audits.Where(m => m.Username.ToLower().Trim() == selectUser);
            }
            // ReSharper disable once AssignNullToNotNullAttribute
            if (Request.Form.GetValues("datefrom").FirstOrDefault() != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                var dateFrom = Request.Form.GetValues("datefrom").FirstOrDefault().Trim().ToLower();
                if (!string.IsNullOrEmpty(dateFrom))
                    audits = audits.Where(m => Convert.ToDateTime(dateFrom) <= m.TimeAccessed.Date);
            }
            // ReSharper disable once AssignNullToNotNullAttribute
            if (Request.Form.GetValues("dateto").FirstOrDefault() != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                var dateto = Request.Form.GetValues("dateto").FirstOrDefault().Trim().ToLower();
                if (!string.IsNullOrEmpty(dateto))
                    audits = audits.Where(m => m.TimeAccessed.Date <= Convert.ToDateTime(dateto));
            }


            if (sortColumnDir == "asc")
            {
                switch (sortColumn)
                {
                    case "TimeAccessed":
                        audits = audits.OrderBy(s => s.TimeAccessed).ThenBy(s => s.Username);
                        break;
                    case "Message":
                        audits = audits.OrderBy(s => s.Message).ThenBy(s => s.Username);
                        break;
                    case "UrlAccessed":
                        audits = audits.OrderBy(s => s.UrlAccessed).ThenBy(s => s.Username);
                        break;
                    default:
                        audits = audits.OrderBy(s => s.Username);
                        break;
                }
            }
            else
            {
                switch (sortColumn)
                {
                    case "TimeAccessed":
                        audits = audits.OrderByDescending(s => s.TimeAccessed).ThenBy(s => s.Username);
                        break;
                    case "Message":
                        audits = audits.OrderByDescending(s => s.Message).ThenBy(s => s.Username);
                        break;
                    case "UrlAccessed":
                        audits = audits.OrderByDescending(s => s.UrlAccessed).ThenBy(s => s.Username);
                        break;
                    default:
                        audits = audits.OrderByDescending(s => s.Username);
                        break;
                }
            }


            recordsTotal = audits.Count();
            if (pageSize > recordsTotal)
            {
                pageSize = recordsTotal;
            }
            var data = audits.Skip(skip).Take(pageSize).ToList();

            var json = new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data.Select(m => new
                {
                    m.ID,
                    m.Username,
                    TimeAccessed = m.TimeAccessed.ToString("dd/MM/yyyy HH:mm:ss"),
                    m.Message,
                    m.UrlAccessed,
                })
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }


    public class AuditObject
    {
        public IEnumerable<Audit> Audits { get; set; }
        public IEnumerable<string> UserName { get; set; }
    }
}

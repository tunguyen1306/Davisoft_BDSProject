using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Routing;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Helpers;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class Log
    {
        #region Repository

        private static IAuditTracker AuditTracker
        {
            get { return DependencyHelper.GetService<IAuditTracker>(); }
        }

        #endregion

        public static void Info(string message = "")
        {
            RouteData routeData = HttpContext.Current.Request.RequestContext.RouteData;
            string controllerName = routeData.GetRequiredString("controller");
            string action = routeData.GetRequiredString("action");
            //int actionId;
            //if (routeData.Values["id"] != null && int.TryParse(routeData.Values["id"].ToString(), out actionId))
            //    Info(message, controllerName, actionId);
            if ((controllerName == "Booking" && action == "AddedConfirm") || 
                (HttpContext.Current.Request.HttpMethod == "POST" &&
                controllerName != "Account" &&
                controllerName != "Report" &&
                !action.ToLower().Contains("ajax") &&
                !action.ToLower().Contains("get") &&
                (!HttpContext.Current.Request.IsAjaxRequest() ||
                action.ToLower().Contains("create") ||
                action.ToLower().Contains("add") ||
                action.ToLower().Contains("edit") ||
                action.ToLower().Contains("modify") ||
                action.ToLower().Contains("delete") ||
                action.ToLower().Contains("submit") ||
                action.ToLower().Contains("approve") ||
                action.ToLower().Contains("approval") ||
                action.ToLower().Contains("verify") ||
                action.ToLower().Contains("reject"))))
            {
                Info(message, controllerName);
            }
        }

        public static void Info(string message, int actionId)
        {
            RouteData routeData = HttpContext.Current.Request.RequestContext.RouteData;
            string controllerName = routeData.GetRequiredString("controller");

            Info(message, controllerName, actionId);
        }

        public static void Info(string message, string controllerName, int actionId = 0)
        {
            HttpRequest request = HttpContext.Current.Request;

            string username = "";
            string email = "";
            if (CurrentUser.Identity != null)
            {
                username = CurrentUser.Identity.DisplayName;
                email = CurrentUser.Identity.Email;
            }

            string[] keys = request.Form.AllKeys;
            List<string> dataForm = keys.Select(t => t + ": " + request.Form[t]).ToList();
            var record = new Audit
                         {
                             Username = username,
                             Email = email,
                             IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                             UrlAccessed = request.RawUrl,
                             TimeAccessed = DateTime.Now,
                             SessionId = HttpContext.Current.Session.SessionID,
                             Data = Json.Encode(new { request.Cookies, request.Headers, request.Files, dataForm, request.QueryString }),
                             Message = message,
                             Type = (int)AuditType.Manual,
                             Controller = controllerName,
                             ActionId = actionId
                         };

            AuditTracker.CreateRecord(record);
        }
        //public static void Debug(bool flag, string message)
        //{
        //    if (flag)
        //        Debug(message);
        //}

        //public static void Debug(string message)
        //{
        //    string fileName = HttpContext.Current.Server.MapPath("~/debug.txt");
        //    File.AppendAllText(fileName, "\n---------------------\n" + DateTime.Now + "\n---------------------\n" + message);
        //}
    }
}

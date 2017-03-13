using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using FluentValidation.Mvc;
using FluentValidation.Validators;
using ServiceStack.Text;
using Davisoft_BDSProject.Web.Infrastructure;
using Davisoft_BDSProject.Web.Infrastructure.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using NS.Mail;
using StackExchange.Profiling;

namespace Davisoft_BDSProject.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //RouteTable.Routes.MapHubs();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterFilterProviders(FilterProviders.Providers);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BinderConfig.RegisterBinder(ModelBinders.Binders);
            MapperConfig.RegisterMappers();

            //FluentValidationModelValidatorProvider.Configure(x => x.Add(typeof(GreaterThanValidator), (metadata, context, rule, validator) => new GreaterThanValidatorEx(metadata, context, rule, validator)));
            FluentValidationModelValidatorProvider.Configure();
            //JsConfig.DateHandler = DateHandler.ISO8601;

            // NS.Framework configuration
            MailConfig.DefaultCssCompiler = html => PreMailer.Net.PreMailer.MoveCssInline(html, true, "#ignore").Html;
            MailConfig.DefaultRazorTemplateLocation = HttpContext.Current.Server.MapPath("~/Content/EmailTemplates");

            ////ScheduleHelper.AlertNoPayment("Subaru_AlertNoPayment", "Subaru_AlertNoPayment");
            //ScheduleHelper.BookingPaymentReminder("Subaru_BookingPaymentReminder", "Subaru_BookingPaymentReminder_1");
            //ScheduleHelper.BookingDailyReport("Subaru_BookingDailyReport", "Subaru_BookingDailyReport_1");
            //ScheduleHelper.AllocationReminder("Subaru_AllocationReminder", "Subaru_AllocationReminder");
            //ScheduleHelper.SetPriceScheduleTrigger("UpdatePriceSchedule", "UpdatePriceSchedule");
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            MiniProfilerEF.Initialize();
        }

        protected void Session_Start()
        {
            // http://stackoverflow.com/questions/2874078/asp-net-session-sessionid-changes-between-requests
            Session["init"] = 0;

            var ignored = MiniProfiler.Settings.IgnoredPaths.ToList();
            ignored.Add("signalr");
            MiniProfiler.Settings.IgnoredPaths = ignored.ToArray();
        }

        protected void Session_End()
        {
//            Session["DbName"] = null;

            //if (Request.Cookies["DatabaseName"] != null)
            //{
            //    var cookie = Request.Cookies["DatabaseName"];
            //    cookie.Value = null;
            //    Response.Cookies.Set(cookie);
            //}
            CurrentUser.Logout();
        }

        protected void Application_BeginRequest()
        {
            try
            {
                const string sessionParamName = "ASPSESSID";
                const string sessionCookieName = "ASP.NET_SessionId";

                if (HttpContext.Current.Request.Form[sessionParamName] != null)
                    UpdateCookie(sessionCookieName, HttpContext.Current.Request.Form[sessionParamName]);
                else if (HttpContext.Current.Request.QueryString[sessionParamName] != null)
                    UpdateCookie(sessionCookieName, HttpContext.Current.Request.QueryString[sessionParamName]);

                const string authParamName = "AUTHID";
                string authCookieName = FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[authParamName] != null)
                    UpdateCookie(authCookieName, HttpContext.Current.Request.Form[authParamName]);
                else if (HttpContext.Current.Request.QueryString[authParamName] != null)
                    UpdateCookie(authCookieName, HttpContext.Current.Request.QueryString[authParamName]);
            }
            catch
            {
            }

            //if (Request.IsLocal)
            //    MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        private void UpdateCookie(string cookieName, string cookieValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookieName) ?? new HttpCookie(cookieName);
            cookie.Value = cookieValue;
            HttpContext.Current.Request.Cookies.Set(cookie);
        }
    }
}

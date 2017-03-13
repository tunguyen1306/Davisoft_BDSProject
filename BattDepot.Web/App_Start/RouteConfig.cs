using System.Web.Mvc;
using System.Web.Routing;

namespace Davisoft_BDSProject.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Account", action = "Login" });

            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Account", action = "Logout" });

            routes.MapRoute(
                name: "BypassAuth",
                url: "n/{token}",
                defaults: new { controller = "Account", action = "BypassLogin" });
            routes.MapRoute(
                name: "ProcessAQueue",
                url: "ProcessAQueue",
                defaults: new { controller = "Api", action = "ProcessAQueue" });
            routes.MapRoute(
                name: "UpdateDpoExpiry",
                url: "UpdateDpoExpiry",
                defaults: new { controller = "Api", action = "UpdateDpoExpiry" });
            routes.MapRoute(
                name: "UpdateAllocation1",
                url: "UpdateAllocation",
                defaults: new { controller = "Api", action = "UpdateAllocation" });
            routes.MapRoute(
                name: "UpdateAllocation2",
                url: "Allocation/UpdateAllocation",
                defaults: new { controller = "Api", action = "UpdateAllocation" });
            routes.MapRoute(
                name: "UpdateModel",
                url: "UpdateModel",
                defaults: new { controller = "Api", action = "UpdateModel" });
            routes.MapRoute(
                "/",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Davisoft_BDSProject.Web.Controllers" });
        }
    }
}

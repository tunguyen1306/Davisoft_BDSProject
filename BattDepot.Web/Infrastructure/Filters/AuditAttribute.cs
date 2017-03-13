using System.Web.Mvc;
using Davisoft_BDSProject.Web.Infrastructure.Utility;

namespace Davisoft_BDSProject.Web.Infrastructure.Filters
{
    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log.Info();
            base.OnActionExecuting(filterContext);
        }
    }
}

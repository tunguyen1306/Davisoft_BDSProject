using System;
using System.Web.Mvc;

namespace Davisoft_BDSProject.Web.Infrastructure.Filters
{
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        #region Public members

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            // for the God sake
            return;

            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                filterContext.Result = new HttpNotFoundResult();
        }

        #endregion
    }
}

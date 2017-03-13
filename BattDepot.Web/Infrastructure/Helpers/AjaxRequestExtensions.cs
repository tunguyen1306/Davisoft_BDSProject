using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public static class AjaxRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (request["X-Requested-With"] == "XMLHttpRequest")
                return true;
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
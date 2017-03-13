using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Davisoft_BDSProject.Web.Infrastructure.Filters;

namespace Davisoft_BDSProject.Web.Controllers
{
    [ExcludeFilters(typeof(RequestAuthorizeAttribute))]
    public class ErrorsController : Controller
    {
        //
        // GET: /Errors/
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult Error500()
        {
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBDS_Project.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        public ActionResult RegisterCompany()
        {
            return View();
        }
        public ActionResult RegisterEmployee()
        {
            return View();
        }

    }
}

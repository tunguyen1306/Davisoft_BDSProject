using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBDS_Project.Controllers
{
    public class ManagementController : Controller
    {
        //
        // GET: /Management/

      
        public ActionResult ManagementCompany()
        {
            return View();
        }
        public ActionResult ManagementPersonal()
        {
            return View();
        }

    }
}

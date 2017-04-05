using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBDS_Project.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Blank()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            return View();
        }

    }
}

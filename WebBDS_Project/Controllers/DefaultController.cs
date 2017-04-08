using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBDS_Project.Models;

namespace WebBDS_Project.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
        davisoft_bdsprojectEntities db = new davisoft_bdsprojectEntities();
        public ActionResult Index()
        {
           
           
            return View();
          
        }
        public ActionResult Blank()
        {
            return View();
        }
        public ActionResult Detail(string  id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var Model = new NewsModel
            {
                tblbdsnew = db.bdsnews.Where(x=>x.Id== id_).FirstOrDefault(),
                ListPicture=db.bdspictures.Where(x=>x.advert_id==id_).ToList()

            };

            return View();
        }

    }
}

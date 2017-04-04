using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBDS_Project.Models;

namespace WebBDS_Project.Controllers
{
    public class AdvertsController : Controller
    {
        davisoft_bdsprojectEntities db = new davisoft_bdsprojectEntities();
        //
        // GET: /Adverts/

        public ActionResult AdvertCompany()
        {
             
            var dataCity = from data in db.states
                           join datatext in db.statetexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            var registerModel = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListMarriea = db.bdsmarriages.ToList(),
                ListSalary = db.bdssalaries.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListBdscareer = db.bdscareers.ToList(),
                ListTimework = db.bdstimeworks.ToList(),
                Listbdslanguage = db.bdslanguages.ToList(),
                Listbdsnewstype = db.bdsnewstypes.OrderBy(x=>x.Order).ToList(),
                ListGeoModel= dataCity.ToList()

            };
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult AdvertCompany(RegisterInformationModel create)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != create.tblCaptCha.Captcha)
            {
                ModelState.AddModelError("Captcha", "Wrong value of sum, please try again.");
                return RedirectToAction("AdvertCompany", "Adverts");
            }
            else
            {
                create.tblbdsnew.FromDeadline = DateTime.Now;
                create.tblbdsnew.FromCreateNews = DateTime.Now;
                db.bdsnews.Add(create.tblbdsnew);
                db.SaveChanges();
                return RedirectToAction("Index", "Default");
            }
            return null;
        }
         
  
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int) DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image) bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                  for (i = 1; i < 10; i++) 
                    { 
                        pen.Color = Color.FromArgb( 
                        (rand.Next(0, 255)), 
                        (rand.Next(0, 255)), 
                        (rand.Next(0, 255))); 
 
                        r = rand.Next(0, (130 / 3)); 
                        x = rand.Next(0, 130); 
                        y = rand.Next(0, 30); 
 
                        gfx.DrawEllipse(pen,new Rectangle()); 
                    } 
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.ForestGreen, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }

    }
}

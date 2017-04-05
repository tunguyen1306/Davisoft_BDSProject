using ImageResizer;
using System;
using System.Collections.Generic;
using System.Data;
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
            CaptCha cap = new CaptCha();
            bdsnew bdsNew = new bdsnew();
            var registerModel = new RegisterInformationModel
            {
                ListBdsScopes = db.bdsscopes.ToList(),
                ListMarriea = db.bdsmarriages.ToList(),
                ListSalary = db.bdssalaries.ToList(),
                ListDucation = db.bdseducations.ToList(),
                ListBdscareer = db.bdscareers.ToList(),
                ListTimework = db.bdstimeworks.ToList(),
                Listbdslanguage = db.bdslanguages.ToList(),
                Listbdsnewstype = db.bdsnewstypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,
                tblbdsnew= bdsNew


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


               var tblpict = db.bdspictures.Where(x => x.advert_id == 99999999);
                foreach (var item in tblpict)
                {

                    bdspicture tblpic = db.bdspictures.Find(item.id);
                    tblpic.advert_id = create.tblbdsnew.Id;
                    db.Entry(tblpic).State = EntityState.Modified;
                   
                }
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
        [HttpPost]
        public void SaveImg(NewsPicture newPicture)
        {
            var t = newPicture.cfile == null ? "" : newPicture.cfile;
            var file = t.Replace("data:image/png;base64,", "");
            var photoBytes = Convert.FromBase64String(file);
            string format = "jpg";
            if (newPicture.isactive == 1)
            {
                newPicture.isactive = 1;
            }
            else
            {
                newPicture.isactive = 2;
            }
            var picture = new NewsPicture
            {
                tblPicture = new bdspicture { advert_id = newPicture.idProducts, angleType = 0, cms_sql_id = 0, converted = DateTime.Now, tocheck = true, type_id = 1, title = newPicture.nameImg, position = newPicture.isactive }
            };
            if (newPicture.idpicture == 0)
            {
                var settings = new ResizeSettings();
                settings.Scale = ScaleMode.DownscaleOnly;
                settings.Format = format;

                //string uploadFolder = picture.DirectoryPhysical;

                string path = Server.MapPath("~/fileUpload/") + DateTime.Now.Day + DateTime.Now.Month + "/";
                // check for directory
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // filename with placeholder for size
                if (picture.GetConvertedFileName() == null || string.IsNullOrWhiteSpace(picture.GetConvertedFileName()))
                    picture.SetFileName(DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + "_" + picture.CreateFilename() + "_{0}." + format);

                if (picture.GetFilePathPhysical(NewsPicture.PictureSize.Large) != null)
                {
                    string dest = path + picture.FileName(NewsPicture.PictureSize.Large);
                    settings.MaxWidth = 720;
                    settings.MaxHeight = 480;
                    if (picture.WaterMarkLarge == NewsPicture.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPicture.PictureSize.Large);
                }

                if (picture.GetFilePathPhysical(NewsPicture.PictureSize.Medium) != null)
                {
                    string dest = path + picture.FileName(NewsPicture.PictureSize.Medium);
                    settings.MaxWidth = 288;
                    settings.MaxHeight = 192;
                    if (picture.WaterMarkLarge == NewsPicture.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPicture.PictureSize.Medium);
                }
                if (picture.GetFilePathPhysical(NewsPicture.PictureSize.Tiny) != null)
                {
                    string dest = path + picture.FileName(NewsPicture.PictureSize.Tiny);
                    settings.MaxWidth = 1500;
                    settings.MaxHeight = 600;
                    if (picture.WaterMarkLarge == NewsPicture.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPicture.PictureSize.Tiny);
                }

                if (picture.GetFilePathPhysical(NewsPicture.PictureSize.Small) != null)
                {
                    string dest = path + picture.FileName(NewsPicture.PictureSize.Small);
                    settings.MaxWidth = 600;
                    settings.MaxHeight = 300;
                    if (picture.WaterMarkLarge == NewsPicture.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPicture.PictureSize.Small);
                }
                db.bdspictures.Add(picture.tblPicture);
                db.SaveChanges();
            }
            if (newPicture.idpicture > 0)
            {

               bdspicture tblpict = db.bdspictures.Find(newPicture.idpicture);
                tblpict.title = newPicture.nameImg;
                tblpict.position = newPicture.isactive;
                db.Entry(tblpict).State = EntityState.Modified;
                db.SaveChanges();
                
            }




        }
    }
}

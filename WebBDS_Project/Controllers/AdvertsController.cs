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
            if (Session["IdUser"] ==null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
           
 
            var dataCity = (from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();
            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố"});
          var ListSalary=  db.BDSSalaries.ToList();
         
            CaptCha cap = new CaptCha();
            BDSNew BDSNew = new BDSNew();
            var registerModel = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.ToList(),
                ListMarriea = db.BDSMarriages.ToList(),
                ListSalary = ListSalary,
                ListDucation = db.BDSEducations.ToList(),
                ListBDSCareer = db.BDSCareers.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,
                tblBDSNew= BDSNew,
                ListBDSEmployerInformation = db.BDSEmployerInformations.ToList(),
                ListBdsAdcount = db.BDSAccounts.ToList(),
                ListBDSEmper = db.BDSEmpers.ToList()



            };
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult CheckCapcha(RegisterInformationModel create)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != create.tblCaptCha.Captcha)
            {
                return Json(new { error = 0 });
            }
            else
            {
                return Json(new { error = 1 });
            }
            return null;
        }
        [HttpPost]
        public ActionResult AdvertCompany(RegisterInformationModel create)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != create.tblCaptCha.Captcha)
            {
                ModelState.AddModelError("Captcha", "Wrong value of sum, please try again.");
                var dataCity = from data in db.States
                               join datatext in db.StateTexts on data.name_id equals datatext.id
                               where datatext.language_id == "vi"
                               select new GeoModel { CityId = data.state_id, CityName = datatext.text };
                CaptCha cap = new CaptCha();
                BDSNew BDSNew = new BDSNew();
                var registerModel = new RegisterInformationModel
                {
                    ListBDSScopes = db.BDSScopes.ToList(),
                    ListMarriea = db.BDSMarriages.ToList(),
                    ListSalary = db.BDSSalaries.ToList(),
                    ListDucation = db.BDSEducations.ToList(),
                    ListBDSCareer = db.BDSCareers.ToList(),
                    ListTimework = db.BDSTimeWorks.ToList(),
                    ListBDSLanguage = db.BDSLanguages.ToList(),
                    ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                    ListGeoModel = dataCity.ToList(),
                    tblCaptCha = cap,
                    tblBDSNew = BDSNew,
                    ListBDSEmployerInformation = db.BDSEmployerInformations.ToList(),
                    ListBdsAdcount = db.BDSAccounts.ToList(),
                    ListBDSEmper = db.BDSEmpers.ToList()

                };
                var listPicture = db.BDSPictures.Where(x => x.advert_id == 99999999);
                foreach (var item in listPicture)
                {
                    var pic = db.BDSPictures.Find(item.ID);
                    db.BDSPictures.Remove(pic);
                }
                
                db.SaveChanges();
                return View(registerModel);
            }
            else
            {
                create.tblBDSNew.IdAcount=int.Parse( Session["IdUser"].ToString());
                create.tblBDSNew.FromDeadline = DateTime.Now;
                create.tblBDSNew.FromCreateNews = DateTime.Now;
                create.tblBDSNew.CreateDate = DateTime.Now;
                create.tblBDSNew.Active = 1;
                create.tblBDSNew.CreateUser = 1;
                db.BDSNews.Add(create.tblBDSNew);
                db.SaveChanges();


               var tblpict = db.BDSPictures.Where(x => x.advert_id == 99999999);
                foreach (var item in tblpict)
                {

                    BDSPicture tblpic = db.BDSPictures.Find(item.ID);
                    tblpic.advert_id = create.tblBDSNew.ID;
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
            int a = rand.Next(10, 50);
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
            var t = newPicture.cfile ?? "";
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
                tblPicture = new BDSPicture { advert_id = newPicture.idProducts, angleType = 0, cms_sql_id = 0, converted = DateTime.Now, tocheck =true, type_id = 1, title = newPicture.nameImg, position = newPicture.isactive }
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
                    settings.MaxHeight = 405;
                    if (picture.WaterMarkLarge == NewsPicture.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPicture.PictureSize.Large);
                }

                if (picture.GetFilePathPhysical(NewsPicture.PictureSize.Medium) != null)
                {
                    string dest = path + picture.FileName(NewsPicture.PictureSize.Medium);
                    settings.MaxWidth = 320;
                    settings.MaxHeight = 180;
                    if (picture.WaterMarkLarge == NewsPicture.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPicture.PictureSize.Medium);
                }
                if (picture.GetFilePathPhysical(NewsPicture.PictureSize.Tiny) != null)
                {
                    string dest = path + picture.FileName(NewsPicture.PictureSize.Tiny);
                    settings.MaxWidth = 220;
                    settings.MaxHeight = 123;
                    if (picture.WaterMarkLarge == NewsPicture.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPicture.PictureSize.Tiny);
                }

                if (picture.GetFilePathPhysical(NewsPicture.PictureSize.Small) != null)
                {
                    string dest = path + picture.FileName(NewsPicture.PictureSize.Small);
                    settings.MaxWidth = 120;
                    settings.MaxHeight = 67;
                    if (picture.WaterMarkLarge == NewsPicture.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.tblPicture.originalFilepath))
                        picture.tblPicture.originalFilepath = picture.GetFilePath(NewsPicture.PictureSize.Small);
                }
                db.BDSPictures.Add(picture.tblPicture);
                db.SaveChanges();
            }
            if (newPicture.idpicture > 0)
            {

               BDSPicture tblpict = db.BDSPictures.Find(newPicture.idpicture);
                tblpict.title = newPicture.nameImg;
                tblpict.position = newPicture.isactive;
                db.Entry(tblpict).State = EntityState.Modified;
                db.SaveChanges();
                
            }




        }
        public ActionResult EditNews(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            CaptCha cap = new CaptCha();
            BDSNew BDSNew = new BDSNew();
            var registerModel = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.ToList(),
                ListMarriea = db.BDSMarriages.ToList(),
                ListSalary = db.BDSSalaries.ToList(),
                ListDucation = db.BDSEducations.ToList(),
                ListBDSCareer = db.BDSCareers.ToList(),
                ListTimework = db.BDSTimeWorks.ToList(),
                ListBDSLanguage = db.BDSLanguages.ToList(),
                ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,
                tblBDSNew = db.BDSNews.FirstOrDefault(x=>x.ID==id_),
                ListBDSEmployerInformation = db.BDSEmployerInformations.ToList(),
                ListBdsAdcount = db.BDSAccounts.ToList(),
                ListBDSEmper = db.BDSEmpers.ToList(),
                ListBDSPicture=db.BDSPictures.Where(x=>x.advert_id== id_).ToList()


            };
            return View(registerModel);
        }
        [HttpPost,ActionName("EditNews")]
        public ActionResult EditNews(RegisterInformationModel create)
        {

            if (Session["Captcha"] == null || Session["Captcha"].ToString() != create.tblCaptCha.Captcha)
            {
                ModelState.AddModelError("Captcha", "Wrong value of sum, please try again.");
                var dataCity = from data in db.States
                               join datatext in db.StateTexts on data.name_id equals datatext.id
                               where datatext.language_id == "vi"
                               select new GeoModel { CityId = data.state_id, CityName = datatext.text };
                CaptCha cap = new CaptCha();
                BDSNew BDSNew = new BDSNew();
                var registerModel = new RegisterInformationModel
                {
                    ListBDSScopes = db.BDSScopes.ToList(),
                    ListMarriea = db.BDSMarriages.ToList(),
                    ListSalary = db.BDSSalaries.ToList(),
                    ListDucation = db.BDSEducations.ToList(),
                    ListBDSCareer = db.BDSCareers.ToList(),
                    ListTimework = db.BDSTimeWorks.ToList(),
                    ListBDSLanguage = db.BDSLanguages.ToList(),
                    ListBDSNewsType = db.BDSNewsTypes.OrderBy(x => x.Order).ToList(),
                    ListGeoModel = dataCity.ToList(),
                    tblCaptCha = cap,
                    tblBDSNew = db.BDSNews.FirstOrDefault(x => x.ID == create.tblBDSNew.ID),
                    ListBDSEmployerInformation = db.BDSEmployerInformations.ToList(),
                    ListBdsAdcount = db.BDSAccounts.ToList(),
                    ListBDSEmper = db.BDSEmpers.ToList(),
                    ListBDSPicture = db.BDSPictures.Where(x => x.advert_id == create.tblBDSNew.ID).ToList()


                };
                var listPicture = db.BDSPictures.Where(x => x.advert_id == 99999999);
                foreach (var item in listPicture)
                {
                    var pic = db.BDSPictures.Find(item.ID);
                    db.BDSPictures.Remove(pic);
                }

                db.SaveChanges();
                return View(registerModel);
            }
            else
            {
                BDSNew tblNews = db.BDSNews.Find(create.tblBDSNew.ID);
                tblNews.Title = create.tblBDSNew.Title;
                tblNews.AddressWork = create.tblBDSNew.AddressWork;
                tblNews.FromSalary = create.tblBDSNew.FromSalary;
                tblNews.ToSalary = create.tblBDSNew.ToSalary;
                tblNews.Quantity = create.tblBDSNew.Quantity;
                tblNews.Bonus = create.tblBDSNew.Bonus;
                tblNews.Sex = create.tblBDSNew.Sex;
                tblNews.IdTimeWork = create.tblBDSNew.IdTimeWork;
                tblNews.IdEducation = create.tblBDSNew.IdEducation;
                tblNews.Career = create.tblBDSNew.Career;
                tblNews.DesCompany = create.tblBDSNew.DesCompany;
                tblNews.DesWork = create.tblBDSNew.DesWork;
                tblNews.Benefit = create.tblBDSNew.Benefit;
                tblNews.FromAge = create.tblBDSNew.FromAge;
                tblNews.ToAge = create.tblBDSNew.ToAge;
                tblNews.TimeProbationary = create.tblBDSNew.TimeProbationary;
                tblNews.NameCompany = create.tblBDSNew.NameCompany;
                tblNews.AddressApply = create.tblBDSNew.AddressApply;
                tblNews.NamesContact = create.tblBDSNew.NamesContact;
                tblNews.PhoneContact = create.tblBDSNew.PhoneContact;
                tblNews.Email = create.tblBDSNew.Email;
                tblNews.FromDeadline = create.tblBDSNew.FromDeadline;
                tblNews.ToDeadline = create.tblBDSNew.ToDeadline;
                tblNews.IdLanguage = create.tblBDSNew.IdLanguage;
                tblNews.WebSiteCompany = create.tblBDSNew.WebSiteCompany;
                tblNews.IdTypeNews = create.tblBDSNew.IdTypeNews;
                tblNews.FromCreateNews = create.tblBDSNew.FromCreateNews;
                tblNews.ToCreateNews = create.tblBDSNew.ToCreateNews;
                tblNews.IdAcount = create.tblBDSNew.IdAcount;
                tblNews.KeySearch = create.tblBDSNew.KeySearch;
                tblNews.MoneyInDay = create.tblBDSNew.MoneyInDay;
                tblNews.TotalMoney = create.tblBDSNew.TotalMoney;
                tblNews.UrlImage = create.tblBDSNew.UrlImage;
                tblNews.IdTypeNewsCuurent = create.tblBDSNew.IdTypeNewsCuurent;
                tblNews.DateReup = create.tblBDSNew.DateReup;
                tblNews.CountReup = create.tblBDSNew.CountReup;
                tblNews.Status = create.tblBDSNew.Status;
                tblNews.RefTranHis = create.tblBDSNew.RefTranHis;
                tblNews.Active = create.tblBDSNew.Active;
                tblNews.CreateDate = create.tblBDSNew.CreateDate;
                tblNews.CreateUser = create.tblBDSNew.CreateUser;
                tblNews.ModifiedDate = create.tblBDSNew.ModifiedDate;
                tblNews.ModifiedUser = create.tblBDSNew.ModifiedUser;
                tblNews.MaxReup = create.tblBDSNew.MaxReup;
                 db.Entry(tblNews).State = EntityState.Modified;
                db.SaveChanges();

                var tblpict = db.BDSPictures.Where(x => x.advert_id == 99999999);
                foreach (var item in tblpict)
                {

                    BDSPicture tblpic = db.BDSPictures.Find(item.ID);
                    tblpic.advert_id = create.tblBDSNew.ID;
                    db.Entry(tblpic).State = EntityState.Modified;

                }
                db.SaveChanges();
                return RedirectToAction("Index", "Default");
            }
            return null;
        }


        [ActionName("LoadCarre")]
        public ActionResult LoadCarre(int id)
        {
            var data = db.BDSNews.FirstOrDefault(x=>x.ID== id).Career;
            
            return Json(new { idData = data });
        }
        [ActionName("LoadTypeNew")]
        public ActionResult LoadTypeNew(int id)
        {
            var data = db.BDSNews.FirstOrDefault(x => x.ID == id).IdTypeNews;

            return Json(new { idData = data });
        }
        [HttpPost]
        public ActionResult DeleteImg(int idpicture)
        {
            BDSPicture tblPic = db.BDSPictures.Find(idpicture);
            NewsPicture proPic = new NewsPicture();
            db.BDSPictures.Remove(tblPic);
            db.SaveChanges();
            DeleteIMG(proPic.tblPicture.originalFilepath);
            return View(tblPic);
        }
        public void DeleteIMG(string picture)
        {
            NewsPicture vgp = new NewsPicture();
            if (picture == null)
                return;
            var fo = picture.Substring(0, 3);
            string dir = Server.MapPath("~/fileUpload/" + fo + "/" + picture);

            System.IO.File.Delete(dir);



        }
    }
}

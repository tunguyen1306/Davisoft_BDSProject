using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
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
    public class RegisterController : Controller
    {
        davisoft_bdsprojectEntities db = new davisoft_bdsprojectEntities();
        //
        // GET: /Register/

        public ActionResult RegisterCompany()
        {
            CaptCha cap = new CaptCha();
            bdsnew bdsNew = new bdsnew();
            var dataCity = from data in db.states
                           join datatext in db.stateTexts on data.name_id equals datatext.id
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
                Listbdsnewstype = db.bdsnewstypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,
                tblbdsnew = bdsNew,
                ListBdsemployerinformation = db.bdsemployerinformations.ToList(),
                Listbdspersonalinformation = db.bdspersonalinformations.ToList(),
                ListBdsAdcount = db.bdsaccounts.ToList(),
                Listbdsemper = db.bdsempers.ToList()
            };
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult RegisterCompany(RegisterInformationModel bdsInformationModel)
        {
            try
            {
                if (Session["Captcha"] == null || Session["Captcha"].ToString() != bdsInformationModel.tblCaptCha.Captcha)
                {
                    CaptCha cap = new CaptCha();
                    bdsnew bdsNew = new bdsnew();
                    var dataCity = from data in db.states
                                   join datatext in db.stateTexts on data.name_id equals datatext.id
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
                        Listbdsnewstype = db.bdsnewstypes.OrderBy(x => x.Order).ToList(),
                        ListGeoModel = dataCity.ToList(),
                        tblCaptCha = cap,
                        tblbdsnew = bdsNew,
                        ListBdsemployerinformation = db.bdsemployerinformations.ToList(),
                        Listbdspersonalinformation = db.bdspersonalinformations.ToList(),
                        ListBdsAdcount = db.bdsaccounts.ToList(),
                        Listbdsemper = db.bdsempers.ToList()
                    };
                    return View(registerModel);
                }
                else
                {
                    bdsInformationModel.TblBdsAdcount.CreateDate = DateTime.Now;
                    bdsInformationModel.TblBdsAdcount.ModifiedDate = DateTime.Now;
                    db.bdsaccounts.Add(bdsInformationModel.TblBdsAdcount);
                    db.SaveChanges();
                    bdsemployerinformation tblemployee = new bdsemployerinformation();
                    bdsInformationModel.TblBdsemployerinformation.IdAccount = bdsInformationModel.TblBdsAdcount.Id;

                    bdsInformationModel.TblBdsemployerinformation.Active = 1;
                    bdsInformationModel.TblBdsemployerinformation.CreateDate = DateTime.Now;
                    bdsInformationModel.TblBdsemployerinformation.CreateUser = 1;
                    bdsInformationModel.TblBdsemployerinformation.ModifiedDate = DateTime.Now;
                    bdsInformationModel.TblBdsemployerinformation.ModifiedUser = 1;
                    bdsInformationModel.TblBdsemployerinformation.Featured = 1;
                    bdsInformationModel.TblBdsemployerinformation.PhoneContact = bdsInformationModel.TblBdsemployerinformation.Phone;
                    bdsInformationModel.TblBdsemployerinformation.DistrictContact = bdsInformationModel.TblBdsemployerinformation.District;
                    bdsInformationModel.TblBdsemployerinformation.City = bdsInformationModel.TblBdsemployerinformation.City;
                    bdsInformationModel.TblBdsemployerinformation.AddressContact = bdsInformationModel.TblBdsemployerinformation.Address;
                    bdsInformationModel.TblBdsemployerinformation.EmailContact = bdsInformationModel.TblBdsAdcount.Email;
                    bdsInformationModel.TblBdsemployerinformation.TypeContact = 1;
                    db.bdsemployerinformations.Add(bdsInformationModel.TblBdsemployerinformation);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return RedirectToAction("Thanks", "Register");
        }
        public ActionResult RegisterPersonal()
        {

            CaptCha cap = new CaptCha();
            bdsnew bdsNew = new bdsnew();
            var dataCity = from data in db.states
                           join datatext in db.stateTexts on data.name_id equals datatext.id
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
                Listbdsnewstype = db.bdsnewstypes.OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,
                tblbdsnew = bdsNew,
                ListBdsemployerinformation = db.bdsemployerinformations.ToList(),
                Listbdspersonalinformation = db.bdspersonalinformations.ToList(),
                ListBdsAdcount = db.bdsaccounts.ToList(),
                Listbdsemper = db.bdsempers.ToList()
            };
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult CheckEmail(string Email)
        {
            var countEmail = db.bdsaccounts.Where(x => x.Email == Email).Count();

            return Json(countEmail);
        }
        [HttpPost]
        public ActionResult RegisterPersonal(RegisterInformationModel bdsInformationModel)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != bdsInformationModel.tblCaptCha.Captcha)
            {
                CaptCha cap = new CaptCha();
                bdsnew bdsNew = new bdsnew();
                var dataCity = from data in db.states
                               join datatext in db.stateTexts on data.name_id equals datatext.id
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
                    Listbdsnewstype = db.bdsnewstypes.OrderBy(x => x.Order).ToList(),
                    ListGeoModel = dataCity.ToList(),
                    tblCaptCha = cap,
                    tblbdsnew = bdsNew,
                    ListBdsemployerinformation = db.bdsemployerinformations.ToList(),
                    Listbdspersonalinformation = db.bdspersonalinformations.ToList(),
                    ListBdsAdcount = db.bdsaccounts.ToList(),
                    Listbdsemper = db.bdsempers.ToList()
                };
                return View(registerModel);
            }
            else
            {
                var dataCity = from data in db.states
                               join datatext in db.stateTexts on data.name_id equals datatext.id
                               where datatext.language_id == "vi"
                               select new GeoModel { CityId = data.state_id, CityName = datatext.text };
                bdsInformationModel.TblBdsAdcount.CreateDate = DateTime.Now;
                bdsInformationModel.TblBdsAdcount.ModifiedDate = DateTime.Now;

                db.bdsaccounts.Add(bdsInformationModel.TblBdsAdcount);
                db.SaveChanges();
                bdsInformationModel.TblBdspersonalinformation.IdAccount = bdsInformationModel.TblBdsAdcount.Id;
                bdsInformationModel.TblBdspersonalinformation.Active = 1;
                bdsInformationModel.TblBdspersonalinformation.CreateDate = DateTime.Now;
                bdsInformationModel.TblBdspersonalinformation.ModifiedDate = DateTime.Now;
                bdsInformationModel.TblBdspersonalinformation.CreateUser = 1;
                bdsInformationModel.TblBdspersonalinformation.ModifiedUser = 1;
                bdsInformationModel.TblBdspersonalinformation.FullAddress = bdsInformationModel.TblBdspersonalinformation.Address + "," + dataCity.FirstOrDefault(x => x.CityId == bdsInformationModel.TblBdspersonalinformation.City).CityName;
                db.bdspersonalinformations.Add(bdsInformationModel.TblBdspersonalinformation);
                db.SaveChanges();

            }
            return RedirectToAction("Index", "Default");
        }
        [HttpPost]
        public ActionResult GetCity()
        {
            var dataCity = from data in db.states
                           join datatext in db.stateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            return Json(dataCity.ToList());
        }
        [HttpPost]
        public ActionResult GetDistrict(int id)
        {
            var dataDistrict = from data in db.districts
                               join datatext in db.districttexts on data.name_id equals datatext.id
                               where datatext.language_id == "vi" && data.state_id == id
                               select new GeoModel { DistId = data.district_id, DistName = datatext.text };
            return Json(dataDistrict.ToList());
        }

        public ActionResult UploadImg()
        {
            var path = string.Empty; var path1 = string.Empty;
            var NewPath = string.Empty;
            var fortmatName = string.Empty;
            var fileNameFull = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var file = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (file != null && file.ContentLength > 0)
                {

                    var fileName = Path.GetFileName(file.FileName);
                    string newFileNmae = Path.GetFileNameWithoutExtension(fileName);
                    fortmatName = Path.GetExtension(fileName);

                    NewPath = newFileNmae.Replace(newFileNmae, (DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()).ToString());
                    fileNameFull = DateTime.Now.Day + "" + DateTime.Now.Month + "_" + NewPath + fortmatName;
                    path = Server.MapPath("~/fileUpload/") + DateTime.Now.Day + DateTime.Now.Month + "/";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    path1 = Path.Combine(path, fileNameFull);

                    file.SaveAs(path1);
                }
            }

            if (HttpContext.Request.Url != null && HttpContext.Request.Url.Host.Contains("localhost"))

                path = ConfigurationManager.AppSettings["UrlImage"] + DateTime.Now.Day + DateTime.Now.Month + "/";

            var _fullUrl = path + fileNameFull;
            return Json(new
            {
                fullurl = _fullUrl,
                shorurl = "/fileUpload/" + DateTime.Now.Day + DateTime.Now.Month + "/" + fileNameFull,
                imgName = fileNameFull
            });

        }
        public ActionResult Thanks()
        {

            return View();
        }
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
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
            using (var gfx = Graphics.FromImage((Image)bmp))
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

                        gfx.DrawEllipse(pen, new Rectangle());
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

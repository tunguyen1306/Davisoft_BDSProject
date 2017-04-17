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
        davisoft_bdsprojectEntities2 db = new davisoft_bdsprojectEntities2();
        //
        // GET: /Register/

        public ActionResult RegisterCompany()
        {
            CaptCha cap = new CaptCha();
            BDSNew BDSNew = new BDSNew();
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
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
                ListBDSPersonalInformation = db.BDSPersonalInformations.ToList(),
                ListBdsAdcount = db.BDSAccounts.ToList(),
                ListBDSEmper = db.BDSEmpers.ToList()
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
                    BDSNew BDSNew = new BDSNew();
                    var dataCity = from data in db.States
                                   join datatext in db.StateTexts on data.name_id equals datatext.id
                                   where datatext.language_id == "vi"
                                   select new GeoModel { CityId = data.state_id, CityName = datatext.text };
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
                        ListBDSPersonalInformation = db.BDSPersonalInformations.ToList(),
                        ListBdsAdcount = db.BDSAccounts.ToList(),
                        ListBDSEmper = db.BDSEmpers.ToList()
                    };
                    return View(registerModel);
                }
                else
                {
                    bdsInformationModel.TblBdsAdcount.CreateDate = DateTime.Now;
                    bdsInformationModel.TblBdsAdcount.ModifiedDate = DateTime.Now;
                    db.BDSAccounts.Add(bdsInformationModel.TblBdsAdcount);
                    db.SaveChanges();
                    BDSEmployerInformation tblemployee = new BDSEmployerInformation();
                    bdsInformationModel.TblBDSEmployerInformation.IdAccount = bdsInformationModel.TblBdsAdcount.ID;

                    bdsInformationModel.TblBDSEmployerInformation.Active = 1;
                    bdsInformationModel.TblBDSEmployerInformation.CreateDate = DateTime.Now;
                    bdsInformationModel.TblBDSEmployerInformation.CreateUser = 1;
                    bdsInformationModel.TblBDSEmployerInformation.ModifiedDate = DateTime.Now;
                    bdsInformationModel.TblBDSEmployerInformation.ModifiedUser = 1;
                    bdsInformationModel.TblBDSEmployerInformation.Featured = 1;
                    bdsInformationModel.TblBDSEmployerInformation.PhoneContact = bdsInformationModel.TblBDSEmployerInformation.Phone;
                    bdsInformationModel.TblBDSEmployerInformation.DistrictContact = bdsInformationModel.TblBDSEmployerInformation.District;
                    bdsInformationModel.TblBDSEmployerInformation.City = bdsInformationModel.TblBDSEmployerInformation.City;
                    bdsInformationModel.TblBDSEmployerInformation.AddressContact = bdsInformationModel.TblBDSEmployerInformation.Address;
                    bdsInformationModel.TblBDSEmployerInformation.EmailContact = bdsInformationModel.TblBdsAdcount.Email;
                    bdsInformationModel.TblBDSEmployerInformation.TypeContact = 1;
                    db.BDSEmployerInformations.Add(bdsInformationModel.TblBDSEmployerInformation);
                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in State \"{1}\" has the following validation errors:",
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
            BDSNew BDSNew = new BDSNew();
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
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
                ListBDSPersonalInformation = db.BDSPersonalInformations.ToList(),
                ListBdsAdcount = db.BDSAccounts.ToList(),
                ListBDSEmper = db.BDSEmpers.ToList()
            };
            return View(registerModel);
        }
        [HttpPost]
        public ActionResult CheckEmail(string Email)
        {
            var countEmail = db.BDSAccounts.Where(x => x.Email == Email).Count();

            return Json(countEmail);
        }
        [HttpPost]
        public ActionResult RegisterPersonal(RegisterInformationModel bdsInformationModel)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != bdsInformationModel.tblCaptCha.Captcha)
            {
                CaptCha cap = new CaptCha();
                BDSNew BDSNew = new BDSNew();
                var dataCity = from data in db.States
                               join datatext in db.StateTexts on data.name_id equals datatext.id
                               where datatext.language_id == "vi"
                               select new GeoModel { CityId = data.state_id, CityName = datatext.text };
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
                    ListBDSPersonalInformation = db.BDSPersonalInformations.ToList(),
                    ListBdsAdcount = db.BDSAccounts.ToList(),
                    ListBDSEmper = db.BDSEmpers.ToList()
                };
                return View(registerModel);
            }
            else
            {
                var dataCity = from data in db.States
                               join datatext in db.StateTexts on data.name_id equals datatext.id
                               where datatext.language_id == "vi"
                               select new GeoModel { CityId = data.state_id, CityName = datatext.text };
                bdsInformationModel.TblBdsAdcount.CreateDate = DateTime.Now;
                bdsInformationModel.TblBdsAdcount.ModifiedDate = DateTime.Now;

                db.BDSAccounts.Add(bdsInformationModel.TblBdsAdcount);
                db.SaveChanges();
                bdsInformationModel.TblBDSPersonalInformation.IdAccount = bdsInformationModel.TblBdsAdcount.ID;
                bdsInformationModel.TblBDSPersonalInformation.Active = 1;
                bdsInformationModel.TblBDSPersonalInformation.CreateDate = DateTime.Now;
                bdsInformationModel.TblBDSPersonalInformation.ModifiedDate = DateTime.Now;
                bdsInformationModel.TblBDSPersonalInformation.CreateUser = 1;
                bdsInformationModel.TblBDSPersonalInformation.ModifiedUser = 1;
                bdsInformationModel.TblBDSPersonalInformation.FullAddress = bdsInformationModel.TblBDSPersonalInformation.Address + "," + dataCity.FirstOrDefault(x => x.CityId == bdsInformationModel.TblBDSPersonalInformation.City).CityName;
                db.BDSPersonalInformations.Add(bdsInformationModel.TblBDSPersonalInformation);
                db.SaveChanges();

            }
            return RedirectToAction("Index", "Default");
        }
        [HttpPost]
        public ActionResult GetCity()
        {
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoModel { CityId = data.state_id, CityName = datatext.text };
            return Json(dataCity.ToList());
        }
        [HttpPost]
        public ActionResult GetDistrict(int id)
        {
            var dataDistrict = from data in db.Districts
                               join datatext in db.DistrictTexts on data.name_id equals datatext.id
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

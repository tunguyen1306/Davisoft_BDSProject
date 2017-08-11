using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using WebBDS_Project.Models;

namespace WebBDS_Project.Controllers
{
    public class RegisterController : Controller
    {
        davisoft_bdsprojectEntities1 db = new davisoft_bdsprojectEntities1();
        //
        // GET: /Register/

        public ActionResult RegisterCompany()
        {
            CaptCha cap = new CaptCha();
            BDSNew BDSNew = new BDSNew();
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
            var dataDist = (from data in db.Districts
                            join datatext in db.DistrictTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi"
                            select new GeoModel { DistId = data.district_id, DistName = datatext.text }).ToList();
            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataDist.Insert(0, new GeoModel { DistId = 0, DistName = "Chọn quận/huyện" });
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
                ListGeoDisModel = dataDist.ToList(),
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
            bdsInformationModel.Status = false;
            try
            {
                bdsInformationModel.Status = false;
                if (Session["Captcha"] == null || Session["Captcha"].ToString() != bdsInformationModel.tblCaptCha.Captcha)
                {
                    CaptCha cap = new CaptCha();
                    BDSNew BDSNew = new BDSNew();
                    var dataCity = (from data in db.States
                                    join datatext in db.StateTexts on data.name_id equals datatext.id
                                    where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                                    select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

                    dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
                    dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
                    dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
                    var dataDist = (from data in db.Districts
                                    join datatext in db.DistrictTexts on data.name_id equals datatext.id
                                    where datatext.language_id == "vi"
                                    select new GeoModel { DistId = data.district_id, DistName = datatext.text }).ToList();
                
                    dataDist.Insert(0, new GeoModel { DistId = 0, DistName = "Chọn quận/huyện" });
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
                        ListGeoDisModel= dataCity.ToList(),
                        tblCaptCha = cap,
                        tblBDSNew = BDSNew,
                        ListBDSEmployerInformation = db.BDSEmployerInformations.ToList(),
                        ListBDSPersonalInformation = db.BDSPersonalInformations.ToList(),
                        ListBdsAdcount = db.BDSAccounts.ToList(),
                        ListBDSEmper = db.BDSEmpers.ToList(),
                        Status = false,
                        Msg = "Mã an toàn không đúng. Vui lòng nhập lại !"
                    };
                
                }
                else
                {
                    bdsInformationModel.TblBdsAdcount.CreateDate = DateTime.Now;
                    bdsInformationModel.TblBdsAdcount.CreateUser = 1;
                    bdsInformationModel.TblBdsAdcount.Active = 1;
                    bdsInformationModel.TblBdsAdcount.Money = 0;
                    bdsInformationModel.TblBdsAdcount.Point = 0;
                    bdsInformationModel.TblBdsAdcount.MailActive = 0;
                    bdsInformationModel.TblBdsAdcount.Token = Guid.NewGuid().ToString();
                 
                    db.BDSAccounts.Add(bdsInformationModel.TblBdsAdcount);
                    db.SaveChanges();
                    BDSEmployerInformation tblemployee = new BDSEmployerInformation();
                    bdsInformationModel.TblBDSEmployerInformation.IdAccount = bdsInformationModel.TblBdsAdcount.ID;

                    bdsInformationModel.TblBDSEmployerInformation.Active = 1;
                    bdsInformationModel.TblBDSEmployerInformation.CreateDate = DateTime.Now;
                    bdsInformationModel.TblBDSEmployerInformation.CreateUser = 1;
                    bdsInformationModel.TblBDSEmployerInformation.ModifiedDate = DateTime.Now;
                    bdsInformationModel.TblBDSEmployerInformation.ModifiedUser = 1;
                    bdsInformationModel.TblBDSEmployerInformation.Featured = 0;
                    bdsInformationModel.TblBDSEmployerInformation.PhoneContact = bdsInformationModel.TblBDSEmployerInformation.Phone;
                    bdsInformationModel.TblBDSEmployerInformation.DistrictContact = bdsInformationModel.TblBDSEmployerInformation.District;
                    bdsInformationModel.TblBDSEmployerInformation.City = bdsInformationModel.TblBDSEmployerInformation.City;
                    bdsInformationModel.TblBDSEmployerInformation.AddressContact = bdsInformationModel.TblBDSEmployerInformation.Address;
                    bdsInformationModel.TblBDSEmployerInformation.EmailContact = bdsInformationModel.TblBdsAdcount.Email;
                    bdsInformationModel.TblBDSEmployerInformation.TypeContact = 1;
                    bdsInformationModel.TblBDSEmployerInformation.IdAccount = bdsInformationModel.TblBdsAdcount.ID;
               
                    db.BDSEmployerInformations.Add(bdsInformationModel.TblBDSEmployerInformation);
                    db.SaveChanges();
              

                    bdsInformationModel.Status = true;
                    bdsInformationModel.Msg = "Tạo tài khoản thành công.Vui lòng kiểm tra email để kích hoạt tài khoản!";
                    SendTemplateEmail(bdsInformationModel.TblBdsAdcount.Email, bdsInformationModel.TblBdsAdcount.Email, bdsInformationModel.TblBdsAdcount.Token,"Email kích hoạt tài khoản");
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
                bdsInformationModel.Status = false;
                bdsInformationModel.Msg = "Tạo tài khoản thất bại!";
            }

            return Json(bdsInformationModel);
        }
        public ActionResult RegisterPersonal()
        {

            CaptCha cap = new CaptCha();
            BDSNew BDSNew = new BDSNew();
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
            var dataDist = (from data in db.Districts
                            join datatext in db.DistrictTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi"
                            select new GeoModel { DistId = data.district_id, DistName = datatext.text }).ToList();
          
            dataDist.Insert(0, new GeoModel { DistId = 0, DistName = "Chọn quận/huyện" });
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
                ListGeoDisModel = dataDist.ToList(),
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
            bdsInformationModel.Status=false;
            try
            {
                if (Session["Captcha"] == null || Session["Captcha"].ToString() != bdsInformationModel.tblCaptCha.Captcha)
                {
                    CaptCha cap = new CaptCha();
                    BDSNew BDSNew = new BDSNew();
                    var dataCity = (from data in db.States
                                    join datatext in db.StateTexts on data.name_id equals datatext.id
                                    where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                                    select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

                    dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
                    dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
                    dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });

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
                        ListBDSEmper = db.BDSEmpers.ToList(),
                        Status = false,
                        Msg = "Mã an toàn không đúng. Vui lòng nhập lại !"

                    };
                
                }
                else
                {

                    bdsInformationModel.TblBdsAdcount.CreateDate = DateTime.Now;
                    bdsInformationModel.TblBdsAdcount.CreateUser = 1;
                    bdsInformationModel.TblBdsAdcount.Active = 1;
                    bdsInformationModel.TblBdsAdcount.Money = 0;
                    bdsInformationModel.TblBdsAdcount.Point = 0;
                    bdsInformationModel.TblBdsAdcount.MailActive = 0;
                    bdsInformationModel.TblBdsAdcount.Token = Guid.NewGuid().ToString();


                    db.Entry(bdsInformationModel.TblBdsAdcount).State = EntityState.Added;
                    db.SaveChanges();

                    bdsInformationModel.TblBDSPersonalInformation.IdAccount = bdsInformationModel.TblBdsAdcount.ID;
                    bdsInformationModel.TblBDSPersonalInformation.Active = 1;
                    bdsInformationModel.TblBDSPersonalInformation.CreateDate = DateTime.Now;
                    bdsInformationModel.TblBDSPersonalInformation.ModifiedDate = DateTime.Now;
                    bdsInformationModel.TblBDSPersonalInformation.CreateUser = 1;
                    bdsInformationModel.TblBDSPersonalInformation.ModifiedUser = 1;

                    db.Entry(bdsInformationModel.TblBDSPersonalInformation).State = EntityState.Added;
                    db.SaveChanges();

                    bdsInformationModel.tblBDSPerNew.Active = 1;
                    bdsInformationModel.tblBDSPerNew.CreateDate = DateTime.Now;
                    bdsInformationModel.tblBDSPerNew.CreateUser = 1;
                    bdsInformationModel.tblBDSPerNew.CreateUser = 1;
                    bdsInformationModel.tblBDSPerNew.PerId = bdsInformationModel.TblBDSPersonalInformation.ID;
                    bdsInformationModel.tblBDSPerNew.ProvinceProfile =
                        bdsInformationModel.TblBDSPersonalInformation.Province;
                    db.Entry(bdsInformationModel.tblBDSPerNew).State = EntityState.Added;
                    db.SaveChanges();

                    bdsInformationModel.Status = true;
                    bdsInformationModel.Msg = "Tạo tài khoản thành công.Vui lòng kiểm tra email để kích hoạt tài khoản!";
                    SendTemplateEmail(bdsInformationModel.TblBdsAdcount.Email, bdsInformationModel.TblBdsAdcount.Email, bdsInformationModel.TblBdsAdcount.Token, "Email kích hoạt tài khoản");
                }
            }
            catch (Exception)
            {
                bdsInformationModel.Status = false;
                bdsInformationModel.Msg = "Tạo tài khoản thất bại!";
               
            }
            
            return Json(bdsInformationModel);
        }
        [HttpPost]
        public ActionResult GetCity()
        {
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
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
                    path = Server.MapPath("~/fileupload/") + DateTime.Now.Day + DateTime.Now.Month + "/";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    path1 = Path.Combine(path, fileNameFull);

                    file.SaveAs(path1);
                }
            }

            //if (HttpContext.Request.Url != null && HttpContext.Request.Url.Host.Contains("localhost"))

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

        public ActionResult SendTemplateEmail(string recepientEmail, string username,string key,string Subject)
        {
            string body = string.Empty;
            var activelink = "/Register/Active/" + key;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Template/Email/Activation.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("##name##", username);
            body = body.Replace("##activatelink##", activelink);
            SendEmail("donotreply@example.com", recepientEmail, Subject, body);

            return Json("");
        }
        public static bool SendEmail(string from, string to, string subject, string body)
        {
            //var emailUsername = Settings.GetSetting("EmailConfig", "Username", typeof(string));
            //var emailPassword = Settings.GetSetting("EmailConfig", "Password", typeof(string));
            try
            {
                var section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                var message = new MailMessage("donotreply@example.com", to, subject, body) { IsBodyHtml = true };
                message.From = new MailAddress("donotreply@example.com", "", Encoding.UTF8);// Settings.GetSetting("Report", "CompanyName", typeof(string))
                message.ReplyToList.Add(from);
                var client = new SmtpClient();
                client.Port = section.Network.Port;//Settings.GetSetting("EmailConfig", "Port", typeof(int));
                client.Host = section.Network.Host;//Settings.GetSetting("EmailConfig", "Host", typeof(string));
                client.EnableSsl = section.Network.EnableSsl;//true;
                // setup up the host, increase the timeout to 60 minutes
                client.Timeout = (60 * 60 * 1000);
                client.DeliveryMethod = section.DeliveryMethod;//SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(section.Network.UserName, section.Network.Password);
                //
                client.Send(message);

            }
            catch (Exception e)
            {
                //   Tools.Logger(e.ToString(),"sendmailerr","SendMail");
                return false;
            }
            return true;
        }
        public ActionResult Active(string token)
        {
            var result = 0;
            var data = db.BDSAccounts.Where(x => x.Token == token).ToList();
            if (data.Count>0)
            {
                result = 1;

            }
            else
            {
                result = 0;
            }

            return Json(new { result= result });
        }

    }
}

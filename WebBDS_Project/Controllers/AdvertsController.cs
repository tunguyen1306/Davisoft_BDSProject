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
        davisoft_bdsprojectEntities1 db = new davisoft_bdsprojectEntities1();
        //
        // GET: /Adverts/

        public ActionResult AdvertCompany()
        {
            if (Session["IdUser"] ==null && Session["EmailUser"] == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }
            int idAccount = (int)Session["IdUser"];

            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();
          
            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố"});
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
            if (!db.BDSEmployerInformations.Select(x => x.IdAccount).ToList().Contains(idAccount))
            {
                return RedirectToAction("Warning", "Adverts");
            }

            var TblBDSEmployerInformation = db.BDSEmployerInformations.First(T => T.IdAccount == idAccount);
            CaptCha cap = new CaptCha();
            BDSNew BDSNew = new BDSNew { 
                CreateUser = 1, 
                Active = 1,
                CreateDate = DateTime.Now,
                DateReup = null,
                IdLanguage = 1,
                FromCreateNews = DateTime.Now, 
                ToCreateNews =DateTime.Now.AddDays(3), 
                FromDeadline = DateTime.Now,
                ToDeadline = DateTime.Now.AddDays(30),
                NameCompany = TblBDSEmployerInformation.Name,
                AddressApply = TblBDSEmployerInformation.AddressContact,
                NamesContact = TblBDSEmployerInformation.NameContact,
                Email = TblBDSEmployerInformation.EmailContact,
                WebSiteCompany = TblBDSEmployerInformation.WebSite
                
                
            };
            var registerModel = new RegisterInformationModel
            {
                ListBDSScopes = db.BDSScopes.Where(T=>T.Active==1).ToList(),
                ListMarriea = db.BDSMarriages.Where(T => T.Active == 1).ToList(),
                ListSalary = db.BDSSalaries.Where(T => T.Active == 1).ToList(),
                ListDucation = db.BDSEducations.Where(T => T.Active == 1).ToList(),
                ListBDSCareer = db.BDSCareers.Where(T => T.Active == 1).ToList(),
                ListTimework = db.BDSTimeWorks.Where(T => T.Active == 1).ToList(),
                ListBDSLanguage = db.BDSLanguages.Where(T => T.Active == 1).ToList(),
                ListBDSNewsType = db.BDSNewsTypes.Where(T => T.Active == 1).OrderBy(x => x.Order).ToList(),
                ListGeoModel = dataCity.ToList(),
                tblCaptCha = cap,
                tblBDSNew= BDSNew,
                TblBDSEmployerInformation =TblBDSEmployerInformation
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
        public ActionResult AdvertCompany(RegisterInformationModel create, HttpPostedFileBase[] files)
        {
            create.Status = false;
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != create.tblCaptCha.Captcha)
            {
                ModelState.AddModelError("Captcha", "Wrong value of sum, please try again.");
                create.Msg = "Capcha không chính xác!";
                var dataCity = (from data in db.States
                                join datatext in db.StateTexts on data.name_id equals datatext.id
                                where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                                select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

                dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
                dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
                dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
                CaptCha cap = new CaptCha();
                var registerModel = new RegisterInformationModel
                {
                    ListBDSScopes = db.BDSScopes.Where(T => T.Active == 1).ToList(),
                    ListMarriea = db.BDSMarriages.Where(T => T.Active == 1).ToList(),
                    ListSalary = db.BDSSalaries.Where(T => T.Active == 1).ToList(),
                    ListDucation = db.BDSEducations.Where(T => T.Active == 1).ToList(),
                    ListBDSCareer = db.BDSCareers.Where(T => T.Active == 1).ToList(),
                    ListTimework = db.BDSTimeWorks.Where(T => T.Active == 1).ToList(),
                    ListBDSLanguage = db.BDSLanguages.Where(T => T.Active == 1).ToList(),
                    ListBDSNewsType = db.BDSNewsTypes.Where(T => T.Active == 1).OrderBy(x => x.Order).ToList(),
                    ListGeoModel = dataCity.ToList(),
                    tblCaptCha = cap,
                
                   

                };        
                return Json(registerModel);
            }
            else
            {
                var checkMoney = false;
                if (Session["IdUser"] != null)
                {
                    int id = int.Parse(Session["IdUser"].ToString());
                    var model = db.BDSAccounts.FirstOrDefault(T => T.ID == id);
                    if (model != null)
                    {
                        if (model.Money >= create.tblBDSNew.TotalMoney)
                        {
                            checkMoney = true;
                        }
                    }
                }

                if (!checkMoney)
                {
                    create.Msg = "Số tiền trong tài khoản không đủ!";
                    ModelState.AddModelError("TotalMoney", "Số tiền trong tài khoản không đủ!");
                    var dataCity = (from data in db.States
                                    join datatext in db.StateTexts on data.name_id equals datatext.id
                                    where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                                    select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

                    dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
                    dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
                    dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
                    CaptCha cap = new CaptCha();
                 
                    var registerModel = new RegisterInformationModel
                    {
                        ListBDSScopes = db.BDSScopes.Where(T => T.Active == 1).ToList(),
                        ListMarriea = db.BDSMarriages.Where(T => T.Active == 1).ToList(),
                        ListSalary = db.BDSSalaries.Where(T => T.Active == 1).ToList(),
                        ListDucation = db.BDSEducations.Where(T => T.Active == 1).ToList(),
                        ListBDSCareer = db.BDSCareers.Where(T => T.Active == 1).ToList(),
                        ListTimework = db.BDSTimeWorks.Where(T => T.Active == 1).ToList(),
                        ListBDSLanguage = db.BDSLanguages.Where(T => T.Active == 1).ToList(),
                        ListBDSNewsType = db.BDSNewsTypes.Where(T => T.Active == 1).OrderBy(x => x.Order).ToList(),
                        ListGeoModel = dataCity.ToList(),
                        tblCaptCha = cap,
                     
                      
                       

                    };
                    return Json(registerModel);
                }
                create.tblBDSNew.IdAcount=int.Parse( Session["IdUser"].ToString());
                create.tblBDSNew.FromDeadline = DateTime.Now;          
                create.tblBDSNew.CreateDate = DateTime.Now;
                create.tblBDSNew.Active = 1;
                create.tblBDSNew.Status = 0;
                create.tblBDSNew.CountReup = 0;
                create.tblBDSNew.MaxReup = 0;
                create.tblBDSNew.CreateUser = 1;

                create.tblBDSNew.Career = create.tblBDSNew.Career.Substring(create.tblBDSNew.Career.Length - 1) == "," ? create.tblBDSNew.Career.Substring(0, create.tblBDSNew.Career.Length - 1) : create.tblBDSNew.Career;
                create.tblBDSNew.IdTypeNewsCuurent = create.tblBDSNew.IdTypeNews;
                db.Entry( create.tblBDSNew).State=EntityState.Added;
                db.SaveChanges();
                if (files!=null && files.Length>0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i] != null)
                        {
                            byte[] binaryData;
                            binaryData = new Byte[files[i].InputStream.Length];
                            long bytesRead = files[i].InputStream.Read(binaryData, 0, (int)files[i].InputStream.Length);
                            files[i].InputStream.Close();
                            string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
                            SaveImg(new NewsPicture { nameImg = files[i].FileName, idProducts = create.tblBDSNew.ID, isactive = 1, index = i, cfile = base64String });
                        }
                   }
                }
              
            
                     

                foreach (var item in create.tblBDSNew.Career.Split(','))
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        BDSNews_Career i = new BDSNews_Career { ID_News = create.tblBDSNew.ID, ID_Career = int.Parse(item) };
                        db.Entry(i).State = EntityState.Added;
                    }
                   
                }
                db.SaveChanges();

                var type = db.BDSNewsTypes.First(T => T.ID == create.tblBDSNew.IdTypeNewsCuurent.Value);
                String Fname = "Đăng tin '{A}' trong vòng '{B}' ngày tổng giá phải trả '{C}' VNĐ";
                string name = Fname.Replace("{A}", type.Name).Replace("{B}", ((int)Math.Ceiling(create.tblBDSNew.ToCreateNews.Value.Subtract(create.tblBDSNew.FromCreateNews.Value).TotalDays)) + "").Replace("{C}", create.tblBDSNew.TotalMoney.Value.ToString("n2"));
                BDSTransactionHistory tran = new BDSTransactionHistory()
                {
                    Name = name,
                    Description = name,
                    KeySearch = name.NormalizeD(),
                    Active = 1,
                    CreateUser = 1,
                    CreateDate = DateTime.Now,
                    TypeTran = 2,
                    PointTran = 0,
                    MoneyTran = create.tblBDSNew.TotalMoney.Value,
                    DateTran = DateTime.Now,

                };
                create.tblBDSNew = db.BDSNews.First(T => T.ID == create.tblBDSNew.ID);
                db.BDSTransactionHistories.Add(tran);
                db.SaveChanges();
                create.tblBDSNew.RefTranHis = tran.ID;
                db.Entry(create.tblBDSNew).State=EntityState.Modified;
                db.SaveChanges();
                var account = db.BDSAccounts.First(T => T.ID == create.tblBDSNew.IdAcount);
                account.Money -= create.tblBDSNew.TotalMoney;
                db.Entry(account).State = EntityState.Modified;              
                db.SaveChanges();

               
               
                create.Msg = "Đăng tin thành công!";
                create.Status = true;
                return Json(create);
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
                tblPicture = new BDSPicture { advert_id = newPicture.idProducts,Active=1, angleType = 0, cms_sql_id = 0, converted = DateTime.Now, tocheck =true, type_id = 1, title = newPicture.nameImg, position = newPicture.isactive }
            };
            if (newPicture.idpicture == 0)
            {
                var settings = new ResizeSettings();
                settings.Scale = ScaleMode.DownscaleOnly;
                settings.Format = format;

                //string uploadFolder = picture.DirectoryPhysical;

                string path = Server.MapPath("~/fileupload/") + DateTime.Now.Day + DateTime.Now.Month + "/";
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
            var dataCity = (from data in db.States
                            join datatext in db.StateTexts on data.name_id equals datatext.id
                            where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                            select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

            dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
            dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
            dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
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
        public ActionResult EditNews(RegisterInformationModel create, HttpPostedFileBase[] files, int[] img_id)
        {

            if (Session["Captcha"] == null || Session["Captcha"].ToString() != create.tblCaptCha.Captcha)
            {
                ModelState.AddModelError("Captcha", "Wrong value of sum, please try again.");
                var dataCity = (from data in db.States
                                join datatext in db.StateTexts on data.name_id equals datatext.id
                                where datatext.language_id == "vi" && data.state_id != 59 && data.state_id != 28
                                select new GeoModel { CityId = data.state_id, CityName = datatext.text }).ToList();

                dataCity.Insert(0, new GeoModel { CityId = 0, CityName = "Chọn thành/phố" });
                dataCity.Insert(1, new GeoModel { CityId = 59, CityName = "TP.Hồ Chí Minh" });
                dataCity.Insert(2, new GeoModel { CityId = 28, CityName = "TP.Hà Nội" });
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
                tblNews.UrlImage = create.tblBDSNew.UrlImage;          

                tblNews.Career = tblNews.Career.Substring(tblNews.Career.Length - 1) == "," ? tblNews.Career.Substring(0, tblNews.Career.Length - 1) : tblNews.Career;
                db.Entry(tblNews).State = EntityState.Modified;
                db.SaveChanges();
                if (img_id != null && img_id.Length > 0)
                {
                    var listPic = db.BDSPictures.Where(T => T.advert_id == tblNews.ID && !img_id.Contains(T.ID)).ToList();
                    foreach (var pic in listPic)
                    {
                        db.Entry(pic).State = EntityState.Deleted;
                    }
                }
                     
                if (files != null && files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i] != null)
                        {
                            byte[] binaryData;
                            binaryData = new Byte[files[i].InputStream.Length];
                            long bytesRead = files[i].InputStream.Read(binaryData, 0, (int)files[i].InputStream.Length);
                            files[i].InputStream.Close();
                            string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
                            SaveImg(new NewsPicture { nameImg = files[i].FileName, idProducts = create.tblBDSNew.ID, isactive = 1, index = i, cfile = base64String });
                        }
                    }
                }
               
                db.SaveChanges();


                var listMap = db.BDSNews_Career.Where(T => T.ID_News == tblNews.ID).ToList();
                foreach (var item in listMap)
                {
                    db.Entry(item).State = EntityState.Deleted;

                }
                db.SaveChanges();
                foreach (var item in create.tblBDSNew.Career.Split(','))
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        BDSNews_Career i = new BDSNews_Career { ID_News = create.tblBDSNew.ID, ID_Career = int.Parse(item) };
                        db.Entry(i).State = EntityState.Added;
                    }
                   
                }
                db.SaveChanges();
                return RedirectToAction("ListNewOfUser", "Management");
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
       
        public void DeleteIMG(string picture)
        {
            NewsPicture vgp = new NewsPicture();
            if (picture == null)
                return;
            var fo = picture.Substring(0, 3);
            string dir = Server.MapPath("~/fileupload/" + fo + "/" + picture);

            System.IO.File.Delete(dir);



        }
        public ActionResult DeleteAdvert(int idAdvert)
        {
            BDSNew tblNew = db.BDSNews.Find(idAdvert);
            db.BDSNews.Remove(tblNew);
            db.SaveChanges();
            return RedirectToAction("ListNewOfUser", "Management");
        }

  
        public JsonResult CalcuMoney(int idType, string rDate)
        {
            try
            {
                var price = db.BDSNewsTypePrices
                .Where(T => T.IdNewsType == idType && T.Active == 1 && T.ApplyPrice <= DateTime.Now)
                .OrderByDescending(T => T.ApplyPrice)
                .FirstOrDefault();

                var fromDate = rDate.Split('-')[0];
                var toDate = rDate.Split('-')[1];
                var FromDate = DateTime.Parse(fromDate, MvcApplication.CultureInfo, System.Globalization.DateTimeStyles.None);
                var ToDate = DateTime.Parse(toDate, MvcApplication.CultureInfo, System.Globalization.DateTimeStyles.None);
                var totalDay = ToDate.Subtract(FromDate).TotalDays;
                if (totalDay < 1)
                {
                    totalDay = 1;
                }
                else
                {
                    totalDay = Math.Ceiling(totalDay);
                }
                var pInDay = 0.0;
                var pAll = 0.0;
                if (price != null)
                {
                    pInDay = price.Price;
                    pAll = totalDay * pInDay;
                }
                var checkMoney = false;
                if (Session["IdUser"]!=null)
                {
                    int id = int.Parse( Session["IdUser"].ToString());
                    var model= db.BDSAccounts.FirstOrDefault(T=>T.ID==id);
                    if (model!=null)
                    {
                        if( model.Money>=pAll)
                        {
                            checkMoney = true;
                        }
                    }
                }

                return Json(new { PriceInDay = pInDay, TotalPrice = pAll, TotalDay = totalDay, CheckMoney = checkMoney }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpPost]
        public JsonResult Reup(int id)
        {
            try
            {
               var news=  db.BDSNews.Where(T => T.ID == id).FirstOrDefault();
               if (news != null && news.CountReup < news.MaxReup && DateTime.Now <= news.ToCreateNews)
                {
                    news.CountReup += 1;
                    news.DateReup = news.FromCreateNews >= DateTime.Now ? news.FromCreateNews : DateTime.Now;
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { Status = 1 });
                }
              
            }
            catch (Exception e)
            {

                return Json(new { Status = 0,Error=e.Message });
            }
            return Json(new { Status = 0});
        }
        public JsonResult EmployerInformation(int idAccount)
        {
            return Json(
               db.BDSEmployerInformations.Where(T => T.IdAccount == idAccount).ToList().Select(T => new
                 {
                     Name = T.Name,
                     AddressContact = T.AddressContact + ", " + (from a in db.Districts
                                                                 join b in db.DistrictTexts on a.name_id equals b.id
                                                                 where b.language_id == "vi" && a.state_id == T.DistrictContact
                                                                 select new { ID = a.state_id, Name = b.text }).FirstOrDefault().Name + ", " +

                                                                 (from a in db.States
                                                                  join b in db.StateTexts on a.name_id equals b.id
                                                                  where b.language_id == "vi" && a.Status == 1 && a.state_id == T.CityContact
                                                                  select new { ID = a.state_id, Name = b.text }).FirstOrDefault().Name
                                                                 ,
                     NameContact = T.NameContact,
                     PhoneContact = T.PhoneContact,
                     EmailContact = T.EmailContact,
                     WebSite = T.WebSite
                 }).FirstOrDefault(),
                 JsonRequestBehavior.AllowGet);
        }
        public ActionResult Warning()
        {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBDS_Project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Management
            routes.MapRoute(
             name: "quan-li-tin-dang",
             url: "tai-khoan/quan-li-tin-dang",
             defaults: new { controller = "Management", action = "ListNewOfUser" }
         );
            
            routes.MapRoute(
             name: "quan-ly-tai-khoan-nha-tuyen-dung",
             url: "tai-khoan/quan-ly-tai-khoan-nha-tuyen-dung",
             defaults: new { controller = "Management", action = "ManagementAcountEmployee" }
         );
            routes.MapRoute(
            name: "quan-ly-nha-tuyen-dung",
            url: "tai-khoan/quan-ly-nha-tuyen-dung",
            defaults: new { controller = "Management", action = "ManagementCompany" }
        );
            routes.MapRoute(
            name: "nop-tien",
            url: "tai-khoan/nop-tien",
            defaults: new { controller = "Management", action = "Payment" }
        );
            routes.MapRoute(
           name: "cam-on",
           url: "tai-khoan/cam-on",
           defaults: new { controller = "Management", action = "ThanksApply" }
       );
            routes.MapRoute(
          name: "ho-so-da-luu",
          url: "tai-khoan/ho-so-da-luu",
          defaults: new { controller = "Management", action = "YourArchive" }
      );
            routes.MapRoute(
          name: "quen-mat-khau",
          url: "tai-khoan/quen-mat-khau",
          defaults: new { controller = "Management", action = "ForgetPass" }
      ); routes.MapRoute(
          name: "ung-vien-gui-ho-so",
          url: "tai-khoan/ung-vien-gui-ho-so",
          defaults: new { controller = "Management", action = "ApplyArchive" }
      );
            //Login
            routes.MapRoute(
          name: "dang-nhap",
          url: "tai-khoan/dang-nhap",
          defaults: new { controller = "Login", action = "Login" }
      );
            routes.MapRoute(
         name: "trang-dang-nhap",
         url: "tai-khoan/trang-dang-nhap",
         defaults: new { controller = "Login", action = "LoginForm" }
     );
            //tuyendung
            routes.MapRoute(
      name: "trang-chu",
      url: "tuyen-dung/trang-chu",
      defaults: new { controller = "Default", action = "Index" }
  );
            routes.MapRoute(
    name: "chi-tiet-tin-tuc",
    url: "tin-tuc/chi-tiet-tin-tuc",
    defaults: new { controller = "Default", action = "DetailNews"}
);
            routes.MapRoute(
   name: "chi-tiet-tin",
   url: "tin-tuc/chi-tiet-tin",
   defaults: new { controller = "Default", action = "Detail" }
);
            routes.MapRoute(
name: "tiem-kiem",
url: "tin-tuc/tiem-kiem",
defaults: new { controller = "Default", action = "Search" }
);
            //dang-tin
            routes.MapRoute(
name: "dang-tin",
url: "tin-tuyen-dung/dang-tin",
defaults: new { controller = "Adverts", action = "AdvertCompany" }
);
            //Dang ky
            routes.MapRoute(
name: "dang-ky-tuyen-dung",
url: "dang-ky/dang-ky-tuyen-dung",
defaults: new { controller = "Register", action = "RegisterCompany" }
);
            routes.MapRoute(
name: "dang-ky-nguoi-tim-viec",
url: "dang-ky/dang-ky-nguoi-tim-viec",
defaults: new { controller = "Register", action = "RegisterPersonal" }
);
            routes.MapRoute(
name: "dang-tin-thanh",
url: "dang-ky/dang-tin-thanh",
defaults: new { controller = "Register", action = "Thanks" }
);


            routes.MapRoute(
               name: "tim-kiem",
               url: "tim-kiem",
               defaults: new { controller = "Default", action = "Search" }
           );
            routes.MapRoute(
               name: "sua-tin-dang",
               url: "ti-tuc-tuyen-dung/sua-tin-dang",
               defaults: new { controller = "Adverts", action = "EditNews" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        
          

        }
    }
}
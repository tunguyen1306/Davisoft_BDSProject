using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NS.Entity;
using Davisoft_BDSProject.Domain;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Domain.Enum;
using Davisoft_BDSProject.Domain.Helpers;

namespace Davisoft_BDSProject.Seed
{
    [TestClass]
    public class DatabaseSeeder
    {
        [TestMethod]
        public void Insert_data_for_test()
        {
            using (var db = new Davisoft_BDSProjectDb())
            {
                db.Database.Delete();
                db.Database.Create();

                Insert_counters(db);
                Insert_currencies_and_exchanges(db);
                Insert_roles_and_users(db);
                Insert_menu(db);
                Insert_settings(db);
                Insert_Language(db);
                db.SaveChanges();
            }
        }
        private void Insert_Language(Davisoft_BDSProjectDb db)
        {
            db.Languages.Add(new Language { Value = "en-GB", DisplayName = "English", Image = "flag-icon flag-icon-gb" });
            db.SaveChanges();
        }

        private void Insert_counters(Davisoft_BDSProjectDb db)
        {
            db.Counters.Add(new Counter("BookingRunningNumber", 2000000));
            db.SaveChanges();
        }
        private void Insert_settings(Davisoft_BDSProjectDb db)
        {
            db.Settings.Add(new Setting { Module = Enum.GetName(typeof(Setting.ModuleType), Setting.ModuleType.SystemConfig), Name = Enum.GetName(typeof(Setting.ModuleKey.SystemConfig), Setting.ModuleKey.SystemConfig.LoginByEmail), Value = true.ToString(), Type = "Checkbox", Summary = "Use email to login", Note = "", Description = "Check to login using Email instead of DisplayName" });
            db.Settings.Add(new Setting { Module = Enum.GetName(typeof(Setting.ModuleType), Setting.ModuleType.SystemConfig), Name = Enum.GetName(typeof(Setting.ModuleKey.SystemConfig), Setting.ModuleKey.SystemConfig.Gst), Value = "7", Type = "Textbox", Summary = "GST", Note = "", Description = "The GST to calculate price" });
            db.SaveChanges();
        }

        private void Insert_currencies_and_exchanges(Davisoft_BDSProjectDb db)
        {
            db.Currencies.Add(new Currency { Name = "SGD", IsDefault = true });
            db.Currencies.Add(new Currency { Name = "USD" });
            db.SaveChanges();
        }

        private void Insert_roles_and_users(Davisoft_BDSProjectDb db)
        {

            var rAdmin = new Role { Name = "Admin", RoleLevel = RoleLevel.CompanyLevel };

            db.Users.Add(new User { DisplayName = "ADMIN", Password = EncryptHelper.EncryptPassword("123456"), Email = "ADMIN@LOCALHOST", Phone = "+84 0168 7040 132", Roles = { rAdmin } });
            db.SaveChanges();
        }
        private void Insert_menu(Davisoft_BDSProjectDb db)
        {
            Func<string, Role> getRoleByName =
                roleName => db.Roles.FirstOrDefault(r => r.Name == roleName);
            Menu m12 = db.MenuItems.Add(new Menu { Title = "Master file" });
            Menu m13 = db.MenuItems.Add(new Menu { Title = "System Settings" });

            db.SaveChanges();

            db.MenuItems.Add(new Menu { Title = "Branches", Url = "/Branch", ParentID = m12.ID, Icon = "i-brand" });
            db.MenuItems.Add(new Menu { Title = "Users", Url = "/Users/Index", ParentID = m12.ID, Icon = "i-multi-agents" });
            db.MenuItems.Add(new Menu { Title = "Users Deleted", Url = "/Users/ListDeletedUsers", ParentID = m12.ID, Icon = "i-multi-agents" });
            db.MenuItems.Add(new Menu { Title = "Roles", Url = "/Roles", ParentID = m12.ID, Icon = "i-role" });
            db.MenuItems.Add(new Menu { Title = "Countries", Url = "/Countries", ParentID = m12.ID, Icon = "i-world" });
            db.MenuItems.Add(new Menu { Title = "Currencies", Url = "/Currencies", ParentID = m12.ID, Icon = "i-currency" });
            db.MenuItems.Add(new Menu { Title = "Holiday", Url = "/Holiday", ParentID = m12.ID, Icon = "i-holiday" });
            db.MenuItems.Add(new Menu { Title = "Menus", Url = "/Menus", ParentID = m13.ID, Icon = "i-menu" });
            db.MenuItems.Add(new Menu { Title = "Settings", Url = "/Settings", ParentID = m13.ID, Icon = "i-configuration" });
            db.MenuItems.Add(new Menu { Title = "Email Settings", Url = "/EmailReceiveSetting/Index", ParentID = m13.ID, Icon = "i-configuration" });
            db.MenuItems.Add(new Menu { Title = "Logs", Url = "/Audit", ParentID = m13.ID, Icon = "i-log" });
            db.MenuItems.Add(new Menu { Title = "Resources", Url = "/Resource", ParentID = m13.ID, Icon = "i-resource" });

            db.SaveChanges();
        }
    }
}

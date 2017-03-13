using System;
using System.Collections.Generic;
using System.Linq;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using NS.Mvc;

namespace Davisoft_BDSProject.Web.Models
{
    public class RoleModel
    {
        public const string PermissionNamePrefix = "CheckBox.Permission.";
        public const string SettingPermissionNamePrefix = "CheckBox.SettingPermission.";

        public Role Role { get; set; }
        public IDictionary<string, IList<Permission>> Permissions { get; set; }
        public IDictionary<string, IList<Permission>> SettingPermissions { get; set; }

        public void PrepareSettingPermission()
        {
            var settingService = DependencyHelper.GetService<ISettingRepository>();
            var settings = settingService.GetSettings();
            SettingPermissions = new Dictionary<string, IList<Permission>>();
            foreach (string module in settings.Select(m => m.Module).Distinct())
            {
                SettingPermissions.Add(module, new List<Permission>());
                foreach (var setting in settings.Where(m => m.Module == module))
                {
                    SettingPermissions[module].Add(new Permission { Target = setting.Module, Right = setting.Name, DisplayName = setting.Summary });
                }
            }
        }
    }
}

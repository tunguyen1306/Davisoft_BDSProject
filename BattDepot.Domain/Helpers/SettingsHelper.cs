using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Davisoft_BDSProject.Domain.Concrete;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Helpers
{
    public class SettingsHelper
    {
        private DbContext _db;

        public SettingsHelper(DbContext db)
        {
            _db = db;
        }
        /// <summary>
        ///     Get value of property in system setting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingModule">Davisoft_BDSProject.Domain.Entities.Setting.ModuleType.Indent or any one</param>
        /// <param name="keyGetValue">Setting.ModuleKey.*.* : the key to get it's value</param>
        /// <param name="typeofData">type of return value</param>
        /// <returns>Return a value of the key in module</returns>
        public dynamic GetSetting<T>(Setting.ModuleType settingModule, T keyGetValue, Type typeofData)
        {
            string module = System.Enum.GetName(typeof(Setting.ModuleType), settingModule);
            string key = System.Enum.GetName(typeof(T), keyGetValue);
            var settingService = new EFSettingRepository(_db);
            Setting t = settingService.GetSetting(module, key);
            dynamic tempData = null;
            if (typeofData == typeof(int)) tempData = Convert.ToInt32(t.Value);
            else if (typeofData == typeof(decimal)) tempData = Convert.ToDecimal(t.Value);
            else if (typeofData == typeof(bool)) tempData = Convert.ToBoolean(t.Value);
            else tempData = t.Value;
            return tempData;
        }

        //#region Nested type: Api Setting
        //public string ApiUsername()
        //{
        //    return GetSetting(Setting.ModuleType.SystemConfig, Setting.ModuleKey.SystemConfig.ApiUsername, typeof(string));
        //}
        //public string ApiPassword()
        //{
        //    return GetSetting(Setting.ModuleType.SystemConfig, Setting.ModuleKey.SystemConfig.ApiPassword, typeof(string));
        //}
        //#endregion Nested type: Api



    }
}

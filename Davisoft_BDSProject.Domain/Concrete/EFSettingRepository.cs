using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFSettingRepository : Repository, ISettingRepository
    {
        public EFSettingRepository(DbContext db) : base(db)
        {
        }

        public IEnumerable<Setting> GetSettings()
        {
            return GetAll<Setting>().AsEnumerable();
        }

        public Setting GetSetting(int settingId)
        {
            return Get<Setting>(i => i.ID == settingId);
        }

        public Setting GetSetting(string module, string name)
        {
            return Get<Setting>(i => i.Module == module && i.Name == name);
        }

        public Setting CreateSetting(Setting info)
        {
            return Create(info);
        }

        public bool UpdateSetting(Setting info)
        {
            return Update(info);
        }

        public bool DeleteSetting(int settingId)
        {
            return Delete<Setting>(settingId);
        }
    }
}

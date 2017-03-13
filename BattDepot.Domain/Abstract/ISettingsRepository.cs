using System.Collections.Generic;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface ISettingRepository
    {
        IEnumerable<Setting> GetSettings();
        Setting GetSetting(int settingId);
        Setting GetSetting(string module, string name);
        Setting CreateSetting(Setting info);
        bool UpdateSetting(Setting info);
        bool DeleteSetting(int settingId);

        IEnumerable<Counter> GetAllCounter(string name);
        int GetCounter(string name, int defaultValue = 1);
        int UpdateCounter(string name, int defaultValue = 1);
        bool UpdateCounter(Counter counter);
    }
}

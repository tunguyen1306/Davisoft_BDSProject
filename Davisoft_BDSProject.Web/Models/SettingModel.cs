using System.Collections.Generic;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Web.Models
{
    public class SettingModel
    {
        public Setting Setting { get; set; }
        public string Module { get; set; }
        public string Name { get; set; }
        public string Value{ get; set; }
        public string Type { get; set; }
        public string Note { get; set; }

        public List<string> SettingType { get; set; }

        //public IEnumerable<Country> Countries { get; set; } 
    }
}

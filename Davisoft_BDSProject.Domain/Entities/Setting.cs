using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Setting : EntityBase
    {
        #region Primitive

        public string Module { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public string Summary { get; set; }
        public string TableToGetField { get; set; }
        public int MaxLenght { get; set; }
        public string Description { get; set; }


        #endregion

        #region Navigation


        public List<string> ListOfSettingType()
        {
            return null;
        }

        #endregion

        #region function
      
        #endregion

        #region define class and structure

        public enum ModuleType
        {
            SystemConfig,
        }

        public enum TypeOfValue
        {
            Textbox,
            Checkbox
        }

        public static Func<Setting, bool> IsActive()
        {
            return setting => true;
        }

        public class ModuleKey
        {
            public enum SystemConfig
            {
                LoginByEmail,
                Gst,
            }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Davisoft_BDSProject.Web.Models
{
    public class ResourceItemModel
    {
        public string LanguageCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsChange { get; set; }

    }
}
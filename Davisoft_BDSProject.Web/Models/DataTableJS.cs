using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Web.Models
{
    public class DataTableJS
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object data { get; set; }
        public string error { get; set; }
        public Dictionary<string, string> search { get; set; }
        public List<Dictionary<string, string>> order { get; set; }
        public List<Dictionary<string, object>> columns { get; set; }

    }

    
}

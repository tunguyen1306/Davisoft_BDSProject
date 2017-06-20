using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSMenu : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public int MOrder { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
    }
}

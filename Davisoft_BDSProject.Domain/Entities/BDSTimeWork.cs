using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSTimeWork : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public string KeyUrl { get; set; }
        public int FromTime { get; set; }
        public int ToTime { get; set; }
        public int Type { get; set; }
        public int? Perfix { get; set; }
        public int? Default { get; set; }
        public int? Index { get; set; }
    }
}

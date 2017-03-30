using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSSalary : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public int FromSalary { get; set; }
        public int ToSalary { get; set; }
        public int Type { get; set; }
        public int Perfix { get; set; }
        public int Default { get; set; }
    }
}

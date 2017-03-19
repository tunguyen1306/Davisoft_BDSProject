using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    class BDSNewsTypePrice : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdNewsType { get; set; }
        public double Price { get; set; }
        public DateTime ApplyPrice { get; set; }
        public int Perfix { get; set; }
    }
}

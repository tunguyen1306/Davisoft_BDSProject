using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSScope : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
    }
}

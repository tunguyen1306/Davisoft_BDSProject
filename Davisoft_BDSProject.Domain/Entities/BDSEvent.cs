using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    class BDSEvent : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public double DisPercent { get; set; }
        public int MultiApply { get; set; }
        public int TypeApply { get; set; }
    }
}

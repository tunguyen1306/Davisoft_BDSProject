using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSTransactionHistory : BDSBaseEntiry
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
  
        public int TypeTran { get; set; }
        public System.DateTime DateTran { get; set; }
        public int PointTran { get; set; }
        public double MoneyTran { get; set; }
        public int Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSTransaction : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public int IdAccount { get; set; }
        public int? IdBank { get; set; }
        public int? IdBranch { get; set; }
        public double Money { get; set; }
        public double MoneyEventAdd { get; set; }
        public double EventDisPercent { get; set; }
        public int Point { get; set; }
        public System.DateTime TranDate { get; set; }
        public int Type { get; set; }
        [ForeignKey("IdAccount")]
        public virtual BDSAccount BDSAccount { get; set; }
        [ForeignKey("IdBank")]
        public virtual BDSBank BDSBank { get; set; }
        [ForeignKey("IdBranch")]
        public virtual BDSBranch BDSBranch { get; set; }
    }
}

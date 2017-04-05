using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSBranch : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Phone { get; set; }
        public int IdArea { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        [ForeignKey("IdArea")]
        public virtual BDSArea BDSArea { get; set; }
    }
}

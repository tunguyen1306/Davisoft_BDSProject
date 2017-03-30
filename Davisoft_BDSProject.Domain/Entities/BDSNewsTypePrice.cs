using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSNewsTypePrice : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public int IdNewsType { get; set; }
       [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }
        public DateTime ApplyPrice { get; set; }
        public int Perfix { get; set; }
        [ForeignKey("IdNewsType")]
        public virtual BDSNewsType BDSNewsType { get; set; }
    }
}

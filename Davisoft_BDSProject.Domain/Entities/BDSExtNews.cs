using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSExtNews : BDSBaseEntiry
    {
        public string KeyUrl { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public int CatExtNews { get; set; }
        public int? IdAccount { get; set; }
        public System.DateTime DateCreate { get; set; }

        public System.DateTime? ApproveDate { get; set; }

        public int ApproveStatus { get; set; }

        public int? ApproveUser { get; set; }
        public string UrlImage { get; set; }
        [NotMapped]
        public string UrlImageFile { get; set; }
       
    }
}

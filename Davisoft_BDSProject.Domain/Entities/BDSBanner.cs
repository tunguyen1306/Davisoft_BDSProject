using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSBanner : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public int Type { get; set; }
        public string Position { get; set; }
        public string Page { get; set; }
       
        public string Banner { get; set; }

         [NotMapped]
        public string UrlImageFile { get; set; }

         public int BWidth { get; set; }

         public int BHeight { get; set; }
    }
}

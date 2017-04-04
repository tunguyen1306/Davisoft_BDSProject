using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSNewsType : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string CodeNewsType { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public string UrlIcon { get; set; }
        [NotMapped]
        public string UrlImageFile { get; set; }
    }
}

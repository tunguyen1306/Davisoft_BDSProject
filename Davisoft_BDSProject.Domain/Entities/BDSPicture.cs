using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
   public class BDSPicture:BDSBaseEntiry
    {
      
        public int? advert_id { get; set; }
        public string originalFilepath { get; set; }
        public Nullable<byte> position { get; set; }
        public Nullable<System.DateTime> converted { get; set; }
        public string convertedFilename { get; set; }
        public Nullable<bool> tocheck { get; set; }
        public Nullable<bool> isvalidated { get; set; }
        public string convertedFilename90 { get; set; }
        public string convertedFilename180 { get; set; }
        public string convertedFilename270 { get; set; }
        public Nullable<byte> angleType { get; set; }
        public Nullable<byte> type_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Nullable<int> cms_sql_id { get; set; }
        public string shortdescription { get; set; }
        [ForeignKey("advert_id")]
        public virtual BDSNew BDSNew { get; set; }
    }
}

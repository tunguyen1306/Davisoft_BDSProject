using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSPersonalInformation : BDSBaseEntiry
    {
   
        public int IdAccount { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public int Sex { get; set; }
        public int MaritalStatus { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        [NotMapped]
        public string UrlImageFile { get; set; }
        public string KeySearch { get; set; }
        public DateTime? DateReup { get; set; }
        public int? CountReup { get; set; }
        public int? MaxReup { get; set; }
        [ForeignKey("IdAccount")]
        public virtual BDSAccount BDSAccount { get; set; }
        
        [ForeignKey("MaritalStatus")]
        public virtual BDSMarriage BDSMarriage { get; set; }
        public int? Province { get; set; }
        public string PermanentAddress { get; set; }
        public string TemporaryAddress { get; set; }
    }
}

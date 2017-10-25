using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSEmployerInformation:BDSBaseEntiry
    {
        public int IdAccount { get; set; }
        public string Name { get; set; }
        public int Scope { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public int Featured { get; set; }
        [NotMapped]
        public string UrlImageFile { get; set; }
        public string Address { get; set; }
        public string FullAddress { get; set; }
        public string WebSite { get; set; }
        public int? City { get; set; }
        public int? District { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string NameContact { get; set; }
        public string EmailContact { get; set; }
        public string PhoneContact { get; set; }
        public string AddressContact { get; set; }
        public int? CityContact { get; set; }
        public int? DistrictContact { get; set; }
        public int TypeContact { get; set; }
       

        public string KeySearch { get; set; }

        [ForeignKey("IdAccount")]
        public virtual BDSAccount BDSAccount { get; set; }
        [ForeignKey("Scope")]
        public virtual BDSScope BDSScope { get; set; }
        [ForeignKey("TypeContact")]
        public virtual BDSTypeContact BDSTypeContact { get; set; }
    }
}

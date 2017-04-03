using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSPersonalInformation : BDSBaseEntiry
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public int Sex { get; set; }
        public string Address { get; set; }
        public int City { get; set; }
        public int District { get; set; }
        public string FullAddress { get; set; }
        public int MaritalStatus { get; set; }
        public int Salary { get; set; }
        public int Experience { get; set; }
        public int Education { get; set; }
        public int IdLoaiNghe { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string UrlImageFile { get; set; }

        public string KeySearch { get; set; }

        [ForeignKey("IdAccount")]
        public virtual BDSAccount BDSAccount { get; set; }
        [ForeignKey("Salary")]
        public virtual BDSSalary BDSSalary { get; set; }
        [ForeignKey("Education")]
        public virtual BDSEducation BDSEducation { get; set; }
        [ForeignKey("IdLoaiNghe")]
        public virtual BDSCareer BDSCareer { get; set; }
        [ForeignKey("MaritalStatus")]
        public virtual BDSMarriage BDSMarriage { get; set; }

    }
}

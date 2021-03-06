﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSNew : BDSBaseEntiry
    {
        public string Title { get; set; }
        public int AddressWork { get; set; }
        public int FromSalary { get; set; }
        public int ToSalary { get; set; }
        public int Quantity { get; set; }
        public string Bonus { get; set; }
        public int Sex { get; set; }
        public int ShowEmail { get; set; }
        public int IdTimeWork { get; set; }
        public int IdEducation { get; set; }
        public string Career { get; set; }
        [NotMapped]
        public int[] CareerID { get; set; }
        public string DesCompany { get; set; }
        public string DesWork { get; set; }
        public string Benefit { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public int TimeProbationary { get; set; }
        public string NameCompany { get; set; }
        public string AddressApply { get; set; }
        public string NamesContact { get; set; }
        public string PhoneContact { get; set; }
        public string Email { get; set; }
        public DateTime? FromDeadline { get; set; }
        public DateTime? ToDeadline { get; set; }
        public int IdLanguage { get; set; }
        public string WebSiteCompany { get; set; }
        public int IdTypeNews { get; set; }
        public DateTime? FromCreateNews { get; set; }
        public DateTime? ToCreateNews { get; set; }
        [NotMapped]
        public string FromDateToDateString { get; set; }
        public int IdAcount { get; set; }
        public string KeySearch { get; set; }
        public double MoneyInDay { get; set; }
        public double TotalMoney { get; set; }
        public int? IdNewsDates { get; set; }
        public String Reason { get; set; }
        public string UrlImage { get; set; }
        [NotMapped]
        public string UrlImageFile { get; set; }

        [ForeignKey("IdAcount")]
        public virtual BDSAccount BDSAccount { get; set; }

       
        public virtual ICollection<BDSPicture> BDSPictures { get; set; }


        [ForeignKey("IdEducation")]
        public virtual BDSEducation BDSEducation { get; set; }

        [ForeignKey("IdTimeWork")]
        public virtual BDSTimeWork BDSTimeWork { get; set; }

        [ForeignKey("IdTypeNews")]
        public virtual BDSNewsType BDSNewsType { get; set; }

        [ForeignKey("IdLanguage")]
        public virtual BDSLanguage BDSLanguage { get; set; }
        [ForeignKey("IdNewsDates")]
        public virtual BDSNewsDate BDSNewsDate { get; set; }
        [NotMapped]
        public virtual int IdPictrure { get; set; }
        public int IdTypeNewsCuurent { get; set; }
        public DateTime? DateReup { get; set; }
        public int? CountReup { get; set; }
        public int? MaxReup { get; set; }
        public int Status { get; set; }

        public int? RefTranHis { get; set; }

        public virtual ICollection<BDSCareer> BDSCareers { get; set; }
    }
}

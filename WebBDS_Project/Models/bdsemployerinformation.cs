//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBDS_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bdsemployerinformation
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public string Name { get; set; }
        public Nullable<int> Scope { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string Address { get; set; }
        public string FullAddress { get; set; }
        public string WebSite { get; set; }
        public int City { get; set; }
        public int District { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string NameContact { get; set; }
        public string EmailContact { get; set; }
        public string PhoneContact { get; set; }
        public string AddressContact { get; set; }
        public int CityContact { get; set; }
        public int DistrictContact { get; set; }
        public int TypeContact { get; set; }
    }
}

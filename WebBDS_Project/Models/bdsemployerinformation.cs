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
    
    public partial class BDSEmployerInformation
    {
        public int ID { get; set; }
        public int IdAccount { get; set; }
        public string Name { get; set; }
        public int Scope { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string Address { get; set; }
        public string FullAddress { get; set; }
        public string WebSite { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> District { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string NameContact { get; set; }
        public string EmailContact { get; set; }
        public string PhoneContact { get; set; }
        public string AddressContact { get; set; }
        public Nullable<int> CityContact { get; set; }
        public Nullable<int> DistrictContact { get; set; }
        public int TypeContact { get; set; }
        public string KeySearch { get; set; }
        public int Active { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedUser { get; set; }
        public Nullable<int> Featured { get; set; }
    }
}

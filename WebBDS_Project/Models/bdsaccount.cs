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
    
    public partial class BDSAccount
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string Token { get; set; }
        public Nullable<double> Money { get; set; }
        public Nullable<int> Point { get; set; }
        public Nullable<int> MailActive { get; set; }
        public string KeySearch { get; set; }
        public int Active { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedUser { get; set; }
    }
}

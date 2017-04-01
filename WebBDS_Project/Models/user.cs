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
    
    public partial class user
    {
        public user()
        {
            this.branches = new HashSet<branch>();
            this.roles = new HashSet<role>();
        }
    
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Picture { get; set; }
        public Nullable<System.DateTime> LastAccess { get; set; }
        public string Address { get; set; }
        public string AddressEnglish { get; set; }
        public string FaxNo { get; set; }
        public string Website { get; set; }
        public string Status_Value { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> LanguageID { get; set; }
    
        public virtual branch branch { get; set; }
        public virtual language language { get; set; }
        public virtual ICollection<branch> branches { get; set; }
        public virtual ICollection<role> roles { get; set; }
    }
}
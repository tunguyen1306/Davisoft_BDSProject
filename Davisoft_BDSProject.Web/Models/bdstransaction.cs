//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Davisoft_BDSProject.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bdstransaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public int Active { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedUser { get; set; }
        public Nullable<int> IdAccount { get; set; }
        public Nullable<int> IdBank { get; set; }
        public Nullable<decimal> Money { get; set; }
        public Nullable<decimal> MoneyEventAdd { get; set; }
        public Nullable<int> Point { get; set; }
        public Nullable<System.DateTime> TranDate { get; set; }
        public Nullable<int> Type { get; set; }
    }
}
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
    
    public partial class BDSEmper
    {
        public int Id { get; set; }
        public int IdAccountEm { get; set; }
        public int IdAccountPer { get; set; }
        public Nullable<int> Point { get; set; }
        public Nullable<int> Active { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public Nullable<int> RefTranHis { get; set; }
    }
}

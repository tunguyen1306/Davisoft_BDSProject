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
    
    public partial class bdssalary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> FromSalary { get; set; }
        public Nullable<int> ToSalary { get; set; }
        public int Type { get; set; }
        public int Perfix { get; set; }
        public short Default { get; set; }
        public string KeySearch { get; set; }
        public Nullable<int> Active { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedUser { get; set; }
    }
}

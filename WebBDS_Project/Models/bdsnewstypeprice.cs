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
    
    public partial class bdsnewstypeprice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int IdNewsType { get; set; }
        public decimal Price { get; set; }
        public Nullable<System.DateTime> ApplyPrice { get; set; }
        public Nullable<int> Perfix { get; set; }
        public string KeySearch { get; set; }
        public int Active { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedUser { get; set; }
    }
}
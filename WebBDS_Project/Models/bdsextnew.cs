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
    
    public partial class bdsextnew
    {
        public int Id { get; set; }
        public string KeyUrl { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public Nullable<int> Active { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedUser { get; set; }
        public Nullable<int> CatExtNews { get; set; }
        public Nullable<int> IdAccount { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<int> ApproveStatus { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public Nullable<int> ApproveUser { get; set; }
    }
}

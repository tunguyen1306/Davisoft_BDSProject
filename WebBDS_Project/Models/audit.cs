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
    
    public partial class audit
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string IPAddress { get; set; }
        public string UrlAccessed { get; set; }
        public System.DateTime TimeAccessed { get; set; }
        public string SessionId { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
        public string Controller { get; set; }
        public int ActionId { get; set; }
        public int Type { get; set; }
    }
}

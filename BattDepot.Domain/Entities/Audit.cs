using System;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Audit : EntityBase
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string IPAddress { get; set; }
        public string UrlAccessed { get; set; }
        public DateTime TimeAccessed { get; set; }
        public string SessionId { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
        public string Controller { get; set; }
        public int ActionId { get; set; }
        /*Type 0 : auto -- 1: manual*/
        public int Type { get; set; }
    }

    public enum AuditType
    {
        Auto,
        Manual,
        Exception
    }
}

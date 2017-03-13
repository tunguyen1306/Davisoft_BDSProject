using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Web.Models
{
    public class EmailReceiveSettingModel
    {
        public string Type { get; set; }
        public ICollection<EmailReceiveItem> EmailReceiveItems { get; set; }
    }

    public class EmailReceiveItem
    {
        public EmailReceiveItem()
        {
            EmailReceive = new EmailReceiveSetting();
            UserIDs = new List<int>();
            RoleIDs = new List<int>();
        }
        // file name template
        public EmailReceiveSetting EmailReceive { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<User> Users { get; set; }
        public List<int> UserIDs { get; set; }
        public List<int> RoleIDs { get; set; }
        public string EMailBody { get; set; }
    }
}
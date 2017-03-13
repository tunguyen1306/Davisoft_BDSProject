using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class EmailReceiveSetting : EntityBase
    {
        public EmailReceiveSetting()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            ReceiveUsers = new HashSet<ReceiveUser>();
            ReceiveRoles = new HashSet<ReceiveRole>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }
        //root folder contains template
        public string Type { get; set; }
        // file name template
        public string TemplateName { get; set; }

        public virtual ICollection<ReceiveUser> ReceiveUsers { get; set; }
        public virtual ICollection<ReceiveRole> ReceiveRoles { get; set; }
    }

    public class ReceiveUser : EntityBase
    {
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
    public class ReceiveRole : EntityBase
    {
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
    }
}

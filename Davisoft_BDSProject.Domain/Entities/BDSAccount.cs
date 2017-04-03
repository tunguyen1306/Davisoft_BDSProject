using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSAccount : BDSBaseEntiry
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
       [NotMapped]
        public string RePassWord { get; set; }
        public string Token { get; set; }
        public double? Money { get; set; }
        public int? Point { get; set; }
        public int? MailActive { get; set; }
        public string KeySearch { get; set; }

    }
}

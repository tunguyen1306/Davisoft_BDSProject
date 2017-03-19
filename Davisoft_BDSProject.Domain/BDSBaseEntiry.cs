using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain
{
    public class BDSBaseEntiry : EntityBase
    {
        public int Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        public BDSBaseEntiry()
        {
            Active = 1;
            CreateDate = DateTime.Now;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class EmailLog : EntityBase
    {
        public int BookingID { get; set; }
        public DateTime RequestDateTime { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}

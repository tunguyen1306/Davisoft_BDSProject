using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class Holiday : EntityBase
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Description { get; set; }
        public bool IsFullDay { get; set; }

        public int Year { get { return Date.Year; } }
        public int Month { get { return Date.Month; } }
        public int Day { get { return Date.Day; } }
    }
}

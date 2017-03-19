using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    class BDSBank : BDSBaseEntiry
    {
        public int IdArea { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string UrlImage { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }

        [ForeignKey("IdArea")]
        public virtual BDSBankArea BDSBankArea { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSNewsDate : BDSBaseEntiry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeySearch { get; set; }
        public int IdNews { get; set; }
        public int IdTypeNews { get; set; }
        public int DIndex { get; set; }
        public DateTime? FromCreateNews { get; set; }
        public DateTime? ToCreateNews { get; set; }
        public int Status { get; set; }

        [ForeignKey("IdTypeNews")]
        public virtual BDSNewsType BDSNewsType { get; set; }

    

       
    }
}

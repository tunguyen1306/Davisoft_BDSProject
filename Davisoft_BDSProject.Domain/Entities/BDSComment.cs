using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davisoft_BDSProject.Domain.Entities
{
    public class BDSComment : BDSBaseEntiry
    {
       
    
            public string Name { get; set; }
            public string Description { get; set; }
            public string KeySearch { get; set; }
      
            public int? IdAccount { get; set; }
            public System.DateTime?   DateComment { get; set; }
            public string TextComment { get; set; }
            public int? Status { get; set; }
            public int?   ApproveUser { get; set; }
            public string NameComment { get; set; }
            public string DescriptionComment { get; set; }
            public string CompanyComment { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBDS_Project.Models
{
    public class BdsAcountModel
    {
        public bdsemployerinformation tblbdsemployerinformation { get; set; }
        public List<bdsemployerinformation> Listbdsemployerinformation { get; set; }
        public bdsaccount tblbdsaccount { get; set; }
        public List<bdsaccount> Listbdsaccount { get; set; }
        public bdspersonalinformation tblbdsbdspersonalinformation { get; set; }
        public List<bdspersonalinformation> Listbdsbdspersonalinformation { get; set; }

    }
}
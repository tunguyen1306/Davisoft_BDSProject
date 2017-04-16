using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBDS_Project.Models
{
    public class BdsAcountModel
    {
        public BDSEmployerInformation tblBDSEmployerInformation { get; set; }
        public List<BDSEmployerInformation> ListBDSEmployerInformation { get; set; }
        public BDSAccount tblBDSAccount { get; set; }
        public List<BDSAccount> ListBDSAccount { get; set; }
        public BDSPersonalInformation tblbdsBDSPersonalInformation { get; set; }
        public List<BDSPersonalInformation> ListbdsBDSPersonalInformation { get; set; }

    }
}
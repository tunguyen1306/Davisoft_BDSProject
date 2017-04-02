﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBDS_Project.Models
{
    public class RegisterInformationModel
    {
        public bdspersonalinformation TblBdspersonalinformation { get; set; }
        public bdsaccount TbBdsAdcount { get; set; }
        public bdsemployerinformation TblBdsemployerinformation { get; set; }
        public List<bdsscope> ListBdsScopes { get; set; }
        public List<state> ListStates { get; set; }
        public List<statetext> ListStatetexts { get; set; }
        public List<district> ListDistrict { get; set; }
        public List<districttext> ListDistricttext { get; set; }
        public List<GeoModel> ListGeoModel { get; set; }
        public List<bdsmarriage> ListMarriea{ get; set; }
        public bdsmarriage tblMarriea { get; set; }

        public List<bdssalary> ListSalary { get; set; }
        public bdssalary tblSalary { get; set; }

        public List<bdseducation> ListDucation { get; set; }
        public bdseducation tblDucation { get; set; }

        public List<bdscareer> ListBdscareer { get; set; }
        public bdscareer tblBdscareer { get; set; }
        public List<bdstimework> ListTimework { get; set; }
        public bdstimework tbltimework { get; set; }
        public bdsnew tblbdsnew { get; set; }
        public bdsnewstype tblbdsnewstype { get; set; }
        public bdsnewstypeprice tblbdsnewstypeprice { get; set; }
    }
}
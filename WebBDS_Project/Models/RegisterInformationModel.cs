using System;
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
    }
}
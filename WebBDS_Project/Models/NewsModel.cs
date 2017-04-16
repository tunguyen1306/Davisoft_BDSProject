using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBDS_Project.Models
{
    public class NewsModel
    {
        public List<BDSPicture> ListPicture { get; set; }
        public BDSPicture tblPicture { get; set; }
        public BDSNew tblBDSNew { get; set; }
        public List<BDSNew> ListNew { get; set; }
                public BDSEmployerInformation tblBDSEmployerInformation { get; set; }
        public List<BDSEmployerInformation> ListBDSEmployerInformation { get; set; }
                public BDSAccount tblBDSAccount { get; set; }
        public List<BDSAccount> ListBDSAccount { get; set; }
       
      
        public List<BDSScope> ListBDSScopes { get; set; }
        public List<State> ListStates { get; set; }
        public List<StateText> ListStateTexts { get; set; }
        public List<District> ListDistrict { get; set; }
        public List<DistrictText> ListDistrictText { get; set; }
        public List<GeoModel> ListGeoModel { get; set; }
        public List<BDSMarriage> ListMarriea { get; set; }
        public BDSMarriage tblMarriea { get; set; }

        public List<BDSSalary> ListSalary { get; set; }
        public BDSSalary tblSalary { get; set; }

        public List<BDSEducation> ListDucation { get; set; }
        public BDSEducation tblDucation { get; set; }

        public List<BDSCareer> ListBDSCareer { get; set; }
        public BDSCareer tblBDSCareer { get; set; }
        public List<BDSTimeWork> ListTimework { get; set; }
        public BDSTimeWork tbltimework { get; set; }
      

        public BDSNewsType tblBDSNewsType { get; set; }
        public List<BDSNewsType> ListBDSNewsType { get; set; }

        public BDSLanguage tblBDSLanguage { get; set; }
        public List<BDSLanguage> ListBDSLanguage { get; set; }

        public BDSNewsTypePrice tblBDSNewsTypePrice { get; set; }
        public CaptCha tblCaptCha { get; set; }

        public List<ListCityNew> ListCityText { get; set; }
       



    }

    public class ListCityNew
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
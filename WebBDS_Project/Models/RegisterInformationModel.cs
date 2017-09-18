using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBDS_Project.Models
{
    [Serializable]
    public class RegisterInformationModel
    {
        public BDSPersonalInformation TblBDSPersonalInformation { get; set; }
        public BDSAccount TblBdsAdcount { get; set; }
        public List<BDSAccount> ListBdsAdcount { get; set; }
        public BDSEmployerInformation TblBDSEmployerInformation { get; set; }
        public List< BDSEmployerInformation> ListBDSEmployerInformation { get; set; }
        public List<BDSNew> ListBDSNew { get; set; }
        public List<BDSPersonalInformation> ListBDSPersonalInformation { get; set; }
        public List<BDSScope> ListBDSScopes { get; set; }
        public List<State> ListStates { get; set; }
        public List<StateText> ListStateTexts { get; set; }
        public List<District> ListDistrict { get; set; }
        public List<DistrictText> ListDistrictText { get; set; }
        public List<GeoModel> ListGeoModel { get; set; }
        public List<GeoModel> ListGeoDisModel { get; set; }
        public List<BDSMarriage> ListMarriea{ get; set; }
        public List<BDSTransactionHistory> ListBdsTransactionHistory { get; set; }
        public BDSTransactionHistory TblBdsTransactionHistory { get; set; }
        public BDSMarriage tblMarriea { get; set; }

        public List<BDSSalary> ListSalary { get; set; }
        public BDSSalary tblSalary { get; set; }

        public List<BDSEducation> ListDucation { get; set; }
        public BDSEducation tblDucation { get; set; }

        public List<BDSCareer> ListBDSCareer { get; set; }
        public BDSCareer tblBDSCareer { get; set; }
        public List<BDSTimeWork> ListTimework { get; set; }
        public BDSTimeWork tbltimework { get; set; }
        public BDSNew tblBDSNew { get; set; }

        public BDSNewsType tblBDSNewsType { get; set; }
        public List<BDSNewsType> ListBDSNewsType { get; set; }

        public BDSLanguage tblBDSLanguage { get; set; }
        public List<BDSLanguage> ListBDSLanguage { get; set; }

        public BDSNewsTypePrice tblBDSNewsTypePrice { get; set; }
        public CaptCha tblCaptCha { get; set; }

        public List<ListCity>  ListCityText { get; set; }

        public BDSEmper tblBDSEmper { get; set; }
        public List<BDSEmper> ListBDSEmper { get; set; }

        public BDSApply tblBDSApply { get; set; }
        public List<BDSApply> ListBDSApply { get; set; }
        public BDSPicture tblBDSPicture { get; set; }
        public List<BDSPicture> ListBDSPicture { get; set; }
        public BDSPerNew tblBDSPerNew { get; set; }
        public List<BDSPerNews_Degrees> ListPerNewDegrees { get; set; }
        public List<BDSPerNews_Experiences> ListPerNewExperiences { get; set; }
        public List<BDSPerNews_LangDegrees> ListPerNewLangDegrees { get; set; }
        public List<BDSPerNews_References> ListPerNewReferences { get; set; }

        public string Msg { get; set; }
        public bool Status { get; set; }
    }

    public class ListCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Davisoft_BDSProject.Web.Models
{
    public class LanguageResourceModel
    {
        public List<XmlNode> Resources { get; set; }
        public string PathResx1 { get; set; }
        public string PathResx2 { get; set; }
        public string LanguageCode { get; set; }
        public LanguageResourceModel(string path1, string path2, string code = null)
        {
            LanguageCode = code;
            var xmlDoc2 = new XmlDocument();
            if (!System.IO.File.Exists(path1))
            {
                System.IO.File.Copy(HttpContext.Current.Server.MapPath("~/App_GlobalResources/Resource.resx"), path1);
            }
            xmlDoc2.Load(path1);
            var t2 = xmlDoc2.GetElementsByTagName("data");
            var rList = t2.Cast<XmlNode>().ToList();

            var xmlDoc3 = new XmlDocument();
            if (!System.IO.File.Exists(path2))
            {
                System.IO.File.Copy(HttpContext.Current.Server.MapPath("~/App_GlobalResources/MenuResource.resx"), path2);
            }
            xmlDoc3.Load(path2);
            var t3 = xmlDoc3.GetElementsByTagName("data");
            rList.AddRange(t3.Cast<XmlNode>());

            this.Resources = rList;
            this.PathResx1 = path1;
            this.PathResx2 = path2;
        }
    }
}
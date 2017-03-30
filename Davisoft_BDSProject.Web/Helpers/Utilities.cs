using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Web;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using Davisoft_BDSProject.Web.Models;
using MultiLanguage;
using WebGrease.Css.Extensions;

namespace Davisoft_BDSProject.Web.Helpers
{
    public static class Utilities
    {
        public static ICollection<ResourceModel> ResourceModels = null;

        public static void UpdatedResource()
        {
            var model = new ResourceViewModel();
            model.Prepare();
            ResourceModels = model.ResourceModels;
          
        }
        public static string Resource(string key, String defStr)
        {

            try
            {
                var model = new ResourceViewModel();
                if (ResourceModels == null)
                {
                    model.Prepare();
                    ResourceModels = model.ResourceModels;
                }
                 String lang = SessionManager.CurrentCulture;
                try
                {
                     lang = (CurrentUser.Identity != null
                  ? CurrentUser.Identity.Language.Value
                  : SessionManager.CurrentCulture);
                }
                catch(Exception e)
                {

                }
              
                var item = ResourceModels.Where(m =>
                        m.Code.ToLower() == key.ToLower()).FirstOrDefault();
                if (item != null && item.Resources.Where(T => lang.IndexOf(T.LanguageCode) == 0).FirstOrDefault() != null)
                {
                    defStr = item.Resources.Where(T => lang.IndexOf(T.LanguageCode) == 0).FirstOrDefault().Description;
                }
                else
                {

                    String path = HttpContext.Current.Server.MapPath("~/App_GlobalResources/Resource{0}.resx");
                    if (model.Languages == null)
                    {
                        model.PrepareLanguages();
                    }
                    model.Languages.ForEach(T =>
                    {

                        model.EditAnyResourceByLanguage(new List<ResourceItemModel>{new ResourceItemModel
                    {
                        Code = key,
                        Description = defStr,
                        IsChange = true,
                        LanguageCode = T.Value
                    }}, String.Format(path, T.Value.IndexOf("en") > -1 ? "" : "." + T.Value));

                    });
                    UpdatedResource();
                }
          

            }
            catch (Exception)
            {
                
              
            }
            
          
            return defStr;

        }
        public static string Highlight(this string str, string hl, bool any)
        {
            if (hl.Trim().Length==0)
            {
                return str;
            }
            String strTem = str.ToUpper();
            if (any)
            {
               
                int begin = strTem.IndexOf(hl.ToUpper());
                if (begin>-1)
                {
                    int begin2 = begin + hl.Length;
                    hl = str.Substring(begin, hl.Length);
                    str = str.Substring(0, begin) + "<b class='highlight'>" + hl + "</b>" + str.Substring(begin2, str.Length - begin2);
                }
               
            }
            else
            {
                int begin = str.IndexOf(hl);
                if (begin>-1)
                {
                    int begin2 = begin + hl.Length;
                    hl = str.Substring(begin, hl.Length);
                    str = str.Substring(0, begin) + "<b class='highlight'>" + hl + "</b>" + str.Substring(begin2, str.Length - begin2);
                }
                
            }
            return str;
        }
    }
}
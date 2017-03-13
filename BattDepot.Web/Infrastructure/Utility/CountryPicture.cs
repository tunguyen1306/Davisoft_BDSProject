using System;
using System.IO;
using System.Web;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class CountryPicture
    {
        public static string Upload(int countryId, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = countryId + Path.GetExtension(file.FileName);
                    string filePath = HttpContext.Current.Server.MapPath(GetImagePath(countryId, fileName));

                    string directory = Path.GetDirectoryName(filePath);
                    if (directory != null) Directory.CreateDirectory(directory);

                    file.SaveAs(filePath);

                    return fileName;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public static bool Delete(int countryId, string fileName)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(GetImagePath(countryId, fileName));
                File.Delete(path);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected static string GetImagePath(int countryId, string fileName)
        {
            return "~/data/country_img/" + fileName;
        }
    }
}

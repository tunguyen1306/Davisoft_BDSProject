using System;
using System.IO;
using System.Web;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class PdiPicture
    {
        public static string Upload(string folderId, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string filePath = HttpContext.Current.Server.MapPath(GetImagePath(folderId, file.FileName));
                try
                {
                    string directory = Path.GetDirectoryName(filePath);
                    if (directory != null) Directory.CreateDirectory(directory);
                    file.SaveAs(filePath);
                }
                catch (Exception ex)
                {
                }
                return file.FileName;
            }
            return null;
        }

        protected static string GetImagePath(string folder, string fileName)
        {
            return "~/data/pdidelivery/" + folder + "/" + fileName;
        }
    }
}

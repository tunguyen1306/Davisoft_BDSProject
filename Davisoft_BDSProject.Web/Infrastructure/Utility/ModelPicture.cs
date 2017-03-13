using System;
using System.IO;
using System.Web;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class ModelPicture
    {
        public static string Upload(int modelId, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = modelId + Path.GetExtension(file.FileName);
                    string filePath = HttpContext.Current.Server.MapPath(GetImagePath(modelId, fileName));

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

        public static bool Delete(int modelId, string fileName)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(GetImagePath(modelId, fileName));
                File.Delete(path);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected static string GetImagePath(int modelId, string fileName)
        {
            return "~/data/model_img/" + fileName;
        }
    }
}
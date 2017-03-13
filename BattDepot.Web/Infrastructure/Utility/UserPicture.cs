using System;
using System.IO;
using System.Web;
using Davisoft_BDSProject.Web.Infrastructure.Helpers;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class UserPicture
    {
        public static string Upload(int userId, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = userId + Path.GetExtension(file.FileName);
                    string thumbName = userId + "_400x400" + Path.GetExtension(file.FileName);
                    string filePath = HttpContext.Current.Server.MapPath(GetImagePath(fileName));

                    string directory = Path.GetDirectoryName(filePath);
                    if (directory != null) Directory.CreateDirectory(directory);
                    file.SaveAs(filePath);
                    ImageHelper.CreateImageHighQuality(GetfolderPath(), GetfolderPath(), fileName, thumbName, ImageHelper.MaxHeightUserAvatar, ImageHelper.MaxWidthUserAvatar);

                    return thumbName;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public static bool Delete(int userId, string fileName)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(GetImagePath(fileName));
                File.Delete(path);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetfolderPath()
        {
            return HttpContext.Current.Server.MapPath("~/data/user_img/");
        }
        protected static string GetImagePath(string fileName)
        {
            return "~/data/user_img/" + fileName;
        }
    }
}

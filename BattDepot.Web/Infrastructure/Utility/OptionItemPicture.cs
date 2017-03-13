using System;
using System.IO;
using System.Web;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class OptionItemPicture
    {
        public static string Upload(int id, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = id + Path.GetExtension(file.FileName);
                    string filePath = HttpContext.Current.Server.MapPath(GetImagePath(id, fileName));

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

        public static bool Delete(int id, string fileName)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(GetImagePath(id, fileName));
                File.Delete(path);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected static string GetImagePath(int id, string fileName)
        {
            return "~/data/optionitem_img/" + fileName;
        }
    }
    public class PromotionItemPicture
    {
        public static string Upload(int id, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = id + Path.GetExtension(file.FileName);
                    string filePath = HttpContext.Current.Server.MapPath(GetImagePath(id, fileName));

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

        public static bool Delete(int id, string fileName)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(GetImagePath(id, fileName));
                File.Delete(path);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected static string GetImagePath(int id, string fileName)
        {
            return "~/data/promotionitem_img/" + fileName;
        }
    }
}
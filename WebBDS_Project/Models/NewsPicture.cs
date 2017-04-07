using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebBDS_Project.Models
{
    public class NewsPicture
    {
        public List<bdspicture> ListPicture { get; set; }
        public bdspicture tblPicture { get; set; }
        public bdsnew tblbdsnew { get; set; }
        public List<bdsnew> ListNew { get; set; }




        public int idProducts { get; set; }
        public string cfile { get; set; }
        public string nameImg { get; set; }
        public byte isactive { get; set; }
        public int idpicture { get; set; }
        public int index { get; set; }

        public string GetFilePathPhysical(PictureSize size)
        {
            // check if we have converted files
            //if (IsConverted)
            return DirectoryPhysical + FileName(size);
            //else
            //    return tblPicture.originalFilepath;
        }
        public enum PictureSize : int
        {
            Original = 853, // The summ of ascii values of the word "original"
            Large = 1,
            Medium = 2,
            Small = 3,
            Tiny = 4
        }
        public string DirectoryPhysical
        {
            get { return "~/fileUpload"; }
        }
        public string FileName(PictureSize size)
        {
            // check if we have converted files
            //if (IsConverted)
            //{
            switch (AngelType)
            {
                case RotationAngle.Rotated0:
                    return string.Format(tblPicture.convertedFilename, (int)size);
                    break;

                case RotationAngle.Rotated90:
                    if (!string.IsNullOrWhiteSpace(tblPicture.convertedFilename90))
                        return string.Format(tblPicture.convertedFilename90, (int)size);
                    break;

                case RotationAngle.Rotated180:
                    if (!string.IsNullOrWhiteSpace(tblPicture.convertedFilename180))
                        return string.Format(tblPicture.convertedFilename180, (int)size);
                    break;

                case RotationAngle.Rotated270:
                    if (!string.IsNullOrWhiteSpace(tblPicture.convertedFilename270))
                        return string.Format(tblPicture.convertedFilename270, (int)size);
                    break;
            }

            return "";
            //}
            //else
            //{
            //    if (tblPicture.originalFilepath.StartsWith("http:", StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        return Path.GetFileName(tblPicture.originalFilepath);
            //    }
            //    return tblPicture.originalFilepath;
            //}
        }
        //public bool IsConverted
        //{
        //    get
        //    {
        //        switch (AngelType)
        //        {
        //            default:
        //                return !string.IsNullOrWhiteSpace(tblPicture.convertedFilename);

        //            case RotationAngle.Rotated90:
        //                return !string.IsNullOrWhiteSpace(tblPicture.convertedFilename90);

        //            case RotationAngle.Rotated180:
        //                return !string.IsNullOrWhiteSpace(tblPicture.convertedFilename180);

        //            case RotationAngle.Rotated270:
        //                return !string.IsNullOrWhiteSpace(tblPicture.convertedFilename270);
        //        }
        //    }
        //}
        public RotationAngle AngelType
        {
            get { return (RotationAngle)AngelTypeId; }
            set { AngelTypeId = (int)value; }
        }
        public enum RotationAngle : int
        {
            Rotated0 = 0,
            Rotated90 = 1,
            Rotated180 = 2,
            Rotated270 = 3,
        }
        public int AngelTypeId { get; set; }
        public string GetConvertedFileName()
        {
            // check if we have converted files
            //if (IsConverted)
            //{
            switch (AngelType)
            {
                case RotationAngle.Rotated0:
                    return tblPicture.convertedFilename;
                    break;

                case RotationAngle.Rotated90:
                    return tblPicture.convertedFilename90;
                    break;

                case RotationAngle.Rotated180:
                    return tblPicture.convertedFilename180;
                    break;

                case RotationAngle.Rotated270:
                    return tblPicture.convertedFilename270;
                    break;
            }

            return null;
            //}
            //else
            //    return null;
        }
        public string SetFileName(string filenamePattern)
        {
            // check if we have converted files

            switch (AngelType)
            {
                case RotationAngle.Rotated0:
                    tblPicture.convertedFilename = filenamePattern;
                    break;

                case RotationAngle.Rotated90:
                    tblPicture.convertedFilename90 = filenamePattern;
                    break;

                case RotationAngle.Rotated180:
                    tblPicture.convertedFilename180 = filenamePattern;
                    break;

                case RotationAngle.Rotated270:
                    tblPicture.convertedFilename270 = filenamePattern;
                    break;
            }

            return "";
        }
        public string CreateFilename()
        {
            string encoded = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            encoded = encoded.Replace("/", "_").Replace("+", "-");
            return encoded.Substring(0, 22);
        }
        public string GetFilePath(PictureSize size)
        {
            // check if we have converted files

            return FileName(size);

        }
        public WatermarkType WaterMarkLarge { get; set; }
        public enum WatermarkType
        {
            [Description("none")]
            None = 0,
            [Description("image")]
            Image,
            [Description("text")]
            Text
        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public class ImageHelper
    {
        public const int MaxWidthStockUpload = 1024;
        public const int MaxHeightStockUpload = 1024;
        public const int MaxWidthThumbnailUpload = 400;
        public const int MaxHeightThumbnailUpload = 300;
        public const int MaxWidthUserAvatar = 400;
        public const int MaxHeightUserAvatar = 400;
        public static void CreateImageHighQuality(string sOriginalPath, string sPhysicalPath, string sOrgFileName, string sTargetFileName, int maxHeight, int maxWidth)
        {
            try
            {
                var oImg = Image.FromFile(sOriginalPath + @"\" + sOrgFileName);
                int imgWidth = GetScaleFactorWidth(oImg, maxHeight, maxWidth);
                int imgHeight = GetScaleFactorHeight(oImg, maxHeight, maxWidth);

                Image oThumbNail = new Bitmap(imgWidth, imgHeight, oImg.PixelFormat);
                Graphics oGraphic = Graphics.FromImage(oThumbNail);
                oGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var oRectangle = new Rectangle(0, 0, imgWidth, imgHeight);
                oGraphic.DrawImage(oImg, oRectangle);
                var targetPath = sPhysicalPath + @"\" + sTargetFileName;
                oThumbNail.Save(targetPath);
                oImg.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
        public static void CreateImage60PercentQuality(string imageType, string sOriginalPath, string sPhysicalPath, string sOrgFileName, string sTargetFileName, int maxHeight, int maxWidth)
        {
            try
            {
                var oImg = Image.FromFile(sOriginalPath + @"\" + sOrgFileName);
                int imgWidth = GetScaleFactorWidth(oImg, maxHeight, maxWidth);
                int imgHeight = GetScaleFactorHeight(oImg, maxHeight, maxWidth);

                Image oThumbNail = new Bitmap(imgWidth, imgHeight, oImg.PixelFormat);
                Graphics oGraphic = Graphics.FromImage(oThumbNail);
                oGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                var oRectangle = new Rectangle(0, 0, imgWidth, imgHeight);
                oGraphic.DrawImage(oImg, oRectangle);
                var targetPath = sPhysicalPath + @"\" + sTargetFileName;
                SaveJpeg(targetPath, oThumbNail, 60, imageType);
                //oThumbNail.Save(sPhysicalPath + @"\" + sTargetFileName);
                oImg.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
        public static void SaveJpeg(string path, Image img, int quality, string mimeType)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo(mimeType);
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        } 
        public static Image GetImage(string path)
        {
            try
            {
                return Image.FromFile(path);
            }
            catch (Exception ex)
            {
            }
            return null;
        }



        static int GetScaleFactorWidth(System.Drawing.Image currentImage, int maxHeight, int maxWidth)
        {
            int imgWidth = currentImage.Width;
            int imgHeight = currentImage.Height;
            double scaleFactor = 0.0;
            if ((imgWidth > maxWidth) || (imgHeight > maxHeight))
            {
                if ((maxHeight / imgHeight) > (maxWidth / imgWidth))
                {
                    scaleFactor = maxHeight / Convert.ToDouble(imgHeight);
                }
                else
                {
                    scaleFactor = maxWidth / Convert.ToDouble(imgWidth);
                }
            }
            if (imgWidth > maxWidth)
            {
                scaleFactor = maxWidth / Convert.ToDouble(imgWidth);
                imgWidth = System.Convert.ToInt32(imgWidth * scaleFactor);
                imgHeight = System.Convert.ToInt32(imgHeight * scaleFactor);
            }
            if (imgHeight > maxHeight)
            {
                imgWidth = System.Convert.ToInt32(imgWidth / scaleFactor);
                imgHeight = System.Convert.ToInt32(imgHeight / scaleFactor);
                scaleFactor = maxHeight / Convert.ToDouble(imgHeight);
                imgWidth = System.Convert.ToInt32(imgWidth * scaleFactor);
                imgHeight = System.Convert.ToInt32(imgHeight * scaleFactor);
            }
            return imgWidth;
        }

        static int GetScaleFactorHeight(System.Drawing.Image currentImage, int maxHeight, int maxWidth)
        {
            int imgWidth = currentImage.Width;
            int imgHeight = currentImage.Height;
            double scaleFactor = 0.0;
            if ((imgWidth > maxWidth) || (imgHeight > maxHeight))
            {
                if ((maxHeight / imgHeight) > (maxWidth / imgWidth))
                {
                    scaleFactor = maxHeight / Convert.ToDouble(imgHeight);
                }
                else
                {
                    scaleFactor = maxWidth / Convert.ToDouble(imgWidth);
                }
            }
            if (imgWidth > maxWidth)
            {
                scaleFactor = maxWidth / Convert.ToDouble(imgWidth);
                imgWidth = System.Convert.ToInt32(imgWidth * scaleFactor);
                imgHeight = System.Convert.ToInt32(imgHeight * scaleFactor);
            }
            if (imgHeight > maxHeight)
            {
                imgWidth = System.Convert.ToInt32(imgWidth / scaleFactor);
                imgHeight = System.Convert.ToInt32(imgHeight / scaleFactor);
                scaleFactor = maxHeight / Convert.ToDouble(imgHeight);
                imgWidth = System.Convert.ToInt32(imgWidth * scaleFactor);
                imgHeight = System.Convert.ToInt32(imgHeight * scaleFactor);
            }
            return imgHeight;
        }
    }
}
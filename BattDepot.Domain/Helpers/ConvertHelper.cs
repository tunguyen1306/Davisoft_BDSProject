using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using Cyotek.GhostScript;
using Humanizer;

namespace Davisoft_BDSProject.Domain.Helpers
{
    public class ConvertHelper
    {

        public static Bitmap ConvertToImage(string filename)
        {
            Bitmap firstPage = new Pdf2Image(filename).GetImage();
            return firstPage;
        }

        public static String ConvertDecimalToString(decimal input)
        {
            var array = input.ToString().Split('.');
            return Convert.ToInt32(array[0]).ToWords() + " " + (Convert.ToInt32(array[1]) != 0 ? ("and cents " +Convert.ToInt32(array[1]).ToWords() + " ") : "") + "only";
        }
    }
}

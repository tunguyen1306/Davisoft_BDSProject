using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Davisoft_BDSProject.Domain.Helpers
{
    public static class EncryptHelper
    {
        public static string EncryptPassword(string rawText)
        {
            return string.Join("", MD5.Create()
                                      .ComputeHash(Encoding.ASCII.GetBytes(rawText))
                                      .Select(s => s.ToString("x2")));
        }
        public static String SimpleEncrypt(String text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            String encrypt = "";
            foreach (char value in text)
            {
                int ascii = ((int)value + 1);
                if (ascii == 123) ascii = 97;
                encrypt += (char)(ascii);
            }
            return encrypt;
        }

        public static String SimpleDecrypt(String text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            String decrypt = "";
            foreach (char value in text)
            {
                int ascii = ((int)value - 1);
                if (ascii == 96) ascii = 122;
                decrypt += (char)(ascii);
            }
            return decrypt;
        }
    }
}

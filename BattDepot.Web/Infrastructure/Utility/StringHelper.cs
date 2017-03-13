using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Davisoft_BDSProject.Web.Infrastructure.Utility
{
    public class StringHelper
    {
        public static string SubString(string str, int index, int len)
        {
            int l = str.Length;
            int maxlen = index + len > l ? l - index : len;
            return str.Substring(index, maxlen);
        }

        public static string Ensure(object obj)
        {
            if (obj == null)
                return string.Empty;

            return System.Convert.ToString(obj);
        }

        public static decimal DecNormalize(decimal value)
        {
            return value/1.000000000000000000000000000000000m;
        }

        public static string NumNormalize(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) return "";

            const string f = @"[^0-9.]?";
            var result = System.Text.RegularExpressions.Regex.Replace(s, f, "");
            result = result.TrimStart('0', '.').TrimEnd('.');

            var pre = 0;
            if (result.Count(j => j == '.') > 1)
            {
                var i = result.IndexOf('.');
                result = System.Text.RegularExpressions.Regex.Replace(result, @"[^0-9]?", "");
                result = result.Insert(i, ".").TrimEnd('0');
                pre = result.Length - 1 - i;
            }

            result = double.Parse(result).ToString("N" + pre);
            return result;
        }

        public static string EncodeDecode(object _data, bool encode)
        {
            var data = _data.ToString();
            if (encode)
                try
                {
                    byte[] encDataByte = Encoding.UTF8.GetBytes(data);
                    var encodedData = Convert.ToBase64String(encDataByte);
                    return encodedData;
                }
                catch
                {
                    return "";
                }
            try
            {
                var encoder = new UTF8Encoding();
                var utf8Decode = encoder.GetDecoder();

                var todecodeByte = Convert.FromBase64String(data);
                var charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                var decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                var result = new String(decodedChar);
                return result;
            }
            catch
            {
                return "";
            }
        }

        public static T Encrypt<T>(object _toEncrypt, bool isEncrypt)
        {
            var toEncrypt = _toEncrypt.ToString();
            const string securitykey = "3ecur1tyKey";
            if (isEncrypt)
            {
                var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

                var hashmd5 = new MD5CryptoServiceProvider();
                var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(securitykey));
                hashmd5.Clear();

                var tdes = new TripleDESCryptoServiceProvider
                               {
                                   Key = keyArray,
                                   Mode = CipherMode.ECB,
                                   Padding = PaddingMode.PKCS7
                               };

                var cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();

                var value = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                return (T) Convert.ChangeType(value, typeof (T));
            }
            else
            {
                var toEncryptArray = Convert.FromBase64String(toEncrypt);

                var hashmd5 = new MD5CryptoServiceProvider();
                var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(securitykey));
                hashmd5.Clear();

                var tdes = new TripleDESCryptoServiceProvider
                               {
                                   Key = keyArray,
                                   Mode = CipherMode.ECB,
                                   Padding = PaddingMode.PKCS7
                               };

                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdes.Clear();

                var value = Encoding.UTF8.GetString(resultArray);
                return (T) Convert.ChangeType(value, typeof (T));
            }
        }

        public static string GetExcelConnection(string strFilePath)
        {
            string strConn;
            if (strFilePath.Substring(strFilePath.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath +
                          ";Extended Properties=\"Excel 12.0;IMEX=1\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath +
                          ";Extended Properties=\"Excel 8.0;IMEX=1\"";
            return strConn;
        }
        
        public class NameValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }

        public static bool Equals(string input1, string input2)
        {
            return input1.Trim().ToUpper() == input2.Trim().ToUpper();
        }
        
    }
}

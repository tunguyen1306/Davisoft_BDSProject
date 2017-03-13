using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Humanizer;
using ServiceStack;

namespace Davisoft_BDSProject.Domain.Helpers
{
    public static class StringHelper
    {
        public static bool CaseInsensitiveEquals(this string str1, string str2)
        {
            if (String.IsNullOrWhiteSpace(str1) && String.IsNullOrWhiteSpace(str2))
                return true;

            return str1.Trim().Equals(str2.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public static bool CaseInsensitiveContains(this IEnumerable<string> arr, string pattern)
        {
            return arr.Any(s => s.CaseInsensitiveEquals(pattern));
        }

        public static string[] Split(this string str, char separator, bool removeEmptyEntries)
        {
            StringSplitOptions options = removeEmptyEntries
                                             ? StringSplitOptions.RemoveEmptyEntries
                                             : StringSplitOptions.None;
            return str.Split(new[] { separator }, options);
        }

        public static string EncodeDecode(object _data, bool encode)
        {
            string data = _data.ToString();
            if (encode)
                try
                {
                    byte[] encDataByte = Encoding.UTF8.GetBytes(data);
                    string encodedData = Convert.ToBase64String(encDataByte);
                    return encodedData;
                }
                catch
                {
                    return "";
                }
            try
            {
                var encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecodeByte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
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

        public static byte[] ConvertByte(string input)
        {
            return Encoding.ASCII.GetBytes(input);
        }

        public static string ConvertFromByte(byte[] input)
        {
            return Encoding.ASCII.GetString(input);
        }

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

            return Convert.ToString(obj);
        }

        public static decimal DecNormalize(decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }

        public static string NumNormalize(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) return "";

            const string f = @"[^0-9.]?";
            string result = Regex.Replace(s, f, "");
            result = result.TrimStart('0', '.').TrimEnd('.');

            int pre = 0;
            if (result.Count(j => j == '.') > 1)
            {
                int i = result.IndexOf('.');
                result = Regex.Replace(result, @"[^0-9]?", "");
                result = result.Insert(i, ".").TrimEnd('0');
                pre = result.Length - 1 - i;
            }

            result = double.Parse(result).ToString("N" + pre);
            return result;
        }

        public static T Encrypt<T>(object _toEncrypt, bool isEncrypt)
        {
            string toEncrypt = _toEncrypt.ToString();
            const string securitykey = "3ecur1tyKey";
            if (isEncrypt)
            {
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

                var hashmd5 = new MD5CryptoServiceProvider();
                byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(securitykey));
                hashmd5.Clear();

                var tdes = new TripleDESCryptoServiceProvider
                           {
                               Key = keyArray,
                               Mode = CipherMode.ECB,
                               Padding = PaddingMode.PKCS7
                           };

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();

                string value = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                return (T)Convert.ChangeType(value, typeof(T));
            }
            else
            {
                byte[] toEncryptArray = Convert.FromBase64String(toEncrypt);

                var hashmd5 = new MD5CryptoServiceProvider();
                byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(securitykey));
                hashmd5.Clear();

                var tdes = new TripleDESCryptoServiceProvider
                           {
                               Key = keyArray,
                               Mode = CipherMode.ECB,
                               Padding = PaddingMode.PKCS7
                           };

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdes.Clear();

                string value = Encoding.UTF8.GetString(resultArray);
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        public static string InsertSpaceByUpperCase(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (output.Length > 0 && Char.IsUpper(input[i]) &&
                    (!Char.IsUpper(input[i - 1]) ||
                     (i < input.Length - 1 && !Char.IsUpper(input[i + 1]))
                    ))
                {
                    output += " " + input[i];
                }
                else
                    output += input[i];
            }
            return output;
        }
        public static string UpperCaseBySpace(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            input = input.Trim();
            string output = input[0].ToString().ToUpper();
            for (int i = 1; i < input.Length; i++)
            {
                var letter = input[i];
                if (letter == ' ' && output.Length > 0)
                {
                    output += " " + input[i + 1].ToString().ToUpper();
                    i++;
                }
                else
                    output += letter;
            }
            return output;
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

        public static object ConvertYesNoToBool(string value)
        {
            if (value != null && value.Trim().ToLower() == "yes")
                return true;
            if (value != null && value.Trim().ToLower() == "no")
                return false;
            return null;
        }

        public static string RemoveSpecialCharacters(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            value = UpperCaseBySpace(value);
            var sb = new StringBuilder();
            foreach (char c in value)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string GetToken(string onOffSession, string apiPw, string apiUsername)
        {
            var currentDate = DateTime.Now.ToString("yyyyMMdd"); // YYYYMMDD Eg: 20160122
            return EncryptHelper.EncryptPassword(onOffSession + apiPw + currentDate + apiUsername);
        }
    }

}

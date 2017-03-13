using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using NS.Helpers;
using NS.Mail;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public class EmailHelper
    {
        public static string RenderMessage(string path, object model = null)
        {
            try
            {
                var mail = new RazorMailMessage(path, model).Render();
                var stream = mail.AlternateViews[0].ContentStream;
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                //var a = new ExternalException("template file:" + path + "\n\n" + ex.Message);
                //throw a;
                return "";
            }
        }
        public static void SendEmail(string[] to, string subject, RazorMailMessage message)
        {
            Mailer.UseMessage(message)
                  .From("info@subaru-ioffice.com")
                  .To(to)
                  .Subject(subject)
                  .SendAsync();
        }

        public static void SendEmailHaveAttach(string to, string subject, RazorMailMessage message, string attachName, byte[] data)
        {
            Mailer.UseMessage(message)
                  .From("info@subaru-ioffice.com")
                  .To(to)
                  .Subject(subject)
                  .Attach(attachName, data)
                  .SendAsync();
        }

        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
        }
    }
}
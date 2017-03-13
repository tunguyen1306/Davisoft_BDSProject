using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Davisoft_BDSProject.Domain.Helpers;
using Davisoft_BDSProject.Web.Infrastructure.Filters;
using Davisoft_BDSProject.Web.Infrastructure.Utility;
using UrlHelper = Davisoft_BDSProject.Web.Infrastructure.Helpers.UrlHelper;

namespace Davisoft_BDSProject.Web.Controllers
{
    [ExcludeFilters(typeof(RequestAuthorizeAttribute))]
    public class ExportToPdfController : Controller
    {
        //
        // GET: /ExPortToPDF/

        public void PublishToPdf(string modelName, string actionName, string itemId, string parameter, string name, string pdfparameter)
        {
            string pathForExport = UrlHelper.Root + "/" + "Report/" + modelName + actionName + "/" + itemId;
            byte[] fileContent = GeneratePdfFileWithParameters(pathForExport, parameter, pdfparameter);
            string attachment = "";
            if (fileContent != null)
            {

                var filename = modelName + "-" + actionName + "-" + DateTime.Now.ToString("dd-MMM-yy") + ".pdf";
                if (string.IsNullOrEmpty(name))
                    attachment = string.Format("attachment; filename = \"{0}\"",
                                               System.IO.Path.GetFileName(filename));//"attachment; filename=" + modelName + "-" + actionName + "-" + DateTime.Now.ToString("dd-MMM-yy") + ".pdf";
                else
                {
                    filename = name + "-" + DateTime.Now.ToString("dd-MMM-yy") + ".pdf";
                    attachment = string.Format("attachment; filename = \"{0}\"",
                                               System.IO.Path.GetFileName(filename));
                }
                Response.Clear();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(fileContent);
                Response.End();
            }
        }

        public void PublishToPdfWithURL(string path, string name, string parameter = "")
        {
            string pathForExport = UrlHelper.Root + path;// "/" + "Report/" + modelName + actionName + "/" + itemId;
            byte[] fileContent = GeneratePdfFile(pathForExport);
            string attachment = "";
            if (fileContent != null)
            {
                attachment = "attachment; filename=" + name + "-" + DateTime.Now.ToString("dd-MMM-yy") + ".pdf";
                Response.Clear();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(fileContent);
                Response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="parameter">The url parameter such as "id=100" or "id=100&name=abcd&job=it</param>
        /// <param name="filename">the name of file</param>
        public void PublishToPdfWithParameters(string actionName, string parameter, string filename)
        {
            string pathForExport = UrlHelper.Root + "/" + "Report/" + actionName;
            byte[] fileContent = GeneratePdfFileWithParameters(pathForExport, parameter);
            string attachment = "";
            if (fileContent != null)
            {
                if (string.IsNullOrEmpty(filename))
                    attachment = "attachment; filename=" + "-" + actionName + "-" + DateTime.Now.ToString("dd-MMM-yy") + ".pdf";
                else
                {
                    attachment = "attachment; filename=" + filename + "-" + DateTime.Now.ToString("dd-MMM-yy") + ".pdf";
                }
                Response.Clear();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(fileContent);
                Response.End();
            }
        }

        private byte[] GeneratePdfFile(string path)
        {
            string url = path + "&token=" + ConfigurationManager.AppSettings["token"];
            return ProcessingExport(url);
        }
        private byte[] GeneratePdfFileWithPDFparameter(string path, string pdfparameter)
        {
            string url = path + "?token=" + ConfigurationManager.AppSettings["token"];
            return ProcessingExport(url, pdfparameter);
        }
        private byte[] GeneratePdfFileWithParameters(string path, string urlparameter, string pdfparameter = "")
        {
            string url = path + "?token=" + ConfigurationManager.AppSettings["token"] + "&" + urlparameter;
            return ProcessingExport(url, pdfparameter);
        }


        private byte[] ProcessingExport(string url, string pdfparameter = "")
        {
            //if (Request.Url != null && Request.UrlReferrer != null && (Request.Url.Host == Request.UrlReferrer.Host && (Request.Cookies["DatabaseName"] == null || string.IsNullOrEmpty(Request.Cookies["DatabaseName"].Value))))
            if (!ExcludeRequired.Any(m => url.ToLower().Contains(m.ToLower().Trim())) &&
                (Request.Cookies["DatabaseName"] == null || string.IsNullOrEmpty(Request.Cookies["DatabaseName"].Value)))
            {
                CurrentUser.Logout();
                var loginUrl = Url.Action("Login", "Account");
                Response.Redirect(loginUrl);
                Response.End();
                return null;
            }
            string filename = "report " + DateTime.Now.ToString("MM-dd-yyyy HH-mm-ssfff") + ".pdf";

            string filepath = "\"" + Server.MapPath("~/wkhtmltopdf/pdf/") + filename;


            string cookieArgs = "--cookie DatabaseName " + (Request.Cookies["DatabaseName"] != null ? Request.Cookies["DatabaseName"].Value : "IOfficeDb");

            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = Server.MapPath("~/wkhtmltopdf/") + "wkhtmltopdf.exe",
                    Arguments = pdfparameter + " " + cookieArgs + " " + url + " " + filepath,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true
                }

            };

            process.Start();

            //wait until the conversion is done
            process.WaitForExit();

            // read the exit code, close process
            process.Close();
            string fileReadPath = Server.MapPath("~/wkhtmltopdf/pdf/") + filename;
            //initialize the filestream with filepath
            var fs = new FileStream(fileReadPath, FileMode.Open, FileAccess.Read);
            var fileContent = new byte[(int)fs.Length];

            //read the content
            fs.Read(fileContent, 0, (int)fs.Length);

            //close the stream
            fs.Close();
            System.IO.File.Delete(fileReadPath);

            return fileContent;
        }

        private static readonly List<string> ExcludeRequired = new List<string>
        {
            "PrintReportQ01",
            "PrintReportR01",
            "PrintReportR01A",
            "PrintReportR03"
        };
    }
}

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace Davisoft_BDSProject.Domain.Helpers
{
    public class PDFExport
    {
        public string wkhtmltoPDFPath = HttpContext.Current.Server.MapPath("~/wkhtmltopdf/");
        public  string foldertoExport = HttpContext.Current.Server.MapPath("~/attachments/");
        //}
        public byte[] GeneratePdfFile(string path, string name)
        {
            
            string url = path + "?token=" + ConfigurationManager.AppSettings["token"];
            
            //output PDF file Path
            string filename = name + DateTime.Now.ToString("MM-dd-yyyy hh-mm") + ".pdf";

            string filepath = "\"" + foldertoExport   + filename; 

            //variable to store pdf file content

            var process = new Process
                          {
                              StartInfo =
                              {
                                  UseShellExecute = false,
                                  CreateNoWindow = true,
                                  FileName = HttpContext.Current.Server.MapPath("~/wkhtmltopdf/") + "wkhtmltopdf.exe",
                                  Arguments =  url + " " + filepath,
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
            string fileReadPath = foldertoExport + filename;
            //initialize the filestream with filepath
            var fs = new FileStream(fileReadPath, FileMode.Open, FileAccess.Read);
            var fileContent = new byte[(int) fs.Length];

            //read the content
            fs.Read(fileContent, 0, (int) fs.Length);

            //close the stream
            fs.Close();

            File.Delete(fileReadPath);
            return fileContent;
        }
        private byte[] GeneratePdfFile(string path, string name, string parameter)
        {
            string url = path + "?token=" + ConfigurationManager.AppSettings["token"];

            //output PDF file Path
            string filename = name + DateTime.Now.ToString("MM-dd-yyyy hh-mm") + ".pdf";

            string filepath = "\"" + foldertoExport + filename;

            //variable to store pdf file content

            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = wkhtmltoPDFPath + "wkhtmltopdf.exe",
                    Arguments = parameter + " " + url + " " + filepath,
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
            string fileReadPath = foldertoExport + filename;
            //initialize the filestream with filepath
            var fs = new FileStream(fileReadPath, FileMode.Open, FileAccess.Read);
            var fileContent = new byte[(int)fs.Length];

            //read the content
            fs.Read(fileContent, 0, (int)fs.Length);

            //close the stream
            fs.Close();
            File.Delete(fileReadPath);
            return fileContent;
        }

        public string GeneratePdfPath(string path, string name)
        {

            string url = path + "?token=" + ConfigurationManager.AppSettings["token"];

            //output PDF file Path
            string filename = name + DateTime.Now.ToString("MM-dd-yyyy hh-mm") + ".pdf";

            string filepath = "\"" + foldertoExport + filename;

            //variable to store pdf file content

            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = wkhtmltoPDFPath + "wkhtmltopdf.exe",
                    Arguments = url + " " + filepath,
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
            return   filename;
            //initialize the filestream with filepath

            
        }
    
    }
}

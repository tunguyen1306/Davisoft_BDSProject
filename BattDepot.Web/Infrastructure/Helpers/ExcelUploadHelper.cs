using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public class ExcelUploadHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="includeTitleRow">row 0 make title</param>
        /// <returns></returns>
        public static DataTable GetDataFromExcel(string strFilePath, string sheetName, bool includeTitleRow = true)
        {
            //if (!strFilePath.Contains("App_Data"))
            //{
            //    strFilePath = Path.Combine(HttpContext.Current.Server.MapPath("/App_Data/Uploads"), strFilePath);
            //}
            string strNameOfSheet = string.IsNullOrEmpty(sheetName)
                                        ? "Sheet1$"
                                        : sheetName;
            var dataTable = new DataTable();
            OleDbConnection dbConnection = GetExcelCon(strFilePath, includeTitleRow);
            dbConnection.Open();
            var cmdSelect = new OleDbCommand(@"SELECT * FROM [" + strNameOfSheet + "]", dbConnection);
            var dataAdapter = new OleDbDataAdapter { SelectCommand = cmdSelect };
            dataAdapter.Fill(dataTable);
            dbConnection.Close();
            return dataTable;
        }
        private static OleDbConnection GetExcelCon(string strFilePath, bool includeTitleRow)
        {
            //if (!strFilePath.Contains("App_Data"))
            //{
            //    strFilePath = Path.Combine(HttpContext.Current.Server.MapPath("/App_Data/Uploads"), strFilePath);
            //}
            string strConn;
            if (includeTitleRow)
            {
                if (strFilePath.Substring(strFilePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 12.0;IMEX=1\"";
                else
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 8.0;HDR=YES\"";
            }
            else
            {
                if (strFilePath.Substring(strFilePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1\"";
                else
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 8.0;HDR=NO\"";
            }
            return new OleDbConnection(strConn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="includeTitleRow">row 0 make title</param>
        /// <returns></returns>
        public static DataTable GetUploadedExcelSpreadsheetName(string excelFilePath, bool includeTitleRow = true)
        {
            //if (!excelFilePath.Contains("App_Data"))
            //{
            //    excelFilePath = Path.Combine(HttpContext.Current.Server.MapPath("/App_Data/Uploads"), excelFilePath);
            //}
            DataTable dt = null;
            try
            {
                OleDbConnection dbConnection = GetExcelCon(excelFilePath,includeTitleRow);

                dbConnection.Open();
                dt = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //if (dt.Rows.Count > 0)
                //    ;
                dbConnection.Close();
            }
            catch
            {
            }
            return dt;
        }

        public static string SaveFile(HttpPostedFileBase fileImport, FolderUpload folder)
        {
            var filename = Guid.NewGuid() + Path.GetExtension(fileImport.FileName); ;
            var uploadPath = Path.Combine(HttpContext.Current.Server.MapPath("/App_Data/Uploads/" + folder), filename);
            try
            {
                fileImport.SaveAs(uploadPath);
            }
            catch
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/App_Data/Uploads/" + folder));
                fileImport.SaveAs(uploadPath);
            }
            return uploadPath;
        }

        public static string GetFilePath(string fileName, FolderUpload folder)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("/App_Data/Uploads/" + folder), fileName);
        }

        
    }
    public enum FolderUpload
    {
        Accessories,
        // ReSharper disable once InconsistentNaming
        IUs,
        Indents,
        ImportTemporaryPlate,
        MatrixDiscount,
        Shipment,
        COE
    }
}
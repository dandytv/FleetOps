using CCMS.ModelSector;
using FleetOps.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace FleetSys.Common
{
    public class CommonHelpers
    {
        public static bool IsValidatePassword(Login login, MsgRetriever msgRetriever)
        {
            bool result = false;

            if (String.IsNullOrEmpty(login.Password) || String.IsNullOrEmpty(login.ConfirmPassword) )
            {
                 msgRetriever.flag = 1;
                 msgRetriever.desp =  "Passwords cannot be empty";
            }
           else if (login.Password != login.ConfirmPassword)
            {
                msgRetriever.flag = 1;
                msgRetriever.desp = "Both new password do not match";
            }
            else if (!isAlphaNumericOnly(login.Password.Trim()))
            {
                msgRetriever.flag = 1;
                msgRetriever.desp = "Password must be a combination of at least eight characters long, one uppercase, lowercase, number, alphabet and special character";
            }
            else
            {
                result = true;
            }
            return result;
        }
        public static bool isAlphaNumericOnly(string strToCheck)
        {
            const string SpecialCharPattern = "^.*(?=.*[\\W]).*$";
            var HasSpecialCharPattern = Regex.IsMatch(strToCheck, SpecialCharPattern);
            Regex rg = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$");

            return (rg.IsMatch(strToCheck) && HasSpecialCharPattern);
        }
        public static ExcelPackage CreateExcel(string[] Headers, List<string[]> Rows, string Title = "Report")
        {
            int colIndex = 1, rowIndex = 4;
            var pkg = PrepareExcelHeader(Title, Headers);
            var ws = pkg.Workbook.Worksheets[1];
            var cell = ws.Cells[rowIndex, colIndex];
            foreach (var rowVal in Rows)
            {
                foreach (var CellValue in rowVal)
                {
                    cell = ws.Cells[rowIndex, colIndex];
                    cell.Value = CellValue;
                    cell.Merge = true;
                    colIndex++;
                }
                colIndex = 1;
                rowIndex++;
            }
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            return pkg;
        }
        public static ExcelPackage PrepareExcelHeader(string heading, string[] colnames)
        {
            var ExcelPkg = new ExcelPackage();
            ExcelPkg.Workbook.Worksheets.Add("Account Info");
            ExcelWorksheet ws = ExcelPkg.Workbook.Worksheets[1];
            ws.Name = "Account Info";
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Name = "Calibri";
            ws.Cells[1, 1].Value = heading;
            ws.Cells[1, 1, 1, 20].Merge = true;
            ws.Cells[1, 1, 1, 20].Style.Font.Bold = true;
            ws.Cells[1, 1, 1, 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[1, 1, 1, 20].Style.Fill.BackgroundColor.SetColor(Color.Gray);
            ws.Cells[1, 1, 1, 20].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            //var fill = ws.Cells.Style.Fill;
            //fill.PatternType = ExcelFillStyle.Solid;
            //fill.BackgroundColor.SetColor(Color.Gray);
            var border = ws.Cells.Style.Border;
            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
            int colIndex = 1, rowIndex = 3;
            var cell = ws.Cells[rowIndex, colIndex];
            foreach (var col in colnames)
            {
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = col;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor.SetColor(Color.Black);
                cell.Style.Font.Color.SetColor(Color.White);
                colIndex++;
            }
            return ExcelPkg;
        }
        public static string ContentType(string tempType)
        {

            switch (tempType.ToLower())
            {

                case ".jpeg":
                    return "image/jpeg";

                case ".jpg":
                    return "image/jpeg";

                case ".gif":
                    return "image/gif";

                case ".bmp":
                    return "image/bmp";
                case ".png":
                    return "image/png";
                case ".doc":
                    return "application/msword";
                case ".docx":
                    return "application/msword";

                case ".xls":
                    return "application/vnd.ms-excel";
                case ".xlsx":
                    return "application/vnd.ms-excel";
                case ".csv":
                    return "application/csv";

                case ".pdf":
                    return "application/pdf";

                case ".ppt":
                    return "application/vnd.ms-powerpoint";
                case ".pptx":
                    return "application/vnd.ms-powerpoint";

                case ".html":
                    return "text/html";
                case ".htm":
                    return "text/html";

                case ".stm":
                    return "text/html";
                case ".tif":
                    return "image/tiff";

                case ".tiff":
                    return "image/tiff";

                default:
                    return "application/octet-stream";

            }
        }
 
    }
}
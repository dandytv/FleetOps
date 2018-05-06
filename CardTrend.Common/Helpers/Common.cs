using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Common.Helpers
{
   public static class Common
    {
       public static T ParseEnum<T>(string value)
       {
           return (T)Enum.Parse(typeof(T), value, true);
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
       public static ImageFormat ImageType(string tempType)
       {

           switch (tempType.ToLower())
           {

               case ".jpeg":
                   return ImageFormat.Jpeg;

               case ".jpg":
                   return ImageFormat.Jpeg;

               case ".gif":
                   return ImageFormat.Gif;

               case ".bmp":
                   return ImageFormat.Bmp;

               case ".ico":
                   return ImageFormat.Icon;

               default:
                   return ImageFormat.Png;

           }
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
       public static string GetAccessFlag(string val)
       {
           string error = string.Empty;
           switch (val)
           {
               case "60004":
                   error = CardTrend.Common.Resources.LoginMessages.Msg60004;
                   break;
               case "95184":
                   error = CardTrend.Common.Resources.LoginMessages.Msg95184;
                   break;
               case "70922":
                   error = CardTrend.Common.Resources.LoginMessages.Msg70922;
                   break;
               case "70923":
                   error = CardTrend.Common.Resources.LoginMessages.Msg70923;
                   break;
               case "95008":
                   error = CardTrend.Common.Resources.LoginMessages.Msg95008;
                   break;
               case "95670":
                   error = CardTrend.Common.Resources.LoginMessages.Msg95670; //Your password exceeds 90 days. Reset your pasword
                   break;
               case "95674":
                   error = CardTrend.Common.Resources.LoginMessages.Msg95674; //Please change your password Upon first logon
                   break;
               case "0":
                   break;
               default:
                   error = "Login failed";
                   break;
           }
           return error;
       }
       private static string username()
       {
           return Convert.ToString(ConfigurationManager.AppSettings["EmailUserName"]);

       }
       private static string password()
       {
           return Convert.ToString(ConfigurationManager.AppSettings["EmailPassword"]);

       }
       private static string email()
       {
           return Convert.ToString(ConfigurationManager.AppSettings["EmailAddress"]);

       }
       public static int GetIssueNo()
       {
           return Convert.ToInt32(ConfigurationManager.AppSettings["IssueNo"]);
       }

    }
}

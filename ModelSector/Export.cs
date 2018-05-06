using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using CCMS.ModelSector;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;


namespace ModelSector
{
   public class Exporter
    {
        //public static ExcelPackage PrepareExcelHeader(string heading, string[] colnames)
        //{
        //    var ExcelPkg = new ExcelPackage();
        //    ExcelPkg.Workbook.Worksheets.Add("Account Info");
        //    ExcelWorksheet ws = ExcelPkg.Workbook.Worksheets[1];
        //    ws.Name = "Account Info";
        //    ws.Cells.Style.Font.Size = 11;
        //    ws.Cells.Style.Font.Name = "Calibri";
        //    ws.Cells[1, 1].Value = "Account Info for the user";
        //    ws.Cells[1, 1, 1, 20].Merge = true;
        //    ws.Cells[1, 1, 1, 20].Style.Font.Bold = true;
        //    ws.Cells[1, 1, 1, 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //    ws.Cells[1, 1, 1, 20].Style.Fill.BackgroundColor.SetColor(Color.Gray);
        //    ws.Cells[1, 1, 1, 20].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //    //var fill = ws.Cells.Style.Fill;
        //    //fill.PatternType = ExcelFillStyle.Solid;
        //    //fill.BackgroundColor.SetColor(Color.Gray);
        //    var border = ws.Cells.Style.Border;
        //    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
        //    int colIndex = 1, rowIndex = 3;
        //    var cell = ws.Cells[rowIndex, colIndex];
        //    foreach (var col in colnames)
        //    {
        //        cell = ws.Cells[rowIndex, colIndex];
        //        cell.Value = col;
        //        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        cell.Style.Font.Bold = true;
        //        cell.Style.Fill.BackgroundColor.SetColor(Color.Black);
        //        cell.Style.Font.Color.SetColor(Color.White);
        //        colIndex++;
        //    }
        //    return ExcelPkg;
        //}

    }
}

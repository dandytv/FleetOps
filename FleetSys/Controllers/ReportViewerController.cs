using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardTrend.Common.Helpers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FleetOps.App_Start;
using AutoMapper;
using CardTrend.Domain.Dto.ReportViewer;

namespace FleetSys.Controllers
{
    public class ReportViewerController : BaseController
    {
        [Accessibility]
        public async Task<ActionResult> Index()
        {
            var _ReportViewer = new ModelSector.ReportViewer
                {
                    RptType = (await BaseService.GetRptType()).RefLibLst
                    //await BaseClass.WebGetRptType()
                };

            return View(_ReportViewer);
        }
        public async Task<JsonResult> FillData()
        {
            var _ReportViewer = new ModelSector.ReportViewer
              {
                  RptType = (await BaseService.GetRptType()).RefLibLst
                  //await BaseClass.WebGetRptType()
              };
            return Json(new { Model = new ReportViewer(), Selects = _ReportViewer }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> SearchReports(ReportViewer _ReportViewer)
        {
            //ObjReportOps.GetReport(_ReportViewer);
            object list = ReportOpService.GetReportViewer(Mapper.Map<ReportViewerDTO>(_ReportViewer));
            string RptTitle = null;
            if (_ReportViewer.SelectedRptType != null)
            {
                //RptTitle = (from p in await WebGetRptType() where p.Value == _ReportViewer.SelectedRptType select p.Text).FirstOrDefault().ToString();
                RptTitle = (from p in (await BaseService.GetRptType()).RefLibLst where p.Value == _ReportViewer.SelectedRptType select p.Text).FirstOrDefault().ToString();
                RptTitle = RptTitle.Split(':')[1];
            }
            var jsonResult = Json(new { Report = list, text = RptTitle }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult DownloadExcelReport(ReportViewer parameters, string Extension, string title, string userId)
        {
            //ObjReportOps.GetReport(parameters);
            userId = GetUserId;
            var result = ReportOpService.GetReportViewer(Mapper.Map<ReportViewerDTO>(parameters));
            Type myType = result.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            var columns = props[0].GetValue(result, null);
            var columns2 = (string[])columns;
            int colIndex = 1, rowIndex = 4;
            var headerName = title.Split(':').Last() + " " + parameters.RefKey + " " + parameters.Date;
            headerName = headerName.Trim();
            var pkg = Common.CommonHelpers.PrepareExcelHeader(headerName, columns2);
            var ws = pkg.Workbook.Worksheets[1];
            var cell = ws.Cells[rowIndex, colIndex];
            var getRows = ReportOpService.GetRowsReport(Mapper.Map<ReportViewerDTO>(parameters));
            //var getRows = ObjReportOps.GetRowReport(parameters);
            Type rType = getRows.GetType();
            IList<PropertyInfo> props2 = new List<PropertyInfo>(rType.GetProperties());
            foreach (var x in getRows)
            {
                string[] arr = ((IEnumerable)x).Cast<object>()
                                 .Select(y => y.ToString())
                                 .ToArray();
                colIndex = 1;
                foreach (var rowVal in arr)
                {
                    cell = ws.Cells[rowIndex, colIndex];
                    cell.Value = rowVal;
                    colIndex++;
                }
                rowIndex++;
            }
            string contentType = Common.CommonHelpers.ContentType(Extension);

            Byte[] bin = pkg.GetAsByteArray();
            return File(bin, contentType, headerName + Extension);
        }

        public ActionResult downloadCSV(ReportViewer parameters, string title, string userId, string Extension)
        {
            var result = ReportOpService.GetTableReport(Mapper.Map<ReportViewerDTO>(parameters));
                //ObjReportOps.GetTableReport(parameters);
            var tempStore = ToCSV(result);

            string file;
            //string subfileRootFile = System.Web.HttpContext.Current.Server.MapPath("~/Users/") + this.GetUserId;
            //string tempSubFileRoot = subfileRootFile.Replace("\\", "/");
            string contentType = Common.CommonHelpers.ContentType(Extension);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(tempStore);
            //StreamWriter sw = new StreamWriter(tempStore,);

            var headerName = title.Split(':').Last() + parameters.RefKey + " " + parameters.Date;
            file = headerName + "." + Extension;

            return File(byteArray, contentType, file);

        }
    }
}
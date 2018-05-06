using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Domain.Dto.ReportViewer
{
   public class ReportViewerDTO
    {
        public string WorksheetTitle { get; set; }
        public string WorksheetDesc { get; set; }
        public int ColIndex { get; set; }
        public int RowIndex { get; set; }
        public List<string> ColName { get; set; }
        public string prefix { get; set; }
        public string SelectedRptType { get; set; }
        public IEnumerable<SelectListItem> RptType { get; set; }
        public string RefKey { get; set; }
        public string Date { get; set; }
    }
}

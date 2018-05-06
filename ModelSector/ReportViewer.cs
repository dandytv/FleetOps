using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModelSector
{
   public class ReportViewer
    {

        public string WorksheetTitle { get; set; }
        public string WorksheetDesc { get; set; }
        public int ColIndex { get; set; }
        public int RowIndex { get; set; }
        public List<string> ColName { get; set; }
        public string prefix { get; set; }

        [DisplayName("Report Type")]
        public string SelectedRptType { get; set; }

       [Required]
        public IEnumerable<SelectListItem> RptType { get; set; }


        [DisplayName("Reference Key")]
        public string RefKey { get; set; }
        [DisplayName("ReportDate")]
        public string Date { get; set; }

    }
   public class ReportBrowser
   {
       public string ReportName { get; set; }
       public string ReportCategory { get; set; }
       public string FileName { get; set; }
       public string ReportDate { get; set; }
   }
 
}

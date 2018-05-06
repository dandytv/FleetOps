using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ModelSector
{
   public class NegativeFiles
    {
        public IEnumerable<SelectListItem> Ind { get; set; }
        [Display(Name="Indicator")]
        public string SelectedInd { get; set; }
        public IEnumerable<SelectListItem> RefTo { get; set; }
       [Display(Name="Reference")]
        public string SelectedRefTo { get; set; }
       public string RefKey { get; set; }
       public string LastUpdate { get; set; }
       public string AcctNo { get; set; }
       public string RefId { get; set; }
       public string CardNo { get; set; }
       [Display(Name = "Search Key")]
       public string SearchKey { get; set; }
    }
}

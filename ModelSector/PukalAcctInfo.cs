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
   public class PukalAcctInfo
    {
        [DisplayName("Area Code")]
        [Required]
        public string SelectedAreaCode { get; set; }
        public IEnumerable<SelectListItem> AreaCode { get; set; }
        [Display(Name = "Ref Code")]
        public string SelectedRefCd { get; set; }
        public IEnumerable<SelectListItem> RefCd { get; set; }
        [Display(Name = "Statement Date")]
        public string SelectedStatementDate { get; set; }
        public IEnumerable<SelectListItem> StmDate { get; set; }

        public IEnumerable<SelectListItem> IssBank { get; set; }
        [DisplayName("Issuing Bank")]
        public string SelectedIssBank { get; set; }
    }
}

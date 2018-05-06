using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector.Fraud
{
   public class FraudCaseListViewModel
    {
        [Display(Name = "Case No")]
        public Int64 EventId { get; set; }
        [Display(Name = "Account No")]
        public string RefKey { get; set; }
        [Display(Name = "Status")]
        public string e_Descp { get; set; }
        [Display(Name = "Close Date")]
        public string CloseDate { get; set; }
        [Display(Name = "Updated Date")]
        public string LastUpdDate { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }
        [Display(Name = "Updated By")]
        public string UserId { get; set; }

        //EventType
        [Display(Name = "Fraud Type")]
        public string et_Descp { get; set; }

        [Display(Name = "Company Name")]
        public string CmpyName1 { get; set; }
    }
}

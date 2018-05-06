using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCMS.ModelSector;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector.Global_Resources;

namespace ModelSector
{
   public class StatusMaintanance
    {


        public CardnAccNo _CardnAccNo { get; set; }

        [Display(Name = "currentstatus", ResourceType= typeof(locale))]
        public string SelectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }

        [Display(Name = "changetostatus", ResourceType = typeof(locale))]
        public string SelectedChangetoStatus { get; set; }
        public IEnumerable<SelectListItem> ChangetoStatus { get; set; }

        [Display(Name = "reasoncode", ResourceType= typeof(locale))]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; } 
        
        [Display(Name = "remarks", ResourceType= typeof(locale))]
        public string Remarks { get; set; }

        public string ReferenceId { get; set; }

        [Display(Name = "referencetype", ResourceType= typeof(locale))]
        public string ReferenceType { get; set; }
    }
}

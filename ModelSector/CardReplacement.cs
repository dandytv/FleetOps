using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCMS.ModelSector;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector;
using ModelSector.Helpers;
namespace ModelSector
{
   public class CardReplacement
    {
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedReasonCodeDdl")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "RemarksLbl")]
        public string Remarks { get; set; }      
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedCurrentStatusDdl")]
        public string SelectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedChangetoStatusDdl")]
        public string SelectedChangetoStatus { get; set; }
        public IEnumerable<SelectListItem> ChangeToStatus { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "DescpLbl")]
        public string Descp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedFeeCdDdl")]
        public string SelectedFeeCd { get; set; }
        public IEnumerable<SelectListItem> FeeCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "ExpiryDateLbl")]
        public string ExpiryDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "NewCardExpiryDateLbl")]
        public string NewCardExpiryDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "TerminatedDateLbl")]
        public string TerminatedDate { get; set; }    
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "XReferenceIDLbl")]
        public string XReferenceID { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "XReferenceNoLbl")]
        public string XReferenceNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedCardMediaTypeDdl")]
        public string SelectedCardMediaType { get; set; }
        public IEnumerable<SelectListItem> CardMedia { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "AccNoLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public String AccNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "CardNoLbl")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "Card No Range = 16 to 19 digit")]
        public string CardNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "NewCardNoLbl")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "Card No Range = 16 to 19 digit")]
        public string NewCardNo { get; set; }
    }
}

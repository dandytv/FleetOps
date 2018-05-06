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
    public class ReembossCard
    {

        [DisplayName("Reason Code")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("Current Status")]
        public string SelectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }

        [DisplayName("Change to Status")]
        public string SelectedChangetoStatus { get; set; }
        public IEnumerable<SelectListItem> ChangeToStatus { get; set; }

        [DisplayName("Description")]
        public string Descp { get; set; }

        [DisplayName("Fee Code")]
        public string SelectedFeeCd { get; set; }
        public IEnumerable<SelectListItem> FeeCd { get; set; }

        [DisplayName("Expiry Date")]
        public string ExpiryDate { get; set; }


        [DisplayName("New Card Expiry Date")]
        public string NewCardExpiryDate { get; set; }


        [DisplayName("Terminated Date")]
        public string TerminatedDate { get; set; }



        [DisplayName("User Id")]
        public string UserId { get; set; }

        [DisplayName("xReference Id")]
        public string XReferenceID { get; set; }

        [DisplayName("xReference No")]
        public string XReferenceNo { get; set; }


        [DisplayName("Account No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public String AccNo { get; set; }
        [DisplayName("Card No")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "Card No Range = 16 to 19 digit")]
        public string CardNo { get; set; }
        [DisplayName("New Card No")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "Card No Range = 16 to 19 digit")]
        public string NewCardNo { get; set; }
        //   public DebtsCollection PrimartInfo { get; set; }
        //     public CardnAccNo AccountNo { get; set; }
    }
}

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
    public class VoiceAuth
    {
        [Display(Name = "Dealer")]
        [Required]
        public string BusnLocation { get; set; }
        [Display(Name = "Card No")]
        [Required]
        public string CardNo { get; set; }
        [Display(Name = "Txn Indicator")]
        public string TxnInd { get; set; }
        [Display(Name = "Txn Date")]
        public string TxnDate { get; set; }
        [Display(Name = "Txn Amount")]
        public string TxnAmt { get; set; }
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Display(Name = "Txn Code")]
        public string TxnCd { get; set; }
        [Display(Name = "Driver Code")]
        public string DriverCd { get; set; }
        [Display(Name = "ID's")]
        public string Ids { get; set; }
        [Display(Name = "Response Code")]
        public string ResponseCd { get; set; }
        [Display(Name = "Approval Code")]
        public string ApprovalCd { get; set; }

        [Display(Name="Creation Date")]
        public string CreationDate { get; set; }
        [Display(Name="Emboss Name")]
        public string EmbossName { get; set; }
        [Display(Name = "Business Name")]
        public string BusnName { get; set; }
    }

    public class VoiceAuthDetail
    {
        [Display(Name = "Account No")]
        public string AcctNo { get; set; }
        [Display(Name = "Account Name")]
        public string AcctName { get; set; }
        [Display(Name = "Status")]
        public string SelectedAcctStatus { get; set; }
        public IEnumerable<SelectListItem> AcctStatus { get; set; }
        [Display(Name = "Corporate Code")]
        public string CorpCd { get; set; }
        [Display(Name = "Account Balance")]
        public string AcctBalance { get; set; }
        [Display(Name = "Credit Limit")]
        public string CreditLimit { get; set; }

        [Display(Name = "Card No")]
        public string CardNo { get; set; }
        [Display(Name = "Card Type")]
        public string SelectedCardType { get; set; }
        public IEnumerable<SelectListItem> CardType { get; set; }
        [Display(Name = "Member Since")]
        public string MemberSince { get; set; }
        public IEnumerable<SelectListItem> CardStatus { get; set; }
        [Display(Name = "Card Status")]
        public string SelectedCardStatus { get; set; }
        public string XRefCardNo { get; set; }

        public IEnumerable<SelectListItem> ProductGroup { get; set; }

        [Display(Name = "Product Group")]
        public string SelectedProductGroup { get; set; }

        [Display(Name = "Business Name")]
        public string BusnName { get; set; }
        [Display(Name = "Dealer")]
        public string BusnLocation { get; set; }
        [Display(Name = "Floor Limit")]
        public string FloorLimit { get; set; }
        public IEnumerable<SelectListItem> BusnStatus { get; set; }
        [Display(Name = "Business Status")]
        public string SelectedBusnStatus { get; set; }
        [Display(Name = "Transaction Code")]
        public string SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCode { get; set; }
        [Display(Name = "Terminal Id")]
        [Required]
        public string termId { get; set; }
        [Display(Name = "Driver Code")]
        public string DriverCd { get; set; }
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        [Display(Name="Txn Amount")]
        [Required]
        public string TxnAmt { get; set; }

        public IEnumerable<SelectListItem> ProdCd { get; set; }
        public string SelectedProdCd { get; set; }

        public string AuthNo { get; set; }

        public string RespCd { get; set; }
    }

    public class VoiceAuthProducts
    {
        [Required]
        [Display(Name = "Product Code")]
        public string SelectedProdCd { get; set; }
        public IEnumerable<SelectListItem> ProdCd { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public string Qty { get; set; }
        [Required]
        [Display(Name = "Amount Points")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string AmtPoints { get; set; }
        [Display(Name = "Fast Track")]
        public string FastTrack { get; set; }
        [Display(Name = "Unit Price")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string UnitPrice { get; set; }
        [Display(Name = "Sequence")]
        public string Seq { get; set; }
    }
    public class CardDetail
    {

    }
}

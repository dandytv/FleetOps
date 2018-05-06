using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSector;
using CCMS.ModelSector;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector.Global_Resources;
using System.Web.Mvc;
using ModelSector.Helpers;
namespace ModelSector
{
  public class ManualSlipEntry
  {
        public CreationDatenUserId _CreationDatenUserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "CardNoLbl")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "Card No Range = 16 to 19 digit")]
        public string CardNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "AuthNoLbl")]
        public string AuthNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "ArrayCntLbl")]
        public int? ArrayCnt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "AmountLbl")]
        public string Amount { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "BatchEntryStsLbl")]
        public string BatchEntrySts { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "BusnLocationLbl")]
        public string BusnLocation { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "DescpLbl")]
        public string Descp { get; set; }     
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "RcptNoLbl")]
        public string RcptNo { get; set; }       
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "InvoiceNoLbl")]
        public int? InvoiceNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "OrigBatchNoLbl")]
        public int? OrigBatchNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "OdometerLbl")]
        public int? Odometer { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "RrnLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string Rrn { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SettleDateLbl")]
        [Required(ErrorMessage = "*")]
        public string SettleDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "StansLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? Stans{ get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SiteIdLbl")]
        public string SiteId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SelectedStsDdl")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SelectedTermIdDdl")]
        public string SelectedTermId { get; set; }
        public IEnumerable<SelectListItem> TermId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "TotalCntLbl")]
        public int? TotalCnt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "TotalAmtLbl")]
        public string TotalAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "DisplayTotalAmtLbl")]
        public string DisplayTotalAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "TxnDescpLbl")]
        public string TxnDescp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SelectedTxnCdDdl")]
        [Required(ErrorMessage = "*")]
        public int SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "TxnDetailIdLbl")]
        public string TxnDetailId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "AuthCardNoLbl")]
        public string AuthCardNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "AuthCardExpLbl")]
        public string AuthCardExp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "TxnAmtLbl")]
        public string TxnAmt { get; set; }
        public string DisplayTxnAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "ShownTxnAmtLbl")]
        public string ShownTxnAmt { get; set; }      
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "TxnIdLbl")]
        public string TxnId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "TxnDateLbl")]
        public string TxnDate { get; set; }    
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "CardExpireLbl")]
        [RegularExpression(@"^[0-9]{1,4}$", ErrorMessage = "YYMM")]
        public string CardExpire { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "DriverCdLbl")]
        public int? DriverCd  { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "CoRegDateLbl")]
        public string CoRegDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "BatchIdLbl")]
        public string BatchId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SelectedProdCdDdl")]
        [Required(ErrorMessage = "Please Select A Product")]
        public string SelectedProdCd { get; set; }
        public IEnumerable<SelectListItem> ProdCd { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "QuantityLbl")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string Quantity { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "ProdAmtLbl")]
        public string ProdAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "UnitPriceLbl")]
        [Required(ErrorMessage = "Please Fill In The Unit Price")]
        public string UnitPrice { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "ProdDescLbl")]
        public string ProdDesc { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SettleIdLbl")]
        public string SettleId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "LastUpdDateLbl")]
        public string LastUpdDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "VATNoLbl")]
        public string VATNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "VATAmtLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string VATAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "VATRateLbl")]
        public string VATRate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SelectedVATCdDdl")]
        public string SelectedVATCd { get; set; }
        public IEnumerable<SelectListItem> VATCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "AppvCdLbl")]
        public string AppvCd { get; set; }
  }

  public class ManualTxnProduct
  {
        public CreationDatenUserId _CreationDatenUserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "BatchIdLbl")]
        public string BatchId { get; set; }        
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SelectedProdCdDdl")]
        [Required(ErrorMessage="Please Select A Product")]
        public string SelectedProdCd { get; set; }
        public IEnumerable<SelectListItem> ProdCd { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "QuantityLbl")]
        public string Quantity { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "ProdAmtLbl")]
        public string ProdAmt { get; set; }       
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "UnitPriceLbl")]
        [Required(ErrorMessage="Please Fill In The Unit Price")]
        public string UnitPrice { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "ProdDescLbl")]
        public string ProdDesc { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "TxnIdLbl")]
        public string TxnId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "TxnDetailIdLbl")]
        public string TxnDetailId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SettleIdLbl")]
        public string SettleId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "LastUpdDateLbl")]
        public string LastUpdDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "VATAmtLbl")]
        public string VATAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "VATRateLbl")]
        public string VATRate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendManualSlip", "SelectedVATCDdl")]
        public string SelectedVATCd { get; set; }
        public IEnumerable<SelectListItem> VATCd { get; set; }
  }
}

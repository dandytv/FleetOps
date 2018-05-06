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
    public class PukalAcctMaintInfo
    {
        [DisplayName("Txn Code")]
        [Required]
        public string SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        public long BatchId { get; set; }
        public string RefCd { get; set; }
        public string AcctOfficeCd { get; set; }
        public string Sts { get; set; }
        public string Func { get; set; }
        [DisplayName("Cheque No")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter a valid 10 digits")]
        public decimal ChequeNo { get; set; }
        [DisplayName("Cheque Amount")]
        public string ChequeAmt { get; set; }
        [DisplayName("Txt Date")]
        [Required]
        public string CreationDate { get; set; }
        [DisplayName("Statement Date")]
        [Required]
        public string StatementDate { get; set; }
        public int CycStmtId { get; set; }
        [DisplayName("Owner")]
        public string SelectedOwner { get; set; }
        public IEnumerable<SelectListItem> Owner { get; set; }
        [DisplayName("UserId")]
        public string UserId { get; set; }
        public List<PukalAcctBatchView> MultipleTxnRecord { get; set; }
        [DisplayName("GL Settlement")]
        public string SelectedSettlement { get; set; }
        public IEnumerable<SelectListItem> GLSettlement { get; set; }
        [Required]
        [DisplayName("Slip No")]
        public string SlipNo { get; set; }

        public IEnumerable<SelectListItem> IssBank { get; set; }
        [DisplayName("Issuing Bank")]
        public string SelectedIssBank { get; set; }
    }
}

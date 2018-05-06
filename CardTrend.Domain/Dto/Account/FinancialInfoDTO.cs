using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class FinancialInfoDTO
    {
       public string AcctNo { get; set; }
       public string TaxId { get; set; }
       public string StopLPC { get; set; }
       public byte? DunCd { get; set; }
       public decimal? AllowanceFactor { get; set; }
       public decimal? AccruedInterestAmt { get; set; }
       public decimal? AccruedCreditUsageAmt { get; set; }
       public decimal? PromptPaymtRebate { get; set; }
       public byte? PromptPaymtRebateTerms { get; set; }
       public string PromptPaymtRebateExpiry { get; set; }
       public decimal? LitLimitPerTxn { get; set; }
       public decimal? AmtLimitPerTxn { get; set; }
       public byte? CycNo { get; set; }
       public string StmtType { get; set; }
       public string StmtInd { get; set; }
       public DateTime? StmtDate { get; set; }
       public string PaymtMethod { get; set; }
       public int? PymtTerms { get; set; }
       public byte? GracePeriod { get; set; }
       public string DirectDebitInd { get; set; }
       public string BankAcctType { get; set; }
       public string BankName { get; set; }
       public string BankAcctNo { get; set; }
       public string BankBranchCd { get; set; }
       public string PayeeCd { get; set; }
       public string SKDSNo { get; set; }
       public decimal? SKDSQuota { get; set; }
       public DateTime? SKDSFromDate { get; set; }
       public DateTime? SKDSToDate { get; set; }
       public decimal? SKDSRate { get; set; }
       public string SKDSRef { get; set; }
       public string TaxCategory { get; set; }
       public DateTime? WriteOffDate { get; set; }
       public string LastPaymtRecvType { get; set; }
       public decimal? LastPaymtRecvAmt { get; set; }
       public DateTime? LastPaymtDate { get; set; }
       public string InvBillInd { get; set; }
       public string PymtInd { get; set; }
       public string VehPerfRptInd { get; set; }
       public string RiskCategory { get; set; }
       public decimal? CreditLimit { get; set; }
       public string SecuredCreditLine { get; set; }
       public string Ewt { get; set; }
       public string Owner { get; set; }
    }
}

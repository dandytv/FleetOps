using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class AccountCaoDTO
    {
       public AccountCaoDTO()
       {
           remarkHistories = new List<WebSecDepRemarksDTO>();
       }
       public string AcctNo { get; set; }
       public string ApplId { get; set; }
       public decimal? CreditLimit { get; set; }
       public string PymtMode { get; set; }
       public int? PymtTerms { get; set; }
       public string BGSerialNo { get; set; }
       public decimal? TxnAmtLimit { get; set; }
       public decimal? TxnLitLimit { get; set; }
       public string SaleTerritory { get; set; }
       public string TradingArea { get; set; }
       public string RiskCategory { get; set; }
       public string AssessmentType { get; set; }
       public decimal? PropCreditLimit { get; set; }
       public decimal? RecCreditLimit { get; set; }
       public decimal? RecSecurityAmt { get; set; }
       public decimal? PropSecurityAmt { get; set; }
       public string DirectDebitInd { get; set; }
       public string DepositType { get; set; }
       public string DepositExp { get; set; }
       public string BankAcctType { get; set; }
       public string BankName { get; set; }
       public string BankAcctNo { get; set; }
       public string BankBranchCd { get; set; }
       public decimal? DepositAmt { get; set; }
       public DateTime? ValidityDate { get; set; }
       public DateTime? NRID { get; set; }
       public decimal? SecurityAmt { get; set; }
       public DateTime? EffFromDate { get; set; }
       public DateTime? EffToDate { get; set; }
       public DateTime? CreationDate { get; set; }
       public string SAPRefNo { get; set; }
       public string TxnId { get; set; }
       public string ReasonCd { get; set; }
       public string AppvUserId1 { get; set; }
       public string AppvSts1 { get; set; }
       public DateTime? AppvDate1 { get; set; }
       public string AppvUserId2 { get; set; }
       public string AppvSts2 { get; set; }
       public DateTime? AppvDate2 { get; set; }
       public string AppvUserId3 { get; set; }
       public string AppvSts3 { get; set; }
       public DateTime? AppvDate3 { get; set; }
       public string AppvUserId4 { get; set; }
       public string AppvSts4 { get; set; }
       public DateTime? AppvDate4 { get; set; }
       public string DocPath { get; set; }
       public string Remarks { get; set; }
       public List<WebSecDepRemarksDTO> remarkHistories { set; get; }
       public string UserId { get; set; }
       public string Quantitativerating { get; set; }
       public string Qualitativerating { get; set; }
    }
}

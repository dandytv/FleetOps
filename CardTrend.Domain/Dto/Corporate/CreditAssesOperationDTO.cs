using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Corporate
{
   public class CreditAssesOperationDTO
    {
       public string AcctNo { get; set; }
       public string CorpCd { get; set; }
       public string DirectDebitInd { get; set; }
       public string DepositInd { get; set; }
       public string DepositType { get; set; }
       public string BankAcctType { get; set; }
       public string BankName { get; set; }
       public string BankAcctNo { get; set; }
       public decimal? DepositAmt { get; set; }
       public decimal? SecurityDeposit { get; set; }
       public DateTime? ValidityDate { get; set; }
       public string TxnId { get; set; }
       public string UserId { get; set; }
       public string CreationDate { get; set; }
       public string BGSerialNo { get; set; }
       public DateTime? DepositFromDate { get; set; }
       public DateTime? DepositToDate { get; set; }
       public DateTime? EffFromDate { get; set; }
       public DateTime? EffToDate { get; set; }
       public DateTime? NIRD { get; set; }
       public string Remark { get; set; }
       public string SAPRefNo { get; set; }
    }
}

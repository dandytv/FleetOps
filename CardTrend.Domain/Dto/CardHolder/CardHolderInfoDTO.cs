using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.CardHolder
{
   public class CardHolderInfoDTO
    {
       public decimal TxnLimit { get; set; }
       public decimal LitLimit { get; set; }
       public DateTime? PinTriedUpdDate { get; set; }
       public string LocationInd { get; set; }
       public string LastTxnDate { get; set; }
       public Int64 AcctNo { get; set; }
       public string EmbName { get; set; }
       public string CardType { get; set; }
       public string CardNo { get; set; }
       public string Sts { get; set; }
       public DateTime? BlockedDate { get; set; }
       public string ReasonCd { get; set; }
       public DateTime? CreationDate { get; set; }
       public string MemSince { get; set; }
       public string CardExpiry { get; set; }
       public Int64? XPreCardNo { get; set; }
       public Int64? XRefCardNo { get; set; }
       public string PINInd { get; set; }
       public string VehRegsNo { get; set; }
       public string SKDSInd { get; set; }
       public decimal SKDSQuota { get; set; }
       public string SKDSNo { get; set; }
       public string DriverCd { get; set; }
       public string DriverName { get; set; }
       public DateTime? TerminatedDate { get; set; }
       public string PVV { get; set; }
       public string PINoffSet { get; set; }
       public string DialogueInd { get; set; }
       public string PushAlertInd { get; set; }
       public string LocationCheckFlag { get; set; }
       public int? LocationMaxCnt { get; set; }
       public decimal LocationMaxAmt { get; set; }
       public string FuelCheckFlag { get; set; }
       public decimal FuelLitPerKM { get; set; }
       public byte? PINExceedCnt { get; set; }
       public int? PINAttempted { get; set; }
       public string AnnlFeeCd { get; set; }
       public string JoiningFeeCd { get; set; }
       public string RenewalInd { get; set; }
       public string EntityId { get; set; }
       public string OdometerInd { get; set; }
       public string PrimaryCard { get; set; }
       public string Model { get; set; }
       public string CostCentre { get; set; }
       public string BranchCd { get; set; }
       public string DivisionCd { get; set; }
       public string DeptCd { get; set; }
       public string ProdGroup { get; set; }
       public int CardMedia { get; set; }
       public string PrimaryCardNo { get; set; }
      // public string CostCenter { get; set; }
    }
}

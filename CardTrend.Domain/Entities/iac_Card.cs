using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Entities
{
  public class iac_Card
    {
        [Key]
        public long CardNo { get; set; }
        public Nullable<long> CardId { get; set; }
        public short IssNo { get; set; }
        public string Sts { get; set; }
        public Nullable<System.DateTime> BlockDate { get; set; }
        public Nullable<long> AcctNo { get; set; }
        public string CardLogo { get; set; }
        public string PlasticType { get; set; }
        public string CardType { get; set; }
        public string CostCentre { get; set; }
        public Nullable<long> PriCardNo { get; set; }
        public Nullable<long> XrefCardNo { get; set; }
        public int EntityId { get; set; }
        public string PriSec { get; set; }
        public string VehRegsNo { get; set; }
        public string SmartSerialNo { get; set; }
        public string EmbName { get; set; }
        public Nullable<System.DateTime> MemSince { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public Nullable<System.DateTime> OldExpiryDate { get; set; }
        public Nullable<System.DateTime> TerminationDate { get; set; }
        public Nullable<int> CardMedia { get; set; }
        public string RenewalInd { get; set; }
        public string VIPInd { get; set; }
        public string Cvc { get; set; }
        public string Cvc2 { get; set; }
        public string OdometerInd { get; set; }
        public string PinInd { get; set; }
        public string PinBlock { get; set; }
        public string PVV { get; set; }
        public string PinOffSet { get; set; }
        public string TempPVV { get; set; }
        public Nullable<int> BDKIdx { get; set; }
        public Nullable<byte> CycNo { get; set; }
        public string DialogueInd { get; set; }
        public string SKDSInd { get; set; }
        public string SKDSNo { get; set; }
        public Nullable<decimal> SKDSQuota { get; set; }
        public Nullable<System.DateTime> SKDSEffectiveDate { get; set; }
        public Nullable<System.DateTime> SKDSEndDate { get; set; }
        public Nullable<int> PriorityNo { get; set; }
        public Nullable<System.DateTime> ActivationDate { get; set; }
        public Nullable<System.DateTime> FirstTxnDate { get; set; }
        public string ProdGroup { get; set; }
        public string PartnerRefNo { get; set; }
        public string JoiningFeeCd { get; set; }
        public string AnnlFeeCd { get; set; }
        public string DriverCd { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string StaffNo { get; set; }
        public string GovernmentLevyFeeCd { get; set; }
        public string BranchCd { get; set; }
        public string DivisionCd { get; set; }
        public string DeptCd { get; set; }
        public Nullable<int> ApplId { get; set; }
        public Nullable<int> AppcId { get; set; }
        public Nullable<long> MigrateId { get; set; }
        public string ReasonCd { get; set; }
        public Nullable<int> PrcsId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> LastUpdDate { get; set; }
        public string SKDSProdGroup { get; set; }
        public string SKDSInstantInd { get; set; }
    }
}

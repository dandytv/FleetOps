using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Merchant
{
    public class MerchGeneralInfoDTO
    {
        public string BusnName { get; set; }
        public string BusnLocation { get; set; }
        public string ReconAcct { get; set; }
        public string AgreementNo { get; set; }
        public DateTime? AgreementDate { get; set; }
        public string AffiliateWith { get; set; }
        public string PersonInCharge { get; set; }
        public string Ownership { get; set; }
        public string Establishment { get; set; }
        public string Sic { get; set; }
        public string Mcc { get; set; }

        public string CoRegNo { get; set; }
        public string CoRegName { get; set; }
        public DateTime? CoRegDate { get; set; }
        public DateTime? OwnershipTrsfDate { get; set; }
        public string OwnershipTo { get; set; }
        public string DBAName { get; set; }
        public string DBARegion { get; set; }
        public string AreaCd { get; set; }
        public string DBACity { get; set; }
        public string DBAState { get; set; }

        public string Moso { get; set; }
        public string PayeeName { get; set; }
        public string AutoDebitInd { get; set; }
        public string BankName { get; set; }
        public string BankAcctType { get; set; }
        public string BankAcctNo { get; set; }
        public string BankBranchCd { get; set; }
        public string Sts { get; set; }
        public int? EntityId { get; set; }
        public string TaxId { get; set; }
        public string WithholdingTaxInd { get; set; }
        public decimal? WithholdingTaxRate { get; set; }
        public Byte? CycNo { get; set; }
        public string UserId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ReasonCd { get; set; }
        public string StmtPrint { get; set; }
        public string TaxCode { get; set; }
        public decimal? TopUpLimit { get; set; }
        public decimal? TopUpAmt { get; set; }
        public string SAPNo { get; set; }
        public Int64? AcctNo { get; set; }
        public decimal? MSF { get; set; }
    }
}

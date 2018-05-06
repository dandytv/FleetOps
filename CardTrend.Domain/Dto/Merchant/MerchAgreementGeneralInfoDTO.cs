using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Merchant
{
   public class MerchAgreementGeneralInfoDTO
    {
       public string MerchantAccount { get; set; }
       public string SAPNo { get; set; }
       public string PersonInCharge { get; set; }
       public string BusinessName { get; set; }
       public string AffiliatedWith { get; set; }
       public string WithholdingTaxInd { get; set; }
       public decimal WithholdingTaxRate { get; set; }
       public string TaxId { get; set; }
       public string Status { get; set; }
       public DateTime CreationDate { get; set; }

       public string AcctNo { get; set; }
       public string AgreeNo { get; set; }
       public DateTime? AgreeDate { get; set; }
       public string Ownership { get; set; }
       public string Establishment { get; set; }
       public string CoRegsNo { get; set; }
       public string CoRegsName { get; set; }
       public DateTime? CoRegsDate { get; set; }
       public string Moso { get; set; }
       public string PayeeName { get; set; }
       public string AutoDebit { get; set; }
       public string BankName { get; set; }
       public string BankAcctType { get; set; }
       public string BankAcctNo { get; set; }
       public string BankBranchCd { get; set; }
       public int EntityId { get; set; }
       public string WithholdInd { get; set; }
       public byte CycNo { get; set; }
       public string UserId { get; set; }
       public string ReasonCd { get; set; }
       public byte SrcFrom { get; set; }
       public decimal Msf { get; set; }
    }
}

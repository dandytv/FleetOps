using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class AcctGeneralInfoDTO
    {
       public string AcctNo { get; set; }
       public string PlasticType { get; set; }
       public string AccountName { get; set; }
       public string CmpyRegsNo { get; set; }
       public string CmpyType { get; set; }
       public DateTime RegsDate { get; set; }
       public string SIC { get; set; }
       public string CustomerNo { get; set; }
       public string CorpCd { get; set; }
       public string TermsofPayment { get; set; }
       public string CustomerGroup { get; set; }
       public string BusnEstablishment { get; set; }
       public string SrcCd { get; set; }
       public string SrcRefNo { get; set; }
       public string Sts { get; set; }
       public DateTime BlockedDate { get; set; }
       public DateTime CreationDate { get; set; }
       public DateTime TerminatedDate { get; set; }
       public string ReasonCd { get; set; }
       public string OverrideStsUserId { get; set; }
       public string OverrideSts { get; set; }
       public DateTime OverrideStsStart { get; set; }
       public DateTime OverrideStsExpiry { get; set; }
       public int ApplId { get; set; }
       public string ApplRef { get; set; }
       public DateTime CaptDate { get; set; }
       public string Remarks { get; set; }
       public string WebUserId { get; set; }
       public Int64 LoyaltyCardNo { get; set; }
       public string EntityId { get; set; }
       public int AuditId { get; set; }
       public string WebPassword { get; set; }
       public string ReconAcct { get; set; }
       public string Industry { get; set; }
       public string TaxId { get; set; }
       public string SalesGroup { get; set; }
       public string AccountType { get; set; }
       public byte CutOff { get; set; }
       public int PymtTerms { get; set; }
       public string LangId { get; set; }
       public string CmpyEmbName { get; set; }
       public string FamilyName { get; set; }
       public string AuthName { get; set; }
       public string TradingArea { get; set; }
    }
}

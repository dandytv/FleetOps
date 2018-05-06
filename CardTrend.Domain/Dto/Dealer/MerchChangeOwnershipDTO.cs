using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Dealer
{
   public class MerchChangeOwnershipDTO
    {
       public string FromMerchantId { get; set; }
       public string ToMerchantID { get; set; }
       public string FloatAcctInd { get; set; }
       public string CreationDate { get; set; }

       public string CurrentBusnNo { get; set; }
       public string CurrentSiteId { get; set; }
       public string NewSiteId { get; set; }
       public string CutOffDate { get; set; }
       public string CutOffTime { get; set; }
       public string BusnName { get; set; }
       public string TaxId { get; set; }
       public string DBAName { get; set; }
       public string DBAState { get; set; }
       public string CoRegNo { get; set; }
       public string CoRegName { get; set; }
       public string DealerName { get; set; }
       public string DealerContact { get; set; }
       public string PayeeName { get; set; }
       public string BankName { get; set; }
       public string BankAcctType { get; set; }
       public string BankBranchCd { get; set; }
       public string BankAcctNo { get; set; }
       public string SapNo { get; set; }
       public string MaskedFlag { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Application
{
   public class GetAcctSignUpDTO
    {
       public int ApplId { get; set; }
       public Int64? AccountNo { get; set; }
       public string CompanyName { get; set; }
       public string CorporateAccount { get; set; }
       public string ApplRef { get; set; }
       public decimal CreditLimit { get; set; }
       public string PendingReason { get; set; }
       public string UserId { get; set; }
       public string CreationDate { get; set; }
       public DateTime? ApprovedDate { get; set; }
       public DateTime? ReceivedDate { get; set; }
       public DateTime? RejectedDate { get; set; }
       public string AppvCd { get; set; }
       public string PreferredLanguage { get; set; }
       public string Website { get; set; }
    }
}

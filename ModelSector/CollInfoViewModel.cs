using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector
{
   public class CollInfoViewModel
    {
        [DisplayName("Payment Term")]
        public string PaymentTerm { get; set; }
        [DisplayName("Dunning Code")]
        public string DunningCode { get; set; }
        [DisplayName("Permanent Credit Limit")]
        public string PermanentCreditLimit { get; set; }
        [DisplayName("Temporary Credit Limit")]
        public string TempCreditLimit { get; set; }
        [DisplayName("Total TAR")]
        public string TotalTAR { get; set; }
        [DisplayName("Outstanding Amount")]
        public string OutstandingAmt { get; set; }
        [DisplayName("Overdue Amount")]
        public string OverdueAmt { get; set; }
        [DisplayName("Age Code")]
        public string AgeCode { get; set; }
        [DisplayName("Delinquent Days")]
        public string DelinquentDays { get; set; }
        [DisplayName("Latest Notification Sent On")]
        public string LatestNotificationSentOn { get; set; }
    }
}

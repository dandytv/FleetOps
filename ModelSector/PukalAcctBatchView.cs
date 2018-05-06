using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector
{
   public class PukalAcctBatchView
    {
        [DisplayName("Account No")]
        public long AcctNo { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("Sales Amount")]
        public string SalesAmt { get; set; }
        [DisplayName("Pukal Sedut")]
        public string SedutAmt { get; set; }
        [DisplayName("Payment Amount")]
        public string PaymentAmt { get; set; }
    }
}

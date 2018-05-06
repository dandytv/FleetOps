using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FleetSys.ViewModel
{
    public class CollPaymentHistViewModel
    {
            [DisplayName("Statement Date")]
            public string StatementDate { get; set; }
            [DisplayName("Due Date")]
            public string DueDate { get; set; }
            [DisplayName("Transaction Date")]
            public string TxnDate { get; set; }
            [DisplayName("Posting Date")]
            public string PostingDate { get; set; }
            [DisplayName("Transaction Description")]
            public string TxnDesc { get; set; }
            [DisplayName("Transaction Amount")]
            public string TxnAmt { get; set; }
            [DisplayName("Approval Code")]
            public string ApprovalCode { get; set; }       
    }
}
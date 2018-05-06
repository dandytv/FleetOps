using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModelSector
{
   public class PukalAcctBatchList
    {
        [DisplayName("Batch Id")]
        public long BatchId { get; set; }
        [DisplayName("Ref Cd")]
        public string RefCd { get; set; }
        [DisplayName("Cheque No")]
        public long ChequeNo { get; set; }
        [DisplayName("Cheque Amount")]
        public string ChequeAmt { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Status Description")]
        public string StsDescp { get; set; }
        [DisplayName("Statement Date")]
        public string StatementDate { get; set; }
        [DisplayName("Area Code")]
        public string AreaCode { get; set; }
        [DisplayName("Owner")]
        public string Owner { get; set; }

        [DisplayName("SlipNo")]
        public string SlipNo { get; set; }
        [DisplayName("IssBank")]
        public string IssBank { get; set; }
    }
}

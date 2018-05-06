using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Fraud
{
   public class FraudCaseDTO
    {
        public Int64 CaseNo { get; set; }
        public string FraudType { get; set; }
        public string AccountNo { get; set; }
        public string CompanyName { get; set; }
        public string Status { get; set; }
        public string CloseDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreationDate { get; set; }
    }
}

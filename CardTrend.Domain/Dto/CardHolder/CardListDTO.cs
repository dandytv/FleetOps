using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.CardHolder
{
   public class CardListDTO
    {
        public string CardNo { get; set; }
        public string EmbName { get; set; }
        public string Sts { get; set; }
        public DateTime? BlockedDate { get; set; }
        public string CardExpiry { get; set; }
        public Int64? XrefCardNo { get; set; }
        public string CardType { get; set; }
        public string PINInd { get; set; }
        public string VehRegsNo { get; set; }
        public decimal? SKDSQuota { get; set; }
        public string SKDSNo { get; set; }
        public string DriverCd { get; set; }
        public string DriverName { get; set; }
        public DateTime? TerminatedDate { get; set; }
        public string JoiningFeeCd { get; set; }
        public string AnnlFeeCd { get; set; }
        public string PrimaryCardNo { get; set; }
        public string CostCenter { get; set; }
    }
}

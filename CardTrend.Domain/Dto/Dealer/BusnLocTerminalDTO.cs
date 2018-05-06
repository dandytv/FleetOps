using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Dealer
{
    public class BusnLocTerminalDTO
    {
        public string TermId { get; set; }
        public string BusnLocation { get; set; }
        public string Sts { get; set; }
        public string Status { get; set; }
        public DateTime? DeployDate { get; set; }
        public string SaleTerritory { get; set; }
        public string ReplacedByTermId { get; set; }
        public DateTime? ReplacedDate { get; set; }
        public string ReasonCd { get; set; }
        public string Reason { get; set; }
        public string IPEK { get; set; }
        public DateTime? SettleFromTime { get; set; }
        public DateTime? SettleToTime { get; set; }
        public int? LastBatchId { get; set; }
        public Int64? SettleTxnId { get; set; }
        public string DeviceModel { get; set; }
        public string TermType { get; set; }
        public string SerialNo { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UserId { get; set; }
        public DateTime? LastUpdDate { get; set; }
    }
}

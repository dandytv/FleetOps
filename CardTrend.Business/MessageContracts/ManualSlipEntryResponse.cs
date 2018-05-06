using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.ManualSlipEntry;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class ManualSlipEntryResponse : ResponseBase
    {
        public ManualSlipEntryResponse()
        {
            manualSlipEntryBatchDetail = new ManualSlipEntry();
            manualTxnProducts = new List<ManualTxnProduct>();
            merchManualTxns = new List<ManualSlipEntry>();
        }
        public List<ManualTxnProduct> manualTxnProducts { get; set; }
        public IList<ManualSlipEntry> merchManualTxns { get; set; }
        public ManualSlipEntry manualSlipEntryBatchDetail { get; set; }

    }
}

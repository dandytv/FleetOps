using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelSector;
using CCMS.ModelSector;

namespace FleetOps.ViewModel
{
    public class PrepaidViewModel
    {
        public Prepaid _Prepaid { get; set; }
        public PurchaseOrder _PurchaseOrder { get; set; }
        public DeliveryAdvice _DeliveryAdvice { get; set; }
        public PrepaidCardnAcct _PrepaidCardnAcct { get; set; }
    }
}
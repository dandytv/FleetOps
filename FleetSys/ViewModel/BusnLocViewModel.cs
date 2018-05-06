using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CCMS.ModelSector;
using ModelSector;
using FleetSys.Models;

namespace FleetOps.ViewModel
{
    public class BusnLocViewModel
    {
        public MerchantDetails _MerchantDetails { get; set; }
        public ContactLstModel _ContactLstModel { get; set; }
        public AddrListMaintModel _AddrListMaintModel { get; set; }
        public ChangeStatus _ChangeStatus { get; set; }
        public BusnLocTerminal _BusnLocTerminal { get; set; }

    }
}
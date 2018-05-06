using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities.DAL;
using FleetOps.Models;
using CCMS.ModelSector;
using FleetOps.ViewModel;
using ModelSector;
using FleetSys.Models;

namespace FleetOps.ViewModel
{
    public class MerchantAcctSignUpViewModel 
    {
        public MA_GeneralInfo _AccountDetails { get; set; }
        public MerchantDetails _MerchantDetails { get; set; }
        public CardRangeAcceptance _CardAcceptance { get; set; }
        public ContactList _ContactList { get; set; }
        public AddrListMaintModel _AddressDtl { get; set; }
        public MerchantAcctMaint _merchantAcctMaint { get; set; }
    }

}
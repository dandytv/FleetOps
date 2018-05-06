using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities.DAL;
using FleetOps.Models;
using CCMS.ModelSector;
using FleetOps.ViewModel;

namespace FleetOps.ViewModel
{
    public class CAMaintnOverviewViewModel
    {
        public AccountDetails _AcctDetails { get; set; }
        public FinancialInfo _FinanceInfo { get; set; }
        public VelocityLimitnList _VelocityLimitnListAcct { get; set; }
        public CardDetailsnSelect _CardDetailsnSelect { get; set; }
        public VelocityLimitnList _VelocityLimitnListCard { get; set; }
        public VehicleDetails _VehicleDetails { get; set; }
        public ProductAcceptance _ProductAcceptance { get; set; }
        public LocationAcceptance _LocationAcceptance { get; set; }
        public ContactDetailsnList _ContactDetailsnListAcct { get; set; }
        public MerchAddress _AddressAccount { get; set; }
        public ContactDetailsnList _ContactDetailsnListCard { get; set; }
        public MerchAddress _AddressCard { get; set; }
        public TemporaryCreditnList _TemporaryCreditnList { get; set; }
        public MiscInfo _MiscInfo { get; set; }
        public ContactDetailsnList _ContactDetailsnListOthers { get; set; }
        public MerchAddress _AddressOthers { get; set; }
    }
}
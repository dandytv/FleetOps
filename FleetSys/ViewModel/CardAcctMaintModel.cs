using CCMS.ModelSector;
using FleetSys.Models;
using ModelSector;
namespace FleetOps.ViewModel
{
    public class AccountViewModel
    {
        public GeneralInfoModel _generalInfo { get; set; }
        public FinancialInfoModel _financialInfo { get; set; }
        public VelocityLimitnList _velocityLimitsByAcctList { get; set; }
        public CardHolderInfoModel _cardHolderInfo { get; set; }
        //public VelocityLimitsListModel _velocityLimitsListByCard { get; set; }
        public VehiclesListModel _vehiclesList { get; set; }
        public ProdAcceptListModel _prodAccptList { get; set; }
        public LocationAcceptance _LocAccptList { get; set; }
        public ContactLstModel _contactList { get; set; }
        public AddressList _addrListMaint { get; set; }
        public TempCreditCtrlModel _TempCreditControl { get; set; }
        public MiscellaneousInfoModel _miscellaneousInfo { get; set; }
        public EventLogger _eventLoggerInfo { get; set; }
        public PaymentTxn _pyTxn { get; set; }
        //public SKDS _SKDS { get; set; }

    }
}


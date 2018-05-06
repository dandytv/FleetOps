using CCMS.ModelSector;
using ModelSector;
namespace FleetSys.ViewModel
{
    public class AccountListModel
    {
        public GeneralInfoModel _generalInfo { get; set; }
        public FinancialInfoModel _financialInfo { get; set; }
        public VeloctyLimitListMaintModel _velocityLimitsByAcctList { get; set; }
        public CardHolderInfoModel _cardHolderInfo { get; set; }
        //public VelocityLimitsListModel _velocityLimitsListByCard { get; set; }
        public VehiclesListModel _vehiclesList { get; set; }
        public ProdAcceptListModel _prodAccptList { get; set; }
        public LocationAcceptListModel _LocAccptList { get; set; }
        public ContactLstModel _contactList { get; set; }
        public AddrListMaintModel _addrListMaint{ get; set; }
        public TempCreditCtrlModel _TempCreditControl { get; set; }
        public MiscellaneousInfoModel _miscellaneousInfo { get; set; }
        public EventLogger _eventLoggerInfo { get; set; }
        public PaymentTxn _pyTxn { get; set; }
        //public SKDS _SKDS { get; set; }

    }
}


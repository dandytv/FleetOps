using CCMS.ModelSector;
using FleetSys.Models;
using ModelSector;

namespace FleetOps.ViewModel
{
    public class CardAcctViewModel
    {
        public AcctSignUp _AcctSignUp { get; set; }
        public MerchAddress _AddressDtl { get; set; }
        public ContactDetailsnList _ContactDtl { get; set; }
        public VelocityLimitnList _VelocityDtl { get; set; }
        public CardDetailsnSelect _CardDtl { get; set; }
        public VehicleDetails _VehicleDtl { get; set; }
        public MiscInfo _MiscInfo { get; set; }
        public LocationAcceptance _LocationAcceptanceDtl { get; set; }
        public FinancialInfo _FinancilInfo { get; set; }
        public CardReplacement _CardReplacement { get; set; }
        public StatusMaintanance _StatusMaint { get; set; }
        public TxnAdjustment _TxnAdjustment { get; set; }
        public CreditAssesOperation  _CreditAssesOperation { get; set; }
    }
}
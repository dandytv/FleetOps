using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CCMS.ModelSector;
using ModelSector;

namespace FleetOps.ViewModel
{
    public class CardHolderViewModel
    {
        public MerchAddress _AddressDtl { get; set; }
        public ContactDetailsnList _ContactDtl { get; set; }
        public VelocityLimitnList _VelocityDtl { get; set; }
        public StatusMaintanance _StatusMaint { get; set; }
        public LocationAcceptance _LocationAcceptanceDtl { get; set; }
        public CardAppcInfoModel _cardApplInfoSignUp { get; set; }
        public CardAppcInfoModel _cardNewCard { get; set; }

    }
}
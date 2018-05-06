using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.Merchant;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class MechSignUpResponse : ResponseBase
    {
        public MechSignUpResponse()
        {
            merchGeneralInfo = new MA_GeneralInfo();
            merchantDetail = new MerchantDetails();
            merchChangeOwnerships = new List<MerchChangeOwnership>();
            merchProductPrizes = new List<MerchProductPrize>();
            busnLocTerminal = new BusnLocTerminal();
            busnLocTerminals = new List<BusnLocTerminal>();
            eService = new eService();
            eServices = new List<eService>();
            merchantDetails = new List<MerchantDetails>();
            merchPostedTxnSearches = new List<MerchPostedTxnSearch>();
            merchAgreements = new List<MA_GeneralInfo>();
            merchChangeOwnership = new MerchChangeOwnership();
        }
        public eService eService { get; set; }
        public List<eService> eServices { get; set; }
        public List<MerchPostedTxnSearch> merchPostedTxnSearches { get; set; }
        public List<MerchChangeOwnership> merchChangeOwnerships { get; set; }
        public MerchChangeOwnership merchChangeOwnership { get; set; }
        public List<MerchProductPrize> merchProductPrizes { get; set; }
        public MA_GeneralInfo merchGeneralInfo { get; set; }
        public BusnLocTerminal busnLocTerminal { get; set; }
        public List<BusnLocTerminal> busnLocTerminals { get; set; }
        public MerchantDetails merchantDetail { get; set; }
        public IList<MerchantDetails> merchantDetails { get; set; }
        public IList<MA_GeneralInfo> merchAgreements { get; set; }
    }
}

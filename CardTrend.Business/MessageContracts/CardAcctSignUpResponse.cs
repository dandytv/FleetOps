using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.Application;
using CardTrend.Domain.Dto.CardHolder;
using CardTrend.Domain.Dto.Corporate;
using CCMS.ModelSector;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class CardAcctSignUpResponse : ResponseBase
    {
        public CardAcctSignUpResponse()
        {
            acctSignUp = new AcctSignUp();
            skds = new SKDS();
            mileStoneInfo = new Milestone();
            veloctyLimit = new VeloctyLimitListMaintModel();
            contact = new ContactLstModel();
            vehicle = new VehiclesListModel();
            Address = new AddrListMaintModel();
            miscellaneousInfo = new MiscellaneousInfoModel();
            creditAssesOperation = new CreditAssesOperation();
            creditAssesOperations = new List<CreditAssesOperation>();
            milestoneHistories = new List<Milestone>();
            vehicles = new List<VehiclesListModel>();
            contacts = new List<ContactLstModel>();
            Addresses = new List<AddrListMaintModel>();
            skdses = new List<SKDS>();
            acctSignUps = new List<AcctSignUp>();
            veloctyLimits = new List<VeloctyLimitListMaintModel>();
            cards = new List<CardHolderInfoModel>();
        }
        public SKDS skds { get; set; }
        public Milestone mileStoneInfo { get; set; }
        public AcctSignUp acctSignUp { get; set; }
        public AddrListMaintModel Address { get; set; }
        public ContactLstModel contact { get; set; }
        public VehiclesListModel vehicle { get; set; }
        public VeloctyLimitListMaintModel veloctyLimit { get; set; }
        public CreditAssesOperation creditAssesOperation { get; set; }
        public MiscellaneousInfoModel miscellaneousInfo { get; set; }
        public List<AcctSignUp> acctSignUps { get; set; }
        public List<VeloctyLimitListMaintModel> veloctyLimits { get; set; }
        public List<VehiclesListModel> vehicles { get; set; }
        public List<ContactLstModel> contacts { get; set; }
        public List<SKDS> skdses { get; set; }
        public List<CreditAssesOperation> creditAssesOperations { get; set; }
        public List<AddrListMaintModel> Addresses { get; set; }
        public IList<Milestone> milestoneHistories { get; set; }
        public IList<CardHolderInfoModel> cards { get; set; }
    }
}

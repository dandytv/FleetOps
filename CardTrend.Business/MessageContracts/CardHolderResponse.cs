using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.CardHolder;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
   public class CardHolderResponse : ResponseBase
    {
       public CardHolderResponse()
       {
           cardHolderInfo = new CardHolderInfoModel();
           personalInfo = new PersonInfoModel();
           changeStatus = new ChangeStatus();
           cardReplacement = new CardReplacement();
           cardFinancialInfoModel = new CardFinancialInfoModel();
           cardHolderInfos = new List<CardHolderInfoModel>();
           locationAccepts = new List<LocationAcceptListModel>();
           locationAccept = new LocationAcceptListModel();
       }
       public CardHolderInfoModel cardHolderInfo { get; set; }
       public CardFinancialInfoModel cardFinancialInfoModel { get; set; }
       public PersonInfoModel personalInfo { get; set; }
       public LocationAcceptListModel locationAccept { get; set; }
       public ChangeStatus changeStatus { get; set; }
       public CardReplacement cardReplacement { get; set; }
       public IList<LocationAcceptListModel> locationAccepts { get; set; }
       public IList<CardHolderInfoModel> cardHolderInfos { get; set; }
    }
}

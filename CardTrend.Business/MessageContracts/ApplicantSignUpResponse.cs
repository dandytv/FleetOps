using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.Applicant;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class ApplicantSignUpResponse : ResponseBase
    {
        public ApplicantSignUpResponse()
        {
            cardAppcInfo = new CardAppcInfoModel();
            cardFinancialInfo = new CardFinancialInfoModel();
            cardAppcInfoLst = new List<CardAppcInfoModel>();
        }
        public CardAppcInfoModel cardAppcInfo { get;set;}
        public CardFinancialInfoModel cardFinancialInfo { get; set; }
        public List<CardAppcInfoModel> cardAppcInfoLst { get; set; }
    }
}

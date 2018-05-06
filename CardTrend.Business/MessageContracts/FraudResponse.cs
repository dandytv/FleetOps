using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.Fraud;
using CCMS.ModelSector;
using ModelSector.Fraud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
   public class FraudResponse : ResponseBase
    {
       public FraudResponse()
       {
           fraudCases = new List<FraudCaseListViewModel>();
           webFraudDetail = new FraudMainViewModel();
           fraudCard = new FraudCards();
           fraudCardDetails = new List<FraudCardDetailsViewModel>();
           fraudCustomerDetails = new List<FraudCustomerDetailsViewModel>();
           fraudTxnDisputes = new List<FraudTxnDisputeViewModel>();
       }
       public List<FraudCaseListViewModel> fraudCases { get; set; }
       public FraudMainViewModel webFraudDetail { get; set; }
       public FraudCards fraudCard { get; set; }
       public IList<FraudCardDetailsViewModel> fraudCardDetails { get; set; }
       public IList<FraudCustomerDetailsViewModel> fraudCustomerDetails { get; set; }
       public IList<FraudTxnDisputeViewModel> fraudTxnDisputes { get; set; }
    }
}

using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.Collection;
using ModelSector;
using ModelSector.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class GetAllAcctCollectionResponse : ResponseBase
    {
        public GetAllAcctCollectionResponse()
        {
            collectionTasks = new List<CollectionTaskListViewModel>();
            collAgeingHist = new List<CollAgeingHistDTO>();
            collectionHistories = new List<CollectionHistoryViewModel>();
            collInfoViewModel = new CollInfoViewModel();
            collectionFollowUps = new List<CollectionFollowUpViewModel>();
            CollectionAcctInfo = new CollectionAcctInfoViewModel();
            CollAgeingHists = new List<CollAgeingHistViewModel>();
            collPaymentHistViews = new List<CollPaymentHistViewModel>();
        }
        public IList<CollectionTaskListViewModel> collectionTasks { get; set; }
        public IList<CollPaymentHistViewModel> collPaymentHistViews { get; set; }
        public IList<CollAgeingHistDTO> collAgeingHist { get; set; }
        public IList<CollectionHistoryViewModel> collectionHistories { get; set; }
        public CollInfoViewModel collInfoViewModel { get; set; }
        public IList<CollectionFollowUpViewModel> collectionFollowUps { get; set; }
        public IList<CollAgeingHistViewModel> CollAgeingHists { get; set; }
        public CollectionAcctInfoViewModel CollectionAcctInfo { get; set; }
        public Int64 tOtalNoOfRecs { get; set; }
    }
}

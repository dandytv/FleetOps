using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.Collection;
using ModelSector;
using ModelSector.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
   public interface ICollectionOpService
   {
       Task<GetAllAcctCollectionResponse> GetAllAcctCollection(CollectionTaskListViewModel collectionTaskModel);
       Task<GetAllAcctCollectionResponse> GetThresholdLimitCollection(int offSet, Int64 noOfRecs, Int64 tOtalNoOfRecs, string sSearch);
       Task<GetAllAcctCollectionResponse> GetCollFollowUp(string eventId);
       Task<GetAllAcctCollectionResponse> GetCollectionAccountInfo(string accountId);
       Task<GetAllAcctCollectionResponse> GetCollAgeingHistory(string accountId);
       Task<GetAllAcctCollectionResponse> GetCollectionHistory(string acctNo, string collectionCaseSts);
       Task<GetAllAcctCollectionResponse> GetCollPaymentHist(string acctNo, int offSet, Int64 NoOfRecs);
       Task<GetAllAcctCollectionResponse> GetCollectionFinancialInfo(string acctNo);
       Task<SaveAcctSignUpResponse> SaveCollectionFollowUp(int eventId, string userId, string collectionSts, string priority, string recallDate, string remarks);
   }
   public class CollectionOpService : ICollectionOpService
   {
       private static Autofac.IContainer Container { get; set; }
       private static ICardTrendLogger Logger;
       public CollectionOpService()
       {
           RegisterDAOComponents();
           using (var scope = Container.BeginLifetimeScope())
           {
               Logger = scope.Resolve<ICardTrendLogger>();
           }
       }
       #region CollectionOpService registerComponent
       public static void RegisterDAOComponents()
       {
           var afBuilder = new ContainerBuilder();
           afBuilder.RegisterType<CollectionOpDAO>().As<ICollectionOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
           afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
           afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
           Container = afBuilder.Build();
       }
       #endregion
       #region CollectionOp Service
       /*************************************
         Created by:   Tuan Tran
         Created on:   Mar 6, 2017
         Function:     GetAllAcctCollection
         Purpose:      GetAllAcctCollection
         Inputs:       collectionTaskDTO class
         Returns:      GetAllAcctCollectionResponse
        *************************************/
       public async Task<GetAllAcctCollectionResponse> GetAllAcctCollection(CollectionTaskListViewModel collectionTaskModel)
       {
           Logger.Info("Invoking GetAllAcctCollection function");
           var response = new GetAllAcctCollectionResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var collectionTaskDAO = scope.Resolve<ICollectionOpDAO>();
                   var collectionTask = Mapper.Map<CollectionTaskListViewModel, CollectionTaskDTO>(collectionTaskModel);
                   var results = await collectionTaskDAO.GetAllAcctCollection(collectionTask);
                   if(results.Count() > 0)
                       response.collectionTasks = Mapper.Map<IList<CollectionTaskDTO>,IList<CollectionTaskListViewModel>>(results);
               }
               response.Status = ResponseStatus.Success;
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetAllAcctCollection: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
         Created by:   Tuan Tran
         Created on:   Mar 6, 2017
         Function:     GetCollPaymentHist
         Purpose:      GetCollPaymentHist
         Inputs:       acctNo,offSet,NoOfRecs,tOtalNoOfRecs
         Returns:      GetAllAcctCollectionResponse
        *************************************/
       public async Task<GetAllAcctCollectionResponse> GetCollPaymentHist(string acctNo, int offSet, Int64 NoOfRecs)
       {
           Logger.Info("Invoking GetCollPaymentHist function");
           var response = new GetAllAcctCollectionResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var collectionTaskDAO = scope.Resolve<ICollectionOpDAO>();
                   //var collectionTask = Mapper.Map<CollectionTaskListViewModel, CollectionTaskDTO>(collectionTaskModel);
                   var results = await collectionTaskDAO.GetCollPaymentHist(acctNo, offSet, NoOfRecs);
                   if (results.CollPaymentHsts.Count() > 0)
                   {
                       response.collPaymentHistViews = Mapper.Map<IList<CollPaymentHistDTO>, IList<CollPaymentHistViewModel>>(results.CollPaymentHsts);
                       response.tOtalNoOfRecs = results.TotalNoOfRecs;
                   }
               }
               response.Status = ResponseStatus.Success;
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetCollPaymentHist: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
         Created by:   dandy boy
         Created on:    april 25, 2017
         Function:     GetThresholdLimitCollection
         Purpose:      GetThresholdLimitCollection
         Inputs:       offSet,noOfRecs,tOtalNoOfRecs,sSearch
         Returns:      GetAllAcctCollectionResponse
       *************************************/
       public async Task<GetAllAcctCollectionResponse> GetThresholdLimitCollection(int offSet, Int64 noOfRecs, Int64 tOtalNoOfRecs, string sSearch)
       {
           Logger.Info("Invoking GetThresholdLimitCollection function");
           var response = new GetAllAcctCollectionResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var collectionTaskDAO = scope.Resolve<ICollectionOpDAO>();
                   var result = await collectionTaskDAO.GetThresholdLmtCollection(offSet,noOfRecs,tOtalNoOfRecs,sSearch);
                   if(result != null)
                   {
                       response.collectionTasks = Mapper.Map<IList<DelinquentAcctsTresholdLimitDTO>, IList<CollectionTaskListViewModel>>(result.delinquentAcctsTresholdLimits);
                       response.tOtalNoOfRecs = result.tOtalNoOfRecs;
                   }
               }
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetThresholdLimitCollection: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
         Created by:   dandy boy
         Created on:   april 25, 2017
         Function:     GetCollFollowUp
         Purpose:      GetCollFollowUp
         Inputs:       eventId
         Returns:      GetAllAcctCollectionResponse
       *************************************/
       public async Task<GetAllAcctCollectionResponse> GetCollFollowUp(string eventId)
       {
           Logger.Info("Invoking GetCollFollowUp function");
           var response = new GetAllAcctCollectionResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var collectionTaskDAO = scope.Resolve<ICollectionOpDAO>();
                   var results = await collectionTaskDAO.GetCollFollowUp(eventId);
                   if(results.Count() > 0)
                       response.collectionFollowUps = Mapper.Map<List<CollectionFollowUpDTO>, List<CollectionFollowUpViewModel>>(results);
               }
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetCollFollowUp: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
         Created by:   dandy boy
         Created on:   april 25, 2017
         Function:     GetCollectionAccountInfo
         Purpose:      GetCollectionAccountInfo
         Inputs:       accountId
         Returns:      GetAllAcctCollectionResponse
       *************************************/
       public async Task<GetAllAcctCollectionResponse> GetCollectionAccountInfo(string accountId)
       {
           Logger.Info("Invoking GetCollectionAccountInfo function");
           var response = new GetAllAcctCollectionResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var collectionTaskDAO = scope.Resolve<ICollectionOpDAO>();
                   var result = await collectionTaskDAO.GetCollAcctInfo(accountId);
                   if(result != null)
                       response.CollectionAcctInfo = Mapper.Map<DelinquentAcctInfoDTO,CollectionAcctInfoViewModel>(result);
               }
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetCollectionAccountInfo: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
         Created by:   dandy boy
         Created on:   april 25, 2017
         Function:     GetCollAgeingHistory
         Purpose:      GetCollAgeingHistory
         Inputs:       accountId
         Returns:      GetAllAcctCollectionResponse
       *************************************/
       public async Task<GetAllAcctCollectionResponse> GetCollAgeingHistory(string accountId)
       {
           Logger.Info("Invoking GetCollAgeingHistory function");
           var response = new GetAllAcctCollectionResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var collectionTaskDAO = scope.Resolve<ICollectionOpDAO>();
                   var result = await collectionTaskDAO.GetCollAgeingHistory(accountId);
                   if (result.Count() > 0)
                       response.CollAgeingHists = Mapper.Map<List<CollAgeingHistDTO>,List<CollAgeingHistViewModel>>(result);
               }
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetCollAgeingHistory: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
         Created by:   dandy boy
         Created on:   april 26, 2017
         Function:     SaveCollectionFollowUp
         Purpose:      SaveCollectionFollowUp
         Inputs:       issNo,eventId,userId,collectionSts,priority,recallDate,remarks
         Returns:      SaveAcctSignUpResponse
       *************************************/
       public async Task<SaveAcctSignUpResponse> SaveCollectionFollowUp(int eventId, string userId, string collectionSts, string priority, string recallDate, string remarks)
       {
           Logger.Info("Invoking SaveCollectionFollowUp function");
           var response = new SaveAcctSignUpResponse()
           {
               Status = ResponseStatus.Failure,
           };
           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var collectionTaskDAO = scope.Resolve<ICollectionOpDAO>();
                   var controlDAO = scope.Resolve<IControlDAO>();
                   var result = await collectionTaskDAO.SaveCollectionFollowUp(eventId,userId,collectionSts,priority,recallDate,remarks);
                   var message = await controlDAO.GetMessageCode(result);
                   response.desp = message.Descp;
                   response.flag = message.Flag;
               }
               response.Status = ResponseStatus.Success;
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in SaveCollectionFollowUp: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.flag = 1;
               response.Message = msg;
           }
           return response;
       }
         /*************************************
         Created by:   dandy boy
         Created on:   april 28, 2017
         Function:     GetCollectionHistory
         Purpose:      GetCollectionHistory
         Inputs:       acctNo,collectionCaseSts
         Returns:      GetAllAcctCollectionResponse
         *************************************/
       public async Task<GetAllAcctCollectionResponse> GetCollectionHistory(string acctNo,string collectionCaseSts)
       {
           Logger.Info("Invoking GetCollectionHistory function");
           var response = new GetAllAcctCollectionResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var collectionTaskDAO = scope.Resolve<ICollectionOpDAO>();
                   var results = await collectionTaskDAO.GetCollHistory(acctNo, collectionCaseSts);
                   if(results.Count() > 0)
                       response.collectionHistories = Mapper.Map<List<CollectionHistoryDTO>,List<CollectionHistoryViewModel>>(results);
               }
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetCollectionHistory: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
         Created by:   Tuan Tran
         Created on:   May 11, 2017
         Function:     GetCollectionFinancialInfo
         Purpose:      GetCollectionFinancialInfo
         Inputs:       acctNo
         Returns:      GetAllAcctCollectionResponse
       *************************************/
       public async Task<GetAllAcctCollectionResponse> GetCollectionFinancialInfo(string acctNo)
       {
           Logger.Info("Invoking GetCollectionFinancialInfo function");
           var response = new GetAllAcctCollectionResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var collectionTaskDAO = scope.Resolve<ICollectionOpDAO>();
                   var result = await collectionTaskDAO.GetCollFinancialInfo(acctNo);
                   if(result != null)
                       response.collInfoViewModel = Mapper.Map<DelinquentAcctFinancialInfoDTO,CollInfoViewModel>(result);
               }
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetCollectionFinancialInfo: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       #endregion
   }
}

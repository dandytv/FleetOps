using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Extensions;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.Fraud;
using CCMS.ModelSector;
using ModelSector.Fraud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Business.CcmsServices
{
   public interface IFraudOpService
    {
        Task<FraudResponse> GetFraudCases();
        Task<FraudResponse> GetFraudByEventId(string eventId);
        Task<FraudResponse> FraudCustomerDetailsByacctNoEventId(string acctNo, string eventId);
        Task<FraudResponse> GetCardNoListByAcctNo(int ind, string dropDown, string showList, string acctNo, string eventId);
        Task<FraudResponse> GetFraudCardDetailsList(List<string> fraudCards, string account, string eventId, FraudCards fraudCardModel);
        Task<FraudResponse> GetCardNoByAcctNo(int ind, string dropDown, string showList, string acctNo, string eventId);
        Task<IList<SelectListItem>> GetFraudDropdown(int ind, string dropDown, string showList, Int64? eventID, Int64? acctNo);
        Task<FraudResponse> GetFraudDisputeTxnSearch(Int64 eventID, int searchType, string txnCategory, string txnCode, string acctNo, string cardNo, string fromDate, string toDate);
        Task<SaveAcctSignUpResponse> SaveTransaction(List<string> liTxnId, string eventId, string acctNo, string cardNo, string isPostedDispute);
        Task<SaveAcctSignUpResponse> SaveFraud(FraudCustomerDetailsViewModel fraudCustomerDetail, FraudCardDetailsViewModel fraudCardDetail, FraudIncidentsViewModel webFraudDetail, string userId);
    }
    public class FraudOpService:IFraudOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public FraudOpService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region CardAcctSignUpService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<FraudOpDAO>().As<IFraudOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region PukalAcctService
        /*************************************
         Created by:   Tuan Tran
         Created on:   april 3, 2017
         Function:     GetFraudCases
         Purpose:      GetFraudCases
         Inputs:       
         Returns:      PukalAcctResponse
        *************************************/
        public async Task<FraudResponse> GetFraudCases()
        {
            Logger.Info("Invoking GetFraudCases fuction use EF to call SP");
            var response = new FraudResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    var ftFrauds = await pukalAcctDAO.FtFraudCaseList();
                    if(ftFrauds.Count() > 0)
                        response.fraudCases = Mapper.Map<List<FraudCaseDTO>,List<FraudCaseListViewModel>>(ftFrauds);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFraudCases: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   april 4, 2017
         Function:     GetFraudByEventId
         Purpose:      GetFraudByEventId
         Inputs:       eventId
         Returns:      PukalAcctResponse
        *************************************/
        public async Task<FraudResponse> GetFraudByEventId(string eventId)
        {
            Logger.Info("Invoking GetFraudByEventId fuction use EF to call SP");
            var response = new FraudResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    var fraudDetail = await pukalAcctDAO.GetFraudByEventId(eventId);
                    if(fraudDetail != null)
                    {
                        fraudDetail.EventId = eventId;
                        response.webFraudDetail = Mapper.Map<WebFraudDetailDTO,FraudMainViewModel>(fraudDetail);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFraudByEventId: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
         Created by:   Dandy Boy
         Created on:   april 6, 2017
         Function:     FraudCustomerDetailsByacctNoEventId
         Purpose:      FraudCustomerDetailsByacctNoEventId
         Inputs:       acctNo,eventId
         Returns:      PukalAcctResponse
       *************************************/
        public async Task<FraudResponse> FraudCustomerDetailsByacctNoEventId(string acctNo, string eventId)
        {
            Logger.Info("Invoking FraudCustomerDetails fuction use EF to call SP");
            var response = new FraudResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    var fraudDetail = await pukalAcctDAO.FraudCustomerDetailsList(acctNo, eventId);
                    if(fraudDetail.Count() > 0)
                    {
                        IList<FraudCustomerDetailsDTO> FraudCustomerDetails = new List<FraudCustomerDetailsDTO>();

                        if (fraudDetail.Count() > 0)
                        {
                            foreach (var item in fraudDetail)
                            {
                                decimal avgSales = 0;
                                avgSales = ((Convert.ToDecimal(item.Month1Amount) + Convert.ToDecimal(item.Month2Amount) + Convert.ToDecimal(item.Month3Amount) + Convert.ToDecimal(item.Month4Amount) + Convert.ToDecimal(item.Month5Amount) + Convert.ToDecimal(item.Month6Amount)) / 6);
                                FraudCustomerDetails.Add(new FraudCustomerDetailsDTO
                                {
                                    SubsidyNo = item.SubsidyNo,
                                    AccountNo = item.AccountNo,
                                    CompanyName = item.CompanyName,
                                    AccountType = item.AccountType,
                                    ClientType = item.ClientType,
                                    AgingDays = item.AgingDays,
                                    Month1Amount = item.Month1Amount,
                                    Month2Amount = item.Month2Amount,
                                    Month3Amount = item.Month3Amount,
                                    Month4Amount = item.Month4Amount,
                                    Month5Amount = item.Month5Amount,
                                    Month6Amount = item.Month6Amount,
                                    Month1Date = item.Month1Date,
                                    Month2Date = item.Month2Date,
                                    Month3Date = item.Month3Date,
                                    Month4Date = item.Month4Date,
                                    Month5Date = item.Month5Date,
                                    Month6Date = item.Month6Date,
                                    AvgSales = avgSales,
                                    AvgSalesDisplay = avgSales != 0 ? "Average Sales" : string.Empty
                                });
                            }
                        }
                        response.fraudCustomerDetails = Mapper.Map<IList<FraudCustomerDetailsDTO>,IList<FraudCustomerDetailsViewModel>>(FraudCustomerDetails);
                    }
                    
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in FraudCustomerDetails: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Dandy Boy
        Created on:   april 6, 2017
        Function:     GetCardNoListByAcctNo
        Purpose:      GetCardNoListByAcctNo
        Inputs:       ind,dropDown,showList,acctNo,eventId
        Returns:      PukalAcctResponse
        *************************************/
        public async Task<FraudResponse> GetCardNoListByAcctNo(int ind, string dropDown, string showList, string acctNo, string eventId)
        {
            Logger.Info("Invoking GetCardNoListByAcctNo fuction use EF to call SP");
            var response = new FraudResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    var fraudDetails = await pukalAcctDAO.GetCardNoListByAcctNo(ind, dropDown, showList, acctNo, eventId);
                    if(fraudDetails.Count() > 0)
                    {
                        var cardNoLst = new List<string>();
                        foreach(var item in fraudDetails)
                        {
                             cardNoLst.Add(item.CardNo.ToString());
                        }
                        response.fraudCard.CardNo = cardNoLst;
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCardNoListByAcctNo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Dandy Boy
         Created on:   May 12, 2017
         Function:     GetFraudCardDetailsList
         Purpose:      GetFraudCardDetailsList
         Inputs:       fraudCards,account,eventId,fraudCardPr
         Returns:      FraudResponse
        *************************************/
        public async Task<FraudResponse> GetFraudCardDetailsList(List<string> fraudCards, string account, string eventId, FraudCards fraudCardModel)
        {
            Logger.Info("Invoking GetFraudCardDetailsList fuction use EF to call SP");
            var response = new FraudResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    var fraudCardPr = Mapper.Map<FraudCards, FraudCardDTO>(fraudCardModel);
                    var fraudCardDetails = await pukalAcctDAO.FraudCardDetailsList(fraudCards, account, eventId);
                    var fraudCardDetailModels = new List<FraudCardDetailDTO>();
                    if(fraudCardDetails.Count() > 0)
                    {
                        foreach(var fraud in fraudCardDetails)
                        {
                            var avgSales = ((Convert.ToDecimal(fraud.Month1Amount) + Convert.ToDecimal(fraud.Month2Amount) + Convert.ToDecimal(fraud.Month3Amount) + Convert.ToDecimal(fraud.Month4Amount) + Convert.ToDecimal(fraud.Month5Amount) + Convert.ToDecimal(fraud.Month6Amount))/6);
                            FraudCardDetailDTO fraudCardDetail = new FraudCardDetailDTO();
                            FraudCardDTO fraudCard = new FraudCardDTO();
                            fraudCard.SelectedCardNo = Convert.ToString(fraud.CardNo);
                            fraudCard.AcctNo = account;
                            fraudCard.EventId = Convert.ToString(eventId);
                            fraudCard.CardNo = fraudCardPr.CardNo;
                            fraudCardDetail.AvgSales = avgSales;
                            fraudCardDetail.FraudCards.Add(fraudCard);
                            fraudCardDetail.Month1Amount = fraud.Month1Amount;
                            fraudCardDetail.Month1Date = fraud.Month1Date;
                            fraudCardDetail.Month2Amount = fraud.Month2Amount;
                            fraudCardDetail.Month2Date = fraud.Month2Date;
                            fraudCardDetail.Month3Amount = fraud.Month3Amount;
                            fraudCardDetail.Month3Date = fraud.Month3Date;
                            fraudCardDetail.Month4Amount = fraud.Month4Amount;
                            fraudCardDetail.Month4Date = fraud.Month4Date;
                            fraudCardDetail.Month5Amount = fraud.Month5Amount;
                            fraudCardDetail.Month5Date = fraud.Month5Date;
                            fraudCardDetail.Month6Amount = fraud.Month6Amount;
                            fraudCardDetail.Month6Date = fraud.Month6Date;

                            fraudCardDetail.CardAvgSalesDisplay = !String.IsNullOrEmpty((avgSales).ToString("0.00")) ? "Average Sales" : string.Empty;
                            fraudCardDetail.SingleTxn = fraud.SingleTxn;
                            fraudCardDetail.LitLimit = fraud.LitLimit;

                            fraudCardDetail.DailyTxn = fraud.DailyTxn;
                            fraudCardDetail.DailyLitre = fraud.DailyLitre;
                            fraudCardDetail.DailyCnt = fraud.DailyCnt;

                            fraudCardDetail.MonthlyTxn = fraud.MonthlyTxn;
                            fraudCardDetail.MonthlyLitre = fraud.MonthlyLitre;
                            fraudCardDetail.MonthlyCnt = fraud.MonthlyCnt;

                            fraudCardDetailModels.Add(fraudCardDetail);
                        }
                        response.fraudCardDetails = Mapper.Map<IList<FraudCardDetailDTO>,IList<FraudCardDetailsViewModel>>(fraudCardDetailModels);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFraudCardDetailsList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   May 15, 2017
        Function:     GetCardNoByAcctNo
        Purpose:      GetCardNoByAcctNo
        Inputs:       ind,dropDown,showList,acctNo,eventId
        Returns:      FraudResponse
        *************************************/
        public async Task<FraudResponse> GetCardNoByAcctNo(int ind, string dropDown, string showList, string acctNo, string eventId)
        {
            Logger.Info("Invoking GetCardNoByAcctNo fuction use EF to call SP");
            var response = new FraudResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    var fraudDetail = await pukalAcctDAO.GetCardNoByAcctNo(ind, dropDown, showList, acctNo, eventId);
                    FraudCardDTO fraudCardDTO = new FraudCardDTO();
                    var liCardNo = new List<string>();
                    if(fraudDetail.Count() > 0)
                    {
                        foreach(var fraud in fraudDetail)
                        {
                            liCardNo.Add(Convert.ToString(fraud.CardNo));
                        }
                    }
                    fraudCardDTO.CardNo = liCardNo;
                    if (fraudCardDTO != null)
                        response.fraudCard = Mapper.Map<FraudCardDTO,FraudCards>(fraudCardDTO);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCardNoByAcctNo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   May 18, 2017
        Function:     GetFraudDisputeTxnSearch
        Purpose:      GetFraudDisputeTxnSearch
        Inputs:       eventID,searchType,txnCategory,txnCode,acctNo,cardNo,fromDate,toDate
        Returns:      FraudResponse
        *************************************/
        public async Task<FraudResponse> GetFraudDisputeTxnSearch(Int64 eventID, int searchType, string txnCategory, string txnCode, string acctNo, string cardNo, string fromDate, string toDate)
        {
            Logger.Info("Invoking GetFraudDisputeTxnSearch fuction use EF to call SP");
            var response = new FraudResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    int? txnCat = null, txnCd = null;
                    if (!string.IsNullOrEmpty(txnCategory))
                        txnCat = Convert.ToInt32(txnCategory);
                    if(!string.IsNullOrEmpty(txnCode))
                        txnCd = Convert.ToInt32(txnCode);

                    var fraudDisputeTxn = await pukalAcctDAO.GetFraudTxnSearch(eventID, searchType, txnCat, txnCd, acctNo, cardNo, fromDate, toDate);
                    if (fraudDisputeTxn.Count() > 0)
                        response.fraudTxnDisputes = Mapper.Map<IList<FraudTxnDisputeDTO>,IList<FraudTxnDisputeViewModel>>(fraudDisputeTxn);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFraudDisputeTxnSearch: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 18, 2017
        Function:     SaveTransaction
        Purpose:      SaveTransaction
        Inputs:       TxnAdjustmentDTO
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveTransaction(List<string> liTxnId, string eventId, string acctNo, string cardNo, string isPostedDispute)
        {
            Logger.Info("Invoking SaveTransaction function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await pukalAcctDAO.SaveTxn(liTxnId, eventId, acctNo, cardNo, isPostedDispute);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveTransaction: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 18, 2017
        Function:     SaveFraud
        Purpose:      SaveFraud
        Inputs:       FraudCustomerDetailsDTO,FraudCardDetailDTO,WebFraudDetailDTO,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveFraud(FraudCustomerDetailsViewModel fraudCustomerDetail, FraudCardDetailsViewModel fraudCardDetail, FraudIncidentsViewModel webFraudDetail, string userId)
        {
            Logger.Info("Invoking SaveFraud function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var fraudCustomerDetailDto = Mapper.Map<FraudCustomerDetailsViewModel, FraudCustomerDetailsDTO>(fraudCustomerDetail);
                    var fraudCardDetailDto = Mapper.Map<FraudCardDetailsViewModel, FraudCardDetailDTO>(fraudCardDetail);
                    var webFraudDetailDto = Mapper.Map<FraudIncidentsViewModel, WebFraudDetailDTO>(webFraudDetail);
                    var result = await pukalAcctDAO.SaveFraud(fraudCustomerDetailDto, fraudCardDetailDto, webFraudDetailDto, userId);
                    var message = await controlDAO.GetMessageCode(result.Flag);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                    response.returnValue.BatchId = result.paraOut.BatchId;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveFraud: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   April 21, 2017
        Function:     GetFraudDropdown
        Purpose:      GetFraudDropdown
        Inputs:       ind,dropDown,showList,eventID,acctNo
        Returns:      List of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetFraudDropdown(int ind, string dropDown, string showList, Int64? eventID, Int64? acctNo)
        {
            Logger.Info("Invoking GetFraudDropdown function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctDAO = scope.Resolve<IFraudOpDAO>();
                    var results = await pukalAcctDAO.GetFraudDropdown(ind,dropDown,showList,eventID,acctNo);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFraudDropdown: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        #endregion
    }
}

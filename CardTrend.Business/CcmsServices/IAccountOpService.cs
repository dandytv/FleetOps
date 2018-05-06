using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.Corporate;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
    public interface IAccountOpService
    {
        Task<GeneralInfoResponse> GetGeneralAccountInfo(int accountNo);
        Task<CreditAssesOperationListResponse> GetAcctDepositInfos(string applid = null, string acctNo = null, string corpCd = null);
        Task<AccountOpResponse> GetFinancialInfoForm(int accountNo);
        Task<AccountOpResponse> GetCostCentres(string refTo, string refKey);
        Task<AccountOpResponse> GetTxnInstants(string acctNo);
        Task<AccountOpResponse> GetOnlineTransactionList(string acctNo, int flag);
        Task<AccountOpResponse> GetTxnInstantUnposts(string acctNo);
        Task<AccountOpResponse> GetWebCostCentreSearch(string refTo, string refKey, string costCentre);
        Task<AccountOpResponse> GetCostCentreByRefToAndCostCentre(string refTo, string refKey, string costCentre);
        Task<SaveGeneralInfoResponse> SaveGeneralAccountInfo(GeneralInfoModel generalAcctInfoModel);
        Task<SaveGeneralInfoResponse> SaveAcctDepositInfoMaint(CreditAssesOperation creditDepositInfoModel, string applId = null, string corpCd = null);
        Task<SaveAcctSignUpResponse> SaveCostCentreMaint(CostCentre costCentre, string userId);
        Task<SaveGeneralInfoResponse> SaveFinancialInfoMaint(FinancialInfoModel financialInfoModel, string userId);
        Task<SaveAcctSignUpResponse> SaveCollectionCaseInfo(string accountNo, string collector);
        Task<SaveGeneralInfoResponse> DeleteLocationAcceptance(string acctNo, List<string> busnLocation, string cardNo, string userId);
        Task<SaveAcctSignUpResponse> SaveTempCreditCtrlMaint(string acctNo, string creditLimit, string effDateFrom, string effDateTo, string userId);
        Task<SaveAcctSignUpResponse> SaveProductDiscount(ProductDiscount productDiscountModel, string acctNo, string func, string refTo);
        Task<SaveAcctSignUpResponse> DeleteProductDiscount(ProductDiscount productDiscountModel, string acctNo, string refTo);
        Task<AccountOpResponse> GetCreditApplAssessentForm(string acctNo, string applId);
        Task<AccountOpResponse> GetSecDepRemarks(string accountNo, string eventType, string txnId);
        Task<AccountOpResponse> GetEventSearchWithoutDate(string refKey, string eventType, string eventDate);
        Task<AccountOpResponse> GetAcctHistoryByAccount(Int64 acctNo);
        Task<AccountOpResponse> GetSecHistoryDepositByAccount(string acctNo);
        Task<AccountOpResponse> GetTempCreditLimitDetail(string acctNo);
        Task<AccountOpResponse> GetFtUpToBalDetail(string acctNo);
        Task<AccountOpResponse> GetProductDiscounts(string acctNo, string discType, string refTo);
        Task<AccountOpResponse> GetProductDiscount(string acctNo, string discType, string id, string refTo);
        Task<AccountOpResponse> GetEventLoggerDetail(string module, string eventId);
        Task<AccountOpResponse> GetEventlist(string module, string accountNo, string busnLocation);
        Task<AccountOpResponse> GetEventSearch(EventLogger eventLog);
        Task<AccountOpResponse> GetPaymentTxnList(string acctNo);
        Task<AccountOpResponse> GetPaymentTxnDetail(string txnId);
        Task<AccountOpResponse> SearchBillingItem(string accountNo, string fromDate, string toDate, string TxnCategory, string sts);
    }
    public class AccountOpService: IAccountOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public AccountOpService()
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
            afBuilder.RegisterType<AccountOpDAO>().As<IAccountOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardAcctSignUpDAO>().As<ICardAcctSignUpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region AccountOp Service
        public async Task<GeneralInfoResponse> GetGeneralAccountInfo(int accountNo)
        {
            Logger.Info("Invoking GetGeneralAccountInfo function");
            var response = new GeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountInfoDAO = scope.Resolve<IAccountOpDAO>();
                    var result = await accountInfoDAO.FtGeneralInfo(accountNo);
                    if(result != null)
                        response.generalInfo = Mapper.Map<GeneralInfoModel>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetGeneralAccountInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 20, 2017
        Function:     SaveGeneralAccountInfo
        Purpose:      SaveGeneralAccountInfo
        Inputs:       GeneralInfoDTO
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveGeneralInfoResponse> SaveGeneralAccountInfo(GeneralInfoModel generalAcctInfoModel)
        {
            Logger.Info("Invoking SaveGeneralAccountInfo function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountInfoDAO = scope.Resolve<IAccountOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var generalAcctInfo = Mapper.Map<GeneralInfoModel, GeneralInfoDTO>(generalAcctInfoModel);
                    var result = await accountInfoDAO.SaveGeneralInfoMaint(generalAcctInfo);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveGeneralAccountInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 20, 2017
        Function:     GetAcctDepositInfos
        Purpose:      GetAcctDepositInfos
        Inputs:       applid,acctNo,corpCd
        Returns:      AccountOpResponse
        *************************************/
        public async Task<CreditAssesOperationListResponse> GetAcctDepositInfos(string applid = null, string acctNo = null, string corpCd = null)
        {
            Logger.Info("Invoking GetAcctDepositInfos function");
            var response = new CreditAssesOperationListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountInfoDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountInfoDAO.FtAcctDepositInfoList(applid, acctNo, corpCd);
                    if(results.Count() > 0)
                      response.creditAssesOperationLst = Mapper.Map<IList<CreditAssesOperationDTO>,IList<CreditAssesOperation>>(results);

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctDepositInfos: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 23, 2017
        Function:     SaveAcctDepositInfoMaint
        Purpose:      SaveAcctDepositInfoMaint
        Inputs:       CreditAssesOperation,applId,corpCd
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveGeneralInfoResponse> SaveAcctDepositInfoMaint(CreditAssesOperation creditDepositInfoModel, string applId = null, string corpCd = null)
        {
            Logger.Info("Invoking SaveAcctDepositInfoMaint function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var creditDepositInfo = Mapper.Map<CreditAssesOperation, CreditAssesOperationDTO>(creditDepositInfoModel);
                    var result = await accountOpDAO.SaveAcctDepositInfoMaint(creditDepositInfo,applId,corpCd);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveAcctDepositInfoMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Feb 23, 2017
         Function:     GetFinancialInfoForm
         Purpose:      GetFinancialInfoForm
         Inputs:       accountNo
         Returns:      AccountOpResponse
         *************************************/
        public async Task<AccountOpResponse> GetFinancialInfoForm(int accountNo)
        {
            Logger.Info("Invoking GetFinancialInfoForm function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountInfoDAO = scope.Resolve<IAccountOpDAO>();
                    var result = await accountInfoDAO.FtFinancialInfoForm(accountNo);
                    if(result != null)
                        response.financialInfo = Mapper.Map<FinancialInfoDTO,FinancialInfoModel>(result);

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFinancialInfoForm: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 14, 2017
        Function:     GetCostCentres
        Purpose:      GetCostCentres
        Inputs:       refTo,refKey
        Returns:      AccountOpResponse
       *************************************/
        public async Task<AccountOpResponse> GetCostCentres(string refTo, string refKey)
        {
            Logger.Info("Invoking GetCostCentres function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountInfoDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountInfoDAO.GetCostCentres(refTo,refKey);
                    if(results.Count() >  0)
                        response.costCentres = Mapper.Map<List<CostCentreDTO>,List<CostCentre>>(results);

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCostCentres: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 14, 2017
        Function:     GetCostCentreByRefToAndCostCentre
        Purpose:      GetCostCentreByRefToAndCostCentre
        Inputs:       refTo,refKey,costCentre
        Returns:      AccountOpResponse
       *************************************/
        public async Task<AccountOpResponse> GetCostCentreByRefToAndCostCentre(string refTo, string refKey, string costCentre)
        {
            Logger.Info("Invoking GetCostCentreByRefToAndCostCentre function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountInfoDAO = scope.Resolve<IAccountOpDAO>();
                    var result = await accountInfoDAO.GetCostCentreByRefToAndCostCentre(refTo, refKey,costCentre);
                    if(result != null)
                    {
                        response.costCentre = Mapper.Map<CostCentreDTO,CostCentre>(result);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCostCentreByRefToAndCostCentre: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   dandy
        Created on:  June 14, 2017
        Function:     SaveCostCentreMaint
        Purpose:      SaveCostCentreMaint
        Inputs:       CostCentreDTO,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveCostCentreMaint(CostCentre costCentre, string userId)
        {
            Logger.Info("Invoking SaveCostCentreMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var costCentreDTO = Mapper.Map<CostCentre, CostCentreDTO>(costCentre);
                    var result = await accountOpDAO.SaveCostCentreMaint(costCentreDTO, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveCostCentreMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 19, 2017
        Function:     GetCreditApplAssessentForm
        Purpose:      GetCreditApplAssessentForm
        Inputs:       acctNo,appId
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetCreditApplAssessentForm(string acctNo, string applId)
        {
            Logger.Info("Invoking GetCAOGeneralInfo function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var result = await cardListDAO.GetCAOGeneralInfo(acctNo, applId);
                    if (result != null)
                    {
                        result.Remarks = string.Empty;
                        response.creditAssesOperation = Mapper.Map<AccountCaoDTO,CreditAssesOperation>(result);
                        response.creditAssesOperation.remarkHistory = Mapper.Map<List<WebSecDepRemarksDTO>, List<RemarkHistory>>(await accountOpDAO.GetSecDepRemarksListSelect(acctNo, "cao", string.Empty));
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCAOGeneralInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 20, 2017
        Function:     GetSecDepRemarks
        Purpose:      GetSecDepRemarks
        Inputs:       accountNo,eventType,txnId
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetSecDepRemarks(string accountNo, string eventType, string txnId)
        {
            Logger.Info("Invoking GetSecDepRemarks function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountOpDAO.GetSecDepRemarksListSelect(accountNo, eventType, txnId);
                    if (results.Count() > 0)
                    {
                      
                        response.WebSecDepRemarks = Mapper.Map<List<WebSecDepRemarksDTO>, List<RemarkHistory>>(results);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetSecDepRemarks: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 20, 2017
        Function:     GetAcctHistoryByAccount
        Purpose:      GetAcctHistoryByAccount
        Inputs:       acctNo
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetAcctHistoryByAccount(Int64 acctNo)
        {
            Logger.Info("Invoking GetAcctHistoryByAccount function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountOpDAO.WebAcctHistoryListSelect(acctNo);
                    if (results.Count() > 0)
                    {
                        response.creditLimitHistories = Mapper.Map<List<CreditLimitHistoryDTO>,List<CreditLimitHistory>>(results);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctHistoryByAccount: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 22, 2017
        Function:     GetSecHistoryDepositByAccount
        Purpose:      GetSecHistoryDepositByAccount
        Inputs:       acctNo
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetSecHistoryDepositByAccount(string acctNo)
        {
            Logger.Info("Invoking GetSecHistoryDepositByAccount function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountOpDAO.WebSecHistoryDepositList(acctNo);
                    if (results.Count() > 0)
                    {
                        response.creditLimitHistories = Mapper.Map<List<CreditLimitHistoryDTO>, List<CreditLimitHistory>>(results);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetSecHistoryDepositByAccount: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 20 2017
        Function:     SaveFinancialInfoMaint
        Purpose:      SaveFinancialInfoMaint
        Inputs:       FinancialInfoDTO,userId
        Returns:      AccountOpResponse
        // *************************************/
        public async Task<SaveGeneralInfoResponse> SaveFinancialInfoMaint(FinancialInfoModel financialInfoModel, string userId)
        {
            Logger.Info("Invoking SaveFinancialInfoMaint function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var financialInfo = Mapper.Map<FinancialInfoModel, FinancialInfoDTO>(financialInfoModel);
                    var result = await accountOpDAO.FtFinancialInfoMaint(financialInfo, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveFinancialInfoMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   June 22, 2017
         Function:     GetEventSearchWithoutDate
         Purpose:      GetEventSearchWithoutDate
         Inputs:       refKey,eventType,eventDate
         Returns:      AccountOpResponse
         *************************************/
        public async Task<AccountOpResponse> GetEventSearchWithoutDate(string refKey, string eventType, string eventDate)
        {
            Logger.Info("Invoking GetEventSearchWithoutDate function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountOpDAO.WebEventSearchWithoutDate(refKey, eventType, eventDate);
                    if (results.Count() > 0)
                    {
                        response.eventLoggers = Mapper.Map<List<EventLoggerDTO>,List<EventLogger>>(results);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEventSearchWithoutDate: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   June 22, 2017
         Function:     GetTempCreditLimitDetail
         Purpose:      GetTempCreditLimitDetail
         Inputs:       acctNo
         Returns:      AccountOpResponse
         *************************************/
        public async Task<AccountOpResponse> GetTempCreditLimitDetail(string acctNo)
        {
            Logger.Info("Invoking GetTempCreditLimitDetail function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var result = await accountOpDAO.FtTempCreditLimitDetail(acctNo);
                    if (result != null)
                    {
                        response.tempCreditCtrl = Mapper.Map<TempCreditCtrlDTO,TempCreditCtrlModel>(result);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetTempCreditLimitDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   June 22, 2017
         Function:     GetPaymentTxnList
         Purpose:      GetPaymentTxnList
         Inputs:       acctNo
         Returns:      AccountOpResponse
         *************************************/
        public async Task<AccountOpResponse> GetPaymentTxnList(string acctNo)
        {
            Logger.Info("Invoking GetPaymentTxnList function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var result = await accountOpDAO.GetPaymentTxnList(acctNo);
                    if (result.Any())
                    {
                        response.PaymentTxns = Mapper.Map<List<PaymentTxnDTO>,List<PaymentTxn>>(result);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPaymentTxnList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   June 22, 2017
         Function:     GetPaymentTxnDetail
         Purpose:      GetPaymentTxnDetail
         Inputs:       txnId
         Returns:      AccountOpResponse
         *************************************/
        public async Task<AccountOpResponse> GetPaymentTxnDetail(string txnId)
        {
            Logger.Info("Invoking GetPaymentTxnDetail function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var result = await accountOpDAO.GetPaymentTxnDetail(txnId);
                    if (result != null)
                    {
                        response.PaymentTxn = Mapper.Map<PaymentTxnDTO,PaymentTxn>(result);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPaymentTxnDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 22 2017
        Function:     DeleteLocationAcceptance
        Purpose:      DeleteLocationAcceptance
        Inputs:       acctNo,busnLocation,cardNo,userId
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveGeneralInfoResponse> DeleteLocationAcceptance(string acctNo, List<string> busnLocation, string cardNo, string userId)
        {
            Logger.Info("Invoking DeleteLocationAcceptance function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await accountOpDAO.DeleteLocationAcceptance(acctNo, busnLocation, cardNo, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in DeleteLocationAcceptance: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 23, 2017
        Function:     GetWebCostCentreSearch
        Purpose:      GetWebCostCentreSearch
        Inputs:       refTo,refKey,costCentre
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetWebCostCentreSearch(string refTo, string refKey, string costCentre)
        {
            Logger.Info("Invoking GetWebCostCentreSearch function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountInfoDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountInfoDAO.WebCostCentreSearch(refTo, refKey,costCentre);
                    if(results.Count() > 0)
                        response.costCentres = Mapper.Map<List<CostCentreDTO>,List<CostCentre>>(results);

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetWebCostCentreSearch: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   dandy
        Created on:  June 23, 2017
        Function:     SaveCollectionCaseInfo
        Purpose:      SaveCollectionCaseInfo
        Inputs:       accountNo,collector
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveCollectionCaseInfo(string accountNo, string collector)
        {
            Logger.Info("Invoking SaveCollectionCaseInfo function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await accountOpDAO.SaveCollectionCaseInfo(accountNo, collector);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveCollectionCaseInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  June 14, 2017
        Function:     SaveTempCreditCtrlMaint
        Purpose:      SaveTempCreditCtrlMaint
        Inputs:       acctNo,creditLimit,effDateFrom,effDateTo,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveTempCreditCtrlMaint(string acctNo, string creditLimit, string effDateFrom, string effDateTo, string userId)
        {
            Logger.Info("Invoking SaveTempCreditCtrlMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await accountOpDAO.SaveTempCreditCtrlMaint(acctNo, creditLimit,effDateFrom,effDateTo,userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveTempCreditCtrlMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 26, 2017
        Function:     GetFtUpToBalDetail
        Purpose:      GetFtUpToBalDetail
        Inputs:       acctNo
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetFtUpToBalDetail(string acctNo)
        {
            Logger.Info("Invoking GetFtUpToBalDetail function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await accountOpDAO.FtUpToBalDetail(acctNo);
                    if (result != null)
                    {
                        response.upToDateBal = Mapper.Map<UpToDateBalDTO,UpToDateBal>(result);
                        response.upToDateBal.AcctType = await controlDAO.GetPlasticType("I");
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFtUpToBalDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 26, 2017
        Function:     GetTxnInstants
        Purpose:      GetTxnInstants
        Inputs:       acctNo
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetTxnInstants(string acctNo)
        {
            Logger.Info("Invoking GetTxnInstants function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountOpDAO.TxnInstantListSelect(acctNo);
                    if (results.Count() > 0)
                    {
                        response.financilInfoItems = Mapper.Map<List<FinancilInfoItemDTO>,List<FinancilInfoItemsList>>(results);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetTxnInstants: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   June 26, 2017
        Function:     GetOnlineTransactionList
        Purpose:      GetOnlineTransactionList
        Inputs:       acctNo,amount
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetOnlineTransactionList(string acctNo,int flag)
        {
            Logger.Info("Invoking GetOnlineTransactionList function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountOpDAO.GetOnLineTransactionList(acctNo, flag);
                    if (results.Count() > 0)
                    {
                        response.onlineTransactions = Mapper.Map<List<AcctBalanceSelectAmountDTO>, List<OnlineTransaction>>(results);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetOnlineTransactionList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   July 10, 2017
        Function:     GetTxnInstantUnposts
        Purpose:      GetTxnInstantUnposts
        Inputs:       acctNo
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetTxnInstantUnposts(string acctNo)
        {
            Logger.Info("Invoking GetTxnInstantUnposts function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountOpDAO.TxnInstantUnpostedTxnList(acctNo);
                    if (results.Count() > 0)
                    {
                        response.financilInfoItems = Mapper.Map<List<FinancilInfoItemDTO>, List<FinancilInfoItemsList>>(results);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetTxnInstantUnposts: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 26, 2017
        Function:     GetProductDiscounts
        Purpose:      GetProductDiscounts
        Inputs:       acctNo,discType,refTo
        Returns:      AccountOpResponse
        *************************************/
        public async Task<AccountOpResponse> GetProductDiscounts(string acctNo, string discType, string refTo)
        {
            Logger.Info("Invoking GetProductDiscounts function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var results = await accountOpDAO.WebProductDiscountListSelect(acctNo, discType, refTo);
                    if (results.Count() > 0)
                    {
                        response.productDiscounts = Mapper.Map<List<ProductDiscountDTO>,List<ProductDiscount>>(results);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetProductDiscounts: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 28, 2017
        Function:     GetProductDiscount
        Purpose:      GetProductDiscount
        Inputs:       acctNo,discType,id,refTo
        Returns:      AccountOpResponse
        *************************************/
      public async Task<AccountOpResponse> GetProductDiscount(string acctNo, string discType, string id, string refTo)
        {
            Logger.Info("Invoking GetProductDiscount function");
            var response = new AccountOpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                    var result = await accountOpDAO.WebProductDiscountSelect(acctNo, discType,id, refTo);
                    if (result != null)
                    {
                        response.productDiscount = Mapper.Map<ProductDiscountDTO,ProductDiscount>(result);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetProductDiscount: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
      /*************************************
        Created by:   dandy
        Created on:  June 28, 2017
        Function:     SaveProductDiscount
        Purpose:      SaveProductDiscount
        Inputs:       ProductDiscountDTO,acctNo,func,refTo
        Returns:      SaveAcctSignUpResponse
        *************************************/
      public async Task<SaveAcctSignUpResponse> SaveProductDiscount(ProductDiscount productDiscountModel, string acctNo, string func, string refTo)
      {
          Logger.Info("Invoking SaveProductDiscount function");
          var response = new SaveAcctSignUpResponse()
          {
              Status = ResponseStatus.Failure,
          };
          try
          {
              using (var scope = Container.BeginLifetimeScope())
              {
                  var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                  var controlDAO = scope.Resolve<IControlDAO>();
                  var productDiscount = Mapper.Map<ProductDiscount, ProductDiscountDTO>(productDiscountModel);
                  var result = await accountOpDAO.ProductDiscountMaint(productDiscount, acctNo, func, refTo);
                  var message = await controlDAO.GetMessageCode(result);
                  response.desp = message.Descp;
                  response.flag = message.Flag;
              }
              response.Status = ResponseStatus.Success;
          }
          catch (Exception ex)
          {
              string msg = string.Format("Error in SaveProductDiscount: detail:{0}", ex.Message);
              Logger.Error(msg, ex);
              response.Status = ResponseStatus.Exception;
              response.flag = 1;
              response.Message = msg;
          }
          return response;
      }
      /*************************************
        Created by:   dandy
        Created on:   June 28, 2017
        Function:     DeleteProductDiscount
        Purpose:      DeleteProductDiscount
        Inputs:       ProductDiscountDTO,acctNo,refTo
        Returns:      SaveAcctSignUpResponse
        *************************************/
      public async Task<SaveAcctSignUpResponse> DeleteProductDiscount(ProductDiscount productDiscountModel, string acctNo, string refTo)
      {
          Logger.Info("Invoking DeleteProductDiscount function");
          var response = new SaveAcctSignUpResponse()
          {
              Status = ResponseStatus.Failure,
          };
          try
          {
              using (var scope = Container.BeginLifetimeScope())
              {
                  var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                  var controlDAO = scope.Resolve<IControlDAO>();
                  var productDiscount = Mapper.Map<ProductDiscount, ProductDiscountDTO>(productDiscountModel);
                  var result = await accountOpDAO.DeleteProductDiscount(productDiscount, acctNo,refTo);
                  var message = await controlDAO.GetMessageCode(result);
                  response.desp = message.Descp;
                  response.flag = message.Flag;
              }
              response.Status = ResponseStatus.Success;
          }
          catch (Exception ex)
          {
              string msg = string.Format("Error in DeleteProductDiscount: detail:{0}", ex.Message);
              Logger.Error(msg, ex);
              response.Status = ResponseStatus.Exception;
              response.flag = 1;
              response.Message = msg;
          }
          return response;
      }
      /*************************************
        Created by:   Tuan Tran
        Created on:   June 29, 2017
        Function:     GetEventLoggerDetail
        Purpose:      GetEventLoggerDetail
        Inputs:       module,eventId
        Returns:      AccountOpResponse
        *************************************/
      public async Task<AccountOpResponse> GetEventLoggerDetail(string module, string eventId)
      {
          Logger.Info("Invoking GetEventLoggerDetail function");
          var response = new AccountOpResponse()
          {
              Status = ResponseStatus.Failure,
          };
          try
          {
              using (var scope = Container.BeginLifetimeScope())
              {
                  var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                  var controlDAO = scope.Resolve<IControlDAO>();
                  var result = await accountOpDAO.GetEventLoggerDetail(module,eventId);
                  if (result != null)
                  {
                      response.eventLogger = Mapper.Map<EventLoggerDTO,EventLogger>(result);
                      if (module == "I")
                      {
                          response.eventLogger.EventType = await controlDAO.GetRefLib("EventType");
                          response.eventLogger.ReasonCd = await controlDAO.GetRefLib("ReasonCd");
                      }else
                      {
                          response.eventLogger.EventType = await controlDAO.GetRefLib("MerchEventType");
                          response.eventLogger.ReasonCd = await controlDAO.GetRefLib("MerchReasonCd");
                      }
                  }

              }
              response.Status = ResponseStatus.Success;
          }
          catch (Exception ex)
          {
              string msg = string.Format("Error in GetEventLoggerDetail: detail:{0}", ex.Message);
              Logger.Error(msg, ex);
              response.Status = ResponseStatus.Exception;
              response.Message = msg;
          }
          return response;
      }
      /*************************************
        Created by:   Tuan Tran
        Created on:   June 29, 2017
        Function:     GetEventlist
        Purpose:      GetEventlist
        Inputs:       module,accountNo,busnLocation
        Returns:      AccountOpResponse
      *************************************/
      public async Task<AccountOpResponse> GetEventlist(string module, string accountNo, string busnLocation)
      {
          Logger.Info("Invoking GetEventlist function");
          var response = new AccountOpResponse()
          {
              Status = ResponseStatus.Failure,
          };
          try
          {
              using (var scope = Container.BeginLifetimeScope())
              {
                  var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                  var results = await accountOpDAO.GetEventlist(module, accountNo, busnLocation);
                  if(results.Count() > 0)
                  {
                      response.eventLoggers = Mapper.Map<List<EventLoggerDTO>,List<EventLogger>>(results);
                  }

              }
              response.Status = ResponseStatus.Success;
          }
          catch (Exception ex)
          {
              string msg = string.Format("Error in GetEventlist: detail:{0}", ex.Message);
              Logger.Error(msg, ex);
              response.Status = ResponseStatus.Exception;
              response.Message = msg;
          }
          return response;
      }
      /*************************************
        Created by:   Tuan Tran
        Created on:   June 29, 2017
        Function:     GetEventSearch
        Purpose:      GetEventSearch
        Inputs:       eventLog
        Returns:      AccountOpResponse
        *************************************/
      public async Task<AccountOpResponse> GetEventSearch(EventLogger eventLog)
      {
          Logger.Info("Invoking GetEventSearch function");
          var response = new AccountOpResponse()
          {
              Status = ResponseStatus.Failure,
          };
          try
          {
              using (var scope = Container.BeginLifetimeScope())
              {
                  var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                  var eventLogDTO = Mapper.Map<EventLogger, EventLoggerDTO>(eventLog);
                  var results = await accountOpDAO.FtEventSearch(eventLogDTO);
                  if (results.Count() > 0)
                  {
                      response.eventLoggers = Mapper.Map<List<EventLoggerDTO>, List<EventLogger>>(results);
                  }

              }
              response.Status = ResponseStatus.Success;
          }
          catch (Exception ex)
          {
              string msg = string.Format("Error in GetEventSearch: detail:{0}", ex.Message);
              Logger.Error(msg, ex);
              response.Status = ResponseStatus.Exception;
              response.Message = msg;
          }
          return response;
      }
      /*************************************
        Created by:   Tuan Tran
        Created on:   June 29, 2017
        Function:     SearchBillingItem
        Purpose:      SearchBillingItem
        Inputs:       accountNo,fromDate,toDate,TxnCategory,sts
        Returns:      AccountOpResponse
        *************************************/
      public async Task<AccountOpResponse> SearchBillingItem(string accountNo, string fromDate, string toDate, string TxnCategory, string sts)
      {
          Logger.Info("Invoking SearchBillingItem function");
          var response = new AccountOpResponse()
          {
              Status = ResponseStatus.Failure,
          };
          try
          {
              using (var scope = Container.BeginLifetimeScope())
              {
                  var accountOpDAO = scope.Resolve<IAccountOpDAO>();
                  var results = await accountOpDAO.SearchBillingItem(accountNo, fromDate, toDate, TxnCategory, sts);
                  if (results.Count() > 0)
                  {
                      response.BillingItems = Mapper.Map<List<BillingItemDTO>, List<BillingItem>>(results);
                  }

              }
              response.Status = ResponseStatus.Success;
          }
          catch (Exception ex)
          {
              string msg = string.Format("Error in SearchBillingItem: detail:{0}", ex.Message);
              Logger.Error(msg, ex);
              response.Status = ResponseStatus.Exception;
              response.Message = msg;
          }
          return response;
      }
        #endregion
    }
}

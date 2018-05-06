using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.Dealer;
using CardTrend.Domain.Dto.Merchant;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
    public interface IMechSignUpService
    {
        Task<MechSignUpResponse> GetMAGeneralInfoDetail(string accountNo);
        Task<MechSignUpResponse> GetIFrameMerchGeneralInfo(string busnLocation);
        Task<MechSignUpResponse> GetBusinessLocationGeneralInfoDetail(string busnLocation);
        Task<MechSignUpResponse> GetBusnLocTermDetail(string termId, string busnLocation);
        Task<MechSignUpResponse> GetBusnLocTerms(string busnLocation);
        Task<MechSignUpResponse> GetBusinessLocationList(string acctNo);
        Task<MechSignUpResponse> GetIFrameMerchGeneralInfoes(string busnLocation, int category, int month, int year);
        Task<MechSignUpResponse> GetMerchAgreementList();
        Task<MechSignUpResponse> GetMerchChgOwnership(string busnLocation);
        Task<MechSignUpResponse> GetMerchOwnershipChanges(string businessLocation);
        Task<MechSignUpResponse> GetMerchtPostedTxnSearch(string accountNo, string busnLocation, string txnCd, string txnDate);
        Task<MechSignUpResponse> GetMerchProductPriceSearch(MerchProductPrize merchProductPrize, bool isListSelect);
        Task<SaveAcctSignUpResponse> SaveMAGeneralInfo(MA_GeneralInfo merchAgreementModel, string Func);
        Task<SaveAcctSignUpResponse> SaveEventMaint(EventLogger loggerModel, string module);
        Task<SaveAcctSignUpResponse> SaveBusnLocTerm(BusnLocTerminal merch);
        Task<SaveAcctSignUpResponse> SaveBusnLocationGeneralInfo(MerchantDetails merch);
        Task<SaveAcctSignUpResponse> SaveMerchChgOwnershipMaint(MerchChangeOwnership model, string userId);
    }
    public class MechSignUpService:IMechSignUpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public MechSignUpService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region MechSignUpService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<MechSignUpDAO>().As<IMechSignUpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region MechSignUp Service
        /*************************************
        Created by:   Tuan Tran
        Created on:   March 3, 2017
        Function:     GetMerchAgreementList
        Purpose:      GetMerchAgreementList
        Inputs:       
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetMerchAgreementList()
        {
            Logger.Info("Invoking GetMerchAgreementList function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var result = await mechSignUpDAO.GetMerchAgreementList();
                    if(result.Count() > 0)
                        response.merchAgreements = Mapper.Map<IList<MerchAgreementGeneralInfoDTO>,IList<MA_GeneralInfo>>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMerchAgreementList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   March 3, 2017
        Function:     GetBusinessLocationList
        Purpose:      GetBusinessLocationList
        Inputs:       acctNo
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetBusinessLocationList(string acctNo)
        {
            Logger.Info("Invoking GetBusinessLocationList function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var results = await mechSignUpDAO.GetBusinessLocationList(acctNo);
                    if (results.Count() > 0)
                        response.merchantDetails = Mapper.Map<IList<MerchGeneralInfoDTO>, IList<MerchantDetails>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetBusinessLocationList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 3, 2017
        Function:     SaveMAGeneralInfo
        Purpose:      SaveMAGeneralInfo
        Inputs:       MerchAgreementGeneralInfoDTO,Func
        Returns:      SaveGeneralInfoResponse
         *************************************/
        public async Task<SaveAcctSignUpResponse> SaveMAGeneralInfo(MA_GeneralInfo merchAgreementModel, string Func)
        {
            Logger.Info("Invoking SaveMAGeneralInfo function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var merchAgreement = Mapper.Map<MA_GeneralInfo, MerchAgreementGeneralInfoDTO>(merchAgreementModel);
                    var result = await mechSignUpDAO.SaveMAGeneralInfo(merchAgreement, Func);
                    response.desp = result.Descp;
                    response.flag = result.Flag;
                    response.returnValue.BatchId = result.paraOut.BatchId;
                    response.returnValue.RetCd = result.paraOut.RetCd;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveMAGeneralInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.desp = ex.Message;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   june 30, 2017
        Function:     GetMAGeneralInfoDetail
        Purpose:      GetMAGeneralInfoDetail
        Inputs:       
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetMAGeneralInfoDetail(string accountNo)
        {
            Logger.Info("Invoking GetMAGeneralInfoDetail function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var result = await mechSignUpDAO.GetMAGeneralInfoDetail(accountNo);
                    if(result != null)
                    {
                        if (string.IsNullOrEmpty(result.Sts))
                        {
                            result.Sts = "A";
                        }
                        if(string.IsNullOrEmpty(result.ReasonCd))
                        {
                            result.ReasonCd = "ACTV";
                        }
                        response.merchGeneralInfo = Mapper.Map<MerchGeneralInfoDTO, MA_GeneralInfo>(result);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMAGeneralInfoDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   dandy boy
        Created on:   june 30, 2017
        Function:     GetBusinessLocationGeneralInfoDetail
        Purpose:      GetBusinessLocationGeneralInfoDetail
        Inputs:       busnLocation
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetBusinessLocationGeneralInfoDetail(string busnLocation)
        {
            Logger.Info("Invoking GetBusinessLocationGeneralInfoDetail function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var result = await mechSignUpDAO.GetBusinessLocationGeneralInfoDetail(busnLocation);
                    if (result != null)
                    {
                        response.merchantDetail = Mapper.Map<MerchGeneralInfoDTO, MerchantDetails>(result);
                        response.merchantDetail.AutoDebitInd = true;

                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetBusinessLocationGeneralInfoDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   June 30, 2017
        Function:     GetMerchtPostedTxnSearch
        Purpose:      GetMerchtPostedTxnSearch
        Inputs:       
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetMerchtPostedTxnSearch(string accountNo, string busnLocation, string txnCd, string txnDate)
        {
            Logger.Info("Invoking GetMerchtPostedTxnSearch function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var results = await mechSignUpDAO.GetMerchtPostedTxnSearch(accountNo, busnLocation, txnCd, txnDate);
                    if (results.Count() > 0)
                    {
                        response.merchPostedTxnSearches = Mapper.Map<List<MerchPostedTxnSearchDTO>,List<MerchPostedTxnSearch>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMerchtPostedTxnSearch: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  June 30, 2017
        Function:     SaveEventMaint
        Purpose:      SaveEventMaint
        Inputs:       EventLoggerDTO
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveEventMaint(EventLogger loggerModel, string module)
        {
            Logger.Info("Invoking SaveEventMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var _Logger = Mapper.Map<EventLogger, EventLoggerDTO>(loggerModel);
                    var result = await mechSignUpDAO.SaveEventMaint(_Logger,module);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveEventMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   June 30, 2017
        Function:     GetIFrameMerchGeneralInfo
        Purpose:      GetIFrameMerchGeneralInfo
        Inputs:       busnLocation
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetIFrameMerchGeneralInfo(string busnLocation)
        {
            Logger.Info("Invoking GetIFrameMerchGeneralInfo function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var result = await mechSignUpDAO.iFrameMerchGeneralInfoSelect(busnLocation);
                    if (result != null)
                    {
                        response.eService = Mapper.Map<EServiceDTO,eService>(result);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetIFrameMerchGeneralInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   June 30, 2017
        Function:     GetIFrameMerchGeneralInfo
        Purpose:      GetIFrameMerchGeneralInfo
        Inputs:       busnLocation,category,month,year
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetIFrameMerchGeneralInfoes(string busnLocation, int category, int month, int year)
        {
            Logger.Info("Invoking GetIFrameMerchGeneralInfoes function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var results = await mechSignUpDAO.iFrameMerchTxnListSelect(busnLocation,category,month,year);
                    if (results.Count() > 0)
                    {
                        response.eServices = Mapper.Map<List<EServiceDTO>,List<eService>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetIFrameMerchGeneralInfoes: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 3, 2017
        Function:     SaveBusnLocationGeneralInfo
        Purpose:      SaveBusnLocationGeneralInfo
        Inputs:       MerchGeneralInfoDTO
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveBusnLocationGeneralInfo(MerchantDetails merch)
        {
            Logger.Info("Invoking SaveBusnLocationGeneralInfo function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var merchAgreement = Mapper.Map<MerchantDetails, MerchGeneralInfoDTO>(merch);
                    var result = await mechSignUpDAO.SaveBusnLocationGeneralInfo(merchAgreement);
                    var message = await controlDAO.GetMessageCode(result.Flag);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                    response.returnValue.BusnLocation = result.paraOut.BusnLocation;
                    response.returnValue.EntityId = result.paraOut.EntityId;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveBusnLocationGeneralInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.desp = ex.Message;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   june 30, 2017
        Function:     GetMAGeneralInfoDetail
        Purpose:      GetMAGeneralInfoDetail
        Inputs:       
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetBusnLocTermDetail(string termId, string busnLocation)
        {
            Logger.Info("Invoking GetBusnLocTermDetail function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                     var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await mechSignUpDAO.GetBusnLocTermDetail(termId, busnLocation);
                    if (result != null)
                    {
                        response.busnLocTerminal = Mapper.Map<BusnLocTerminalDTO, BusnLocTerminal>(result);
                        response.busnLocTerminal.ReasonCd = await controlDAO.GetRefLib("TermReasonCd");
                        if (string.IsNullOrEmpty(result.Sts))
                        {
                            response.busnLocTerminal.SelectedStatus = "A";
                        }else
                        {
                            response.busnLocTerminal.SelectedStatus = result.Sts;
                        }

                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetBusnLocTermDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   june 30, 2017
        Function:     GetBusnLocTerms
        Purpose:      GetBusnLocTerms
        Inputs:       busnLocation
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetBusnLocTerms(string busnLocation)
        {
            Logger.Info("Invoking GetBusnLocTerms function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await mechSignUpDAO.GetBusnLocTermList(busnLocation);
                    if (results.Count() > 0)
                    {                       
                        response.busnLocTerminals = Mapper.Map<List<BusnLocTerminalDTO>,List<BusnLocTerminal>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetBusnLocTerms: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 3, 2017
        Function:     SaveBusnLocTerm
        Purpose:      SaveBusnLocTerm
        Inputs:       MerchantDetails
        Returns:      SaveGeneralInfoResponse
         *************************************/
        public async Task<SaveAcctSignUpResponse> SaveBusnLocTerm(BusnLocTerminal merch)
        {
            Logger.Info("Invoking SaveBusnLocTerm function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var busnLocTerminal = Mapper.Map<BusnLocTerminal, BusnLocTerminalDTO>(merch);
                    var result = await mechSignUpDAO.SaveBusnLocTerm(busnLocTerminal);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveBusnLocTerm: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.desp = ex.Message;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   june 30, 2017
        Function:     GetMerchOwnershipChanges
        Purpose:      GetMerchOwnershipChanges
        Inputs:       
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetMerchOwnershipChanges(string businessLocation)
        {
            Logger.Info("Invoking GetMerchOwnershipChanges function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var results = await mechSignUpDAO.WebMerchOwnershipChangeListSelect(businessLocation);
                    if (results.Count() > 0)
                    {
                        response.merchChangeOwnerships = Mapper.Map<List<MerchChangeOwnershipDTO>, List<MerchChangeOwnership>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMerchOwnershipChanges: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   june 30, 2017
        Function:     GetMerchProductPriceSearch
        Purpose:      GetMerchProductPriceSearch
        Inputs:       busnLocation,ProdCd,effDateFrom,effDateTo,isListSelect
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetMerchProductPriceSearch(MerchProductPrize merchProductPrize, bool isListSelect)
        {
            Logger.Info("Invoking GetMerchProductPriceSearch function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var merchProductPrizeDto = Mapper.Map<MerchProductPrize, MerchProductPrizeDTO>(merchProductPrize);
                    var results = await mechSignUpDAO.WebMerchProductPriceSearch(merchProductPrizeDto.BusnLocation, merchProductPrizeDto.ProdCd, merchProductPrizeDto.StartDate, merchProductPrizeDto.EndDate, isListSelect);
                    if (results.Count() > 0)
                    {
                        response.merchProductPrizes = Mapper.Map<List<MerchProductPrizeDTO>, List<MerchProductPrize>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMerchProductPriceSearch: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 3, 2017
        Function:     SaveMerchChgOwnershipMaint
        Purpose:      SaveMerchChgOwnershipMaint
        Inputs:       MerchChangeOwnershipDTO,userId
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveMerchChgOwnershipMaint(MerchChangeOwnership model, string userId)
        {
            Logger.Info("Invoking SaveMerchChgOwnershipMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var merchChangeOwnership = Mapper.Map<MerchChangeOwnership, MerchChangeOwnershipDTO>(model);
                    var result = await mechSignUpDAO.SaveMerchChgOwnershipMaint(merchChangeOwnership, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveMerchChgOwnershipMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.desp = ex.Message;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   June 30, 2017
        Function:     GetMerchChgOwnership
        Purpose:      GetMerchChgOwnership
        Inputs:       busnLocation
        Returns:      MechSignUpResponse
        *************************************/
        public async Task<MechSignUpResponse> GetMerchChgOwnership(string busnLocation)
        {
            Logger.Info("Invoking GetMerchChgOwnership function");
            var response = new MechSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var mechSignUpDAO = scope.Resolve<IMechSignUpDAO>();
                    var result = await mechSignUpDAO.WebMerchChgOwnershipSelect(busnLocation);
                    if (result != null)
                    {
                        response.merchChangeOwnership = Mapper.Map<MerchChangeOwnershipDTO, MerchChangeOwnership>(result);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMerchChgOwnership: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }        
        #endregion
    }
}

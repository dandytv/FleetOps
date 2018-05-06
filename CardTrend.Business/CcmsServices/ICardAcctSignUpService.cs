using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.Application;
using CardTrend.Domain.Dto.CardHolder;
using CardTrend.Domain.Dto.Corporate;
using CCMS.ModelSector;
using ModelSector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
    public interface ICardAcctSignUpService
    {
        Task<CardAcctSignUpResponse> GetCardList(string accountNo);
        Task<CardAcctSignUpResponse> GetCAOGeneralInfo(string acctNo = null, string appId = null);
        Task<CardAcctSignUpResponse> GetApplicationGeneralInfo(string applId);
        Task<CardAcctSignUpResponse> GetMiscellaneousInfoDetail(int applId);
        Task<CardAcctSignUpResponse> GetAcctDepositInfoDetail(string applid = null, string acctNo = null, string txnid = null, string corpCd = null);
        Task<List<Milestone>> WebMilestoneListSelect(string userId, string workflowCd, int Ind);
        Task<List<PukalApproval>> GetApprovalMilestoneListSelect(string userId, string workflowCd, int Ind);
        Task<CardAcctSignUpResponse> GetAcctSignUpList(string applicationId, string page);
        Task<CardAcctSignUpResponse> GetCustAcctVelocityList(VeloctyLimitListMaintModel cusVeloctyLimitModel);
        Task<CardAcctSignUpResponse> GetAcctDepositInfoList(string applid = null, string acctNo = null, string CorpCd = null);
        Task<CardAcctSignUpResponse> GetMilestoneHistorySelect(string workFlowCd, Int64 RefKey);
        Task<CardAcctSignUpResponse> GetVehicles(string acctNo, string applId);
        Task<CardAcctSignUpResponse> GetAddressList(string refTo, string refKey);
        Task<CardAcctSignUpResponse> GetSPOMilestones(string status);
        Task<CardAcctSignUpResponse> GetCustAcctVelocityDetail(VeloctyLimitListMaintModel cusVeloctyLimitModel);
        Task<CardAcctSignUpResponse> GetVehicleDetail(string appcId, string cardNo, string vehRegtNo);
        Task<CardAcctSignUpResponse> GetContactlist(string refTo, string refKey);
        Task<CardAcctSignUpResponse> GetContactDetail(string refTo, string refKey, string refCd);
        Task<CardAcctSignUpResponse> GetAddressDetail(string refTo, string refKey, string refCd);
        Task<CardAcctSignUpResponse> GetMilestoneInfo(string workflowcd, int taskNo);
        Task<CardAcctSignUpResponse> MilestoneApplValidation(Int64 aprId);
        Task<CardAcctSignUpResponse> GetSKDSList(string accountNo, string applId, string page);
        Task<CardAcctSignUpResponse> GetSKDSDetail(string accountNo, string applId, string txnId);
        Task<SaveAcctSignUpResponse> SaveApplicationGeneralInfoResult(AcctSignUp acctSignModel, string userId);
        Task<SaveAcctSignUpResponse> SaveMilestone(Milestone mileStoneModel);
        Task<SaveAcctSignUpResponse> SaveMileStoneAdj(Milestone mileStoneModel);
        Task<SaveAcctSignUpResponse> SaveSKDS(SKDS skds);
        Task<SaveAcctSignUpResponse> DelContact(string refTo, string refKey, string refCd);
        Task<SaveAcctSignUpResponse> DelAddress(string refTo, string refKey, string refCd, string userId);
        Task<SaveAcctSignUpResponse> DelCustAcctVelocity(string accNo, string cardNo, string applId, string appcId, string velInd, string prodCd, string costCenter, string corpCd);
        Task<SaveAcctSignUpResponse> SaveAddressList(AddrListMaintModel webAddress, string refTo, string refCd, string refKey, string func, string userId);
        Task<SaveAcctSignUpResponse> SaveCustAcctVelocity(VeloctyLimitListMaintModel veloctyLimitModel, string applId, string func);
        Task<SaveAcctSignUpResponse> SaveCreditAssessmentOperation(CreditAssesOperation accountCaoModel, string userId);
        Task<SaveAcctSignUpResponse> SaveVehicles(VehiclesListModel vehicleModel, string userId);
        Task<SaveAcctSignUpResponse> SaveContactsList(ContactLstModel issContact, string refTo, string func);
        Task<SaveAcctSignUpResponse> SaveMiscellaneousInfo(MiscellaneousInfoModel miscellaneousInfo);
        bool AutoGenerateFolder(int flag, string path);
    }
    public class CardAcctSignUpService : ICardAcctSignUpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public CardAcctSignUpService()
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
            afBuilder.RegisterType<CardAcctSignUpDAO>().As<ICardAcctSignUpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<AccountOpDAO>().As<IAccountOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);            
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region CardService
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 22, 2017
        Function:     GetCardList
        Purpose:      GetCardList
        Inputs:       accountNo
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetCardList(string accountNo)
        {
            Logger.Info("Invoking GetCardList fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var results = await cardListDAO.FtCardHolderList(accountNo);
                    if(results.Count() > 0)
                        response.cards = Mapper.Map<IList<CardListDTO>,IList<CardHolderInfoModel>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCardList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 23, 2017
        Function:     GetApplicationGeneralInfo
        Purpose:      GetApplicationGeneralInfo
        Inputs:       applId
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetApplicationGeneralInfo(string applId)
        {
            Logger.Info("Invoking GetApplicationGeneralInfo function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var result = await cardListDAO.GetApplicationGeneralInfo(applId);
                    if(result != null)
                     response.acctSignUp = Mapper.Map<AcctSignUp>(result);

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetApplicationGeneralInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 14, 2017
        Function:     GetAcctDepositInfoDetail
        Purpose:      GetAcctDepositInfoDetail
        Inputs:       applId,acctNo,txnid,corpCd
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetAcctDepositInfoDetail(string applid = null, string acctNo = null, string txnid = null, string corpCd = null)
        {
            Logger.Info("Invoking GetAcctDepositInfoDetail function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var accountDAO = scope.Resolve<IAccountOpDAO>();
                    var result = await cardListDAO.GetAcctDepositInfoDetail(applid, acctNo, txnid, corpCd);
                    var remarks = await accountDAO.GetSecDepRemarksListSelect(acctNo, "SecDep", txnid);
                    if(result != null)
                    {
                        response.creditAssesOperation = Mapper.Map<AccountCaoDTO, CreditAssesOperation>(result);
                        response.creditAssesOperation.remarkHistory = Mapper.Map<IList<WebSecDepRemarksDTO>, IList<RemarkHistory>>(remarks);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctDepositInfoDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        #endregion
        #region Milestone
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 22, 2017
        Function:     WebMilestoneListSelect
        Purpose:      WebMilestoneListSelect
        Inputs:       MilestoneDTO
        Returns:      List of MilestoneDTO
        *************************************/
        public async Task<List<Milestone>> WebMilestoneListSelect(string userId, string workflowCd, int Ind)
        {
            if (string.IsNullOrEmpty(workflowCd))
                workflowCd = "APPL";
            // MilestoneDTO
            Logger.Info("Invoking WebMilestoneListSelect fuction");
            var data = new List<Milestone>();
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    
                    var cardAcctSignUpDAO = scope.Resolve<ICardAcctSignUpDAO>();

                    var results = await cardAcctSignUpDAO.WebMilestoneListSelect(userId, workflowCd, Ind);
                    if(results.Count() > 0)
                    {
                        data = Mapper.Map<List<MilestoneDTO>, List<Milestone>>(results);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebMilestoneListSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
            }
            return data;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 22, 2017
        Function:     GetApprovalMilestoneListSelect
        Purpose:      GetApprovalMilestoneListSelect
        Inputs:       userId,workflowCd,Ind
        Returns:      List<PukalApproval>
        *************************************/
        public async Task<List<PukalApproval>> GetApprovalMilestoneListSelect(string userId, string workflowCd, int Ind)
        {
            if (string.IsNullOrEmpty(workflowCd))
                workflowCd = "APPL";
            // MilestoneDTO
            Logger.Info("Invoking GetApprovalMilestoneListSelect fuction");
            var data = new List<PukalApproval>();
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {

                    var cardAcctSignUpDAO = scope.Resolve<ICardAcctSignUpDAO>();

                    var results = await cardAcctSignUpDAO.WebMilestoneListSelect(userId, workflowCd, Ind);
                    if (results.Count() > 0)
                    {
                        data = Mapper.Map<List<MilestoneDTO>, List<PukalApproval>>(results);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetApprovalMilestoneListSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
            }
            return data;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 22, 2017
        Function:     SaveApplicationGeneralInfoResult
        Purpose:      SaveApplicationGeneralInfoResult
        Inputs:       AcctSignUpDTO
        Returns:      SaveAcctSignUpResponse 
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveApplicationGeneralInfoResult(AcctSignUp acctSignModel,string userId)
        {
            Logger.Info("Invoking SaveApplicationGeneralInfoResult function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var acctSign = Mapper.Map<AcctSignUp, AcctSignUpDTO>(acctSignModel);
                    acctSign.UserId = userId;
                    var result = await cardListDAO.SaveApplicationGeneralInfoResult(acctSign);
                    var message = await controlDAO.GetMessageCode(result.Flag);
                    if (message.Flag == 0)
                    {
                        if (message.Descp.Contains("updated"))
                        {
                           response.desp = message.Descp;
                           response.flag = message.Flag;
                        }
                        else
                        {
                            string tempObj = result.paraOut.ApplId;
                            int temInt = message.Flag;
                            if (this.AutoGenerateFolder(temInt, result.paraOut.DocPath))
                            {
                                response.desp = message.Descp;
                                response.flag = message.Flag;
                            }
                            else
                            {
                                response.desp = message.Descp + ", Directory Creation Failed ";
                                response.flag = 1;
                            }
                        }
                    }
                    else
                    {
                        response.desp = message.Descp;
                        response.flag = 1;


                    }
                    response.returnValue = result.paraOut;
         
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveApplicationGeneralInfoResult: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 14, 2017
        Function:     SaveCreditAssessmentOperation
        Purpose:      SaveCreditAssessmentOperation
        Inputs:       AccountCaoDTO
        Returns:      SaveAcctSignUpResponse 
       *************************************/
        public async Task<SaveAcctSignUpResponse> SaveCreditAssessmentOperation(CreditAssesOperation accountCaoModel,string userId)
        {
            Logger.Info("Invoking SaveCreditAssessmentOperation function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var accountCaoDTO = Mapper.Map<CreditAssesOperation, AccountCaoDTO>(accountCaoModel);
                    var result = await cardListDAO.SaveCreditAssessmentOperation(accountCaoDTO,userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveCreditAssessmentOperation: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 22, 2017
        Function:     SaveMilestone
        Purpose:      SaveMilestone
        Inputs:       MilestoneDTO
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveMilestone(Milestone mileStoneModel)
        {
            Logger.Info("Invoking SaveMilestone function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAccountDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var mileStone = Mapper.Map<Milestone, MilestoneDTO>(mileStoneModel);
                    var result = await cardAccountDAO.SaveApprovalMilestone(mileStone);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveMilestone: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 15, 2017
        Function:     GetMilestoneHistorySelect
        Purpose:      GetMilestoneHistorySelect
        Inputs:       workFlowCd,RefKey
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetMilestoneHistorySelect(string workFlowCd, Int64 RefKey)
        {
            Logger.Info("Invoking GetMilestoneHistorySelect function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var results = await cardListDAO.GetMilestoneHistorySelect(workFlowCd, RefKey);
                    if(results.Count() >  0)
                        response.milestoneHistories = Mapper.Map<IList<MilestoneDTO>,IList<Milestone>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMilestoneHistorySelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 16, 2017
        Function:     GetMilestoneInfo
        Purpose:      GetMilestoneInfo
        Inputs:       workflowcd,taskNo
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetMilestoneInfo(string workflowcd, int taskNo)
        {
            Logger.Info("Invoking GetMilestoneHistorySelect function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAcctSignUp = scope.Resolve<ICardAcctSignUpDAO>();
                    var mileStone = await cardAcctSignUp.MilestoneInfo(workflowcd, taskNo);
                    if(mileStone != null)
                        response.mileStoneInfo = Mapper.Map<MilestoneDTO,Milestone>(mileStone);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMilestoneHistorySelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 17, 2017
        Function:     MilestoneApplValidation
        Purpose:      MilestoneApplValidation
        Inputs:       aprId
        Returns:      CardAcctSignUpResponse
       *************************************/
        public async Task<CardAcctSignUpResponse> MilestoneApplValidation(Int64 aprId)
        {
            Logger.Info("Invoking MilestoneApplValidation function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAcctSignUp = scope.Resolve<ICardAcctSignUpDAO>();
                    var mileStone = await cardAcctSignUp.WebMilestoneApplValidation(aprId);
                    if(mileStone != null)
                    response.mileStoneInfo = Mapper.Map<MilestoneDTO,Milestone>(mileStone);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in MilestoneApplValidation: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 20, 2017
        Function:     SaveMileStoneAdj
        Purpose:      SaveMileStoneAdj
        Inputs:       MilestoneDTO
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveMileStoneAdj(Milestone mileStoneModel)
        {
            Logger.Info("Invoking SaveMileStoneAdj function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAccountDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var mileStone = Mapper.Map<Milestone, MilestoneDTO>(mileStoneModel);
                    var result = await cardAccountDAO.SaveMilestoneAdj(mileStone);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveMileStoneAdj: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   april 3, 2017
        Function:     GetSPOMilestones
        Purpose:      GetSPOMilestones
        Inputs:       status
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetSPOMilestones(string status)
        {
            Logger.Info("Invoking GetSPOMilestones function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAccountDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var result = await cardAccountDAO.WebSPOMilestoneList(status);
                    if(result != null)
                        response.milestoneHistories = Mapper.Map<IList<SPOMilestoneDTO>, IList<Milestone>>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetSPOMilestones: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        #endregion
        #region CorporateCodes
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 22, 2017
        Function:     GetAcctSignUpList
        Purpose:      GetAcctSignUpList
        Inputs:       applicationId,page
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetAcctSignUpList(string applicationId, string page)
        {
            Logger.Info("Invoking GetAcctSignUpList fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var results = await cardListDAO.GetAcctSignUpList(applicationId,page);
                    if(results.Count() > 0)
                        response.acctSignUps = Mapper.Map<List<GetAcctSignUpDTO>,List<AcctSignUp>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctSignUpList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 16, 2017
        Function:     GetCustAcctVelocityList
        Purpose:      GetCustAcctVelocityList
        Inputs:       VeloctyLimitListMaintDTO
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetCustAcctVelocityList(VeloctyLimitListMaintModel cusVeloctyLimitModel)
        {
            Logger.Info("Invoking GetCustAcctVelocityList fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var cusVeloctyLimit = Mapper.Map<VeloctyLimitListMaintModel, VeloctyLimitListMaintDTO>(cusVeloctyLimitModel);
                    var results = await cardListDAO.GetCustAcctVelocityList(cusVeloctyLimit);
                    if(results.Count() > 0)
                        response.veloctyLimits = Mapper.Map<List<VeloctyLimitListMaintDTO>,List<VeloctyLimitListMaintModel>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCustAcctVelocityList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 16, 2017
        Function:     GetVehicles
        Purpose:      GetVehicles
        Inputs:       acctNo,applId
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetVehicles(string acctNo, string applId)
        {
            Logger.Info("Invoking GetVehicles fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var results = await cardListDAO.GetVehicleList(acctNo, applId);
                    if(results.Count() > 0)
                    {
                        response.vehicles = Mapper.Map<List<VehicleDTO>,List<VehiclesListModel>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetVehicles: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 15, 2017
        Function:     GetVehicleDetail
        Purpose:      GetVehicleDetail
        Inputs:       appcId,cardNo,vehRegtNo
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetVehicleDetail(string appcId, string cardNo, string vehRegtNo)
        {
            Logger.Info("Invoking GetVehicleDetail fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var result = await cardListDAO.GetVehicleDetail(appcId, cardNo, vehRegtNo);
                    if(result != null)
                    {
                        result.AppcId = appcId;
                        response.vehicle = Mapper.Map<VehicleDTO,VehiclesListModel>(result);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetVehicleDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 15, 2017
        Function:     GetAddressDetail
        Purpose:      GetAddressDetail
        Inputs:       refTo,refKey,refCd
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetAddressDetail(string refTo, string refKey, string refCd)
        {
            Logger.Info("Invoking GetVehicleDetail fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var result = await cardListDAO.GetAddressDetail(refTo, refKey, refCd);
                    if(result != null)
                    {
                        result.AddressType = result.RefCd;
                        if (string.IsNullOrEmpty(result.Country))
                            result.Country = "458";
                        response.Address = Mapper.Map<WebAddressDTO, AddrListMaintModel>(result);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetVehicleDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:   May 30, 2017
        Function:     DelCustAcctVelocity
        Purpose:      DelCustAcctVelocity
        Inputs:       acctNo,cardNo,applId,appcId,velInd,prodCd,costCenter,corpCd
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> DelCustAcctVelocity(string accNo, string cardNo, string applId, string appcId, string velInd, string prodCd, string costCenter, string corpCd)
        {
            Logger.Info("Invoking DelCustAcctVelocity function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await cardListDAO.DelCustAcctVelocity(accNo, cardNo, applId, appcId, velInd, prodCd, costCenter, corpCd);
                    var msgRetrieve = await controlDAO.GetMessageCode(Convert.ToInt32(result));
                    response.desp = msgRetrieve.Descp;
                    response.flag = msgRetrieve.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in DelCustAcctVelocity: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   June 06, 2017
        Function:     SaveCustAcctVelocity
        Purpose:      SaveCustAcctVelocity
        Inputs:       VeloctyLimitListMaintDTO
        Returns:      SaveAcctSignUpResponse 
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveCustAcctVelocity(VeloctyLimitListMaintModel veloctyLimitModel, string applId, string func)
        {
            Logger.Info("Invoking SaveCustAcctVelocity function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var velocityLimit = Mapper.Map<VeloctyLimitListMaintModel, VeloctyLimitListMaintDTO>(veloctyLimitModel);
                    var result = await cardListDAO.SaveCustAcctVelocity(velocityLimit, applId, func);

                    var message = await controlDAO.GetMessageCode(Convert.ToInt32(result));
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveCustAcctVelocity: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 15, 2017
        Function:     SaveVehicles
        Purpose:      SaveVehicles
        Inputs:       VeloctyLimitListMaintDTO
        Returns:      SaveAcctSignUpResponse 
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveVehicles(VehiclesListModel vehicleModel, string userId)
        {
            Logger.Info("Invoking SaveVehicles function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var vehicleDTO = Mapper.Map<VehiclesListModel, VehicleDTO>(vehicleModel);
                    var result = await cardListDAO.SaveVehicleList(vehicleDTO,userId);

                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveVehicles: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 6, 2017
        Function:     GetCustAcctVelocityDetail
        Purpose:      GetCustAcctVelocityDetail
        Inputs:       VeloctyLimitListMaintDTO
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetCustAcctVelocityDetail(VeloctyLimitListMaintModel cusVeloctyLimitModel)
        {
            Logger.Info("Invoking GetCustAcctVelocityList fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var cusVeloctyLimit = Mapper.Map<VeloctyLimitListMaintModel, VeloctyLimitListMaintDTO>(cusVeloctyLimitModel);
                    var result = await cardListDAO.GetCustAcctVelocityDetail(cusVeloctyLimit);
                    if(result != null)
                        response.veloctyLimit = Mapper.Map<VeloctyLimitListMaintDTO,VeloctyLimitListMaintModel>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCustAcctVelocityDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 15, 2017
        Function:     GetContactlist
        Purpose:      GetContactlist
        Inputs:       refTo,refKey
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetContactlist(string refTo, string refKey)
        {
            Logger.Info("Invoking GetContactlist fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var results = await cardListDAO.GetContactlist(refTo, refKey);
                    if(results.Count() > 0)
                        response.contacts = Mapper.Map<List<IssContactDTO>,List<ContactLstModel>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetContactlist: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 16, 2017
        Function:     GetAddressList
        Purpose:      GetAddressList
        Inputs:       refTo,refKey
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetAddressList(string refTo, string refKey)
        {
            Logger.Info("Invoking GetAddressList fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var results = await cardListDAO.GetAddressList(refTo, refKey);
                    if(results.Count() > 0)
                        response.Addresses = Mapper.Map<List<WebAddressDTO>,List<AddrListMaintModel>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAddressList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 16, 2017
        Function:     SaveAddressList
        Purpose:      SaveAddressList
        Inputs:       WebAddressDTO,refTo,refCd,refKey,func,userId
        Returns:      SaveAcctSignUpResponse 
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveAddressList(AddrListMaintModel webAddress, string refTo, string refCd, string refKey, string func, string userId)
        {
            Logger.Info("Invoking SaveAddressList function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var webAddressDTO = Mapper.Map<AddrListMaintModel, WebAddressDTO>(webAddress);
                    var result = await cardListDAO.SaveAddressList(webAddressDTO, refTo, refCd, refKey, func, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveAddressList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 16, 2017
        Function:     GetContactDetail
        Purpose:      GetContactDetail
        Inputs:       refTo,refKey,refCd
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetContactDetail(string refTo, string refKey, string refCd)
        {
            Logger.Info("Invoking GetContactDetail fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var result = await cardListDAO.GetContactDetail(refTo, refKey, refCd);
                    if(result != null)
                    {
                        result.ContactType = refCd;
                        result.ContactStatus = result.Status;
                        response.contact = Mapper.Map<IssContactDTO,ContactLstModel>(result);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetContactDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 19, 2017
        Function:     DelAddress
        Purpose:      DelAddress
        Inputs:       refTo,refKey,refCd,userId
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> DelAddress(string refTo, string refKey, string refCd, string userId)
        {
            Logger.Info("Invoking DelAddress fuction use EF to call SP");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAcctSignUpDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await cardAcctSignUpDAO.DelAddress(refTo, refKey, refCd,userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in DelAddress: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  june 16, 2017
        Function:     SaveContactsList
        Purpose:      SaveContactsList
        Inputs:       IssContactDTO
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveContactsList(ContactLstModel issContact, string refTo, string func)
        {
            Logger.Info("Invoking SaveContactsList function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAcctSignUpDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var issContactDTO = Mapper.Map<ContactLstModel, IssContactDTO>(issContact);
                    var result = await cardAcctSignUpDAO.SaveContactsList(issContactDTO,refTo,func);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveContactsList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  june 16, 2017
        Function:     DelContact
        Purpose:      DelContact
        Inputs:       refTo,refKey,refCd
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> DelContact(string refTo, string refKey, string refCd)
        {
            Logger.Info("Invoking DelContact function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAcctSignUpDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await cardAcctSignUpDAO.DelContact(refTo, refKey, refCd);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in DelContact: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  june 16, 2017
        Function:     SaveMiscellaneousInfo
        Purpose:      SaveMiscellaneousInfo
        Inputs:       miscellaneousInfo
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveMiscellaneousInfo(MiscellaneousInfoModel miscellaneousInfo)
        {
            Logger.Info("Invoking SaveMiscellaneousInfo function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAcctSignUpDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var miscellaneousInfoDto = Mapper.Map<MiscellaneousInfoModel, MiscellaneousInfoDTO>(miscellaneousInfo);
                    var result = await cardAcctSignUpDAO.SaveMiscellaneousInfo(miscellaneousInfoDto);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveMiscellaneousInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        #endregion
        #region Security Deposit
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 16, 2017
        Function:     GetAcctDepositInfoList
        Purpose:      GetAcctDepositInfoList
        Inputs:       applid,acctNo, & CorpCd no required
        Returns:      CardAcctSignUpResponse
    *************************************/
        public async Task<CardAcctSignUpResponse> GetAcctDepositInfoList(string applid = null, string acctNo = null, string CorpCd = null)
        {
            Logger.Info("Invoking GetAcctDepositInfoList fuction use EF to call SP");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAcctSignUpDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var results = await cardAcctSignUpDAO.GetAcctDepositInfoList(applid, acctNo, CorpCd);
                    if(results.Count() > 0)
                        response.creditAssesOperations = Mapper.Map<List<CreditAssesOperationDTO>, List<CreditAssesOperation>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctDepositInfoList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 13, 2017
        Function:     GetCAOGeneralInfo
        Purpose:      GetCAOGeneralInfo
        Inputs:       acctNo,appId
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetCAOGeneralInfo(string acctNo = null, string appId = null)
        {
            Logger.Info("Invoking GetCAOGeneralInfo function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var result = await cardListDAO.GetCAOGeneralInfo(acctNo, appId);
                    if(result != null)
                        response.creditAssesOperation =  Mapper.Map<AccountCaoDTO,CreditAssesOperation>(result);
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
        Created on:   June 13, 2017
        Function:     GetMiscellaneousInfoDetail
        Purpose:      GetMiscellaneousInfoDetail
        Inputs:       applId
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetMiscellaneousInfoDetail(int applId)
        {
            Logger.Info("Invoking GetMiscellaneousInfoDetail function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var result = await cardListDAO.GetMiscellaneousInfoDetail(applId);
                    if (result != null)
                        response.miscellaneousInfo = Mapper.Map<MiscellaneousInfoDTO, MiscellaneousInfoModel>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMiscellaneousInfoDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 13, 2017
        Function:     GetSKDSList
        Purpose:      GetSKDSList
        Inputs:       accountNo,applId,page
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetSKDSList(string accountNo, string applId, string page)
        {
            Logger.Info("Invoking GetSKDSList function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var result = await cardListDAO.GetSKDSList(accountNo, applId, page);
                    if (result != null)
                        response.skdses = Mapper.Map<List<SkdsDTO>,List<SKDS>>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetSKDSList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 13, 2017
        Function:     GetSKDSDetail
        Purpose:      GetSKDSDetail
        Inputs:       accountNo,applId,txnId
        Returns:      CardAcctSignUpResponse
        *************************************/
        public async Task<CardAcctSignUpResponse> GetSKDSDetail(string accountNo, string applId, string txnId)
        {
            Logger.Info("Invoking GetSKDSDetail function");
            var response = new CardAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardListDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var result = await cardListDAO.GetSKDSDetail(accountNo, applId, txnId);
                    if (result != null)
                        response.skds = Mapper.Map<SkdsDTO, SKDS>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetSKDSDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:   june 16, 2017
        Function:     SaveSKDS
        Purpose:      SaveSKDS
        Inputs:       skds
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveSKDS(SKDS skds)
        {
            Logger.Info("Invoking SaveSKDS function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardAcctSignUpDAO = scope.Resolve<ICardAcctSignUpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var skdsDto = Mapper.Map<SKDS, SkdsDTO>(skds);
                    var result = await cardAcctSignUpDAO.SaveSKDS(skdsDto);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveSKDS: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        #endregion
        public bool AutoGenerateFolder(int flag, string path)
        {
            Logger.Info("Invoking AutoGenerateFolder function");
            if (flag == 0)
            {
                string fileRoot = path;
                if (!Directory.Exists(fileRoot))
                {
                    try
                    {
                        Directory.CreateDirectory(fileRoot);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        string msg = string.Format("Error in AutoGenerateFolder: detail:{0}", ex.Message);
                        Logger.Error(msg, ex);
                        return false;
                    }
                }
            }
            return false;
        }
    }
}

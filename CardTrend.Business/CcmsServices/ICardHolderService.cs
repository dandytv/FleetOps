using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.CardHolder;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
   public interface ICardHolderService
    {
       Task<CardHolderResponse> GetGeneralInfo(string cardNo);
       Task<CardHolderResponse> GetFinancialInfo(string cardNo);
       Task<CardHolderResponse> GetPersonInfo(string entityId);
       Task<CardHolderResponse> GetChangedAcctStsDetail(string id, string refCd);
       Task<CardHolderResponse> GetCardReplacementInfo(string cardNo);
       Task<CardHolderResponse> GetCardHolders(string accountNo);
       Task<CardHolderResponse> GetLocationAcceptances(string accountNo, string cardNo);
       Task<CardHolderResponse> GetLocationAcceptance(string acctNo, string busnLoc, string cardNo);
       Task<SaveAcctSignUpResponse> ResetWebPin(string cardNo, string userId);
       Task<SaveAcctSignUpResponse> ChangeWebPin(string cardNo, string userId);
       Task<SaveAcctSignUpResponse> SaveFinancialInfo(CardFinancialInfoModel finInfoModel, string cardNo);
       Task<SaveAcctSignUpResponse> SavePersonInfo(PersonInfoModel personalInfoModel, string entityId);
       Task<SaveAcctSignUpResponse> SaveLocationAccept(LocationAcceptListModel locationAcceptModel, string accountNo, string cardNo, string userId);
       Task<SaveAcctSignUpResponse> SaveGeneralInfo(CardHolderInfoModel cardHolderInfoModel, string userId);
       Task<SaveAcctSignUpResponse> SaveCardReplacement(CardReplacement cardReplacement, string userId);
       Task<SaveAcctSignUpResponse> DeleteLocationAcceptance(string acctNo, string busnLocation, string cardNo, string userId);
       Task<SaveAcctSignUpResponse> StatusSave(ChangeStatus changeStatusModel, string userId);
    }
   public class CardHolderService : ICardHolderService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public CardHolderService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region Card Service registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<CardHolderDAO>().As<ICardHolderDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region Card Service
        /*************************************
        Created by:   dandy
        Created on:  May 22, 2017
        Function:     GetGeneralInfo
        Purpose:      GetGeneralInfo
        Inputs:       cardNo
        Returns:      CardHolderResponse
        *************************************/
        public async Task<CardHolderResponse> GetGeneralInfo(string cardNo)
        {
            Logger.Info("Invoking GetCardList fuction use EF to call SP");
            var response = new CardHolderResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var applicantHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var cardHolderInfo = await applicantHolderDAO.GeneralInfoSelect(cardNo);
                    if(cardHolderInfo != null)
                    {
                        cardHolderInfo.CardNo = cardNo;
                        response.cardHolderInfo = Mapper.Map<CardHolderInfoDTO, CardHolderInfoModel>(cardHolderInfo);
                    }
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
        Created by:   dandy
        Created on:  May 23, 2017
        Function:     GetFinancialInfo
        Purpose:      GetFinancialInfo
        Inputs:       cardNo
        Returns:      CardHolderResponse
        *************************************/
        public async Task<CardHolderResponse> GetFinancialInfo(string cardNo)
        {
            Logger.Info("Invoking GetFinancialInfo fuction use EF to call SP");
            var response = new CardHolderResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var applicantHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var cardHolderInfo = await applicantHolderDAO.FinancialInfoSelect(cardNo);
                    if(cardHolderInfo != null)
                        response.cardFinancialInfoModel = Mapper.Map<CardHolderInfoDTO, CardFinancialInfoModel>(cardHolderInfo);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFinancialInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  May 23, 2017
        Function:     GetPersonInfo
        Purpose:      GetPersonInfo
        Inputs:       cardNo
        Returns:      CardHolderResponse
         *************************************/
        public async Task<CardHolderResponse> GetPersonInfo(string entityId)
        {
            Logger.Info("Invoking GetPersonInfo fuction use EF to call SP");
            var response = new CardHolderResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var applicantHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var personalInfo = await applicantHolderDAO.PersonInfoSelect(entityId);
                    if(personalInfo != null)
                    {
                        personalInfo.EntityId = entityId;
                        response.personalInfo = Mapper.Map<PersonalInfoDTO,PersonInfoModel>(personalInfo);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPersonInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  May 23, 2017
        Function:     GetChangedAcctStsDetail
        Purpose:      GetChangedAcctStsDetail
        Inputs:       id,refCd
        Returns:      CardHolderResponse
        *************************************/
        public async Task<CardHolderResponse> GetChangedAcctStsDetail(string id, string refCd)
        {
            Logger.Info("Invoking GetChangedAcctStsDetail fuction use EF to call SP");
            var response = new CardHolderResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var applicantHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var personalInfo = await applicantHolderDAO.FtChangedAcctStsDetail(id,refCd);
                    response.changeStatus = Mapper.Map <ChangeStatusDTO,ChangeStatus>(personalInfo);
                    if (refCd.ToUpper() == "CARD")
                    {
                        response.changeStatus.CurrentStatus = await controlDAO.GetRefLib("CardSts", "1");
                        response.changeStatus.RefType = await controlDAO.GetRefLib("EventType");
                        response.changeStatus.ReasonCode = await controlDAO.GetRefLib("ReasonCd", "32");
                        response.changeStatus.ChangeStatusTo = await controlDAO.GetRefLib("CardSts", "1");
                    }
                    else if (refCd.ToUpper() == "MERCH")
                    {
                        response.changeStatus.CurrentStatus = await controlDAO.GetRefLib("MerchAcctSts");
                        response.changeStatus.RefType = await controlDAO.GetRefLib("EventType");
                        response.changeStatus.ReasonCode = await controlDAO.GetRefLib("ReasonCd");
                        response.changeStatus.ChangeStatusTo = await controlDAO.GetRefLib("MerchAcctSts");
                    }
                    else if (refCd.ToUpper() == "BUSN")
                    {
                        response.changeStatus.CurrentStatus = await controlDAO.GetRefLib("MerchAcctSts");
                        response.changeStatus.RefType = await controlDAO.GetRefLib("EventType");
                        response.changeStatus.ReasonCode = await controlDAO.GetRefLib("MerchReasonCd");
                        response.changeStatus.ChangeStatusTo = await controlDAO.GetRefLib("MerchAcctSts");
                    }
                    else if (refCd.ToUpper() == "APPC")
                    {
                        response.changeStatus.CurrentStatus = await controlDAO.GetRefLib("AppcSts");
                        response.changeStatus.RefType = await controlDAO.GetRefLib("EventType");
                        response.changeStatus.ReasonCode = await controlDAO.GetRefLib("ReasonCd");
                        response.changeStatus.ChangeStatusTo = await controlDAO.GetRefLib("AppcSts");
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetChangedAcctStsDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  May 23, 2017
        Function:     GetCardReplacementInfo
        Purpose:      GetCardReplacementInfo
        Inputs:       cardNo
        Returns:      CardHolderResponse
         *************************************/
        public async Task<CardHolderResponse> GetCardReplacementInfo(string cardNo)
        {
            Logger.Info("Invoking GetCardReplacementInfo fuction use EF to call SP");
            var response = new CardHolderResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var applicantHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var cardReplaceObj = await applicantHolderDAO.CardReplacementInfoSelect(cardNo);
                    if (cardReplaceObj != null)
                    {
                        response.cardReplacement = Mapper.Map<CardReplacementDTO, CardReplacement>(cardReplaceObj);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCardReplacementInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  May 24, 2017
        Function:     GetCardHolders
        Purpose:      GetCardHolders
        Inputs:       accountNo
        Returns:      CardHolderResponse
        *************************************/
        public async Task<CardHolderResponse> GetCardHolders(string accountNo)
        {
            Logger.Info("Invoking GetCardHolders fuction use EF to call SP");
            var response = new CardHolderResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var applicantHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var cardHolderInfors = await applicantHolderDAO.CardHolderList(accountNo);
                    if (cardHolderInfors.Count() > 0)
                    {
                        response.cardHolderInfos = Mapper.Map<List<CardHolderInfoModel>>(cardHolderInfors);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCardHolders: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  May 25, 2017
        Function:     SaveGeneralInfo
        Purpose:      SaveGeneralInfo
        Inputs:       CardHolderInfoDTO,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveGeneralInfo(CardHolderInfoModel cardHolderInfoModel, string userId)
        {
            Logger.Info("Invoking SaveGeneralInfo function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var cardHolderInfo = Mapper.Map<CardHolderInfoModel, CardHolderInfoDTO>(cardHolderInfoModel);
                    var result = await cardHolderDAO.SaveGeneralInfo(cardHolderInfo,userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveGeneralInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  May 25, 2017
        Function:     ResetWebPin
        Purpose:      ResetWebPin
        Inputs:       cardNo,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> ResetWebPin(string cardNo, string userId)
        {
            Logger.Info("Invoking ResetWebPin function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await cardHolderDAO.WebPinReset(cardNo, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in ResetWebPin: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  May 25, 2017
        Function:     ChangeWebPin
        Purpose:      ChangeWebPin
        Inputs:       cardNo,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> ChangeWebPin(string cardNo, string userId)
        {
            Logger.Info("Invoking ChangeWebPin function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await cardHolderDAO.WebPinChange(cardNo, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in ChangeWebPin: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  May 25, 2017
        Function:     SaveFinancialInfo
        Purpose:      SaveFinancialInfo
        Inputs:       CardHolderInfoDTO,cardNo
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveFinancialInfo(CardFinancialInfoModel finInfoModel, string cardNo)
        {
            Logger.Info("Invoking SaveFinancialInfo function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var finInfo = Mapper.Map<CardFinancialInfoModel, CardHolderInfoDTO>(finInfoModel);
                    var result = await cardHolderDAO.SaveFinancialInfo(finInfo, cardNo);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveFinancialInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:   May 25, 2017
        Function:     SavePersonInfo
        Purpose:      SavePersonInfo
        Inputs:       CardHolderInfoDTO,cardNo
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SavePersonInfo(PersonInfoModel personalInfoModel, string entityId)
        {
            Logger.Info("Invoking SavePersonInfo function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var personalInfoDto = Mapper.Map<PersonInfoModel, PersonalInfoDTO>(personalInfoModel);
                    var result = await cardHolderDAO.SavePersonInfo(personalInfoDto, entityId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SavePersonInfo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:   May 29, 2017
        Function:     GetLocationAcceptances
        Purpose:      GetLocationAcceptances
        Inputs:       accountNo,cardNo
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<CardHolderResponse> GetLocationAcceptances(string accountNo, string cardNo)
        {
            Logger.Info("Invoking GetLocationAcceptance fuction use EF to call SP");
            var response = new CardHolderResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var applicantHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var acceptLocation = await applicantHolderDAO.GetLocationAcceptances(accountNo, cardNo);
                    if (acceptLocation.Count() > 0)
                        response.locationAccepts = Mapper.Map<IList<LocationAcceptDTO>,IList<LocationAcceptListModel>>(acceptLocation);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetLocationAcceptance: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan
        Created on:   June 01, 2017
        Function:     GetLocationAcceptance
        Purpose:      GetLocationAcceptance
        Inputs:       acctNo,busnLoc,cardNo
        Returns:      CardHolderResponse
        *************************************/
        public async Task<CardHolderResponse> GetLocationAcceptance(string acctNo, string busnLoc, string cardNo)
        {
            Logger.Info("Invoking GetLocationAcceptance fuction use EF to call SP");
            var response = new CardHolderResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var applicantHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var locationAccept = await applicantHolderDAO.LocationAcceptanceSelect(acctNo, busnLoc, cardNo);
                    if(locationAccept != null)
                       response.locationAccept = Mapper.Map<LocationAcceptDTO,LocationAcceptListModel>(locationAccept);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetLocationAcceptance: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        } 
        /*************************************
        Created by:   dandy
        Created on:   May 30, 2017
        Function:     SaveCardReplacement
        Purpose:      SaveCardReplacement
        Inputs:       personalInfoDto,entityId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveCardReplacement(CardReplacement cardReplacement, string userId)
        {
            Logger.Info("Invoking SaveCardReplacement function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var cardReplacementDto = Mapper.Map<CardReplacementDTO>(cardReplacement);
                    var result = await cardHolderDAO.SaveCardReplacement(cardReplacementDto, userId);
                    var message = await controlDAO.GetMessageCode(result.Flag);
                    response.returnValue.NewcardNo = result.paraOut.NewcardNo;
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveCardReplacement: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   dandy
        Created on:   May 30, 2017
        Function:     DeleteLocationAcceptance
        Purpose:      DeleteLocationAcceptance
        Inputs:       acctNo,busnLocation,cardNo
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> DeleteLocationAcceptance(string acctNo, string busnLocation, string cardNo,string userId)
        {
            Logger.Info("Invoking DeleteLocationAcceptance function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await cardHolderDAO.DeleteLocationAcceptance(acctNo, busnLocation, cardNo,userId);
                    var msgRetrieve = await controlDAO.GetMessageCode(result);
                    response.desp = msgRetrieve.Descp;
                    response.flag = msgRetrieve.Flag;
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
        Created by:   dandy
        Created on:   07 06, 2017
        Function:     StatusSave
        Purpose:      StatusSave
        Inputs:       changeStatusDto,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> StatusSave(ChangeStatus changeStatusModel, string userId)
        {
            Logger.Info("Invoking StatusSave function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var changeStatusDto = Mapper.Map<ChangeStatus, ChangeStatusDTO>(changeStatusModel);
                    var result = await cardHolderDAO.StatusSave(changeStatusDto, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in StatusSave: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:   07 06, 2017
        Function:     SaveLocationAccept
        Purpose:      SaveLocationAccept
        Inputs:       changeStatusDto,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveLocationAccept(LocationAcceptListModel locationAcceptModel, string accountNo, string cardNo, string userId)
        {
            Logger.Info("Invoking SaveLocationAccept function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var cardHolderDAO = scope.Resolve<ICardHolderDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var locationAcceptDTO = Mapper.Map<LocationAcceptListModel, LocationAcceptDTO>(locationAcceptModel);
                    var result = await cardHolderDAO.SaveLocationAccept(locationAcceptDTO, accountNo, cardNo,userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveLocationAccept: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        #endregion
    }
}

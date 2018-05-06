using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.Applicant;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
   public interface IApplicantSignUpService
    {
       Task<ApplicantSignUpResponse> GetApplicantInfo(string applidData, string appcidData, string acctNo);
       Task<ApplicantSignUpResponse> GetFinancialInfo(string cardNo);
       Task<ApplicantSignUpResponse> GetApllicants(string applicationId, string acctNo);
       Task<SaveAcctSignUpResponse> SaveFinancial(CardFinancialInfoModel cardFinancialInfoModel, string appcId);
       Task<SaveAcctSignUpResponse> SaveApplicantInfo(CardAppcInfoModel cardAppcInfoModel, string applId, string appcid, string userId);
    }
    public class ApplicantSignUpService: IApplicantSignUpService
    {
       private static Autofac.IContainer Container { get; set; }
       private static ICardTrendLogger Logger;
       public ApplicantSignUpService()
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
           afBuilder.RegisterType<ApplicantSignUpDAO>().As<IApplicantSignUpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
           afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
           afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
           Container = afBuilder.Build();
       }
       #endregion
        #region Applicant Service
       /*************************************
         Created by:   Tuan Tran
         Created on:   June 08, 2017
         Function:     GetApplicantInfo
         Purpose:      GetApplicantInfo
         Inputs:       applidData,appcidData,acctNo
         Returns:      ApplicantSignUpResponse
        *************************************/
       public async Task<ApplicantSignUpResponse> GetApplicantInfo(string applidData, string appcidData, string acctNo)
       {
           Logger.Info("Invoking GetApplicantInfo function");
           var response = new ApplicantSignUpResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var applicantSignUpDAO = scope.Resolve<IApplicantSignUpDAO>();
                   var result = await applicantSignUpDAO.GetApplicantInfo(applidData, appcidData, acctNo);
                   if(result != null)
                   {
                       response.cardAppcInfo = Mapper.Map<CardAppcInfoDTO,CardAppcInfoModel>(result);
                   }
               }
               response.Status = ResponseStatus.Success;
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetApplicantInfo: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
         Created by:   Tuan Tran
         Created on:   June 08, 2017
         Function:     GetFinancialInfo
         Purpose:      GetFinancialInfo
         Inputs:       applidData,appcidData,acctNo
         Returns:      ApplicantSignUpResponse
        *************************************/
       public async Task<ApplicantSignUpResponse> GetFinancialInfo(string cardNo)
       {
           Logger.Info("Invoking GetFinancialInfo function");
           var response = new ApplicantSignUpResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var applicantSignUpDAO = scope.Resolve<IApplicantSignUpDAO>();
                   var result = await applicantSignUpDAO.GetFinancialInfo(cardNo);
                   if(result != null)
                   {
                       response.cardFinancialInfo = Mapper.Map<CardFinancialInfoDTO,CardFinancialInfoModel>(result);
                   }
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
        Created by:   Tuan Tran
        Created on:   June 08, 2017
        Function:     GetApllicants
        Purpose:      GetApllicants
        Inputs:       applicationId,acctNo
        Returns:      ApplicantSignUpResponse
       *************************************/
       public async Task<ApplicantSignUpResponse> GetApllicants(string applicationId, string acctNo)
       {
           Logger.Info("Invoking GetApllicants function");
           var response = new ApplicantSignUpResponse()
           {
               Status = ResponseStatus.Failure,
           };

           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var applicantSignUpDAO = scope.Resolve<IApplicantSignUpDAO>();
                   var controlDAO = scope.Resolve<IControlDAO>();
                   var results = await applicantSignUpDAO.GetApllicants(applicationId, acctNo);
                   if(results.Count() > 0)
                   {
                       foreach(var item in results)
                       {
                           item.CardTypeLst = await controlDAO.WebGetCardType();
                           item.BranchCdLst = await controlDAO.GetRefLib("BranchCd");
                           item.DivisionCodeLst = await controlDAO.GetRefLib("DivisionCd");
                           item.DeptCdLst = await controlDAO.GetRefLib("DeptCd");
                       }
                       response.cardAppcInfoLst = Mapper.Map<List<CardAppcInfoDTO>,List<CardAppcInfoModel>>(results);
                   }

               }
               response.Status = ResponseStatus.Success;
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in GetApllicants: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
        Created by:   dandy
        Created on:  june 08, 2017
        Function:     SaveFinancial
        Purpose:      SaveFinancial
        Inputs:       cardFinancialInfoDTO,appcId
        Returns:      SaveAcctSignUpResponse
        *************************************/
       public async Task<SaveAcctSignUpResponse> SaveFinancial(CardFinancialInfoModel cardFinancialInfoModel, string appcId)
       {
           Logger.Info("Invoking SaveFinancial function");
           var response = new SaveAcctSignUpResponse()
           {
               Status = ResponseStatus.Failure,
           };
           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var applicantSignUpDAO = scope.Resolve<IApplicantSignUpDAO>();
                   var controlDAO = scope.Resolve<IControlDAO>();
                   var cardFinancialInfoDTO = Mapper.Map<CardFinancialInfoModel, CardFinancialInfoDTO>(cardFinancialInfoModel);
                   var result = await applicantSignUpDAO.SaveFinancial(cardFinancialInfoDTO, appcId);
                   var message = await controlDAO.GetMessageCode(result);
                   response.desp = message.Descp;
                   response.flag = message.Flag;
               }
               response.Status = ResponseStatus.Success;
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in SaveFinancial: detail:{0}", ex.Message);
               Logger.Error(msg, ex);
               response.Status = ResponseStatus.Exception;
               response.flag = 1;
               response.Message = msg;
           }
           return response;
       }
       /*************************************
        Created by:   dandy
        Created on:  june 08, 2017
        Function:     SaveApplicantInfo
        Purpose:      SaveApplicantInfo
        Inputs:       CardAppcInfoDTO,applId,appcid,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
       public async Task<SaveAcctSignUpResponse> SaveApplicantInfo(CardAppcInfoModel cardAppcInfoModel, string applId, string appcid, string userId)
       {
           Logger.Info("Invoking SaveApplicantInfo function");
           var response = new SaveAcctSignUpResponse()
           {
               Status = ResponseStatus.Failure,
           };
           try
           {
               using (var scope = Container.BeginLifetimeScope())
               {
                   var applicantSignUpDAO = scope.Resolve<IApplicantSignUpDAO>();
                   var controlDAO = scope.Resolve<IControlDAO>();
                   var cardAppcInfoDto = Mapper.Map<CardAppcInfoModel, CardAppcInfoDTO>(cardAppcInfoModel);
                   var result = await applicantSignUpDAO.SaveApplicantInfo(cardAppcInfoDto, applId, appcid, userId);
                   var message = await controlDAO.GetMessageCode(Convert.ToInt32(result.Flag));
                   response.desp = message.Descp;
                   response.returnValue.AppcId = result.paraOut.AppcId;
                   response.returnValue.EntityId = result.paraOut.EntityId;
                   response.flag = message.Flag;
               }
               response.Status = ResponseStatus.Success;
           }
           catch (Exception ex)
           {
               string msg = string.Format("Error in SaveApplicantInfo: detail:{0}", ex.Message);
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

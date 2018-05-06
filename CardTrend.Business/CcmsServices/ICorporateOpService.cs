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
  public  interface ICorporateOpService
    {
      Task<CorporateResponse> GetCorpAcctDetail(string CorpCd);
      Task<CorporateResponse> GetCorpAcctList();
      Task<CorporateResponse> GetAcctCorpList(string corpCd);
      Task<SaveCorpAcctResponse> SaveCorporateAcct(Corporate corporateModel,string userId, string func);
    }
  public class CorporateOpService : ICorporateOpService
  {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public CorporateOpService()
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
            afBuilder.RegisterType<CorporateOpDAO>().As<ICorporateOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region CorporateService
        /*************************************
           Created by:   Tuan Tran
           Created on:   Mar 6, 2017
           Function:     GetCorpAcctDetail
           Purpose:      GetCorpAcctDetail
           Inputs:       CorpCd
           Returns:      CorporateResponse
        *************************************/
        public async Task<CorporateResponse> GetCorpAcctDetail(string CorpCd)
        {
            Logger.Info("Invoking GetCorpAcctDetail fuction use EF to call SP");
            var response = new CorporateResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var coporateDAO = scope.Resolve<ICorporateOpDAO>();
                    var result = await coporateDAO.GetCorpAcctDetail(CorpCd);
                    if(result != null)
                        response.coporate = Mapper.Map<CorporateDTO,Corporate>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCorpAcctDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   Tuan Tran
           Created on:   March 6, 2017
           Function:     GetCorpAcctList
           Purpose:      GetCorpAcctList
           Inputs:       
           Returns:      CorporateResponse
         *************************************/
        public async Task<CorporateResponse> GetCorpAcctList()
        {
            Logger.Info("Invoking GetCorpAcctList fuction use EF to call SP");
            var response = new CorporateResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var coporateDAO = scope.Resolve<ICorporateOpDAO>();
                    var results = await coporateDAO.GetCorpAcctList();
                    if (results.Count() > 0)
                        response.corporates = Mapper.Map<IList<CorporateDTO>,IList<Corporate>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCorpAcctList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   March 6, 2017
         Function:     GetAcctCorpList
         Purpose:      GetAcctCorpList
         Inputs:       corpCd
         Returns:      CorporateResponse
       *************************************/
        public async Task<CorporateResponse> GetAcctCorpList(string corpCd)
        {
            Logger.Info("Invoking GetCorpAcctList fuction use EF to call SP");
            var response = new CorporateResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var coporateDAO = scope.Resolve<ICorporateOpDAO>();
                    var results = await coporateDAO.GetAcctCorpList(corpCd);
                    if (results.Count() > 0)
                        response.generalInfoes = Mapper.Map<IList<GeneralInfoDTO>, IList<GeneralInfoModel>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctCorpList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   Tuan Tran
           Created on:   Mar 6, 2017
           Function:     SaveCorporateAcct
           Purpose:      SaveCorporateAcct
           Inputs:       CorporateDTO,func
           Returns:      SaveCorpAcctResponse
           *************************************/
        public async Task<SaveCorpAcctResponse> SaveCorporateAcct(Corporate corporateModel,string userId,string func)
        {
            Logger.Info("Invoking SaveCorporateAcct function");
            var response = new SaveCorpAcctResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var coporateDAO = scope.Resolve<ICorporateOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var corporateDto = Mapper.Map<Corporate, CorporateDTO>(corporateModel);
                    corporateDto.UserId = userId;
                    var result = await coporateDAO.SaveCorporateAcct(corporateDto, func);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveCorporateAcct: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.desp = ex.Message;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        #endregion
  }
}

using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.DAL.DAOWEB;
using CardTrend.Domain.WebDto;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsWebServices
{
    public interface ISecurityOpService
    {
        SecurityOpResponse GetUserAccessModuleList(string userId, string level);
        Task<SecurityOpResponse> GetUserAccessPgNCtrlList(string accessInd, string userId, List<string> moduleList, List<string> pageList, string ctrlId);
    }
    public class SecurityOpService : ISecurityOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public SecurityOpService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }         
        }
        #region RegisterComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<SecurityOpDAO>().As<ISecurityOpDAO>().WithParameter("connString", AppConfigurationHelper.CCMSEntityWebCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region Services
        /*************************************
        Created by:   dandy boy
        Created on:   june 28, 2017
        Function:     GetUserAccessModuleList
        Purpose:      GetUserAccessModuleList
        Inputs:       userId,level
        Returns:      SecurityOpResponse
        *************************************/
        public SecurityOpResponse GetUserAccessModuleList(string userId, string level)
        {
            Logger.Info("Invoking GetUserAccessListSelect function");
            var response = new SecurityOpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var securityOpDAO = scope.Resolve<ISecurityOpDAO>();
                    var results = securityOpDAO.WebGetUserAccessModuleList(userId, level);
                    if(results.Count() > 0)
                        response.webModules = Mapper.Map<List<UserAccessLevelDTO>, List<WebModule>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetUserAccessModuleList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   june 28, 2017
        Function:     GetUserAccessPgNCtrlList
        Purpose:      GetUserAccessPgNCtrlList
        Inputs:       accessInd,userId,moduleList,pageList,ctrlId
        Returns:      SecurityOpResponse
        *************************************/
        public async Task<SecurityOpResponse> GetUserAccessPgNCtrlList(string accessInd, string userId, List<string> moduleList, List<string> pageList, string ctrlId)
        {
            Logger.Info("Invoking GetUserAccessPgNCtrlList function");
            var response = new SecurityOpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var securityOpDAO = scope.Resolve<ISecurityOpDAO>();
                    var results = await securityOpDAO.WebGetUserAccessPgNCtrlList(accessInd,userId,moduleList,pageList,ctrlId);
                    if (results.Count() > 0)
                    {
                        if(pageList == null)
                        {
                            response.webPages = Mapper.Map<List<UserAccessLevelDetailDTO>, List<WebPage>>(results);
                        }else
                        {
                            response.webPageSections = Mapper.Map<List<UserAccessLevelDetailDTO>, List<WebPageSection>>(results);
                        }
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetUserAccessPgNCtrlList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        #endregion
    }
}

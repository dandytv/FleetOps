using Autofac;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAOWEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardTrend.Domain;
using CardTrend.DAL.DAO;
using AutoMapper;
using CardTrend.Domain.WebDto;
using ModelSector;
using System.Web.Mvc;
using CardTrend.Domain.Dto.ControlList;

namespace CardTrend.Business
{
    public interface IUserAccessService
    {
        UserAccessResponse GetUserAccessDetail(string accessInd, string userId);
        UserAccessResponse GetUserAccesses(string accessInd);
        Task<UserAccessResponse> GetUserTitleList();
        Task<UserAccessResponse> GetUserAccessListSelect();
        Task<IList<SelectListItem>> GetMap();
        Task<IList<SelectListItem>> GetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true);
        Task<SaveAcctSignUpResponse> SaveUserLogin(string username, string password);
        Task<SaveAcctSignUpResponse> SaveUserAccess(UserAccess userAccess, bool isUpdate = false);
        Task<SaveAcctSignUpResponse> SaveUserAccessMapping(UserAccess userAccess);
        Task<SaveAcctSignUpResponse> SaveWebUserAccessLevel(List<WebModule> moduleList, List<WebPage> pageList, List<WebControl> ctrlList, List<WebPageSection> sectionList, string userId);
    }
    
    public class UserAccessService : IUserAccessService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;

        public UserAccessService()
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
            afBuilder.RegisterType<UserAccessDAO>().As<IUserAccessDAO>().WithParameter("connString", AppConfigurationHelper.CCMSEntityWebCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion

        #region Service
        public async Task<UserAccessResponse> GetUserTitleList()      
        {
            Logger.Info("Invoking GetUserTitleList fuction use EF to call SP");
            var response = new UserAccessResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var results = await userAccessDAO.GetUserTitles();
                    response.userTitles = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetUserTitleList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        public async Task<SaveAcctSignUpResponse> SaveUserLogin(string username, string password)
        {
            Logger.Info("Invoking SaveUserLogin fuction use EF to call SP");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = userAccessDAO.SaveUserLogin(username, password);
                    var message = await controlDAO.GetMessageCode(result);
                    response.Message = message.Descp;
                    response.flag = message.Flag;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveUserLogin: detail:{0}", ex.Message);
                response.Message = msg;
                response.Status = ResponseStatus.Exception;
                Logger.Error(msg, ex);
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   Feb 8, 2017
        Function:     GetUserAccessListSelect
        Purpose:      GetUserAccessListSelect
        Inputs:       
        Returns:      UserAccessResponse
        *************************************/
        public async Task<UserAccessResponse> GetUserAccessListSelect()
        {
            Logger.Info("Invoking GetUserAccessListSelect function");
            var response = new UserAccessResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var results = await userAccessDAO.GetUserAccessListSelect();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetUserAccessListSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   Feb 8, 2017
        Function:     GetUserAccessDetail
        Purpose:      GetUserAccessDetail
        Inputs:       accessInd,userId
        Returns:      UserAccessResponse
        *************************************/
        public UserAccessResponse GetUserAccessDetail(string accessInd, string userId)
        {
            Logger.Info("Invoking GetUserAccessDetail function");
            var response = new UserAccessResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var result = userAccessDAO.GetUserAccessDetail(accessInd,userId);
                    if(result != null)
                        response.userAccess = Mapper.Map<UserAccessDTO,UserAccess>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetUserAccessDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   Feb 8, 2017
        Function:     SaveUserAccess
        Purpose:      SaveUserAccess
        Inputs:       UserAccess,isUpdate
        Returns:      UserAccessResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveUserAccess(UserAccess userAccess, bool isUpdate = false)
        {
            Logger.Info("Invoking SaveUserAccess function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var userAccessDto = Mapper.Map<UserAccess, UserAccessDTO>(userAccess);
                    var result = await userAccessDAO.SaveUserAccess(userAccessDto, isUpdate);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveUserAccess: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   Feb 8, 2017
        Function:     SaveUserAccessMapping
        Purpose:      SaveUserAccessMapping
        Inputs:       UserAccess
        Returns:      UserAccessResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveUserAccessMapping(UserAccess userAccess)
        {
            Logger.Info("Invoking SaveUserAccess function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var userAccessDto = Mapper.Map<UserAccess, UserAccessDTO>(userAccess);
                    var result = await userAccessDAO.SaveWebUserAccessMapping(userAccessDto);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveUserAccess: detail:{0}", ex.Message);
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
        Function:     GetMap
        Purpose:      GetMap
        Inputs:       
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetMap()
        {
            Logger.Info("Invoking GetMap function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var results = await userAccessDAO.WebGetMap();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMap: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   july 26, 2017
        Function:     GetUserAccesses
        Purpose:      GetUserAccesses
        Inputs:       accessInd
        Returns:      UserAccessResponse
        *************************************/
        public UserAccessResponse GetUserAccesses(string accessInd)
        {
            Logger.Info("Invoking GetUserAccesses function");
            var response = new UserAccessResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var results = userAccessDAO.GetUserAccessList(accessInd);
                    if(results.Count() > 0)
                        response.userAccesses = Mapper.Map<IList<UserAccessListDTO>,IList<UserAccess>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetUserAccesses: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 20, 2017
        Function:     GetRefLib
        Purpose:      GetRefLib
        Inputs:       refType,RefNo,RefInd,RefId,PrependNull
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true)
        {
            Logger.Info("Invoking GetRefLib function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var results = await userAccessDAO.GetRefLib(refType, RefNo, RefInd, RefId, PrependNull);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRefLib: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   Feb 8, 2017
        Function:     SaveWebUserAccessLevel
        Purpose:      SaveWebUserAccessLevel
        Inputs:       ModuleList,PageList,CtrlList,SectionList,userId
        Returns:      UserAccessResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveWebUserAccessLevel(List<WebModule> moduleList, List<WebPage> pageList, List<WebControl> ctrlList, List<WebPageSection> sectionList, string userId)
        {
            Logger.Info("Invoking SaveWebUserAccessLevel function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var userAccessDAO = scope.Resolve<IUserAccessDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var moduleListDto = Mapper.Map<List<WebModule>,List<WebModuleDTO>>(moduleList);
                    var pageListDto = Mapper.Map<List<WebPage>, List<WebPageDTO>>(pageList);
                    var ctrlListDto = Mapper.Map<List<WebControl>, List<WebControlDTO>>(ctrlList);
                    var sectionListDto = Mapper.Map<List<WebPageSection>, List<WebPageSectionDTO>>(sectionList);
                    var result = await userAccessDAO.SaveWebUserAccessLevel(moduleListDto, pageListDto, ctrlListDto, sectionListDto, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveWebUserAccessLevel: detail:{0}", ex.Message);
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

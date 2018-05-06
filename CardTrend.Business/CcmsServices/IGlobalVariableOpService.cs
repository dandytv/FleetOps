using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.GlobalVariables;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
    public interface IGlobalVariableOpService
    {
        Task<LookupParameterListResponse> WebUndefinedProdType();
        Task<LookupParameterListResponse> GetEventTypes(string eventTypeId);
        Task<LookupParameterListResponse> GetProdGroupRefs();
        Task<LookupParameterListResponse> GetRefList(string refType);
        Task<LookupParameterListResponse> GetRefDetail(string refType, string refCd, string refNo, string refId);
        Task<LookupParameterListResponse> GetProdRefs();
        Task<LookupParameterListResponse> GetRebatePlans(string planId);
        Task<LookupParameterListResponse> GetRebatePlanDetails();
        Task<LookupParameterListResponse> GetDetailEventTypes();
        Task<LookupParameterListResponse> GetEventTypeTemplates(string eventTypeId);
        Task<LookupParameterListResponse> GetDetailProdRefs(string prodCd);
        Task<LookupParameterListResponse> GetProdGroupRefSelect(string grodGroup);
        Task<SaveGeneralInfoResponse> SaveRefMaint(LookupParameters lookupParameterModel, string flag);
        Task<SaveAcctSignUpResponse> SaveProdGroupRefMaint(LookupParameters lookupParameterModel, string userId);
        Task<SaveAcctSignUpResponse> SaveProdRefMaint(LookupParameters lookupParameterModel, string userId);
        Task<SaveAcctSignUpResponse> SaveEventTypeMaint(LookupParameters lookupParameterModel, string userId);
        Task<SaveAcctSignUpResponse> SaveRebatePlanMaint(LookupParameters lookupParameterModel, string userId);
    }
    public class GlobalVariableOpService : IGlobalVariableOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public GlobalVariableOpService()
        {
            // Register the dependency and resolve
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region GlobalVariableOpService registerComponent
        public static void RegisterDAOComponents()
        {
            // Mapped IGlobalVariableOpDAO to GlobalVariableOpDAO so that whenever an instance of IGlobalVariableOpDAO is required then it will create an object of the GlobalVariableOpDAO class
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<GlobalVariableOpDAO>().As<IGlobalVariableOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region GlobalVariableOp Service
        /*************************************
         Created by:   Tuan Tran
         Created on:   March 8, 2017
         Function:     WebUndefinedProdType
         Purpose:      WebUndefinedProdType
         Inputs:       
         Returns:      LookupParameterListResponse
        *************************************/
        public async Task<LookupParameterListResponse> WebUndefinedProdType()
        {
            Logger.Info("Invoking WebUndefinedProdType function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var result = await globalVariableOpDAO.WebUndefinedProdType();
                    if(result.Count() > 0)
                        response.lookupParameters = Mapper.Map<IList<LookupParameterDTO>,IList<LookupParameters>>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebUndefinedProdType: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
          Created by:   dandy boy
          Created on:   March 8, 2017
          Function:     GetEventTypes
          Purpose:      GetEventTypes
          Inputs:       
          Returns:      LookupParameterListResponse
       *************************************/
        public async Task<LookupParameterListResponse> GetEventTypes(string eventTypeId)
        {
            Logger.Info("Invoking GetEventTypes function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var results = await globalVariableOpDAO.WebEventTypeSelect(eventTypeId);
                    if (results.Count() > 0)
                        response.lookupParameters = Mapper.Map<List<EventTypeDTO>,List<LookupParameters>>(results);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEventTypes: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
          Created by:   dandy boy
          Created on:   March 29, 2017
          Function:     GetProdGroupRefs
          Purpose:      GetProdGroupRefs
          Inputs:       
          Returns:      LookupParameterListResponse
        *************************************/
        public async Task<LookupParameterListResponse> GetProdGroupRefs()
        {
            Logger.Info("Invoking GetProdGroupRefs function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var results = await globalVariableOpDAO.WebProdGroupRefs();
                    if(results.Count() > 0)
                        response.lookupParameters = Mapper.Map<IList<ProdGroupRefDTO>,IList<LookupParameters>>(results);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetProdGroupRefs: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
          Created by:   dandy boy
          Created on:   March 29, 2017
          Function:     GetRefList
          Purpose:      GetRefList
          Inputs:       refType
          Returns:      LookupParameterListResponse
       *************************************/
        public async Task<LookupParameterListResponse> GetRefList(string refType)
        {
            Logger.Info("Invoking GetRefList function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var results = await globalVariableOpDAO.WebRefListSelect(refType);
                    var list = new List<LookupParameterDTO>();
                    if(results.Count() > 0)
                    {
                        foreach(var item in results)
                        {
                            LookupParameterDTO lookParameter = new LookupParameterDTO();
                            if (refType.ToLower() == "state")
                            {
                                lookParameter.Country = item.Country;
                                lookParameter.ParamCd = item.CtryCd.ToString();
                                lookParameter.StateName = item.StateName;
                                lookParameter.StateCd = item.StateCd;
                            }
                            else if (refType.ToLower() == "city")
                            {
                                lookParameter.Country = item.Country;
                                lookParameter.StateName = item.State;
                                lookParameter.ParamCd = item.ParamCd;
                                lookParameter.City = item.City;
                            }else
                            {
                                lookParameter.Descp = item.Descp;
                                lookParameter.ParamCd = item.ParamCd;
                            }
                            list.Add(lookParameter);
                        }
                    }
                    response.lookupParameters = Mapper.Map<List<LookupParameterDTO>, List<LookupParameters>>(list);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRefList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
          Created by:   dandy boy
          Created on:   March 30, 2017
          Function:     GetRefDetail
          Purpose:      GetRefDetail
          Inputs:       refType,refCd,refNo,refId
          Returns:      LookupParameterListResponse
       *************************************/
        public async Task<LookupParameterListResponse> GetRefDetail(string refType, string refCd, string refNo, string refId)
        {
            Logger.Info("Invoking GetRefDetail function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var result = await globalVariableOpDAO.WebRefDetail(refType, refCd, refNo, refId);
                    if(result != null)
                    {
                        LookupParameterDTO lookParameter = new LookupParameterDTO();
                        if (refType.ToLower() == "state")
                        {
                            lookParameter.Country = Convert.ToString(result.CtryCd);
                            lookParameter.StateCd = result.StateCd;
                            lookParameter.StateName = result.Descp;
                        }
                        else if (refType.ToLower() == "city")
                        {
                            lookParameter.ParamCd = result.ParamCd;
                            lookParameter.Country = Convert.ToString(result.RefNo);
                            lookParameter.StateCd = result.RefId;
                            lookParameter.City = result.Descp;
                        }
                        else
                        {
                            lookParameter.ParamCd = result.ParamCd;
                            lookParameter.Descp = result.Descp;
                        }
                        response.lookupParameter = Mapper.Map<LookupParameterDTO, LookupParameters>(lookParameter);
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRefDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
          Created by:   dandy boy
          Created on:   March 30, 2017
          Function:     GetProdRefs
          Purpose:      GetProdRefs
          Inputs:       
          Returns:      LookupParameterListResponse
        *************************************/
        public async Task<LookupParameterListResponse> GetProdRefs()
        {
            Logger.Info("Invoking GetProdRefs function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var results = await globalVariableOpDAO.WebProdRefListSelect();
                    if(results.Count() > 0)
                        response.lookupParameters = Mapper.Map<IList<ProdRefDTO>,IList<LookupParameters>>(results);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetProdRefs: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   dandy boy
         Created on:   March 30, 2017
         Function:     GetRebatePlans
         Purpose:      GetRebatePlans
         Inputs:       planId
         Returns:      LookupParameterListResponse
       *************************************/
        public async Task<LookupParameterListResponse> GetRebatePlans(string planId)
        {
            Logger.Info("Invoking GetRebatePlans function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var results = await globalVariableOpDAO.WebRebatePlanSelect(planId);
                    if(results.Count() > 0)
                        response.lookupParameters = Mapper.Map<List<RebatePlanDTO>, List<LookupParameters>>(results);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRebatePlans: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
          /*************************************
          Created by:   dandy boy
          Created on:   March 31, 2017
          Function:     GetRebatePlanDetails
          Purpose:      GetRebatePlanDetails
          Inputs:       
          Returns:      LookupParameterListResponse
          *************************************/
        public async Task<LookupParameterListResponse> GetRebatePlanDetails()
        {
            Logger.Info("Invoking GetRebatePlanDetails function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var rebatePlanDetails = await globalVariableOpDAO.WebRebatePlanListSelect();
                    if(rebatePlanDetails.Count() > 0)
                        response.lookupParameters = Mapper.Map<IList<RebatePlanDetailDTO>,IList<LookupParameters>>(rebatePlanDetails);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRebatePlanDetails: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   dandy boy
           Created on:   March 31, 2017
           Function:     GetDetailEventTypes
           Purpose:      GetDetailEventTypes
           Inputs:       
           Returns:      LookupParameterListResponse
        *************************************/
        public async Task<LookupParameterListResponse> GetDetailEventTypes()
        {
            Logger.Info("Invoking GetDetailEventTypes function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var results = await globalVariableOpDAO.WebEventTypeListSelect();
                    if(results.Count() > 0)
                        response.lookupParameters = Mapper.Map<List<EventTypeDTO>, List<LookupParameters>>(results);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetDetailEventTypes: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   dandy boy
         Created on:   April 3, 2017
         Function:     SaveRefMaint
         Purpose:      SaveRefMaint
         Inputs:       
         Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveGeneralInfoResponse> SaveRefMaint(LookupParameters lookupParameterModel, string flag)
        {
            Logger.Info("Invoking SaveRefMaint function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    int result = 0;
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var lookupParameter = Mapper.Map<LookupParameterDTO>(lookupParameterModel);
                    if(lookupParameter.Type.ToLower() == "state")
                    {
                        result = await globalVariableOpDAO.WebRefMaint(lookupParameter.Type, lookupParameter.StateCd, lookupParameter.Country, null, lookupParameter.StateName, flag);
                    }else if(lookupParameter.Type.ToLower() == "city")
                    {
                        result = await globalVariableOpDAO.WebRefMaint(lookupParameter.Type, lookupParameter.ParamCd, lookupParameter.Country, lookupParameter.StateCd, lookupParameter.City, flag);
                    }else
                    {
                        result = await globalVariableOpDAO.WebRefMaint(lookupParameter.Type, lookupParameter.ParamCd, null, null, lookupParameter.ParameterDescp, flag);
                    }
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveRefMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   dandy boy
         Created on:   April 3, 2017
         Function:     GetDetailProdRefs
         Purpose:      GetDetailProdRefs
         Inputs:       
         Returns:      LookupParameterListResponse
        *************************************/
        public async Task<LookupParameterListResponse> GetDetailProdRefs(string prodCd)
        {
            Logger.Info("Invoking GetDetailProdRefs function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var results = await globalVariableOpDAO.WebProdRefSelect(prodCd);
                    if(results.Count() > 0)
                    {
                      
                        response.lookupParameters = Mapper.Map<IList<ProdRefDTO>, IList<LookupParameters>>(results);
                    }
                  
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetDetailProdRefs: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
         Created by:   dandy boy
         Created on:   April 07, 2017
         Function:     GetProdGroupRefSelect
         Purpose:      GetProdGroupRefSelect
         Inputs:       grodGroup
         Returns:      LookupParameterListResponse
        *************************************/
        public async Task<LookupParameterListResponse> GetProdGroupRefSelect(string grodGroup)
        {
            Logger.Info("Invoking GetProdGroupRefSelect function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var results = await globalVariableOpDAO.WebProdGroupRefSelect(grodGroup);
                    if(results.Count() > 0)
                        response.lookupParameters = Mapper.Map<IList<LookupParameterDTO>, IList<LookupParameters>>(results);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetProdGroupRefSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   dandy boy
         Created on:   April 07, 2017
         Function:     GetEventTypeTemplates
         Purpose:      GetEventTypeTemplates
         Inputs:       eventTypeId
         Returns:      LookupParameterListResponse
        *************************************/
        public async Task<LookupParameterListResponse> GetEventTypeTemplates(string eventTypeId)
        {
            Logger.Info("Invoking GetEventTypeTemplates function");
            var response = new LookupParameterListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var evtTypeId = Convert.ToInt64(eventTypeId);
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var results = await globalVariableOpDAO.GetEventTypeTemplates(evtTypeId);
                    if (results.Count() > 0)
                        response.TmplDisplays = Mapper.Map<IList<TmplDisplayDTO>, IList<TmplDisplayer>>(results);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEventTypeTemplates: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   dandy
        Created on:  July 03, 2017
        Function:     WebProdGroupRefMaint
        Purpose:      WebProdGroupRefMaint
        Inputs:       LookupParameterDTO,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveProdGroupRefMaint(LookupParameters lookupParameterModel, string userId)
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
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var _LookupParameters = Mapper.Map<LookupParameterDTO>(lookupParameterModel);
                    var result = await globalVariableOpDAO.WebProdGroupRefMaint(_LookupParameters, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebProdGroupRefMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  July 10, 2017
        Function:     SaveEventTypeMaint
        Purpose:      SaveEventTypeMaint
        Inputs:       LookupParameterDTO,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveEventTypeMaint(LookupParameters lookupParameterModel, string userId)
        {
            Logger.Info("Invoking SaveEventTypeMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var _LookupParameters = Mapper.Map<EventTypeDTO>(lookupParameterModel);
                    var result = await globalVariableOpDAO.WebEventTypeMaint(_LookupParameters, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveEventTypeMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  July 10, 2017
        Function:     SaveRebatePlanMaint
        Purpose:      SaveRebatePlanMaint
        Inputs:       LookupParameterDTO,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveRebatePlanMaint(LookupParameters lookupParameterModel, string userId)
        {
            Logger.Info("Invoking SaveRebatePlanMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var _LookupParameters = Mapper.Map<RebatePlanDTO>(lookupParameterModel);
                    var result = await globalVariableOpDAO.WebRebatePlanMaint(_LookupParameters, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveRebatePlanMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy
        Created on:  July 03, 2017
        Function:     SaveProdRefMaint
        Purpose:      SaveProdRefMaint
        Inputs:       ProdRefDTO,userId
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveProdRefMaint(LookupParameters lookupParameterModel, string userId)
        {
            Logger.Info("Invoking SaveProdRefMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var globalVariableOpDAO = scope.Resolve<IGlobalVariableOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var prodRef = Mapper.Map<ProdRefDTO>(lookupParameterModel);
                    var result = await globalVariableOpDAO.WebProdRefMaint(prodRef, userId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveProdRefMaint: detail:{0}", ex.Message);
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

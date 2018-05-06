using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.EventConfiguration;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
   public interface IEventConfigService
    {
       Task<EventConfigResponse> GetNotifyEventConfDetails(string userId);
       Task<SaveGeneralInfoResponse> DeleteWebNtfEvtConf(string scheduleId);
       Task<SaveGeneralInfoResponse> DeleteWebNtfEvtConfRcpt(string schRcptId);
       Task<EventConfigResponse> GetNtfyEventConf(string planId);
       Task<SaveAcctSignUpResponse> SaveEventAcctConfMaint(LookupParameters lookUpParameterModel);
       Task<SaveAcctSignUpResponse> SaveNtfyEvtConfMaint(LookupParameters lookUpParameterModel);
       Task<EventConfigResponse> GetWebNtfyEventRcpts(string planId);
       Task<EventConfigResponse> GetEventAcctConfListSelect(string refTo, string refKey);
       Task<EventConfigResponse> GetEventAcctConfSelect(string eventTypeId, string eventScheduleId, string acctNo);
       Task<EventConfigResponse> GetEventAcctRcpts(string eventScheduleId);
       Task<string> GetRefCmpyName(string selectedRefTo, string refKey);
    }
    public class EventConfigService : IEventConfigService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public EventConfigService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region EventConfigService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<EventConfigDAO>().As<IEventConfigDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region EventConfigService
        /*************************************
        Created by:   Tuan Tran
        Created on:   April 18, 2017
        Function:     GetNotifyEventConfDetails
        Purpose:      GetNotifyEventConfDetails
        Inputs:       userId
        Returns:      EventConfigResponse
        *************************************/
        public async Task<EventConfigResponse> GetNotifyEventConfDetails(string userId)
        {
            Logger.Info("Invoking GetNotifyEventConfDetails fuction use EF to call SP");
            var response = new EventConfigResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var results = await eventConfigDAO.GetNtfyEvtConfListSelect(userId);
                    if(results.Count() > 0)
                    {
                        response.lookupParameters = Mapper.Map<List<WebNtfyEvtConfDetailDTO>,List<LookupParameters>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetNotifyEventConfDetails: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   April 18, 2017
        Function:     GetNtfyEventConf
        Purpose:      GetNtfyEventConf
        Inputs:       planId
        Returns:      EventConfigResponse
        *************************************/
        public async Task<EventConfigResponse> GetNtfyEventConf(string planId)
        {
            Logger.Info("Invoking GetNtfyEventConf fuction use EF to call SP");
            var response = new EventConfigResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var results = await eventConfigDAO.WebNtfyEventConfSelect(planId);
                    foreach (var item in results)
                    {
                        item.EventScheduleId = Convert.ToInt64(planId);
                    }
                    if(results.Count() > 0)
                    {
                        response.lookupParameters = Mapper.Map<List<NtfyEventConfDTO>,List<LookupParameters>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetNtfyEventConf: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   April 18, 2017
        Function:     DeleteWebNtfEvtConf
        Purpose:      DeleteWebNtfEvtConf
        Inputs:       scheduleId
        Returns:      EventConfigResponse
       *************************************/
        public async Task<SaveGeneralInfoResponse> DeleteWebNtfEvtConf(string scheduleId)
        {
            Logger.Info("Invoking DeleteWebNtfEvtConf function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await eventConfigDAO.WebNtfEvtConfDelete(scheduleId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in DeleteWebNtfEvtConf: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   April 18, 2017
        Function:     DeleteWebNtfEvtConfRcpt
        Purpose:      DeleteWebNtfEvtConfRcpt
        Inputs:       schRcptId
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveGeneralInfoResponse> DeleteWebNtfEvtConfRcpt(string schRcptId)
        {
            Logger.Info("Invoking DeleteWebNtfEvtConfRcpt function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await eventConfigDAO.WebNtfEvtConfRcptDelete(schRcptId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in DeleteWebNtfEvtConfRcpt: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   April 18, 2017
        Function:     SaveEventAcctConfMaint
        Purpose:      SaveEventAcctConfMaint
        Inputs:       NtfyEventConfDTO
        Returns:      SaveAcctSignUpResponse
       *************************************/
        public async Task<SaveAcctSignUpResponse> SaveEventAcctConfMaint(LookupParameters lookUpParameterModel)
        {
            Logger.Info("Invoking SaveEventAcctConfMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var lookUpParameters = Mapper.Map<LookupParameters, NtfyEventConfDTO>(lookUpParameterModel);
                    var result = await eventConfigDAO.SaveEventAcctConfMaint(lookUpParameters);
                    var message = await controlDAO.GetMessageCode(result.Flag);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                    response.returnValue.NewEventSchId = result.paraOut.NewEventSchId;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveEventAcctConfMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   April 18, 2017
        Function:     SaveNtfyEvtConfMaint
        Purpose:      SaveNtfyEvtConfMaint
        Inputs:       NtfyEventConfDTO
        Returns:      SaveAcctSignUpResponse
        *************************************/
        public async Task<SaveAcctSignUpResponse> SaveNtfyEvtConfMaint(LookupParameters lookUpParameterModel)
        {
            Logger.Info("Invoking SaveNtfyEvtConfMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var lookUpParameters = Mapper.Map<LookupParameters, NtfyEventConfDTO>(lookUpParameterModel);
                    var result = await eventConfigDAO.SaveWebNtfyEvtConfMaint(lookUpParameters);
                    var message = await controlDAO.GetMessageCode(result.Flag);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                    response.returnValue.NewEventSchId = result.paraOut.NewEventSchId;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveNtfyEvtConfMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   April 18, 2017
        Function:     GetWebNtfyEventRcpts
        Purpose:      GetWebNtfyEventRcpts
        Inputs:       planId
        Returns:      EventConfigResponse
         *************************************/
        public async Task<EventConfigResponse> GetWebNtfyEventRcpts(string planId)
        {
            Logger.Info("Invoking GetWebNtfyEventRcpts fuction use EF to call SP");
            var response = new EventConfigResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var results = await eventConfigDAO.WebNtfyEventRcptListSelect(planId);
                    if(results.Count() > 0)
                    response.eventRcpts = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetWebNtfyEventRcpts: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   April 19, 2017
        Function:     GetEventAcctConfListSelect
        Purpose:      GetEventAcctConfListSelect
        Inputs:       refTo,refKey
        Returns:      EventConfigResponse
        *************************************/
        public async Task<EventConfigResponse> GetEventAcctConfListSelect(string refTo, string refKey)
        {
            Logger.Info("Invoking GetEventAcctConfListSelect fuction use EF to call SP");
            var response = new EventConfigResponse()
            {
                Status = ResponseStatus.Failure
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var results = await eventConfigDAO.WebEventAcctConfListSelect(refTo,refKey);
                    if(results.Count() > 0)
                        response.lookupParameters = Mapper.Map<List<NtfyEventConfDTO>,List<LookupParameters>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEventAcctConfListSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   june 26, 2017
        Function:     GetEventAcctConfSelect
        Purpose:      GetEventAcctConfSelect
        Inputs:       eventTypeId,eventScheduleId,acctNo
        Returns:      EventConfigResponse
        *************************************/
        public async Task<EventConfigResponse> GetEventAcctConfSelect(string eventTypeId, string eventScheduleId, string acctNo)
        {
            Logger.Info("Invoking GetEventAcctConfSelect fuction use EF to call SP");
            var response = new EventConfigResponse()
            {
                Status = ResponseStatus.Failure
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var results = await eventConfigDAO.WebEventAcctConfSelect(eventTypeId, eventScheduleId, acctNo);
                    if(results.Count() > 0)
                        response.lookupParameters = Mapper.Map<List<NtfyEventConfDTO>,List<LookupParameters>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEventAcctConfSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   june 26, 2017
        Function:     GetEventAcctRcpts
        Purpose:      GetEventAcctRcpts
        Inputs:       eventScheduleId
        Returns:      EventConfigResponse
        *************************************/
        public async Task<EventConfigResponse> GetEventAcctRcpts(string eventScheduleId)
        {
            Logger.Info("Invoking GetEventAcctRcpts fuction use EF to call SP");
            var response = new EventConfigResponse()
            {
                Status = ResponseStatus.Failure
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var results = await eventConfigDAO.WebEventAcctRcptListSelect(eventScheduleId);
                    if(results.Count() > 0)
                     response.eventRcpts = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEventAcctRcpts: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   April 20, 2017
        Function:     GetRefCmpyName
        Purpose:      GetRefCmpyName
        Inputs:       refTo,refKey
        Returns:      string
        *************************************/
        public async Task<string> GetRefCmpyName(string selectedRefTo, string refKey)
        {
            Logger.Info("Invoking GetRefCmpyName fuction use EF to call SP");
            var response = new EventConfigResponse()
            {
                Status = ResponseStatus.Failure
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var eventConfigDAO = scope.Resolve<IEventConfigDAO>();
                    var cpnName = await eventConfigDAO.WebGetRefCmpyName(selectedRefTo,refKey);
                    response.CmpyName = cpnName;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRefCmpyName: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.CmpyName;
        }
        #endregion
    }
}

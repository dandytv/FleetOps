using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Extensions;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.EventConfiguration;
using CardTrend.Domain.Dto.NotifSearch;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
    public interface INotifSearchService
    {
        Task<NotificationSearchResponse> GetNtfyEventSearch(string evtInd, string eventType, string refTo, string refKey, string startDate, string endDate);
        Task<NotificationSearchResponse> GetEventSelect(string eventId);
        Task<NotificationSearchResponse> GetNtfyEventRcptListSelect(string scheduleId);
    }
    public class NotifSearchService : INotifSearchService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public NotifSearchService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region NotifSearchService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<NotifSearchDAO>().As<INotifSearchDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region NotifSearch Service
        /*************************************
        Created by:   dandy boy
        Created on:   April 21, 2017
        Function:     GetNtfyEventSearch
        Purpose:      GetNtfyEventSearch
        Inputs:       evtInd,eventType,refTo,refKey,startDate,endDate
        Returns:      NotificationSearchResponse
        *************************************/
        public async Task<NotificationSearchResponse> GetNtfyEventSearch(string evtInd, string eventType, string refTo, string refKey, string startDate, string endDate)
        {
            Logger.Info("Invoking GetNtfyEventSearch function");
            var response = new NotificationSearchResponse()
            {
                Status = ResponseStatus.Failure
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var notificationSearchDAO = scope.Resolve<INotifSearchDAO>();
                    string frDate = DateTime.ParseExact(startDate, "dd/MM/yyyy",null).ToShortDateString();
                    string toDate = DateTime.ParseExact(endDate, "dd/MM/yyyy",null).ToShortDateString();
                    var results = await notificationSearchDAO.WebNtfyEventSearch(evtInd, eventType, refTo, refKey, frDate, toDate);
                    if(results.Count() > 0)
                        response.lookupParameters = Mapper.Map<IList<NtfyEventSearchDTO>,IList<LookupParameters>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetNtfyEventSearch: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   April 21, 2017
        Function:     GetEventSelect
        Purpose:      GetEventSelect
        Inputs:       eventId
        Returns:      NotificationSearchResponse
        *************************************/
        public async Task<NotificationSearchResponse> GetEventSelect(string eventId)
        {
            Logger.Info("Invoking GetEventSelect function");
            var response = new NotificationSearchResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var notifSearchDAO = scope.Resolve<INotifSearchDAO>();
                    var results = await notifSearchDAO.WebEventSelect(eventId);
                    if(results.Count() > 0)
                    {
                        response.lookupParameters = Mapper.Map<IList<NtfyEventConfSearchDTO>,IList<LookupParameters>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEventSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   April 21, 2017
        Function:     GetNtfyEventRcptListSelect
        Purpose:      GetNtfyEventRcptListSelect
        Inputs:       scheduleId
        Returns:      NotificationSearchResponse
       *************************************/
        public async Task<NotificationSearchResponse> GetNtfyEventRcptListSelect(string scheduleId)
        {
            Logger.Info("Invoking GetNtfyEventRcptListSelect function");
            var response = new NotificationSearchResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var notifSearchDAO = scope.Resolve<INotifSearchDAO>();
                    var results = await notifSearchDAO.WebNtfyEventRcptListSelect(scheduleId);
                    if(results.Count() > 0)
                        response.eventRcpts = Mapper.Map<List<EventRcptDTO>,List<EventRcptList>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetNtfyEventRcptListSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        #endregion
    }
}

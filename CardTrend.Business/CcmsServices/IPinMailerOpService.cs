using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.PinMailer;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
   public interface IPinMailerOpService
    {
       Task<PinMailerBatchResponse> GetPinMailerBatchList();
       Task<PinMailerBatchResponse> GetPinMailerBatchView(long batchID, int status);
       Task<SaveGeneralInfoResponse> SavePinMailerPrint(int batchId, List<long> cardList);
    }
   public class PinMailerOpService:IPinMailerOpService
   {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public PinMailerOpService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region PinMailerOpService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<PinMailerOpDAO>().As<IPinMailerOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
       #region PinMailerOpService Service
        /*************************************
            Created by:   Tuan Tran
            Created on:   March 14, 2017
            Function:     GetPinMailerBatchList
            Purpose:      GetPinMailerBatchList
            Inputs:       
            Returns:      PinMailerBatchResponse
        *************************************/
        public async Task<PinMailerBatchResponse> GetPinMailerBatchList()
        {
            Logger.Info("Invoking GetPinMailerBatchList function");
            var response = new PinMailerBatchResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pinMailer = scope.Resolve<IPinMailerOpDAO>();
                    var result = await pinMailer.GetPinMailerBatch();
                    if(result.Count() > 0)
                        response.pinMailerBatchs = Mapper.Map<IList<PinMailerBatchDTO>,IList<PinMailerBatchList>>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPinMailerBatchList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   Tuan Tran
           Created on:   Mar 14, 2017
           Function:     GetPinMailerBatchView
           Purpose:      GetPinMailerBatchView
           Inputs:       batchID,status
           Returns:      PinMailerBatchResponse
       *************************************/
        public async Task<PinMailerBatchResponse> GetPinMailerBatchView(long batchID, int status)
        {
            Logger.Info("Invoking GetPinMailerBatchView function");
            var response = new PinMailerBatchResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pinMailer = scope.Resolve<IPinMailerOpDAO>();
                    var results = await pinMailer.GetPinMailerBatchView(batchID,status);
                    if(results.Count() > 0 )
                        response.pinMailerBatchViews = Mapper.Map<IList<PinMailerBatchViewDTO>,IList<PinMailerBatchView>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPinMailerBatchView: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Mar 14, 2017
         Function:     SavePinMailerPrint
         Purpose:      SavePinMailerPrint
         Inputs:       batchId,cardList
         Returns:      SaveGeneralInfoResponse
         *************************************/
        public async Task<SaveGeneralInfoResponse> SavePinMailerPrint(int batchId, List<long> cardList)
        {
            Logger.Info("Invoking SavePinMailerPrint function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pinMailer = scope.Resolve<IPinMailerOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await pinMailer.SavePinMailerPrint(batchId, cardList);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SavePinMailerPrint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.desp = msg;
            }
            return response;

        }
       #endregion
   }
}

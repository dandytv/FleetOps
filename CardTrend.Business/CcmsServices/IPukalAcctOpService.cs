using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.PukaAcct;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
    public interface IPukalAcctOpService
    {
        Task<PukalPaymentResponse> GetPukalAccounts(string refcd, string acctOfficeCd, Int64 cycStmtId);
        Task<PukalPaymentResponse> GetPukalSeduts(string refcd, string acctOfficeCd, string status);
        Task<PukalPaymentResponse> GetPukalAcctBatches(long batchID, string refCd, string acctOfficeCd, int cycStmtId);
    }
    public class PukalAcctOpService : IPukalAcctOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public PukalAcctOpService()
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
            afBuilder.RegisterType<PukalAcctOpDAO>().As<IPukalAcctOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region PukalAcctOpService Service
        /*************************************
           Created by:   Tuan Tran
           Created on:   March 20, 2017
           Function:     GetPukalAccounts
           Purpose:      GetPukalAccounts
           Inputs:       refCode,acctofficeCd,cycStmtId
           Returns:      PukalPaymentResponse
        *************************************/
        public async Task<PukalPaymentResponse> GetPukalAccounts(string refcd,string acctOfficeCd,Int64 cycStmtId )
        {
            Logger.Info("Invoking GetPukalAccounts function");
            var response = new PukalPaymentResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctOpDAO = scope.Resolve<IPukalAcctOpDAO>();
                    var pukals = await pukalAcctOpDAO.GetPukalAccountList(refcd,acctOfficeCd,cycStmtId);
                    if(pukals.Count() > 0)
                        response.pukalPayments = Mapper.Map<IList<PukalPaymentDTO>,IList<PukalAcctBatchList>>(pukals);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPukalAccounts: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   Tuan Tran
           Created on:   March 20, 2017
           Function:     GetPukalSeduts
           Purpose:      GetPukalSeduts
           Inputs:       refCode,acctofficeCd,status
           Returns:      PukalPaymentResponse
        *************************************/
        public async Task<PukalPaymentResponse> GetPukalSeduts(string refcd, string acctOfficeCd, string status)
        {
            Logger.Info("Invoking GetPukalSeduts function");
            var response = new PukalPaymentResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctOpDAO = scope.Resolve<IPukalAcctOpDAO>();
                    var pukalSeduts = await pukalAcctOpDAO.GetPukalSedutList(refcd, acctOfficeCd,status);
                    if(pukalSeduts.Count() > 0)
                        response.pukalSeduts = Mapper.Map<IList<PukalSedutDTO>,IList<WebPukalSedutList>>(pukalSeduts);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPukalSeduts: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   March 20, 2017
        Function:     GetPukalAcctBatches
        Purpose:      GetPukalAcctBatches
        Inputs:       batchID,refCd,acctOfficeCd,cycStmtId
        Returns:      PukalPaymentResponse
        *************************************/
        public async Task<PukalPaymentResponse> GetPukalAcctBatches(long batchID, string refCd, string acctOfficeCd, int cycStmtId)
        {
            Logger.Info("Invoking GetPukalAcctBatches function");
            var response = new PukalPaymentResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var pukalAcctOpDAO = scope.Resolve<IPukalAcctOpDAO>();
                    var pukalAcctbatchViews = await pukalAcctOpDAO.GetPukalAcctBatch(batchID, refCd, acctOfficeCd, cycStmtId);
                    if (pukalAcctbatchViews.Count() > 0)
                        response.pukalAcctBatches = Mapper.Map<IList<PukalAcctBatchDTO>,IList<PukalAcctBatchView>>(pukalAcctbatchViews);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPukalAcctBatches: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        #endregion
    }
}

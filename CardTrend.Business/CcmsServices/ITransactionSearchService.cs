using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Extensions;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.TransactionSearch;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
   public interface ITransactionSearchService
    {
       Task<TransactionSearchResponse> GetAccountTransactionSearch(Int64? accountNo, Int64? cardNo, string transacionCategory, int txtCd, string fromDate, string toDate, string statementDate);
       Task<TransactionSearchResponse> GetMerchTransactionSearch(string bussinessLocation, string merchAcctNo, string txnCd, string fromtxnDate, string toTxnDate, string txnCat);
       Task<TransactionSearchResponse> GetObjectDetail(string prefix, string value);
    }
    public class TransactionSearchService : ITransactionSearchService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public TransactionSearchService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region TransactionSearchService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<TransactionSearchDAO>().As<ITransactionSearchDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region Transaction Service
        /*************************************
           Created by:   dandy boy
           Created on:   March 24, 2017
           Function:     GetAccountTransactionSearch
           Purpose:      GetAccountTransactionSearch
           Inputs:       
           Returns:      TransactionSearchResponse
        *************************************/
        public async Task<TransactionSearchResponse> GetAccountTransactionSearch(Int64? accountNo, Int64? cardNo, string transacionCategory, int txtCd,string fromDate,string toDate, string statementDate)
        {
            Logger.Info("Invoking GetAccountTransactionSearch function");
            var response = new TransactionSearchResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using(var scope = Container.BeginLifetimeScope())
                {
                    var transacionSearchDAO = scope.Resolve<ITransactionSearchDAO>();
                    string frDate = (string)NumberExtensions.ConvertDatetimeDB(fromDate);
                    string tDate = (string)NumberExtensions.ConvertDatetimeDB(toDate);
                    var results = await transacionSearchDAO.GetAccountTxnSearch(accountNo, cardNo, transacionCategory, txtCd, frDate, tDate, statementDate);
                    if(results.Count() > 0)
                        response.transactionSearches = Mapper.Map<IList<TransactionSearchDTO>,IList<AcctPostedTxnSearch>>(results);
                }
                response.Status = ResponseStatus.Success;
            }catch(Exception ex)
            {
                string msg = string.Format("Error in GetAccountTransactionSearch: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
          Created by:   dandy boy
          Created on:   March 24, 2017
          Function:     GetMerchTransactionSearch
          Purpose:      GetMerchTransactionSearch
          Inputs:       
          Returns:      TransactionSearchResponse
       *************************************/
        public async Task<TransactionSearchResponse> GetMerchTransactionSearch(string bussinessLocation, string merchAcctNo, string txnCd, string fromtxnDate, string toTxnDate, string txnCat)
        {
            Logger.Info("Invoking GetAccountTransactionSearch function");
            var response = new TransactionSearchResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var transacionSearchDAO = scope.Resolve<ITransactionSearchDAO>();
                    string frTransactionDate = (string)NumberExtensions.ConvertDatetimeDB(fromtxnDate);
                    string toTransactionDate = (string)NumberExtensions.ConvertDatetimeDB(toTxnDate);
                    var results = await transacionSearchDAO.GetMerchTxnSearch(bussinessLocation, merchAcctNo, txnCd, frTransactionDate, toTransactionDate, txnCat);
                    if(results.Count() > 0)
                        response.merchPostedTxnSearches = Mapper.Map<IList<TransactionSearchDTO>, IList<MerchPostedTxnSearch>>(results);
                }
                response.Status = ResponseStatus.Success;

            }catch(Exception ex)
            {
                string msg = string.Format("Error in GetMerchTransactionSearch: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
      /*************************************
          Created by:   dandy boy
          Created on:   March 28, 2017
          Function:     GetObjectDetail
          Purpose:      GetObjectDetail
          Inputs:       prefix,value
          Returns:      TransactionSearchResponse
      *************************************/
        public async Task<TransactionSearchResponse> GetObjectDetail(string prefix, string value)
        {
            Logger.Info("Invoking GetObjectDetail function");
            var response = new TransactionSearchResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    ObjectDetailDTO detail = new ObjectDetailDTO();
                    var transacionSearchDAO = scope.Resolve<ITransactionSearchDAO>();
                    var result = await transacionSearchDAO.GetObjectDetail(prefix, value);
                    detail.Preifix = result.Flag;
                    if (result.Flag == "ACCT")
                        detail.AcctNo = result.AcctNo;
                    else if (result.Flag == "CARD")
                    {
                        detail.AcctNo = result.AcctNo;
                        detail.CardNo = result.CardNo;
                    }
                    else if (result.Flag == "MERCH")
                    {
                        detail.MerchAcctNo = Convert.ToString(result.AcctNo);
                    }
                    else if (result.Flag == "BUSN")
                    {
                        detail.MerchAcctNo = Convert.ToString(result.AcctNo);
                        detail.BusnLocation = result.BusnLocation;
                    }
                    if(detail != null)
                        response.objectDetail = Mapper.Map<ObjectDetailDTO,ObjectDetail>(detail);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetObjectDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        #endregion
    }
}

using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.MerchantMultiAdjustment;
using CardTrend.Domain.Dto.MultiplePayment;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
    public interface IMerchMultitxnAdjustmentService
    {
        Task<MerchMultitxnAdjustmentResponse> GetMerchantMultiTxnAdjustmentList();
        Task<MerchMultitxnAdjustmentResponse> GetMerchantMultiTxnAdjustmentDetail(string invoiceNo, string batchId);
        Task<SaveAcctSignUpResponse> SaveMerchantMultiTxnAdjustmentMaint(TxnAdjustmentDTO adjustmentDetail, string userId);
        Task<MerchMultitxnAdjustmentResponse> GetGLCodes(string adjTxnCode);
    }
    public class MerchMultitxnAdjustmentService : IMerchMultitxnAdjustmentService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public MerchMultitxnAdjustmentService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region MerchMultitxnAdjustmentService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<MerchMultitxnAdjustmentDAO>().As<IMerchMultitxnAdjustmentDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region MerchMultitxnAdjustmentService Service
        /*************************************
           Created by:   dandy boy
           Created on:   March 21, 2017
           Function:     GetMerchantMultiTxnAdjustmentList
           Purpose:      GetMerchantMultiTxnAdjustmentList
           Inputs:       
           Returns:      MerchMultitxnAdjustmentResponse
        *************************************/
        public async Task<MerchMultitxnAdjustmentResponse> GetMerchantMultiTxnAdjustmentList()
        {
            Logger.Info("Invoking GetMerchantMultiTxnAdjustmentList function");
            var response = new MerchMultitxnAdjustmentResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var merchMultitxnAdjustmentDAO = scope.Resolve<IMerchMultitxnAdjustmentDAO>();
                    var results = await merchMultitxnAdjustmentDAO.MerchantMultiTxnAdjustmentListSelect();
                    if(results.Count() > 0)
                        response.txtAdjustments = Mapper.Map<List<MerchantMultiTxnAdjustmentDTO>,List<TxnAdjustment>>(results);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMerchantMultiTxnAdjustmentList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   dandy boy
           Created on:   March 21, 2017
           Function:     GetMerchantMultiTxnAdjustmentDetail
           Purpose:      GetMerchantMultiTxnAdjustmentDetail
           Inputs:       invoiceNo,batchId
           Returns:      MerchMultitxnAdjustmentResponse
       *************************************/
        public async Task<MerchMultitxnAdjustmentResponse> GetMerchantMultiTxnAdjustmentDetail(string invoiceNo,string batchId)
        {
            Logger.Info("Invoking GetMerchantMultiTxnAdjustmentList function");
            var response = new MerchMultitxnAdjustmentResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var merchMultitxnAdjustmentDAO = scope.Resolve<IMerchMultitxnAdjustmentDAO>();
                    var result = await merchMultitxnAdjustmentDAO.MerchantMultiTxnAdjustmentSelect(invoiceNo,batchId);
                    var txnList = new List<MultipleTxnRecordDTO>();
                    TxnAdjustmentDetailDTO txnAdjustment = new TxnAdjustmentDetailDTO();
                    if (result.Count() > 0)
                    {
                        foreach (var item in result)
                        {
                            MultipleTxnRecordDTO multipleTxtRecord = new MultipleTxnRecordDTO();
                            multipleTxtRecord.TxnAmt = Convert.ToString(decimal.Round(item.Amt, 2, MidpointRounding.AwayFromZero));
                            multipleTxtRecord.MerchantAcctNo = item.MerchantNo;
                            multipleTxtRecord.AcctName = item.MerchantName;
                            multipleTxtRecord.TxnId = item.Ids.ToString();
                            multipleTxtRecord.Descp = item.Description;
                            txnList.Add(multipleTxtRecord);
                            //
                            txnAdjustment.TxnCd = item.TxnCd;
                            txnAdjustment.TxnType = item.TxnType;
                            txnAdjustment.TxnDate = item.TxnDate;
                            txnAdjustment.ChequeAmt = item.ChequeAmt;
                            txnAdjustment.Owner = item.Owner;
                            txnAdjustment.Sts = item.Sts;
                            txnAdjustment.GroupingBatchId = item.GroupingBatchId;
                            txnAdjustment.BatchId = item.BatchId;
                            txnAdjustment.InvoiceNo = item.InvoiceNo;
                            txnAdjustment.ApprovalStatus = item.ApprovalStatus;
                            txnAdjustment.ApprovalDesc = item.ApprovalDesc;
                        }
                        txnAdjustment.multipleTxnRecord = txnList;
                        response.txnAdjustmentDetail = Mapper.Map<TxnAdjustmentDetailDTO, TxnAdjustment>(txnAdjustment);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMerchantMultiTxnAdjustmentList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   dandy boy
           Created on:   March 22, 2017
           Function:     SaveMerchantMultiTxnAdjustmentMaint
           Purpose:      SaveMerchantMultiTxnAdjustmentMaint
           Inputs:       TxnAdjustmentDTO,invoiceNo,userId
           Returns:      SaveAcctSignUpResponse
       *************************************/
        public async Task<SaveAcctSignUpResponse> SaveMerchantMultiTxnAdjustmentMaint(TxnAdjustmentDTO adjustmentDetail, string userId)
        {
            Logger.Info("Invoking SaveMerchantMultiTxnAdjustmentMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var merchMultitxnAdjustmentDAO = scope.Resolve<IMerchMultitxnAdjustmentDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await merchMultitxnAdjustmentDAO.SaveMerchantMultiTxnAdjustmentMaint(adjustmentDetail,userId);
                    var message = await controlDAO.GetMessageCode(result.Flag);
                    response.desp = message.Descp;
                    response.returnValue.BatchId = result.paraOut.BatchId;
                    response.returnValue.RetCd = result.paraOut.RetCd;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveMerchantMultiTxnAdjustmentMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.desp = ex.Message;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   dandy boy
           Created on:   March 23, 2017
           Function:     GetGLCodes
           Purpose:      GetGLCodes
           Inputs:       TxnAdjustmentDTO,invoiceNo,userId
           Returns:      SaveAcctSignUpResponse
       *************************************/

        public async Task<MerchMultitxnAdjustmentResponse> GetGLCodes(string adjTxnCode)
        {
            Logger.Info("Invoking GetGLCodes function");
            var response = new MerchMultitxnAdjustmentResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var merchMultitxnAdjustmentDAO = scope.Resolve<IMerchMultitxnAdjustmentDAO>();
                    var results = await merchMultitxnAdjustmentDAO.GetGLCode(adjTxnCode);
                    if(results.Count() > 0)
                        response.multiPaymentGLCodes = Mapper.Map<IList<MultiPaymentGLCodeDTO>, IList<MultiPayment>>(results);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetGLCodes: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }

            return response;
        }
        #endregion
    }
}

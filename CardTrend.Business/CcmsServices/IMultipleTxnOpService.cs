using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.MultipleAdjustment;
using CardTrend.Domain.Dto.MultiplePayment;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
   public interface IMultipleTxnOpService
    {
       Task<MultipleTxnOpResponse> GetMultiTxnAdjustmentListSelect();
       Task<MultipleTxnOpResponse> GetMultiTxnAdjustmentSelect(string batchId, string chequeNo);
       Task<SaveAcctSignUpResponse> SaveftMultipleAdjMaint(TxnAdjustment txnAdjustmentModel);
       Task<MultiPaymentResponse> GetGLCode(string txnCode, string settlement, string acctNo);
    }
    public class MultipleTxnOpService: IMultipleTxnOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;

        public MultipleTxnOpService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region MultiPaymentOpService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<MultipleTxnOpDAO>().As<IMultipleTxnOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region MultipleTxnOpService
        /*************************************
           Created by:   Tuan Tran
           Created on:   March 8, 2017
           Function:     GetMultiTxnAdjustmentListSelect
           Purpose:      GetMultiTxnAdjustmentListSelect
           Inputs:       
           Returns:      MultipleTxnOpResponse
        *************************************/
        public async Task<MultipleTxnOpResponse> GetMultiTxnAdjustmentListSelect()
        {
            Logger.Info("Invoking GetMultiTxnAdjustmentListSelect function");
            var response = new MultipleTxnOpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var multipleTxnOpDAO = scope.Resolve<IMultipleTxnOpDAO>();
                    var results = await multipleTxnOpDAO.WebMultiTxnAdjustmentListSelect();
                    if(results.Count() > 0)
                    {
                        response.txtAdjustments = Mapper.Map<List<TxnMultipleAdjustmentDTO>,List<TxnAdjustment>>(results);
                    }
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMultiTxnAdjustmentListSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   Tuan Tran
           Created on:   Mar 8, 2017
           Function:     GetMultiTxnAdjustmentSelect
           Purpose:      GetMultiTxnAdjustmentSelect
           Inputs:       batchId,chequeNo
           Returns:      MultipleTxnOpResponse
        *************************************/
        public async Task<MultipleTxnOpResponse> GetMultiTxnAdjustmentSelect(string batchId, string chequeNo)
        {
            Logger.Info("Invoking GetMultiTxnAdjustmentSelect function");
            var response = new MultipleTxnOpResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var multipleTxnOpDAO = scope.Resolve<IMultipleTxnOpDAO>();
                    var result = await multipleTxnOpDAO.WebMultiTxnAdjustmentSelect(batchId, chequeNo);
                    var txnList = new List<MultipleTxnRecordDTO>();
                    TxnAdjustmentDTO txnAdjustment = new TxnAdjustmentDTO();
                    if(result.Count() > 0)
                    {
                        foreach(var item in result)
                        {
                            MultipleTxnRecordDTO multipleTxtRecord = new MultipleTxnRecordDTO();
                            multipleTxtRecord.AcctNo = Convert.ToInt32(item.AccountNo);
                            multipleTxtRecord.CardNo = item.CardNo;
                            multipleTxtRecord.TxnAmt = Convert.ToString(decimal.Round(item.TxnAmount, 2, MidpointRounding.AwayFromZero));
                            multipleTxtRecord.TxnDescp = item.TxnDescription;
                            multipleTxtRecord.AppvCd = item.AppvCd;
                            multipleTxtRecord.DeftBusnLocation = item.Dealer;
                            multipleTxtRecord.DeftTermId = item.TermId;
                            multipleTxtRecord.SelectedOwner = item.Owner;
                            multipleTxtRecord.SelectedStsDescp = item.Status;
                            multipleTxtRecord.SelectedSts = item.Sts;
                            multipleTxtRecord.TxnId = Convert.ToString(item.TxnId);
                            multipleTxtRecord.AcctName = item.AccountName;
                            txnList.Add(multipleTxtRecord);
                            //
                            txnAdjustment.TxnDate = item.TxnDate;
                            txnAdjustment.ChequeAmt = item.ChequeAmt;
                            txnAdjustment.ChequeNo = item.ChequeNo;
                            txnAdjustment.BillingAmount = item.BillingAmount;
                            txnAdjustment.UserId = item.UserId;
                            txnAdjustment.WUId = item.WUId;
                            txnAdjustment.CreationDate = item.CreationDate;
                            txnAdjustment.BatchId = item.BatchId;
                            txnAdjustment.Owner = item.Owner;
                            txnAdjustment.Sts = item.Sts;
                            txnAdjustment.TxnCd = item.TxnCd;
                            txnAdjustment.TxnType = item.TxnType;
                            txnAdjustment.PymtType = item.PymtType;

                        }

                    }
                    txnAdjustment.multipleTxnRecord = txnList; 
                    response.txnAdjustment = Mapper.Map<TxnAdjustment>(txnAdjustment);

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMultiTxnAdjustmentSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
           Created by:   Tuan Tran
           Created on:   Mar 8, 2017
           Function:     SaveftMultipleAdjMaint
           Purpose:      SaveftMultipleAdjMaint
           Inputs:       TxnAdjustmentDTO
           Returns:      SaveAcctSignUpResponse
         *************************************/
        public async Task<SaveAcctSignUpResponse> SaveftMultipleAdjMaint(TxnAdjustment txnAdjustmentModel)
        {
            Logger.Info("Invoking SaveftMultipleAdjMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var multipleTxnOpDAO = scope.Resolve<IMultipleTxnOpDAO>();
                    var txnAdjustment = Mapper.Map<TxnAdjustment, TxnAdjustmentDTO>(txnAdjustmentModel);
                    var result = await multipleTxnOpDAO.ftMultipleAdjMaint(txnAdjustment);
                    response.returnValue = result.paraOut;
                    response.desp = result.Descp;
                    response.flag = result.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveftMultipleAdjMaint: detail:{0}", ex.Message);
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
           Created on:   May 2, 2017
           Function:     GetGLCode
           Purpose:      GetGLCode
           Inputs:       txnCode,settlement,acctNo
           Returns:      MultiPaymentResponse
          *************************************/
        public async Task<MultiPaymentResponse> GetGLCode(string txnCode, string settlement, string acctNo)
        {
            Logger.Info("Invoking GetGLCode function");
            var response = new MultiPaymentResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var multiPaymentOpDAO = scope.Resolve<IMultiPaymentOpDAO>();
                    var objCodes = await multiPaymentOpDAO.WebGetGLCode(txnCode, settlement, acctNo);
                    if (objCodes.Count() > 0)
                        response.multiPayments = Mapper.Map<List<GLCodeDTO>,List<MultiPayment>>(objCodes);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetGLCode: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        #endregion
    }
}

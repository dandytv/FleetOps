using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.MultiplePayment;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Business.CcmsServices
{
   public interface IMultiPaymentOpService
    {
       Task<MultiPaymentResponse> WebMultiPaymentListSelect();
       Task<MultiPaymentResponse> WebMultiPaymentSelect(string batchId, MultiPayment multiPayment);
       Task<MultiPaymentResponse> WebGetGLCodeByTxnCd(string txnCd, string settleVal, string acctNo);
       Task<IEnumerable<SelectListItem>> GetPymtTxnCd(string GlSettlementCd, string TxnCat);
       Task<SaveAcctSignUpResponse> SaveWebMultiPaymentMaint(TxnAdjustment txnAdjustment);
       Task<MultiPaymentResponse> GetGLCode(string txnCode, string settlement, string acctNo);
    }
    public class MultiPaymentOpService: IMultiPaymentOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;

        public MultiPaymentOpService()
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
            afBuilder.RegisterType<MultiPaymentOpDAO>().As<IMultiPaymentOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region MultiPaymentOpService
        /*************************************
         Created by:   Tuan Tran
         Created on:   March 5, 2017
         Function:     WebMultiPaymentListSelect
         Purpose:      WebMultiPaymentListSelect
         Inputs:       
         Returns:      MultiPaymentResponse
        *************************************/
        public async Task<MultiPaymentResponse> WebMultiPaymentListSelect()
        {
            Logger.Info("Invoking WebMultiPaymentListSelect function");
            var response = new MultiPaymentResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var multiPaymentOpDAO = scope.Resolve<IMultiPaymentOpDAO>();
                    var results = await multiPaymentOpDAO.WebMultiPaymentListSelect();
                    if(results.Count() > 0)
                        response.multiPayments = Mapper.Map<IList<MultiPaymentDTO>,IList<MultiPayment>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebMultiPaymentListSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Mar 5, 2017
         Function:     WebMultiPaymentSelect
         Purpose:      WebMultiPaymentSelect
         Inputs:       batchId,MultiPaymentDTO
         Returns:      MultiPaymentResponse
        *************************************/
        public async Task<MultiPaymentResponse> WebMultiPaymentSelect(string batchId, MultiPayment multiPayment)
        {
            Logger.Info("Invoking WebMultiPaymentSelect function");
            var response = new MultiPaymentResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var multiPaymentOpDAO = scope.Resolve<IMultiPaymentOpDAO>();
                    var multi = Mapper.Map<MultiPayment, MultiPaymentDTO>(multiPayment);
                    var result = await multiPaymentOpDAO.WebMultiPaymentSelect(batchId, multi);
                    var txnList = new List<MultipleTxnRecordDTO>();
                    MultiPaymentDTO multilPayment = new MultiPaymentDTO();
                    if(result.Count() > 0)
                    {
                       foreach(var item in result)
                       {
                           MultipleTxnRecordDTO multipleTxtRecord = new MultipleTxnRecordDTO();
                           multipleTxtRecord.AcctNo = Convert.ToInt32(item.AcctNo.ToString());
                           multipleTxtRecord.TxnAmt = Convert.ToString(decimal.Round(item.TxnAmt, 2, MidpointRounding.AwayFromZero));
                           multipleTxtRecord.TxnDescp = item.Descp;
                           multipleTxtRecord.SelectedSts = item.Sts;
                           multipleTxtRecord.ChequeNo = Convert.ToString(item.ChequeNo);
                           multipleTxtRecord.CreationDate = CardTrend.Common.Extensions.NumberExtensions.DateConverter(item.CreationDate);
                           multipleTxtRecord.SelectedOwner = item.Owner;
                           multipleTxtRecord.TxnId = item.TxnId;
                           multipleTxtRecord.AcctName = item.AccountName;
                           txnList.Add(multipleTxtRecord);
                           //-----------
                           multilPayment.TxnId = item.TxnId;
                           multilPayment.TxnDate = item.TxnDate;
                           multilPayment.DueDate = item.DueDate;
                           multilPayment.RefNo = item.RefNo;
                           multilPayment.TxnCd = item.TxnCd;
                           multilPayment.TxnCdDescp = Convert.ToString(item.TxnCd);
                           multilPayment.ChequeNo = item.ChequeNo;
                           multilPayment.ChequeAmt = item.ChequeAmt;
                           multilPayment.CreationDate = item.CreationDate;
                           multilPayment.Owner = item.Owner;
                           multilPayment.IssuingBank = item.IssuingBank;
                           multilPayment.SlipNo = item.SlipNo;
                           multilPayment.Sts = item.Sts;
                           multilPayment.PymtType = item.PymtType;
                           multilPayment.SettleVal = item.SettleVal;
                       }
                    }
                    multilPayment.MultipleTxnRecordList = txnList;
                    response.multiPayment = Mapper.Map<MultiPaymentDTO, MultiPayment>(multilPayment);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebMultiPaymentSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Mar 5, 2017
         Function:     WebGetGLCodeByTxnCd
         Purpose:      WebGetGLCodeByTxnCd
         Inputs:       txnCd,settleVal,acctNo
         Returns:      MultiPaymentResponse
        *************************************/
        public async Task<MultiPaymentResponse> WebGetGLCodeByTxnCd(string txnCd, string settleVal, string acctNo)
        {
            Logger.Info("Invoking WebGetGLCodeByTxnCd function");
            var response = new MultiPaymentResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var multiPaymentOpDAO = scope.Resolve<IMultiPaymentOpDAO>();
                    var glcodes = await multiPaymentOpDAO.WebGetGLCode(txnCd,settleVal,acctNo);
                    if (glcodes.Count() > 0)
                        response.multiPayments = Mapper.Map<List<MultiPayment>>(glcodes);
                    response.Status = ResponseStatus.Success;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetGLCodeByTxnCd: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Mar 5, 2017
         Function:     GetPymtTxnCd
         Purpose:      GetPymtTxnCd
         Inputs:       GlSettlementCd,TxnCat
         Returns:      IEnumerable
         *************************************/
        public async Task<IEnumerable<SelectListItem>> GetPymtTxnCd(string GlSettlementCd, string TxnCat)
        {
            Logger.Info("Invoking GetPymtTxnCd function");
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var multiPaymentOpDAO = scope.Resolve<IMultiPaymentOpDAO>();
                    var pymtTxnCds = await multiPaymentOpDAO.WebGetPymtTxnCd(GlSettlementCd, TxnCat);
                    if (pymtTxnCds != null)
                        return pymtTxnCds;
                    else
                        return new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPymtTxnCd: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
            }
            return null;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Mar 5, 2017
         Function:     SaveWebMultiPaymentMaint
         Purpose:      SaveWebMultiPaymentMaint
         Inputs:       TxnAdjustmentDTO
         Returns:      SaveAcctSignUpResponse
       *************************************/
        public async Task<SaveAcctSignUpResponse> SaveWebMultiPaymentMaint(TxnAdjustment txnAdjustmentModel)
        {
            Logger.Info("Invoking SaveWebMultiPaymentMaint function");
            var response = new SaveAcctSignUpResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var multiPaymentOpDAO = scope.Resolve<IMultiPaymentOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var txnAdjustment = Mapper.Map<TxnAdjustment, TxnAdjustmentDTO>(txnAdjustmentModel);
                    var result = await multiPaymentOpDAO.WebMultiPaymentMaint(txnAdjustment);
                    var message = await controlDAO.GetMessageCode(result.Flag);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                    response.returnValue.BatchId = result.paraOut.BatchId;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveWebMultiPaymentMaint: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
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
                        response.multiPayments = Mapper.Map<List<MultiPayment>>(objCodes);
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

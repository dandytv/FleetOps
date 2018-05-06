using Autofac;
using AutoMapper;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.ManualSlipEntry;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
   public interface IManualSlipEntryOpService
    {
       ManualSlipEntryResponse GetManualSlipEntryBatchList();
       Task<ManualSlipEntryResponse> GetManualSlipEntryTxnList(string settleId);
       Task<ManualSlipEntryResponse> GetManualSlipEntryBatchDetail(string settleId);
       Task<SaveGeneralInfoResponse> SaveMerchManualBatch(ManualSlipEntry manualSlipEntryBatchDetailModel);
       Task<SaveGeneralInfoResponse> SaveManualSlipEntry(ManualSlipEntry merchManualTxnModel);
       Task<SaveGeneralInfoResponse> DeleteMerchManualTxn(string batchId, string settleId, string txnId, string detailTxnId);
       Task<SaveGeneralInfoResponse> SaveMerchManualTxnProduct(ManualSlipEntry manualSlipEntryModel);
       Task<ManualSlipEntryResponse> GetMerchManualTxnProductDetail(string txnId, string txnDetailId);
       ManualSlipEntryResponse GetManualSlipEntryTxnDetail(string TxnId);
       ManualSlipEntryResponse GetManualTransaction(string settleId);
       ManualSlipEntryResponse GetManualTxnProducts(string txnId);
    }
    public class ManualSlipEntryOpService : IManualSlipEntryOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;

        public ManualSlipEntryOpService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region ManualSlipEntryOpService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<ManualSlipEntryOpDAO>().As<IManualSlipEntryOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region ManualSlipEntryOpService
        /*************************************
        Created by:   Tuan Tran
        Created on:   March 2, 2017
        Function:     GetManualSlipEntryBatchList
        Purpose:      GetManualSlipEntryBatchList
        Inputs:       
        Returns:      PinMailerBatchResponse
        *************************************/
        public ManualSlipEntryResponse GetManualSlipEntryBatchList()
        {
            Logger.Info("Invoking GetManualSlipEntryBatchList fuction use EF to call SP");
            var response = new ManualSlipEntryResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var results = manualSlipEntryOpDAO.GetManualSlipEntryBatchList();
                    if(results.Count() > 0)
                        response.merchManualTxns = Mapper.Map<IList<ManualSlipEntryDTO>,IList<ManualSlipEntry>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetManualSlipEntryBatchList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 1, 2017
        Function:     GetManualSlipEntryTxnList
        Purpose:      GetManualSlipEntryTxnList
        Inputs:       settleId
        Returns:      ManualSlipEntryResponse
       *************************************/
        public async Task<ManualSlipEntryResponse> GetManualSlipEntryTxnList(string settleId)
        {
            Logger.Info("Invoking GetManualSlipEntryTxnList fuction use EF to call SP");
            var response = new ManualSlipEntryResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var results = await manualSlipEntryOpDAO.GetManualSlipEntryTxnList(settleId);
                    if(results.Count() > 0)
                        response.merchManualTxns = Mapper.Map<IList<MerchManualTxnDTO>,IList<ManualSlipEntry>>(results);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetManualSlipEntryTxnList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 1, 2017
        Function:     GetManualSlipEntryTxnDetail
        Purpose:      GetManualSlipEntryTxnDetail
        Inputs:       TxnId
        Returns:      ManualSlipEntryResponse
       *************************************/
        public ManualSlipEntryResponse GetManualSlipEntryTxnDetail(string TxnId)
        {
            Logger.Info("Invoking GetManualSlipEntryTxnDetail fuction use EF to call SP");
            var response = new ManualSlipEntryResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var result = manualSlipEntryOpDAO.GetManualSlipEntryTxnDetail(TxnId);
                    if(result != null)
                        response.manualSlipEntryBatchDetail = Mapper.Map<MerchManualTxnDTO,ManualSlipEntry>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetManualSlipEntryTxnDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 1, 2017
        Function:     GetManualSlipEntryBatchDetail
        Purpose:      GetManualSlipEntryBatchDetail
        Inputs:       settleId
        Returns:      ManualSlipEntryResponse
        *************************************/
        public async Task<ManualSlipEntryResponse> GetManualSlipEntryBatchDetail(string settleId)
        {
            Logger.Info("Invoking GetManualSlipEntryBatchDetail fuction use EF to call SP");
            var response = new ManualSlipEntryResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await manualSlipEntryOpDAO.GetManualSlipEntryBatchDetail(settleId);
                    if(result != null)
                    {
                        response.manualSlipEntryBatchDetail = Mapper.Map<ManualSlipEntryBatchDetailDTO,ManualSlipEntry>(result);
                        response.manualSlipEntryBatchDetail.SettleId = settleId;
                        response.manualSlipEntryBatchDetail.Sts = await controlDAO.GetRefLib("MerchBatchSts");
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetManualSlipEntryBatchDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 2, 2017
        Function:     SaveMerchManualBatch
        Purpose:      SaveMerchManualBatch
        Inputs:       ManualSlipEntryBatchDetailDTO
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveGeneralInfoResponse> SaveMerchManualBatch(ManualSlipEntry manualSlipEntryBatchDetailModel)
        {
            Logger.Info("Invoking SaveMerchManualBatch function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var manualSlipEntryBatchDetail = Mapper.Map<ManualSlipEntry, ManualSlipEntryBatchDetailDTO>(manualSlipEntryBatchDetailModel);
                    var result = await manualSlipEntryOpDAO.SaveMerchManualBatch(manualSlipEntryBatchDetail);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveMerchManualBatch: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.desp = ex.Message;
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 2, 2017
        Function:     SaveManualSlipEntry
        Purpose:      SaveManualSlipEntry
        Inputs:       MerchManualTxnDTO
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveGeneralInfoResponse> SaveManualSlipEntry(ManualSlipEntry merchManualTxnModel)
        {
            Logger.Info("Invoking SaveManualSlipEntry function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var merchManualTxnDto = Mapper.Map<ManualSlipEntry, MerchManualTxnDTO>(merchManualTxnModel);
                    var result = await manualSlipEntryOpDAO.SaveManualSlipEntry(merchManualTxnDto);
                    var message = await controlDAO.GetMessageCode(result.Flag);
                    response.paraRes.TxnId = result.paraOut.TxnId;
                    response.paraRes.SettleId = result.paraOut.SettleId;
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveManualSlipEntry: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.desp = ex.Message;
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   May 5, 2017
        Function:     DeleteMerchManualTxn
        Purpose:      DeleteMerchManualTxn
        Inputs:       batchId,settleId,txnId,detailTxnId
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveGeneralInfoResponse> DeleteMerchManualTxn(string batchId, string settleId, string txnId, string detailTxnId)
        {
            Logger.Info("Invoking DeleteMerchManualTxn function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var result = await manualSlipEntryOpDAO.DeleteMerchManualTransaction(batchId,settleId,txnId,detailTxnId);
                    var message = await controlDAO.GetMessageCode(result);
                    response.desp = message.Descp;
                    response.flag = message.Flag;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in DeleteMerchManualTxn: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.desp = ex.Message;
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   May 5, 2017
        Function:     GetManualTransaction
        Purpose:      GetManualTransaction
        Inputs:       settleId
        Returns:      ManualSlipEntryResponse
        *************************************/
        public ManualSlipEntryResponse GetManualTransaction(string settleId)
        {
            Logger.Info("Invoking GetManualTransaction fuction use EF to call SP");
            var response = new ManualSlipEntryResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var result = manualSlipEntryOpDAO.GetManualTxn(settleId);
                    if(result != null)
                        response.manualSlipEntryBatchDetail = Mapper.Map<ManualTxnDTO,ManualSlipEntry>(result);
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetManualTransaction: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
        Created by:   dandy boy
        Created on:   June 29, 2017
        Function:     GetManualTxnProducts
        Purpose:      GetManualTxnProducts
        Inputs:       settleId
        Returns:      ManualSlipEntryResponse
        *************************************/
        public  ManualSlipEntryResponse GetManualTxnProducts(string txnId)
        {
            Logger.Info("Invoking GetManualTxnProducts fuction use EF to call SP");
            var response = new ManualSlipEntryResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results =  manualSlipEntryOpDAO.GetManualTxnProductList(txnId);
                    if (results.Count() > 0)
                    {
                        response.manualTxnProducts = Mapper.Map<List<ManualTxnProductDTO>,List<ManualTxnProduct>>(results);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetManualTxnProducts: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   june 29, 2017
        Function:     SaveMerchManualTxnProduct
        Purpose:      SaveMerchManualTxnProduct
        Inputs:       MerchManualTxnDTO
        Returns:      SaveGeneralInfoResponse
        *************************************/
        public async Task<SaveGeneralInfoResponse> SaveMerchManualTxnProduct(ManualSlipEntry manualSlipEntryModel)
        {
            Logger.Info("Invoking SaveMerchManualTxnProduct function");
            var response = new SaveGeneralInfoResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var manualSlipEntry = Mapper.Map<ManualSlipEntry, MerchManualTxnDTO>(manualSlipEntryModel);
                    var result = await manualSlipEntryOpDAO.SaveMerchManualTxnProduct(manualSlipEntry);
                    if(result != null)
                    {
                        var message = await controlDAO.GetMessageCode(result.Flag);
                        response.desp = message.Descp;
                        response.flag = message.Flag;
                        response.paraRes.TxnDetailId = result.paraOut.TxnDetailId;
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in SaveMerchManualTxnProduct: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.desp = ex.Message;
                response.Status = ResponseStatus.Exception;
                response.flag = 1;
                response.Message = msg;
            }
            return response;

        }
        /*************************************
        Created by:   dandy Boy
        Created on:   june 29, 2017
        Function:     GetMerchManualTxnProductDetail
        Purpose:      GetMerchManualTxnProductDetail
        Inputs:       txnId,txnDetailId
        Returns:      ManualSlipEntryResponse
        *************************************/
        public async Task<ManualSlipEntryResponse> GetMerchManualTxnProductDetail(string txnId, string txnDetailId)
        {
            Logger.Info("Invoking GetMerchManualTxnProductDetail fuction use EF to call SP");
            var response = new ManualSlipEntryResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var manualSlipEntryOpDAO = scope.Resolve<IManualSlipEntryOpDAO>();
                    var result = await manualSlipEntryOpDAO.GetMerchManualTxnProductDetail(txnId, txnDetailId);
                    if (result != null)
                    {
                        response.manualSlipEntryBatchDetail = Mapper.Map<MerchManualTxnDTO,ManualSlipEntry>(result);
                    }

                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMerchManualTxnProductDetail: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        #endregion
    }
}

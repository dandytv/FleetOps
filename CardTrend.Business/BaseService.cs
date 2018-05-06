using CardTrend.Common.Log;
using CardTrend.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using Autofac;
using System.Threading.Tasks;
using CardTrend.DAL.DAO;
using CardTrend.Business.MessageContracts;
using CardTrend.Business.MessageBase;
using CardTrend.Common.EncryptData;
using CardTrend.Common.Helpers;
using System.IO;
using System.Xml.Linq;
using CardTrend.DAL.Contexts;
using System.Web.Mvc;

namespace CardTrend.Business
{
    public interface IBaseService
    {		
        Task<IList<SelectListItem>> GetPlasticType();
        Task<IList<SelectListItem>> GetPlasticType(string module);
        Task<IList<SelectListItem>> WebGetCorpCd(bool value);
        Task<IList<SelectListItem>> GetCycle(string module);
        Task<IList<SelectListItem>> GetCAOReasonCd();
        Task<IList<SelectListItem>> GetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true);
        Task<IList<SelectListItem>> GetRefLib2(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true);
        IEnumerable<SelectListItem> WebGetYear();
        Task<IList<SelectListItem>> GetCardType(string AppcId = null, string CardNo = null, string ApplId = null, string AcctNo = null);
        GetDataVersionResponse GetDataVersion(string DataType = null, string Ind = null);
        Task<IList<SelectListItem>> WebGetState(string CtryCd);
        Task<IList<SelectListItem>> WebProductGroupSelect();
        Task<IList<SelectListItem>> WebGetPlan(string MapInd);
        Task<IList<SelectListItem>> WebGetTxnCode(string Module, string CategoryType = null, string ManualEntry = "y");
        Task<IList<SelectListItem>> WebGetProduct(string ApplId, bool combineProdCdnPrice = true);
        Task<ControlListResponse> GetRptType();
        Task<ControlListResponse> GetNextMilestone(int currentTaskNo, string workFlowCd);
        Task<ControlListResponse> GetCycleStmt(string cycNo);
        Task<IList<SelectListItem>> GetTransactionCategory();
        Task<IList<SelectListItem>> GetEvtRefConf(string EvtTypeId);
        Task<IList<SelectListItem>> GetEvtType();
        Task<IList<SelectListItem>> GetEvtInd();
        Task<IList<SelectListItem>> GetEvtRef(string eventTypeId);
        Task<IList<SelectListItem>> GetUserAccess(string userAccess, bool PrependNull = true);
        Task<IList<SelectListItem>> GetPymtTxnCd(string txnCat, string shortDescp);
        Task<IList<SelectListItem>> GetTermId(string busnLocation);
        Task<IList<SelectListItem>> GetCostCentre(string refKey, string refTo, bool PrependNull = true);
        Task<IList<SelectListItem>> GetSKDS(string applId, string acctNo);
        Task<IList<SelectListItem>> GetFeeCd(string feeType, bool PrependNull = true);
        Task<IList<SelectListItem>> GetCardMedia();
        Task<IList<SelectListItem>> WebGetDealerBusnLoc(string acctNo, string cardNo, string state);
        Task<IList<SelectListItem>> GetMerchType(string merchType);
        Task<IList<SelectListItem>> GetCardNo(string acctNo);
        Task<IList<SelectListItem>> WebGetBillItemTxnCategory(string id);
        Task<IList<SelectListItem>> WebGetDealerByMerch(string acctNo);
        Task<IList<SelectListItem>> GetFrameGetTxnCategory(string refName);
        Task<IList<SelectListItem>> GetDeviceModel();
        Task<IList<SelectListItem>> GetAcctSubsidy(string acctNo);
    }
    public class BaseService : IBaseService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;

        public BaseService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }         
        }
        #region BaseService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
               // afBuilder.RegisterType<pdb_ccmsContext>().As<Ipdb_ccmsContext>().WithParameter("connectionString", AppConfigurationHelper.pdb_ccmsCnnStr);
                afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
                afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
                Container = afBuilder.Build();
        }
        #endregion
        #region ControlService
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 23, 2017
        Function:     GetPlasticType
        Purpose:      GetPlasticType
        Inputs:       
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetPlasticType()
        {
            Logger.Info("Invoking GetPlasticType function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.GetPlasticTypeSelect();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPlasticType: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 23, 2017
        Function:     GetPlasticType
        Purpose:      GetPlasticType
        Inputs:       module
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetPlasticType(string module)
        {
            Logger.Info("Invoking GetPlasticType(string module) function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.GetPlasticType(module);
                    response.RefLibLst = results;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPlasticType(string module): detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 23, 2017
        Function:     WebGetCorpCd
        Purpose:      WebGetCorpCd
        Inputs:       value
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> WebGetCorpCd(bool value)
        {
            Logger.Info("Invoking WebGetCorpCd function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetCorpCd(value);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetCorpCd: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 23, 2017
        Function:     GetCycle
        Purpose:      GetCycle
        Inputs:       module
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetCycle(string module)
        {
            Logger.Info("Invoking WebGetCycle function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.GetCycle(module);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetCycle: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 23, 2017
        Function:     GetCAOReasonCd
        Purpose:      GetCAOReasonCd
        Inputs:       
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetCAOReasonCd()
        {
            Logger.Info("Invoking GetCAOReasonCd function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.GetCAOReasonCd();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCAOReasonCd: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 23, 2017
        Function:     GetDeviceModel
        Purpose:      GetDeviceModel
        Inputs:       
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetDeviceModel()
        {
            Logger.Info("Invoking GetDeviceModel function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetDeviceModel();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetDeviceModel: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 20, 2017
        Function:     GetRefLib
        Purpose:      GetRefLib
        Inputs:       refType,RefNo,RefInd,RefId,PrependNull
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true)
        {
            Logger.Info("Invoking GetRefLib function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.GetRefLib(refType, RefNo, RefInd, RefId, PrependNull);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRefLib: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }

        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 20, 2017
        Function:     GetRefLib2
        Purpose:      GetRefLib2
        Inputs:       refType,RefNo,RefInd,RefId,PrependNull
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetRefLib2(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true)
        {
            Logger.Info("Invoking GetRefLib function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.GetRefLib2(refType, RefNo, RefInd, RefId, PrependNull);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRefLib: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 17, 2017
        Function:     WebGetYear
        Purpose:      WebGetYear
        Inputs:       
        Returns:      IEnumerable of selectList
        *************************************/
        public IEnumerable<SelectListItem> WebGetYear()
        {
            Logger.Info("Invoking WebGetYear function");
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = ctrLstDAO.WebGetYear();
                    return results;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetYear: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                //response.Status = ResponseStatus.Exception;
                //response.Message = msg;
            }
            return null;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 10, 2017
        Function:     GetCardType
        Purpose:      GetCardType
        Inputs:       AppcId,CardNo,ApplId,AcctNo
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetCardType(string AppcId = null, string CardNo = null, string ApplId = null, string AcctNo = null)
        {
            Logger.Info("Invoking GetCardType function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetCardType(AppcId, CardNo, ApplId, AcctNo);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCardType: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 10, 2017
        Function:     GetDataVersion
        Purpose:      GetDataVersion
        Inputs:       DataType,Ind
        Returns:      ControlListResponse
        *************************************/
        public GetDataVersionResponse GetDataVersion(string DataType = null, string Ind = null)
        {
            Logger.Info("Invoking GetDataVersion function");
            var response = new GetDataVersionResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = ctrLstDAO.GetDataVersion(DataType, Ind);
                    response.dataVersionLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetDataVersion: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 8, 2017
        Function:     WebGetState
        Purpose:      WebGetState
        Inputs:       CtryCd
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> WebGetState(string CtryCd)
        {
            Logger.Info("Invoking WebGetState function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetState(CtryCd);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetState: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Feb 8, 2017
        Function:     WebProductGroupSelect
        Purpose:      WebProductGroupSelect
        Inputs:       
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> WebProductGroupSelect()
        {
            Logger.Info("Invoking WebProductGroupSelect function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebProductGroupSelect();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebProductGroupSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Feb 8, 2017
         Function:     WebGetPlan
         Purpose:      WebGetPlan
         Inputs:       MapInd
         Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> WebGetPlan(string MapInd)
        {
            Logger.Info("Invoking WebGetPlan function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetPlan(MapInd);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetPlan: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Feb 3, 2017
         Function:     WebGetTxnCode
         Purpose:      WebGetTxnCode
         Inputs:       Module,CategoryType,ManualEntry
         Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> WebGetTxnCode(string Module, string CategoryType = null, string ManualEntry = "y")
        {
            Logger.Info("Invoking WebGetTxnCode function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetTxnCode(Module,CategoryType,ManualEntry);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetTxnCode: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   Mar 3, 2017
        Function:     WebGetProduct
        Purpose:      WebGetProduct
        Inputs:       ApplId,combineProdCdnPrice
        Returns:      SelectListItem List
       *************************************/
        public async Task<IList<SelectListItem>> WebGetProduct(string ApplId, bool combineProdCdnPrice = true)
        {
            Logger.Info("Invoking WebGetProduct function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetProduct(ApplId, combineProdCdnPrice);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetProduct: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Mar 3, 2017
         Function:     GetRptType
         Purpose:      GetRptType
         Inputs:       
         Returns:      ControlListResponse
        *************************************/
        public async Task<ControlListResponse> GetRptType()
        {
            Logger.Info("Invoking GetRptType function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetRptType();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRptType: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
         Created by:   Tuan Tran
         Created on:   Mar 16, 2017
         Function:     GetNextMilestone
         Purpose:      GetNextMilestone
         Inputs:       currentTaskNo,workFlowCd
         Returns:      ControlListResponse
       *************************************/
        public async Task<ControlListResponse> GetNextMilestone(int currentTaskNo, string workFlowCd)
        {
            Logger.Info("Invoking GetNextMilestone function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetNextMilestone(currentTaskNo,workFlowCd);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetNextMilestone: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        /*************************************
          Created by:   Tuan Tran
          Created on:   Mar 20, 2017
          Function:     GetCycleStmt
          Purpose:      GetCycleStmt
          Inputs:       cycNo
          Returns:      ControlListResponse
         *************************************/
        public async Task<ControlListResponse> GetCycleStmt(string cycNo)
        {
            Logger.Info("Invoking GetCycleStmt function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetCycleStmt(cycNo);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCycleStmt: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }

        /*************************************
          Created by:   dandy boy
          Created on:   Mar 27, 2017
          Function:     GetTransactionCategory
          Purpose:      GetTransactionCategory
          Inputs:       
          Returns:      ControlListResponse
         *************************************/
        public async Task<IList<SelectListItem>> GetTransactionCategory()
        {
            Logger.Info("Invoking GetTransactionCategory function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetTxnCategory();
                    response.RefLibLst = results;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetTransactionCategory: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }

            return response.RefLibLst;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   mar 29, 2017
        Function:     GetEvtRefConf
        Purpose:      GetEvtRefConf
        Inputs:       EvtTypeId
        Returns:      List of SelectListItem
       *************************************/
        public async Task<IList<SelectListItem>> GetEvtRefConf(string EvtTypeId)
        {
            Logger.Info("Invoking GetEvtRefConf function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetEvtRefConf(EvtTypeId);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEvtRefConf: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   April 19, 2017
        Function:     WebGetEvtType
        Purpose:      WebGetEvtType
        Inputs:       
        Returns:      List of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetEvtType()
        {
            Logger.Info("Invoking WebGetEvtType function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetEvtType();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetEvtType: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   April 21, 2017
        Function:     GetEvtInd
        Purpose:      GetEvtInd
        Inputs:       
        Returns:      List of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetEvtInd()
        {
            Logger.Info("Invoking GetEvtInd function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetEvtInd();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEvtInd: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }

        /*************************************
        Created by:   dandy boy
        Created on:   April 21, 2017
        Function:     GetEvtRef
        Purpose:      GetEvtRef
        Inputs:       
        Returns:      List of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetEvtRef(string eventTypeId)
        {
            Logger.Info("Invoking GetEvtRef function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetEvtRef(eventTypeId);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetEvtRef: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   April 25, 2017
        Function:     GetUserAccess
        Purpose:      GetUserAccess
        Inputs:       
        Returns:      List of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetUserAccess(string userAccess, bool PrependNull = true)
        {
            Logger.Info("Invoking GetUserAccess function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetUserAccess(userAccess,PrependNull);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetUserAccess: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   May 2, 2017
        Function:     GetPymtTxnCd
        Purpose:      WebGetState
        Inputs:       txnCat,shortDescp
        Returns:      list of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetPymtTxnCd(string txnCat, string shortDescp)
        {
            Logger.Info("Invoking GetPymtTxnCd function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetPymtTxnCd(txnCat,shortDescp);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetPymtTxnCd: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   May 11, 2017
        Function:     GetTermId
        Purpose:      GetTermId
        Inputs:       busnLocation
        Returns:      list of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetTermId(string busnLocation)
        {
            Logger.Info("Invoking GetTermId function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetTermId(busnLocation);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetTermId: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   dandy boy
        Created on:   May 22, 2017
        Function:     GetCostCentre
        Purpose:      GetCostCentre
        Inputs:       refKey,refTo
        Returns:      list of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetCostCentre(string refKey, string refTo, bool PrependNull = true)
        {
            Logger.Info("Invoking GetCostCentre function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetCostCentre(refKey, refTo, PrependNull);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCostCentre: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   May 22, 2017
        Function:     GetSKDS
        Purpose:      GetSKDS
        Inputs:       applId,acctNo
        Returns:      list of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetSKDS(string applId, string acctNo)
        {
            Logger.Info("Invoking GetSKDS function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetSKDS(applId, acctNo);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetSKDS: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   May 22, 2017
        Function:     GetFeeCd
        Purpose:      GetFeeCd
        Inputs:       feeType
        Returns:      list of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetFeeCd(string feeType, bool PrependNull = true)
        {
            Logger.Info("Invoking GetFeeCd function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetFeeCd(feeType, PrependNull);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFeeCd: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   May 22, 2017
        Function:     GetCardMedia
        Purpose:      GetCardMedia
        Inputs:       
        Returns:      list of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetCardMedia()
        {
            Logger.Info("Invoking GetCardMedia function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetCardMedia();
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCardMedia: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   May 30, 2017
        Function:     WebGetDealerBusnLoc
        Purpose:      WebGetDealerBusnLoc
        Inputs:       acctNo,cardNo,state
        Returns:      list of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> WebGetDealerBusnLoc(string acctNo, string cardNo, string state)
        {
            Logger.Info("Invoking WebGetDealerBusnLoc function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetDealerBusnLoc(acctNo,cardNo,state);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetDealerBusnLoc: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }

        /*************************************
        Created by:   Dandy
        Created on:   05 June, 2017
        Function:     GetMerchType
        Purpose:      GetMerchType
        Inputs:       
        Returns:      list of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> GetMerchType(string merchType)
        {
            Logger.Info("Invoking GetMerchType function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetMerchType(merchType);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetMerchType: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 19, 2017
        Function:     GetCardNo
        Purpose:      GetCardNo
        Inputs:       acctNo
        Returns:      SelectListItem list 
        *************************************/
        public async Task<IList<SelectListItem>> GetCardNo(string acctNo)
        {
            Logger.Info("Invoking GetCardNo function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetCardNo(acctNo);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetCardNo: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   June 19, 2017
        Function:     WebGetBillItemTxnCategory
        Purpose:      WebGetBillItemTxnCategory
        Inputs:       id
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> WebGetBillItemTxnCategory(string id)
        {
            Logger.Info("Invoking WebGetBillItemTxnCategory function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetBillItemTxnCategory(id);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetBillItemTxnCategory: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Tuan Tran
        Created on:   May 30, 2017
        Function:     WebGetDealerByMerch
        Purpose:      WebGetDealerByMerch
        Inputs:       acctNo
        Returns:      list of SelectListItem
        *************************************/
        public async Task<IList<SelectListItem>> WebGetDealerByMerch(string acctNo)
        {
            Logger.Info("Invoking WebGetDealerByMerch function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var ctrLstDAO = scope.Resolve<IControlDAO>();
                    var results = await ctrLstDAO.WebGetDealerByMerch(acctNo);
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in WebGetDealerByMerch: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Dandy boy
        Created on:   July 01, 2017
        Function:     GetFrameGetTxnCategory
        Purpose:      GetFrameGetTxnCategory
        Inputs:       refName
        Returns:      ControlListResponse
       *************************************/
        public async Task<IList<SelectListItem>> GetFrameGetTxnCategory(string refName)
        {
            Logger.Info("Invoking GetFrameGetTxnCategory function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.iFrameGetTxnCategory(refName);
                    if(results.Count() > 0)
                    response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetFrameGetTxnCategory: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        /*************************************
        Created by:   Dandy boy
        Created on:   July 01, 2017
        Function:     GetAcctSubsidy
        Purpose:      GetAcctSubsidy
        Inputs:       acctNo
        Returns:      ControlListResponse
        *************************************/
        public async Task<IList<SelectListItem>> GetAcctSubsidy(string acctNo)
        {
            Logger.Info("Invoking GetAcctSubsidy function");
            var response = new ControlListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var controlDAO = scope.Resolve<IControlDAO>();
                    var results = await controlDAO.WebGetAcctSubsidy(acctNo);
                    if (results.Count() > 0)
                        response.RefLibLst = results;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctSubsidy: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.RefLibLst;
        }
        #endregion
    }
}

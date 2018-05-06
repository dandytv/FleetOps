using Autofac;
using CardTrend.Business.MessageBase;
using CardTrend.Business.MessageContracts;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.SOASummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
   public interface IAccountSOAOpService
    {
       Task<AcctSOASummSelectResponse> GetAcctSOASummSelect(string acctNo);
       Task<AcctSOASummResponse> GetAcctSOASummList(string acctNo);
       Task<List<AcctSOATxnCategoryDTO>> GetAcctSOATxnCategoryList(string acctNo, string selectedStmtDate);
       Task<List<AcctSOATxnDTO>> GetAcctSOATxnList(string acctNo, string selectedStmtDate, string txnCode);
    }
   public class AccountSOAOpService : IAccountSOAOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public AccountSOAOpService()
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
            afBuilder.RegisterType<AccountSOAOpDAO>().As<IAccountSOAOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region AccountSOAOp Service
        public async Task<AcctSOASummSelectResponse> GetAcctSOASummSelect(string acctNo)
        {
            Logger.Info("Invoking GetAcctSOASummSelect function");
            var response = new AcctSOASummSelectResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountSOAOpDAO = scope.Resolve<IAccountSOAOpDAO>();
                    var result = await accountSOAOpDAO.WebAcctSOASummSelect(acctNo);
                    response.acctSOASummary = result;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctSOASummSelect: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        public async Task<AcctSOASummResponse> GetAcctSOASummList(string acctNo)
        {
            Logger.Info("Invoking GetAcctSOASummList function");
            var response = new AcctSOASummResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountSOAOpDAO = scope.Resolve<IAccountSOAOpDAO>();
                    var result = await accountSOAOpDAO.WebAcctSOASummList(acctNo);
                    response.AcctSOASummaryLst = result;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctSOASummList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response;
        }
        public async Task<List<AcctSOATxnCategoryDTO>> GetAcctSOATxnCategoryList(string acctNo, string selectedStmtDate)
        {
            Logger.Info("Invoking GetAcctSOATxnCategoryList function");
            var response = new GetAcctSOATxnCategoryListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountSOAOpDAO = scope.Resolve<IAccountSOAOpDAO>();
                    var result = await accountSOAOpDAO.WebAcctSOATxnCategoryList(acctNo, selectedStmtDate);
                    response.acctSOATxnCategory = result;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctSOATxnCategoryList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.acctSOATxnCategory.ToList();
        }
        public async Task<List<AcctSOATxnDTO>> GetAcctSOATxnList(string acctNo, string selectedStmtDate, string txnCode)
        {
            Logger.Info("Invoking GetAcctSOATxnList function");
            var response = new GetAcctSOATxnListResponse()
            {
                Status = ResponseStatus.Failure,
            };

            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var accountSOAOpDAO = scope.Resolve<IAccountSOAOpDAO>();
                    var result = await accountSOAOpDAO.WebAcctSOATxnList(acctNo, selectedStmtDate, txnCode);
                    response.accountSOATxnLst = result;
                }
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetAcctSOATxnList: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
                response.Status = ResponseStatus.Exception;
                response.Message = msg;
            }
            return response.accountSOATxnLst.ToList();
        }
        #endregion
    }
}

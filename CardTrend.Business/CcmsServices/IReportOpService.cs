using Autofac;
using CardTrend.Common.Helpers;
using CardTrend.Common.Log;
using CardTrend.DAL.DAO;
using CardTrend.Domain.Dto.ReportViewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.CcmsServices
{
    public interface IReportOpService
    {
        object GetReportViewer(ReportViewerDTO rpt);
        List<object> GetRowsReport(ReportViewerDTO rpt);
        DataTable GetTableReport(ReportViewerDTO rpt);
    }
    public class ReportOpService : IReportOpService
    {
        private static Autofac.IContainer Container { get; set; }
        private static ICardTrendLogger Logger;
        public ReportOpService()
        {
            RegisterDAOComponents();
            using (var scope = Container.BeginLifetimeScope())
            {
                Logger = scope.Resolve<ICardTrendLogger>();
            }
        }
        #region ReportOpService registerComponent
        public static void RegisterDAOComponents()
        {
            var afBuilder = new ContainerBuilder();
            afBuilder.RegisterType<ReportOpDAO>().As<IReportOpDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<ControlDAO>().As<IControlDAO>().WithParameter("connString", AppConfigurationHelper.pdb_ccmsCnnStr);
            afBuilder.RegisterType<CardTrendNLogLogger>().As<ICardTrendLogger>().AutoActivate();
            Container = afBuilder.Build();
        }
        #endregion
        #region ReportOpService Service
        public object GetReportViewer(ReportViewerDTO rpt)
        {
            Logger.Info("Invoking GetReportViewer function");
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var reportpDAO = scope.Resolve<IReportOpDAO>();
                    var result = reportpDAO.GetReport(rpt);
                    return result;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetReportViewer: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
            }
            return null;
        }
        public List<object> GetRowsReport(ReportViewerDTO rpt)
        {
            Logger.Info("Invoking GetRowsReport function");
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var reportpDAO = scope.Resolve<IReportOpDAO>();
                    var result = reportpDAO.GetRowReport(rpt);
                    return result;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetRowsReport: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
            }
            return null;
        }
        public DataTable GetTableReport(ReportViewerDTO rpt)
        {
            Logger.Info("Invoking GetTableReport function");
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    var reportpDAO = scope.Resolve<IReportOpDAO>();
                    var result = reportpDAO.GetTableReport(rpt);
                    return result;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error in GetTableReport: detail:{0}", ex.Message);
                Logger.Error(msg, ex);
            }
            return null;
        }
        #endregion
    }
}

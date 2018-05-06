using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Collection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface ICollectionOpDAO
    {
        Task<IList<CollectionTaskDTO>> GetAllAcctCollection(CollectionTaskDTO collection);
        Task<DelinquentAcctsTresholdLimit> GetThresholdLmtCollection(int offSet, Int64 noOfRecs, Int64 tOtalNoOfRecs, string sSearch);
        Task<DelinquentAcctInfoDTO> GetCollAcctInfo(string accountNo);
        Task<DelinquentAcctFinancialInfoDTO> GetCollFinancialInfo(string AcctNo);
        Task<List<CollAgeingHistDTO>> GetCollAgeingHistory(string AcctNo);
        Task<CollPaymentHstList> GetCollPaymentHist(string acctNo, int offSet, Int64 NoOfRecs);
        Task<List<CollectionHistoryDTO>> GetCollHistory(string acctNo, string collectionCaseSts);
        Task<List<CollectionFollowUpDTO>> GetCollFollowUp(string eventId);
        Task<int> SaveCollectionFollowUp( int eventId, string userId, string collectionSts, string priority, string recallDate, string remarks);
    }
    public class CollectionOpDAO : DAOBase, ICollectionOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public CollectionOpDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<IList<CollectionTaskDTO>> GetAllAcctCollection(CollectionTaskDTO collection)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { };
                var paramCollection = new List<SqlParameter>();
                if(collection != null)
                {
                    parameters = new object[] { Common.Helpers.Common.GetIssueNo(), collection.accountNoSelected, collection.CorpCd, collection.SaleTerritory, collection.Collectionsts, collection.Owner,
                                                collection.RecallDate==null?null:CardTrend.Common.Extensions.NumberExtensions.DateConverterDB(collection.RecallDate),collection.ToRecallDate == DateTime.MinValue?null:collection.ToRecallDate
                                                ,collection.CreationDate == null?null:CardTrend.Common.Extensions.NumberExtensions.DateConverterDB(collection.CreationDate),collection.ToCreationDate == DateTime.MinValue?null:collection.ToCreationDate};
                    var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo",
                                        "@CorpCd",
                                        "@SalesTerritory",
                                        "@Status",
                                        "@Owner",
                                        "@FromRecallDate",
                                        "@ToRecallDate",
                                        "@FromCreationDate",
                                        "@ToCreationDate"
                                   };
                    paramCollection = BuildParameterList(parameters, paramNameList);
                }else
                {
                    parameters = new object[] { Common.Helpers.Common.GetIssueNo(), collection.Owner };
                   var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@Owner"
                                   };
                   paramCollection = BuildParameterList(parameters, paramNameList);
                }

                var results = await cardtrendentities.Database.SqlQuery<CollectionTaskDTO>
                    (BuildSqlCommand("WebDelinquentAccts", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                return results;
            }
        }
        public async Task<DelinquentAcctsTresholdLimit> GetThresholdLmtCollection(int offSet, Int64 noOfRecs, Int64 tOtalNoOfRecs, string sSearch)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                DelinquentAcctsTresholdLimit delinquentAcctsTresholdLimit = new DelinquentAcctsTresholdLimit();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), offSet, noOfRecs, sSearch};
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@OffSet",
                                        "@RecordsCnt",
                                        "@SearchText"
                                   };

                var outPutParameter = new object[] { new ColumnInfo { FieldName = "@TotalRecs", DataType = "int" } };
                var paramCollection = BuildParameterListWithOutPutAndRrn(parameters, outPutParameter, paramNameList);

                var transactionResults = await cardtrendentities.Database.SqlQuery<DelinquentAcctsTresholdLimitDTO>(BuildSqlCommand("WebDelinquentAcctsTresholdLimit", paramCollection), paramCollection.ToArray()).ToListAsync();
                var resultCode = paramCollection.Where(x => x.ParameterName == "@TotalRecs").FirstOrDefault().Value;
                delinquentAcctsTresholdLimit.delinquentAcctsTresholdLimits = transactionResults;
                delinquentAcctsTresholdLimit.tOtalNoOfRecs = Convert.ToInt64(resultCode);
                return delinquentAcctsTresholdLimit;
            }
        }
        public async Task<DelinquentAcctInfoDTO> GetCollAcctInfo(string accountNo)
        {

            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<DelinquentAcctInfoDTO>(BuildSqlCommand("WebDelinquentAcctInfo", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;

            }
        }
        public async Task<DelinquentAcctFinancialInfoDTO> GetCollFinancialInfo(string AcctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), AcctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<DelinquentAcctFinancialInfoDTO>(BuildSqlCommand("WebDelinquentAcctFinancialInfo", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<List<CollAgeingHistDTO>> GetCollAgeingHistory(string AcctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { AcctNo, DBNull.Value };
                var paramNameList = new[]
                                   {
                                        "@RefKey",
                                        "@RptDate"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CollAgeingHistDTO>(BuildSqlCommand("RptAcctAgeing", paramCollection), paramCollection.ToArray()).ToListAsync();
                return result;

            }
        }
        public async Task<CollPaymentHstList> GetCollPaymentHist(string acctNo, int offSet, Int64 NoOfRecs)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                CollPaymentHstList collPaymentHstList = new CollPaymentHstList();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, offSet, NoOfRecs };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@OffSet",
                                        "@NoOfRecs"
                                   };
                var outPutParameter = new object[] { new ColumnInfo { FieldName = "@TotalNoOfRecs", DataType = "bigint" } };
                var paramCollection = BuildParameterListWithOutPut(parameters, outPutParameter, paramNameList);
                var resultLst = cardtrendentities.Database.SqlQuery<CollPaymentHistDTO>(BuildSqlCommand("WebDelinquentAcctPymtHistory", paramCollection), paramCollection.ToArray());
                //var resultLst = await cardtrendentities.Database.SqlQuery<CollPaymentHistDT>(BuildSqlCommand("WebDelinquentAcctPymtHistory", paramCollection), paramCollection.ToArray()).ToListAsync();
                var collPaymentHsts = await resultLst.ToListAsync();
                var totalNoOfRecs = paramCollection.Where(x => x.ParameterName == "@TotalNoOfRecs").FirstOrDefault().Value;
                collPaymentHstList.CollPaymentHsts = collPaymentHsts;
                collPaymentHstList.TotalNoOfRecs = Convert.ToInt64(totalNoOfRecs);
                return collPaymentHstList;
            }
        }
        public async Task<List<CollectionHistoryDTO>> GetCollHistory(string acctNo,string collectionCaseSts)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, collectionCaseSts};
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@Ind"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CollectionHistoryDTO>(BuildSqlCommand("WebDelinquentAcctCollHistory", paramCollection), paramCollection.ToArray()).ToListAsync();
                return result;
            }
        }
        public async Task<List<CollectionFollowUpDTO>> GetCollFollowUp(string eventId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), eventId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@EventId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CollectionFollowUpDTO>(BuildSqlCommand("WebDelinquentAcctCollFollowupList", paramCollection), paramCollection.ToArray()).ToListAsync();
                return result;
            }
        }
        public async Task<int> SaveCollectionFollowUp( int eventId, string userId, string collectionSts, string priority, string recallDate, string remarks)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), eventId, userId, collectionSts, priority, recallDate, remarks };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@EventID",
                                        "@UserID",
                                        "@CollectionSts",
                                        "@Priority",
                                        "@RecallDate",
                                        "@Remarks"
                                    };

                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebDelinquentAcctCollFollowupMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
    }
}

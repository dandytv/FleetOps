using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto.PukaAcct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface IPukalAcctOpDAO
    {
        Task<List<PukalPaymentDTO>> GetPukalAccountList(string refCd, string acctOfficeCd, Int64 cycStmtId);
        Task<List<PukalSedutDTO>> GetPukalSedutList(string refCd, string accountOfficeCode, string status);
        Task<List<PukalAcctBatchDTO>> GetPukalAcctBatch(long batchID, string refCd, string acctOfficeCd, int cycStmtId);
    }
    public class PukalAcctOpDAO : DAOBase,IPukalAcctOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public PukalAcctOpDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<List<PukalPaymentDTO>> GetPukalAccountList(string refCd, string acctOfficeCd, Int64 cycStmtId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { refCd, acctOfficeCd.TrimStart().TrimEnd(), cycStmtId };
                var paramNameList = new[]
                                   {
                                        "@RefCd",
	                                    "@AcctOfficeCd",
                                        "@CycStmtId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<PukalPaymentDTO>
                                    (BuildSqlCommand("WebTxnPukalPaymentListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<PukalSedutDTO>> GetPukalSedutList(string refCd,string accountOfficeCode,string status)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { refCd, accountOfficeCode, status };
                var paramNameList = new[]
                                   {
                                        "@RefCd",
	                                    "@AcctOfficeCd",
                                        "@Sts"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<PukalSedutDTO>
                                    (BuildSqlCommand("WebPukalSedutListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<PukalAcctBatchDTO>> GetPukalAcctBatch(long batchID, string refCd, string acctOfficeCd, int cycStmtId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(),batchID,refCd,acctOfficeCd,cycStmtId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@BatchId",
                                        "@RefCd",
                                        "@AcctOfficeCd",
                                        "@CycStmtId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<PukalAcctBatchDTO>
                                    (BuildSqlCommand("WebTxnPukalPaymentSelect", paramCollection), paramCollection.ToArray()).ToListAsync();

                return results;
            }
        }
    }
}

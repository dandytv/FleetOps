using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto.SOASummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface IAccountSOAOpDAO
    {
        Task<AcctSOASummaryDetailDTO> WebAcctSOASummSelect(string accountNo);
        Task<List<AcctSOASummaryDTO>> WebAcctSOASummList(string accountNo);
        Task<List<AcctSOATxnCategoryDTO>> WebAcctSOATxnCategoryList(string accountNo, string selectedStmtDate);
        Task<List<AcctSOATxnDTO>> WebAcctSOATxnList(string accountNo, string selectedStmtDate, string txnCode);
    }
    public class AccountSOAOpDAO :DAOBase, IAccountSOAOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public AccountSOAOpDAO(string connString)
        {
            _connectionString = connString;
        }

        /// <summary>
        /// Get application info
        /// </summary>
        /// <param name="issNo"></param>
        /// <param name="applId"></param>
        /// <returns></returns>
        public async Task<AcctSOASummaryDetailDTO> WebAcctSOASummSelect(string accountNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { accountNo };
                var paramNameList = new[]
                                   {
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<AcctSOASummaryDetailDTO>
                    (BuildSqlCommand("WebAcctSOASummSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();

                return result;
            }
        }
        /// <summary>
        /// Get application info
        /// </summary>
        /// <param name="issNo"></param>
        /// <param name="applId"></param>
        /// <returns></returns>
        public async Task<List<AcctSOASummaryDTO>> WebAcctSOASummList(string accountNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { accountNo };
                var paramNameList = new[]
                                   {
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<AcctSOASummaryDTO>
                    (BuildSqlCommand("WebAcctSOASummList", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                return result;
            }
        }
        /// <summary>
        /// Get AcctSOATxnCategoryList info
        /// </summary>
        /// <param name="accountNo,selectedStmtDate"></param>
        /// <returns>list of AcctSOATxnCategoryDTO</returns>
        public async Task<List<AcctSOATxnCategoryDTO>> WebAcctSOATxnCategoryList(string accountNo, string selectedStmtDate)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { accountNo, selectedStmtDate };
                var paramNameList = new[]
                                   {
                                        "@AcctNo",
                                        "@StmtDate"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<AcctSOATxnCategoryDTO>
                    (BuildSqlCommand("WebAcctSOATxnCategoryList", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                return result;
            }
        }
        /// <summary>
        /// Get list WebAcctSOATxnList 
        /// </summary>
        /// <param name="accountNo,selectedStmtDate,txnCode"></param>
        /// <returns>list of AcctSOATxnDTO</returns>
        /// 
        public async Task<List<AcctSOATxnDTO>> WebAcctSOATxnList(string accountNo, string selectedStmtDate, string txnCode)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { accountNo, selectedStmtDate, Convert.ToString(txnCode) };
                var paramNameList = new[]
                                   {
                                        "@AcctNo",
                                        "@StmtDate",
                                        "@TxnCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<AcctSOATxnDTO>
                    (BuildSqlCommand("WebAcctSOATxnList", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                return result;
            }
        }
    }
}

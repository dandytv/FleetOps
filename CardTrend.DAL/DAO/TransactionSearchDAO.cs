using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto.TransactionSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface ITransactionSearchDAO
    {
        Task<List<TransactionSearchDTO>> GetAccountTxnSearch(Int64? accountNo, Int64? cardNo, string transacionCategory, int txtCd, string fromDate, string toDate, string statementDate);
        Task<List<TransactionSearchDTO>> GetMerchTxnSearch(string bussinessLocation, string merchAcctNo, string txnCd, string fromtxnDate, string toTxnDate, string txnCat);
        Task<ObjectDetailDTO> GetObjectDetail(string Prefix, string Value);
    }
    public class TransactionSearchDAO : DAOBase,ITransactionSearchDAO
    {
        private readonly string _connectionString = string.Empty;
        public TransactionSearchDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<List<TransactionSearchDTO>> GetAccountTxnSearch(Int64? accountNo,Int64? cardNo,string transacionCategory,int txtCd,string fromDate,string toDate,string statementDate)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo != 0 ? accountNo.ToString() : null, cardNo != 0?cardNo.ToString() : null, transacionCategory, txtCd != 0?txtCd.ToString() : null, fromDate,toDate, statementDate };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo",
	                                    "@CardNo",
                                        "@TxnCategory",
                                        "@TxnCd",
                                        "@FromDate",
                                        "@ToDate",
                                        "@StatementDate"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var transactionResults = await cardtrendentities.Database.SqlQuery<TransactionSearchDTO>(BuildSqlCommand("WebAcctTxnSearch", paramCollection), paramCollection.ToArray()).ToListAsync();
                return transactionResults;
            }
        }
        public async Task<List<TransactionSearchDTO>> GetMerchTxnSearch(string bussinessLocation,string merchAcctNo,string txnCd,string fromtxnDate,string toTxnDate,string txnCat)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), bussinessLocation,merchAcctNo , txnCd, fromtxnDate, toTxnDate,txnCat};
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
	                                    "@BusnLocation",
	                                    "@MerchAcctNo",
                                        "@TxnCd",
                                        "@FrmTxnDate",
                                        "@ToTxnDate",
                                        "@TxnCat"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var merchPostedTxnSearch = await cardtrendentities.Database.SqlQuery<TransactionSearchDTO>(BuildSqlCommand("WebMerchTxnSearch", paramCollection), paramCollection.ToArray()).ToListAsync();
                return merchPostedTxnSearch;
            }
        }
        public async Task<ObjectDetailDTO> GetObjectDetail(string Prefix, string Value)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] {Prefix,Value};

                var paramNameList = new[]
                                   {
                                        "@ShortPrefix",
	                                    "@Value"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var merchPostedTxnSearch = await cardtrendentities.Database.SqlQuery<ObjectDetailDTO>(BuildSqlCommand("WebGetObjectDetail", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return merchPostedTxnSearch;
            }
        }
    }
}

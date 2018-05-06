using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.Corporate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface ICorporateOpDAO
    {
        Task<CorporateDTO> GetCorpAcctDetail(string CorpCd);
        Task<IList<CorporateDTO>> GetCorpAcctList();
        Task<IList<GeneralInfoDTO>> GetAcctCorpList(string corpCd);
        Task<int> SaveCorporateAcct(CorporateDTO corporate, string func);
    }
    public class CorporateOpDAO : DAOBase, ICorporateOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public CorporateOpDAO(string connString)
        {
            _connectionString = connString;
        }

        /// <summary>
        /// Tuan 
        /// </summary>
        /// <param name="milestone"></param>
        /// <returns></returns>
        public async Task<CorporateDTO> GetCorpAcctDetail(string CorpCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), CorpCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@CorpCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
               var result = await cardtrendentities.Database.SqlQuery<CorporateDTO>
                    (BuildSqlCommand("WebCorpAcctSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();
               return result;
            }
        }

        /// <summary>
        /// Tuan 
        /// </summary>
        /// <param name="milestone"></param>
        /// <returns></returns>
        public async Task<IList<CorporateDTO>> GetCorpAcctList()
        {
            var corporateLst = new List<CorporateDTO>();
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CorporateDTO>
                     (BuildSqlCommand("WebCorpAcctListSelect", paramCollection), paramCollection.ToArray())
                     .ToListAsync();
                return result;
            }
        }
        public async Task<IList<GeneralInfoDTO>> GetAcctCorpList(string corpCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), corpCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@CorpCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<GeneralInfoDTO>
                     (BuildSqlCommand("WebAcctCorpListSelect", paramCollection), paramCollection.ToArray())
                     .ToListAsync();
                return result;
            }
        }
        public async Task<int> SaveCorporateAcct(CorporateDTO corporate, string func)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
               var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@CorpCd", SqlDbType.VarChar) {SqlValue = (object)corporate.CorporateCode?? DBNull.Value},
                    new SqlParameter("@CorpName", SqlDbType.NVarChar) {SqlValue = (object) corporate.CorporateName?? DBNull.Value},
                    new SqlParameter("@TradeLimit", SqlDbType.Decimal) {SqlValue = corporate.TradeLimit},
                    new SqlParameter("@ComplexInd", SqlDbType.VarChar) {SqlValue = (object)corporate.ComplexAcctInd?? DBNull.Value},

                    new SqlParameter("@InvBillInd", SqlDbType.VarChar) {SqlValue = (object)corporate.InvoiceCenter?? DBNull.Value},
                    new SqlParameter("@PymtInd", SqlDbType.VarChar) {SqlValue = (object)corporate.PaymentCenter?? DBNull.Value},
                    new SqlParameter("@PersonInCharge", SqlDbType.VarChar) {SqlValue = (object)corporate.PersonInCharge?? DBNull.Value},
                    new SqlParameter("@UserFlag", SqlDbType.VarChar) {SqlValue = func},
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)corporate.UserId?? DBNull.Value},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

               await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebCorpAcctMaint @IssNo,@CorpCd,@CorpName,@TradeLimit,@ComplexInd," +
                                                          "@InvBillInd,@PymtInd,@PersonInCharge,@UserId,@UserFlag", parameters);
               var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
               return Convert.ToInt32(resultCode);
            }
        }
    }
}

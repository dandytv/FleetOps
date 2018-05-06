using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.PinMailer;
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
    public interface IPinMailerOpDAO
    {
        Task<IList<PinMailerBatchDTO>> GetPinMailerBatch();
        Task<List<PinMailerBatchViewDTO>> GetPinMailerBatchView(long batchId, int status);
        Task<int> SavePinMailerPrint(int batchId, List<long> cardList);
    }
    public class PinMailerOpDAO : DAOBase,IPinMailerOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public PinMailerOpDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<IList<PinMailerBatchDTO>> GetPinMailerBatch()
        {
            var corporateLst = new List<PinMailerBatchDTO>();
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<PinMailerBatchDTO>
                     (BuildSqlCommand("PINMailerGetBatch", paramCollection), paramCollection.ToArray())
                     .ToListAsync();
                return result;
            }
        }
        public async Task<List<PinMailerBatchViewDTO>> GetPinMailerBatchView(long batchId,int status)
        {
            var corporateLst = new List<PinMailerBatchViewDTO>();
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { batchId, status };
                var paramNameList = new[]
                                   {
                                        "@BatchId",
                                        "@Ind"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<PinMailerBatchViewDTO>
                  (BuildSqlCommand("WebPINMailerListSelect", paramCollection), paramCollection.ToArray())
                  .ToListAsync();
                return result;
            }
        }
        public async Task<int> SavePinMailerPrint(int batchId,List<long> cardList)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@BatchId", batchId);
                DataTable dt = new DataTable();
                dt.Columns.Add("BatchId");
                dt.Columns.Add("CardNo", typeof(long));
                dt.Columns.Add("Sts");
                for (int i = 0; i < cardList.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["BatchId"] = batchId;
                    dr["CardNo"] = cardList[i];
                    dr["Sts"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }
                Parameters[1] = new SqlParameter("@PINMailer", dt);
                Parameters[1].SqlDbType = SqlDbType.Structured;
                Parameters[2] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[2].Direction = ParameterDirection.ReturnValue;

                var parameters = new[] { 
                    new SqlParameter("@BatchId", SqlDbType.Int) {SqlValue = batchId},      
                    new SqlParameter("@PINMailer",SqlDbType.Structured) {SqlValue = dt,TypeName ="PINMailer"},                 
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };
                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebPINMailerMaint @BatchId,@PINMailer", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
    }
}

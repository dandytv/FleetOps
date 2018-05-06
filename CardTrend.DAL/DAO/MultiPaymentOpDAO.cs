using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.ControlList;
using CardTrend.Domain.Dto.MultiplePayment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.DAL.DAO
{
    public interface IMultiPaymentOpDAO
    {
        Task<IList<MultiPaymentDTO>> WebMultiPaymentListSelect();
        Task<IList<MultiPaymentDTO>> WebMultiPaymentSelect(string batchId, MultiPaymentDTO multiPayment);
        Task<List<GLCodeDTO>> WebGetGLCode(string txnCode, string settlement, string acctNo);
        Task<IEnumerable<SelectListItem>> WebGetPymtTxnCd(string GlSettlementCd, string TxnCat);
        Task<IssMessageDTO> WebMultiPaymentMaint(TxnAdjustmentDTO txtAdjustment);
    }
    public class MultiPaymentOpDAO : DAOBase, IMultiPaymentOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public MultiPaymentOpDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<IList<MultiPaymentDTO>> WebMultiPaymentListSelect()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<MultiPaymentDTO>
                    (BuildSqlCommand("WebMultiPaymentListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                return results;
            }
        }
        public async Task<IList<MultiPaymentDTO>> WebMultiPaymentSelect(string batchId,MultiPaymentDTO multiPayment)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] {Common.Helpers.Common.GetIssueNo(),batchId, multiPayment.RefKey};
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@BatchId",
                                        "@RefKey"
                                   };
                var outPutParameter = new object[] { new ColumnInfo { FieldName = "@TxnAmt", DataType = "money" } };
                var paramCollection = BuildParameterListWithOutPutAndRrn(parameters, outPutParameter, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MultiPaymentDTO>
                  (BuildSqlCommand("WebMultiPaymentSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return result;
            }
        }
        public async Task<List<GLCodeDTO>> WebGetGLCode(string txnCode, string settlement, string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { txnCode, acctNo, 1000 };
                var paramNameList = new[]
                                   {
                                        "@TxnCd",                                    
                                        "@AcctNo",
                                        "@SettleVal"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<GLCodeDTO>
                  (BuildSqlCommand("WebGetGLCode", paramCollection), paramCollection.ToArray())
                  .ToListAsync();
                return results;
            }
        }
        public async Task<IEnumerable<SelectListItem>> WebGetPymtTxnCd(string GlSettlementCd, string TxnCat)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), TxnCat, GlSettlementCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",                                    
                                        "@TxnCat",
                                        "@settleval"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<TxnCodeDTO>
                             (BuildSqlCommand("WebGetPymtTxnCd", paramCollection), paramCollection.ToArray())
                             .ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.TxnCd.ToString()
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IssMessageDTO> WebMultiPaymentMaint(TxnAdjustmentDTO txtAdjustment)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                DataTable dt = new DataTable();
                dt.Columns.Add("TxnId");
                dt.Columns.Add("AcctNo", typeof(long));
                dt.Columns.Add("PymtAmt", typeof(decimal));
                dt.Columns.Add("Descp");
                foreach (var item in txtAdjustment.multipleTxnRecord)
                {
                    DataRow dr = dt.NewRow();
                    dr["AcctNo"] = item.AcctNo;
                    if (!string.IsNullOrEmpty(item.TxnAmt))
                    {
                        dr["PymtAmt"] = CardTrend.Common.Extensions.NumberExtensions.ConvertDecimalToDb(item.TxnAmt);
                    }
                    else
                    {
                        dr["PymtAmt"] = DBNull.Value;
                    }
                    dr["Descp"] = item.TxnDescp;
                    dr["TxnId"] = item.TxnId;
                    dt.Rows.Add(dr);
                }

                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@BatchId", SqlDbType.Int) {SqlValue = txtAdjustment.BatchId},
                    new SqlParameter("@RefKey", SqlDbType.BigInt) {SqlValue = (object)txtAdjustment.RefId?? DBNull.Value},
                    new SqlParameter("@ChequeNo", SqlDbType.BigInt) {SqlValue = (object)(Convert.ToUInt64(txtAdjustment.ChequeNo))?? DBNull.Value},
                    new SqlParameter("@TxnCd", SqlDbType.VarChar) {SqlValue = (object)Convert.ToString(txtAdjustment.TxnCd)?? DBNull.Value},
                    new SqlParameter("@TxnDate", SqlDbType.Date) {SqlValue = (object)txtAdjustment.TxnDate?? DBNull.Value},
                    new SqlParameter("@DueDate", SqlDbType.Date) {SqlValue = (object)txtAdjustment.DueDate?? DBNull.Value},
                    new SqlParameter("@TtlPymt", SqlDbType.Money) {SqlValue = (object)Convert.ToDecimal(txtAdjustment.ChequeAmt)?? DBNull.Value},
                    new SqlParameter("@SlipNo", SqlDbType.VarChar) {SqlValue = (object)txtAdjustment.SlipNo?? DBNull.Value},
                    new SqlParameter("@IssueingBankCd", SqlDbType.VarChar) {SqlValue = (object)txtAdjustment.IssueingBank?? DBNull.Value},
                    new SqlParameter("@Payment",SqlDbType.Structured){SqlValue = dt,TypeName ="Payment"} ,
                    new SqlParameter("@Owner", SqlDbType.VarChar) {SqlValue = (object)txtAdjustment.Owner?? DBNull.Value},
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)txtAdjustment.UserId?? DBNull.Value},
                    new SqlParameter("@SettleVal", SqlDbType.VarChar) {SqlValue = (object)txtAdjustment.SelectedGLSettlement?? DBNull.Value},
                    new SqlParameter("@Batch", SqlDbType.VarChar,20) {Direction = ParameterDirection.Output},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebMultiPaymentMaint @IssNo,@BatchId,@RefKey,@ChequeNo,@TxnCd," +
                                           "@TxnDate,@DueDate,@TtlPymt,@SlipNo,@IssueingBankCd,@Payment,@Owner,@UserId,@SettleVal,@Batch OUT", parameters);

                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var batchId = parameters.Where(x => x.ParameterName == "@Batch").FirstOrDefault().Value;
                ResourceManager myManager = new ResourceManager(typeof(CardTrend.Common.Resources.IssMessages));
                issMessage.Descp = myManager.GetString("Msg" + resultCode);
                issMessage.Flag = Convert.ToInt32(resultCode);
                issMessage.paraOut.BatchId = Convert.ToString(batchId);
                return issMessage;
            }
        }

    }
}

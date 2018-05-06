using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.MerchantMultiAdjustment;
using CardTrend.Domain.Dto.MultiplePayment;
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
    public interface IMerchMultitxnAdjustmentDAO
    {
        Task<List<MerchantMultiTxnAdjustmentDTO>> MerchantMultiTxnAdjustmentListSelect();
        Task<List<TxnAdjustmentDetailDTO>> MerchantMultiTxnAdjustmentSelect(string invoiceNo, string batchId);
        Task<IssMessageDTO> SaveMerchantMultiTxnAdjustmentMaint(TxnAdjustmentDTO adjustmentDetail, string userId);
        Task<List<MultiPaymentGLCodeDTO>> GetGLCode(string adjTxnCode);
    }
    public class MerchMultitxnAdjustmentDAO : DAOBase, IMerchMultitxnAdjustmentDAO
    {

        private readonly string _connectionString = string.Empty;
        public MerchMultitxnAdjustmentDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<List<MerchantMultiTxnAdjustmentDTO>> MerchantMultiTxnAdjustmentListSelect()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var txnAdjustments = await cardtrendentities.Database.SqlQuery<MerchantMultiTxnAdjustmentDTO>
                 (BuildSqlCommand("WebMerchantMultiTxnAdjustmentListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();

                return txnAdjustments;
            }
        }
        public async Task<List<TxnAdjustmentDetailDTO>> MerchantMultiTxnAdjustmentSelect(string invoiceNo,string batchId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), batchId, invoiceNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@OrigBatchId",
                                        "@InvoiceNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var txnAdjustmentDetail = await cardtrendentities.Database.SqlQuery<TxnAdjustmentDetailDTO>(BuildSqlCommand("WebMerchantMultiTxnAdjustmentSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return txnAdjustmentDetail;
            }
        }
        public async Task<IssMessageDTO> SaveMerchantMultiTxnAdjustmentMaint(TxnAdjustmentDTO adjustmentDetail,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                DataTable dt = new DataTable();
                dt.Columns.Add("Acctno", typeof(long));
                dt.Columns.Add("Cardno");
                dt.Columns.Add("TxnAmt", typeof(decimal));
                dt.Columns.Add("Descp");
                dt.Columns.Add("InvoiceNo");
                dt.Columns.Add("AppvCd");
                dt.Columns.Add("DeftBusnlocation");
                dt.Columns.Add("DeftTermId");
                dt.Columns.Add("Owner");
                dt.Columns.Add("Sts");
                dt.Columns.Add("TxnDate");
                dt.Columns.Add("DueDate");
                dt.Columns.Add("TxnCd");
                dt.Columns.Add("TxnId");
                foreach (var item in adjustmentDetail.multipleTxnRecord)
                {
                    DataRow dr = dt.NewRow();
                    dr["DeftBusnlocation"] = item.MerchantAcctNo;
                    dr["TxnAmt"] = (object)NumberExtensions.ConvertDecimalToDb(item.TxnAmt) ?? DBNull.Value;
                    dr["Descp"] = (object)item.Descp ?? DBNull.Value;
                    dr["TxnCd"] = adjustmentDetail.TxnCd;
                    dr["TxnDate"] = adjustmentDetail.TxnDate.ToShortDateString();
                    dr["DueDate"] = adjustmentDetail.DueDate == DateTime.MinValue ? DBNull.Value : NumberExtensions.ConvertDatetimeDB(adjustmentDetail.DueDate.ToShortDateString());
                    dr["Owner"] = adjustmentDetail.Owner;
                    dr["InvoiceNo"] = (object)adjustmentDetail.InvoiceNo ?? DBNull.Value;
                    dr["TxnId"] = (object)adjustmentDetail.TxnId ?? DBNull.Value;
                    dt.Rows.Add(dr);
                }

                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@CheqAmt", SqlDbType.Money) {SqlValue = NumberExtensions.ConvertDecimalToDb(adjustmentDetail.ChequeAmt)},
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)userId?? DBNull.Value},
                    new SqlParameter("@Adjustment", SqlDbType.Structured) {SqlValue = dt,TypeName ="Adjustment"},
                    new SqlParameter("@RetCd", SqlDbType.Int) {Direction = ParameterDirection.Output},
                    new SqlParameter("@RcptNo", SqlDbType.VarChar) {SqlValue = (object)adjustmentDetail.InvoiceNo?? DBNull.Value},
                    new SqlParameter("@BatchId", SqlDbType.Int) {SqlValue = (object)adjustmentDetail.BatchId?? DBNull.Value},
                    new SqlParameter("@BatchOut", SqlDbType.VarChar,25) {Direction = ParameterDirection.Output},
                    new SqlParameter("@Owner", SqlDbType.VarChar) {SqlValue = (object)adjustmentDetail.Owner?? DBNull.Value},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebMerchantMultiTxnAdjustmentMaint @IssNo,@CheqAmt,@UserId,@Adjustment,@RetCd OUT," +
                           "@RcptNo,@BatchId,@BatchOut OUT,@Owner", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                ResourceManager myManager = new ResourceManager(typeof(CardTrend.Common.Resources.IssMessages));
                issMessage.paraOut.BatchId = parameters.Where(x => x.ParameterName == "@BatchOut").FirstOrDefault().Value.ToString();
                issMessage.paraOut.RetCd = parameters.Where(x => x.ParameterName == "@RetCd").FirstOrDefault().Value.ToString();
                issMessage.Descp = myManager.GetString("Msg" + resultCode);
                issMessage.Flag = Convert.ToInt32(resultCode);
                return issMessage;

            }
        }
        public async Task<List<MultiPaymentGLCodeDTO>> GetGLCode(string adjTxnCode)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { adjTxnCode };
                var paramNameList = new[]
                                   {
                                        "@TxnCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var multiPaymentGLCodes = await cardtrendentities.Database.SqlQuery<MultiPaymentGLCodeDTO>
                                     (BuildSqlCommand("WebGetGlCode", paramCollection), paramCollection.ToArray()).ToListAsync();

                return multiPaymentGLCodes;
            }
        }
    }
}

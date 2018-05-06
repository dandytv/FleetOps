using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.MultipleAdjustment;
using CardTrend.Domain.Dto.MultiplePayment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface IMultipleTxnOpDAO
    {
        Task<List<TxnMultipleAdjustmentDTO>> WebMultiTxnAdjustmentListSelect();
        Task<List<TxnAdjustmentDTO>> WebMultiTxnAdjustmentSelect(string BatchId, string ChequeNo);
        Task<IssMessageDTO> ftMultipleAdjMaint(TxnAdjustmentDTO txtAdjustment);
        Task<List<GLCodeDTO>> WebGetGLCode(string txnCode, string settlement, string acctNo);
    }
    public class MultipleTxnOpDAO : DAOBase, IMultipleTxnOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public MultipleTxnOpDAO(string connString)
        {
            _connectionString = connString;
        }
        /// <author>
        /// Tuan
        /// </author>
        /// <param date="6/03/2017"></param>
        /// <param name="no"></param>
        /// <returns> list of TxnAdjustmentDTO</returns>
        public async Task<List<TxnMultipleAdjustmentDTO>> WebMultiTxnAdjustmentListSelect()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo()};
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<TxnMultipleAdjustmentDTO>
                    (BuildSqlCommand("WebMultiTxnAdjustmentListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();
                return results;
            }
        }
        /// <author>
        /// Tuan
        /// </author>
        /// <param date="6/03/2017"></param>
        /// <param name="batchId,chequeNo"></param>
        /// <returns> TxnAdjustmentDTO</returns>
        public async Task<List<TxnAdjustmentDTO>> WebMultiTxnAdjustmentSelect(string BatchId, string ChequeNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), BatchId, ChequeNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@BatchId",
                                        "@Rrn"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<TxnAdjustmentDTO>
                 (BuildSqlCommand("WebMultiTxnAdjustmentSelect", paramCollection), paramCollection.ToArray()).ToListAsync();

                return result;
            }
        }
        /// <author>
        /// Tuan
        /// </author>
        /// <param date="6/03/2017"></param>
        /// <param name="TxnAdjustmentDTO object"></param>
        /// <returns> IssMessageDTO</returns>
        public async Task<IssMessageDTO> ftMultipleAdjMaint(TxnAdjustmentDTO txtAdjustment)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Acctno");
                dt.Columns.Add("Cardno", typeof(string));//
                dt.Columns.Add("TxnAmt");
                dt.Columns.Add("Descp");
                dt.Columns.Add("InvoiceNo");
                dt.Columns.Add("AppvCd");
                dt.Columns.Add("DeftBusnLocation");
                dt.Columns.Add("DeftTermId");
                dt.Columns.Add("Owner");
                dt.Columns.Add("Sts");
                dt.Columns.Add("TxnDate");
                dt.Columns.Add("DueDate");
                dt.Columns.Add("TxnCd");
                dt.Columns.Add("TxnId");

                foreach (var item in txtAdjustment.multipleTxnRecord)
                {
                    DataRow dr = dt.NewRow();
                    dr["AcctNo"] = item.AcctNo;
                    dr["Cardno"] = item.CardNo;

                    if (!string.IsNullOrEmpty(item.TxnAmt))
                    {
                        dr["TxnAmt"] = CardTrend.Common.Extensions.NumberExtensions.ConvertDecimalToDb(item.TxnAmt);
                    }
                    else
                    {
                        dr["TxnAmt"] = DBNull.Value;
                    }

                    dr["Descp"] = item.TxnDescp;
                    dr["InvoiceNo"] = item.InvoiceNo;
                    dr["AppvCd"] = item.AppvCd;
                    dr["DeftBusnLocation"] = item.DeftBusnLocation;
                    dr["DeftTermId"] = item.DeftTermId;
                    dr["Owner"] = txtAdjustment.Owner;
                    dr["Sts"] = item.SelectedSts;
                    dr["TxnDate"] = txtAdjustment.TxnDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    dr["DueDate"] = DBNull.Value;
                    dr["TxnCd"] = txtAdjustment.AdjTxnCd;
                    dr["TxnId"] = item.TxnId;
                    dt.Rows.Add(dr);
                }


                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@CheqAmt", SqlDbType.Money) {SqlValue = txtAdjustment.ChequeAmt},
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)txtAdjustment.UserId?? DBNull.Value},
                    new SqlParameter("@Adjustment",SqlDbType.Structured) {SqlValue = dt,TypeName ="Adjustment"},
                    new SqlParameter("@RetCd", SqlDbType.Int) {Direction = ParameterDirection.Output},
                    new SqlParameter("@RcptNo", SqlDbType.VarChar) {SqlValue = (object)txtAdjustment.ChequeNo?? DBNull.Value},
                    new SqlParameter("@BatchId", SqlDbType.Int) {SqlValue = (object)txtAdjustment.BatchId?? DBNull.Value},
                    new SqlParameter("@Batch", SqlDbType.VarChar,25) {Direction = ParameterDirection.Output},                   
                    new SqlParameter("@Owner", SqlDbType.VarChar) {SqlValue = (object)txtAdjustment.Owner?? DBNull.Value},                    
                    new SqlParameter("@RcptOut", SqlDbType.Int) {Direction = ParameterDirection.Output},                  
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebMultiTxnAdjustmentMaint @IssNo,@CheqAmt,@UserId,@Adjustment,@RetCd OUT," +
                                           "@RcptNo,@BatchId,@Batch OUT,@Owner,@RcptOut OUT", parameters);

                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var batchId = parameters.Where(x => x.ParameterName == "@Batch").FirstOrDefault().Value;
                var RetCd = parameters.Where(x => x.ParameterName == "@RetCd").FirstOrDefault().Value;
                var ChequeNo = parameters.Where(x => x.ParameterName == "@RcptOut").FirstOrDefault().Value;
                ResourceManager myManager = new ResourceManager(typeof(CardTrend.Common.Resources.IssMessages));
                string strMessage = myManager.GetString("Msg" + resultCode);

                return new IssMessageDTO() { Descp = strMessage, Flag = NumberExtensions.getFlagCode(resultCode), paraOut = new ReturnObject { BatchId = Convert.ToString(batchId), RetCd = Convert.ToString(RetCd), ChequeNo = Convert.ToString(ChequeNo) } };
            }
        }
        public async Task<List<GLCodeDTO>> WebGetGLCode(string txnCode, string settlement, string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { txnCode, acctNo, settlement };
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
    }
}

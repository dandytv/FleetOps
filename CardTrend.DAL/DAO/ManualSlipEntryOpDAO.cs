using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.ManualSlipEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface IManualSlipEntryOpDAO
    {
       List<ManualSlipEntryDTO> GetManualSlipEntryBatchList();
       Task<IList<MerchManualTxnDTO>> GetManualSlipEntryTxnList(string SettleId);
       Task<ManualSlipEntryBatchDetailDTO> GetManualSlipEntryBatchDetail(string SettleId);
       Task<MerchManualTxnDTO> GetMerchManualTxnProductDetail(string TxnId, string TxnDetailId);
       Task<int> SaveMerchManualBatch(ManualSlipEntryBatchDetailDTO manualSlipEntry);
       Task<IssMessageDTO> SaveManualSlipEntry(MerchManualTxnDTO merchmanualTxn);
       Task<int> DeleteMerchManualTransaction(string batchId, string settleId, string txnId, string detailTxnId);
       Task<IssMessageDTO> SaveMerchManualTxnProduct(MerchManualTxnDTO manualSlipEntry);
       List<ManualTxnProductDTO> GetManualTxnProductList(string txnId);
       MerchManualTxnDTO GetManualSlipEntryTxnDetail(string TxnId);
       ManualTxnDTO GetManualTxn(string settleId);
    }
    public class ManualSlipEntryOpDAO : DAOBase, IManualSlipEntryOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public ManualSlipEntryOpDAO(string connString)
        {
            _connectionString = connString;
        }
        /// <author>
        /// Tuan
        /// </author>
        /// <param date="27/02/2017"></param>
        /// <param name="no"></param>
        /// <returns> list of ManualSlipEntryDTO</returns>
        public List<ManualSlipEntryDTO> GetManualSlipEntryBatchList()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@AcqNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results =  cardtrendentities.Database.SqlQuery<ManualSlipEntryDTO>
                    (BuildSqlCommand("WebMerchManualBatchListSelect", paramCollection), paramCollection.ToArray())
                    .ToList();

                return results;
            }
        }
        /// <author>
        /// Tuan
        /// </author>
        /// <param date="27/02/2017"></param>
        /// <param name="SettleId"></param>
        /// <returns> list of MerchManualTxnDTO</returns>
        public async Task<IList<MerchManualTxnDTO>> GetManualSlipEntryTxnList(string SettleId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), SettleId };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@SettleId"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MerchManualTxnDTO>
                            (BuildSqlCommand("WebMerchManualTxnListSelect", paramCollection), paramCollection.ToArray())
                            .ToListAsync();
                return result;
            }
        }
        /// <author>
        /// Tuan
        /// </author>
        /// <param date="28/02/2017"></param>
        /// <param name="SettleId"></param>
        /// <returns> ManualSlipEntryBatchDetailDTO</returns>
        public async Task<ManualSlipEntryBatchDetailDTO> GetManualSlipEntryBatchDetail(string SettleId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), SettleId };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@SettleId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<ManualSlipEntryBatchDetailDTO>
                         (BuildSqlCommand("WebMerchManualBatchSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        /// <author>
        /// Tuan
        /// </author>
        /// <param date="28/02/2017"></param>
        /// <param name="ManualSlipEntryBatchDetailDTO"></param>
        /// <returns> string</returns>
        public async Task<int> SaveMerchManualBatch(ManualSlipEntryBatchDetailDTO manualSlipEntry)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(),manualSlipEntry.SettleId, manualSlipEntry.Dealer, manualSlipEntry.SiteId, manualSlipEntry.TermId, manualSlipEntry.TxnCd
                                                ,manualSlipEntry.InvoiceNo, manualSlipEntry.BatchId,manualSlipEntry.OrigBatchNo,manualSlipEntry.Descp,manualSlipEntry.SettleDate
                                                ,manualSlipEntry.Sts,manualSlipEntry.UserId };
                var paramNameList = new[]
                                    {
                                        "@AcqNo",
	                                    "@SettleId",
                                        "@BusnLocation",
                                        "@SiteId",
                                        "@TermId",
                                        "@TxnCd",
                                        "@InvoiceNo",
                                        "@BatchId",
                                        "@OrigBatchNo",
                                        "@Descp",
                                        "@SettleDate",
                                        "@Sts",
                                        "@UserId"
                                    };
                var outPutParameter = new object[] { new ColumnInfo { FieldName = "@oBatchId", DataType = "int", ColLength = 32 }, new ColumnInfo { FieldName = "@oSettleId", DataType = "varchar", ColLength = 19 } };

                var paramCollection = BuildParameterListWithOutPutAndRrn(parameters, outPutParameter, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync
                    (BuildSqlCommandWithRrn("WebMerchManualBatchMaint", paramCollection), paramCollection.ToArray());

                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;

                return Convert.ToInt32(resultCode);
            }
        }
        /// <author>
        /// Tuan
        /// </author>
        /// <param date="1/03/2017"></param>
        /// <param name="ManualSlipEntryBatchDetailDTO"></param>
        /// <returns> string</returns>
        public async Task<IssMessageDTO> SaveManualSlipEntry(MerchManualTxnDTO merchmanualTxn)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(),merchmanualTxn.TxnCd, merchmanualTxn.Dealer, merchmanualTxn.TerminalId, merchmanualTxn.SiteId, merchmanualTxn.SettleId
                                                ,merchmanualTxn.TxnId, merchmanualTxn.ReceiptNo,merchmanualTxn.InvoiceNo,merchmanualTxn.Stan,merchmanualTxn.CardNo
                                                ,merchmanualTxn.CardExpiry,merchmanualTxn.DriverCard,merchmanualTxn.AuthCardExp,NumberExtensions.ConvertIntToDb(merchmanualTxn.DriverCd),
                                                 merchmanualTxn.TxnDate,NumberExtensions.ConvertIntToDb(merchmanualTxn.ArrayCount),merchmanualTxn.Quantity,
                                                merchmanualTxn.TotalAmt,merchmanualTxn.Description,merchmanualTxn.OdometerReading,merchmanualTxn.Rrn,merchmanualTxn.AuthNo,
                                                merchmanualTxn.Sts,merchmanualTxn.UserId,merchmanualTxn.VATNo};

                var paramNameList = new[]
                                    {
                                        "@AcqNo",
	                                    "@TxnCd",
	                                    "@BusnLocation",
                                        "@TermId",
                                        "@SiteId",
                                        "@SettleId",
                                        "@TxnId",
                                        "@RcptNo",
                                        "@InvoiceNo",
                                        "@Stan",
                                        "@CardNo",
                                        "@CardExp",
                                        "@AuthCardNo",
                                        "@AuthCardExp",
                                        "@DriverCd",
                                        "@TxnDate",
                                        "@ArrayCnt",
                                        "@Qty",
                                        "@Amt",
                                        "@Descp",
                                        "@Odometer",
                                        "@Rrn",
                                        "@AuthNo",
                                        "@Sts",
                                        "@UserId",
                                        "@VATNo"
                                    };
                var outPutParameter = new object[] { new ColumnInfo { FieldName = "@oTxnId", DataType = "varchar", ColLength = 19 }, new ColumnInfo { FieldName = "@oSettleId", DataType = "varchar", ColLength = 19 } };

                var paramCollection = BuildParameterListWithOutPutAndRrn(parameters, outPutParameter, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync
                    (BuildSqlCommandWithRrn("WebMerchManualTxnMaint", paramCollection), paramCollection.ToArray());

                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var txnId = paramCollection.Where(x => x.ParameterName == "@oTxnId").FirstOrDefault().Value;
                var settleId = paramCollection.Where(x => x.ParameterName == "@oSettleId").FirstOrDefault().Value;
                return new IssMessageDTO() { Descp = string.Empty, Flag = Convert.ToInt32(resultCode), paraOut = new ReturnObject { TxnId = Convert.ToString(txnId), SettleId = Convert.ToString(settleId) } };
            }
        }
        /// <author>
        /// Tuan
        /// </author>
        /// <param date="1/03/2017"></param>
        /// <param name="TxnId"></param>
        /// <returns> MerchManualTxnDTO</returns>
        public MerchManualTxnDTO GetManualSlipEntryTxnDetail(string TxnId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), TxnId };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@TxnId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result =  cardtrendentities.Database.SqlQuery<MerchManualTxnDTO>(BuildSqlCommand("WebMerchManualTxnSelect", paramCollection), paramCollection.ToArray()).FirstOrDefault();
                return result;
            }
        }
        public async Task<int> DeleteMerchManualTransaction(string batchId, string settleId, string txnId, string detailTxnId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), batchId, settleId, txnId, detailTxnId };
                var paramNameList = new[]{"@AcqNo","@BatchId","@SettleId","@TxnId","@DetailTxnId"};
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebMerchManualTxnDelete", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode); ;
            }
        }
        public ManualTxnDTO GetManualTxn(string settleId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(),settleId };
                var paramNameList = new[]{"@AcqNo","@SettleId"};
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = cardtrendentities.Database.SqlQuery<ManualTxnDTO>(BuildSqlCommand("WebGetManualTxn", paramCollection), paramCollection.ToArray()).FirstOrDefault();
                return result;
            }
        }
        public List<ManualTxnProductDTO> GetManualTxnProductList(string txnId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), txnId };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@TxnId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result =  cardtrendentities.Database.SqlQuery<ManualTxnProductDTO>
                         (BuildSqlCommand("WebMerchManualTxnDetailListSelect", paramCollection), paramCollection.ToArray()).ToList();
                return result;
            }
        }
        public async Task<IssMessageDTO> SaveMerchManualTxnProduct(MerchManualTxnDTO manualSlipEntry)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), manualSlipEntry.SettleId, manualSlipEntry.TxnId, manualSlipEntry.TxnDetailId, manualSlipEntry.ProdCd, manualSlipEntry.Quantity,
                                              manualSlipEntry.ProdAmt,manualSlipEntry.Description,manualSlipEntry.UnitPrice,manualSlipEntry.UserId,manualSlipEntry.VATAmt,manualSlipEntry.VATCd};
                var paramNameList = new[]
                                    {
                                        "@AcqNo",
                                        "@SettleId",
                                        "@TxnId",
                                        "@TxnDetailId",
                                        "@ProdCd",
                                        "@Qty",
                                        "@AmtPts",
                                        "@Descp",
                                        "@UnitPrice",
                                        "@UserId",
                                        "@VATAmt",
                                        "@VATCd"
                                    };
                var outPutParameter = new object[] { new ColumnInfo { FieldName = "@oTxnDetailId", DataType = "varchar", ColLength = 19 } };
                var paramCollection = BuildParameterListWithOutPutAndRrn(parameters, outPutParameter, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync
                      (BuildSqlCommandWithRrn("WebMerchManualTxnDetailMaint", paramCollection), paramCollection.ToArray());

                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var oTxnDetailId = paramCollection.Where(x => x.ParameterName == "@oTxnDetailId").FirstOrDefault().Value;
                issMessage.Flag = Convert.ToInt32(resultCode);
                issMessage.paraOut.TxnDetailId = Convert.ToString(oTxnDetailId);
                return issMessage;
            }
        }
        public async Task<MerchManualTxnDTO> GetMerchManualTxnProductDetail(string TxnId, string TxnDetailId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), TxnId, TxnDetailId };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@TxnId",
                                        "@TxnDetailId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MerchManualTxnDTO>
                          (BuildSqlCommand("WebMerchManualTxnDetailSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
    }
}

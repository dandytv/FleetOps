using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CCMS;
using ModelSector;
using Utilities.DAL;
using System.Data.SqlClient;
using System.Data;
using CCMS.ModelSector;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class ManualSlipEntryOps : BaseClass
    {
        public string BatchId { get; set; }
        public string SettleId { get; set; }
        public string TxnId { get; set; }
        public string TxnDetailId { get; set; }

        #region "ManualBatch"
        public List<ManualSlipEntry> GetManualSlipEntryBatchList()
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                var execResult = objDataEngine.ExecuteCommand("WebMerchManualBatchListSelect", CommandType.StoredProcedure, Parameters);
                var _ManualSlipEntryBatList = new List<ManualSlipEntry>();
                while (execResult.Read())
                {
                    _ManualSlipEntryBatList.Add(new ManualSlipEntry
                    {
                        BusnLocation = Convert.ToString(execResult["Dealer"]),//
                        SelectedTermId = Convert.ToString(execResult["Terminal Id"]),//
                        SiteId = Convert.ToString(execResult["Site Id"]),
                        BatchId = Convert.ToString(execResult["Batch Id"]),//
                        InvoiceNo = ConvertInt(execResult["Invoice No"]),//
                        SettleDate = DateConverter(execResult["Settle Date"]),//
                        TotalCnt = ConvertInt(execResult["Total Count"]),//
                        TotalAmt = ConverterDecimal(execResult["Total Amount"]),//
                        DisplayTotalAmt = ConverterDecimal(execResult["Total Amount"]),
                        Descp = Convert.ToString(execResult["Description"]),//
                        SelectedSts = Convert.ToString(execResult["Status"]),//

                        TxnDescp = Convert.ToString(execResult["Txn Description"]),//
                        SettleId = Convert.ToString(execResult["SettleId"]),

                        _CreationDatenUserId = new CreationDatenUserId
                        {
                            CreationDate = DateConverter(execResult["Creation Date"]),//
                            UserId = Convert.ToString(execResult["User Id"])//
                        }
                    });

                };
                return _ManualSlipEntryBatList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }



        }
        public async Task<ManualSlipEntry> GetManualSlipEntryBatchDetail(string SettleId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = String.IsNullOrEmpty(SettleId) ? new SqlParameter("@SettleId", DBNull.Value) : new SqlParameter("@SettleId", SettleId);

                var execResult = objDataEngine.ExecuteCommand("WebMerchManualBatchSelect", CommandType.StoredProcedure, Parameters);

                while (execResult.Read())
                {
                    var _ManualSlipEntryBatDetail = new ManualSlipEntry
                    {
                        BusnLocation = Convert.ToString(execResult["Dealer"]),//
                        SelectedTermId = Convert.ToString(execResult["TermId"]),//
                        SiteId = Convert.ToString(execResult["SiteId"]),
                        SettleId = Convert.ToString(execResult["Settle Id"]),
                        BatchId = Convert.ToString(execResult["BatchId"]),//

                        SelectedTxnCd = ConvertInt(execResult["TxnCd"]),
                        InvoiceNo = ConvertInt(execResult["InvoiceNo"]),//
                        SettleDate = DateConverter(execResult["SettleDate"]),//
                        TotalCnt = ConvertInt(execResult["Cnt"]),//
                        DisplayTotalAmt = ConverterDecimal(execResult["Amt"]),//
                        Descp = Convert.ToString(execResult["Descp"]),
                        OrigBatchNo = ConvertInt(execResult["OrigBatchNo"]),
                        Sts = await BaseClass.WebGetRefLib("MerchBatchSts"),
                        SelectedSts = Convert.ToString(execResult["Sts"]),
                    };
                    return _ManualSlipEntryBatDetail;

                };
                return new ManualSlipEntry();

            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> SaveMerchManualBatch(ManualSlipEntry _ManualSlipEntry)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[16];
                SqlCommand cmd = new SqlCommand();

                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = String.IsNullOrEmpty(_ManualSlipEntry.SettleId) ? new SqlParameter("@SettleId", DBNull.Value) : new SqlParameter("@SettleId", _ManualSlipEntry.SettleId);
                Parameters[2] = String.IsNullOrEmpty(_ManualSlipEntry.BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", _ManualSlipEntry.BusnLocation);
                Parameters[3] = String.IsNullOrEmpty(_ManualSlipEntry.SiteId) ? new SqlParameter("@SiteId", DBNull.Value) : new SqlParameter("@SiteId", _ManualSlipEntry.SiteId);
                Parameters[4] = String.IsNullOrEmpty(_ManualSlipEntry.SelectedTermId) ? new SqlParameter("@TermId", DBNull.Value) : new SqlParameter("@TermId", _ManualSlipEntry.SelectedTermId);
                Parameters[5] = new SqlParameter("@TxnCd", ConvertIntToDb(_ManualSlipEntry.SelectedTxnCd));
                Parameters[6] = new SqlParameter("@InvoiceNo ", ConvertIntToDb(_ManualSlipEntry.InvoiceNo));
                Parameters[7] = String.IsNullOrEmpty(_ManualSlipEntry.BatchId) ? new SqlParameter("@BatchId", DBNull.Value) : new SqlParameter("@BatchId", _ManualSlipEntry.BatchId);
                Parameters[8] = new SqlParameter("@OrigBatchNo", ConvertLongToDb(_ManualSlipEntry.OrigBatchNo));
                Parameters[9] = String.IsNullOrEmpty(_ManualSlipEntry.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _ManualSlipEntry.Descp);
                Parameters[10] = String.IsNullOrEmpty(_ManualSlipEntry.SettleDate) ? new SqlParameter("@SettleDate", DBNull.Value) : new SqlParameter("@SettleDate", DateConverterDB(_ManualSlipEntry.SettleDate));
                Parameters[11] = String.IsNullOrEmpty(_ManualSlipEntry.SelectedSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _ManualSlipEntry.SelectedSts);
                Parameters[12] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[13] = new SqlParameter("@oBatchId", SqlDbType.NVarChar, 20);
                Parameters[13].Direction = ParameterDirection.Output;
                Parameters[14] = new SqlParameter("@oSettleId", SqlDbType.NVarChar, 20);
                Parameters[14].Direction = ParameterDirection.Output;

                Parameters[15] = new SqlParameter("@RETURN_VALUE", SqlDbType.VarChar, 19);
                Parameters[15].Direction = ParameterDirection.ReturnValue;

                //Parameters[9] = String.IsNullOrEmpty(_ManualSlipEntry.SiteId) ? new SqlParameter("@SiteId", DBNull.Value) : new SqlParameter("@SiteId", DateConverterDB(_ManualSlipEntry.SiteId));
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebMerchManualBatchMaint", CommandType.StoredProcedure, Parameters);
                var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
                this.BatchId = Convert.ToString(Cmd.Parameters[13].Value);
                this.SettleId = Convert.ToString(Cmd.Parameters[14].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> MerchManualBatchTxnDelete(ManualSlipEntry _ManualSlipEntry)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                Parameters[0] = new SqlParameter("@AcqNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_ManualSlipEntry.BatchId) ? new SqlParameter("@BatchId", DBNull.Value) : new SqlParameter("@BatchId", _ManualSlipEntry.BatchId);
                Parameters[2] = String.IsNullOrEmpty(_ManualSlipEntry.SettleId) ? new SqlParameter("@SettleId", DBNull.Value) : new SqlParameter("@SettleId", _ManualSlipEntry.SettleId);
                Parameters[3] = String.IsNullOrEmpty(_ManualSlipEntry.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _ManualSlipEntry.TxnId);
                Parameters[4] = String.IsNullOrEmpty(_ManualSlipEntry.TxnDetailId) ? new SqlParameter("@DetailTxnId", DBNull.Value) : new SqlParameter("@DetailTxnId", _ManualSlipEntry.TxnDetailId);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;

                var Cmd = objDataEngine.ExecuteWithReturnValue("WebMerchManualTxnDelete", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = GetMessageCode(Result);
                return await Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }



        }
        #endregion

        #region "ManualTxn"
        public async Task<List<ManualSlipEntry>> GetManualSlipEntryTxnList(string SettleId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = String.IsNullOrEmpty(SettleId) ? new SqlParameter("@SettleId", DBNull.Value) : new SqlParameter("@SettleId", SettleId);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebMerchManualTxnListSelect", CommandType.StoredProcedure, Parameters);
                var _ManualSlipEntryTxnList = new List<ManualSlipEntry>();

                while (execResult.Read())
                {
                    _ManualSlipEntryTxnList.Add(new ManualSlipEntry
                    {


                        BusnLocation = Convert.ToString(execResult["Dealer"]),
                        SelectedTermId = Convert.ToString(execResult["Terminal Id"]),
                        SiteId = Convert.ToString(execResult["Site Id"]),
                        BatchId = Convert.ToString(execResult["Batch Id"]),//
                        InvoiceNo = ConvertInt(execResult["Invoice No"]),//
                        RcptNo = Convert.ToString(execResult["Receipt No"]),//
                        TxnDate = DateConverter(execResult["Transaction Date"]),//
                        CardNo = Convert.ToString(execResult["Card No"]),
                        AuthCardNo = Convert.ToString(execResult["Driver Card"]),//
                        DisplayTxnAmt = ConverterDecimal(execResult["Txn Amount"]),
                        ShownTxnAmt = ConverterDecimal(execResult["Txn Amount"]),//
                        Descp = Convert.ToString(execResult["Description"]),//
                        AuthNo = Convert.ToString(execResult["Auth Resp"]),//
                        Odometer = ConvertInt(execResult["Odometer Reading"]),
                        ArrayCnt = ConvertInt(execResult["Array Count"]),
                        SelectedTxnCd = ConvertInt(execResult["TxnCd"]),
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        SelectedSts = Convert.ToString(execResult["Sts"]),
                        VATNo = Convert.ToString(execResult["VATNo"]),
                        VATAmt = ConverterDecimal(execResult["VATAmt"]),
                        _CreationDatenUserId = new CreationDatenUserId
                        {
                            CreationDate = DateConverter(execResult["Creation Date"]),
                            UserId = Convert.ToString(execResult["User Id"])
                        },

                    });

                };
                return _ManualSlipEntryTxnList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }


        public ManualSlipEntry WebGetManualTxn(string SettleId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = String.IsNullOrEmpty(SettleId) ? new SqlParameter("@SettleId", DBNull.Value) : new SqlParameter("@SettleId", SettleId);

                var execResult = objDataEngine.ExecuteCommand("WebGetManualTxn", CommandType.StoredProcedure, Parameters);

                while (execResult.Read())
                {
                    var _ManualSlipEntryTxnDetail = new ManualSlipEntry

                    {

                        BusnLocation = Convert.ToString(execResult["Dealer"]),
                        SelectedTermId = Convert.ToString(execResult["Termid"])

                    };
                    return _ManualSlipEntryTxnDetail;
                }
                return new ManualSlipEntry();
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public ManualSlipEntry GetManualSlipEntryTxnDetail(string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", TxnId);

                var execResult = objDataEngine.ExecuteCommand("WebMerchManualTxnSelect", CommandType.StoredProcedure, Parameters);

                while (execResult.Read())
                {
                    var _ManualSlipEntryTxnDetail = new ManualSlipEntry

                    {

                        BusnLocation = Convert.ToString(execResult["Dealer"]),
                        SelectedTermId = Convert.ToString(execResult["TermId"]),
                        SelectedTxnCd = ConvertInt(execResult["TxnCd"]),
                        SiteId = Convert.ToString(execResult["SiteId"]),
                        InvoiceNo = ConvertInt(execResult["InvoiceNo"]),
                        CardNo = Convert.ToString(execResult["CardNo"]),
                        CardExpire = Convert.ToString(execResult["CardExpiry"]),
                        AuthCardNo = Convert.ToString(execResult["DriverCard"]),
                        AuthCardExp = Convert.ToString(execResult["AuthCardExpiry"]),
                        TxnDate = DateConverter(execResult["TxnDate"]),
                        RcptNo = Convert.ToString(execResult["RcptNo"]),
                        DisplayTotalAmt = ConverterDecimal(execResult["Amt"]),
                        AuthNo = Convert.ToString(execResult["AuthNo"]),
                        Odometer = ConvertInt(execResult["Odometer"]),
                        DriverCd = ConvertInt(execResult["DriverCd"]),
                        Rrn = Convert.ToString(execResult["Rrn"]),
                        Descp = Convert.ToString(execResult["Descp"]),
                        SettleId = Convert.ToString(execResult["SettleId"]),
                        Stans = ConvertInt(execResult["Stan"]),
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        SelectedSts = Convert.ToString(execResult["sts"]),
                        VATAmt = ConverterDecimal(execResult["VATAmt"]),
                        VATNo = Convert.ToString(execResult["VATNo"]),
                        AppvCd = Convert.ToString(execResult["AuthNo"]),
                        _CreationDatenUserId = new CreationDatenUserId
                        {
                            CreationDate = DateConverter(execResult["CreationDate"]),
                            UserId = Convert.ToString(execResult["sts"])

                        },

                    };
                    return _ManualSlipEntryTxnDetail;

                };
                return new ManualSlipEntry();
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

         

        }
        public async Task<MsgRetriever> SaveManualSlipEntry(ManualSlipEntry _ManualSlipEntry)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[29];

                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = new SqlParameter("@TxnCd", ConvertIntToDb(_ManualSlipEntry.SelectedTxnCd));
                Parameters[2] = String.IsNullOrEmpty(_ManualSlipEntry.BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", _ManualSlipEntry.BusnLocation);
                Parameters[3] = String.IsNullOrEmpty(_ManualSlipEntry.SelectedTermId) ? new SqlParameter("@TermId", DBNull.Value) : new SqlParameter("@TermId", _ManualSlipEntry.SelectedTermId);
                Parameters[4] = String.IsNullOrEmpty(_ManualSlipEntry.SiteId) ? new SqlParameter("@SiteId", DBNull.Value) : new SqlParameter("@SiteId", _ManualSlipEntry.SiteId);

                Parameters[5] = String.IsNullOrEmpty(_ManualSlipEntry.SettleId) ? new SqlParameter("@SettleId", DBNull.Value) : new SqlParameter("@SettleId", _ManualSlipEntry.SettleId);
                Parameters[6] = String.IsNullOrEmpty(_ManualSlipEntry.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _ManualSlipEntry.TxnId);
                Parameters[7] = String.IsNullOrEmpty(_ManualSlipEntry.RcptNo) ? new SqlParameter("@RcptNo", DBNull.Value) : new SqlParameter("@RcptNo", _ManualSlipEntry.RcptNo);
                Parameters[8] = new SqlParameter("@InvoiceNo", ConvertIntToDb(_ManualSlipEntry.InvoiceNo));
                Parameters[9] = new SqlParameter("@Stan", ConvertLongToDb(_ManualSlipEntry.Stans));

                Parameters[10] = String.IsNullOrEmpty(_ManualSlipEntry.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _ManualSlipEntry.CardNo);
                Parameters[11] = String.IsNullOrEmpty(_ManualSlipEntry.CardExpire) ? new SqlParameter("@CardExp", DBNull.Value) : new SqlParameter("@CardExp", _ManualSlipEntry.CardExpire);
                Parameters[12] = String.IsNullOrEmpty(_ManualSlipEntry.AuthCardNo) ? new SqlParameter("@AuthCardNo", DBNull.Value) : new SqlParameter("@AuthCardNo", _ManualSlipEntry.AuthCardNo);
                Parameters[13] = String.IsNullOrEmpty(_ManualSlipEntry.AuthCardExp) ? new SqlParameter("@AuthCardExp", DBNull.Value) : new SqlParameter("@AuthCardExp", _ManualSlipEntry.AuthCardExp);
                Parameters[14] = new SqlParameter("@DriverCd", ConvertIntToDb(_ManualSlipEntry.DriverCd));

                Parameters[15] = new SqlParameter("@TxnDate", ConvertDatetimeDB(_ManualSlipEntry.TxnDate));
                Parameters[16] = new SqlParameter("@ArrayCnt", ConvertIntToDb(_ManualSlipEntry.ArrayCnt));
                Parameters[17] = new SqlParameter("@Qty", ConvertDecimalToDb(_ManualSlipEntry.Quantity));
                Parameters[18] = new SqlParameter("@Amt", ConvertDecimalToDb(_ManualSlipEntry.DisplayTotalAmt));
                Parameters[19] = String.IsNullOrEmpty(_ManualSlipEntry.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _ManualSlipEntry.Descp);

                Parameters[20] = new SqlParameter("@Odometer", ConvertLongToDb(_ManualSlipEntry.Odometer));
                Parameters[21] = new SqlParameter("@Rrn", ConvertLongToDb(_ManualSlipEntry.Rrn));//String.IsNullOrEmpty(_ManualSlipEntry.Rrn) ? new SqlParameter("@Rrn", DBNull.Value) : 
                Parameters[22] = String.IsNullOrEmpty(_ManualSlipEntry.AuthNo) ? new SqlParameter("@AuthNo", DBNull.Value) : new SqlParameter("@AuthNo", _ManualSlipEntry.AuthNo);
                Parameters[23] = String.IsNullOrEmpty(_ManualSlipEntry.SelectedSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _ManualSlipEntry.SelectedSts);
                Parameters[24] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[25] = String.IsNullOrEmpty(_ManualSlipEntry.VATNo) ? new SqlParameter("@VATNo", DBNull.Value) : new SqlParameter("@VATNo", _ManualSlipEntry.VATNo);
                //Parameters[26] = String.IsNullOrEmpty(_ManualSlipEntry.AppvCd) ? new SqlParameter("@AuthNo", DBNull.Value) : new SqlParameter("@AuthNo", _ManualSlipEntry.AppvCd);
                Parameters[26] = new SqlParameter("@oTxnId", SqlDbType.VarChar, 19);
                Parameters[26].Direction = ParameterDirection.Output;
                Parameters[27] = new SqlParameter("@oSettleId", SqlDbType.VarChar, 19);
                Parameters[27].Direction = ParameterDirection.Output;
                Parameters[28] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[28].Direction = ParameterDirection.ReturnValue;

                var Cmd = objDataEngine.ExecuteWithReturnValue("WebMerchManualTxnMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                this.TxnId = Convert.ToString(Cmd.Parameters["@oTxnId"].Value);
                this.SettleId = Convert.ToString(Cmd.Parameters["@oSettleId"].Value);
                var Descp = GetMessageCode(Result);
                return await Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> MerchManualTxnDelete(ManualSlipEntry _ManualSlipEntry)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                Parameters[0] = new SqlParameter("@AcqNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_ManualSlipEntry.BatchId) ? new SqlParameter("@BatchId", DBNull.Value) : new SqlParameter("@BatchId", _ManualSlipEntry.BatchId);
                Parameters[2] = String.IsNullOrEmpty(_ManualSlipEntry.SettleId) ? new SqlParameter("@SettleId", DBNull.Value) : new SqlParameter("@SettleId", _ManualSlipEntry.SettleId);
                Parameters[3] = String.IsNullOrEmpty(_ManualSlipEntry.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _ManualSlipEntry.TxnId);
                Parameters[4] = String.IsNullOrEmpty(_ManualSlipEntry.TxnDetailId) ? new SqlParameter("@DetailTxnId", DBNull.Value) : new SqlParameter("@DetailTxnId", _ManualSlipEntry.TxnDetailId);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;

                var Cmd = objDataEngine.ExecuteWithReturnValue("WebMerchManualTxnDelete", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = GetMessageCode(Result);
                return await Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        #endregion

        #region "Product"
        public List<ManualTxnProduct> GetManualTxnProductList(string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", TxnId);

                var execResult = objDataEngine.ExecuteCommand("WebMerchManualTxnDetailListSelect", CommandType.StoredProcedure, Parameters);
                var _ManualTxnDetailList = new List<ManualTxnProduct>();

                while (execResult.Read())
                {
                    _ManualTxnDetailList.Add(new ManualTxnProduct
                    {


                        BatchId = Convert.ToString(execResult["Batch Id"]),
                        SelectedProdCd = Convert.ToString(execResult["Prod"]),
                        Quantity = ConverterDecimal(execResult["Quantity"]),
                        ProdAmt = ConverterDecimal(execResult["Prod Amount"]),
                        ProdDesc = Convert.ToString(execResult["Prod Description"]),
                        LastUpdDate = DateConverter(execResult["Last Update Date"]),
                        SettleId = Convert.ToString(execResult["SettleId"]),
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        TxnDetailId = Convert.ToString(execResult["TxnDetailId"]),
                        VATAmt = ConverterDecimal(execResult["VATAmt"]),
                        VATRate = ConverterDecimal(execResult["VATRate"]),
                        SelectedVATCd = Convert.ToString(execResult["VATCd"]),

                        _CreationDatenUserId = new CreationDatenUserId
                        {
                            CreationDate = DateConverter(execResult["Creation Date"]),
                            UserId = Convert.ToString(execResult["User Id"])
                        },
                    });

                };
                return _ManualTxnDetailList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

          
        }
        public ManualSlipEntry GetMerchManualTxnProductDetail(string TxnId, string TxnDetailId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", TxnId);
                Parameters[2] = String.IsNullOrEmpty(TxnDetailId) ? new SqlParameter("@TxnDetailId", DBNull.Value) : new SqlParameter("@TxnDetailId", TxnDetailId);
                var execResult = objDataEngine.ExecuteCommand("WebMerchManualTxnDetailSelect", CommandType.StoredProcedure, Parameters);

                while (execResult.Read())
                {
                    var _ManualTxnProduct = new ManualSlipEntry
                    {

                        SelectedProdCd = Convert.ToString(execResult["ProdCd"]),
                        Quantity = ConvertInt(execResult["Qty"]).ToString("0"),
                        ProdAmt = ConverterDecimal(execResult["ProdAmt"]),
                        ProdDesc = Convert.ToString(execResult["Descp"]),
                        UnitPrice = ConverterDecimal(execResult["UnitPrice"]),
                        SettleId = Convert.ToString(execResult["SettleId"]),
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        TxnDetailId = Convert.ToString(execResult["TxnDetailId"]),
                        VATAmt = ConverterDecimal(execResult["VATAmt"]),
                        VATRate = ConverterDecimal(execResult["VATRate"]),
                        SelectedVATCd = Convert.ToString(execResult["VATCd"]),
                    };
                    return _ManualTxnProduct;

                };
                return new ManualSlipEntry();
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
          

        }
        /*
        public async Task<MsgRetriever> SaveMerchManualTxnProduct(ManualSlipEntry _ManualTxnProduct)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[14];
                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = string.IsNullOrEmpty(_ManualTxnProduct.SettleId) ? new SqlParameter("@SettleId", DBNull.Value) : new SqlParameter("@SettleId", _ManualTxnProduct.SettleId);
                Parameters[2] = string.IsNullOrEmpty(_ManualTxnProduct.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _ManualTxnProduct.TxnId);
                Parameters[3] = string.IsNullOrEmpty(_ManualTxnProduct.TxnDetailId) ? new SqlParameter("@TxnDetailId", DBNull.Value) : new SqlParameter("@TxnDetailId", _ManualTxnProduct.TxnDetailId);
                Parameters[4] = string.IsNullOrEmpty(_ManualTxnProduct.SelectedProdCd) ? new SqlParameter("@ProdCd", DBNull.Value) : new SqlParameter("@ProdCd", _ManualTxnProduct.SelectedProdCd.Split('-').First());
                Parameters[5] = new SqlParameter("@Qty", ConvertDecimalToDb(_ManualTxnProduct.Quantity));
                Parameters[6] = new SqlParameter("@AmtPts", ConvertDecimalToDb(_ManualTxnProduct.ProdAmt));
                Parameters[7] = String.IsNullOrEmpty(_ManualTxnProduct.ProdDesc) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _ManualTxnProduct.ProdDesc);
                Parameters[8] = new SqlParameter("@UnitPrice", ConvertDecimalToDb(_ManualTxnProduct.UnitPrice));
                Parameters[9] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[10] = String.IsNullOrEmpty(_ManualTxnProduct.VATAmt) ? new SqlParameter("@VATAmt", DBNull.Value) : new SqlParameter("@VATAmt", _ManualTxnProduct.VATAmt);
                Parameters[11] = String.IsNullOrEmpty(_ManualTxnProduct.SelectedVATCd) ? new SqlParameter("@VATCd", DBNull.Value) : new SqlParameter("@VATCd", _ManualTxnProduct.SelectedVATCd);
                Parameters[12] = new SqlParameter("@oTxnDetailId", SqlDbType.NVarChar, 19);
                Parameters[12].Direction = ParameterDirection.Output;
                Parameters[13] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[13].Direction = ParameterDirection.ReturnValue;

                var Cmd = objDataEngine.ExecuteWithReturnValue("WebMerchManualTxnDetailMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                this.TxnDetailId = Convert.ToString(Cmd.Parameters["@oTxnDetailId"].Value);

                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
          

        }
        */
        public async Task<MsgRetriever> MerchManualProductTxnDelete(ManualSlipEntry _TxnProduct)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);



            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                Parameters[0] = new SqlParameter("@AcqNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_TxnProduct.BatchId) ? new SqlParameter("@BatchId", DBNull.Value) : new SqlParameter("@BatchId", _TxnProduct.BatchId);
                Parameters[2] = String.IsNullOrEmpty(_TxnProduct.SettleId) ? new SqlParameter("@SettleId", DBNull.Value) : new SqlParameter("@SettleId", _TxnProduct.SettleId);
                Parameters[3] = String.IsNullOrEmpty(_TxnProduct.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _TxnProduct.TxnId);
                Parameters[4] = String.IsNullOrEmpty(_TxnProduct.TxnDetailId) ? new SqlParameter("@DetailTxnId", DBNull.Value) : new SqlParameter("@DetailTxnId", _TxnProduct.TxnDetailId);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;

                var Cmd = objDataEngine.ExecuteWithReturnValue("WebMerchManualTxnDelete", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = GetMessageCode(Result);
                return await Descp;
            }
            finally
            {

                objDataEngine.CloseConnection();
            }
        }
        #endregion
    }
}
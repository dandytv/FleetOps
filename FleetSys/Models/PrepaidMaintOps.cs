using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelSector;
using CCMS.ModelSector;
using Utilities.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class PrepaidMaintOps : BaseClass
    {
        public List<PurchaseOrder> WebReloadFundSearch(Prepaid Params)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[5];
            Parameters[0] = String.IsNullOrEmpty(Params.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", Params.AcctNo);
            Parameters[1] = String.IsNullOrEmpty(Params.SelectedStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", Params.SelectedStatus);
            Parameters[2] = String.IsNullOrEmpty(Params.DocNo) ? new SqlParameter("@DocNo", DBNull.Value) : new SqlParameter("@DocNo", Params.DocNo);



            Parameters[3] = new SqlParameter("@FromDate", ConvertDatetimeDB(Params.FromDate));
            Parameters[4] = new SqlParameter("@ToDate", ConvertDatetimeDB(Params.ToDate));
            var execResult = objDataEngine.ExecuteCommand("WebReloadFundSearch", CommandType.StoredProcedure, Parameters);
            var _PrepaidList = new List<PurchaseOrder>();
            while (execResult.Read())
            {
                var _Po = new PurchaseOrder();
                _Po.TxnId = ConvertToInt(execResult["TxnId"]);
                _Po.DocNo = Convert.ToString(execResult["DocNo"]);
                _Po.TxnDate = DateConverter(execResult["TxnDate"]);
                _Po.TxnAmt = ConverterDecimal(execResult["POAmt"]);
                _Po.Balance = ConverterDecimal(execResult["POBal"]);
                _Po.SelectedStatus = Convert.ToString(execResult["Sts"]);
                _Po.CreationDate = DateConverter(execResult["CreationDate"]);
                _Po.XRefDoc = Convert.ToString(execResult["Remarks"]);
                _Po.UserId = Convert.ToString(execResult["CreatedBy"]);
                _PrepaidList.Add(_Po);
            };
            objDataEngine.CloseConnection();
            return _PrepaidList;
        }
        public PurchaseOrder WebReloadFundSelect(string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", TxnId);
            var Reader = objDataEngine.ExecuteCommand("WebReloadFundSelect", CommandType.StoredProcedure, Parameters);
            while (Reader.Read())
            {
                var _PurchaseOrder = new PurchaseOrder
                {
                    AcctNo = Convert.ToInt64(Reader["AcctNo"]),
                    DocNo = Convert.ToString(Reader["DocNo"]),
                    TxnDate = DateConverter(Reader["TxnDate"]),
                    TxnAmt = ConverterDecimal(Reader["POAmt"]),
                    SelectedStatus = Convert.ToString(Reader["Sts"]),
                    Remarks = Convert.ToString(Reader["Remarks"])
                };
                objDataEngine.CloseConnection();
                return _PurchaseOrder;
            }
            return new PurchaseOrder();
        }
        public async Task<MsgRetriever> WebReloadFundMaint(PurchaseOrder _Order, string Func)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[12];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_Order.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _Order.Remarks);
            Parameters[2] = String.IsNullOrEmpty(_Order.DocNo) ? new SqlParameter("@DocNo", DBNull.Value) : new SqlParameter("@DocNo", _Order.DocNo);
            Parameters[3] = new SqlParameter("@AcctNo", _Order.AcctNo);
            Parameters[4] = new SqlParameter("@Func", Func);
            Parameters[5] = new SqlParameter("@TxnDate", ConvertDatetimeDB(_Order.TxnDate));
            Parameters[6] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[7] = new SqlParameter("@CreatedBy", this.GetUserId);
            Parameters[8] = Func.ToLower() == "n" ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", ConvertIntToDb(_Order.TxnId));
            Parameters[9] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[9].Direction = ParameterDirection.ReturnValue;
            Parameters[10] = new SqlParameter("@TxnAmt", ConvertDecimalToDb(_Order.TxnAmt));
            Parameters[11] = String.IsNullOrEmpty(_Order.SelectedStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _Order.SelectedStatus);
            var Cmd = objDataEngine.ExecuteWithReturnValue("WebReloadFundMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return await Descp;
        }


        public List<DeliveryAdvice> WebReloadAllocationListSelect(string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@ParentTxnId", DBNull.Value) : new SqlParameter("@ParentTxnId", ConvertIntToDb(TxnId));
            var execResult = objDataEngine.ExecuteCommand("WebReloadAllocationListSelect", CommandType.StoredProcedure, Parameters);
            var _DeliveryList = new List<DeliveryAdvice>();
            while (execResult.Read())
            {
                _DeliveryList.Add(new DeliveryAdvice
                {
                    TxnDate = DateConverter(execResult["TxnDate"]),
                    EffDateFrom = DateConverter(execResult["EffDateFrom"]),
                    TxnAmt = ConverterDecimal(execResult["DAAmt"]),
                    DABal = ConverterDecimal(execResult["DaBal"]),
                    SelectedStatus = Convert.ToString(execResult["Sts"]),
                    Remarks = Convert.ToString(execResult["Remarks"]),
                    CreatedBy = Convert.ToString(execResult["CreatedBy"]),
                    CreationDate = DateConverter(execResult["CreationDate"]),
                    DocNo = Convert.ToString(execResult["DocNo"]),
                    PoTxnId = Convert.ToString(execResult["POTxnId"]),
                    DaTxnId = Convert.ToString(execResult["DATxnId"])
                });
            }
            objDataEngine.CloseConnection();
            return _DeliveryList;
        }
        public DeliveryAdvice WebReloadAllocationSelect(int TxnId, string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = new SqlParameter("@TxnId", TxnId);
            var Reader = objDataEngine.ExecuteCommand("WebReloadAllocationSelect", CommandType.StoredProcedure, Parameters);
            while (Reader.Read())
            {
                var _DeliveryAdvice = new DeliveryAdvice
                {
                    DocNo = Convert.ToString(Reader["DocNo"]),
                    AcctNo = Convert.ToInt64(Reader["AcctNo"]),
                    Remarks = Convert.ToString(Reader["Remarks"]),
                    TxnDate = DateConverter(Reader["TxnDate"]),
                    EffDateFrom = DateConverter(Reader["EffDateFrom"]),
                    SelectedStatus = Convert.ToString(Reader["Sts"]),
                    TxnAmt = ConverterDecimal(Reader["DAAmt"]),
                    TxnId = ConvertInt(Reader["TxnId"]),
                    ParentTxnId = Convert.ToString(Reader["ParentTxnId"]),
                    CreatedBy = Convert.ToString(Reader["CreatedBy"]),
                    CreationDate = DateConverter(Reader["CreationDate"]),
                    UserId = Convert.ToString(Reader["UserId"])
                };
                return _DeliveryAdvice;
            }
            return new DeliveryAdvice();
        }
        public async Task<MsgRetriever> WebReloadAllocationMaint(DeliveryAdvice _Delivery, string Func)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[13];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = Func.ToLower() == "n" ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", ConvertIntToDb(_Delivery.TxnId));
            Parameters[2] = new SqlParameter("@ParentTxnId", ConvertIntToDb(_Delivery.ParentTxnId));
            Parameters[3] = String.IsNullOrEmpty(_Delivery.DocNo) ? new SqlParameter("@DocNo", DBNull.Value) : new SqlParameter("@DocNo", _Delivery.DocNo);
            Parameters[4] = new SqlParameter("@AcctNo", _Delivery.AcctNo);
            Parameters[5] = new SqlParameter("@TxnDate", ConvertDatetimeDB(_Delivery.TxnDate));
            Parameters[6] = new SqlParameter("@TxnAmt", ConvertDecimalToDb(_Delivery.TxnAmt));
            Parameters[7] = String.IsNullOrEmpty(_Delivery.EffDateFrom) ? new SqlParameter("@EffDateFrom", DBNull.Value) : new SqlParameter("@EffDateFrom", ConvertDatetimeDB(_Delivery.EffDateFrom));
            Parameters[8] = string.IsNullOrEmpty(_Delivery.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _Delivery.Remarks);
            Parameters[9] = String.IsNullOrEmpty(_Delivery.SelectedStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _Delivery.SelectedStatus);
            Parameters[10] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[11] = new SqlParameter("@Func", Func);
            Parameters[12] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[12].Direction = ParameterDirection.ReturnValue;
            var Cmd = objDataEngine.ExecuteWithReturnValue("[WebReloadAllocationMaint]", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return await Descp;

        }
        public List<PrepaidCardnAcct> WebReloadAllocationDetailListSelect(string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@ParentTxnId", DBNull.Value) : new SqlParameter("@ParentTxnId", TxnId);
            var execResult = objDataEngine.ExecuteCommand("WebReloadAllocationDetailListSelect", CommandType.StoredProcedure, Parameters);
            var CardNAcct = new List<PrepaidCardnAcct>();
            while (execResult.Read())
            {
                CardNAcct.Add(new PrepaidCardnAcct
                {
                    CardNo = Convert.ToString(execResult["CardNo"]),
                    ReloadAmt = ConverterDecimal(execResult["ReloadAmt"]),
                    SelectedStatus = Convert.ToString(execResult["Sts"]),
                    Remarks = Convert.ToString(execResult["Remarks"]),
                    ReloadDate = DateConverter(execResult["ReloadDate"]),
                    UserId = Convert.ToString(execResult["UserId"]),
                    CreationDate = DateConverter(execResult["CreationDate"]),
                    TxnId = ConvertLongToDb(execResult["TxnId"]),
                    AcctNo = Convert.ToString(execResult["AcctNo"])
                });
            }
            objDataEngine.CloseConnection();
            return CardNAcct;
        }
        public PrepaidCardnAcct WebReloadAllocationDetailSelect(int TxnId, string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@AcctNo", AcctNo);
            Parameters[1] = new SqlParameter("@TxnId", TxnId);
            var Reader = objDataEngine.ExecuteCommand("WebReloadAllocationDetailSelect", CommandType.StoredProcedure, Parameters);
            while (Reader.Read())
            {
                var _PrepaidCardnAcct = new PrepaidCardnAcct
                {
                    CardNo = Convert.ToString(Reader["CardNo"]),
                    AcctNo = Convert.ToString(Reader["AcctNo"]),
                    SelectedStatus = Convert.ToString(Reader["Sts"]),
                    Remarks = Convert.ToString(Reader["Remarks"]),
                    ParentTxnId = Convert.ToInt64(Reader["ParentTxnId"]),
                    TxnDate = DateTimeConverter(Reader["TxnDate"]),
                    EffDateFrom = DateConverter(Reader["EffDateFrom"]),
                    TxnAmt = ConverterDecimal(Reader["ReloadAmt"]),
                };
                return _PrepaidCardnAcct;
            }
            return new PrepaidCardnAcct();
        }
        public async Task<MsgRetriever> WebReloadAllocDetailMaint(PrepaidCardnAcct _PrepaidCardnAcct, int ParentTxnId, string func, string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();

            SqlParameter[] Parameters = new SqlParameter[14];
            Parameters[0] = String.IsNullOrEmpty(_PrepaidCardnAcct.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _PrepaidCardnAcct.CardNo);
            Parameters[1] = String.IsNullOrEmpty(_PrepaidCardnAcct.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _PrepaidCardnAcct.AcctNo);
            Parameters[2] = String.IsNullOrEmpty(_PrepaidCardnAcct.SelectedStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _PrepaidCardnAcct.SelectedStatus);
            Parameters[3] = String.IsNullOrEmpty(_PrepaidCardnAcct.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _PrepaidCardnAcct.Remarks);
            Parameters[4] = String.IsNullOrEmpty(_PrepaidCardnAcct.TxnDate) ? new SqlParameter("@TxnDate", DBNull.Value) : new SqlParameter("@TxnDate", ConvertDatetimeDB(_PrepaidCardnAcct.TxnDate));
            Parameters[5] = String.IsNullOrEmpty(_PrepaidCardnAcct.TxnAmt) ? new SqlParameter("@TxnAmt", DBNull.Value) : new SqlParameter("@TxnAmt", ConverterDecimal(_PrepaidCardnAcct.TxnAmt));
            Parameters[6] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[7] = func.ToLower() == "n" ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", ConvertIntToDb(_PrepaidCardnAcct.TxnId));
            Parameters[8] = new SqlParameter("@ParentTxnId", ParentTxnId);
            Parameters[9] = new SqlParameter("@Func", func);
            Parameters[10] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[10].Direction = ParameterDirection.ReturnValue;
            Parameters[11] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[12] = String.IsNullOrEmpty(_PrepaidCardnAcct.EffDateFrom) ? new SqlParameter("@EffDateFrom", DBNull.Value) : new SqlParameter("@EffDateFrom", ConvertDatetimeDB(_PrepaidCardnAcct.EffDateFrom));
            Parameters[13] = new SqlParameter("@CreatedBy", this.GetUserId);

            var Cmd = objDataEngine.ExecuteWithReturnValue("[WebReloadAllocDetailMaint]", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return await Descp;
        }
    }
}
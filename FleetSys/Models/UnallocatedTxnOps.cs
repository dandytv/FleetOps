using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using Utilities.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using CCMS.ModelSector;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class UnallocatedTxnOps : BaseClass
    {

        #region "UnallocateTxn"

        public async Task<List<UnsettleTxn>> GetUnsettleTxnList(string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", TxnId);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnUnsettleListSelect", CommandType.StoredProcedure, Parameters);
                var UnallocateTxn = new List<UnsettleTxn>();
                while (execResult.Read())
                {
                    UnallocateTxn.Add(new UnsettleTxn
                    {
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        BatchId = Convert.ToString(execResult["BatchId"]),
                        RecType = Convert.ToString(execResult["RecType"]),
                        AcctNo = Convert.ToString(execResult["AcctNo"]),
                        TxnCd = ConvertInt(execResult["TxnCd"]),
                        CheqNo = Convert.ToString(execResult["ChqNo"]),
                        PayeeName = Convert.ToString(execResult["PayeeName"]),
                        TxnDate = DateConverter(execResult["TxnDate"]),
                        STxnAmt = ConverterDecimal(execResult["TxnAmt"]),
                        Descp = Convert.ToString(execResult["Descp"]),
                        SelectedSts = Convert.ToString(execResult["Sts"]),
                        creationDate = DateConverter(execResult["CreationDate"]),
                        LastUpdDate = DateConverter(execResult["LastUpdDate"]),
                        UserId = Convert.ToString(execResult["UserId"]),
                    });

                };
                return UnallocateTxn;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<UnsettleTxn> GetUnsettleTxnDetail(string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", TxnId);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnUnsettleSelect", CommandType.StoredProcedure, Parameters);
                var UnsettleTxnDetail = new UnsettleTxn();

                while (execResult.Read())
                {
                    UnsettleTxnDetail = new UnsettleTxn
                    {
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        BatchId = Convert.ToString(execResult["BatchId"]),
                        RecType = Convert.ToString(execResult["RecType"]),
                        AcctNo = Convert.ToString(execResult["AcctNo"]),
                        TxnCd = ConvertInt(execResult["TxnCd"]),
                        CheqNo = Convert.ToString(execResult["ChqNo"]),
                        PayeeName = Convert.ToString(execResult["PayeeName"]),
                        TxnDate = DateConverter(execResult["TxnDate"]),
                        STxnAmt = ConverterDecimal(execResult["TxnAmt"]),
                        Descp = Convert.ToString(execResult["Descp"]),
                        SelectedSts = Convert.ToString(execResult["Sts"]),
                        creationDate = DateConverter(execResult["CreationDate"]),
                        LastUpdDate = DateConverter(execResult["LastUpdDate"]),
                        UserId = Convert.ToString(execResult["UserId"]),
                    };

                };
                return UnsettleTxnDetail;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> SaveUnsettleTxn(UnsettleTxn _ust)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[14];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_ust.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _ust.TxnId);
                Parameters[2] = String.IsNullOrEmpty(_ust.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _ust.AcctNo);

                Parameters[3] = new SqlParameter("@TxnCd", ConvertIntToDb(_ust.TxnCd));
                Parameters[4] = String.IsNullOrEmpty(_ust.TxnDate) ? new SqlParameter("@TxnDate", DBNull.Value) : new SqlParameter("@TxnDate", DateConverterDB(_ust.TxnDate));
                Parameters[5] = String.IsNullOrEmpty(_ust.BookingDate) ? new SqlParameter("@BookingDate", DBNull.Value) : new SqlParameter("@BookingDate", DateConverterDB(_ust.BookingDate));
                Parameters[6] = new SqlParameter("@TxnAmt", ConvertDecimalToDb(_ust.STxnAmt));
                Parameters[7] = String.IsNullOrEmpty(_ust.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _ust.Descp);
                //      Parameters[8] = String.IsNullOrEmpty(_ust.PayeeName) ? new SqlParameter("@PayeeName ", DBNull.Value) : new SqlParameter("@PayeeName", _ust.PayeeName);
                Parameters[8] = String.IsNullOrEmpty(_ust.CheqNo) ? new SqlParameter("@CheqNo", DBNull.Value) : new SqlParameter("@CheqNo", _ust.CheqNo);
                Parameters[9] = String.IsNullOrEmpty(_ust.SelectedSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _ust.SelectedSts);
                Parameters[10] = new SqlParameter("@RcptNo", SqlDbType.BigInt);
                Parameters[10].Direction = ParameterDirection.Output;
                Parameters[11] = new SqlParameter("@RetCd", SqlDbType.BigInt);
                Parameters[11].Direction = ParameterDirection.Output;
                Parameters[12] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[13] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[13].Direction = ParameterDirection.ReturnValue;

                var Cmd = objDataEngine.ExecuteWithReturnValue("WebTxnUnsettleMaint", CommandType.StoredProcedure, Parameters);
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


        #region "UnbalanceTxn"
        public async Task<List<UnbalanceTxnSearch>> UnbalanceTxnSearchList(string acctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@sAcctNo", DBNull.Value) : new SqlParameter("@sAcctNo", acctNo);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnUnallocateSearch", CommandType.StoredProcedure, Parameters);
                var _UnallocatedTxnSearchList = new List<UnbalanceTxnSearch>();
                while (execResult.Read())
                {
                    _UnallocatedTxnSearchList.Add(new UnbalanceTxnSearch
                    {
                        SelectedRecType = Convert.ToString(execResult["RecType"]),
                        LBE = Convert.ToString(execResult["LBE"]),
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        AcctNo = Convert.ToString(execResult["AcctNo"]),
                        selectedTxnCd = Convert.ToString(execResult["TxnCd"]),
                        TxnDate = Convert.ToString(execResult["TxnDate"]),
                        DisplayTxnDate = DateConverter(execResult["TxnDate"]),
                        TxnAmount = Convert.ToString(ConverterDecimal(execResult["Txn Amount"])),
                        SettledAmt = Convert.ToString(ConverterDecimal(execResult["Settled Amount"])),
                        BookingAmt = Convert.ToString(ConverterDecimal(execResult["Booking Amount"])),
                        UnallocatedAmount = ConverterDecimal(execResult["Unallocated Amount"]),
                        Descp = Convert.ToString(execResult["Descp"]),

                    });

                };
                return _UnallocatedTxnSearchList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<List<UnbalanceTxnSearch>> GetUnbalanceTxnList(string RecType, string AcctNo, string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(RecType) ? new SqlParameter("@RecType", DBNull.Value) : new SqlParameter("@RecType", RecType);
                Parameters[2] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@sTxnId", DBNull.Value) : new SqlParameter("@sTxnId", TxnId);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnUnallocateListSelect", CommandType.StoredProcedure, Parameters);
                var UTS_List = new List<UnbalanceTxnSearch>();

                while (execResult.Read())
                {
                    UTS_List.Add(new UnbalanceTxnSearch
                    {
                        TxnSequence = Convert.ToString(execResult["TxnSeq"]),
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        AcctNo = Convert.ToString(execResult["AcctNo"]),
                        TxnDate = DateConverter(execResult["TxnDate"]),
                        DueDate = DateConverter(execResult["DueDate"]),
                        TxnAmount = ConverterDecimal(execResult["Txn Amount"]),
                        DisplayTxnAmount = ConverterDecimal(execResult["Txn Amount"]),
                        OutStandingAmt = ConverterDecimal(execResult["Outstanding Amount"]),
                        Descp = Convert.ToString(execResult["Descp"]),
                        BookingAmt = ConverterDecimal(execResult["Booking Amount"])
                    });

                };
                return UTS_List;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
           
        }

        public async Task<MsgRetriever> SaveBalanceTxn(string inputSrc, List<UnbalanceTxnSearch> UnallocatedModel)
        {
            DataTable dataTableTxnUnallocate = new DataTable();

            dataTableTxnUnallocate.Columns.Add("RecType");
            dataTableTxnUnallocate.Columns.Add("SrcTxnId", typeof(Int64));
            dataTableTxnUnallocate.Columns.Add("SrcTxnSeq", typeof(Int64));
            dataTableTxnUnallocate.Columns.Add("TgtTxnId", typeof(Int64));
            dataTableTxnUnallocate.Columns.Add("TxnDate");
            dataTableTxnUnallocate.Columns.Add("BookingAmt");
            //dataTableTxnUnallocate.Columns.Add("OrigAmt");
            //dataTableTxnUnallocate.Columns.Add("Rc");
            //dataTableTxnUnallocate.Columns.Add("Cnt");
            //dataTableTxnUnallocate.Columns.Add("Err");
            //dataTableTxnUnallocate.Columns.Add("PrcsId");
            //dataTableTxnUnallocate.Columns.Add("PrcsDate");
            //dataTableTxnUnallocate.Columns.Add("PrcsName");


            DataRow dr = dataTableTxnUnallocate.NewRow();
            if (UnallocatedModel != null)
            {
                for (int i = 0; i < UnallocatedModel.Count; i++)
                {
                    //        dr["ID"]= id;
                    dr["RecType"] = (object)UnallocatedModel[i].SelectedRecType ?? DBNull.Value;
                    dr["SrcTxnId"] = (object)UnallocatedModel[i].SrcTxnId ?? DBNull.Value;
                    dr["SrcTxnSeq"] = (object)UnallocatedModel[i].SrcTxnSequence ?? DBNull.Value;
                    dr["TgtTxnId"] = (object)UnallocatedModel[i].TgtTxnId ?? DBNull.Value;
                    dr["TxnDate"] = (object)ConvertDatetimeDB(UnallocatedModel[i].TxnDate) ?? DBNull.Value;
                    dr["BookingAmt"] = (object)UnallocatedModel[i].BookingAmt ?? DBNull.Value;
                    //dr["OrigAmt"] = (object)UnallocatedModel[i].OrigAmt ?? DBNull.Value;
                    //dr["Rc"] = (object)UnallocatedModel[i].re ?? DBNull.Value;
                    //dr["Cnt"] = (object)UnallocatedModel[i].Cnt ?? DBNull.Value;
                    //dr["Err"] = (object)UnallocatedModel[i].Err ?? DBNull.Value;
                    //dr["PrcsId"] = (object)UnallocatedModel[i].PrcsId ?? DBNull.Value;
                    //dr["PrcsDate"] = (object)UnallocatedModel[i].PrcsDate ?? DBNull.Value;
                    //dr["PrcsName"] = (object)UnallocatedModel[i].PrcsName ?? DBNull.Value;
                    dataTableTxnUnallocate.Rows.Add(dr);
                    dr = dataTableTxnUnallocate.NewRow();
                    //      id = id + 1;
                }
            }
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[5];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(inputSrc) ? new SqlParameter("@InputSrc", DBNull.Value) : new SqlParameter("@InputSrc", inputSrc);
                Parameters[2] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[3] = new SqlParameter("@BalanceAllocation", dataTableTxnUnallocate);
                Parameters[4] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[4].Direction = ParameterDirection.ReturnValue;


                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebTxnUnallocateMaint", CommandType.StoredProcedure, Parameters);
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
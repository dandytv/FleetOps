using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CCMS.ModelSector;
using ModelSector;
using System.Data.SqlClient;
using System.Data;
using Utilities.DAL;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FleetOps.Models
{
    public class PukalAcctOps : BaseClass
    {
        #region "PukalAcct"
        public async Task<List<PukalAcctBatchList>> GetPukalAccountList(string refCd, string acctOfficeCd, int cycStmtId )
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = !string.IsNullOrEmpty(refCd) ? new SqlParameter("@RefCd", refCd) : new SqlParameter("@RefCd", DBNull.Value);
            Parameters[1] = !string.IsNullOrEmpty(acctOfficeCd.TrimStart().TrimEnd()) ? new SqlParameter("@AcctOfficeCd",acctOfficeCd.TrimStart().TrimEnd()):new SqlParameter("@AcctOfficeCd",DBNull.Value);
            Parameters[2] =  new SqlParameter("@CycStmtId",cycStmtId);
            try
            {
                var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnPukalPaymentListSelect", CommandType.StoredProcedure, Parameters);
                var _PukalAcctList = new List<PukalAcctBatchList>();

                while (execResult.Read())
                {
                    _PukalAcctList.Add(new PukalAcctBatchList
                    {
                        BatchId = Convert.ToInt64(execResult["BatchId"]),
                        RefCd = Convert.ToString(execResult["RefCd"]),
                        ChequeNo = Convert.ToInt64(execResult["ChequeNo"]),
                        ChequeAmt = Convert.ToDecimal(execResult["ChequeAmt"]).ToString("#,##0.00"),
                        StatementDate = DateConverter(execResult["StmtDate"]),
                        AreaCode = Convert.ToString(execResult["AcctOfficeCd"]) + ":" + Convert.ToString(execResult["AcctOfficeCdDescp"]),
                        CreationDate = DateConverter(execResult["CreationDate"]),
                        StsDescp = Convert.ToString(execResult["StsDescp"]),
                        Owner = Convert.ToString(execResult["Owner"]),
                        SlipNo = Convert.ToString(execResult["SlipNo"]),
                        IssBank = Convert.ToString(execResult["IssBank"]),
                    });
                };
                return _PukalAcctList;
            }catch( Exception ex)
            {
                objDataEngine.CloseConnection();
                return null;
                throw ex;
            }
            finally {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<List<PukalAcctBatchView>> GetPukalAcctBatchView(long batchID, string refCd, string acctOfficeCd, int cycStmtId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[5];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = batchID != 0 ? new SqlParameter("@BatchId", batchID) : new SqlParameter("@BatchId", DBNull.Value);
                Parameters[2] = new SqlParameter("@RefCd", refCd);
                Parameters[3] = new SqlParameter("@AcctOfficeCd", acctOfficeCd);
                Parameters[4] = new SqlParameter("@CycStmtId", cycStmtId);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnPukalPaymentSelect", CommandType.StoredProcedure, Parameters);
                var _PukalAcctBatchView = new List<PukalAcctBatchView>();

                while (await execResult.ReadAsync())
                {
                    _PukalAcctBatchView.Add(new PukalAcctBatchView
                    {
                        AcctNo = Convert.ToInt64(execResult["AcctNo"]),
                        CompanyName = Convert.ToString(execResult["CmpyName"]),
                        SalesAmt = Convert.ToDecimal(execResult["SalesAmt"]) == 0 ? "0.00" : Convert.ToDecimal(execResult["SalesAmt"]).ToString("#,##0.00"),
                        SedutAmt = Convert.ToDecimal(execResult["SedutAmt"]) == 0 ? "0.00" : Convert.ToDecimal(execResult["SedutAmt"]).ToString("#,##0.00"),
                        PaymentAmt = Convert.ToDecimal(execResult["PaymentAmt"]) == 0 ? "0.00" : Convert.ToDecimal(execResult["PaymentAmt"]).ToString("#,##0.00")
                    });
                };
                return _PukalAcctBatchView;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> SavePukalAcctEdits(PukalAcctMaintInfo listInfo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                var userid = ClaimsPrincipal.Current.Identities.First();
                SqlParameter[] Parameters = new SqlParameter[16];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@Func", listInfo.Func);
                if (listInfo.BatchId == 0)
                {
                    Parameters[2] = new SqlParameter("@BatchId", SqlDbType.BigInt);
                    Parameters[2].Direction = ParameterDirection.Output;
                }
                else
                {
                    Parameters[2] = new SqlParameter("@BatchId", listInfo.BatchId);
                    Parameters[2].Direction = ParameterDirection.Input;
                }
                Parameters[3] = new SqlParameter("@RefCd", listInfo.RefCd);
                Parameters[4] = new SqlParameter("@AcctOfficeCd", listInfo.AcctOfficeCd);
                Parameters[5] = !string.IsNullOrEmpty(listInfo.SelectedTxnCd )? new SqlParameter("@TxnCd", listInfo.SelectedTxnCd): new SqlParameter("@TxnCd", DBNull.Value);
                Parameters[6] = !string.IsNullOrEmpty(listInfo.SelectedSettlement) ? new SqlParameter("@GLSettlement", listInfo.SelectedSettlement) : new SqlParameter("@GLSettlement", DBNull.Value);
                Parameters[7] = new SqlParameter("@CycStmtId", listInfo.CycStmtId);
                Parameters[8] = new SqlParameter("@ChequeNo", listInfo.ChequeNo);
                Parameters[9] = new SqlParameter("@ChequeAmt", Convert.ToDecimal(listInfo.ChequeAmt));
                Parameters[10] = listInfo.Func == "Save" ? new SqlParameter("@Owner",DBNull.Value) : new SqlParameter("@Owner", listInfo.SelectedOwner);
                Parameters[11] = !string.IsNullOrEmpty(userid.Name) ? new SqlParameter("@UserId", userid.Name) : new SqlParameter("@UserId", DBNull.Value);

                DataTable dt = new DataTable();
                dt.Columns.Add("AcctNo");
                dt.Columns.Add("TxnDate");
                dt.Columns.Add("DueDate");
                dt.Columns.Add("Amt");
                dt.Columns.Add("Descp");
                if (listInfo.MultipleTxnRecord != null )
                {
                    foreach (var item in listInfo.MultipleTxnRecord)
                    {
                        DataRow dr = dt.NewRow();
                        dr["AcctNo"] = item.AcctNo;
                        dr["TxnDate"] = ConvertDatetimeDB(listInfo.CreationDate);
                        dr["DueDate"] = ConvertDatetimeDB(listInfo.CreationDate);
                        dr["Amt"] = Convert.ToDecimal(item.PaymentAmt);
                        dr["Descp"] = DBNull.Value;
                        dt.Rows.Add(dr);
                    }
                }
                Parameters[12] = new SqlParameter("@PukalPayment", dt);
                Parameters[13] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[13].Direction = ParameterDirection.ReturnValue;
                Parameters[14] = !string.IsNullOrEmpty(listInfo.SlipNo) ? new SqlParameter("@SlipNo", listInfo.SlipNo) : new SqlParameter("@SlipNo", DBNull.Value);
                Parameters[15] = !string.IsNullOrEmpty(listInfo.SelectedIssBank) ? new SqlParameter("@IssBank", listInfo.SelectedIssBank) : new SqlParameter("@IssBank", DBNull.Value);
                var execResult = await objDataEngine.ExecuteWithReturnValueAsync("WebTxnPukalPaymentMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(execResult.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                if (listInfo.BatchId == 0)
                    Descp.Id = Convert.ToInt32(execResult.Parameters["@BatchId"].Value).ToString();
                return Descp;
            }catch( Exception ex)
            {
                objDataEngine.CloseConnection();
                throw ex;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<PukalPaymentSummSelect> GetPukapaymentSelect(int BatchId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                var pukaPaymentSelect = new PukalPaymentSummSelect();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", DBNull.Value);
                Parameters[1] = new SqlParameter("@BatchId", BatchId);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnPukalPaymentSummSelect", CommandType.StoredProcedure, Parameters);
                while (await execResult.ReadAsync())
                {
                    pukaPaymentSelect.TxnCd = Convert.ToString(execResult["TxnCd"]);
                    pukaPaymentSelect.TxnDate = DateConverter(execResult["TxnDate"]);
                    pukaPaymentSelect.AcctOfficeCd = Convert.ToString(execResult["AcctOfficeCd"]);
                    pukaPaymentSelect.CycStmtId = Convert.ToInt32(execResult["CycStmtId"]);
                    pukaPaymentSelect.Owner = Convert.ToString(execResult["Owner"]);
                    pukaPaymentSelect.RefCd = Convert.ToString(execResult["RefCd"]);
                    pukaPaymentSelect.StmtDate = DateConverter(execResult["StmtDate"]);
                    pukaPaymentSelect.ChequeNo = Convert.ToDecimal((execResult["ChequeNo"]));
                    pukaPaymentSelect.ChequeAmt = Convert.ToDecimal(execResult["ChequeAmt"]).ToString("#,##0.00");
                    pukaPaymentSelect.GLSettlement = Convert.ToString(execResult["GLSettlement"]);
                    pukaPaymentSelect.Sts = Convert.ToString(execResult["Sts"]);
                    pukaPaymentSelect.SlipNo = Convert.ToString(execResult["SlipNo"]);
                    pukaPaymentSelect.IssBank = Convert.ToString(execResult["IssBank"]);

                }
                return pukaPaymentSelect;
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<List<WebPukalSedutList>> GetPukalSedutList(string refCd, string AcctOfficeCd,string status)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                var webPukalSedutLst = new List<WebPukalSedutList>();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@RefCd", refCd);
                Parameters[1] = new SqlParameter("@AcctOfficeCd", AcctOfficeCd);
                Parameters[2] = new SqlParameter("@Sts", status);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebPukalSedutListSelect", CommandType.StoredProcedure, Parameters);
                while(execResult.Read())
                {
                    var pukalSedut = new WebPukalSedutList();
                    pukalSedut.AcctNo = Convert.ToInt32(execResult["AcctNo"]);
                    pukalSedut.CompanyName = Convert.ToString(execResult["CmpyName1"]);
                    pukalSedut.ActivationDate = DateConverter(execResult["ActivationDate"]);
                    pukalSedut.TerminationDate = DateConverter(execResult["TerminationDate"]);
                    pukalSedut.PukalAmt = ConverterDecimal(execResult["PukalAmt"]);
                    pukalSedut.StmtDate = DateConverter(execResult["StmtDate"]);
                    pukalSedut.Status = Convert.ToString(execResult["Sts"]);
                    pukalSedut.UserId = Convert.ToString(execResult["UserId"]);
                    webPukalSedutLst.Add(pukalSedut);
                }
                return webPukalSedutLst;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> SavePukalSedut(int accountNo, decimal txnAmt, string UserId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = new SqlParameter("@AcctNo", accountNo);
                Parameters[1] = new SqlParameter("@TxnAmt", txnAmt);
                Parameters[2] = String.IsNullOrEmpty(UserId)?new SqlParameter("@UserId",DBNull.Value): new SqlParameter("@UserId", UserId);
                Parameters[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[3].Direction = ParameterDirection.ReturnValue;
                var execResult = await objDataEngine.ExecuteWithReturnValueAsync("WebPukalSedutMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(execResult.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            catch (Exception ex)
            {
                return new MsgRetriever { desp = ex.Message,flag = 0};
                throw ex;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion
    }
}


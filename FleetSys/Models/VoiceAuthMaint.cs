using CCMS.ModelSector;
using FleetOps.DAL;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FleetOps.ViewModel;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class VoiceAuthMaint : BaseClass
    {
        public string AuthId { get; set; }
        public List<VoiceAuth> ftVoiceAuth(string BusnLocation, string CardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", BusnLocation);
            Parameters[2] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
            var execResult = objDataEngine.ExecuteCommand("[WebVAESearch]", CommandType.StoredProcedure, Parameters);

            var _VoiceAuth = new List<VoiceAuth>();
            while (execResult.Read())
            {
                _VoiceAuth.Add(new VoiceAuth
                {
                    BusnLocation = Convert.ToString(execResult["BusnLocation"]),
                    CardNo = Convert.ToString(execResult["CardNo"]),
                    ApprovalCd = Convert.ToString(execResult["ApprovalCd"]),
                    DriverCd = Convert.ToString(execResult["DriverCd"]),
                    TxnInd = Convert.ToString(execResult["TxnInd"]),
                    TxnDate = DateTimeConverter(execResult["TxnDate"]),
                    Ids = Convert.ToString(execResult["Ids"]),
                    ResponseCd = Convert.ToString(execResult["RespCd"]),
                    TxnAmt = ConverterDecimal(execResult["Amt"]),
                    UserId = Convert.ToString(execResult["UserId"]),
                    TxnCd = Convert.ToString(execResult["TxnCd"]),
                     EmbossName=Convert.ToString(execResult["EmbName"]),
                     BusnName=Convert.ToString(execResult["BusnName"]),
                     CreationDate=DateConverter(execResult["CreationDate"])
                });
            }
            return _VoiceAuth;
        }


        public List<VoiceAuthProducts> WebVAEProdListSelect(string ids)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@AcqNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(ids) ? new SqlParameter("@SrcIds", DBNull.Value) : new SqlParameter("@SrcIds", ids);
            var execResult = objDataEngine.ExecuteCommand("[WebVAEProdListSelect]", CommandType.StoredProcedure, Parameters);

            var _Products = new List<VoiceAuthProducts>();
            while (execResult.Read())
            {
                _Products.Add(new VoiceAuthProducts
                {
                    SelectedProdCd = Convert.ToString(execResult["ProdCd"]),
                    Qty = Convert.ToString(execResult["Qty"]),
                    AmtPoints = ConverterDecimal(execResult["Amt"]),
                    FastTrack = Convert.ToString(execResult["FastTrack"]),
                    UnitPrice = ConverterDecimal(execResult["UnitPrice"]),
                });
            }
            return _Products;
        }


        public async Task<VoiceAuthDetail> WebVaeDetailSelect(string BusnLocation, string CardNo, string Ids)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
            Parameters[1] = String.IsNullOrEmpty(BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", BusnLocation);
            Parameters[2] = String.IsNullOrEmpty(Ids) ? new SqlParameter("@VaeIds", DBNull.Value) : new SqlParameter("@VaeIds", Ids);


            var execResult = objDataEngine.ExecuteCommand("[WebVaeDetailSelect]", CommandType.StoredProcedure, Parameters);
            while (execResult.Read())
            {
                    var _Detail = new VoiceAuthDetail
                    {
                        AcctNo = Convert.ToString(execResult["AcctNo"]),
                        AcctName = Convert.ToString(execResult["AcctName"]),
                        SelectedAcctStatus = Convert.ToString(execResult["AcctSts"]),
                        CorpCd = Convert.ToString(execResult["CorpCd"]),
                        AcctBalance = ConverterDecimal(execResult["AcctBal"]),
                        CreditLimit = ConverterDecimal(execResult["CreditLimit"]),
                        CardNo = Convert.ToString(execResult["CardNo"]),
                        SelectedCardType = Convert.ToString(execResult["CardType"]),
                        SelectedCardStatus = Convert.ToString(execResult["CardSts"]),
                        MemberSince = Convert.ToString(execResult["MemSince"]),
                        SelectedProductGroup = Convert.ToString(execResult["ProdGroup"]),
                        BusnLocation = Convert.ToString(execResult["BusnLocation"]),
                        BusnName = Convert.ToString(execResult["BusnName"]),
                        SelectedBusnStatus = Convert.ToString(execResult["BusnSts"]),
                        FloorLimit = ConverterDecimal(execResult["FloorLimit"]),
                        XRefCardNo = Convert.ToString(execResult["AuthCardNo"])
                    };
                    _Detail.ProdCd = await BaseClass.WebGetProductCode(_Detail.SelectedProductGroup);
                    if (!string.IsNullOrEmpty(Ids))
                    {
                        _Detail.termId = Convert.ToString(execResult["TermId"]);
                        _Detail.SelectedTxnCd = Convert.ToString(execResult["TxnCd"]);
                        _Detail.AuthNo = Convert.ToString(execResult["AuthNo"]);
                        _Detail.RespCd = Convert.ToString(execResult["RespCd"]);
                        _Detail.TxnAmt = ConverterDecimal(execResult["TxnAmt"]);
                    }
                    return _Detail;
            }
            return new VoiceAuthDetail();
        }

        public async Task<MsgRetriever> WebManualTxnValidate(VoiceAuthEntryViewModel ViewModel, List<VoiceAuthProducts> list)
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("ProdCd");
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("AmtPts", typeof(decimal));
            dt.Columns.Add("FastTrack", typeof(decimal));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("Seq", typeof(int));
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var DrRow = dt.NewRow();
                    DrRow["Id"] = ConvertIntToDb(i + 1);
                    DrRow["ProdCd"] = list[i].SelectedProdCd;
                    DrRow["Qty"] = ConvertDecimalToDb(list[i].Qty);
                    DrRow["AmtPts"] = ConvertDecimalToDb(list[i].AmtPoints);
                    DrRow["FastTrack"] = ConvertDecimalToDb(list[i].FastTrack);
                    DrRow["UnitPrice"] = ConvertDecimalToDb(list[i].UnitPrice);
                    DrRow["Seq"] = ConvertIntToDb(i + 1);
                    dt.Rows.Add(DrRow);
                }
            }

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[10];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = new SqlParameter("@AcqNo", "1");
            Parameters[2] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.SelectedTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", ViewModel._VoiceAuthDetail.SelectedTxnCd);
            Parameters[3] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", ViewModel._VoiceAuthDetail.BusnLocation);
            Parameters[4] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.TxnAmt) ? new SqlParameter("@Amt", DBNull.Value) : new SqlParameter("@Amt", ConvertDecimalToDb(ViewModel._VoiceAuthDetail.TxnAmt));
            Parameters[5] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", ViewModel._VoiceAuthDetail.CardNo);
            Parameters[6] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.termId) ? new SqlParameter("@TermId", DBNull.Value) : new SqlParameter("@TermId", ViewModel._VoiceAuthDetail.termId);
            Parameters[7] = new SqlParameter("@ProdDetail", dt);
            //1 missing WebManualTxnValidate
            Parameters[8] = new SqlParameter("@AuthNo", SqlDbType.VarChar, 19);
            Parameters[8].Direction = ParameterDirection.Output;
            Parameters[9] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[9].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebManualTxnValidate", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            this.AuthId = Convert.ToString(Cmd.Parameters["@AuthNo"].Value);
            var Descp = GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return await Descp;
        }

        public async Task<MsgRetriever> WebAuthMaint(VoiceAuthEntryViewModel ViewModel, List<VoiceAuthProducts> list, string Processed)
        {

            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("ProdCd");
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("AmtPts", typeof(decimal));
            dt.Columns.Add("FastTrack", typeof(decimal));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("Seq", typeof(int));
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var DrRow = dt.NewRow();
                    DrRow["Id"] = ConvertIntToDb(i + 1);
                    DrRow["ProdCd"] = list[i].SelectedProdCd;
                    DrRow["Qty"] = ConvertDecimalToDb(list[i].Qty);
                    DrRow["AmtPts"] = ConvertDecimalToDb(list[i].AmtPoints);
                    DrRow["FastTrack"] = ConvertDecimalToDb(list[i].FastTrack);
                    DrRow["UnitPrice"] = ConvertDecimalToDb(list[i].UnitPrice);
                    DrRow["Seq"] = ConvertIntToDb(i + 1);
                    dt.Rows.Add(DrRow);
                }
            }
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[12];
            Parameters[0] = new SqlParameter("@PrcsInd",Processed.Trim());
            Parameters[1] = new SqlParameter("@AcqNo", "1");
            Parameters[2] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", ViewModel._VoiceAuthDetail.CardNo);
            Parameters[3] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", ViewModel._VoiceAuthDetail.AcctNo);
            Parameters[4] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", ViewModel._VoiceAuthDetail.BusnLocation);
            Parameters[5] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.SelectedTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", ViewModel._VoiceAuthDetail.SelectedTxnCd);
            Parameters[6] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.TxnAmt) ? new SqlParameter("@Amt", DBNull.Value) : new SqlParameter("@Amt", ConvertDecimalToDb(ViewModel._VoiceAuthDetail.TxnAmt));
            Parameters[7] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.termId) ? new SqlParameter("@TermId", DBNull.Value) : new SqlParameter("@TermId", ViewModel._VoiceAuthDetail.termId);
            Parameters[8] = new SqlParameter("@Product", dt);
            Parameters[9] = new SqlParameter("@Ids", SqlDbType.VarChar, 19);
            Parameters[9].Direction = ParameterDirection.Output;
            Parameters[10] = String.IsNullOrEmpty(ViewModel._VoiceAuthDetail.AuthNo) ? new SqlParameter("@AuthNo", DBNull.Value) : new SqlParameter("@AuthNo", ViewModel._VoiceAuthDetail.AuthNo);
            Parameters[11] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[11].Direction = ParameterDirection.ReturnValue;
            var Cmd = objDataEngine.ExecuteWithReturnValue("WebAuthMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            this.AuthId = Convert.ToString(Cmd.Parameters["@AuthNo"].Value);
            var Descp = GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return await Descp;
        }
    }
}
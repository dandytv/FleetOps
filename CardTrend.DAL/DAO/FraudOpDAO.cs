using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.ControlList;
using CardTrend.Domain.Dto.Fraud;
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
    public interface IFraudOpDAO
    {
        Task<List<FraudCaseDTO>> FtFraudCaseList();
        Task<WebFraudDetailDTO> GetFraudByEventId(string eventId);
        Task<List<FraudCustomerDetailsDTO>> FraudCustomerDetailsList(string acctNo, string eventID);
        Task<IList<SelectListItem>> GetFraudDropdown(int ind, string dropDown, string showList, Int64? eventID, Int64? acctNo);
        Task<IList<WebFraudDetailDTO>> GetCardNoListByAcctNo(int ind, string dropDown, string showList, string acctNo, string eventId);
        Task<List<FraudCardDetailDTO>> FraudCardDetailsList(List<string> fraudCards, string account, string eventId);
        Task<List<WebFraudDetailDTO>> GetCardNoByAcctNo(int ind, string dropDown, string showList, string acctNo, string eventID);
        Task<List<FraudTxnDisputeDTO>> GetFraudTxnSearch(Int64 eventID, int searchType, int? txnCategory, int? txnCode, string acctNo, string cardNo, string fromDate, string toDate);
        Task<int> SaveTxn(List<string> liTxnId, string eventId, string acctNo, string cardNo, string isPostedDispute);
        Task<IssMessageDTO> SaveFraud(FraudCustomerDetailsDTO fraudCustomerDetail, FraudCardDetailDTO fraudCardDetail, WebFraudDetailDTO webFraudDetail, string userId);
    }
    public class FraudOpDAO : DAOBase, IFraudOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public FraudOpDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<List<FraudCaseDTO>> FtFraudCaseList()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var fraudCases = await cardtrendentities.Database.SqlQuery<FraudCaseDTO>(BuildSqlCommand("WebFraudCaseList", paramCollection), paramCollection.ToArray()).ToListAsync();

                return fraudCases;
            }
        }
        public async Task<WebFraudDetailDTO> GetFraudByEventId(string eventId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), null, null, "Y", Convert.ToInt16(eventId), null };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@IND",
                                        "@DropDown",
                                        "@ShowList",
                                        "@EventID",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var fraudDetail = await cardtrendentities.Database.SqlQuery<WebFraudDetailDTO>(BuildSqlCommand("WebFraudList", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return fraudDetail;
            }
        }
        public async Task<IList<WebFraudDetailDTO>> GetCardNoListByAcctNo(int ind, string dropDown, string showList, string acctNo, string eventId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), ind, dropDown, showList, !string.IsNullOrEmpty(eventId) ? (Object)Convert.ToInt16(eventId) : null, acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@IND",
                                        "@DropDown",
                                        "@ShowList",
                                        "@EventID",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var fraudDetails = await cardtrendentities.Database.SqlQuery<WebFraudDetailDTO>(BuildSqlCommand("WebFraudList", paramCollection), paramCollection.ToArray()).ToListAsync();
                return fraudDetails;
            }
        }
        public async Task<List<FraudCustomerDetailsDTO>> FraudCustomerDetailsList(string acctNo, string eventID)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, eventID };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@EventID"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var fraudCases = await cardtrendentities.Database.SqlQuery<FraudCustomerDetailsDTO>(BuildSqlCommand("WebFraudCustomerDetails", paramCollection), paramCollection.ToArray()).ToListAsync();

                return fraudCases;
            }
        }
        public async Task<List<FraudCardDetailDTO>> FraudCardDetailsList(List<string> fraudCards, string account, string eventId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                DataTable dtCardNo = null;
                if (fraudCards != null && fraudCards.Count() > 0)
                {
                    dtCardNo = new DataTable();
                    dtCardNo.Columns.Add("CardNo");
                    foreach (var itm in fraudCards)
                    {
                        DataRow dr = dtCardNo.NewRow();
                        dr["CardNo"] = itm;
                        dtCardNo.Rows.Add(dr);
                    }
                }
                var parameters = new object[] {};
                if(string.IsNullOrEmpty(eventId))
                  parameters = new object[] { Common.Helpers.Common.GetIssueNo(), account,null, dtCardNo };
                else
                  parameters = new object[] { Common.Helpers.Common.GetIssueNo(), account,NumberExtensions.ConvertLongToDb(eventId), dtCardNo };

                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@EventID"
                                   };
                var structuredPara = new object[] { new ColumnInfo { FieldName = "@CardNo", DataType = "FraudCardNumbers", Value = dtCardNo } };
                var paramCollection = BuildParameterStructuredParam(parameters, structuredPara, paramNameList);
                var fraudCardDetails = await cardtrendentities.Database.SqlQuery<FraudCardDetailDTO>(BuildSqlCommand("WebFraudCardDetails", paramCollection), paramCollection.ToArray()).ToListAsync();

                return fraudCardDetails;
            }
        }
        public async Task<List<WebFraudDetailDTO>> GetCardNoByAcctNo(int ind, string dropDown, string showList, string acctNo, string eventID)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), ind, dropDown, showList, eventID, acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@IND",
                                        "@DropDown",
                                        "@ShowList",
                                        "@EventID",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var fraudDetail = await cardtrendentities.Database.SqlQuery<WebFraudDetailDTO>(BuildSqlCommand("WebFraudList", paramCollection), paramCollection.ToArray()).ToListAsync();
                return fraudDetail;
            }
        }
        public async Task<List<FraudTxnDisputeDTO>> GetFraudTxnSearch(Int64 eventID, int searchType, int? txnCategory, int? txnCode, string acctNo, string cardNo, string fromDate,string toDate)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), eventID, searchType, txnCategory, txnCode, acctNo, cardNo, NumberExtensions.ConvertDatetimeDB(fromDate), NumberExtensions.ConvertDatetimeDB(toDate) };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@EventID",
                                        "@SearchType",
                                        "@TxnCategory",
                                        "@TxnCode",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@FromDate",
                                        "@ToDate"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var fraudDisputeTxn = await cardtrendentities.Database.SqlQuery<FraudTxnDisputeDTO>(BuildSqlCommand("WebFraudDisputeTxnList", paramCollection), paramCollection.ToArray()).ToListAsync();
                return fraudDisputeTxn;
            }
        }
        public async Task<int> SaveTxn(List<string> liTxnId, string eventId, string acctNo, string cardNo, string isPostedDispute)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                DataTable dtFraudTxnDisputeViewModel = null;
                if (liTxnId != null && liTxnId.Count() > 0)
                {
                    dtFraudTxnDisputeViewModel = new DataTable();
                    dtFraudTxnDisputeViewModel.Columns.Add("TxnId");

                    for (int i = 0; i < liTxnId.Count(); i++)
                    {
                        DataRow dr = dtFraudTxnDisputeViewModel.NewRow();
                        dr["TxnId"] = Convert.ToString(liTxnId[i]);

                        dtFraudTxnDisputeViewModel.Rows.Add(dr);
                    }
                }

                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@EventID", SqlDbType.BigInt) {SqlValue = (object)NumberExtensions.ConvertLong(eventId) ?? DBNull.Value},
                    new SqlParameter("@AcctNo", SqlDbType.BigInt) {SqlValue = (object)acctNo?? DBNull.Value},
                    new SqlParameter("@CardNo", SqlDbType.BigInt) {SqlValue = (object)cardNo?? DBNull.Value},
                    new SqlParameter("@IND", SqlDbType.SmallInt) {SqlValue = (object)NumberExtensions.ConvertIntToDb(isPostedDispute)?? DBNull.Value},
                    new SqlParameter("@TxnIdTable", SqlDbType.Structured) {SqlValue = dtFraudTxnDisputeViewModel,TypeName ="ReferencesTableType"},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };
                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebFraudDisputeTxnMaint @IssNo,@EventID,@AcctNo,@CardNo,@IND," +
                           "@TxnIdTable", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<IssMessageDTO> SaveFraud(FraudCustomerDetailsDTO fraudCustomerDetail, FraudCardDetailDTO fraudCardDetail, WebFraudDetailDTO webFraudDetail, string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                string accountNo = null;
                if (fraudCardDetail.FraudCards.Count() > 0)
                    accountNo = fraudCardDetail.FraudCards[0].AcctNo;
                DataTable dtCardNo = null;
                if (fraudCardDetail.FraudCards != null && fraudCardDetail.FraudCards.Count() > 0)
                {
                    dtCardNo = new DataTable();
                    dtCardNo.Columns.Add("CardNo");
                    foreach (var itm in fraudCardDetail.FraudCards)
                    {
                        if (!String.IsNullOrEmpty(itm.SelectedCardNo))
                        {
                            DataRow dr = dtCardNo.NewRow();
                            dr["CardNo"] = itm.SelectedCardNo;
                            dtCardNo.Rows.Add(dr);
                        }
                    }
                }

                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@EventID", SqlDbType.BigInt) {SqlValue = (object)fraudCustomerDetail.EventID ?? DBNull.Value},
                    new SqlParameter("@AcctNo", SqlDbType.BigInt) {SqlValue = (object)accountNo?? DBNull.Value},
                    new SqlParameter("@CardNo",SqlDbType.Structured) {SqlValue = dtCardNo,TypeName ="FraudCardNumbers"},
                    new SqlParameter("@ReportedBy", SqlDbType.NVarChar) {SqlValue = (object)webFraudDetail.ReportedBy ?? DBNull.Value},
                    new SqlParameter("@ReportedVia", SqlDbType.NVarChar) {SqlValue = (object)webFraudDetail.ReportVia?? DBNull.Value},
                    new SqlParameter("@IncidentDate", SqlDbType.DateTime) {SqlValue = (object)webFraudDetail.IncidentDate?? DBNull.Value},
                    new SqlParameter("@EstimatedAmountDispute", SqlDbType.Money) {SqlValue = (object)webFraudDetail.DisputeAmt ?? DBNull.Value},                   
                    new SqlParameter("@NatureOfIncident", SqlDbType.VarChar) {SqlValue = (object)webFraudDetail.NatureOfIncident?? DBNull.Value},                   
                    new SqlParameter("@IfOthers", SqlDbType.NVarChar) {SqlValue = (object)webFraudDetail.OtherNatureOfIncident?? DBNull.Value},         
                    new SqlParameter("@Description",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.Descp?? DBNull.Value},

                    new SqlParameter("@InvestigatedBy",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.InvestigatedBy?? DBNull.Value},
                    new SqlParameter("@InvestigateDate",SqlDbType.DateTime){SqlValue = (object)webFraudDetail.InvestigationDate?? DBNull.Value},
                    new SqlParameter("@InvestigateVenue",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.InvestigationVenue?? DBNull.Value},
                    new SqlParameter("@CaseBackground",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.CaseBackGround?? DBNull.Value},
                    new SqlParameter("@InvestigationProcess",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.InvestigationProcess?? DBNull.Value},
                    new SqlParameter("@Finding",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.Findings?? DBNull.Value},
                    new SqlParameter("@ActionTaken",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.ActionTaken?? DBNull.Value},
                    new SqlParameter("@RecommendationPlan",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.Recommendation?? DBNull.Value},
                    new SqlParameter("@Conclusion",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.Conclusion?? DBNull.Value},
                    new SqlParameter("@Status",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.Sts?? DBNull.Value},

                    new SqlParameter("@PreparedByName1",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.PreparedByName1?? DBNull.Value},
                    new SqlParameter("@PreparedByPosition1",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.PreparedByPosition1?? DBNull.Value},
                    new SqlParameter("@PreparedByName2",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.PreparedByName2?? DBNull.Value},
                    new SqlParameter("@PreparedByPosition2",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.PreparedByPosition2?? DBNull.Value},
                    new SqlParameter("@ReviewerName1",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.ReviewerName1?? DBNull.Value},

                    new SqlParameter("@ReviewerPosition1",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.ReviewerPosition1?? DBNull.Value},
                    new SqlParameter("@ReviewerName2",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.ReviewerName2?? DBNull.Value},

                    new SqlParameter("@ReviewerPosition2",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.ReviewerPosition2?? DBNull.Value},

                    new SqlParameter("@EndorsedByName1",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.EndorsedByName1?? DBNull.Value},
                    new SqlParameter("@EndorsedByPosition1",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.EndorsedByPosition1?? DBNull.Value},
                    new SqlParameter("@EndorsedByName2",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.EndorsedByName2?? DBNull.Value},
                    new SqlParameter("@EndorsedByPosition2",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.EndorsedByPosition2?? DBNull.Value},
                    new SqlParameter("@ApprovedByName1",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.ApprovedByName1?? DBNull.Value},

                    new SqlParameter("@ApprovedByPosition1",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.ApprovedByPosition1?? DBNull.Value},
                    new SqlParameter("@ApprovedByName2",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.ApprovedByName2?? DBNull.Value},
                    new SqlParameter("@ApprovedByPosition2",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.ApprovedByPosition2?? DBNull.Value},

                    new SqlParameter("@Remarks",SqlDbType.NVarChar){SqlValue = (object)webFraudDetail.Remarks?? DBNull.Value},
                    new SqlParameter("@UserID",SqlDbType.NVarChar){SqlValue = userId},
                    new SqlParameter("@OutputID", SqlDbType.BigInt) {Direction = ParameterDirection.Output},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebFraudIncidentReportMaint @IssNo,@EventID,@AcctNo,@CardNo,@ReportedBy," +
                                                                        "@ReportedVia,@IncidentDate,@EstimatedAmountDispute,@NatureOfIncident,@IfOthers,@Description," +
                                                                        "@InvestigatedBy,@InvestigateDate,@InvestigateVenue,@CaseBackground,@InvestigationProcess,@Finding," +
                                                                        "@ActionTaken,@RecommendationPlan,@Conclusion,@Status,@PreparedByName1,@PreparedByPosition1,@PreparedByName2,"+
                                                                        "@PreparedByPosition2,@ReviewerName1,@ReviewerPosition1,@ReviewerName2,@ReviewerPosition2,@EndorsedByName1,"+
                                                                        "@EndorsedByPosition1,@EndorsedByName2,@EndorsedByPosition2,@ApprovedByName1,@ApprovedByPosition1,@ApprovedByName2,@ApprovedByPosition2,@Remarks,@UserID,@OutputID OUT", parameters);

                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var eventId = parameters.Where(x => x.ParameterName == "@OutputID").FirstOrDefault().Value;
                return new IssMessageDTO()
                {
                    Flag = Convert.ToInt32(resultCode),
                    paraOut = { BatchId = Convert.ToString(eventId)}
                };
            }
        }
        public async Task<IList<SelectListItem>> GetFraudDropdown(int ind, string dropDown, string showList, Int64? eventID, Int64? acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), ind, dropDown, showList, eventID, acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@IND",
                                        "@DropDown",
                                        "@ShowList",
                                        "@EventID",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<WebDropDownInfoDTO>
                                    (BuildSqlCommand("WebFraudList", paramCollection), paramCollection.ToArray())
                                    .ToListAsync();

                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Name,
                            Value = refLib.Name,
                            Selected = refLib.Name == "1"?true:false
                        });
                    }
                }

                return list;
            }
        }
    }
}

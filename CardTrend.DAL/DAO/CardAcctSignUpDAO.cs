using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.Application;
using CardTrend.Domain.Dto.CardHolder;
using CardTrend.Domain.Dto.Corporate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
//using System.Data.Objects.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface ICardAcctSignUpDAO
    {
        Task<AccountCaoDTO> GetCAOGeneralInfo(string acctNo = null, string appId = null);
        Task<AcctSignUpDTO> GetApplicationGeneralInfo(string applId);
        Task<MiscellaneousInfoDTO> GetMiscellaneousInfoDetail(int applId);
        Task<AccountCaoDTO> GetAcctDepositInfoDetail(string applid = null, string acctNo = null, string txnid = null, string corpCd = null);
        Task<VeloctyLimitListMaintDTO> GetCustAcctVelocityDetail(VeloctyLimitListMaintDTO velocityDetail);
        Task<List<MilestoneDTO>> WebMilestoneListSelect(string userId, string workflowCd, int Ind);
        Task<IList<CardListDTO>> FtCardHolderList(string accountNo);
        Task<IList<SPOMilestoneDTO>> WebSPOMilestoneList(string status);
        Task<List<VehicleDTO>> GetVehicleList(string acctNo, string applId);
        Task<List<WebAddressDTO>> GetAddressList(string refTo, string refKey);
        Task<List<IssContactDTO>> GetContactlist(string refTo, string refKey);
        Task<VehicleDTO> GetVehicleDetail(string appcId, string cardNo, string vehRegtNo);
        Task<WebAddressDTO> GetAddressDetail(string refTo, string refKey, string refCd);
        Task<int> SaveAddressList(WebAddressDTO webAddressDTO, string refTo, string refCd, string refKey, string func, string userId);
        Task<IssContactDTO> GetContactDetail(string refTo, string refKey, string refCd);
        Task<List<GetAcctSignUpDTO>> GetAcctSignUpList(string applicationId, string page);
        Task<List<VeloctyLimitListMaintDTO>> GetCustAcctVelocityList(VeloctyLimitListMaintDTO custAcctVelocity);
        Task<List<CreditAssesOperationDTO>> GetAcctDepositInfoList(string applid = null, string acctNo = null, string CorpCd = null);
        Task<List<MilestoneDTO>> GetMilestoneHistorySelect(string workFlowCd, Int64 RefKey);
        Task<List<SkdsDTO>> GetSKDSList(string accountNo, string applId, string page);
        Task<SkdsDTO> GetSKDSDetail(string accountNo, string applId, string txnId);
        Task<MilestoneDTO> MilestoneInfo(string workflowCd, int taskNo);
        Task<MilestoneDTO> WebMilestoneApplValidation(Int64 applId);
        Task<int> SaveMilestoneAdj(MilestoneDTO mileStone);
        Task<IssMessageDTO> SaveApplicationGeneralInfoResult(AcctSignUpDTO acctSgUp);
        Task<int> SaveApprovalMilestone(MilestoneDTO milestone);
        Task<string> DelCustAcctVelocity(string accNo, string cardNo, string applId, string appcId, string velInd, string prodCd, string costCenter, string corpCd);
        Task<string> SaveCustAcctVelocity(VeloctyLimitListMaintDTO veloctyLimit, string applId, string func);
        Task<int> SaveCreditAssessmentOperation(AccountCaoDTO accountCaoDTO, string userId);
        Task<int> SaveVehicleList(VehicleDTO vehicleDTO, string userId);
        Task<int> SaveContactsList(IssContactDTO issContactDTO, string refTo, string func);
        Task<int> DelContact(string refTo, string refKey, string refCd);
        Task<int> DelAddress(string refTo, string refKey, string refCd, string userId);
        Task<int> SaveMiscellaneousInfo(MiscellaneousInfoDTO miscellaneousInfo);
        Task<int> SaveSKDS(SkdsDTO skds);
    }

    public class CardAcctSignUpDAO : DAOBase, ICardAcctSignUpDAO
    {
        private readonly string _connectionString = string.Empty;
        public CardAcctSignUpDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<IList<CardListDTO>> FtCardHolderList(string accountNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CardListDTO>
                    (BuildSqlCommand("WebCardListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                return results;
            }
        }
        public async Task<IList<SPOMilestoneDTO>> WebSPOMilestoneList(string status)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { status, null };
                var paramNameList = new[]
                                   {
                                        "@Ind",
	                                    "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var spos = await cardtrendentities.Database.SqlQuery<SPOMilestoneDTO>
                     (BuildSqlCommand("WebSPOMilestoneListSelect", paramCollection), paramCollection.ToArray())
                     .ToListAsync();
                return spos;
            }
        }
        public async Task<AcctSignUpDTO> GetApplicationGeneralInfo(string applId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), applId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@ApplId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<AcctSignUpDTO>
                    (BuildSqlCommand("WebApplGeneralInfoSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();

                return results;
            }
        }
        public async Task<AccountCaoDTO> GetAcctDepositInfoDetail(string applid = null, string acctNo = null, string txnid = null, string corpCd = null)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), txnid, applid, acctNo, corpCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@TxnId",
                                        "@ApplId",
                                        "@AcctNo",
                                        "@CorpCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<AccountCaoDTO>(BuildSqlCommand("WebAcctDepositInfoSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<VeloctyLimitListMaintDTO> GetCustAcctVelocityDetail(VeloctyLimitListMaintDTO velocityDetail)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), velocityDetail.AccNo, velocityDetail.CardNo, velocityDetail.ApplId, velocityDetail.AppcId, velocityDetail.SelectedCorpCd, velocityDetail.CostCentre, velocityDetail.VelocityIndicator, "0" };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo",
                                        "@CardNo",
                                        "@ApplId",
                                        "@AppcId",
                                        "@CorpCd",
                                        "@CostCentre",
                                        "@VelocityInd",
                                        "@ProdCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<VeloctyLimitListMaintDTO>(BuildSqlCommand("WebVelocityLimitSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<IssMessageDTO> SaveApplicationGeneralInfoResult(AcctSignUpDTO acctSgUp)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new[] { 
                new SqlParameter("@IssNo", SqlDbType.Int) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                new SqlParameter("@PlasticType", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.PlasticType?? DBNull.Value},
                new SqlParameter("@CycNo", SqlDbType.TinyInt) {SqlValue = (object)acctSgUp.CycNo?? DBNull.Value},
                new SqlParameter("@ApplId", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.ApplId?? DBNull.Value},
                new SqlParameter("@AcctNo", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.AcctNo?? DBNull.Value},

                new SqlParameter("@CorpCd", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.CorpCd?? DBNull.Value},
                new SqlParameter("@ApplRef", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.Reference?? DBNull.Value},
                new SqlParameter("@CmpyLegalName", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.CmpyLegalName?? DBNull.Value},
                new SqlParameter("@CmpyName", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.CmpyName?? DBNull.Value},
                new SqlParameter("@CmpyEmbName", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.CmpyEmbName?? DBNull.Value},

                new SqlParameter("@ContactPerson", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.ContactPerson?? DBNull.Value},
                new SqlParameter("@Position", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.Position?? DBNull.Value},
                new SqlParameter("@BusnCategory", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.BusnCategory?? DBNull.Value},
                new SqlParameter("@OfficePhone", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.OfficePhone?? DBNull.Value},
                new SqlParameter("@MobileNo", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.MobileNo?? DBNull.Value},

                new SqlParameter("@OfficeFax", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.OfficeFax?? DBNull.Value},
                new SqlParameter("@SAPNo", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.SAPNo?? DBNull.Value},
                new SqlParameter("@EmailAddr", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.EmailAddr?? DBNull.Value},
                new SqlParameter("@CmpyType", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.CmpyType?? DBNull.Value},
                new SqlParameter("@CmpyRegsNo", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.CmpyRegsNo?? DBNull.Value},

                new SqlParameter("@CmpyRegsDate", SqlDbType.Date) {SqlValue = (object)acctSgUp.CmpyRegsDate?? DBNull.Value},
                new SqlParameter("@NatureOfBusn", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.NatureOfBusn?? DBNull.Value},
                new SqlParameter("@BillingMethod", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.BillMethod?? DBNull.Value},
                new SqlParameter("@InvoicePref", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.InvoicePref?? DBNull.Value},
                new SqlParameter("@InvBillInd", SqlDbType.Char) {SqlValue = (object)acctSgUp.InvBillInd?? DBNull.Value},

                new SqlParameter("@PymtInd", SqlDbType.Char) {SqlValue = (object)acctSgUp.PymtInd ?? DBNull.Value},
                new SqlParameter("@VehPerfRptInd", SqlDbType.Char) {SqlValue =(object)acctSgUp.VehPerfRptInd ??DBNull.Value},
                new SqlParameter("@LoyaltyCardNo", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.LoyaltyCardNo?? DBNull.Value},
                new SqlParameter("@LoyaltyFullName", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.LoyaltyFullName ?? DBNull.Value},
                new SqlParameter("@LoyaltyIcNo", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.LoyaltyIcNo ?? DBNull.Value},

                new SqlParameter("@LoyaltyContactNo", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.LoyaltyContactNo?? DBNull.Value},
                new SqlParameter("@LoyaltyeBusn", SqlDbType.Char) {SqlValue = (object)acctSgUp.LoyaltyeBusn??DBNull.Value},
                new SqlParameter("@EntityId", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.EntityId ?? DBNull.Value},
                new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = acctSgUp.UserId},
                new SqlParameter("@TaxCategory", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.TaxCategory ?? DBNull.Value},

                new SqlParameter("@WithHoldingTax", SqlDbType.Char) {SqlValue = (object)acctSgUp.WithHoldingTax?? DBNull.Value},
                new SqlParameter("@oApplId", SqlDbType.VarChar,19) {Direction = ParameterDirection.Output},
                new SqlParameter("@oEntityId", SqlDbType.VarChar,19) {Direction = ParameterDirection.Output},
                new SqlParameter("@DocPath", SqlDbType.VarChar,150) {Direction = ParameterDirection.Output},
                new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output},

                new SqlParameter("@LangId", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.LangId ?? DBNull.Value},
                new SqlParameter("@Website", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.Website ?? DBNull.Value},
                new SqlParameter("@ClientClass", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.ClientClass ?? DBNull.Value},
                new SqlParameter("@ClientType", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.ClientType ?? DBNull.Value},
                new SqlParameter("@ReasonCd", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.ReasonCd ?? DBNull.Value},

                new SqlParameter("@AuthName", SqlDbType.VarChar) {SqlValue = (object)acctSgUp.AuthName ?? DBNull.Value}
                };

                await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = WebApplGeneralInfoMaint @IssNo,@PlasticType,@CycNo,@ApplId,@AcctNo" +
                                                                                              ",@CorpCd,@ApplRef,@CmpyLegalName,@CmpyName,@CmpyEmbName" +
                                                                                             ",@ContactPerson,@Position,@BusnCategory,@OfficePhone,@MobileNo" +
                                                                                             ",@OfficeFax,@SAPNo,@EmailAddr,@CmpyType,@CmpyRegsNo" +
                                                                                             ",@CmpyRegsDate,@NatureOfBusn,@BillingMethod,@InvoicePref,@InvBillInd" +
                                                                                             ",@PymtInd,@VehPerfRptInd,@LoyaltyCardNo,@LoyaltyFullName,@LoyaltyIcNo" +
                                                                                             ",@LoyaltyContactNo,@LoyaltyeBusn,@EntityId,@UserId,@TaxCategory,@WithHoldingTax,@oApplId OUT,@oEntityId OUT,@DocPath OUT,@LangId" +
                                                                                             ",@Website,@ClientClass,@ClientType,@ReasonCd,@AuthName", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var appId = parameters.Where(x => x.ParameterName == "@oApplId").FirstOrDefault().Value;
                var entityId = parameters.Where(x => x.ParameterName == "@oEntityId").FirstOrDefault().Value;
                var docPath = parameters.Where(x => x.ParameterName == "@DocPath").FirstOrDefault().Value;
                issMessage.Flag = Convert.ToInt32(resultCode);
                issMessage.paraOut.ApplId = Convert.ToString(appId);
                issMessage.paraOut.EntityId = Convert.ToString(entityId);
                issMessage.paraOut.DocPath = Convert.ToString(docPath);
                return issMessage;
            }
        }
        public async Task<int> SaveMilestoneAdj(MilestoneDTO mileStone)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { mileStone.Id, mileStone.TaskNo, mileStone.aprId, mileStone.RefNo,mileStone.SelectedOwner, mileStone.Priority
                                               ,mileStone.Remarks,mileStone.ReasonCd,mileStone.RecallDate,mileStone.Sts,mileStone.UserId};

                var paramNameList = new[]
                                    {
                                        "@Id",
	                                    "@TaskNo",
                                        "@RefKey",
                                        "@RefNo",
                                        "@Owner",
                                        "@Priority",
                                        "@Remarks",
                                        "@ReasonCd",
                                        "@RecallDate",
                                        "@Sts",
                                        "@UserId"
                                    };

                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync
                    (BuildSqlCommandWithRrn("WebMilestoneAdjustmentApproval", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveApprovalMilestone(MilestoneDTO milestone)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                SqlParameter[] parameters;
                //cardtrendentities.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
                if (milestone.ActionSP.ToLower() == "WebMilestoneSPOApproval".ToLower())
                {
                    parameters = new[] { 
                    new SqlParameter("@Id", SqlDbType.BigInt) {SqlValue = Common.Extensions.NumberExtensions.ConvertLongToDb(milestone.Id)}, 
                    new SqlParameter("@TaskNo", SqlDbType.Int) {SqlValue = (object)milestone.TaskNo?? DBNull.Value},
                    new SqlParameter("@RefKey", SqlDbType.BigInt) {SqlValue = (object) milestone.aprId?? DBNull.Value},
                    new SqlParameter("@RefNo", SqlDbType.NVarChar) {SqlValue = string.Empty},
                    new SqlParameter("@Owner", SqlDbType.NVarChar) {SqlValue = (object)milestone.SelectedOwner?? DBNull.Value},
                    new SqlParameter("@Priority", SqlDbType.SmallInt) {SqlValue = (object)Convert.ToInt16(milestone.Priority)?? DBNull.Value},
                    new SqlParameter("@Remarks", SqlDbType.NVarChar) {SqlValue = (object)milestone.Remarks?? DBNull.Value},
                    new SqlParameter("@ReasonCd", SqlDbType.VarChar) {SqlValue = (object)milestone.ReasonCd?? DBNull.Value},
                    new SqlParameter("@RecallDate", SqlDbType.VarChar) {SqlValue = (object)Common.Extensions.NumberExtensions.ConvertDatetimeDB(milestone.RecallDate)?? DBNull.Value},
                    new SqlParameter("@Sts", SqlDbType.VarChar) {SqlValue = (object)milestone.Sts?? DBNull.Value},
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)milestone.UserId?? DBNull.Value},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };
                    await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = " + milestone.ActionSP + " @Id,@TaskNo,@RefKey,@RefNo,@Owner," +
                                                                              "@Priority,@Remarks,@ReasonCd,@RecallDate,@Sts,@UserId", parameters);
                }
                else
                {
                    parameters = new[] { 
                    new SqlParameter("@Id", SqlDbType.BigInt) {SqlValue = Common.Extensions.NumberExtensions.ConvertLongToDb(milestone.Id)}, 
                    new SqlParameter("@TaskNo", SqlDbType.Int) {SqlValue = (object)milestone.TaskNo?? DBNull.Value},
                    new SqlParameter(milestone.ActionSP.ToLower() == "WebMilestonePukalPaymentApproval".ToLower()? "@RefKey":"@ApplId", SqlDbType.BigInt) {SqlValue = (object) milestone.aprId?? DBNull.Value},
                    new SqlParameter("@Owner", SqlDbType.NVarChar) {SqlValue = (object)milestone.SelectedOwner?? DBNull.Value},
                    new SqlParameter("@Priority", SqlDbType.SmallInt) {SqlValue = (object)Convert.ToInt16(milestone.Priority)?? DBNull.Value},
                    new SqlParameter("@Remarks", SqlDbType.NVarChar) {SqlValue = (object)milestone.Remarks?? DBNull.Value},
                    new SqlParameter("@ReasonCd", SqlDbType.VarChar) {SqlValue = (object)milestone.ReasonCd?? DBNull.Value},
                    new SqlParameter("@RecallDate", SqlDbType.VarChar) {SqlValue = (object)Common.Extensions.NumberExtensions.ConvertDatetimeDB(milestone.RecallDate)?? DBNull.Value},
                    new SqlParameter("@Sts", SqlDbType.VarChar) {SqlValue = (object)milestone.Sts?? DBNull.Value},
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)milestone.UserId?? DBNull.Value},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };
                    var appl = milestone.ActionSP.ToLower() == "WebMilestonePukalPaymentApproval".ToLower() ? "@RefKey" : "@ApplId";
                    await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = " + milestone.ActionSP + " @Id,@TaskNo," + appl + ",@Owner," +
                                                                              "@Priority,@Remarks,@ReasonCd,@RecallDate,@Sts,@UserId", parameters);
                }

                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<IssContactDTO> GetContactDetail(string refTo, string refKey, string refCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refTo, refKey, refCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefTo",
                                        "@RefKey",
                                        "@RefCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<IssContactDTO>(BuildSqlCommand("WebContactSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<List<VehicleDTO>> GetVehicleList(string acctNo, string applId)
        {

            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, applId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo",
	                                    "@ApplId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<VehicleDTO>
                     (BuildSqlCommand("WebVehicleListSelect", paramCollection), paramCollection.ToArray())
                     .ToListAsync();
                return result;
            }
        }
        public async Task<List<MilestoneDTO>> WebMilestoneListSelect(string userId,string workflowCd,int Ind)
        {
            var result = new List<MilestoneDTO>();
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { userId, workflowCd, Ind };
                var paramNameList = new[]
                                   {
                                        "@UserId",
	                                    "@WorkflowCd",
	                                    "@Ind"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                result = await cardtrendentities.Database.SqlQuery<MilestoneDTO>
                    (BuildSqlCommand("WebMilestoneListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();
                if (result.Count() > 0)
                {
                    foreach (var mileStone in result)
                    {
                        mileStone.WorkflowCd = workflowCd;
                    }
                }
            }

            return result;
        }
        public async Task<List<MilestoneDTO>> GetMilestoneHistorySelect(string workFlowCd, Int64 RefKey)
        {
            var milestone = new List<MilestoneDTO>();
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { String.IsNullOrEmpty(workFlowCd) ? "APPL" : workFlowCd, RefKey };
                var paramNameList = new[]
                                   {
                                        "@WorkflowCd",
	                                    "@RefKey"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                milestone = await cardtrendentities.Database.SqlQuery<MilestoneDTO>
                       (BuildSqlCommand("WebMilestoneHistorySelect", paramCollection), paramCollection.ToArray())
                       .ToListAsync();

                return milestone;
            }
        }
        public async Task<List<GetAcctSignUpDTO>> GetAcctSignUpList(string applicationId, string page)
        {

            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), applicationId, page };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@ApplId",
	                                    "@Page"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<GetAcctSignUpDTO>
                     (BuildSqlCommand("WebApplListSelect", paramCollection), paramCollection.ToArray())
                     .ToListAsync();
                return result;
            }

        }
        public async Task<List<WebAddressDTO>> GetAddressList(string refTo, string refKey)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refTo, refKey };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefTo",
	                                    "@RefKey"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<WebAddressDTO>
                     (BuildSqlCommand("WebAddressListSelect", paramCollection), paramCollection.ToArray())
                     .ToListAsync();
                return results;
            }
        }
        public async Task<List<VeloctyLimitListMaintDTO>> GetCustAcctVelocityList(VeloctyLimitListMaintDTO custAcctVelocity)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), custAcctVelocity.AccNo, custAcctVelocity.CardNo, custAcctVelocity.ApplId, custAcctVelocity.AppcId, custAcctVelocity.SelectedCorpCd, custAcctVelocity.CostCentre };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo",
	                                    "@CardNo",
                                        "@ApplId",
                                        "@AppcId",
                                        "@CorpCd",
                                        "@CostCentre"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<VeloctyLimitListMaintDTO>
                     (BuildSqlCommand("WebVelocityLimitListSelect", paramCollection), paramCollection.ToArray())
                     .ToListAsync();
                return result;
            }
        }
        public async Task<List<CreditAssesOperationDTO>> GetAcctDepositInfoList(string applid = null, string acctNo = null, string CorpCd = null)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), applid, acctNo, CorpCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@ApplId",
	                                    "@AcctNo",
                                        "@CorpCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CreditAssesOperationDTO>
                     (BuildSqlCommand("WebAcctDepositInfoListSelect", paramCollection), paramCollection.ToArray())
                     .ToListAsync();
                return result;
            }
        }
        public async Task<List<IssContactDTO>> GetContactlist(string refTo, string refKey)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refTo, refKey };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefTo",
                                        "@RefKey"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<IssContactDTO>(BuildSqlCommand("WebContactListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();

                return result;
            }
        }
        public async Task<MilestoneDTO> MilestoneInfo(string workflowCd, int taskNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { workflowCd, taskNo };
                var paramNameList = new[]
                                   {
                                        "@WorkflowCd",
	                                    "@TaskNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MilestoneDTO>
                     (BuildSqlCommand("WebGetMilestoneInfo", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<MilestoneDTO> WebMilestoneApplValidation(Int64 applId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), applId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@ApplId"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MilestoneDTO>
                   (BuildSqlCommand("WebMilestoneApplValidation", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<VehicleDTO> GetVehicleDetail(string appcId, string cardNo, string vehRegtNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), appcId, cardNo, vehRegtNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AppcId",
                                        "@CardNo",
                                        "@VRN"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<VehicleDTO>(BuildSqlCommand("WebVehicleSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<WebAddressDTO> GetAddressDetail(string refTo, string refKey, string refCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refTo, refKey, refCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefTo",
                                        "@RefKey",
                                        "@RefCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<WebAddressDTO>(BuildSqlCommand("WebAddressSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<AccountCaoDTO> GetCAOGeneralInfo(string acctNo = null, string appId = null)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, appId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo",
                                        "@ApplId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<AccountCaoDTO>(BuildSqlCommand("WebAcctCAOSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<MiscellaneousInfoDTO> GetMiscellaneousInfoDetail(int applId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), applId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@ApplId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MiscellaneousInfoDTO>(BuildSqlCommand("WebApplMiscInfoSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<SkdsDTO> GetSKDSDetail(string accountNo,string applId,string txnId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo, applId, txnId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo",
                                        "@ApplId",
                                        "@TxnId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<SkdsDTO>(BuildSqlCommand("WebAcctSubsidySelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<List<SkdsDTO>> GetSKDSList(string accountNo, string applId,string page)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo, applId, page };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo",
	                                    "@ApplId",
                                        "@Page"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<SkdsDTO>(BuildSqlCommand("WebAcctSubsidyListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<string> DelCustAcctVelocity(string accNo, string cardNo, string applId, string appcId, string velInd, string prodCd, string costCenter, string corpCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), (object)accNo ?? DBNull.Value, (object)cardNo ?? DBNull.Value, (object)applId ?? DBNull.Value, (object)appcId ?? DBNull.Value, string.IsNullOrEmpty(costCenter) ? null : costCenter, corpCd, velInd, prodCd };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@ApplId",
                                        "@AppcId",
                                        "@CostCentre",
                                        "@CorpCd",
                                        "@VelocityInd",
                                        "@ProdCd"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebVelocityLimitDelete", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return resultCode.ToString();
            }
        }
        public async Task<string> SaveCustAcctVelocity(VeloctyLimitListMaintDTO veloctyLimit, string applId, string func)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), func, (object)veloctyLimit.AccNo ?? DBNull.Value, (object)veloctyLimit.CardNo ?? DBNull.Value, (object)veloctyLimit.ApplId ?? DBNull.Value, veloctyLimit.AppcId, veloctyLimit.SelectedCorpCd, veloctyLimit.CostCentre, veloctyLimit.VelocityIndicator,
                                               "0",veloctyLimit.VelocityAmount,veloctyLimit.Counter,veloctyLimit.VelocityLitre,veloctyLimit.UserId};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@Func",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@ApplId",
                                        "@AppcId",
                                        "@CorpCd",
                                        "@CostCentre",
                                        "@VelocityInd",
                                        "@ProdCd",
                                        "@VelocityLimit",
                                        "@VelocityCnt",
                                        "@VelocityLitre",
                                        "@userId"

                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebVelocityLimitMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return resultCode.ToString();
            }
        }
        public async Task<int> SaveVehicleList(VehicleDTO vehicleDTO,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new[] { 
                new SqlParameter("@IssNo", SqlDbType.Int) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                new SqlParameter("@AppcId", SqlDbType.VarChar) {SqlValue = (object)vehicleDTO.AppcId?? DBNull.Value},
                new SqlParameter("@CardNo", SqlDbType.VarChar) {SqlValue = (object)vehicleDTO.CardNo?? DBNull.Value},
                new SqlParameter("@VRN", SqlDbType.NVarChar) {SqlValue = (object)vehicleDTO.VRN?? DBNull.Value},
                new SqlParameter("@SKDSInd", SqlDbType.VarChar) {SqlValue = (object)vehicleDTO.SKDSInd?? DBNull.Value},

                new SqlParameter("@SKDSQuota", SqlDbType.Money) {SqlValue = (object)vehicleDTO.SKDSQuota?? DBNull.Value},
                new SqlParameter("@Manufacturer", SqlDbType.VarChar) {SqlValue = (object)vehicleDTO.VehicleMaker?? DBNull.Value},
                new SqlParameter("@Model", SqlDbType.VarChar) {SqlValue = (object)vehicleDTO.VehicleModel?? DBNull.Value},
                new SqlParameter("@VehRegsDate", SqlDbType.Date) {SqlValue = (object)vehicleDTO.RegisteredDate?? DBNull.Value},

                new SqlParameter("@VehType", SqlDbType.VarChar) {SqlValue = (object)vehicleDTO.VehicleType?? DBNull.Value},
                new SqlParameter("@Color", SqlDbType.VarChar) {SqlValue = (object)vehicleDTO.VehicleColor?? DBNull.Value},
                new SqlParameter("@RoadTaxExpiry", SqlDbType.Date) {SqlValue = (object)vehicleDTO.RoadTaxExpiry?? DBNull.Value},
                new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)userId?? DBNull.Value},
                new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                };
                await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = WebVehicleMaint @IssNo,@AppcId,@CardNo,@VRN,@SKDSInd" +
                                                                                             ",@SKDSQuota,@Manufacturer,@Model,@VehRegsDate,@VehType" +
                                                                                            ",@Color,@RoadTaxExpiry,@UserId", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveContactsList(IssContactDTO issContactDTO, string refTo, string func)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { func, Common.Helpers.Common.GetIssueNo(), refTo, issContactDTO.RefKey, issContactDTO.ContactType, issContactDTO.ContactName, 
                                                issContactDTO.Function,issContactDTO.ContactNo, issContactDTO.ContactStatus,issContactDTO.EmailAddr,issContactDTO.UserId};
                var paramNameList = new[]
                                    {
                                        "@Func",
                                        "@IssNo",
                                        "@RefTo",
                                        "@RefKey",
                                        "@RefCd",
                                        "@ContactName",
                                        "@Occupation",
                                        "@ContactNo",
                                        "@Sts",
                                        "@EmailAddr",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebContactMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> DelContact(string refTo, string refKey, string refCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] {Common.Helpers.Common.GetIssueNo(), refTo, refKey, refCd};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@RefTo",
                                        "@RefKey",
                                        "@RefCd"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebContactDelete", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveCreditAssessmentOperation(AccountCaoDTO accountCaoDTO, string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new[] { 
                new SqlParameter("@IssNo", SqlDbType.Int) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                new SqlParameter("@AcctNo", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.AcctNo?? DBNull.Value},
                new SqlParameter("@ApplId", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.ApplId?? DBNull.Value},
                new SqlParameter("@CreditLimit", SqlDbType.Money) {SqlValue = (object)NumberExtensions.ConvertDecimalToDb(accountCaoDTO.CreditLimit)?? DBNull.Value},
                new SqlParameter("@PymtMode", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.PymtMode?? DBNull.Value},

                new SqlParameter("@PymtTerms", SqlDbType.Int) {SqlValue = (object)accountCaoDTO.PymtTerms?? DBNull.Value},
                new SqlParameter("@TxnLimit", SqlDbType.Money) {SqlValue = (object)accountCaoDTO.TxnAmtLimit?? DBNull.Value},
                new SqlParameter("@LitLimit", SqlDbType.Money) {SqlValue = (object)accountCaoDTO.TxnLitLimit?? DBNull.Value},
                new SqlParameter("@SaleTerritory", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.SaleTerritory?? DBNull.Value},
                new SqlParameter("@RiskCategory", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.RiskCategory?? DBNull.Value},

                new SqlParameter("@AssessmentType", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.AssessmentType?? DBNull.Value},
                new SqlParameter("@DirectDebitInd", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.DirectDebitInd?? DBNull.Value},
                new SqlParameter("@DepositType", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.DepositType?? DBNull.Value},
                new SqlParameter("@DepositExp", SqlDbType.Date) {SqlValue = (object)NumberExtensions.ConvertDatetimeDB(accountCaoDTO.DepositExp)?? DBNull.Value},
                new SqlParameter("@BankAcctType", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.BankAcctType?? DBNull.Value},

                new SqlParameter("@BankCd", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.BankName?? DBNull.Value},
                new SqlParameter("@BankAcctNo", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.BankAcctNo?? DBNull.Value},
                new SqlParameter("@BankBranchCd", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.BankBranchCd?? DBNull.Value},
                new SqlParameter("@DepositAmt", SqlDbType.Money) {SqlValue = (object)accountCaoDTO.DepositAmt?? DBNull.Value},
                new SqlParameter("@ValidityDate", SqlDbType.Date) {SqlValue = (object)accountCaoDTO.ValidityDate?? DBNull.Value},

                new SqlParameter("@NRID", SqlDbType.Date) {SqlValue = (Object)accountCaoDTO.NRID ?? DBNull.Value},
                new SqlParameter("@ReasonCd", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.ReasonCd?? DBNull.Value},
                new SqlParameter("@AppvUserId1", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.AppvUserId1?? DBNull.Value},
                new SqlParameter("@AppvSts1", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.AppvSts1?? DBNull.Value},
                new SqlParameter("@AppvDate1", SqlDbType.SmallDateTime) {SqlValue = (object)accountCaoDTO.AppvDate1?? DBNull.Value},


                new SqlParameter("@AppvUserId2", SqlDbType.NVarChar) {SqlValue = (object)accountCaoDTO.AppvUserId2 ?? DBNull.Value},
                new SqlParameter("@AppvSts2", SqlDbType.VarChar) {SqlValue =(object)accountCaoDTO.AppvSts2 ??DBNull.Value},
                new SqlParameter("@AppvDate2", SqlDbType.SmallDateTime) {SqlValue = (object)accountCaoDTO.AppvDate2?? DBNull.Value},
                new SqlParameter("@AppvUserId3", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.AppvUserId3 ?? DBNull.Value},
                new SqlParameter("@AppvSts3", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.AppvSts3 ?? DBNull.Value},

                new SqlParameter("@AppvDate3", SqlDbType.SmallDateTime) {SqlValue = (object)accountCaoDTO.AppvDate3?? DBNull.Value},
                new SqlParameter("@AppvUserId4", SqlDbType.NVarChar) {SqlValue = (object)accountCaoDTO.AppvUserId4??DBNull.Value},
                new SqlParameter("@AppvSts4", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.AppvSts4 ?? DBNull.Value},
                new SqlParameter("@AppvDate4", SqlDbType.SmallDateTime) {SqlValue = (object)accountCaoDTO.AppvDate4 ?? DBNull.Value},
                new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = userId},

                new SqlParameter("@Remarks", SqlDbType.NVarChar,2000) {SqlValue = (object)accountCaoDTO.Remarks?? DBNull.Value},
                new SqlParameter("@Quantitativerating", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.Quantitativerating?? DBNull.Value},
                new SqlParameter("@Qualitativerating", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.Qualitativerating?? DBNull.Value},
                new SqlParameter("@PropCreditLimit", SqlDbType.Money) {SqlValue = (object)accountCaoDTO.PropCreditLimit?? DBNull.Value},
                new SqlParameter("@RecCreditLimit",SqlDbType.Money){SqlValue = (object)accountCaoDTO.RecCreditLimit?? DBNull.Value},

                new SqlParameter("@PropSecurityAmt", SqlDbType.Money) {SqlValue = (object)accountCaoDTO.PropSecurityAmt ?? DBNull.Value},
                new SqlParameter("@RecSecurityAmt", SqlDbType.Money) {SqlValue = (object)accountCaoDTO.RecSecurityAmt ?? DBNull.Value},
                new SqlParameter("@TradingArea", SqlDbType.VarChar) {SqlValue = (object)accountCaoDTO.TradingArea ?? DBNull.Value},
                new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                };

                await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = WebAcctCAOMaint @IssNo,@AcctNo,@ApplId,@CreditLimit,@PymtMode" +
                                                                                              ",@PymtTerms,@TxnLimit,@LitLimit,@SaleTerritory,@RiskCategory" +
                                                                                             ",@AssessmentType,@DirectDebitInd,@DepositType,@DepositExp,@BankAcctType" +
                                                                                             ",@BankCd,@BankAcctNo,@BankBranchCd,@DepositAmt,@ValidityDate" +
                                                                                             ",@NRID,@ReasonCd,@AppvUserId1,@AppvSts1,@AppvDate1" +
                                                                                             ",@AppvUserId2,@AppvSts2,@AppvDate2,@AppvUserId3,@AppvSts3" +
                                                                                             ",@AppvDate3,@AppvUserId4,@AppvSts4,@AppvDate4,@UserId,@Remarks,@Quantitativerating,@Qualitativerating,@PropCreditLimit,@RecCreditLimit" +
                                                                                             ",@PropSecurityAmt,@RecSecurityAmt,@TradingArea", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveAddressList(WebAddressDTO webAddressDTO, string refTo, string refCd, string refKey, string func,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new[] { 
                new SqlParameter("@Func", SqlDbType.VarChar) {SqlValue = (object)func ?? DBNull.Value}, 
                new SqlParameter("@IssNo", SqlDbType.Int) {SqlValue = Common.Helpers.Common.GetIssueNo()},
                new SqlParameter("@RefTo", SqlDbType.VarChar) {SqlValue = (object)refTo ?? DBNull.Value},
                new SqlParameter("@RefKey", SqlDbType.NVarChar) {SqlValue = (object)refKey?? DBNull.Value},
                new SqlParameter("@RefCd", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.AddressType ?? DBNull.Value},

                new SqlParameter("@Street1", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.Street1?? DBNull.Value},
                new SqlParameter("@Street2", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.Street2?? DBNull.Value},
                new SqlParameter("@Street3", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.Street3?? DBNull.Value},
                new SqlParameter("@Street4", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.Street4?? DBNull.Value},

                new SqlParameter("@Street5", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.Street5?? DBNull.Value},
                new SqlParameter("@City", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.City?? DBNull.Value},
                new SqlParameter("@State", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.StateCd ?? DBNull.Value},
                new SqlParameter("@ZipCd", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.PostalCd?? DBNull.Value},
                new SqlParameter("@Ctry", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.Country ?? DBNull.Value},
                new SqlParameter("@Region", SqlDbType.VarChar) {SqlValue = (object)webAddressDTO.Region ?? DBNull.Value},
                new SqlParameter("@MailInd", SqlDbType.Char,1) {SqlValue = (object)webAddressDTO.MainMailing ?? DBNull.Value},
                new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)userId?? DBNull.Value},
                new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                };
                await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = WebAddressMaint @Func,@IssNo,@RefTo,@RefKey,@RefCd" +
                                                                                             ",@Street1,@Street2,@Street3,@Street4,@Street5,@City,@State,@ZipCd,@Ctry,@Region,@MailInd," +
                                                                                            "@UserId", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> DelAddress(string refTo, string refKey, string refCd,string userId)
        {

            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refTo, refKey, refCd, userId };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@RefTo",
                                        "@RefKey",
                                        "@RefCd",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebAddressDelete", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }


        }
        public async Task<int> SaveMiscellaneousInfo(MiscellaneousInfoDTO miscellaneousInfo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), miscellaneousInfo.ApplId, miscellaneousInfo.AuthName, miscellaneousInfo.Designation, miscellaneousInfo.AuthDate };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@ApplId",
                                        "@AuthName",
                                        "@Designation",
                                        "@AuthDate"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebApplMiscInfoMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveSKDS(SkdsDTO skds)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), skds.accountNo, skds.applId, skds.TxnId, skds.SKDSNo,
                                                skds.QuotaFromDate,skds.QuotaToDate,skds.Quota,skds.Reference,skds.LastSubsidyDate,
                                                skds.EffFromDate,skds.EffToDate,skds.Remarks,skds.SubsidyLevel,skds.Status,skds.UserId};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@ApplId",
                                        "@TxnId",
                                        "@SKDSNo",
                                        "@QuotaFromDate",
                                        "@QuotaToDate",
                                        "@Quota",
                                        "@Ref",
                                        "@LastSubsidyDate",
                                        "@EffFromDate",
                                        "@EffToDate",
                                        "@Remarks",
                                        "@SubsidyLevel",
                                        "@Sts",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebAcctSubsidyMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
    }
}

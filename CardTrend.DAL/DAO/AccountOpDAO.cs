using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.Corporate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface IAccountOpDAO
    {
        Task<GeneralInfoDTO> FtGeneralInfo(int accountNo);
        Task<EventLoggerDTO> GetEventLoggerDetail(string module, string eventId);
        Task<FinancialInfoDTO> FtFinancialInfoForm(int accountNo);
        Task<TempCreditCtrlDTO> FtTempCreditLimitDetail(string acctNo);
        Task<CostCentreDTO> GetCostCentreByRefToAndCostCentre(string refTo, string refKey, string costCentre);
        Task<UpToDateBalDTO> FtUpToBalDetail(string acctNo);
        Task<ProductDiscountDTO> WebProductDiscountSelect(string acctNo, string discType, string id, string refTo);
        Task<List<CostCentreDTO>> GetCostCentres(string refTo, string refKey);
        Task<List<CostCentreDTO>> WebCostCentreSearch(string refTo, string refKey, string costCentre);
        Task<List<WebSecDepRemarksDTO>> GetSecDepRemarksListSelect(string accountNo, string eventType, string txnId);
        Task<List<CreditAssesOperationDTO>> FtAcctDepositInfoList(string applid = null, string acctNo = null, string corpCd = null);
        Task<List<CreditLimitHistoryDTO>> WebAcctHistoryListSelect(Int64 AcctNo);
        Task<List<CreditLimitHistoryDTO>> WebSecHistoryDepositList(string AccountNo);
        Task<List<EventLoggerDTO>> WebEventSearchWithoutDate(string refKey, string eventType, string eventDate);
        Task<List<EventLoggerDTO>> GetEventlist(string module, string accountNo, string busnLocation);
        Task<List<FinancilInfoItemDTO>> TxnInstantListSelect(string acctNo);
        Task<List<FinancilInfoItemDTO>> TxnInstantUnpostedTxnList(string acctNo);
        Task<List<ProductDiscountDTO>> WebProductDiscountListSelect(string acctNo, string discType, string refTo);
        Task<List<EventLoggerDTO>> FtEventSearch(EventLoggerDTO eventLog);
        Task<List<AcctBalanceSelectAmountDTO>> GetOnLineTransactionList(string acctNo, int flag);
        Task<List<PaymentTxnDTO>> GetPaymentTxnList(string accountNo);
        Task<PaymentTxnDTO> GetPaymentTxnDetail(string txnId);
        Task<int> SaveAcctDepositInfoMaint(CreditAssesOperationDTO accountDeptInfo, string applId = null, string corpCd = null);
        Task<int> SaveGeneralInfoMaint(GeneralInfoDTO generalInfo);
        Task<int> SaveCostCentreMaint(CostCentreDTO costCentreDTO, string userId);
        Task<int> FtFinancialInfoMaint(FinancialInfoDTO financialInfo, string userId);
        Task<int> SaveCollectionCaseInfo(string accountNo, string collector);
        Task<int> SaveTempCreditCtrlMaint(string acctNo, string creditLimit, string effDateFrom, string effDateTo, string userId);
        Task<int> DeleteLocationAcceptance(string acctNo, List<string> busnLocation, string cardNo, string userId);
        Task<int> ProductDiscountMaint(ProductDiscountDTO productDiscount, string acctNo, string func, string refTo);
        Task<int> DeleteProductDiscount(ProductDiscountDTO productDiscount, string acctNo, string refTo);
        Task<List<BillingItemDTO>> SearchBillingItem(string accountNo, string fromDate, string toDate, string TxnCategory, string sts);
    }
    public class AccountOpDAO :DAOBase, IAccountOpDAO
    {
        private readonly string _connectionString = string.Empty;

        public AccountOpDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<GeneralInfoDTO> FtGeneralInfo(int accountNo)
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
                var results = await cardtrendentities.Database.SqlQuery<GeneralInfoDTO>
                    (BuildSqlCommand("WebAcctGeneralInfoSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();

                return results;
            }
        }
        public async Task<List<CardTrend.Domain.Dto.Corporate.CreditAssesOperationDTO>> FtAcctDepositInfoList(string applid = null, string acctNo = null, string corpCd = null)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), applid, acctNo, corpCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@ApplId",
                                        "@AcctNo",
                                        "@CorpCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CardTrend.Domain.Dto.Corporate.CreditAssesOperationDTO>
                    (BuildSqlCommand("WebAcctDepositInfoListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();
                return results;
            }
        }
        public async Task<int> SaveAcctDepositInfoMaint(CreditAssesOperationDTO accountDeptInfo, string applId = null, string corpCd = null)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { 
                    Common.Helpers.Common.GetIssueNo(), applId, accountDeptInfo.AcctNo, corpCd, accountDeptInfo.TxnId,accountDeptInfo.DepositInd, accountDeptInfo.DepositType
                                               ,accountDeptInfo.ValidityDate,accountDeptInfo.BankAcctType,accountDeptInfo.SecurityDeposit,accountDeptInfo.BankName,accountDeptInfo.BankAcctNo,
                                                accountDeptInfo.DepositAmt,accountDeptInfo.BGSerialNo,accountDeptInfo.EffFromDate,accountDeptInfo.EffToDate,accountDeptInfo.NIRD,accountDeptInfo.Remark,
                                                accountDeptInfo.SAPRefNo,accountDeptInfo.UserId
                };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
	                                    "@ApplId",
                                        "@AcctNo",
                                        "@CorpCd",
                                        "@TxnId",
                                        "@DepositInd",
                                        "@DepositType",
                                        "@ValidityDate",
                                        "@BankAcctType",
                                        "@SecurityDepositAmt",
                                        "@BankName",
                                        "@BankAcctNo",
                                        "@DepositAmt",
                                        "@BGSerialNo",
                                        "@EffFromDate",
                                        "@EffToDate",
                                        "@NIRD",
                                        "@Remarks",
                                        "@SAPRefNo",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result =  await cardtrendentities.Database.ExecuteSqlCommandAsync
                    (BuildSqlCommandWithRrn("WebAcctDepositInfoMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<FinancialInfoDTO> FtFinancialInfoForm(int accountNo)
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
                var results = await cardtrendentities.Database.SqlQuery<FinancialInfoDTO>
                    (BuildSqlCommand("WebAcctFinInfoSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();
                return results;
            }
        }
        public async Task<List<CostCentreDTO>> GetCostCentres(string refTo, string refKey)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refTo, refKey};
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefTo",
                                        "@RefKey"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CostCentreDTO>
                    (BuildSqlCommand("WebCostCentreListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();
                return results;
            }
        }
        public async Task<CostCentreDTO> GetCostCentreByRefToAndCostCentre(string refTo, string refKey, string costCentre)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refTo, refKey,costCentre};
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefTo",
                                        "@RefKey",
                                        "@CostCentre"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CostCentreDTO>
                    (BuildSqlCommand("WebCostCentreSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();
                return results;
            }
        }
        public async Task<UpToDateBalDTO> FtUpToBalDetail(string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@Acctno"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<UpToDateBalDTO>
                    (BuildSqlCommand("WebAcctUTDBalanceSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();
                return results;
            }
        }
        public async Task<List<WebSecDepRemarksDTO>> GetSecDepRemarksListSelect(string accountNo, string eventType, string txnId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { accountNo, eventType};
                var paramNameList = new[]
                                   {
                                        "@AcctNo",
	                                    "@EventType"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<WebSecDepRemarksDTO>
                    (BuildSqlCommand("WebSecDepRemarksListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                if (eventType.ToLower() != "cao")
                    return results.Where(p => p.TxnId.ToLower().Contains(txnId)).ToList();
                return results;
            }
        }
        public async Task<List<FinancilInfoItemDTO>> TxnInstantListSelect(string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<FinancilInfoItemDTO>
                             (BuildSqlCommand("WebTxnInstantListSelect", paramCollection), paramCollection.ToArray())
                             .ToListAsync();
                return results;
            }
        }
        public async Task<List<AcctBalanceSelectAmountDTO>> GetOnLineTransactionList(string acctNo,int flag)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, flag };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@Acctno",
                                        "@Amount"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<AcctBalanceSelectAmountDTO>
                             (BuildSqlCommand("WebAcctUTDBalanceSelectAmount", paramCollection), paramCollection.ToArray())
                             .ToListAsync();
                return results;
            }
        }
        public async Task<List<FinancilInfoItemDTO>> TxnInstantUnpostedTxnList(string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<FinancilInfoItemDTO>
                    (BuildSqlCommand("WebTxnUnpostedListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();
                return results;
            }
        }
        public async Task<List<CreditLimitHistoryDTO>> WebAcctHistoryListSelect(Int64 AcctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), AcctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CreditLimitHistoryDTO>(BuildSqlCommand("WebAcctHistoryListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<CreditLimitHistoryDTO>> WebSecHistoryDepositList(string AccountNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { AccountNo };
                var paramNameList = new[]
                                   {
                                        "@RefKey"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CreditLimitHistoryDTO>(BuildSqlCommand("WebSecDepLogListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<PaymentTxnDTO>> GetPaymentTxnList(string accountNo)
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
                var results = await cardtrendentities.Database.SqlQuery<PaymentTxnDTO>(BuildSqlCommand("WebTxnPaymentListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<PaymentTxnDTO> GetPaymentTxnDetail(string txnId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), txnId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@TxnId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<PaymentTxnDTO>(BuildSqlCommand("WebTxnPaymentSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<int> FtFinancialInfoMaint(FinancialInfoDTO financialInfo,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {

                var parameters = new[] { 
                new SqlParameter("@IssNo", SqlDbType.Int) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                new SqlParameter("@AcctNo", SqlDbType.VarChar) {SqlValue = (object)financialInfo.AcctNo?? DBNull.Value},
                new SqlParameter("@TaxId", SqlDbType.VarChar) {SqlValue = (object)financialInfo.TaxId?? DBNull.Value},
                new SqlParameter("@LatePaymtInd", SqlDbType.VarChar) {SqlValue = (object)financialInfo.StopLPC ?? DBNull.Value},
                new SqlParameter("@DunCd", SqlDbType.TinyInt) {SqlValue = (object)financialInfo.DunCd?? DBNull.Value},
                new SqlParameter("@AllowanceFactor", SqlDbType.Money) {SqlValue = (object)financialInfo.AllowanceFactor?? DBNull.Value},
                new SqlParameter("@AccruedInterestAmt", SqlDbType.Money) {SqlValue = (object)financialInfo.AccruedInterestAmt?? DBNull.Value},
                new SqlParameter("@AccruedCreditUsageAmt", SqlDbType.Money) {SqlValue = (object)financialInfo.AccruedCreditUsageAmt?? DBNull.Value},
                new SqlParameter("@PromptPaymtRebate", SqlDbType.Money) {SqlValue = (object)financialInfo.PromptPaymtRebate?? DBNull.Value},
                new SqlParameter("@PromptPaymtRebateTerms", SqlDbType.TinyInt) {SqlValue = (object)financialInfo.PromptPaymtRebateTerms?? DBNull.Value},
                new SqlParameter("@PromptPaymtExp", SqlDbType.Date) {SqlValue = (object)NumberExtensions.DateConverterDB(financialInfo.PromptPaymtRebateExpiry)?? DBNull.Value},
                new SqlParameter("@LitLimitPerTxn", SqlDbType.Money) {SqlValue = (object)financialInfo.LitLimitPerTxn?? DBNull.Value},
                new SqlParameter("@AmtLimitPerTxn", SqlDbType.Money) {SqlValue = (object)financialInfo.AmtLimitPerTxn?? DBNull.Value},
                new SqlParameter("@CycNo", SqlDbType.TinyInt) {SqlValue = (object)financialInfo.CycNo?? DBNull.Value},
                new SqlParameter("@StmtType", SqlDbType.VarChar) {SqlValue = (object)financialInfo.StmtType ?? DBNull.Value},
                new SqlParameter("@StmtInd", SqlDbType.VarChar) {SqlValue = (object)financialInfo.StmtInd ?? DBNull.Value},
                new SqlParameter("@StmtDate", SqlDbType.SmallDateTime) {SqlValue = (object)financialInfo.StmtDate?? DBNull.Value},
                new SqlParameter("@InvBillInd", SqlDbType.VarChar) {SqlValue = (object)financialInfo.InvBillInd?? DBNull.Value},
                new SqlParameter("@PymtInd", SqlDbType.VarChar) {SqlValue = (object)financialInfo.PymtInd?? DBNull.Value},
                new SqlParameter("@VehPerfRptInd", SqlDbType.VarChar) {SqlValue = (object)financialInfo.VehPerfRptInd ?? DBNull.Value},
                new SqlParameter("@PaymtMethod", SqlDbType.VarChar) {SqlValue = (object)financialInfo.PaymtMethod ?? DBNull.Value},
                new SqlParameter("@PaymtTerms", SqlDbType.TinyInt) {SqlValue = (object)financialInfo.PymtTerms ?? DBNull.Value},
                new SqlParameter("@GracePeriod", SqlDbType.TinyInt) {SqlValue = (object)financialInfo.GracePeriod?? DBNull.Value},
                new SqlParameter("@DirectDebitInd", SqlDbType.VarChar) {SqlValue = (object)financialInfo.DirectDebitInd ?? DBNull.Value},
                new SqlParameter("@BankAcctType", SqlDbType.VarChar) {SqlValue = (object)financialInfo.BankAcctType?? DBNull.Value},
                new SqlParameter("@BankName", SqlDbType.VarChar) {SqlValue = (object)financialInfo.BankName?? DBNull.Value},
                new SqlParameter("@BankAcctNo", SqlDbType.VarChar) {SqlValue = (object)financialInfo.BankAcctNo?? DBNull.Value},
                new SqlParameter("@BankBranchCd", SqlDbType.VarChar) {SqlValue = (object)financialInfo.BankBranchCd?? DBNull.Value},
                new SqlParameter("@TaxCategory", SqlDbType.VarChar) {SqlValue = (object)financialInfo.TaxCategory?? DBNull.Value},
                new SqlParameter("@WriteOffDate", SqlDbType.DateTime) {SqlValue = (object)financialInfo.WriteOffDate?? DBNull.Value},
                new SqlParameter("@LastPaymtRecvType", SqlDbType.VarChar) {SqlValue = (object)financialInfo.LastPaymtRecvType?? DBNull.Value},
                new SqlParameter("@LastPaymtRecvAmt", SqlDbType.Money) {SqlValue = (object)financialInfo.LastPaymtRecvAmt?? DBNull.Value},
                new SqlParameter("@LastPaymtRecvDate", SqlDbType.DateTime) {SqlValue = (object)financialInfo.LastPaymtDate?? DBNull.Value},

                new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)userId?? DBNull.Value},
                new SqlParameter("@PayeeCd", SqlDbType.BigInt) {SqlValue = (object)financialInfo.PayeeCd?? DBNull.Value},
                new SqlParameter("@RiskCategory", SqlDbType.VarChar) {SqlValue = (object)financialInfo.RiskCategory?? DBNull.Value},
                new SqlParameter("@CreditLimit", SqlDbType.Money) {SqlValue = (object)financialInfo.CreditLimit?? DBNull.Value},

                new SqlParameter("@AssessmentType", SqlDbType.VarChar) {SqlValue = (object)financialInfo.SecuredCreditLine?? DBNull.Value},
                new SqlParameter("@Ewt", SqlDbType.VarChar) {SqlValue = (object)financialInfo.Ewt?? DBNull.Value},

                new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                };

                await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = WebAcctFinInfoMaint @IssNo,@AcctNo,@TaxId,@LatePaymtInd,@DunCd" +
                                                                                              ",@AllowanceFactor,@AccruedInterestAmt,@AccruedCreditUsageAmt,@PromptPaymtRebate,@PromptPaymtRebateTerms" +
                                                                                             ",@PromptPaymtExp,@LitLimitPerTxn,@AmtLimitPerTxn,@CycNo,@StmtType" +
                                                                                             ",@StmtInd,@StmtDate,@InvBillInd,@PymtInd,@VehPerfRptInd" +
                                                                                             ",@PaymtMethod,@PaymtTerms,@GracePeriod,@DirectDebitInd,@BankAcctType" +
                                                                                             ",@BankName,@BankAcctNo,@BankBranchCd,@TaxCategory,@WriteOffDate" +
                                                                                             ",@LastPaymtRecvType,@LastPaymtRecvAmt,@LastPaymtRecvDate,@UserId,@PayeeCd,@RiskCategory,@CreditLimit,@AssessmentType,@Ewt", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveCostCentreMaint(CostCentreDTO costCentreDTO, string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), costCentreDTO.RefTo, costCentreDTO.RefKey, costCentreDTO.CostCentre, costCentreDTO.Descp, costCentreDTO.PersonInCharge, userId };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@RefTo",
                                        "@RefKey",
                                        "@CostCentre",
                                        "@Descp",
                                        "@PersonInCharge",
                                        "@UserId"

                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebCostCentreMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveGeneralInfoMaint(GeneralInfoDTO generalInfo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new[] { 
                new SqlParameter("@IssNo", SqlDbType.Int) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                new SqlParameter("@AcctNo", SqlDbType.VarChar) {SqlValue = (object)generalInfo.AcctNo?? DBNull.Value},
                new SqlParameter("@CmpyRegsNo", SqlDbType.VarChar) {SqlValue = (object)generalInfo.CmpyRegsNo?? DBNull.Value},
                new SqlParameter("@RegsDate", SqlDbType.Date) {SqlValue = (object)generalInfo.RegsDate ?? DBNull.Value},
                new SqlParameter("@CmpyName1", SqlDbType.VarChar) {SqlValue = (object)generalInfo.AccountName?? DBNull.Value},
                new SqlParameter("@Sic", SqlDbType.VarChar) {SqlValue = (object)generalInfo.SIC?? DBNull.Value},
                new SqlParameter("@SAPNo", SqlDbType.VarChar) {SqlValue = (object)generalInfo.CustomerNo?? DBNull.Value},
                new SqlParameter("@CorpCd", SqlDbType.VarChar) {SqlValue = (object)generalInfo.CorpCd?? DBNull.Value},
                new SqlParameter("@ClientClass", SqlDbType.VarChar) {SqlValue = (object)generalInfo.TermsofPayment?? DBNull.Value},
                new SqlParameter("@ClientType", SqlDbType.VarChar) {SqlValue = (object)generalInfo.CustomerGroup?? DBNull.Value},
                new SqlParameter("@BusnEstablishment", SqlDbType.VarChar) {SqlValue = (object)generalInfo.BusnEstablishment?? DBNull.Value},
                new SqlParameter("@SrcCd", SqlDbType.VarChar) {SqlValue = (object)generalInfo.SrcCd?? DBNull.Value},

                new SqlParameter("@SrcRefNo", SqlDbType.VarChar) {SqlValue = (object)generalInfo.SrcRefNo?? DBNull.Value},
                new SqlParameter("@Sts", SqlDbType.VarChar) {SqlValue = (object)generalInfo.Sts?? DBNull.Value},
                new SqlParameter("@CreationDate", SqlDbType.DateTime) {SqlValue = (object)generalInfo.CreationDate ?? DBNull.Value},
                new SqlParameter("@TerminatedDate", SqlDbType.DateTime) {SqlValue = (object)generalInfo.TerminatedDate ?? DBNull.Value},
                new SqlParameter("@ReasonCd", SqlDbType.VarChar) {SqlValue = (object)generalInfo.ReasonCd?? DBNull.Value},

                new SqlParameter("@OverrideStsUserId", SqlDbType.VarChar) {SqlValue = (object)generalInfo.OverrideStsUserId?? DBNull.Value},
                new SqlParameter("@OverrideSts", SqlDbType.VarChar) {SqlValue = (object)generalInfo.OverrideSts?? DBNull.Value},
                new SqlParameter("@OverrideStsStart", SqlDbType.Date) {SqlValue = (object)generalInfo.OverrideStsStart ?? DBNull.Value},
                new SqlParameter("@OverrideStsExp", SqlDbType.Date) {SqlValue = (object)generalInfo.OverrideStsExpiry ?? DBNull.Value},
                new SqlParameter("@ApplId", SqlDbType.Int) {SqlValue = (object)generalInfo.ApplId ?? DBNull.Value},

                new SqlParameter("@ApplRefNo", SqlDbType.VarChar) {SqlValue = (object)generalInfo.ApplRef?? DBNull.Value},
                new SqlParameter("@CaptDate", SqlDbType.DateTime) {SqlValue = (object)generalInfo.CaptDate ?? DBNull.Value},
                new SqlParameter("@Remarks", SqlDbType.VarChar) {SqlValue = (object)generalInfo.Remarks?? DBNull.Value},
                new SqlParameter("@WebUserId", SqlDbType.VarChar) {SqlValue = (object)generalInfo.WebUserId?? DBNull.Value},
                new SqlParameter("@LoyaltyCardNo", SqlDbType.BigInt) {SqlValue = (object)generalInfo.LoyaltyCardNo?? DBNull.Value},

                new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)generalInfo.UserId?? DBNull.Value},
                new SqlParameter("@WebPw", SqlDbType.VarChar) {SqlValue = (object)generalInfo.WebPassword?? DBNull.Value},
                new SqlParameter("@ReconAcct", SqlDbType.VarChar) {SqlValue = (object)generalInfo.ReconAcct?? DBNull.Value},

                new SqlParameter("@BusnCategory", SqlDbType.VarChar) {SqlValue = (object)generalInfo.Industry?? DBNull.Value},
                new SqlParameter("@TaxId", SqlDbType.VarChar) {SqlValue = (object)generalInfo.TaxId?? DBNull.Value},

                new SqlParameter("@SaleTerritory", SqlDbType.VarChar) {SqlValue = (object)generalInfo.SalesGroup?? DBNull.Value},
                new SqlParameter("@AcctType", SqlDbType.VarChar) {SqlValue = (object)generalInfo.AccountType?? DBNull.Value},
                new SqlParameter("@CutOff", SqlDbType.TinyInt) {SqlValue = (object)generalInfo.CutOff?? DBNull.Value},
                new SqlParameter("@PymtTerms", SqlDbType.Int) {SqlValue = (object)generalInfo.PymtTerms?? DBNull.Value},
                new SqlParameter("@LangId", SqlDbType.Char,2) {SqlValue = (object)generalInfo.LangId?? DBNull.Value},

                new SqlParameter("@CmpyEmbName", SqlDbType.VarChar) {SqlValue = (object)generalInfo.CmpyEmbName?? DBNull.Value},
                new SqlParameter("@CmpyType", SqlDbType.VarChar) {SqlValue = (object)generalInfo.CmpyType?? DBNull.Value},
                new SqlParameter("@ContactPerson", SqlDbType.VarChar) {SqlValue = (object)generalInfo.FamilyName ?? DBNull.Value},
                new SqlParameter("@AuthName", SqlDbType.VarChar) {SqlValue = (object)generalInfo.AuthName ?? DBNull.Value},
                new SqlParameter("@TradingArea", SqlDbType.VarChar) {SqlValue = (object)generalInfo.TradingArea ?? DBNull.Value},
                new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                };

                await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = WebAcctGeneralInfoMaint @IssNo,@AcctNo,@CmpyRegsNo,@RegsDate,@CmpyName1" +
                                                                                             ",@Sic,@SAPNo,@CorpCd,@ClientClass,@ClientType" +
                                                                                             ",@BusnEstablishment,@SrcCd,@SrcRefNo,@Sts,@CreationDate" +
                                                                                             ",@TerminatedDate,@ReasonCd,@OverrideStsUserId,@OverrideSts,@OverrideStsStart" +
                                                                                             ",@OverrideStsExp,@ApplId,@ApplRefNo,@CaptDate,@Remarks" +
                                                                                             ",@WebUserId,@LoyaltyCardNo,@UserId,@WebPw,@ReconAcct" +
                                                                                             ",@BusnCategory,@TaxId,@SaleTerritory,@AcctType,@CutOff,@PymtTerms,@LangId,@CmpyEmbName,@CmpyType,@ContactPerson" +
                                                                                             ",@AuthName,@TradingArea", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveCollectionCaseInfo(string accountNo, string collector)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo,collector };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@Collector"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebAcctFinancialInfoCollectionInfo", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveTempCreditCtrlMaint(string acctNo,string creditLimit,string effDateFrom,string effDateTo,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, NumberExtensions.ConvertDecimalToDb(creditLimit), NumberExtensions.DateConverterDB(effDateFrom), NumberExtensions.DateConverterDB(effDateTo), userId };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CreditLimit",
                                        "@EffDateFrom",
                                        "@EffDateTo",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebTempCreditLimitMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> DeleteLocationAcceptance(string acctNo, List<string> busnLocation, string cardNo,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                int resultCode = 0;
                foreach (var item in busnLocation)
                {
                    var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, cardNo, item, userId };
                    var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@BusnLocation",
                                        "@UserId"
                                    };
                    var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                    var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebLocationAcceptanceDelete", paramCollection), paramCollection.ToArray());
                    resultCode = Convert.ToInt32(paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value);
                }

                return resultCode;
            }
        }
        public async Task<int> ProductDiscountMaint(ProductDiscountDTO productDiscount, string acctNo, string func, string refTo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] {func == "N"?null:productDiscount.Id,acctNo,productDiscount.ProdDiscType,productDiscount.ProdGroup,productDiscount.EffDate,productDiscount.PlanId,
                                                        productDiscount.Remarks,productDiscount.UserId,func == "N"?"N" :"E",refTo,productDiscount.EffEndDate,productDiscount.OnlineInd};
                var paramNameList = new[]
                                    {
                                        "@Id",
                                        "@Refkey",
                                        "@ProdDiscType",
                                        "@ProdCd",
                                        "@EffDate",
                                        "@PlanId",
                                        "@Remarks",
                                        "@UserId",
                                        "@flag",
                                        "@Refto",
                                        "@EffEndDate",
                                        "@OnlineInd"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebProductDiscountMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> DeleteProductDiscount(ProductDiscountDTO productDiscount, string acctNo, string refTo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] {productDiscount.Id,acctNo,productDiscount.ProdDiscType,productDiscount.ProdGroup,productDiscount.EffDate,
                                                       productDiscount.UserId,refTo,productDiscount.EffEndDate};
                var paramNameList = new[]
                                    {
                                        "@Id",
                                        "@Refkey",
                                        "@ProdDiscType",
                                        "@ProdCd",
                                        "@EffDate",
                                        "@UserId",
                                        "@Refto",
                                        "@EffEndDate"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebProductDiscountDelete", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<List<EventLoggerDTO>> WebEventSearchWithoutDate(string refKey, string eventType, string eventDate)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { refKey, eventType, NumberExtensions.DateConverterDB(eventDate) };
                var paramNameList = new[]
                                   {
                                        "@RefKey",
	                                    "@EventType",
                                        "@EventDate"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<EventLoggerDTO>
                              (BuildSqlCommand("WebEventSearchWithoutDate", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results.OrderByDescending(s=>s.CreationDate).ToList();
            }
        }
        public async Task<EventLoggerDTO> GetEventLoggerDetail(string module, string eventId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { module, eventId };
                var paramNameList = new[]
                                   {
                                        "@Module",
	                                    "@EventId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<EventLoggerDTO>
                    (BuildSqlCommand("WebEventSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();
                return results;
            }
        }
        public async Task<List<EventLoggerDTO>> GetEventlist(string module, string accountNo, string busnLocation)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { module, accountNo, busnLocation };
                var paramNameList = new[]
                                   {
                                        "@Module",
	                                    "@AcctNo",
                                        "@BusnLocation"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<EventLoggerDTO>
                    (BuildSqlCommand("WebEventListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<TempCreditCtrlDTO> FtTempCreditLimitDetail(string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<TempCreditCtrlDTO>
                    (BuildSqlCommand("WebTempCreditLimitSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();
                return results;
            }
        }
        public async Task<List<CostCentreDTO>> WebCostCentreSearch(string refTo, string refKey, string costCentre)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refTo, refKey, costCentre };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefTo",
                                        "@RefKey",
                                        "@CostCentre"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CostCentreDTO>
                    (BuildSqlCommand("WebCostCentreSearch", paramCollection), paramCollection.ToArray())
                    .ToListAsync();
                return results;
            }
        }
        public async Task<List<ProductDiscountDTO>> WebProductDiscountListSelect(string acctNo, string discType, string refTo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { acctNo, discType, refTo};
                var paramNameList = new[]
                                   {
                                        "@Refkey",
	                                    "@ProdDiscType",
                                        "@Refto"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<ProductDiscountDTO>
                    (BuildSqlCommand("WebProductDiscountListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();
                return results;
            }
        }
        public async Task<ProductDiscountDTO> WebProductDiscountSelect(string acctNo, string discType, string id, string refTo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { NumberExtensions.ConvertLongToDb(id), acctNo, discType, refTo };
                var paramNameList = new[]
                                   {
                                        "@Id",
	                                    "@Refkey",
                                        "@ProdDiscType",
                                        "@Refto"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<ProductDiscountDTO>
                    (BuildSqlCommand("WebProductDiscountSelect", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();
                return results;
            }
        }
        public async Task<List<EventLoggerDTO>> FtEventSearch(EventLoggerDTO eventLog)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { eventLog.Module, eventLog.ReferenceKey, eventLog.EventType,eventLog.EventDate };
                var paramNameList = new[]
                                   {
                                        "@Module",
	                                    "@RefKey",
                                        "@EventType",
                                        "@EventDate"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<EventLoggerDTO>
                    (BuildSqlCommand("WebEventSearch", paramCollection), paramCollection.ToArray())
                    .ToListAsync();
                return results;
            }
        }
        public async Task<List<BillingItemDTO>> SearchBillingItem(string accountNo, string fromDate, string toDate, string TxnCategory, string sts)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo, NumberExtensions.DateConverterDB(fromDate), NumberExtensions.DateConverterDB(toDate), Convert.ToInt16(TxnCategory), sts };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AcctNo",
                                        "@FromDate",
                                        "@ToDate",
                                        "@TxnCategory",
                                        "@Sts"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<BillingItemDTO>
                            (BuildSqlCommand("WebBillingAllItemSearch", paramCollection), paramCollection.ToArray())
                            .ToListAsync();
                return results;
            }
        }
    }
}

using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.Dealer;
using CardTrend.Domain.Dto.Merchant;
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
    public interface IMechSignUpDAO
    {
        Task<MerchGeneralInfoDTO> GetMAGeneralInfoDetail(string acctNo);
        Task<MerchGeneralInfoDTO> GetBusinessLocationGeneralInfoDetail(string busnLocation);
        Task<BusnLocTerminalDTO> GetBusnLocTermDetail(string termId, string busnLocation);
        Task<EServiceDTO> iFrameMerchGeneralInfoSelect(string busnLocation);
        Task<List<BusnLocTerminalDTO>> GetBusnLocTermList(string busnLocation);
        Task<List<MerchGeneralInfoDTO>> GetBusinessLocationList(string acctNo);
        Task<List<EServiceDTO>> iFrameMerchTxnListSelect(string busnLocation, int category, int month, int year);
        Task<List<MerchPostedTxnSearchDTO>> GetMerchtPostedTxnSearch(string accountNo, string busnLocation, string txnCd, string txnDate);
        Task<List<MerchChangeOwnershipDTO>> WebMerchOwnershipChangeListSelect(string businessLocation);
        Task<MerchChangeOwnershipDTO> WebMerchChgOwnershipSelect(string busnLocation);
        Task<List<MerchProductPrizeDTO>> WebMerchProductPriceSearch(string busnLocation, string ProdCd, DateTime? EffDateFrom, DateTime? EffDateTo, bool isListSelect);
        Task<IList<MerchAgreementGeneralInfoDTO>> GetMerchAgreementList();
        Task<int> SaveEventMaint(EventLoggerDTO _Logger,string module);
        Task<int> SaveBusnLocTerm(BusnLocTerminalDTO busnLocTerminal);
        Task<int> SaveMerchChgOwnershipMaint(MerchChangeOwnershipDTO model, string userId);
        Task<IssMessageDTO> SaveMAGeneralInfo(MerchAgreementGeneralInfoDTO accountDeptInfo, string Func);
        Task<IssMessageDTO> SaveBusnLocationGeneralInfo(MerchGeneralInfoDTO merch);
    }
    public class MechSignUpDAO : DAOBase, IMechSignUpDAO
    {
        private readonly string _connectionString = string.Empty;
        public MechSignUpDAO(string connString)
        {
            _connectionString = connString;
        }         
        public async Task<IList<MerchAgreementGeneralInfoDTO>> GetMerchAgreementList()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo()};
                var paramNameList = new[]
                                   {
                                        "@AcqNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<MerchAgreementGeneralInfoDTO>
                    (BuildSqlCommand("WebMerchAgreementListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                return results;
            }
        }
        public async Task<MerchGeneralInfoDTO> GetMAGeneralInfoDetail(string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MerchGeneralInfoDTO>
                    (BuildSqlCommand("WebMerchGeneralInfoSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<MerchGeneralInfoDTO> GetBusinessLocationGeneralInfoDetail( string busnLocation)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), busnLocation };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@BusnLocation"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MerchGeneralInfoDTO>
                    (BuildSqlCommand("WebBusnLocationGeneralInfoSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<List<MerchGeneralInfoDTO>> GetBusinessLocationList(string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MerchGeneralInfoDTO>
                    (BuildSqlCommand("WebBusnLocationListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();

                return result;
            }
        }
        public async Task<List<MerchPostedTxnSearchDTO>> GetMerchtPostedTxnSearch(string accountNo, string busnLocation, string txnCd, string txnDate)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo, busnLocation, txnCd, NumberExtensions.DateConverterDB(txnDate) };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@AcctNo",
                                        "@BusnLocation",
                                        "@TxnCd",
                                        "@TxnDate"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<MerchPostedTxnSearchDTO>
                    (BuildSqlCommand("WebMerchPostedTxnSearch", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                return results;
            }
        }
        public async Task<EServiceDTO> iFrameMerchGeneralInfoSelect(string busnLocation)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { busnLocation };
                var paramNameList = new[]
                                   {
                                        "@SAPNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<EServiceDTO>
                    (BuildSqlCommand("iFrameMerchGeneralInfoSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<List<EServiceDTO>> iFrameMerchTxnListSelect(string busnLocation, int category, int month,int year)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { busnLocation, category, month ,year};
                var paramNameList = new[]
                                   {
                                        "@BusnLocation",
                                        "@Category",
                                        "@Month",
                                        "@Year"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<EServiceDTO>
                    (BuildSqlCommand("iFrameMerchTxnListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();

                return result;
            }
        }      
        public async Task<BusnLocTerminalDTO> GetBusnLocTermDetail(string termId, string busnLocation)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), busnLocation, termId };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@BusnLocation",
                                        "@TermId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<BusnLocTerminalDTO>
                    (BuildSqlCommand("WebBusnLocationTerminalSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<List<BusnLocTerminalDTO>> GetBusnLocTermList(string busnLocation)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), busnLocation };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@BusnLocation"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<BusnLocTerminalDTO>
                    (BuildSqlCommand("WebBusnLocationTerminalListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();

                return result;
            }
        }
        public async Task<List<MerchChangeOwnershipDTO>> WebMerchOwnershipChangeListSelect(string businessLocation)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), businessLocation };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@FrmBusnLoc"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MerchChangeOwnershipDTO>
                    (BuildSqlCommand("WebMerchChgOwnershipListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();

                return result;
            }
        }
        public async Task<MerchChangeOwnershipDTO> WebMerchChgOwnershipSelect(string busnLocation)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), busnLocation };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@BusnLocation"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MerchChangeOwnershipDTO>
                    (BuildSqlCommand("WebMerchChgOwnershipSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();

                return result;
            }
        }
        public async Task<List<MerchProductPrizeDTO>> WebMerchProductPriceSearch(string busnLocation, string ProdCd, DateTime? EffDateFrom, DateTime? EffDateTo, bool isListSelect)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[]{};
                string[] paramNameList ;
                if(isListSelect)
                {
                    parameters = new object[] { Common.Helpers.Common.GetIssueNo(), busnLocation };
                    paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@BusnLocation"
                                   };
                }
                else
                {
                    parameters = new object[] { Common.Helpers.Common.GetIssueNo(), busnLocation, ProdCd, EffDateFrom, EffDateTo };
                    paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@BusnLocation",
                                        "@ProdCd",
                                        "@EffDateFrom",
                                        "@EffDateTo"
                                   };

                }
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<MerchProductPrizeDTO>
                    (BuildSqlCommand(isListSelect ? "WebMerchProductPriceListSelect" : "WebMerchProductPriceSearch", paramCollection), paramCollection.ToArray()).ToListAsync();
                return result;
            }
        }
        public async Task<IssMessageDTO> SaveBusnLocationGeneralInfo(MerchGeneralInfoDTO merch)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new[] { 
                new SqlParameter("@Func", SqlDbType.VarChar) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                new SqlParameter("@AcqNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()},
                new SqlParameter("@AcctNo", SqlDbType.VarChar) {SqlValue = (object)merch.AcctNo?? DBNull.Value},
                new SqlParameter("@BusnLocation", SqlDbType.VarChar) {SqlValue = (object)merch.BusnLocation?? DBNull.Value},
                new SqlParameter("@BusnName", SqlDbType.VarChar) {SqlValue = (object)merch.BusnName?? DBNull.Value},

                new SqlParameter("@SiteId", SqlDbType.VarChar) {SqlValue = (object)merch.ReconAcct?? DBNull.Value},
                new SqlParameter("@AgreeNo", SqlDbType.VarChar) {SqlValue = (object)merch.AgreementNo?? DBNull.Value},
                new SqlParameter("@AgreeDate", SqlDbType.VarChar) {SqlValue = (object)merch.AgreementDate?? DBNull.Value},
                new SqlParameter("@PersonInChrg", SqlDbType.VarChar) {SqlValue = (object)merch.PersonInCharge?? DBNull.Value},
                new SqlParameter("@Ownership", SqlDbType.VarChar) {SqlValue = (object)merch.Ownership?? DBNull.Value},

                new SqlParameter("@Establishment", SqlDbType.VarChar) {SqlValue = (object)merch.Establishment?? DBNull.Value},
                new SqlParameter("@Sic", SqlDbType.VarChar) {SqlValue = (object)merch.Sic?? DBNull.Value},
                new SqlParameter("@Mcc", SqlDbType.VarChar) {SqlValue = (object)merch.Mcc?? DBNull.Value},
                new SqlParameter("@CoRegNo", SqlDbType.VarChar) {SqlValue = (object)merch.CoRegNo?? DBNull.Value},
                new SqlParameter("@CoRegName", SqlDbType.VarChar) {SqlValue = (object)merch.CoRegName?? DBNull.Value},

                new SqlParameter("@CoRegDate", SqlDbType.SmallDateTime) {SqlValue = (object)merch.CoRegDate?? DBNull.Value},
                new SqlParameter("@OwnershipTrsfDate", SqlDbType.Date) {SqlValue = (object)merch.OwnershipTrsfDate?? DBNull.Value},
                new SqlParameter("@OwnershipTo", SqlDbType.VarChar) {SqlValue = (object)merch.OwnershipTo?? DBNull.Value},
                new SqlParameter("@DBAName", SqlDbType.VarChar) {SqlValue = (object)merch.DBAName?? DBNull.Value},
                new SqlParameter("@DBARegion", SqlDbType.VarChar) {SqlValue = (object)merch.DBARegion?? DBNull.Value},

                new SqlParameter("@DBACity", SqlDbType.VarChar) {SqlValue = (object)merch.DBACity?? DBNull.Value},
                new SqlParameter("@DBAState", SqlDbType.VarChar) {SqlValue = (object)merch.DBAState?? DBNull.Value},
                new SqlParameter("@PayeeName", SqlDbType.VarChar) {SqlValue = (object)merch.PayeeName?? DBNull.Value},
                new SqlParameter("@AutoDebit", SqlDbType.Char,1) {SqlValue = (object)merch.AutoDebitInd?? DBNull.Value},
                new SqlParameter("@BankName", SqlDbType.VarChar) {SqlValue = (object)merch.BankName?? DBNull.Value},

                new SqlParameter("@BankAcctType", SqlDbType.VarChar) {SqlValue = (object)merch.BankAcctType ?? DBNull.Value},
                new SqlParameter("@BankAcctNo", SqlDbType.VarChar) {SqlValue =(object)merch.BankAcctNo ??DBNull.Value},
                new SqlParameter("@BankBranchCd", SqlDbType.VarChar) {SqlValue = (object)merch.BankBranchCd?? DBNull.Value},
                new SqlParameter("@Sts", SqlDbType.VarChar) {SqlValue = (object)merch.Sts ?? DBNull.Value},
                new SqlParameter("@EntityId", SqlDbType.VarChar) {SqlValue = (object)merch.EntityId ?? DBNull.Value},

                new SqlParameter("@TaxId", SqlDbType.VarChar) {SqlValue = (object)merch.TaxId?? DBNull.Value},
                new SqlParameter("@WithholdInd", SqlDbType.Char,1) {SqlValue = (object)merch.WithholdingTaxInd??DBNull.Value},
                new SqlParameter("@WithholdRate", SqlDbType.Money) {SqlValue = (object)merch.WithholdingTaxRate ?? DBNull.Value},
                new SqlParameter("@CycNo", SqlDbType.TinyInt) {SqlValue = (object)merch.CycNo ?? DBNull.Value},
                new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)merch.UserId ?? DBNull.Value},


                new SqlParameter("@CreationDate", SqlDbType.DateTime) {SqlValue = (object)merch.CreationDate?? DBNull.Value},
                new SqlParameter("@ReasonCd", SqlDbType.VarChar) {SqlValue = (object)merch.ReasonCd?? DBNull.Value},
                new SqlParameter("@StmtPrint", SqlDbType.Char,1) {SqlValue = (object)merch.StmtPrint?? DBNull.Value},
                new SqlParameter("@TopUpLimit", SqlDbType.Money) {SqlValue = (object)merch.TopUpLimit?? DBNull.Value},
                new SqlParameter("@AreaCd", SqlDbType.VarChar,10) {SqlValue = (object)merch.AreaCd?? DBNull.Value},

                new SqlParameter("@oBusnLocation", SqlDbType.VarChar,15) {Direction = ParameterDirection.Output},
                new SqlParameter("@oEntityId", SqlDbType.VarChar,19) {Direction = ParameterDirection.Output},
                new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                };

                await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = WebBusnLocationGeneralInfoMaint @Func,@AcqNo,@AcctNo,@BusnLocation,@BusnName" +
                                                                              ",@SiteId,@AgreeNo,@AgreeDate,@PersonInChrg,@Ownership" +
                                                                             ",@Establishment,@Sic,@Mcc,@CoRegNo,@CoRegName" +
                                                                             ",@CoRegDate,@OwnershipTrsfDate,@OwnershipTo,@DBAName,@DBARegion" +
                                                                             ",@DBACity,@DBAState,@PayeeName,@AutoDebit,@BankName" +
                                                                             ",@BankAcctType,@BankAcctNo,@BankBranchCd,@Sts,@EntityId" +
                                                                             ",@TaxId,@WithholdInd,@WithholdRate,@CycNo,@UserId,@CreationDate,@ReasonCd,@StmtPrint,@TopUpLimit,@AreaCd,@oBusnLocation OUT" +
                                                                             ",@oEntityId OUT", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var busnLocation = parameters.Where(x => x.ParameterName == "@oBusnLocation").FirstOrDefault().Value;
                var entityId = parameters.Where(x => x.ParameterName == "@oEntityId").FirstOrDefault().Value;
                issMessage.Flag = Convert.ToInt32(resultCode);
                issMessage.paraOut.BusnLocation = Convert.ToString(busnLocation);
                issMessage.paraOut.EntityId = Convert.ToString(entityId);
                return issMessage;
            }
        }
        public async Task<IssMessageDTO> SaveMAGeneralInfo(MerchAgreementGeneralInfoDTO accountDeptInfo, string Func)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Func, Common.Helpers.Common.GetIssueNo(), accountDeptInfo.AcctNo, accountDeptInfo.BusinessName, accountDeptInfo.AgreeNo
                                               ,accountDeptInfo.AgreeDate,accountDeptInfo.AffiliatedWith,accountDeptInfo.SAPNo,accountDeptInfo.PersonInCharge,accountDeptInfo.Ownership
                                               ,accountDeptInfo.Establishment,accountDeptInfo.CoRegsNo,accountDeptInfo.CoRegsName,accountDeptInfo.CoRegsDate,accountDeptInfo.Moso,accountDeptInfo.PayeeName
                                               ,accountDeptInfo.AutoDebit,accountDeptInfo.BankName,accountDeptInfo.BankAcctType,accountDeptInfo.BankAcctNo,accountDeptInfo.BankBranchCd
                                               ,accountDeptInfo.Status,accountDeptInfo.EntityId,accountDeptInfo.TaxId,accountDeptInfo.WithholdInd,accountDeptInfo.WithholdingTaxRate
                                               ,accountDeptInfo.CycNo,accountDeptInfo.UserId,accountDeptInfo.CreationDate,accountDeptInfo.ReasonCd,accountDeptInfo.SrcFrom,accountDeptInfo.Msf};
                var paramNameList = new[]
                                    {
                                        "@Func",
	                                    "@AcqNo",
                                        "@AcctNo",
                                        "@BusnName",
                                        "@AgreeNo",
                                        "@AgreeDate",
                                        "@AffiliateWith",
                                        "@SAPNo",
                                        "@PersonInChrg",
                                        "@Ownership",
                                        "@Establishment",
                                        "@CoRegsNo",
                                        "@CoRegsName",
                                        "@CoRegsDate",
                                        "@Moso",
                                        "@PayeeName",
                                        "@AutoDebit",
                                        "@BankName",
                                        "@BankAcctType",
                                        "@BankAcctNo",
                                        "@BankBranchCd",
                                        "@Sts",
                                        "@EntityId",
                                        "@TaxId",
                                        "@WithholdInd",
                                        "@WithholdRate",
                                        "@CycNo",
                                        "@UserId",
                                        "@CreationDate",
                                        "@ReasonCd",
                                        "@SrcFrom",
                                        "@Msf"
                                    };
                var outPutParameter = new object[] { new ColumnInfo { FieldName = "@oAcctNo", DataType = "varchar", ColLength = 19 }, new ColumnInfo { FieldName = "@oEntityId", DataType = "varchar", ColLength = 19 } };
                var paramCollection = BuildParameterListWithOutPutAndRrn(parameters, outPutParameter, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync
                    (BuildSqlCommandWithRrn("WebMerchGeneralInfoMaint", paramCollection), paramCollection.ToArray());

                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var accountNo = paramCollection.Where(x => x.ParameterName == "@oAcctNo").FirstOrDefault().Value;
                var entityId = paramCollection.Where(x => x.ParameterName == "@oEntityId").FirstOrDefault().Value;
                ResourceManager myManager = new ResourceManager(typeof(CardTrend.Common.Resources.IssMessages));
                string strMessage = myManager.GetString("Msg" + resultCode);
                return new IssMessageDTO()
                {
                    Descp = strMessage,
                    Flag = NumberExtensions.getFlagCode(resultCode),
                    paraOut = { BatchId = accountNo.ToString(), RetCd = entityId.ToString() }
                };
            }
        }
        public async Task<int> SaveBusnLocTerm(BusnLocTerminalDTO busnLocTerminal)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), busnLocTerminal.BusnLocation, busnLocTerminal.TermId, busnLocTerminal.TermType, busnLocTerminal.Status, 
                                                busnLocTerminal.DeployDate,busnLocTerminal.SaleTerritory, busnLocTerminal.ReplacedByTermId,busnLocTerminal.ReplacedDate,busnLocTerminal.ReasonCd,
                                               busnLocTerminal.IPEK,busnLocTerminal.DeviceModel,busnLocTerminal.SerialNo,busnLocTerminal.Remarks,busnLocTerminal.UserId};
                var paramNameList = new[]
                                    {
                                        "@AcqNo",
                                        "@BusnLocation",
                                        "@TermId",
                                        "@TermType",
                                        "@Sts",
                                        "@DeployDate",
                                        "@SaleTerritory",
                                        "@ReplacedByTermId",
                                        "@ReplacedDate",
                                        "@ReasonCd",
                                        "@IPEK",
                                        "@DeviceModel",
                                        "@SerialNo",
                                        "@Remarks",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebBusnLocationTerminalMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveEventMaint(EventLoggerDTO _Logger, string module)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { module, _Logger.EventId, _Logger.EventType, _Logger.AcctNo, _Logger.ReferenceKey, _Logger.Descp, _Logger.ReasonCd
                                              ,_Logger.Reminder,_Logger.RefDocument,_Logger.ClosedDate,_Logger.Status,_Logger.UserId};
                var paramNameList = new[]
                                    {
                                        "@Module",
                                        "@EventId",
                                        "@EventType",
                                        "@AcctNo",
                                        "@RefKey",
                                        "@Descp",
                                        "@ReasonCd",
                                        "@ReminderDate",
                                        "@XrefDoc",
                                        "@ClsDate",
                                        "@Sts",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebEventMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }       
        public async Task<int> SaveMerchChgOwnershipMaint(MerchChangeOwnershipDTO model,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), model.CurrentBusnNo,model.CurrentSiteId,model.NewSiteId,NumberExtensions.ConvertDatetimeDB(model.CutOffDate),model.CutOffTime, model.FloatAcctInd
                                              ,model.BusnName,model.TaxId,model.DBAName,model.DBAState,model.CoRegNo,model.CoRegName,model.DealerName,model.DealerContact,model.PayeeName,model.BankName,model.BankAcctType,
                                               model.BankBranchCd,model.BankAcctNo,model.SapNo,userId};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@CurrentBusnNo",
                                        "@CurrentSiteId",
                                        "@NewSiteId",
                                        "@CutOffDate",
                                        "@CutOffTime",
                                        "@FloatAcctInd",
                                        "@BusnName",
                                        "@TaxId",
                                        "@DBA",
                                        "@DBAState",
                                        "@CmpyRegNo",
                                        "@CmpyRegName",
                                        "@DealerName",
                                        "@DealerContact",
                                        "@PayeeName",
                                        "@BankName",
                                        "@BankAcctType",
                                        "@BankBranchCd",
                                        "@BankAcctNo",
                                        "@SapNo",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebMerchChgOwnershipMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }         

    }
}


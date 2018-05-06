using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.CardHolder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface ICardHolderDAO
    {
        Task<CardHolderInfoDTO> GeneralInfoSelect(string cardNo);
        Task<CardHolderInfoDTO> FinancialInfoSelect(string cardNo);
        Task<PersonalInfoDTO> PersonInfoSelect(string entityId);
        Task<ChangeStatusDTO> FtChangedAcctStsDetail(string id, string refCd);
        Task<CardReplacementDTO> CardReplacementInfoSelect(string cardNo);
        Task<List<CardHolderInfoDTO>> CardHolderList(string acctNo);
        Task<int> WebPinReset(string cardNo, string userId);
        Task<int> WebPinChange(string cardNo, string userId);
        Task<LocationAcceptDTO> LocationAcceptanceSelect(string acctNo, string busnLoc, string cardNo);
        Task<List<LocationAcceptDTO>> GetLocationAcceptances(string accountNo, string cardNo);
        Task<int> SaveGeneralInfo(CardHolderInfoDTO cardHolder, string userId);
        Task<int> SaveFinancialInfo(CardHolderInfoDTO finInfo, string cardNo);
        Task<int> SavePersonInfo(PersonalInfoDTO personalInfoDto, string entityId);
        Task<int> SaveLocationAccept(LocationAcceptDTO locationAcceptDTO, string accountNo, string cardNo, string userId);
        Task<IssMessageDTO> SaveCardReplacement(CardReplacementDTO cardReplacement, string userId);
        Task<int> DeleteLocationAcceptance(string accountNo, string busnLocation, string cardNo, string userId);
        Task<int> StatusSave(ChangeStatusDTO changeStatusDto, string userId);
    }
    public class CardHolderDAO : DAOBase, ICardHolderDAO
    {
        private readonly string _connectionString = string.Empty;
        public CardHolderDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<CardHolderInfoDTO> GeneralInfoSelect(string cardNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), cardNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@CardNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CardHolderInfoDTO>(BuildSqlCommand("WebCardGeneralInfoSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<CardHolderInfoDTO> FinancialInfoSelect(string cardNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), cardNo};
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@CardNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CardHolderInfoDTO>(BuildSqlCommand("WebCardFinInfoSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<PersonalInfoDTO> PersonInfoSelect(string entityId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] {Common.Helpers.Common.GetIssueNo(), entityId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@EntityId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<PersonalInfoDTO>(BuildSqlCommand("WebEntitySelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<LocationAcceptDTO> LocationAcceptanceSelect(string acctNo, string busnLoc, string cardNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, cardNo, busnLoc };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@BusnLocation"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<LocationAcceptDTO>(BuildSqlCommand("WebLocationAcceptanceSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<ChangeStatusDTO> FtChangedAcctStsDetail(string id,string refCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[]{};
                if (refCd.ToUpper() == "ACCT")
                {
                     parameters = new object[] { Common.Helpers.Common.GetIssueNo(), id, null, null, null, null };
                }
                else if (refCd.ToUpper() == "CARD")
                {
                     parameters = new object[] { Common.Helpers.Common.GetIssueNo(), null, id, null, null, null };
                }
                else if (refCd.ToUpper() == "MERCH")
                {
                     parameters = new object[] { Common.Helpers.Common.GetIssueNo(), null, null, id, null, null };
                }
                else if (refCd.ToUpper() == "BUSN")
                {
                     parameters = new object[] { Common.Helpers.Common.GetIssueNo(), null, null, null,id, null };
                }
                else if (refCd.ToUpper() == "APPC")
                {
                     parameters = new object[] { Common.Helpers.Common.GetIssueNo(), null, null, null, null, id };
                }
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@MerchAcctNo",
                                        "@BusnLocation",
                                        "@AppcId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<ChangeStatusDTO>(BuildSqlCommand("WebChangeStatusSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;

            }
        }    
        public async Task<CardReplacementDTO> CardReplacementInfoSelect(string cardNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), cardNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@CardNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CardReplacementDTO>(BuildSqlCommand("WebCardReplacementSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<List<CardHolderInfoDTO>> CardHolderList(string acctNo)
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
                var results = await cardtrendentities.Database.SqlQuery<CardHolderInfoDTO>(BuildSqlCommand("WebCardListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<LocationAcceptDTO>> GetLocationAcceptances(string accountNo, string cardNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo, cardNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CardNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<LocationAcceptDTO>(BuildSqlCommand("WebLocationAcceptanceListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<int> SaveGeneralInfo(CardHolderInfoDTO cardHolder,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), (object)Convert.ToInt64(cardHolder.CardNo)??null, cardHolder.EmbName, cardHolder.Sts, cardHolder.TerminatedDate == DateTime.MinValue?null:cardHolder.TerminatedDate,
                                                cardHolder.VehRegsNo, cardHolder.DriverCd,cardHolder.SKDSInd,cardHolder.SKDSQuota,cardHolder.SKDSNo,cardHolder.DialogueInd,cardHolder.PINInd,cardHolder.OdometerInd,
                                                cardHolder.AcctNo,cardHolder.PushAlertInd,cardHolder.AnnlFeeCd,cardHolder.JoiningFeeCd,cardHolder.RenewalInd,userId,cardHolder.PrimaryCard,
                                                cardHolder.Model,cardHolder.CostCentre,cardHolder.BranchCd,cardHolder.DivisionCd,cardHolder.DeptCd,cardHolder.ProdGroup};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@CardNo",
                                        "@EmbName",
                                        "@Sts",
                                        "@TerminatedDate",
                                        "@VehRegsNo",
                                        "@DriverCd",
                                        "@SKDSInd",
                                        "@SKDSQuota",
                                        "@SKDSNo",
                                        "@DialogueInd",
                                        "@PINInd",
                                        "@OdometerInd",
                                        "@AcctNo",
                                        "@PushAlertInd",
                                        "@AnnlFee",
                                        "@JoiningFee",
                                        "@RenewalInd",
                                        "@UserId",
                                        "@PrimaryCard",
                                        "@VehModel",
                                        "@CostCentre",
                                        "@BranchCd",
                                        "@DivisionCd",
                                        "@DeptCd",
                                        "@ProductGroup"
                                    };

                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebCardGeneralInfoMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> WebPinReset(string cardNo,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), cardNo, userId };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@CardNo",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebPinReset", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> WebPinChange(string cardNo, string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), cardNo, userId };
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@CardNo",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebPinChange", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;

                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveFinancialInfo(CardHolderInfoDTO finInfo,string cardNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), cardNo,finInfo.TxnLimit,finInfo.LitLimit,(object)Convert.ToInt32(finInfo.PINExceedCnt) ?? null,
                                              finInfo.PINAttempted,finInfo.PinTriedUpdDate,finInfo.PushAlertInd,finInfo.LocationInd,finInfo.LocationCheckFlag,
                                              finInfo.LocationMaxCnt,finInfo.LocationMaxAmt,finInfo.FuelCheckFlag,finInfo.FuelLitPerKM};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@CardNo",
                                        "@TxnLimit",
                                        "@LitLimit",
                                        "@PinExceedCnt",
                                        "@PinAttempted",
                                        "@PinTriedUpdDate",
                                        "@PushAlertInd",
                                        "@LocationInd",
                                        "@LocationCheckFlag",
                                        "@LocationMaxCnt",
                                        "@LocationMaxAmt",
                                        "@FuelCheckFlag",
                                        "@FuelLitPerKM"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebCardFinInfoMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SavePersonInfo(PersonalInfoDTO personalInfoDto,string entityId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), personalInfoDto.EntityId, personalInfoDto.Title, personalInfoDto.FirstName,personalInfoDto.LastName,
                                               personalInfoDto.NewIcType,personalInfoDto.NewIc,personalInfoDto.OldIcType,personalInfoDto.OldIc,personalInfoDto.Gender,
                                               personalInfoDto.DOB,NumberExtensions.ConvertDecimalToDb(personalInfoDto.IncomeBK),personalInfoDto.Occupation,personalInfoDto.DeptId,personalInfoDto.DrivingLic};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@EntityId",
                                        "@Title",
                                        "@FirstName",
                                        "@LastName",
                                        "@NewIcType",
                                        "@NewIc",
                                        "@OldIcType",
                                        "@OldIc",
                                        "@Gender",
                                        "@DOB",
                                        "@Income",
                                        "@Occupation",
                                        "@DeptId",
                                        "@DrivingLic"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebEntityMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }      
        public async Task<int> SaveLocationAccept(LocationAcceptDTO locationAcceptDTO,string accountNo,string cardNo,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                if (locationAcceptDTO.SelectedBusnLocations.Count() > 0)
                {
                    Int32 resultCode = 0;
                    // verify again
                    foreach (var item in locationAcceptDTO.SelectedBusnLocations)
                    {
                        var arr = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                        var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@State",
                                        "@BusnLocation",
                                        "@UserId"
                                    };
                        var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo, cardNo, arr[1], arr[0], userId };
                        var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                        var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebLocationAcceptanceMaint", paramCollection), paramCollection.ToArray());
                        resultCode = Convert.ToInt32(paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value);
                    }
                    return resultCode;
                }else if(locationAcceptDTO.SelectedStates.Count() > 0)
                {
                    

                    var state = new StringBuilder();
                    // verify again
                    foreach (var item in locationAcceptDTO.SelectedStates)
                    {
                        state.Append(item);
                    }
                    var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@State",
                                        "@BusnLocation",
                                        "@UserId"
                                    };
                    var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo, cardNo, state, null, userId };
                    var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                    var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebLocationAcceptanceMaint", paramCollection), paramCollection.ToArray());
                    var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                    return Convert.ToInt32(resultCode);
                }
                else
                {
                    var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo, cardNo, null, locationAcceptDTO.MerchantId, userId };
                    var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@State",
                                        "@BusnLocation",
                                        "@UserId"
                                    };
                    var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                    var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebLocationAcceptanceMaint", paramCollection), paramCollection.ToArray());
                    var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                    return Convert.ToInt32(resultCode);
                }
            }
        }
        public async Task<IssMessageDTO> SaveCardReplacement(CardReplacementDTO cardReplacement, string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@CardNo", SqlDbType.VarChar) {SqlValue = (object)cardReplacement.PrevCardNo?? DBNull.Value},
                    new SqlParameter("@NewCardNo", SqlDbType.VarChar,19) {Direction = ParameterDirection.Output},
                    new SqlParameter("@ExpiryDate", SqlDbType.VarChar) {SqlValue = (object)NumberExtensions.ConvertDatetimeDB(cardReplacement.CardExpiry.ToString())?? DBNull.Value},
                    new SqlParameter("@FeeCd", SqlDbType.VarChar) {SqlValue = (object)cardReplacement.FeeCd?? DBNull.Value},

                    new SqlParameter("@ReasonCd", SqlDbType.VarChar) {SqlValue = (object)cardReplacement.RsCode?? DBNull.Value},
                    new SqlParameter("@Remarks", SqlDbType.VarChar) {SqlValue = (object)cardReplacement.Remarks?? DBNull.Value},
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = userId},
                    new SqlParameter("@CardMedia", SqlDbType.Int) {SqlValue = (object)cardReplacement.CardMedia ?? DBNull.Value},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebCardReplacementMaint @IssNo,@CardNo,@NewCardNo OUT,@ExpiryDate,@FeeCd," +
                                                           "@ReasonCd,@Remarks,@UserId,@CardMedia", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var newCardNo = parameters.Where(x => x.ParameterName == "@NewCardNo").FirstOrDefault().Value;
                issMessage.Flag = Convert.ToInt32(resultCode);
                issMessage.paraOut.NewcardNo = Convert.ToString(newCardNo);
                return issMessage;
            }
        }
        public async Task<int> DeleteLocationAcceptance(string accountNo,string busnLocation,string cardNo,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accountNo, cardNo, busnLocation, userId};
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
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> StatusSave(ChangeStatusDTO changeStatusDto, string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), changeStatusDto.AcctNo, changeStatusDto.CardNo, changeStatusDto.MerchAcctNo,changeStatusDto.BusnLocation,
                                               changeStatusDto.AppcId,changeStatusDto.EventType,changeStatusDto.Sts,changeStatusDto.ReasonCd,changeStatusDto.Remarks,userId};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@MerchAcctNo",
                                        "@BusnLocation",
                                        "@AppcId",
                                        "@EventType",
                                        "@Sts",
                                        "@ReasonCd",
                                        "@Descp",
                                        "@UserId"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebChangeStatusMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
    }
}

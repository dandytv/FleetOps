using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Applicant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface IApplicantSignUpDAO
    {
        Task<CardAppcInfoDTO> GetApplicantInfo(string applidData, string appcidData, string acctNo);
        Task<CardFinancialInfoDTO> GetFinancialInfo(string cardNo);
        Task<List<CardAppcInfoDTO>> GetApllicants(string applicationId, string acctNo);
        Task<int> SaveFinancial(CardFinancialInfoDTO cardFinancialInfoDTO, string appcId);
        Task<IssMessageDTO> SaveApplicantInfo(CardAppcInfoDTO cardAppcInfoDto, string applId, string appcid, string userId);
    }
    public class ApplicantSignUpDAO : DAOBase, IApplicantSignUpDAO
    {
        private readonly string _connectionString = string.Empty;
        public ApplicantSignUpDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<CardAppcInfoDTO> GetApplicantInfo(string applidData, string appcidData, string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, applidData, appcidData };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@ApplId",
                                        "@AppcId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CardAppcInfoDTO>(BuildSqlCommand("WebAppcGeneralInfoSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<CardFinancialInfoDTO> GetFinancialInfo(string cardNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), cardNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AppcId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<CardFinancialInfoDTO>(BuildSqlCommand("WebAppcFinInfoSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<List<CardAppcInfoDTO>> GetApllicants(string applicationId, string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), acctNo, applicationId,null};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AcctNo",
                                        "@ApplId",
                                        "@Page"
                                    };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var list = await cardtrendentities.Database.SqlQuery<CardAppcInfoDTO>(BuildSqlCommand("WebAppcListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return list;
            }
        }
        public async Task<int> SaveFinancial(CardFinancialInfoDTO cardFinancialInfoDTO, string appcId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), appcId, cardFinancialInfoDTO.TxnLimit, cardFinancialInfoDTO.LitLimit,cardFinancialInfoDTO.PinExceedCnt,
                                               cardFinancialInfoDTO.PinAttempted,cardFinancialInfoDTO.PinTriedUpdDate};
                var paramNameList = new[]
                                    {
                                        "@IssNo",
                                        "@AppcId",
                                        "@TxnLimit",
                                        "@LitLimit",
                                        "@PinExceedCnt",
                                        "@PinAttempted",
                                        "@PinTriedUpdDate"
                                    };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebAppcFinInfoMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<IssMessageDTO> SaveApplicantInfo(CardAppcInfoDTO cardAppcInfoDto, string applId, string appcid, string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new[] { 
                new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                new SqlParameter("@AcctNo", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.AcctNo?? DBNull.Value},
                new SqlParameter("@ApplId", SqlDbType.VarChar,19) {SqlValue = (object)applId?? DBNull.Value},
                new SqlParameter("@AppcId", SqlDbType.VarChar) {SqlValue = (object)appcid?? DBNull.Value},
                new SqlParameter("@PINInd", SqlDbType.Char,1) {SqlValue = (object)cardAppcInfoDto.PINInd?? DBNull.Value},

                new SqlParameter("@CardType", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.CardType?? DBNull.Value},
                new SqlParameter("@CardNo", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.CardNo?? DBNull.Value},
                new SqlParameter("@DriverName", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.DriverName?? DBNull.Value},
                new SqlParameter("@EmbName", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.EmbName ?? DBNull.Value},
                new SqlParameter("@VehRegsNo", SqlDbType.NVarChar,16) {SqlValue = (object)cardAppcInfoDto.VehRegsNo ?? DBNull.Value},
                new SqlParameter("@SKDSInd", SqlDbType.Char,1) {SqlValue = (object)cardAppcInfoDto.SKDSInd ?? DBNull.Value},
                new SqlParameter("@SKDSQuota", SqlDbType.Money) {SqlValue = (object)cardAppcInfoDto.SKDSQuota ?? DBNull.Value},
                new SqlParameter("@SKDSNo", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.SKDSNo ?? DBNull.Value},
                new SqlParameter("@DialogueInd", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.DialogueInd ?? DBNull.Value},
                new SqlParameter("@VehModel", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.Model ?? DBNull.Value},
                new SqlParameter("@OdometerInd", SqlDbType.Char,1) {SqlValue = (object)cardAppcInfoDto.OdometerInd ?? DBNull.Value},
                new SqlParameter("@PushAlertInd", SqlDbType.Char,1) {SqlValue = (object)cardAppcInfoDto.PushAlertInd ?? DBNull.Value},
                new SqlParameter("@EntityId", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.EntityId ?? DBNull.Value},
                new SqlParameter("@Sts", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.Sts ?? "P"},


                new SqlParameter("@oAppcId", SqlDbType.VarChar,19) {Direction = ParameterDirection.Output},
                new SqlParameter("@oEntityId", SqlDbType.VarChar,19) {Direction = ParameterDirection.Output},
                new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)userId ?? DBNull.Value},
                new SqlParameter("@AnnlFee", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.AnnlFeeCd ?? DBNull.Value},
                new SqlParameter("@JoiningFee", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.JoiningFeeCd ?? DBNull.Value},

                new SqlParameter("@PriCardNo", SqlDbType.BigInt) {SqlValue = DBNull.Value},
                new SqlParameter("@ProdGroup", SqlDbType.VarChar) {SqlValue = (object)cardAppcInfoDto.ProdGroup ?? DBNull.Value},


                new SqlParameter("@PriAppcId", SqlDbType.Int) {SqlValue = (object)cardAppcInfoDto.PriAppcId ?? DBNull.Value},
                new SqlParameter("@CostCentre", SqlDbType.NVarChar) {SqlValue = (object)cardAppcInfoDto.CostCentre ?? DBNull.Value},

                new SqlParameter("@BranchCd", SqlDbType.NVarChar) {SqlValue = (object)cardAppcInfoDto.BranchCd ?? DBNull.Value},
                new SqlParameter("@DivisionCd", SqlDbType.NVarChar) {SqlValue = (object)cardAppcInfoDto.DivisionCd ?? DBNull.Value},
                new SqlParameter("@DeptCd", SqlDbType.NVarChar) {SqlValue = (object)cardAppcInfoDto.DeptCd ?? DBNull.Value},
                new SqlParameter("@CardMedia", SqlDbType.Int) {SqlValue = (object)cardAppcInfoDto.CardMedia ?? DBNull.Value},
                new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebAppcGeneralInfoMaint @IssNo,@AcctNo,@ApplId,@AppcId,@PINInd," +
                                                            "@CardType,@CardNo,@DriverName,@EmbName,@VehRegsNo,@SKDSInd,@SKDSQuota,@SKDSNo,@DialogueInd,@VehModel,@OdometerInd," +
                                                            "@PushAlertInd,@EntityId,@Sts,@oAppcId OUT,@oEntityId OUT,@UserId,@AnnlFee,@JoiningFee,@PriCardNo,@ProdGroup,@PriAppcId,"+
                                                            "@CostCentre,@BranchCd,@DivisionCd,@DeptCd,@CardMedia", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var appcId = parameters.Where(x => x.ParameterName == "@oAppcId").FirstOrDefault().Value;
                var oEntityId = parameters.Where(x => x.ParameterName == "@oEntityId").FirstOrDefault().Value;
                issMessage.Flag = Convert.ToInt32(resultCode);
                issMessage.paraOut.AppcId = Convert.ToString(appcId);
                issMessage.paraOut.EntityId = Convert.ToString(oEntityId);
                return issMessage;
    
            }
        }
    }
}

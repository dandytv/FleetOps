using Utilities.DAL;
using FleetOps.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CCMS.ModelSector;
using System.Web.WebPages.Html;

namespace FleetSys.Models
{
    public class ApplicantHolderOps : BaseClass
    {
        public string EntityId { get; set; }
        public string NewcardNo { get; set; }

        #region "GeneralInfo"
        #region "Table"
        public async Task<List<CardHolderInfoModel>> CardHolderList(string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@AcctNo", String.IsNullOrEmpty(AcctNo) ? "" : AcctNo);

                var reader = await objDataEngine.ExecuteCommandAsync("WebCardListSelect", CommandType.StoredProcedure, Parameters);

                var info = new List<CardHolderInfoModel>();
                while (reader.Read())
                {
                    info.Add(new CardHolderInfoModel
                    {
                        CardNo = Convert.ToString(reader["CardNo"]),
                        EmbossName = Convert.ToString(reader["Emboss Name"]),
                        SelectedCurrentStatus = Convert.ToString(reader["Status"]),
                        CardExpiry = Convert.ToString(reader["Card Expiry"]),
                        XRefCardNo = Convert.ToString(reader["Xref CardNo"]),
                        SelectedCardType = Convert.ToString(reader["Card Type"]),
                        SelectedPINInd = Convert.ToString(reader["PIN"]),
                        vehRegNo = Convert.ToString(reader["VRN"]),
                        DriverCd = Convert.ToString(reader["Driver"]),
                        FullName = Convert.ToString(reader["Full Name"]),
                        BlockedDate = DateTimeConverter(reader["BlockedDate"]),
                        TerminatedDate = Convert.ToString(reader["Card Terminated"]),
                        SelectedCostCentre = Convert.ToString(reader["CostCenter"]),
                        SKDSQuota = ConverterDecimal(reader["SKDS Quota"]),
                        SelectedSKDSNo = Convert.ToString(reader["SKDSNo"]),

                    });
                }
                return info;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "Table"

        //#region "Dropdown"
        ////For dropdown function
        //public static IEnumerable<SelectListItem> WebGetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null)
        //{
        //    var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
        //    objEngine.InitiateConnection();
        //    SqlParameter[] Parameters = new SqlParameter[5];
        //    SqlCommand cmd = new SqlCommand();
        //    Parameters[0] = new SqlParameter("@IssNo", 1);
        //    Parameters[1] = new SqlParameter("@RefType", String.IsNullOrEmpty(refType) ? (object)DBNull.Value : refType);
        //    Parameters[2] = String.IsNullOrEmpty(RefNo) ? new SqlParameter("@RefNo", DBNull.Value) : new SqlParameter("@RefNo", RefNo);
        //    Parameters[3] = String.IsNullOrEmpty(RefInd) ? new SqlParameter("@RefInd", DBNull.Value) : new SqlParameter("@RefInd", RefInd);
        //    Parameters[4] = String.IsNullOrEmpty(RefId) ? new SqlParameter("@RefId", DBNull.Value) : new SqlParameter("@RefId", RefId);
        //    var getObjData = objEngine.ExecuteCommand("WebGetRefLib", CommandType.StoredProcedure, Parameters);
        //    var list = new List<SelectListItem>();
        //    while (getObjData.Read())
        //    {
        //        list.Add(new SelectListItem
        //        {
        //            Text = Convert.ToString(getObjData["Descp"]),
        //            Value = Convert.ToString(getObjData["RefCd"]),
        //            Selected = Convert.ToString(getObjData["RefCd"]) == "1" ? true : false
        //        });
        //    }
        //    objEngine.CloseConnection();
        //    return list;
        //}
        //#endregion "Dropdown"

        #region "Form"
        public async Task<CardHolderInfoModel> GeneralInfoSelect(string cardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@CardNo", cardNo);
                var Reader = await objDataEngine.ExecuteCommandAsync("WebCardGeneralInfoSelect", CommandType.StoredProcedure, Parameters);
                var info = new CardHolderInfoModel();
                while (Reader.Read())
                {
                    info.AcctNo = Convert.ToString(Reader["AcctNo"]);
                    info.SelectedCostCentre = Convert.ToString(Reader["CostCentre"]);
                    info.EmbossName = Convert.ToString(Reader["EmbName"]);
                    info.CardExpiry = Convert.ToString(Reader["CardExpiry"]);
                    info.SelectedCardType = Convert.ToString(Reader["CardType"]);
                    info.SelectedCurrentStatus = Convert.ToString(Reader["Sts"]);
                    info.CardNo = cardNo;
                    info.SelectedReasonCode = Convert.ToString(Reader["ReasonCd"]);
                    info.CreationDate = Convert.ToString(Reader["CreationDate"]);
                    info.MemberSince = Convert.ToString(Reader["MemSince"]);
                    info.SelectedSKDSNo = Convert.ToString(Reader["SKDSNo"]);
                    info.oldCardNo = Convert.ToString(Reader["XPreCardNo"]);
                    info.XRefCardNo = Convert.ToString(Reader["XRefCardNo"]);
                    info.SelectedCardType = Convert.ToString(Reader["CardType"]);
                    info.SelectedVehicleModel = Convert.ToString(Reader["Model"]);
                    info.vehRegNo = Convert.ToString(Reader["VehRegsNo"]);
                    info.SelectedSKDSInd = BoolConverter(Reader["SKDSInd"]);
                    info.SKDSQuota = BaseClass.ConverterDecimal(Reader["SKDSQuota"]);
                    info.DriverCd = Convert.ToString(Reader["DriverCd"]);
                    info.DriverName = Convert.ToString(Reader["Driver Name"]);
                    info.BlockedDate = DateTimeConverter(Reader["BlockedDate"]);
                    info.TerminatedDate = BaseClass.DateConverter(Reader["TerminatedDate"]);
                    info.PVV = Convert.ToString(Reader["PVV"]);
                    info.PINOffset = Convert.ToString(Reader["PINoffSet"]);
                    info.SelectedRenewalInd = Convert.ToString(Reader["RenewalInd"]);
                    info.EntityId = Convert.ToString(Reader["EntityId"]);
                    info.SelectedDialogueInd = Convert.ToString(Reader["DialogueInd"]);
                    info.SelectedPushAlertInd = BaseClass.BoolConverter(Reader["PushAlertInd"]);
                    info.LocInd = BaseClass.BoolConverter(Reader["LocationInd"]);

                    info.SelectedLocCheckFlag = BaseClass.BoolConverter(Reader["LocationCheckFlag"]);
                    info.MaxCountLimit = ConvertInt(Reader["LocationMaxCnt"]);

                    info.AmtLimit = BaseClass.ConverterDecimal(Reader["LocationMaxAmt"]);
                    info.SelectedFuelCheckFlag = BaseClass.BoolConverter(Reader["FuelCheckFlag"]);
                    info.FuelLitre = BaseClass.ConverterDecimal(Reader["FuelLitPerKM"]);
                    info.PINExceedCnt = BaseClass.ConvertInt(Reader["PINExceedCnt"]);
                    info.PINAttempted = BaseClass.ConvertInt(Reader["PINAttempted"]);
                    info.SelectedAnnualFee = Convert.ToString(Reader["AnnlFeeCd"]);
                    info.SelectedJonFee = Convert.ToString(Reader["JoiningFeeCd"]);
                    info.SelectedProductUtilization = Convert.ToString(Reader["ProdGroup"]);
                    info.OdometerIndicator = BaseClass.BoolConverter(Reader["OdometerInd"]);
                    info.PrimaryCard = BaseClass.BoolConverter(Reader["PrimaryCard"]);
                    info.SelectedBranchCd = Convert.ToString(Reader["BranchCd"]);
                    info.SelectedDivisionCode = Convert.ToString(Reader["DivisionCd"]);
                    info.SelectedDeptCd = Convert.ToString(Reader["DeptCd"]);
                    info.SelectedCardMedia = Convert.ToString(Reader["CardMedia"]);


                    return info;
                }
                return new CardHolderInfoModel();
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "Form"

        #region "Save"
        public async Task<MsgRetriever> SaveGeneralInfo(CardHolderInfoModel CardData)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.Admin, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[27];
                SqlCommand cmd = new SqlCommand();

                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(CardData.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", ConvertLongToDb(CardData.CardNo));
                Parameters[2] = String.IsNullOrEmpty(CardData.EmbossName) ? new SqlParameter("@EmbName", DBNull.Value) : new SqlParameter("@EmbName", CardData.EmbossName);
                Parameters[3] = String.IsNullOrEmpty(CardData.SelectedCurrentStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", CardData.SelectedCurrentStatus);
                Parameters[4] = new SqlParameter("@TerminatedDate", ConvertDatetimeDB(CardData.TerminatedDate));
                Parameters[5] = String.IsNullOrEmpty(CardData.vehRegNo) ? new SqlParameter("@VehRegsNo", DBNull.Value) : new SqlParameter("@VehRegsNo", CardData.vehRegNo);
                Parameters[6] = String.IsNullOrEmpty(CardData.DriverCd) ? new SqlParameter("@DriverCd", DBNull.Value) : new SqlParameter("@DriverCd", CardData.DriverCd);
                Parameters[7] = new SqlParameter("@SKDSInd", ConvertBoolDB(CardData.SelectedSKDSInd));
                Parameters[8] = new SqlParameter("@SKDSQuota", ConvertDecimalToDb(CardData.SKDSQuota));
                Parameters[9] = String.IsNullOrEmpty(CardData.SelectedSKDSNo) ? new SqlParameter("@SKDSNo", DBNull.Value) : new SqlParameter("@SKDSNo", CardData.SelectedSKDSNo);
                Parameters[10] = String.IsNullOrEmpty(CardData.SelectedDialogueInd) ? new SqlParameter("@DialogueInd", DBNull.Value) : new SqlParameter("@DialogueInd", CardData.SelectedDialogueInd);
                Parameters[11] = new SqlParameter("@PINInd", ConvertBoolDB(CardData.SelectedPINInd));
                Parameters[12] = new SqlParameter("@OdometerInd", ConvertBoolDB(CardData.OdometerIndicator));
                Parameters[13] = new SqlParameter("@AcctNo", ConvertLongToDb(CardData.AcctNo));//String.IsNullOrEmpty(CardData.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : 
                Parameters[14] = new SqlParameter("@PushAlertInd", ConvertBoolDB(CardData.SelectedPushAlertInd));
                Parameters[15] = String.IsNullOrEmpty(CardData.SelectedAnnualFee) ? new SqlParameter("@AnnlFee", DBNull.Value) : new SqlParameter("@AnnlFee", CardData.SelectedAnnualFee);
                Parameters[16] = String.IsNullOrEmpty(CardData.SelectedJonFee) ? new SqlParameter("@JoiningFee", DBNull.Value) : new SqlParameter("@JoiningFee", CardData.SelectedJonFee);
                Parameters[17] = String.IsNullOrEmpty(CardData.SelectedRenewalInd) ? new SqlParameter("@RenewalInd", DBNull.Value) : new SqlParameter("@RenewalInd ", CardData.SelectedRenewalInd);
                Parameters[18] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[19] = new SqlParameter("@PrimaryCard", ConvertBoolDB(CardData.PrimaryCard));
                Parameters[20] = String.IsNullOrEmpty(CardData.SelectedProductUtilization) ? new SqlParameter("@ProductGroup", DBNull.Value) : new SqlParameter("@ProductGroup", CardData.SelectedProductUtilization);
                Parameters[21] = String.IsNullOrEmpty(CardData.SelectedCostCentre) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", CardData.SelectedCostCentre);
                Parameters[22] = String.IsNullOrEmpty(CardData.SelectedVehicleModel) ? new SqlParameter("@VehModel", DBNull.Value) : new SqlParameter("@VehModel", CardData.SelectedVehicleModel);
                Parameters[23] = String.IsNullOrEmpty(CardData.SelectedBranchCd) ? new SqlParameter("@BranchCd", DBNull.Value) : new SqlParameter("@BranchCd", CardData.SelectedBranchCd);
                Parameters[24] = String.IsNullOrEmpty(CardData.SelectedDivisionCode) ? new SqlParameter("@DivisionCd", DBNull.Value) : new SqlParameter("@DivisionCd", CardData.SelectedDivisionCode);
                Parameters[25] = String.IsNullOrEmpty(CardData.SelectedDeptCd) ? new SqlParameter("@DeptCd", DBNull.Value) : new SqlParameter("@DeptCd", CardData.SelectedDeptCd);
                Parameters[26] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[26].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebCardGeneralInfoMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "Save"
        #endregion "General info"

        #region "Financial Info"
        #region "Reset PIN"
        public async Task<MsgRetriever> WebPinReset(string CardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                SqlCommand cmd = new SqlCommand();

                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
                Parameters[2] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[3].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebPinReset", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);


                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }


        }
        #endregion "Reset PIN"

        #region "PINChange"
        public async Task<MsgRetriever> WebPinChange(string CardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
                Parameters[2] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[3].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebPinChange", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "PINChange"

        #region "form"
        public async Task<CardFinancialInfoModel> FinancialInfoSelect(int IssNo, string cardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", IssNo);
                Parameters[1] = String.IsNullOrEmpty(cardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", cardNo);
                var Reader = await objDataEngine.ExecuteCommandAsync("WebCardFinInfoSelect", CommandType.StoredProcedure, Parameters);

                var finInfo = new CardFinancialInfoModel();
                while (Reader.Read())
                {
                    finInfo.txnLimit = BaseClass.ConverterDecimal(Reader["TxnLimit"]);
                    finInfo.litLimit = BaseClass.ConverterDecimal(Reader["LitLimit"]);
                    finInfo.PinExceedCnt = BaseClass.ConvertInt(Reader["PinExceedCnt"]);
                    finInfo.PinAttempted = BaseClass.ConvertInt(Reader["PinAttempted"]);
                    finInfo.PinTriedUpdDate = DateTimeConverter(Reader["PinTriedUpdDate"]);
                    finInfo.PushAlertInd = BoolConverter(Reader["PushAlertInd"]);
                    finInfo.LocationInd = BoolConverter(Reader["LocationInd"]);
                    finInfo.LocationCheckFlag = BoolConverter(Reader["LocationCheckFlag"]);
                    finInfo.LocationMaxCnt = ConvertInt(Reader["LocationMaxCnt"]);
                    finInfo.LocationMaxAmt = ConvertInt(Reader["LocationMaxAmt"]).ToString("0");
                    finInfo.FuelCheckFlag = BoolConverter(Reader["FuelCheckFlag"]);
                    finInfo.FuelLitPerKM = ConverterDecimal(Reader["FuelLitPerKM"]);
                    finInfo.LastTxnDate = Convert.ToString(Reader["LastTxnDate"]);


                    return finInfo;
                };
                return new CardFinancialInfoModel();
            }
            finally
            {
                objDataEngine.CloseConnection();
            }


        }
        #endregion "Form

        #region "Save"
        public async Task<MsgRetriever> SaveFinancialInfo(CardFinancialInfoModel finInfo, string cardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[15];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(cardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", cardNo);
                Parameters[2] = new SqlParameter("@TxnLimit", ConvertDecimalToDb(finInfo.txnLimit));
                Parameters[3] = new SqlParameter("@LitLimit", ConvertDecimalToDb(finInfo.litLimit));
                Parameters[4] = new SqlParameter("@PinExceedCnt", ConvertIntToDb(finInfo.PinExceedCnt));
                Parameters[5] = new SqlParameter("@PinAttempted", ConvertIntToDb(finInfo.PinAttempted));
                Parameters[6] = String.IsNullOrEmpty(finInfo.PinTriedUpdDate) ? new SqlParameter("@PinTriedUpdDate", DBNull.Value) : new SqlParameter("@PinTriedUpdDate", ConvertDatetimeDB(finInfo.PinTriedUpdDate));
                Parameters[7] = new SqlParameter("@PushAlertInd", ConvertIntToDb(finInfo.PushAlertInd));
                Parameters[8] = new SqlParameter("@LocationInd", ConvertIntToDb(finInfo.LocationInd));
                Parameters[9] = new SqlParameter("@LocationCheckFlag", ConvertIntToDb(finInfo.LocationCheckFlag));
                Parameters[10] = new SqlParameter("@LocationMaxCnt", ConvertIntToDb(finInfo.LocationMaxCnt));
                Parameters[11] = new SqlParameter("@LocationMaxAmt", ConvertDecimalToDb(finInfo.LocationMaxAmt));
                Parameters[12] = new SqlParameter("@FuelCheckFlag", ConvertIntToDb(finInfo.FuelCheckFlag));
                Parameters[13] = new SqlParameter("@FuelLitPerKM", ConvertDecimalToDb(finInfo.FuelLitPerKM));
                Parameters[14] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[14].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebCardFinInfoMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "Save"
        #endregion "Financial Info"



        #region "Person Info"
        #region "form"
        public async Task<PersonInfoModel> PersonInfoSelect(int issNo, string entityId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", issNo);
                Parameters[1] = String.IsNullOrEmpty(entityId) ? new SqlParameter("@EntityId", DBNull.Value) : new SqlParameter("@EntityId", entityId);

                var reader = await objDataEngine.ExecuteCommandAsync("WebEntitySelect", CommandType.StoredProcedure, Parameters);

                var PersonInfo = new PersonInfoModel();
                while (reader.Read())
                {
                    PersonInfo.SelectedTitle = Convert.ToString(reader["Title"]);
                    PersonInfo.FirstName = Convert.ToString(reader["First Name"]);
                    PersonInfo.LastName = Convert.ToString(reader["Last Name"]);
                    PersonInfo.IdNo = Convert.ToString(reader["NewIc"]);
                    PersonInfo.SelectedIdType = Convert.ToString(reader["NewIcType"]);
                    PersonInfo.AltIdNo = Convert.ToString(reader["AlternateIc"]);
                    PersonInfo.SelectedAltIdType = Convert.ToString(reader["AlternateIcType"]);
                    PersonInfo.Selectedgender = Convert.ToString(reader["Gender"]);
                    PersonInfo.DOB = Convert.ToString(reader["DOB"]);
                    PersonInfo.AnnualIncome = BaseClass.ConverterDecimal(reader["Income"]);
                    PersonInfo.SelectedOccupation = Convert.ToString(reader["Occupation"]);
                    PersonInfo.SelectedDeptId = Convert.ToString(reader["DeptId"]);
                    PersonInfo.DrivingLicense = Convert.ToString(reader["DrivingLic"]);
                    PersonInfo.EntityId = entityId;
                }

                return PersonInfo;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "form"

        #region "Save"
        public async Task<MsgRetriever> SavePersonInfo(PersonInfoModel PersonInfo, string entityID)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[16];
                SqlCommand cmd = new SqlCommand();

                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(entityID) ? new SqlParameter("@EntityId", DBNull.Value) : new SqlParameter("@EntityId", entityID);
                Parameters[2] = String.IsNullOrEmpty(PersonInfo.SelectedTitle) ? new SqlParameter("@Title", DBNull.Value) : new SqlParameter("@Title", PersonInfo.SelectedTitle);
                Parameters[3] = String.IsNullOrEmpty(PersonInfo.FirstName) ? new SqlParameter("@FirstName", DBNull.Value) : new SqlParameter("@FirstName", PersonInfo.FirstName);
                Parameters[4] = String.IsNullOrEmpty(PersonInfo.LastName) ? new SqlParameter("@LastName", DBNull.Value) : new SqlParameter("@LastName", PersonInfo.LastName);
                Parameters[5] = String.IsNullOrEmpty(PersonInfo.IdNo) ? new SqlParameter("@NewIc", DBNull.Value) : new SqlParameter("@NewIc", PersonInfo.IdNo);
                Parameters[6] = String.IsNullOrEmpty(PersonInfo.SelectedIdType) ? new SqlParameter("@NewIcType", DBNull.Value) : new SqlParameter("@NewIcType", PersonInfo.SelectedIdType);
                Parameters[7] = String.IsNullOrEmpty(PersonInfo.AltIdNo) ? new SqlParameter("@OldIc", DBNull.Value) : new SqlParameter("@OldIc", PersonInfo.AltIdNo);
                Parameters[8] = String.IsNullOrEmpty(PersonInfo.SelectedAltIdType) ? new SqlParameter("@OldIcType", DBNull.Value) : new SqlParameter("@OldIcType", PersonInfo.SelectedAltIdType);
                Parameters[9] = String.IsNullOrEmpty(PersonInfo.Selectedgender) ? new SqlParameter("@Gender", DBNull.Value) : new SqlParameter("@Gender", PersonInfo.Selectedgender);
                Parameters[10] = new SqlParameter("@DOB", ConvertDatetimeDB(PersonInfo.DOB));
                Parameters[11] = new SqlParameter("@Income", ConvertDecimalToDb(PersonInfo.AnnualIncome));
                Parameters[12] = String.IsNullOrEmpty(PersonInfo.SelectedOccupation) ? new SqlParameter("@Occupation", DBNull.Value) : new SqlParameter("@Occupation", PersonInfo.SelectedOccupation);
                Parameters[13] = String.IsNullOrEmpty(PersonInfo.SelectedDeptId) ? new SqlParameter("@DeptId", DBNull.Value) : new SqlParameter("@DeptId", PersonInfo.SelectedDeptId);
                Parameters[14] = String.IsNullOrEmpty(PersonInfo.DrivingLicense) ? new SqlParameter("@DrivingLic", DBNull.Value) : new SqlParameter("@DrivingLic", PersonInfo.DrivingLicense);
                Parameters[15] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[15].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebEntityMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "Save"
        #endregion "Person Info"

        #region "Location Acceptance List"
        #region "Table"
        public async Task<List<LocationAcceptListModel>> GetLocationAcceptance(string AcctNo, CardnAccNo _CardnAccNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
                Parameters[2] = String.IsNullOrEmpty(_CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _CardnAccNo.CardNo);

                var reader = await objDataEngine.ExecuteCommandAsync("WebLocationAcceptanceListSelect", CommandType.StoredProcedure, Parameters);
                var LocationList = new List<LocationAcceptListModel>();

                while (reader.Read())
                {
                    LocationList.Add(new LocationAcceptListModel
                    {
                        //MerchantId = Convert.ToString(reader["Dealer"]),
                        DBAName = Convert.ToString(reader["DBA Name"]),
                        s_state = Convert.ToString(reader["DBA City"]),
                        UserId = Convert.ToString(reader["User Id"]),
                        CreationDate = Convert.ToString(reader["Creation Date"]),
                        SiteId = Convert.ToString(reader["SiteId"]),
                        BusnLoc = Convert.ToString(reader["MerchantId"]),

                    });
                };
                return LocationList;
            }
            finally
            {

                objDataEngine.CloseConnection();
            }
        }
        #endregion "Table"

        #region "Form"
        public async Task<LocationAcceptListModel> LocationAcceptanceSelect(string AcctNo, string BusnLoc, string CardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
                Parameters[2] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
                Parameters[2] = String.IsNullOrEmpty(BusnLoc) ? new SqlParameter("@BusnLoc", DBNull.Value) : new SqlParameter("@BusnLoc", BusnLoc);

                var reader = await objDataEngine.ExecuteCommandAsync("WebLocationAcceptanceSelect", CommandType.StoredProcedure, Parameters);

                var LocationAccptInfo = new LocationAcceptListModel();
                while (reader.Read())
                {
                    LocationAccptInfo.s_state = Convert.ToString(reader["BusnLocation"]);
                    LocationAccptInfo.UserId = Convert.ToString(reader["User Id"]);
                    LocationAccptInfo.CreationDate = DateConverter(reader["Creation Date"]);


                    return LocationAccptInfo;
                }
                return new LocationAcceptListModel();
            }
            finally
            {

                objDataEngine.CloseConnection();
            }

        }
        #endregion "Form"

        #region "Save"
        public async Task<MsgRetriever> SaveLocationAccept(LocationAcceptListModel LocationAcceptance)
        {
            MsgRetriever Descp = new MsgRetriever();
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                if (LocationAcceptance.SelectedStates[0] != "")
                {
                    foreach (var x in LocationAcceptance.SelectedStates)
                    {
                        objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
                        objDataEngine.InitiateConnection();
                        SqlParameter[] Parameters = new SqlParameter[7];
                        Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                        Parameters[1] = String.IsNullOrEmpty(LocationAcceptance._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", LocationAcceptance._CardnAccNo.AccNo);
                        Parameters[2] = String.IsNullOrEmpty(LocationAcceptance._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", LocationAcceptance._CardnAccNo.CardNo);
                        Parameters[3] = new SqlParameter("@State", Convert.ToString(x));
                        Parameters[4] = new SqlParameter("@BusnLocation", DBNull.Value);
                        Parameters[5] = new SqlParameter("@UserId", GetUserId);
                        Parameters[6] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                        Parameters[6].Direction = ParameterDirection.ReturnValue;

                        var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebLocationAcceptanceMaint", CommandType.StoredProcedure, Parameters);
                        var result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                        Descp = await GetMessageCode(result);
                        objDataEngine.CloseConnection();
                    }
                    objDataEngine.CloseConnection();
                    return Descp;
                }

                else
                {
                    objDataEngine.InitiateConnection();
                    SqlParameter[] Parameters = new SqlParameter[7];
                    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                    Parameters[1] = String.IsNullOrEmpty(LocationAcceptance._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", LocationAcceptance._CardnAccNo.AccNo);
                    Parameters[2] = String.IsNullOrEmpty(LocationAcceptance._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", LocationAcceptance._CardnAccNo.CardNo);
                    Parameters[3] = new SqlParameter("@State", null);
                    Parameters[4] = String.IsNullOrEmpty(LocationAcceptance.BusnLoc) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", LocationAcceptance.BusnLoc);
                    Parameters[5] = new SqlParameter("@UserId", GetUserId);
                    Parameters[6] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                    Parameters[6].Direction = ParameterDirection.ReturnValue;

                    var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebLocationAcceptanceMaint", CommandType.StoredProcedure, Parameters);
                    var result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                    Descp = await GetMessageCode(result);
                }
                return Descp;
            }
           finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "Save"

        #region "delete"
        public async Task<MsgRetriever> DeleteLocationAcceptance(string AcctNo, string BusnLocation, string CardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo ", AcctNo);
                Parameters[2] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
                Parameters[3] = String.IsNullOrEmpty(BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", BusnLocation);
                Parameters[4] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebLocationAcceptanceDelete", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                MsgRetriever Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "delete"
        #endregion "Location Acceptance List"



        #region "Card Replacement'
        #region "Form"
        public async Task<CardReplacement> CardReplacementInfoSelect(string CardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
                var reader = await objDataEngine.ExecuteCommandAsync("WebCardReplacementSelect", CommandType.StoredProcedure, Parameters);
                var CardReplacementInfo = new CardReplacement();
                while (reader.Read())
                {
                    CardReplacementInfo.NewCardNo = Convert.ToString(reader["NewCardNo"]);
                    CardReplacementInfo.XReferenceNo = Convert.ToString(reader["PrevCardNo"]);
                    CardReplacementInfo.ExpiryDate = DateConverter(reader["CardExpiry"]);
                    CardReplacementInfo.TerminatedDate = Convert.ToString(reader["TerminatedDate"]);
                    CardReplacementInfo.SelectedCurrentStatus = Convert.ToString(reader["Sts"]);
                    CardReplacementInfo.Remarks = Convert.ToString(reader["Remarks"]);
                    CardReplacementInfo.SelectedFeeCd = Convert.ToString(reader["FeeCd"]);
                    CardReplacementInfo.SelectedReasonCode = Convert.ToString(reader["ReasonCd"]);
                    CardReplacementInfo.SelectedCardMediaType = Convert.ToString(reader["CardMedia"]);
                    //objDataEngine.CloseConnection();
                };

                if (string.IsNullOrEmpty(CardReplacementInfo.SelectedFeeCd))
                {
                    CardReplacementInfo.SelectedFeeCd = "301";
                }
                return CardReplacementInfo;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        
        }
        #endregion "Form

        #region "Save"
        public async Task<MsgRetriever> SaveCardReplacement(CardReplacement CardReplace)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[10];

                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(CardReplace.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardReplace.CardNo);
                Parameters[2] = new SqlParameter("@NewCardNo", SqlDbType.VarChar, 19);
                Parameters[2].Direction = ParameterDirection.Output;
                Parameters[3] = String.IsNullOrEmpty(CardReplace.ExpiryDate) ? new SqlParameter("@ExpiryDate", DBNull.Value) : new SqlParameter("@ExpiryDate", CardReplace.ExpiryDate);// new SqlParameter("@ExpiryDate", ConvertDatetimeDB(CardReplace.ExpiryDate));
                Parameters[4] = String.IsNullOrEmpty(CardReplace.SelectedFeeCd) ? new SqlParameter("@FeeCd", DBNull.Value) : new SqlParameter("@FeeCd", CardReplace.SelectedFeeCd);
                Parameters[5] = String.IsNullOrEmpty(CardReplace.SelectedReasonCode) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", CardReplace.SelectedReasonCode);
                Parameters[6] = String.IsNullOrEmpty(CardReplace.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", CardReplace.Remarks);
                Parameters[7] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[8] = String.IsNullOrEmpty(CardReplace.SelectedCardMediaType) ? new SqlParameter("@CardMedia", DBNull.Value) : new SqlParameter("@CardMedia", CardReplace.SelectedCardMediaType);
                Parameters[9] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[9].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebCardReplacementMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                NewcardNo = Convert.ToString(Cmd.Parameters["@NewCardNo"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "Save"
        #endregion "Card Replacement"
    }
}
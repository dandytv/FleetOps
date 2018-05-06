
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FleetOps.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using System.Collections;
using CCMS.ModelSector;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class BusnLocOps : BaseClass
    {
        public string EntityId { get; set; }
        public string BusnLoc { get; set; }

        #region "Business Location"
        public async Task<MerchantDetails> GetBusinessLocationGeneralInfoDetail(string acctNo, string BusnLocation)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
            Parameters[1] = String.IsNullOrEmpty(BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", BusnLocation);


            var Reader = objDataEngine.ExecuteCommand("WebBusnLocationGeneralInfoSelect", CommandType.StoredProcedure, Parameters);

            while (Reader.Read())
            {
                var _BusinessLocation = new MerchantDetails
                {
                    BusinessName = Convert.ToString(Reader["BusnName"]),
                    AgreementNo = Convert.ToString(Reader["AgreementNo"]),
                    AgreementDate = BaseClass.DateConverter(Reader["AgreementDate"]),
                    PersonInCharge = Convert.ToString(Reader["PersonInCharge"]),
                    Ownership = await BaseClass.WebGetRefLib("MerchOwnership"),
                    selectedOwnershsip = Convert.ToString(Reader["Ownership"]),
                    BusnSize = Convert.ToString(Reader["Establishment"]),
                    SIC = await BaseClass.WebGetMerchType("S"),
                    SelectedSIC = Convert.ToString(Reader["Sic"]),
                    MCC = Convert.ToString(Reader["Mcc"]),
                    CoRegNo = Convert.ToString(Reader["CoRegNo"]),
                    CoRegName = Convert.ToString(Reader["CoRegName"]),
                    CoRegDate = BaseClass.DateConverter(Reader["CoRegDate"]),
                    OwnershipTrsfDate = Convert.ToString(Reader["OwnershipTrsfDate"]),
                    OwnershipTo = Convert.ToString(Reader["OwnershipTo"]),
                    DBAName = Convert.ToString(Reader["DBAName"]),
                    DBARegion = await BaseClass.WebGetRefLib("RegionCd"),
                    SelectDBARegion = Convert.ToString(Reader["DBARegion"]),
                    DBACity = await BaseClass.WebGetRefLib("City"),
                    SelectDBACity = Convert.ToString(Reader["DBACity"]),
                    DBAState = await BaseClass.WebGetState(GetIssNo, null),
                    SelectDBAState = Convert.ToString(Reader["DBAState"]),
                    PayeeName = Convert.ToString(Reader["PayeeName"]),
                    AutoDebitInd = BaseClass.BoolConverter(Reader["AutoDebitInd"]),
                    BankAcctName = Convert.ToString(Reader["BankName"]),
                    BankAcctType = await BaseClass.WebGetRefLib("BankAcctType"),
                    selectedBankAcctType = Convert.ToString(Reader["BankAcctType"]),
                    BankAccountNo = Convert.ToString(Reader["BankAcctNo"]),
                    BankBranchCode = await BaseClass.WebGetRefLib("BranchCd"),
                    SelectedBankBranchCode = Convert.ToString(Reader["BankBranchCd"]),
                    CurrentStatus = await BaseClass.WebGetRefLib("MerchAcctSts"),
                    selectedCurrentStatus = Convert.ToString(Reader["Sts"]),
                    EntityId = Convert.ToString(Reader["EntityId"]),                    
                    WithholdingTaxInd = BaseClass.BoolConverter(Reader["WithholdingTaxInd"]),
                    WithholdingTaxRate = Convert.ToString(Reader["WithholdingTaxRate"]),
                    cycNo = await BaseClass.WebGetCycle("A"),
                    SelectedcycNo = Convert.ToString(Reader["CycNo"]),
                    UserID = Convert.ToString(Reader["UserId"]),
                    CreationDate = BaseClass.DateConverter(Reader["CreationDate"]),
                    ReasonCd = await BaseClass.WebGetRefLib("MerchReasonCd"),
                    SelectedReasonCode = Convert.ToString(Reader["ReasonCd"]),
                    StmtPrintInd = BaseClass.BoolConverter(Reader["StmtPrint"]),
                    SiteId = Convert.ToString(Reader["ReconAcct"]),
                    BusnLoc = Convert.ToString(Reader["BusnLocation"]),
                    //PaymentTerms=Convert.ToString(Reader["PaymentTrms"]),
                    // TaxCategory =Convert.ToString(Reader["TaxCode"])
                };
                objDataEngine.CloseConnection();
                return _BusinessLocation;
            };
            return new MerchantDetails();
        }
        public async Task<MsgRetriever> SaveBusnLocationGeneralInfo(MerchantDetails merch)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[41];
            Parameters[0] = new SqlParameter("@Func", GetIssNo);
            Parameters[1] = new SqlParameter("@AcqNo", this.GetAcqNo);
            Parameters[2] = String.IsNullOrEmpty(merch.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", merch.AcctNo);
            Parameters[3] = String.IsNullOrEmpty(merch.BusnLoc) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", merch.BusnLoc);
            Parameters[4] = String.IsNullOrEmpty(merch.BusinessName) ? new SqlParameter("@BusnName", DBNull.Value) : new SqlParameter("@BusnName", merch.BusinessName);
            Parameters[5] = String.IsNullOrEmpty(merch.SiteId) ? new SqlParameter("@SiteId", DBNull.Value) : new SqlParameter("@SiteId", merch.SiteId);
            Parameters[6] = String.IsNullOrEmpty(merch.AgreementNo) ? new SqlParameter("@AgreeNo", DBNull.Value) : new SqlParameter("@AgreeNo", merch.AgreementNo);
            Parameters[7] = new SqlParameter("@AgreeDate", BaseClass.DateConverterDB(merch.AgreementDate));
            Parameters[8] = String.IsNullOrEmpty(merch.PersonInCharge) ? new SqlParameter("@PersonInChrg", DBNull.Value) : new SqlParameter("@PersonInChrg", merch.PersonInCharge);
            Parameters[9] = String.IsNullOrEmpty(merch.selectedOwnershsip) ? new SqlParameter("@Ownership", DBNull.Value) : new SqlParameter("@Ownership", merch.selectedOwnershsip);
            Parameters[10] = String.IsNullOrEmpty(merch.BusnSize) ? new SqlParameter("@Establishment", DBNull.Value) : new SqlParameter("@Establishment", merch.BusnSize);
            Parameters[11] = String.IsNullOrEmpty(merch.SelectedSIC) ? new SqlParameter("@Sic", DBNull.Value) : new SqlParameter("@Sic", merch.SelectedSIC);
            Parameters[12] = String.IsNullOrEmpty(merch.MCC) ? new SqlParameter("@Mcc", DBNull.Value) : new SqlParameter("@Mcc", merch.MCC);
            Parameters[13] = String.IsNullOrEmpty(merch.CoRegNo) ? new SqlParameter("@CoRegNo", DBNull.Value) : new SqlParameter("@CoRegNo", merch.CoRegNo);
            Parameters[14] = String.IsNullOrEmpty(merch.CoRegName) ? new SqlParameter("@CoRegName", DBNull.Value) : new SqlParameter("@CoRegName", merch.CoRegName);
            Parameters[15] = new SqlParameter("@CoRegDate", BaseClass.DateConverterDB(merch.CoRegDate));
            Parameters[16] = new SqlParameter("@OwnershipTrsfDate", BaseClass.DateConverterDB(merch.OwnershipTrsfDate));
            Parameters[17] = String.IsNullOrEmpty(merch.OwnershipTo) ? new SqlParameter("@OwnershipTo", DBNull.Value) : new SqlParameter("@OwnershipTo", merch.OwnershipTo);
            Parameters[18] = String.IsNullOrEmpty(merch.DBAName) ? new SqlParameter("@DBAName", DBNull.Value) : new SqlParameter("@DBAName", merch.DBAName);
            Parameters[19] = String.IsNullOrEmpty(merch.SelectDBARegion) ? new SqlParameter("@DBARegion", DBNull.Value) : new SqlParameter("@DBARegion", merch.SelectDBARegion);
            Parameters[20] = String.IsNullOrEmpty(merch.SelectDBACity) ? new SqlParameter("@DBACity", DBNull.Value) : new SqlParameter("@DBACity", merch.SelectDBACity);
            Parameters[21] = String.IsNullOrEmpty(merch.SelectDBAState) ? new SqlParameter("@DBAState", DBNull.Value) : new SqlParameter("@DBAState", merch.SelectDBAState);
            Parameters[22] = String.IsNullOrEmpty(merch.PayeeName) ? new SqlParameter("@PayeeName", DBNull.Value) : new SqlParameter("@PayeeName", merch.PayeeName);
            Parameters[23] = new SqlParameter("@AutoDebit", BaseClass.ConvertBoolDB(merch.AutoDebitInd));
            Parameters[24] = String.IsNullOrEmpty(merch.BankAcctName) ? new SqlParameter("@BankName", DBNull.Value) : new SqlParameter("@BankName", merch.BankAcctName);
            Parameters[25] = String.IsNullOrEmpty(merch.selectedBankAcctType) ? new SqlParameter("@BankAcctType", DBNull.Value) : new SqlParameter("@BankAcctType", merch.selectedBankAcctType);
            Parameters[26] = String.IsNullOrEmpty(merch.BankAccountNo) ? new SqlParameter("@BankAcctNo", DBNull.Value) : new SqlParameter("@BankAcctNo", merch.BankAccountNo);
            Parameters[27] = String.IsNullOrEmpty(merch.SelectedBankBranchCode) ? new SqlParameter("@BankBranchCd", DBNull.Value) : new SqlParameter("@BankBranchCd", merch.SelectedBankBranchCode);
            Parameters[28] = String.IsNullOrEmpty(merch.selectedCurrentStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", merch.selectedCurrentStatus);
            Parameters[29] = String.IsNullOrEmpty(merch.EntityId) ? new SqlParameter("@EntityId", DBNull.Value) : new SqlParameter("@EntityId", merch.EntityId);
            Parameters[30] = String.IsNullOrEmpty(merch.TaxId) ? new SqlParameter("@TaxId", DBNull.Value) : new SqlParameter("@TaxId", merch.TaxId);
            Parameters[31] = new SqlParameter("@WithholdInd", BaseClass.ConvertBoolDB(merch.WithholdingTaxInd));
            Parameters[32] = String.IsNullOrEmpty(merch.WithholdingTaxRate) ? new SqlParameter("@WithholdRate", DBNull.Value) : new SqlParameter("@WithholdRate", merch.WithholdingTaxRate);
            Parameters[33] = String.IsNullOrEmpty(merch.SelectedcycNo) ? new SqlParameter("@CycNo", DBNull.Value) : new SqlParameter("@CycNo", merch.SelectedcycNo);
            Parameters[34] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[35] = new SqlParameter("@CreationDate", BaseClass.DateConverterDB(merch.CreationDate));
            Parameters[36] = String.IsNullOrEmpty(merch.SelectedReasonCode) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", merch.SelectedReasonCode);
            Parameters[37] = new SqlParameter("@StmtPrint", BaseClass.ConvertBoolDB(merch.StmtPrintInd));
            
            Parameters[38] = new SqlParameter("@oBusnLocation", SqlDbType.VarChar, 20);
            Parameters[38].Direction = ParameterDirection.Output;
            Parameters[39] = new SqlParameter("@oEntityId", SqlDbType.VarChar, 20);
            Parameters[39].Direction = ParameterDirection.Output;
            Parameters[40] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[40].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebBusnLocationGeneralInfoMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            this.EntityId = Convert.ToString(Cmd.Parameters["@oEntityId"].Value);
            this.BusnLoc = Convert.ToString(Cmd.Parameters["@oBusnLocation"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return await Descp;
        }
        #endregion

        #region "Terminal Inventory"
        public List<BusnLocTerminal> GetBusnLocTermList(string BusnLocation)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
            Parameters[1] = String.IsNullOrEmpty(BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", BusnLocation);

            var execResult = objDataEngine.ExecuteCommand("WebBusnLocationTerminalListSelect", CommandType.StoredProcedure, Parameters);
            var _BusnLocTermList = new List<BusnLocTerminal>();

            while (execResult.Read())
            {
                _BusnLocTermList.Add(new BusnLocTerminal
                {
                    TermId = Convert.ToString(execResult["TermId"]),
                    SelectedStatus = Convert.ToString(execResult["Status"]),
                    RawStatus = Convert.ToString(execResult["Sts"]),
                    DeployDate = DateConverter(execResult["DeployDate"]),
                    Replacement = Convert.ToString(execResult["ReplacedByTermId"]),
                    ReplacedDate = DateConverter(execResult["ReplacedDate"]),
                    SelectedReasonCode = Convert.ToString(execResult["Reason"]),
                    IPEK = Convert.ToString(execResult["IPEK"]),
                    SettlementStart = DateConverter(execResult["SettleFromTime"]),
                    SettlementEnd = DateConverter(execResult["SettleToTime"]),
                    LastBatchId = ConvertInt(execResult["LastBatchId"]),
                    SettleTxnId = ConvertInt(execResult["SettleTxnId"]),
                    SelectedProdType = Convert.ToString(execResult["DeviceModel"]),
                    SerialNo = Convert.ToString(execResult["SerialNo"]),
                    Remarks = Convert.ToString(execResult["Remarks"]),
                    CreationDate = DateConverter(execResult["CreationDate"]),
                    UserId = Convert.ToString(execResult["UserId"]),
                    LastUpdDate = DateConverter(execResult["LastUpdDate"]),
                });

            };
            objDataEngine.CloseConnection();
            return _BusnLocTermList;
        }
        public async Task<BusnLocTerminal> GetBusnLocTermDetail(string TermId, string BusnLocation)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
            Parameters[1] = String.IsNullOrEmpty(BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", BusnLocation);
            Parameters[2] = String.IsNullOrEmpty(TermId) ? new SqlParameter("@TermId", DBNull.Value) : new SqlParameter("@TermId", TermId);

            var execResult = objDataEngine.ExecuteCommand("WebBusnLocationTerminalSelect", CommandType.StoredProcedure, Parameters);

            while (execResult.Read())
            {
                var _BusnLocTermDetail = new BusnLocTerminal
                {
                    TermId = Convert.ToString(execResult["TermId"]),
                    SelectedTermType = Convert.ToString(execResult["TermType"]),
                    SelectedStatus = Convert.ToString(execResult["Sts"]),
                    DeployDate = DateConverter(execResult["DeployDate"]),
                    SaleTerritory = Convert.ToString(execResult["SaleTerritory"]),
                    Replacement = Convert.ToString(execResult["ReplacedByTermId"]),
                    ReplacedDate = DateConverter(execResult["ReplacedDate"]),
                    ReasonCd = await BaseClass.WebGetRefLib("TermReasonCd"),
                    SelectedReasonCode = Convert.ToString(execResult["ReasonCd"]),
                    IPEK = Convert.ToString(execResult["IPEK"]),
                    SettlementStart = DateConverter(execResult["SettleFromTime"]),
                    SettlementEnd = DateConverter(execResult["SettleToTime"]),
                    LastBatchId = ConvertInt(execResult["LastBatchId"]),
                    SettleTxnId = ConvertInt(execResult["SettleTxnId"]),
                    SelectedProdType = Convert.ToString(execResult["DeviceModel"]),
                    SerialNo = Convert.ToString(execResult["SerialNo"]),
                    Remarks = Convert.ToString(execResult["Remarks"]),
                    CreationDate = DateConverter(execResult["CreationDate"]),
                    UserId = Convert.ToString(execResult["UserId"]),
                    LastUpdDate = DateConverter(execResult["LastUpdDate"]),
                };

                objDataEngine.CloseConnection();
                return _BusnLocTermDetail;

            };
            objDataEngine.CloseConnection();
            return new BusnLocTerminal();

        }
        public async Task<MsgRetriever> SaveBusnLocTerm(BusnLocTerminal _BusnLocTerminal)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[16];
            Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
            Parameters[1] = String.IsNullOrEmpty(_BusnLocTerminal.BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", _BusnLocTerminal.BusnLocation);
            Parameters[2] = String.IsNullOrEmpty(_BusnLocTerminal.TermId) ? new SqlParameter("@TermId", DBNull.Value) : new SqlParameter("@TermId", _BusnLocTerminal.TermId);
            Parameters[3] = String.IsNullOrEmpty(_BusnLocTerminal.SelectedStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _BusnLocTerminal.SelectedStatus);
            Parameters[4] = String.IsNullOrEmpty(_BusnLocTerminal.DeployDate) ? new SqlParameter("@DeployDate", DBNull.Value) : new SqlParameter("@DeployDate", ConvertDatetimeDB(_BusnLocTerminal.DeployDate));
            Parameters[5] = String.IsNullOrEmpty(_BusnLocTerminal.SaleTerritory) ? new SqlParameter("@SaleTerritory", DBNull.Value) : new SqlParameter("@SaleTerritory", _BusnLocTerminal.SaleTerritory);
            Parameters[6] = String.IsNullOrEmpty(_BusnLocTerminal.Replacement) ? new SqlParameter("@ReplacedByTermId", DBNull.Value) : new SqlParameter("@ReplacedByTermId", _BusnLocTerminal.Replacement);
            Parameters[7] = String.IsNullOrEmpty(_BusnLocTerminal.ReplacedDate) ? new SqlParameter("@ReplacedDate", DBNull.Value) : new SqlParameter("@ReplacedDate", ConvertDatetimeDB(_BusnLocTerminal.ReplacedDate));
            Parameters[8] = String.IsNullOrEmpty(_BusnLocTerminal.SelectedReasonCode) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", _BusnLocTerminal.SelectedReasonCode);
            Parameters[9] = String.IsNullOrEmpty(_BusnLocTerminal.IPEK) ? new SqlParameter("@IPEK", DBNull.Value) : new SqlParameter("@IPEK", _BusnLocTerminal.IPEK);
            Parameters[10] = String.IsNullOrEmpty(_BusnLocTerminal.SelectedProdType) ? new SqlParameter("@DeviceModel", DBNull.Value) : new SqlParameter("@DeviceModel", _BusnLocTerminal.SelectedProdType);
            Parameters[11] = String.IsNullOrEmpty(_BusnLocTerminal.SerialNo) ? new SqlParameter("@SerialNo", DBNull.Value) : new SqlParameter("@SerialNo", _BusnLocTerminal.SerialNo);
            Parameters[12] = String.IsNullOrEmpty(_BusnLocTerminal.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _BusnLocTerminal.Remarks);
            Parameters[13] = String.IsNullOrEmpty(_BusnLocTerminal.SelectedTermType) ? new SqlParameter("@TermType", DBNull.Value) : new SqlParameter("@TermType", _BusnLocTerminal.SelectedTermType);
            Parameters[14] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);

            Parameters[15] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[15].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebBusnLocationTerminalMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return await Descp;


        }
        #endregion
    }
}
using CCMS.ModelSector;
using FleetOps.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilities.DAL;

namespace FleetSys.Models
{
    public class EventConfigMaint : BaseClass
    {
        public string ScheduleId { get; set; }

        #region "User Level"

        public async Task<List<LookupParameters>> WebNtfyEvtConfListSelect()
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@UserId", GetUserId);
                var getObjData = await objEngine.ExecuteCommandAsync("[WebNtfyEvtConfListSelect]", CommandType.StoredProcedure, parameters);
                var list = new List<LookupParameters>();
                while (getObjData.Read())
                {
                    var item = new LookupParameters
                    {
                        EventTypeId = Convert.ToString(getObjData["Id"]),
                        SelectedEventType = Convert.ToString(getObjData["Type"]),
                        ShortDescp = Convert.ToString(getObjData["ShortDescp"]),
                        DetailedDescp = Convert.ToString(getObjData["Descp"]),
                        SelectedRefTo = Convert.ToString(getObjData["RefTo"]),
                        RefKey = Convert.ToString(getObjData["RefKey"]),
                        SelectedStatus = Convert.ToString(getObjData["Status"]),
                        LastUpdated = Convert.ToString(getObjData["UpdateDate"]),
                        UpdatedBy = Convert.ToString(getObjData["UpdateBy"])
                    };
                    list.Add(item);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }

        /*
        public async Task<MsgRetriever> WebNtfEvtConfDelete(string ScheduleId)
        {
            SqlParameter[] Parameters = new SqlParameter[2];
            SqlCommand cmd = new SqlCommand();
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                Parameters[0] = new SqlParameter("@ScheduleId", ScheduleId);
                Parameters[1] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[1].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebNtfEvtConfDelete", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        */

        //public async Task<MsgRetriever> WebNtfEvtConfRcptDelete(string @SchRcptId)
        //{
        //    SqlParameter[] Parameters = new SqlParameter[2];
        //    SqlCommand cmd = new SqlCommand();
        //    var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
        //    try
        //    {
        //        await objDataEngine.InitiateConnectionAsync();
        //        Parameters[0] = new SqlParameter("@SchRcptId", @SchRcptId);
        //        Parameters[1] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
        //        Parameters[1].Direction = ParameterDirection.ReturnValue;
        //        var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebNtfEvtConfRcptDelete", CommandType.StoredProcedure, Parameters);
        //        var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
        //        var Descp = await GetMessageCode(Result);
        //        return Descp;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}
        public async Task<List<LookupParameters>> WebNtfyEventConfSelect(string PlanId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@EventScheduleId", PlanId);
                var execResult = await objDataEngine.ExecuteCommandAsync("[WebNtfyEventConfSelect]", CommandType.StoredProcedure, Parameters);
                var _Parameters = new List<LookupParameters>();
                while (execResult.Read())
                {
                    var _Parameter = new LookupParameters
                     {
                         BitmapAmt = Convert.ToString(execResult["ParamInd"]),
                         EvtPlanDetailId = Convert.ToString(execResult["EventPlanDetailId"]),
                         EventScheduleId = PlanId,
                         EventTypeId = Convert.ToString(execResult["EventTypeId"]),
                         EventPlanId = Convert.ToString(execResult["EventPlanId"]),
                         DetailedDescp = Convert.ToString(execResult["Descp"]),
                         type = Convert.ToString(execResult["Type"]),
                         TypeDesc = Convert.ToString(execResult["TypeDescp"]),
                         SelectedPriority = Convert.ToString(execResult["Severity"]),
                         SelectedOwner = Convert.ToString(execResult["OwnerType"]),
                         SelectedStatus = Convert.ToString(execResult["Sts"]),
                         SelectedRefTo = Convert.ToString(execResult["Refto"]),
                         RefKey = Convert.ToString(execResult["Refkey"]),
                         CompanyName = Convert.ToString(execResult["CmpyName"]),
                         MaxOccur = Convert.ToString(execResult["MaxOccur"]),
                         SelectedFrequency = Convert.ToString(execResult["OccurPeriodType"]),
                         MinIntVal = Convert.ToString(execResult["MinIntVal"]),
                         MaxIntVal = Convert.ToString(execResult["MaxIntVal"]),
                         MinMoneyVal = ConverterDecimal(execResult["MinMoneyVal"]),
                         MaxMoneyVal = ConverterDecimal(execResult["MaxMoneyVal"]),
                         MinDateVal = DateConverter(execResult["MinDateVal"]),
                         MaxDateVal = DateConverter(execResult["MaxDateVal"]),
                         MinTimeVal = Convert.ToString(execResult["MinTimeVal"]),
                         MaxTimeVal = Convert.ToString(execResult["MaxTimeVal"]),
                         VarCharVal = Convert.ToString(execResult["VarCharVal"]),
                         PeriodType = Convert.ToString(execResult["PeriodType"]),
                         PeriodInterval = Convert.ToString(execResult["PeriodInterval"]),
                         ApplyAllInd = BoolConverter(execResult["AllAppyInd"]),
                         DefaultInd = BoolConverter(execResult["DefaultInd"]),
                         LastUpdated = Convert.ToString(execResult["LastUpdDate"]),
                         UpdatedBy = Convert.ToString(execResult["UserId"]),
                         ParamInd = Convert.ToString(execResult["ParamInd"]),
                         NotifyInd = ConvertInt(execResult["EvtTypeChannelInd"])
                     };
                    _Parameters.Add(_Parameter);
                }
                return _Parameters;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }


        //[HttpPost]
        //public async Task<MsgRetriever> WebEventAcctConfMaint(LookupParameters _LookupParameters)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
        //    try
        //    {
        //        await objDataEngine.InitiateConnectionAsync();
        //        SqlParameter[] _Parameter = new SqlParameter[19];
        //        SqlCommand cmd = new SqlCommand();
        //        var dtNtfyEventTbl = new DataTable();
        //        var dtNtfyEventRcptTbl = new DataTable();
        //        dtNtfyEventTbl.Columns.Add("EvtNtfyDetailId");
        //        dtNtfyEventTbl.Columns.Add("MinIntVal");
        //        dtNtfyEventTbl.Columns.Add("MaxIntVal");
        //        dtNtfyEventTbl.Columns.Add("MinMoneyVal");
        //        dtNtfyEventTbl.Columns.Add("MaxMoneyVal");
        //        dtNtfyEventTbl.Columns.Add("MinDateVal");
        //        dtNtfyEventTbl.Columns.Add("MaxDateVal");
        //        dtNtfyEventTbl.Columns.Add("MinTimeVal");
        //        dtNtfyEventTbl.Columns.Add("MaxTimeVal");
        //        dtNtfyEventTbl.Columns.Add("VarCharVal");
        //        dtNtfyEventTbl.Columns.Add("PeriodType");
        //        dtNtfyEventTbl.Columns.Add("PeriodInterval");
        //        if (_LookupParameters.ProductItems != null)
        //        {
        //            foreach (var item in _LookupParameters.ProductItems)
        //            {
        //                DataRow dr = dtNtfyEventTbl.NewRow();
        //                dr["EvtNtfyDetailId"] = item.EvtPlanDetailId;
        //                dr["MinIntVal"] = ConvertLongToDb(item.MinIntVal);
        //                dr["MaxIntVal"] = ConvertLongToDb(item.MaxIntVal);
        //                dr["MinMoneyVal"] = ConvertDecimalToDb(item.MinMoneyVal);
        //                dr["MaxMoneyVal"] = ConvertDecimalToDb(item.MaxMoneyVal);
        //                dr["MinDateVal"] = ConvertDatetimeDB(item.MinDateVal);
        //                dr["MaxDateVal"] = ConvertDatetimeDB(item.MaxDateVal);
        //                dr["MinTimeVal"] = item.MinTimeVal;
        //                dr["MaxTimeVal"] = item.MaxTimeVal;
        //                dr["VarCharVal"] = item.VarCharVal;
        //                dr["PeriodType"] = item.PeriodType;
        //                dr["PeriodInterval"] = item.PeriodInterval;
        //                dtNtfyEventTbl.Rows.Add(dr);
        //            }
        //        }
        //        dtNtfyEventRcptTbl.Columns.Add("EventRecipientId");
        //        dtNtfyEventRcptTbl.Columns.Add("EventScheduleId");
        //        dtNtfyEventRcptTbl.Columns.Add("ContactName");
        //        dtNtfyEventRcptTbl.Columns.Add("ContactNo");
        //        dtNtfyEventRcptTbl.Columns.Add("ChannelInd");
        //        dtNtfyEventRcptTbl.Columns.Add("LangInd");
        //        if (_LookupParameters._EventRcptList != null)
        //        {
        //            foreach (var item in _LookupParameters._EventRcptList)
        //            {
        //                DataRow dr = dtNtfyEventRcptTbl.NewRow();
        //                dr["EventRecipientId"] = string.IsNullOrEmpty(Convert.ToString(item.Id)) ? (object)DBNull.Value : item.Id;
        //                dr["EventScheduleId"] = string.IsNullOrEmpty(_LookupParameters.EventScheduleId) ? (object)DBNull.Value : _LookupParameters.EventScheduleId;
        //                dr["ContactName"] = string.IsNullOrEmpty(item.ContactName) ? (object)DBNull.Value : item.ContactName;
        //                dr["ContactNo"] = string.IsNullOrEmpty(item.ContactNo) ? (object)DBNull.Value : item.ContactNo;
        //                dr["ChannelInd"] = item.ChannelInd;
        //                dr["LangInd"] = string.IsNullOrEmpty(item.LangInd) ? (object)DBNull.Value : item.LangInd;
        //                dtNtfyEventRcptTbl.Rows.Add(dr);
        //            }
        //        }
        //        _Parameter[0] = new SqlParameter("@IssNo", GetIssNo);
        //        _Parameter[1] = new SqlParameter("@EventSchId", string.IsNullOrEmpty(_LookupParameters.EventScheduleId) ? (object)DBNull.Value : _LookupParameters.EventScheduleId);
        //        _Parameter[2] = new SqlParameter("@EventTypeID", string.IsNullOrEmpty(_LookupParameters.EventTypeId) ? (object)DBNull.Value : _LookupParameters.EventTypeId);
        //        _Parameter[3] = new SqlParameter("@EventPlanId", string.IsNullOrEmpty(_LookupParameters.PlanId) ? (object)DBNull.Value : _LookupParameters.PlanId);
        //        _Parameter[4] = new SqlParameter("@ShortDescp", string.IsNullOrEmpty(_LookupParameters.ShortDescp) ? (object)DBNull.Value : _LookupParameters.ShortDescp);
        //        _Parameter[5] = new SqlParameter("@Scope", string.IsNullOrEmpty(_LookupParameters.SelectedOwner) ? (object)DBNull.Value : _LookupParameters.SelectedOwner);
        //        _Parameter[6] = new SqlParameter("@Sts", string.IsNullOrEmpty(_LookupParameters.SelectedStatus) ? (object)DBNull.Value : _LookupParameters.SelectedStatus);
        //        _Parameter[7] = new SqlParameter("@ParamInd", "");
        //        _Parameter[8] = new SqlParameter("@RefTo", string.IsNullOrEmpty(_LookupParameters.SelectedRefTo) ? (object)DBNull.Value : _LookupParameters.SelectedRefTo);
        //        _Parameter[9] = new SqlParameter("@RefKey", string.IsNullOrEmpty(_LookupParameters.RefKey) ? (object)DBNull.Value : _LookupParameters.RefKey);
        //        _Parameter[10] = new SqlParameter("@CntEvtOccur", string.IsNullOrEmpty(_LookupParameters.MaxOccur) ? (object)DBNull.Value : _LookupParameters.MaxOccur);
        //        _Parameter[11] = new SqlParameter("@EvtOccurType", string.IsNullOrEmpty(_LookupParameters.SelectedFrequency) ? (object)DBNull.Value : _LookupParameters.SelectedFrequency);
        //        _Parameter[12] = new SqlParameter("@UserId", GetUserId);
        //        _Parameter[13] = new SqlParameter("@ChannelInd", ConvertIntToDb(_LookupParameters.NotifyInd));
        //        _Parameter[14] = new SqlParameter("@DefaultInd", ConvertBoolDB(_LookupParameters.DefaultInd));
        //        _Parameter[15] = new SqlParameter("@NtfyEventTbl", dtNtfyEventTbl);
        //        _Parameter[16] = new SqlParameter("@NtfyEventRcptTbl", dtNtfyEventRcptTbl);
        //        _Parameter[17] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
        //        _Parameter[17].Direction = ParameterDirection.ReturnValue;
        //        _Parameter[18] = new SqlParameter("@NewEventSchId", SqlDbType.BigInt, 19);
        //        _Parameter[18].Direction = ParameterDirection.Output;
        //        var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebEventAcctConfMaint", CommandType.StoredProcedure, _Parameter);
        //        var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
        //        var Descp = await GetMessageCode(Result);
        //        this.ScheduleId = Convert.ToString(Cmd.Parameters["@NewEventSchId"].Value);
        //        Descp.Id = this.ScheduleId;
        //        return Descp;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}


        [HttpPost]
        public async Task<MsgRetriever> WebNtfyEvtConfMaint(LookupParameters _LookupParameters)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] _Parameter = new SqlParameter[19];
                SqlCommand cmd = new SqlCommand();
                var dtNtfyEventTbl = new DataTable();
                var dtNtfyEventRcptTbl = new DataTable();
                dtNtfyEventTbl.Columns.Add("EvtNtfyDetailId");
                dtNtfyEventTbl.Columns.Add("MinIntVal");
                dtNtfyEventTbl.Columns.Add("MaxIntVal");
                dtNtfyEventTbl.Columns.Add("MinMoneyVal");
                dtNtfyEventTbl.Columns.Add("MaxMoneyVal");
                dtNtfyEventTbl.Columns.Add("MinDateVal");
                dtNtfyEventTbl.Columns.Add("MaxDateVal");
                dtNtfyEventTbl.Columns.Add("MinTimeVal");
                dtNtfyEventTbl.Columns.Add("MaxTimeVal");
                dtNtfyEventTbl.Columns.Add("VarCharVal");
                dtNtfyEventTbl.Columns.Add("PeriodType");
                dtNtfyEventTbl.Columns.Add("PeriodInterval");
                if (_LookupParameters.ProductItems != null)
                {
                    foreach (var item in _LookupParameters.ProductItems)
                    {
                        DataRow dr = dtNtfyEventTbl.NewRow();
                        dr["EvtNtfyDetailId"] = item.EvtPlanDetailId;
                        dr["MinIntVal"] = ConvertLongToDb(item.MinIntVal);
                        dr["MaxIntVal"] = ConvertLongToDb(item.MaxIntVal);
                        dr["MinMoneyVal"] = ConvertDecimalToDb(item.MinMoneyVal);
                        dr["MaxMoneyVal"] = ConvertDecimalToDb(item.MaxMoneyVal);
                        dr["MinDateVal"] = ConvertDatetimeDB(item.MinDateVal);
                        dr["MaxDateVal"] = ConvertDatetimeDB(item.MaxDateVal);
                        dr["MinTimeVal"] = item.MinTimeVal;
                        dr["MaxTimeVal"] = item.MaxTimeVal;
                        dr["VarCharVal"] = item.VarCharVal;
                        dr["PeriodType"] = item.PeriodType;
                        dr["PeriodInterval"] = item.PeriodInterval;
                        dtNtfyEventTbl.Rows.Add(dr);
                    }
                }
                dtNtfyEventRcptTbl.Columns.Add("EventRecipientId");
                dtNtfyEventRcptTbl.Columns.Add("EventScheduleId");
                dtNtfyEventRcptTbl.Columns.Add("ContactName");
                dtNtfyEventRcptTbl.Columns.Add("ContactNo");
                dtNtfyEventRcptTbl.Columns.Add("ChannelInd");
                dtNtfyEventRcptTbl.Columns.Add("LangInd");

                if (_LookupParameters._EventRcptList != null)
                {
                    foreach (var item in _LookupParameters._EventRcptList)
                    {
                        DataRow dr = dtNtfyEventRcptTbl.NewRow();
                        dr["EventRecipientId"] = string.IsNullOrEmpty(Convert.ToString(item.Id)) ? (object)DBNull.Value : item.Id;
                        dr["EventScheduleId"] = string.IsNullOrEmpty(_LookupParameters.EventScheduleId) ? (object)DBNull.Value : _LookupParameters.EventScheduleId;
                        dr["ContactName"] = string.IsNullOrEmpty(item.ContactName) ? (object)DBNull.Value : item.ContactName;
                        dr["ContactNo"] = string.IsNullOrEmpty(item.ContactNo) ? (object)DBNull.Value : item.ContactNo;
                        dr["ChannelInd"] = item.ChannelInd;
                        dr["LangInd"] = string.IsNullOrEmpty(item.LangInd) ? (object)DBNull.Value : item.LangInd;
                        dtNtfyEventRcptTbl.Rows.Add(dr);
                    }
                }
                _Parameter[0] = new SqlParameter("@IssNo", GetIssNo);
                _Parameter[1] = new SqlParameter("@EventSchId", string.IsNullOrEmpty(_LookupParameters.EventScheduleId) ? (object)DBNull.Value : _LookupParameters.EventScheduleId);
                _Parameter[2] = new SqlParameter("@EventTypeID", string.IsNullOrEmpty(_LookupParameters.EventTypeId) ? (object)DBNull.Value : _LookupParameters.EventTypeId);
                _Parameter[3] = new SqlParameter("@EventPlanId", string.IsNullOrEmpty(_LookupParameters.EventPlanId) ? (object)DBNull.Value : _LookupParameters.EventPlanId);
                _Parameter[4] = new SqlParameter("@ShortDescp", string.IsNullOrEmpty(_LookupParameters.ShortDescp) ? (object)DBNull.Value : _LookupParameters.ShortDescp);
                _Parameter[5] = new SqlParameter("@Scope", string.IsNullOrEmpty(_LookupParameters.SelectedOwner) ? (object)DBNull.Value : _LookupParameters.SelectedOwner);
                _Parameter[6] = new SqlParameter("@Sts", string.IsNullOrEmpty(_LookupParameters.SelectedStatus) ? (object)DBNull.Value : _LookupParameters.SelectedStatus);
                _Parameter[7] = new SqlParameter("@ParamInd", "");
                _Parameter[8] = new SqlParameter("@RefTo", string.IsNullOrEmpty(_LookupParameters.SelectedRefTo) ? (object)DBNull.Value : _LookupParameters.SelectedRefTo);
                _Parameter[9] = new SqlParameter("@RefKey", string.IsNullOrEmpty(_LookupParameters.RefKey) ? (object)DBNull.Value : _LookupParameters.RefKey);
                _Parameter[10] = new SqlParameter("@CntEvtOccur", string.IsNullOrEmpty(_LookupParameters.MaxOccur) ? (object)DBNull.Value : _LookupParameters.MaxOccur);
                _Parameter[11] = new SqlParameter("@EvtOccurType", string.IsNullOrEmpty(_LookupParameters.SelectedFrequency) ? (object)DBNull.Value : _LookupParameters.SelectedFrequency);
                _Parameter[12] = new SqlParameter("@UserId", GetUserId);
                _Parameter[13] = new SqlParameter("@ChannelInd", ConvertIntToDb(_LookupParameters.NotifyInd));
                _Parameter[14] = new SqlParameter("@DefaultInd", ConvertBoolDB(_LookupParameters.DefaultInd));
                _Parameter[15] = new SqlParameter("@NtfyEventTbl", dtNtfyEventTbl);
                _Parameter[16] = new SqlParameter("@NtfyEventRcptTbl", dtNtfyEventRcptTbl);
                _Parameter[17] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                _Parameter[17].Direction = ParameterDirection.ReturnValue;
                _Parameter[18] = new SqlParameter("@NewEventSchId", SqlDbType.BigInt, 19);
                _Parameter[18].Direction = ParameterDirection.Output;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebNtfyEvtConfMaint", CommandType.StoredProcedure, _Parameter);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                this.ScheduleId = String.IsNullOrEmpty(_LookupParameters.EventScheduleId) ? Convert.ToString(Cmd.Parameters["@NewEventSchId"].Value) : _LookupParameters.EventScheduleId;
                Descp.Id = this.ScheduleId;
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        //WebEventSelect

        #endregion

        #region "Account Level"

        public async Task<List<LookupParameters>> WebEventAcctConfListSelect(string RefTo, string RefKey)
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@RefKey", string.IsNullOrEmpty(RefKey) ? (object)DBNull.Value : RefKey);
                parameters[1] = new SqlParameter("@RefTo", string.IsNullOrEmpty(RefTo) ? (object)DBNull.Value : RefTo);
                var getObjData = await objEngine.ExecuteCommandAsync("[WebEventAcctConfListSelect]", CommandType.StoredProcedure, parameters);
                var list = new List<LookupParameters>();
                while (getObjData.Read())
                {
                    var item = new LookupParameters
                    {
                        EventTypeId = Convert.ToString(getObjData["EventTypeId"]),
                        EventScheduleId = Convert.ToString(getObjData["EventScheduleId"]),
                        type = Convert.ToString(getObjData["Type"]),
                        ShortDescp = Convert.ToString(getObjData["Short Description"]),
                        DetailedDescp = Convert.ToString(getObjData["Detailed Description"]),
                        SelectedStatus = Convert.ToString(getObjData["Status"]),
                        LastUpdated = Convert.ToString(getObjData["Update Date"]),
                        UpdatedBy = Convert.ToString(getObjData["Update By"])
                    };
                    list.Add(item);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }


        //public async Task<List<LookupParameters>> WebEventAcctConfSelect(string @EventTypeId, string @EventScheduleId, string @AcctNo)
        //{
        //    var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
        //    try
        //    {
        //        await objEngine.InitiateConnectionAsync();
        //        SqlParameter[] parameters = new SqlParameter[3];
        //        parameters[0] = new SqlParameter("@EventTypeId", string.IsNullOrEmpty(@EventTypeId) ? (object)DBNull.Value : @EventTypeId);
        //        parameters[1] = new SqlParameter("@EventScheduleId", string.IsNullOrEmpty(@EventScheduleId) ? (object)DBNull.Value : @EventScheduleId);
        //        parameters[2] = new SqlParameter("@AcctNo", string.IsNullOrEmpty(@AcctNo) ? (object)DBNull.Value : @AcctNo);
        //        var execResult = await objEngine.ExecuteCommandAsync("[WebEventAcctConfSelect]", CommandType.StoredProcedure, parameters);
        //        var list = new List<LookupParameters>();
        //        while (execResult.Read())
        //        {
        //            var _Parameter = new LookupParameters
        //            {
        //                EventScheduleId = Convert.ToString(execResult["EventScheduleId"]),
        //                EvtPlanDetailId = Convert.ToString(execResult["EventPlanDetailId"]),
        //                EventTypeId = Convert.ToString(execResult["EventTypeId"]),
        //                PlanId = Convert.ToString(execResult["EventPlanId"]),
        //                ShortDescp = Convert.ToString(execResult["ShortDescp"]),
        //                type = Convert.ToString(execResult["Type"]),
        //                TypeDesc = Convert.ToString(execResult["TypeDescp"]),
        //                SelectedPriority = Convert.ToString(execResult["Severity"]),
        //                SelectedOwner = Convert.ToString(execResult["OwnerType"]),
        //                SelectedStatus = Convert.ToString(execResult["Sts"]),
        //                Descp = Convert.ToString(execResult["Descp"]),
        //                SelectedRefTo = Convert.ToString(execResult["Refto"]),
        //                RefKey = Convert.ToString(execResult["Refkey"]),
        //                CompanyName = Convert.ToString(execResult["CmpyName"]),
        //                MaxOccur = Convert.ToString(execResult["FqyPeriod"]),
        //                SelectedFrequency = Convert.ToString(execResult["OccurPeriodType"]),
        //                MinIntVal = Convert.ToString(execResult["MinIntVal"]),
        //                MaxIntVal = Convert.ToString(execResult["MaxIntVal"]),
        //                MinMoneyVal = ConverterDecimal(execResult["MinMoneyVal"]),
        //                MaxMoneyVal = ConverterDecimal(execResult["MaxMoneyVal"]),
        //                MinDateVal = DateConverter(execResult["MinDateVal"]),
        //                MaxDateVal = DateConverter(execResult["MaxDateVal"]),
        //                MinTimeVal = Convert.ToString(execResult["MinTimeVal"]),
        //                MaxTimeVal = Convert.ToString(execResult["MaxTimeVal"]),
        //                VarCharVal = Convert.ToString(execResult["VarCharVal"]),
        //                PeriodInterval = Convert.ToString(execResult["PeriodInterval"]),
        //                PeriodType = Convert.ToString(execResult["PeriodType"]),
        //                DefaultInd = BoolConverter(execResult["DefaultInd"]),
        //                NotifyInd = ConvertInt(execResult["ChannelInd"]),
        //                TemplateDisplayer = Convert.ToString(execResult["ContentTmplt"]),
        //                LastUpdated = Convert.ToString(execResult["LastUpdDate"]),
        //                UpdatedBy = Convert.ToString(execResult["UserId"]),
        //                BitmapAmt = Convert.ToString(execResult["ParamInd"]),
        //            };
        //            list.Add(_Parameter);
        //        }
        //        return list;
        //    }
        //    finally
        //    {
        //        objEngine.CloseConnection();
        //    }

        //}

        //public async Task<List<EventRcptList>> WebEventAcctRcptListSelect(string ScheduleId)
        //{
        //    var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
        //    try
        //    {
        //        await objEngine.InitiateConnectionAsync();
        //        SqlParameter[] parameters = new SqlParameter[1];
        //        parameters[0] = new SqlParameter("@EventScheduleId", ScheduleId);
        //        var getObjData = await objEngine.ExecuteCommandAsync("[WebEventAcctRcptListSelect]", CommandType.StoredProcedure, parameters);
        //        var list = new List<EventRcptList>();
        //        //a1.Id, a1.ContactName, a1.ContactNo, a1.ContentId, a1.ChannelInd, a1.LangInd
        //        while (getObjData.Read())
        //        {
        //            if (!string.IsNullOrEmpty(Convert.ToString(getObjData["Id"])))
        //            {
        //                var item = new EventRcptList
        //                {
        //                    ChannelInd = ConvertInt(getObjData["ChannelInd"]),
        //                    ContactName = Convert.ToString(getObjData["ContactName"]),
        //                    ContactNo = Convert.ToString(getObjData["ContactNo"]),
        //                    ContentId = Convert.ToInt64(getObjData["ContentId"]),
        //                    Id = Convert.ToInt64(getObjData["Id"]),
        //                    LangInd = Convert.ToString(getObjData["LangInd"])
        //                };
        //                list.Add(item);
        //            }
        //        }
        //        return list;
        //    }
        //    finally
        //    {
        //        objEngine.CloseConnection();
        //    }
        //}


        #endregion
    }
}
using FleetOps.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Utilities.DAL;

namespace FleetSys.Models
{
    public class NotifSearchMaint : BaseClass
    {


        public async Task<List<LookupParameters>> WebNtfyEventSearch(LookupParameters _Params)
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            var list = new List<LookupParameters>();
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[6];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@EvtInd", string.IsNullOrEmpty(_Params.SeletedEventInd) ? (object)DBNull.Value : _Params.SeletedEventInd);
                Parameters[1] = String.IsNullOrEmpty(_Params.SelectedRefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", _Params.SelectedRefTo);
                Parameters[2] = String.IsNullOrEmpty(_Params.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", _Params.RefKey);
                Parameters[3] = String.IsNullOrEmpty(_Params.SelectedEventType) ? new SqlParameter("@EvtType", DBNull.Value) : new SqlParameter("@EvtType", _Params.SelectedEventType);
                Parameters[4] = String.IsNullOrEmpty(_Params.StartDate) ? new SqlParameter("@StartDate", DBNull.Value) : new SqlParameter("@StartDate", ConvertDatetimeDB(_Params.StartDate));
                Parameters[5] = String.IsNullOrEmpty(_Params.EndDate) ? new SqlParameter("@EndDate", DBNull.Value) : new SqlParameter("@EndDate", ConvertDatetimeDB(_Params.EndDate));
                var getObjData = await objEngine.ExecuteCommandAsync("WebNtfyEventSearch", CommandType.StoredProcedure, Parameters);
                while (getObjData.Read())
                {
                    var info = new LookupParameters
                    {
                        Id = Convert.ToString(getObjData["EvtId"]),
                        SeletedEventInd = Convert.ToString(getObjData["EvtTypeInd"]),
                        ShortDescp = Convert.ToString(getObjData["EvtShorDescp"]),
                        SelectedReason = Convert.ToString(getObjData["EvtReason"]),
                        SelectedRefTo = Convert.ToString(getObjData["Refto"]),
                        RefKey = Convert.ToString(getObjData["RefKey"]),
                        SelectedOwner = Convert.ToString(getObjData["OwnerType"]),
                        CreationDate = Convert.ToString(getObjData["CreationDate"]),
                        CompanyName = Convert.ToString(getObjData["CmpyName"]),
                        Channel = Convert.ToString(getObjData["Channel"])
                    };
                    list.Add(info);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }


        public async Task<List<LookupParameters>> WebEventSelect(string EventId)
        {
            var objEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objEngine.InitiateConnection();
            try
            {
                var list = new List<LookupParameters>();
                SqlParameter[] Parameters = new SqlParameter[1];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = String.IsNullOrEmpty(EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", EventId);
                var getObjData = await objEngine.ExecuteCommandAsync("WebEvtSelect", CommandType.StoredProcedure, Parameters);
                while (getObjData.Read())
                {
                    var info = new LookupParameters
                    {
                        Id = Convert.ToString(getObjData["EvtId"]),
                        EventScheduleId= Convert.ToString(getObjData["EventScheduleId"]),
                        ShortDescp = Convert.ToString(getObjData["ShortDescp"]),
                        type = Convert.ToString(getObjData["Type"]),
                        SelectedPriority = Convert.ToString(getObjData["Severity"]),
                        SelectedOwner = Convert.ToString(getObjData["OwnerType"]),
                        TypeDesc = Convert.ToString(getObjData["TypeDescp"]),
                        SelectedRefTo = Convert.ToString(getObjData["Refto"]),
                        RefKey = Convert.ToString(getObjData["Refkey"]),
                        CompanyName = Convert.ToString(getObjData["CmpyName"]),
                        DetailedDescp = Convert.ToString(getObjData["EvtDescp"]),
                        MaxOccur= Convert.ToString(getObjData["MaxOccur"]),
                        SelectedFrequency= Convert.ToString(getObjData["OccurPeriodType"]),
                        MinIntVal = Convert.ToString(getObjData["MinIntVal"]),
                        MaxIntVal = Convert.ToString(getObjData["MaxIntVal"]),
                        MinMoneyVal = Convert.ToString(getObjData["MinMoneyVal"]),
                        MaxMoneyVal = Convert.ToString(getObjData["MaxMoneyVal"]),
                        MinDateVal = Convert.ToString(getObjData["MinDateVal"]),
                        MaxDateVal = Convert.ToString(getObjData["MaxDateVal"]),
                        MinTimeVal = Convert.ToString(getObjData["MinTimeVal"]),
                        MaxTimeVal = Convert.ToString(getObjData["MaxTimeVal"]),
                        VarCharVal = Convert.ToString(getObjData["VarCharVal"]),
                        PeriodType = Convert.ToString(getObjData["PeriodType"]),
                        PeriodInterval = Convert.ToString(getObjData["PeriodInterval"]),
                        NotifyInd = ConvertInt(getObjData["ChannelInd"]),
                        LastUpdated = Convert.ToString(getObjData["LastUpdDate"]),
                        UpdatedBy = Convert.ToString(getObjData["UserId"]),
                        ParamInd = Convert.ToString(getObjData["ParamInd"]),
                        SentDate = DateConverter(getObjData["SentDate"])
                    };

                    String BitmapAmt = Convert.ToString(getObjData["ParamInd"]);
                    info.BitmapAmt = BitmapAmt;
                    list.Add(info);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }


        }
        public async Task<List<EventRcptList>> WebNtfyEventRcptListSelect(string ScheduleId)
        {
            var objEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objEngine.InitiateConnection();
                var list = new List<EventRcptList>();
                SqlParameter[] Parameters = new SqlParameter[1];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = String.IsNullOrEmpty(ScheduleId) ? new SqlParameter("@EventScheduleId", DBNull.Value) : new SqlParameter("@EventScheduleId", ScheduleId);
                var getObjData = await objEngine.ExecuteCommandAsync("[WebNtfyEventRcptListSelect]", CommandType.StoredProcedure, Parameters);
                while (getObjData.Read())
                {
                    //a1.Id, a1.ContactName, a1.ContactNo, a1.ContentId, a1.ChannelInd, a1.LangInd
                    var item = new EventRcptList
                    {
                        ChannelInd = ConvertInt(getObjData["ChannelInd"]),
                        ContactName = Convert.ToString(getObjData["ContactName"]),
                        ContactNo = Convert.ToString(getObjData["ContactNo"]),
                        LangInd = Convert.ToString(getObjData["LangInd"]),
                        ContentId = Convert.ToInt64(getObjData["ContentId"]),
                        Id = Convert.ToInt64(getObjData["Id"])
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
    }
}

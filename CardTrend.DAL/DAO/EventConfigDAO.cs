using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.EventConfiguration;
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
   public interface IEventConfigDAO
   {
       Task<List<WebNtfyEvtConfDetailDTO>> GetNtfyEvtConfListSelect(string userId);
       Task<int> WebNtfEvtConfDelete(string scheduleId);
       Task<int> WebNtfEvtConfRcptDelete(string schRcptId);
       Task<List<NtfyEventConfDTO>> WebNtfyEventConfSelect(string planId);
       Task<IssMessageDTO> SaveEventAcctConfMaint(NtfyEventConfDTO lookUpParameters);
       Task<IssMessageDTO> SaveWebNtfyEvtConfMaint(NtfyEventConfDTO lookUpParameters);
       Task<List<EventRcptDTO>> WebNtfyEventRcptListSelect(string planId);
       Task<string> WebGetRefCmpyName(string selectedRefTo, string refKey);
       Task<List<NtfyEventConfDTO>> WebEventAcctConfListSelect(string refTo, string refKey);
       Task<List<NtfyEventConfDTO>> WebEventAcctConfSelect(string eventTypeId, string eventScheduleId, string acctNo);
       Task<List<EventRcptDTO>> WebEventAcctRcptListSelect(string eventScheduleId);
   }
   public class EventConfigDAO :DAOBase, IEventConfigDAO
    {
        private readonly string _connectionString = string.Empty;
        public EventConfigDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<List<WebNtfyEvtConfDetailDTO>> GetNtfyEvtConfListSelect(string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { userId };
                var paramNameList = new[]
                                   {
                                        "@UserId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<WebNtfyEvtConfDetailDTO>
                     (BuildSqlCommand("WebNtfyEvtConfListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return result;

            }
        }
        public async Task<int> WebNtfEvtConfDelete(string scheduleId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { scheduleId };
                var paramNameList = new[]
                                   {
                                        "@ScheduleId"
                                   };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebNtfEvtConfDelete", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> WebNtfEvtConfRcptDelete (string schRcptId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                //IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { schRcptId };
                var paramNameList = new[]
                                   {
                                        "@SchRcptId"
                                   };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebNtfEvtConfRcptDelete", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<List<NtfyEventConfDTO>> WebNtfyEventConfSelect(string planId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { planId };
                var paramNameList = new[]
                                   {
                                        "@EventScheduleId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<NtfyEventConfDTO>(BuildSqlCommand("WebNtfyEventConfSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<IssMessageDTO> SaveEventAcctConfMaint(NtfyEventConfDTO lookUpParameters)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
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
                if (lookUpParameters.ProductItems != null)
                {
                    foreach (var item in lookUpParameters.ProductItems)
                    {
                        DataRow dr = dtNtfyEventTbl.NewRow();
                        dr["EvtNtfyDetailId"] = item.EvtPlanDetailId;
                        dr["MinIntVal"] = NumberExtensions.ConvertLongToDb(item.MinIntVal);
                        dr["MaxIntVal"] = NumberExtensions.ConvertLongToDb(item.MaxIntVal);
                        dr["MinMoneyVal"] = NumberExtensions.ConvertDecimalToDb(item.MinMoneyVal);
                        dr["MaxMoneyVal"] = NumberExtensions.ConvertDecimalToDb(item.MaxMoneyVal);
                        dr["MinDateVal"] = NumberExtensions.ConvertDatetimeDB(item.MinDateVal);
                        dr["MaxDateVal"] = NumberExtensions.ConvertDatetimeDB(item.MaxDateVal);
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

                if (lookUpParameters.eventRcpts != null)
                {
                    foreach (var item in lookUpParameters.eventRcpts)
                    {
                        DataRow dr = dtNtfyEventRcptTbl.NewRow();
                        dr["EventRecipientId"] = string.IsNullOrEmpty(item.Id.ToString()) ? (object)DBNull.Value : item.Id;
                        dr["EventScheduleId"] = string.IsNullOrEmpty(lookUpParameters.EventScheduleId.ToString()) ? (object)DBNull.Value : lookUpParameters.EventScheduleId;
                        dr["ContactName"] = string.IsNullOrEmpty(item.ContactName) ? (object)DBNull.Value : item.ContactName;
                        dr["ContactNo"] = string.IsNullOrEmpty(item.ContactNo) ? (object)DBNull.Value : item.ContactNo;
                        dr["ChannelInd"] = item.ChannelInd;
                        dr["LangInd"] = string.IsNullOrEmpty(item.LangInd) ? (object)DBNull.Value : item.LangInd;
                        dtNtfyEventRcptTbl.Rows.Add(dr);
                    }
                }

                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@EventSchId", SqlDbType.BigInt) {SqlValue = (object)lookUpParameters.EventScheduleId ?? DBNull.Value},
                    new SqlParameter("@NewEventSchId", SqlDbType.BigInt) {Direction = ParameterDirection.Output},
                    new SqlParameter("@EventTypeID", SqlDbType.BigInt) {SqlValue = (object)lookUpParameters.EventTypeId?? DBNull.Value},
                    new SqlParameter("@EventPlanId", SqlDbType.BigInt) {SqlValue = (object)lookUpParameters.EventPlanId?? DBNull.Value},
                    new SqlParameter("@ShortDescp", SqlDbType.NVarChar) {SqlValue = (object)lookUpParameters.ShortDescp?? DBNull.Value},
                    new SqlParameter("@Scope", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.OwnerType?? DBNull.Value},
                    new SqlParameter("@Sts", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.Sts ?? DBNull.Value},
                    new SqlParameter("@ParamInd", SqlDbType.BigInt) {SqlValue = string.Empty},
                    new SqlParameter("@RefTo", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.Refto?? DBNull.Value},
                    new SqlParameter("@RefKey",SqlDbType.VarChar){SqlValue = (object)lookUpParameters.Refkey?? DBNull.Value} ,

                    new SqlParameter("@CntEvtOccur", SqlDbType.Int) {SqlValue = (object)lookUpParameters.MaxOccur?? DBNull.Value},
                    new SqlParameter("@EvtOccurType", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.Frequency?? DBNull.Value},
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.UserId?? DBNull.Value},
                    new SqlParameter("@ChannelInd", SqlDbType.BigInt) {SqlValue = (object)(NumberExtensions.ConvertIntToDb(lookUpParameters.EvtTypeChannelInd))?? DBNull.Value},
                    new SqlParameter("@DefaultInd", SqlDbType.Char) {SqlValue = (object)lookUpParameters.DefaultInd?? DBNull.Value},
                    new SqlParameter("@NtfyEventTbl", SqlDbType.Structured) {SqlValue = dtNtfyEventTbl,TypeName ="NtfyEventTbl"},
                    new SqlParameter("@NtfyEventRcptTbl",SqlDbType.Structured) {SqlValue = dtNtfyEventRcptTbl,TypeName ="NtfyEventRcptTbl"},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}                   
                };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebEventAcctConfMaint @IssNo,@EventSchId,@NewEventSchId OUT,@EventTypeID,@EventPlanId," +
                                          "@ShortDescp,@Scope,@Sts,@ParamInd,@RefTo,@RefKey,@CntEvtOccur,@EvtOccurType,@UserId,@ChannelInd,@DefaultInd,@NtfyEventTbl,@NtfyEventRcptTbl", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var newEventSchId = parameters.Where(x => x.ParameterName == "@NewEventSchId").FirstOrDefault().Value;
                issMessage.Flag = Convert.ToInt32(resultCode);
                issMessage.paraOut.NewEventSchId = Convert.ToInt64(newEventSchId);
                return issMessage;
            
            }
        }
        public async Task<IssMessageDTO> SaveWebNtfyEvtConfMaint(NtfyEventConfDTO lookUpParameters)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
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
                if (lookUpParameters.ProductItems != null)
                {
                    foreach (var item in lookUpParameters.ProductItems)
                    {
                        DataRow dr = dtNtfyEventTbl.NewRow();
                        dr["EvtNtfyDetailId"] = item.EvtPlanDetailId;
                        dr["MinIntVal"] = NumberExtensions.ConvertLongToDb(item.MinIntVal);
                        dr["MaxIntVal"] = NumberExtensions.ConvertLongToDb(item.MaxIntVal);
                        dr["MinMoneyVal"] = NumberExtensions.ConvertDecimalToDb(item.MinMoneyVal);
                        dr["MaxMoneyVal"] = NumberExtensions.ConvertDecimalToDb(item.MaxMoneyVal);
                        dr["MinDateVal"] = NumberExtensions.ConvertDatetimeDB(item.MinDateVal);
                        dr["MaxDateVal"] = NumberExtensions.ConvertDatetimeDB(item.MaxDateVal);
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

                if (lookUpParameters.eventRcpts != null)
                {
                    foreach (var item in lookUpParameters.eventRcpts)
                    {
                        DataRow dr = dtNtfyEventRcptTbl.NewRow();
                        dr["EventRecipientId"] = string.IsNullOrEmpty(item.Id.ToString()) ? (object)DBNull.Value : item.Id;
                        dr["EventScheduleId"] = string.IsNullOrEmpty(lookUpParameters.EventScheduleId.ToString()) ? (object)DBNull.Value : lookUpParameters.EventScheduleId;
                        dr["ContactName"] = string.IsNullOrEmpty(item.ContactName) ? (object)DBNull.Value : item.ContactName;
                        dr["ContactNo"] = string.IsNullOrEmpty(item.ContactNo) ? (object)DBNull.Value : item.ContactNo;
                        dr["ChannelInd"] = item.ChannelInd;
                        dr["LangInd"] = string.IsNullOrEmpty(item.LangInd) ? (object)DBNull.Value : item.LangInd;
                        dtNtfyEventRcptTbl.Rows.Add(dr);
                    }
                }

                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@EventSchId", SqlDbType.BigInt) {SqlValue = (object)lookUpParameters.EventScheduleId ?? DBNull.Value},
                    new SqlParameter("@NewEventSchId", SqlDbType.BigInt) {Direction = ParameterDirection.Output},
                    new SqlParameter("@EventTypeID", SqlDbType.BigInt) {SqlValue = (object)lookUpParameters.EventTypeId?? DBNull.Value},
                    new SqlParameter("@EventPlanId", SqlDbType.BigInt) {SqlValue = (object)lookUpParameters.EventPlanId?? DBNull.Value},
                    new SqlParameter("@ShortDescp", SqlDbType.NVarChar) {SqlValue = (object)lookUpParameters.ShortDescp?? DBNull.Value},
                    new SqlParameter("@Scope", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.OwnerType?? DBNull.Value},
                    new SqlParameter("@Sts", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.Sts ?? DBNull.Value},
                    new SqlParameter("@ParamInd", SqlDbType.BigInt) {SqlValue = 0},
                    new SqlParameter("@RefTo", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.Refto?? DBNull.Value},
                    new SqlParameter("@RefKey",SqlDbType.VarChar){SqlValue = (object)lookUpParameters.Refkey?? DBNull.Value} ,

                    new SqlParameter("@CntEvtOccur", SqlDbType.Int) {SqlValue = (object)lookUpParameters.MaxOccur?? DBNull.Value},
                    new SqlParameter("@EvtOccurType", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.Frequency?? DBNull.Value},
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = (object)lookUpParameters.UserId?? DBNull.Value},
                    new SqlParameter("@ChannelInd", SqlDbType.BigInt) {SqlValue = (object)lookUpParameters.EvtTypeChannelInd?? DBNull.Value},
                    new SqlParameter("@DefaultInd", SqlDbType.Char) {SqlValue = (object)lookUpParameters.DefaultInd?? DBNull.Value},
                    new SqlParameter("@NtfyEventTbl", SqlDbType.Structured) {SqlValue = dtNtfyEventTbl,TypeName ="NtfyEventTbl"},
                    new SqlParameter("@NtfyEventRcptTbl",SqlDbType.Structured) {SqlValue = dtNtfyEventRcptTbl,TypeName ="NtfyEventRcptTbl"},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}                   
                };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebNtfyEvtConfMaint @IssNo,@EventSchId,@NewEventSchId OUT,@EventTypeID,@EventPlanId," +
                                      "@ShortDescp,@Scope,@Sts,@ParamInd,@RefTo,@RefKey,@CntEvtOccur,@EvtOccurType,@UserId,@ChannelInd,@DefaultInd,@NtfyEventTbl,@NtfyEventRcptTbl", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                var newEventSchId = parameters.Where(x => x.ParameterName == "@NewEventSchId").FirstOrDefault().Value;              
                issMessage.Flag = Convert.ToInt32(resultCode);
                issMessage.paraOut.NewEventSchId = Convert.ToInt64(newEventSchId);
                return issMessage;
            }
        }
        public async Task<List<EventRcptDTO>> WebNtfyEventRcptListSelect(string planId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Convert.ToInt64(planId) };
                var paramNameList = new[]
                                   {
                                        "@EventScheduleId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<EventRcptDTO>
                     (BuildSqlCommand("WebNtfyEventRcptListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<string> WebGetRefCmpyName(string selectedRefTo, string refKey)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { selectedRefTo, refKey };
                var paramNameList = new[]
                                   {
                                        "@RefTo",
                                        "@RefKey"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var companyName = await cardtrendentities.Database.SqlQuery<string>
                    (BuildSqlCommand("WebGetRefCmpyName", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return companyName;
            }
        }
        public async Task<List<NtfyEventConfDTO>> WebEventAcctConfListSelect(string refTo, string refKey)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { refTo, refKey };
                var paramNameList = new[]
                                   {
                                        "@RefTo",
                                        "@RefKey"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<NtfyEventConfDTO>
                             (BuildSqlCommand("WebEventAcctConfListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<NtfyEventConfDTO>> WebEventAcctConfSelect(string eventTypeId, string eventScheduleId, string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { eventTypeId, eventScheduleId, acctNo };
                var paramNameList = new[]
                                   {
                                        "@EventTypeId",
                                        "@EventScheduleId",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<NtfyEventConfDTO>
                             (BuildSqlCommand("WebEventAcctConfSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<EventRcptDTO>> WebEventAcctRcptListSelect(string eventScheduleId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { eventScheduleId };
                var paramNameList = new[]
                                   {
                                        "@EventScheduleId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<EventRcptDTO>
                             (BuildSqlCommand("WebEventAcctRcptListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
    }
}


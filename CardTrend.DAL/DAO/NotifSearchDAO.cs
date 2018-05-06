using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto.EventConfiguration;
using CardTrend.Domain.Dto.NotifSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface INotifSearchDAO
    {
        Task<List<NtfyEventSearchDTO>> WebNtfyEventSearch(string evtInd, string eventType, string refTo, string refKey, string startDate, string endDate);
        Task<List<NtfyEventConfSearchDTO>> WebEventSelect(string eventId);
        Task<List<EventRcptDTO>> WebNtfyEventRcptListSelect(string scheduleId);
    }
    public class NotifSearchDAO : DAOBase, INotifSearchDAO
    {
        private readonly string _connectionString = string.Empty;
        public NotifSearchDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<List<NtfyEventSearchDTO>> WebNtfyEventSearch(string evtInd,string eventType,string refTo,string refKey,string startDate,string endDate)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { evtInd, eventType, refTo, refKey, startDate, endDate };
                var paramNameList = new[]
                                   {
                                        "@EvtInd",
                                        "@EvtType",
                                        "@RefTo",
                                        "@RefKey",
                                        "@StartDate",
                                        "@EndDate"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<NtfyEventSearchDTO>
                             (BuildSqlCommand("WebNtfyEventSearch", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<NtfyEventConfSearchDTO>> WebEventSelect(string eventId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { eventId };
                var paramNameList = new[]
                                   {
                                        "@EvtInd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<NtfyEventConfSearchDTO>
                             (BuildSqlCommand("WebEvtSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<EventRcptDTO>> WebNtfyEventRcptListSelect(string scheduleId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { scheduleId };
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
    }
}

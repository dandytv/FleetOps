using CardTrend.DAL.Contexts;
using CardTrend.Domain.WebDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAOWEB
{
    public interface ISecurityOpDAO
    {
        List<UserAccessLevelDTO> WebGetUserAccessModuleList(string userId, string level);
        Task<List<UserAccessLevelDetailDTO>> WebGetUserAccessPgNCtrlList(string accessInd, string userId, List<string> moduleList, List<string> pageList, string ctrlId);
    }
    public class SecurityOpDAO :DAOBase, ISecurityOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public SecurityOpDAO(string connString)
        {
            _connectionString = connString;
        }
        public List<UserAccessLevelDTO> WebGetUserAccessModuleList(string userId,string level)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), userId, level };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@UserId",
                                        "@Lvl"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result =  cardtrendentities.Database.SqlQuery<UserAccessLevelDTO>(BuildSqlCommand("WebUserAccessLevelListSelect", paramCollection), paramCollection.ToArray()).ToList();
                return result;
            }
        }

        public async Task<List<UserAccessLevelDetailDTO>> WebGetUserAccessPgNCtrlList(string accessInd, string userId, List<string> moduleList, List<string> pageList, string ctrlId)
        {
            
                IList<UserAccessLevelDetailDTO> userAccesses = new List<UserAccessLevelDetailDTO>();
                if (pageList == null)
                {
                    if (moduleList.Count() > 0)
                    {
                        foreach(var x in moduleList)
                        {
                            var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accessInd, userId ,x,null,ctrlId};
                            var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AccessInd",
                                        "@UserId",
                                        "@ModuleId",
                                        "@PageId",
                                        "@CtrlId"
                                   };
                            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
                            {
                            var paramCollection = BuildParameterList(parameters, paramNameList);
                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            var result = await cardtrendentities.Database.SqlQuery<UserAccessLevelDetailDTO>(BuildSqlCommand("WebUserAccessLevelSelectBK", paramCollection), paramCollection.ToArray()).ToListAsync();
                            watch.Stop();
                            var elapsedMs = watch.ElapsedMilliseconds;
                            if (result.Count() > 0)
                                for (int i = 0; i < result.Count();i ++)
                                    userAccesses.Add(result[i]);
                            }
                        }
                    }
                }
                else
                {
                    for(int i = 0;i< pageList.Count; i++)
                    {
                        var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accessInd, userId, moduleList[i],pageList[i], ctrlId };
                        var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AccessInd",
                                        "@UserId",
                                        "@ModuleId",
                                        "@PageId",
                                        "@CtrlId"
                                   };
                        using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
                        {
                            var paramCollection = BuildParameterList(parameters, paramNameList);
                            var result = await cardtrendentities.Database.SqlQuery<UserAccessLevelDetailDTO>(BuildSqlCommand("WebUserAccessLevelSelectBK", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                            if (result != null)
                                userAccesses.Add(result);
                        }
                    }
                }

                return userAccesses.ToList();
            }
      
        

    }
}

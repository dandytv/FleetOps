using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.ControlList;
using CardTrend.Domain.WebDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.DAL.DAOWEB
{
    public interface IUserAccessDAO
    {
        UserAccessDTO GetUserAccessDetail(string accessInd, string userId);
        Task<IList<GetUserTitleDto>> GetUserTitles();
        Task<IList<SelectListItem>> WebGetMap();
        IList<UserAccessListDTO> GetUserAccessList(string accessInd);
        Task<IList<SelectListItem>> GetUserAccessListSelect(string AccessInd = "I");
        Task<IList<SelectListItem>> GetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true);
        int SaveUserLogin(string username, string password);
        Task<int> SaveUserAccess(UserAccessDTO userAccess, bool isUpdate = false);
        Task<int> SaveWebUserAccessMapping(UserAccessDTO userAccess);
        Task<int> SaveWebUserAccessLevel(List<WebModuleDTO> ModuleList, List<WebPageDTO> PageList, List<WebControlDTO> CtrlList, List<WebPageSectionDTO> SectionList, string UserId);
    }
    public class UserAccessDAO : DAOBase, IUserAccessDAO
    {
        private readonly string _connectionString = string.Empty;
        public UserAccessDAO(string connString)
        {
            _connectionString = connString;
        }
        public UserAccessDTO GetUserAccessDetail(string accessInd, string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accessInd, userId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AccessInd",
                                        "@UserId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var result = cardtrendentities.Database.SqlQuery<UserAccessDTO>(BuildSqlCommand("WebUserAccessSelect", paramCollection), paramCollection.ToArray()).FirstOrDefault();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                return result;
            }
        }
        public async Task<IList<SelectListItem>> GetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refType, RefNo, RefInd, RefId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefType",
                                        "@RefNo",
                                        "@RefInd",
                                        "@RefId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<RefLibDTO>
                    (BuildSqlCommand("WebGetRefLib", paramCollection), paramCollection.ToArray())
                    .ToListAsync();


                var list = new List<SelectListItem>();
                if (PrependNull)
                {
                    list.Add(new SelectListItem { Text = "", Value = "" });
                }

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.RefCd,
                            Selected = refLib.RefCd == "1" ? true : false
                        });
                    }
                }

                return list;
            }
        }
        public async Task<IList<GetUserTitleDto>> GetUserTitles()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var results = await cardtrendentities.Database.SqlQuery<GetUserTitleDto>("SPOGetUserTitle").ToListAsync();
                return results;
            }
        }
        public async Task<IList<SelectListItem>> GetUserAccessListSelect(string AccessInd = "I")
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), AccessInd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AccessInd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<UserAccessListDTO>
                    (BuildSqlCommand("WebUserAccessListSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.UserId,
                            Value = refLib.UserId
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetMap()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var results = await cardtrendentities.Database.SqlQuery<UserAccessDTO>("WebGetMapId").ToListAsync();
                var list = new List<SelectListItem>();
                if (results.Count() > 0)
                {
                    foreach (var eventType in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = eventType.UserId,
                            Value = eventType.UserId
                        });
                    }
                }
                return list;
            }
        }
        public IList<UserAccessListDTO> GetUserAccessList(string accessInd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), accessInd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AccessInd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = cardtrendentities.Database.SqlQuery<UserAccessListDTO>
                    (BuildSqlCommand("WebUserAccessListSelect", paramCollection), paramCollection.ToArray())
                    .ToList();

                return results;
            }
        }
        public int SaveUserLogin(string username, string password)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { username, password };
                var paramNameList = new[]
                                   {
                                        "@UserId",
	                                    "@Pw"
                                   };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = cardtrendentities.Database.ExecuteSqlCommand
                    (BuildSqlCommandWithRrn("WebUserSignIn", paramCollection), paramCollection.ToArray());

                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveUserAccess(UserAccessDTO userAccess, bool isUpdate = false)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), userAccess.AccessInd, userAccess.Sts, userAccess.UserId, userAccess.Name, userAccess.ContactNo, userAccess.EmailAddr, userAccess.Title, userAccess.DeptId, userAccess.PrivilegeCd, userAccess.CreateBy, userAccess.Password, userAccess.ChangePassInd, isUpdate ? "U" : "N" };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@AccessInd",
                                        "@Sts",
                                        "@UserId",
                                        "@Name",
                                        "@ContactNo",
                                        "@EmailAddr",
                                        "@Title",
                                        "@DeptId",
                                        "@PrivilegeCd",
                                        "@CreatedBy",
                                        "@Pw",
                                        "@ChangePassInd",
                                        "@Flag"
                                   };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebUserAccessMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveWebUserAccessMapping(UserAccessDTO userAccess)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), userAccess.UserId, userAccess.MapUserId, userAccess.Name, userAccess.ContactNo, userAccess.DeptId, userAccess.AccessInd, userAccess.EmailAddr, userAccess.Password};
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@UserId",
                                        "@MapUserId",
                                        "@ContactName",
                                        "@ContactNo",
                                        "@DeptId",
                                        "@AccessInd",
                                        "@EmailAddr",
                                        "@PwBlock"
                                   };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync(BuildSqlCommandWithRrn("WebUserAccessMapping", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> SaveWebUserAccessLevel(List<WebModuleDTO> ModuleList, List<WebPageDTO> PageList, List<WebControlDTO> CtrlList, List<WebPageSectionDTO> SectionList, string UserId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
           {
               DataTable dataTable = new DataTable();
               dataTable.Columns.Add("ModuleID");
               dataTable.Columns.Add("PageID");
               dataTable.Columns.Add("SectionID");
               dataTable.Columns.Add("ControlID");
               dataTable.Columns.Add("Status");
               DataRow dr = dataTable.NewRow();
               if (ModuleList != null)
               {
                   for (int i = 0; i < ModuleList.Count; i++)
                   {
                       dr["ModuleID"] = ModuleList[i].ModuleId;
                       dr["PageID"] = ModuleList[i].PageId;//DBNull.Value;
                       dr["ControlID"] = ModuleList[i].CtrlId;//DBNull.Value;
                       dr["Status"] = ModuleList[i].Sts;
                       dataTable.Rows.Add(dr);
                       dr = dataTable.NewRow();
                   }
               }

               if (PageList != null)
               {
                   for (int i = 0; i < PageList.Count; i++)
                   {
                       //    dr["ID"] = id;
                       dr["ModuleID"] = PageList[i].ModuleId;
                       dr["PageID"] = PageList[i].PageId;
                       dr["ControlID"] = PageList[i].CtrlId;
                       dr["Status"] = PageList[i].Sts;
                       dataTable.Rows.Add(dr);
                       dr = dataTable.NewRow();
                   }
               }

               if (SectionList != null)
               {
                   for (int i = 0; i < SectionList.Count; i++)
                   {
                       dr["ModuleID"] = SectionList[i].ModuleId;//DBNull.Value;
                       dr["PageID"] = SectionList[i].PageId;//DBNull.Value;
                       dr["ControlID"] = SectionList[i].CtrlId;//DBNull.Value;
                       dr["SectionID"] = SectionList[i].SectionId;
                       dr["Status"] = SectionList[i].SectionStatus;
                       dataTable.Rows.Add(dr);
                       dr = dataTable.NewRow();
                   }
               }

               if (CtrlList != null)
               {
                   for (int i = 0; i < CtrlList.Count; i++)
                   {
                       //dr["ID"] = id;
                       dr["ModuleID"] = CtrlList[i].ModuleId;//DBNull.Value;
                       dr["PageID"] = CtrlList[i].PageId;
                       dr["ControlID"] = CtrlList[i].CtrlId;
                       dr["SectionID"] = CtrlList[i].SectionId;
                       dr["Status"] = CtrlList[i].Sts;
                       dataTable.Rows.Add(dr);
                       dr = dataTable.NewRow();
                       //id = id + 1;
                   }
               }

               var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@UserId", SqlDbType.VarChar) {SqlValue = UserId},
                    new SqlParameter("@Tbl",SqlDbType.Structured) {SqlValue = dataTable,TypeName ="UserAccessLvlFleet"},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

               await cardtrendentities.Database.ExecuteSqlCommandAsync(TransactionalBehavior.DoNotEnsureTransaction, "exec @RETURN_VALUE = WebUserAccessLevelMaint @IssNo,@UserId,@Tbl", parameters);
               var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
               return Convert.ToInt32(resultCode);
           }
        }
    }
}

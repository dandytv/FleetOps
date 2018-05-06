using FleetOps.App_Start;
using Utilities.DAL;
using FleetOps.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FleetSys.Controllers
{
    [Authorize]
    public class RepositoryController : BaseClass
    {
        [CompressFilter]
        public async Task<ActionResult> _Querymeta(string name_startsWith, bool AppendWildCard = true)
        {
            if (AppendWildCard == true)
            {
                var prefix = name_startsWith.Substring(0, 3);
                var searchobject = name_startsWith.Substring(3, (name_startsWith.Length - 3)) + "%";
                var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

                try
                {
                    await objDataEngine.InitiateConnectionAsync();
                    SqlParameter[] Parameters = new SqlParameter[3];
                    SqlCommand cmd = new SqlCommand();
                    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                    Parameters[1] = new SqlParameter("@Val", searchobject);
                    Parameters[2] = new SqlParameter("@Obj", prefix);
                    var Reader = await objDataEngine.ExecuteCommandAsync("WebGetObject", CommandType.StoredProcedure, Parameters);

                    var result = new List<SearchResult1>();

                    while (Reader.Read())
                    {
                        result.Add(new SearchResult1
                        {
                            Object = Convert.ToString(Reader["Obj"]),
                            MatchedValue = Convert.ToString(Reader["MatchedVal"]),
                            Descp = Convert.ToString(Reader["Descp"]),
                            Link = Convert.ToString(Reader["Link"]),
                            Dest = Convert.ToString(Reader["Dest"]),
                        });

                    }
                    return Json(new { theResult = result }, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    objDataEngine.CloseConnection();
                }
            }
            else
            {
                var prefix = name_startsWith.Substring(0, 3);
                var searchobject = name_startsWith.Substring(3, (name_startsWith.Length - 3));
                var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

                try
                {
                    await objDataEngine.InitiateConnectionAsync();
                    SqlParameter[] Parameters = new SqlParameter[3];
                    SqlCommand cmd = new SqlCommand();
                    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                    Parameters[1] = new SqlParameter("@Val", searchobject);
                    Parameters[2] = new SqlParameter("@Obj", prefix);
                    var Reader = await objDataEngine.ExecuteCommandAsync("WebGetObject", CommandType.StoredProcedure, Parameters);

                    var result = new List<SearchResult1>();

                    while (Reader.Read())
                    {
                        result.Add(new SearchResult1
                        {
                            Object = Convert.ToString(Reader["Obj"]),
                            MatchedValue = Convert.ToString(Reader["MatchedVal"]),
                            Descp = Convert.ToString(Reader["Descp"]),
                            Link = Convert.ToString(Reader["Link"]),
                            Dest = Convert.ToString(Reader["Dest"]),
                        });
                    }
                    return Json(new { theResult = result }, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    objDataEngine.CloseConnection();
                }
            }
        }
        public HttpStatusCodeResult Error403()
        {
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Bad Request");
        }
        public async Task<ActionResult> _QueryDetailInfo(string term)
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}
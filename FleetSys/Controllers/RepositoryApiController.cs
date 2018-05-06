using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using FleetSys.Models;
using Utilities.DAL;
using System.Data.SqlClient;
using System.Data;
using FleetOps.Models;

namespace FleetSys.Controllers
{
    [Authorize]
    public class RepositoryApiController : ApiController
    {
        // GET: api/RepositoryApi

        [HttpGet]
        public async Task<HttpResponseMessage> SearchModules(string name_startsWith)
        {
            var prefix = name_startsWith.Substring(0, 3);
            var searchobject = name_startsWith.Substring(3, (name_startsWith.Length - 3)) + "%";



            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", BaseClass.GetIssNo);
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
            //list.Add(new SearchResult
            //{
            //    Object = "zzzzzz",
            //    MatchedValue = "Just a sample",
            //    Descp = "Some description goes in here",
            //    Link = "home/index",
            //    Dest = "some dest",
            //});
            return Request.CreateResponse(HttpStatusCode.OK, new { theResult = result });
        }

        // GET: api/RepositoryApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RepositoryApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RepositoryApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RepositoryApi/5
        public void Delete(int id)
        {
        }
    }
}

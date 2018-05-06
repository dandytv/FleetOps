using CCMS.ModelSector;
using FleetOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
namespace FleetSys.Controllers
{
    [Authorize]
    public class ApplicationsApiController : ApiController
    {
        // GET: api/ApplicationsApi
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }
        [HttpGet]




        // GET: api/ApplicationsApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApplicationsApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApplicationsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApplicationsApi/5
        public void Delete(int id)
        {
        }



    }
}

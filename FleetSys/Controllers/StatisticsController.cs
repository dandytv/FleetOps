using FleetOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FleetSys.Models;
using System.Threading.Tasks;
namespace FleetSys.Controllers
{
    public class StatisticsController : BaseClass
    {
        AccountOps _AcctOps = new AccountOps();
        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetFinancialInfo(int AccountNo)
        {
            var Maint =await _AcctOps.FtFinancialInfoForm(AccountNo);
            return Json(Maint, JsonRequestBehavior.AllowGet);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Utilities.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using CCMS.ModelSector;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class TerminalInventoryOps : BaseClass
    {
        public async Task<List<TerminalInventory>> TermInventorySelect( TerminalInventory _TerminalInventory)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService,DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@TermId", String.IsNullOrEmpty(_TerminalInventory.TerminalId) ? "" : _TerminalInventory.TerminalId);
            Parameters[1] = new SqlParameter("@RefCd", string.IsNullOrEmpty(_TerminalInventory.SelectedTerminalType) ? "" : _TerminalInventory.SelectedTerminalType);

            var execResult = objDataEngine.ExecuteCommand("WebTermInventorySelect", CommandType.StoredProcedure, Parameters);
            var _TermInventory = new List<TerminalInventory>();

            while (execResult.Read())
            {
                _TermInventory.Add(new TerminalInventory
                {
                    TerminalId = Convert.ToString(execResult["termid"]),
                    TerminalType = await BaseClass.WebGetRefLib("TermSts"),
                    SelectedTerminalType = Convert.ToString(execResult["TermType"]),
                    Status = await BaseClass.WebGetRefLib("TermSts"),
                    SelectedStatus = Convert.ToString(execResult["sts"]),
                    UserId = Convert.ToString(execResult["UserId"]),
                    Description = Convert.ToString(execResult["Description"]),
                    Printer = Convert.ToString(execResult["PrinterId"]),
                    PinPad = Convert.ToString(execResult["PinPad"]),
                    CreationDate = DateConverter(execResult["creationdate"])
                });

            }
            objDataEngine.CloseConnection();
            return  _TermInventory;
        }
    }
}
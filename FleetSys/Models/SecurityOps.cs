using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelSector;
using CCMS.ModelSector;
using System.Collections;
using System.Configuration;
using Utilities.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using System.IO;
using System.Threading.Tasks;

namespace ModelSector
{
    public class SecurityOps : BaseClass
    {

        public List<SecurityViewModel> WebGetUserAccessModuleList(string UserId)
        {
            //Level code = > 0=Module, 1=Page & 2=PageControl
            // Status Code => 0=Invisible, 1=Editable, 2= ReadOnly

            var objDataEngine = new FleetDataEngine(AccessMode.Admin, DBType.Web);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(UserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", UserId);
                Parameters[2] = new SqlParameter("@Lvl", "0");

                var execResult = objDataEngine.ExecuteCommand("WebUserAccessLevelListSelect", CommandType.StoredProcedure, Parameters);
                var _SecurityViewModel = new List<SecurityViewModel>();

                //get WebModule list

                while (execResult.Read())
                {
                    _SecurityViewModel.Add(new SecurityViewModel
                    {
                        _WebModule = new WebModule
                        {
                            Level = ConvertInt(execResult["Lvl"]),
                            ModuleId = Convert.ToString(execResult["ModuleId"]),
                            ShortDescp = Convert.ToString(execResult["ShortDescp"]),
                            Descp = Convert.ToString(execResult["Descp"]),
                            Sts = ConvertInt(execResult["Sts"])
                        }
                    });
                }
                return _SecurityViewModel;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }


        public List<SecurityViewModel> WebGetUserAccessPgNCtrlList(string AccessInd, string UserId, List<string> ModuleList, List<string> PageList, string CtrlId)
        {
            // Status Code => 0=Invisible, 1=Editable, 2= ReadOnly

            FleetDataEngine objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
            try
            {
                var _SecurityViewModel = new List<SecurityViewModel>();

                SqlParameter[] Parameters = new SqlParameter[6];

                if (PageList == null)
                {
                    //get WebPage list
                    if (ModuleList != null)
                    {
                        foreach (var x in ModuleList)
                        {
                            objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
                            objDataEngine.InitiateConnection();
                            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                            Parameters[1] = String.IsNullOrEmpty(AccessInd) ? new SqlParameter("@AccessInd", DBNull.Value) : new SqlParameter("@AccessInd", AccessInd);
                            Parameters[2] = String.IsNullOrEmpty(UserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", UserId);
                            Parameters[3] = String.IsNullOrEmpty(x) ? new SqlParameter("@ModuleId", DBNull.Value) : new SqlParameter("@ModuleId", x);
                            Parameters[4] = new SqlParameter("@PageId", DBNull.Value);
                            Parameters[5] = String.IsNullOrEmpty(CtrlId) ? new SqlParameter("@CtrlId", DBNull.Value) : new SqlParameter("@CtrlId", CtrlId);
                           
                            var execResult = objDataEngine.ExecuteCommand("WebUserAccessLevelSelect", CommandType.StoredProcedure, Parameters);

                            while (execResult.Read())
                            {
                                _SecurityViewModel.Add(new SecurityViewModel
                                {
                                    _WebPage = new WebPage
                                    {
                                        ModuleId = Convert.ToString(execResult["Module Id"]),
                                        PageId = Convert.ToString(execResult["Page Id"]),
                                        Descp = Convert.ToString(execResult["Page Description"]),
                                        Sts = ConvertInt(execResult["Sts"]),
                                        URL = Convert.ToString(execResult["Url"]),
                                        Level = Convert.ToInt32(execResult["Lvl"])
                                    }
                                });
                            };
                            objDataEngine.CloseConnection();
                        }
                    }
                }


                else
                {
                    //get WebPageControl list
                    for (int i = 0; i < PageList.Count; i++)
                    {
                        objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
                        objDataEngine.InitiateConnection();
                        Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                        Parameters[1] = String.IsNullOrEmpty(AccessInd) ? new SqlParameter("@AccessInd", DBNull.Value) : new SqlParameter("@AccessInd", AccessInd);
                        Parameters[2] = String.IsNullOrEmpty(UserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", UserId);
                        Parameters[3] = String.IsNullOrEmpty(ModuleList[i]) ? new SqlParameter("@ModuleId", DBNull.Value) : new SqlParameter("@ModuleId", ModuleList[i]);
                        Parameters[4] = String.IsNullOrEmpty(PageList[i]) ? new SqlParameter("@PageId", DBNull.Value) : new SqlParameter("@PageId", PageList[i]);
                        Parameters[5] = String.IsNullOrEmpty(CtrlId) ? new SqlParameter("@CtrlId", DBNull.Value) : new SqlParameter("@CtrlId", CtrlId);
                        var execResult = objDataEngine.ExecuteCommand("WebUserAccessLevelSelect", CommandType.StoredProcedure, Parameters);

                        while (execResult.Read())
                        {
                            _SecurityViewModel.Add(new SecurityViewModel
                            {
                                _WebPageControl = new WebPageSection
                                {
                                    PageId = Convert.ToString(execResult["Page Id"]),
                                    CtrlId = Convert.ToString(execResult["Control Id"]),
                                    Descp = Convert.ToString(execResult["Control Description"]),
                                    Sts = ConvertInt(execResult["CtrlSts"]),
                                    ModuleId = ModuleList[i],
                                    Level = Convert.ToInt32(execResult["Lvl"]),
                                    Section = Convert.ToString(execResult["SectionName"]),
                                    SectionId = Convert.ToString(execResult["Section Id"]),
                                    SectionStatus = Convert.ToInt32(execResult["Section Status"]),
                                    URL = Convert.ToString(execResult["URL"]),
                                }
                            });
                        };
                        objDataEngine.CloseConnection();
                    }

                }
                return _SecurityViewModel;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }


        public async Task<MsgRetriever> SaveWebUserAccessLevel(List<WebModule> ModuleList, List<WebPage> PageList, List<WebControl> CtrlList, List<WebPageSection> SectionList, string UserId)
        {
            //DataSet dataset = new DataSet();
            DataTable dataTable = new DataTable();
            //  dataTable.Columns.Add("ID");
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
                    //        dr["ID"]= id;
                    dr["ModuleID"] = ModuleList[i].ModuleId;
                    dr["PageID"] = ModuleList[i].PageId;//DBNull.Value;
                    dr["ControlID"] = ModuleList[i].CtrlId;//DBNull.Value;
                    dr["Status"] = ModuleList[i].Sts;
                    dataTable.Rows.Add(dr);
                    dr = dataTable.NewRow();
                    //      id = id + 1;
                }
            }
            if (PageList != null)
            {
                for (int i = 0; i < PageList.Count; i++)
                {
                    //    dr["ID"] = id;
                    dr["ModuleID"] = PageList[i].ModuleId;
                    dr["PageID"] = PageList[i].PageId;
                    dr["ControlID"] = PageList[i].CtrlId;//DBNull.Value; 
                    dr["Status"] = PageList[i].Sts;
                    dataTable.Rows.Add(dr);
                    dr = dataTable.NewRow();
                    //  id = id + 1;
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
            var objDataEngine = new FleetDataEngine(AccessMode.Admin, DBType.Web);

            try
            {
                await objDataEngine.InitiateConnectionAsync();
                var _SecurityViewModel = new List<SecurityViewModel>();

                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@UserId", UserId);
                Parameters[2] = new SqlParameter("@Tbl", dataTable);
                Parameters[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[3].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebUserAccessLevelMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
    }
}

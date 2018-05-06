using CCMS.ModelSector;
using FleetOps.Models;
using FleetSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilities.DAL;

namespace FleetSys.Models
{
    public class CollectionOps : BaseClass
    {
        public Int64 TOtalNoOfRecs { get; set; }
        /*
        public async Task<List<CollectionTaskListViewModel>> GetAllAcctCollection(CollectionTaskListViewModel collectionTaskListViewModel)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters;
                if (collectionTaskListViewModel != null)
                {
                    Parameters = new SqlParameter[10];
                    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                    Parameters[1] = String.IsNullOrEmpty(collectionTaskListViewModel.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", collectionTaskListViewModel.AcctNo);
                    Parameters[2] = String.IsNullOrEmpty(collectionTaskListViewModel.SelectedCorpCode) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", collectionTaskListViewModel.SelectedCorpCode);
                    Parameters[3] = String.IsNullOrEmpty(collectionTaskListViewModel.SelectedSalesTerritory) ? new SqlParameter("@SalesTerritory", DBNull.Value) : new SqlParameter("@SalesTerritory", collectionTaskListViewModel.SelectedSalesTerritory);
                    Parameters[4] = String.IsNullOrEmpty(collectionTaskListViewModel.SelectedCollectionSts) ? new SqlParameter("@Status", DBNull.Value) : new SqlParameter("@Status", collectionTaskListViewModel.SelectedCollectionSts);
                    Parameters[5] = String.IsNullOrEmpty(collectionTaskListViewModel.SelectedOwner) ? new SqlParameter("@Owner", DBNull.Value) : new SqlParameter("@Owner", collectionTaskListViewModel.SelectedOwner);
                    Parameters[6] = String.IsNullOrEmpty(collectionTaskListViewModel.RecallFromDate) ? new SqlParameter("@FromRecallDate", DBNull.Value) : new SqlParameter("@FromRecallDate", DateConverterDB(collectionTaskListViewModel.RecallFromDate));
                    Parameters[7] = String.IsNullOrEmpty(collectionTaskListViewModel.RecallToDate) ? new SqlParameter("@ToRecallDate", DBNull.Value) : new SqlParameter("@ToRecallDate", DateConverterDB(collectionTaskListViewModel.RecallToDate));
                    Parameters[8] = String.IsNullOrEmpty(collectionTaskListViewModel.CreationFromDate) ? new SqlParameter("@FromCreationDate", DBNull.Value) : new SqlParameter("@FromCreationDate", DateConverterDB(collectionTaskListViewModel.CreationFromDate));
                    Parameters[9] = String.IsNullOrEmpty(collectionTaskListViewModel.CreationToDate) ? new SqlParameter("@ToCreationDate", DBNull.Value) : new SqlParameter("@ToCreationDate", DateConverterDB(collectionTaskListViewModel.CreationToDate));
                }
                else
                {
                    Parameters = new SqlParameter[2];
                    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                    Parameters[1] = String.IsNullOrEmpty(collectionTaskListViewModel.SelectedOwner) ? new SqlParameter("@Owner", DBNull.Value) : new SqlParameter("@Owner", collectionTaskListViewModel.SelectedOwner);

                }

                var execResult = await objDataEngine.ExecuteCommandAsync("WebDelinquentAccts", CommandType.StoredProcedure, Parameters);
                var liCollectionTaskListViewModel = new List<CollectionTaskListViewModel>();

                while (execResult.Read())
                {
                    liCollectionTaskListViewModel.Add(new CollectionTaskListViewModel
                    {
                        EventId = Convert.ToString(execResult["EventId"])==null? string.Empty: Convert.ToString(execResult["EventId"]),
                        AcctNo = Convert.ToString(execResult["AcctNo"])==null? string.Empty: Convert.ToString((execResult["AcctNo"])),
                        CmpyName1 = Convert.ToString(execResult["CmpyName1"])==null? string.Empty: Convert.ToString((execResult["CmpyName1"])),
                        SelectedSalesTerritory = Convert.ToString(execResult["SaleTerritory"])==null? string.Empty: Convert.ToString((execResult["SaleTerritory"])),
                        AccumAgeingAmt = ConverterDecimal(execResult["CollectionAmt"]),
                        GraceDueDate = Convert.ToString(execResult["GraceDueDate"])==null? string.Empty: Convert.ToString((execResult["GraceDueDate"])),
                        CycAge = Convert.ToString(execResult["CycAge"])==null? string.Empty: Convert.ToString((execResult["CycAge"])),
                        Priority = Convert.ToString(execResult["Priority"])==null? string.Empty: Convert.ToString((execResult["Priority"])),
                        AccountSts = Convert.ToString(execResult["AccountSts"])==null? string.Empty: Convert.ToString((execResult["AccountSts"])),
                        SelectedCollectionSts = Convert.ToString(execResult["Collectionsts"])==null? string.Empty: Convert.ToString((execResult["Collectionsts"])),
                        SelectedOwner = Convert.ToString(execResult["Owner"])==null? string.Empty: Convert.ToString((execResult["Owner"])),
                        RecallDate = Convert.ToString(execResult["RecallDate"])==null? string.Empty: Convert.ToString((execResult["RecallDate"])),
                        CreationDate = Convert.ToString(execResult["CreationDate"])==null? string.Empty: Convert.ToString((execResult["CreationDate"])),
                        SelectedCorpCode = Convert.ToString(execResult["CorpCd"])==null? string.Empty: Convert.ToString((execResult["CorpCd"])),
                        CorpAcct = Convert.ToString(execResult["CorpName"])==null? string.Empty: Convert.ToString((execResult["CorpName"]))
                    });

                };

                return liCollectionTaskListViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        */
        /*
        public async Task<List<CollectionTaskListViewModel>> GetThresholdLmtCollection(int offSet, Int64 NoOfRecs, Int64 TOtalNoOfRecs, string sSearch)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[5];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@OffSet", ConvertLongToDb(offSet));
                Parameters[2] = new SqlParameter("@RecordsCnt", ConvertIntToDb(NoOfRecs));
                Parameters[3] = new SqlParameter("@SearchText", sSearch);
                Parameters[4] = new SqlParameter("@TotalRecs", SqlDbType.BigInt);
                Parameters[4].Direction = ParameterDirection.Output;

                SqlCommand cmd = new SqlCommand();
                var execResult = await objDataEngine.ExecuteCommandWithReturnValueAsync(cmd,"WebDelinquentAcctsTresholdLimit", CommandType.StoredProcedure, Parameters);
                var liCollectionTaskListViewModel = new List<CollectionTaskListViewModel>();

                while (execResult.Read())
                {
                    liCollectionTaskListViewModel.Add(new CollectionTaskListViewModel
                    {
                        AcctNo = Convert.ToString(execResult["AcctNo"]) == null ? string.Empty : Convert.ToString(execResult["AcctNo"]),
                        CmpyName1 = Convert.ToString(execResult["CompanyName"]) == null ? string.Empty : Convert.ToString(execResult["CompanyName"]),
                        CorpAcct = Convert.ToString(execResult["CorpAccount"]) == null ? string.Empty : Convert.ToString(execResult["CorpAccount"]),
                        CorpName = Convert.ToString(execResult["CorporateName"]) == null ? string.Empty : Convert.ToString(execResult["CorporateName"]),
                        SelectedSalesTerritory = Convert.ToString(execResult["SaleTerritory"]) == null ? string.Empty : Convert.ToString(execResult["SaleTerritory"]),
                        PermCreditLimit = ConverterDecimal(execResult["CreditLimit"]),
                        TempCreditLimit = ConverterDecimal(execResult["TempCreditLimit"]),
                        PercentageUsage = Convert.ToString(execResult["Usage"]) == null ? string.Empty : Convert.ToString(execResult["Usage"]),
                        AvailBalance = ConverterDecimal(execResult["AvailBal"]),
                        PukalAcctInd = Convert.ToString(execResult["PukalAccountInd"]) == null ? string.Empty : Convert.ToString(execResult["PukalAccountInd"])
                    });

                };
                execResult.Close();
                this.TOtalNoOfRecs = Convert.ToInt64(cmd.Parameters["@TotalRecs"].Value);
                return liCollectionTaskListViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        */
        //public async Task<CollectionAcctInfoViewModel> GetCollAcctInfo(string AcctNo)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

        //    try
        //    {
        //        objDataEngine.InitiateConnection();
        //        SqlParameter[] Parameters = new SqlParameter[2];
        //        Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //        Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);

        //        var execResult = await objDataEngine.ExecuteCommandAsync("WebDelinquentAcctInfo", CommandType.StoredProcedure, Parameters);

        //        while (execResult.Read())
        //        {
        //            var _CollectionAcctInfoViewModel = new CollectionAcctInfoViewModel
        //            {
        //                AcctNo = Convert.ToString(execResult["Account Number"]),
        //                CmpyName = Convert.ToString(execResult["Company Name"]),
        //                ClientType = Convert.ToString(execResult["Client Type"]),
        //                CorpCode = Convert.ToString(execResult["Corporate Code"]),
        //                CorpName = Convert.ToString(execResult["Corporate Name"]),
        //                SalesTerritory = Convert.ToString(execResult["Sales Territory"]),
        //                CreationDate = Convert.ToString(execResult["Creation Date"]),
        //                BlockedDate = Convert.ToString(execResult["Blocked Date"]),
        //                TempReinstatementDateFrom = Convert.ToString(execResult["Temp Reinstatement From"]),
        //                TempReinstatementDateTo = Convert.ToString(execResult["Temp Reinstatement To"]),
        //                ContactPerson = Convert.ToString(execResult["Contact Person"]),
        //                Occupation = Convert.ToString(execResult["Occupation"]),
        //                OfficePhone = Convert.ToString(execResult["Office Phone"]),
        //                MobileNo = Convert.ToString(execResult["Mobile No"]),
        //                EmailAddr = Convert.ToString(execResult["Email Address"])
        //            };

        //            return _CollectionAcctInfoViewModel;

        //        };

        //        return new CollectionAcctInfoViewModel();
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}

        //public async Task<CollInfoViewModel> GetCollFinancialInfo(string AcctNo)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

        //    try
        //    {
        //        objDataEngine.InitiateConnection();
        //        SqlParameter[] Parameters = new SqlParameter[2];
        //        Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //        Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);

        //        var execResult = await objDataEngine.ExecuteCommandAsync("WebDelinquentAcctFinancialInfo", CommandType.StoredProcedure, Parameters);

        //        while (execResult.Read())
        //        {
        //            var _CollInfoViewModel = new CollInfoViewModel
        //            {
        //                PaymentTerm = Convert.ToString(execResult["Payment Term"]),
        //                DunningCode = Convert.ToString(execResult["Dunning Code"]),
        //                PermanentCreditLimit = ConverterDecimal(execResult["Permanent Credit Limit"]),
        //                TempCreditLimit = ConverterDecimal(execResult["Temporary Credit Limit"]),
        //                TotalTAR = ConverterDecimal(execResult["Total Tar"]),
        //                OutstandingAmt = ConverterDecimal(execResult["Outstanding Amount"]),
        //                OverdueAmt = ConverterDecimal(execResult["Overdue Amount"]),
        //                AgeCode = Convert.ToString(execResult["Age Code"]),
        //                DelinquentDays = Convert.ToString(execResult["Delinquent Days"])

        //            };

        //            return _CollInfoViewModel;

        //        };

        //        return new CollInfoViewModel();
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}

        //public async Task<List<CollAgeingHistViewModel>> GetCollAgeingHistory(string AcctNo)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

        //    try
        //    {
        //        objDataEngine.InitiateConnection();
        //        SqlParameter[] Parameters = new SqlParameter[2];
        //        Parameters[0] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", AcctNo);
        //        Parameters[1] = new SqlParameter("@RptDate", DBNull.Value);
        //        var execResult = await objDataEngine.ExecuteCommandAsync("RptAcctAgeing", CommandType.StoredProcedure, Parameters);

        //        var liCollAgeingHistViewModel = new List<CollAgeingHistViewModel>();
        //        while (execResult.Read())
        //        {
        //            liCollAgeingHistViewModel.Add(new CollAgeingHistViewModel
        //            {

        //                Ageing = Convert.ToString(execResult["Ageing"]),
        //                Category = Convert.ToString(execResult["Category"]),
        //                TxnAmt = ConverterDecimal(execResult["TxnAmt"]),
        //                OutstandingAmt = ConverterDecimal(execResult["OutstandingAmt"]),
        //                BillingDate = Convert.ToString(execResult["BillingDate"]),
        //                DueDate = Convert.ToString(execResult["DueDate"]),
        //                GraceDueDate = Convert.ToString(execResult["GraceDueDate"]),
        //                LatestPaymentReceived = ConverterDecimal(execResult["LatestPaymentReceived"]),
        //                LatestPaymentDate = Convert.ToString(execResult["LatestPaymentDate"]),

        //            });
        //        };

        //        return liCollAgeingHistViewModel;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}

        public async Task<List<CollPaymentHistViewModel>> GetCollPaymentHist(string AcctNo, int offSet, Int64 NoOfRecs, Int64 TOtalNoOfRecs)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[5];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
                Parameters[2] = new SqlParameter("@NoOfRecs", ConvertLongToDb(NoOfRecs));
                Parameters[3] = new SqlParameter("@OffSet", ConvertIntToDb(offSet));
                Parameters[4] = new SqlParameter("@TotalNoOfRecs", SqlDbType.BigInt);
                Parameters[4].Direction = ParameterDirection.Output;

                SqlCommand cmd = new SqlCommand();
                var execResult = await objDataEngine.ExecuteCommandWithReturnValueAsync(cmd, "WebDelinquentAcctPymtHistory", CommandType.StoredProcedure, Parameters);
                var liCollPaymentHist = new List<CollPaymentHistViewModel>();

                while (execResult.Read())
                {
                    liCollPaymentHist.Add(new CollPaymentHistViewModel
                    {
                        StatementDate = Convert.ToString(execResult["Statement Date"]),
                        DueDate = Convert.ToString(execResult["Due Date"]),
                        TxnDate = Convert.ToString(execResult["Transaction Date"]),
                        PostingDate = Convert.ToString(execResult["Posting Date"]),
                        TxnDesc = Convert.ToString(execResult["Transaction Description"]),
                        TxnAmt = ConverterDecimal(execResult["Transaction Amount"]),
                        ApprovalCode = Convert.ToString(execResult["Approval Code"])

                    });
                };
                execResult.Close();
                this.TOtalNoOfRecs = Convert.ToInt64(cmd.Parameters["@TotalNoOfRecs"].Value);

                return liCollPaymentHist;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }

        //public async Task<List<CollectionHistoryViewModel>> GetCollHistory(string AcctNo, string CollectionCaseSts)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

        //    try
        //    {
        //        objDataEngine.InitiateConnection();
        //        SqlParameter[] Parameters = new SqlParameter[3];
        //        Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //        Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
        //        Parameters[2] = new SqlParameter("@Ind", CollectionCaseSts);
        //        var execResult = await objDataEngine.ExecuteCommandAsync("WebDelinquentAcctCollHistory", CommandType.StoredProcedure, Parameters);

        //        var liCollectionHistoryViewModel = new List<CollectionHistoryViewModel>();
        //        while (execResult.Read())
        //        {
        //            liCollectionHistoryViewModel.Add(new CollectionHistoryViewModel
        //            {

        //                CollectionNo = Convert.ToString(execResult["Collect No"]),
        //                Priority = Convert.ToString(execResult["Priority"]),
        //                CollectSts = Convert.ToString(execResult["Collect Status"]),
        //                UserId = Convert.ToString(execResult["User Id"]),
        //                CloseDate = Convert.ToString(execResult["Close Date"]),
        //                CreationDate = Convert.ToString(execResult["Creation Date"]),
        //            });
        //        };

        //        return liCollectionHistoryViewModel;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}

        //public async Task<List<CollectionFollowUpViewModel>> GetCollFollowUp(string EventId)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

        //    try
        //    {
        //        objDataEngine.InitiateConnection();
        //        SqlParameter[] Parameters = new SqlParameter[2];
        //        Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //        Parameters[1] = String.IsNullOrEmpty(EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", EventId);
        //        //  Parameters[2] =new SqlParameter("@Dropdown", ConvertIntToDb( Dropdown));
        //        //  Parameters[3] =  new SqlParameter("@Ind",ConvertIntToDb (Ind));

        //        var execResult = await objDataEngine.ExecuteCommandAsync("WebDelinquentAcctCollFollowupList", CommandType.StoredProcedure, Parameters);
        //        var liCollectionFollowUpViewModel = new List<CollectionFollowUpViewModel>();
        //        while (execResult.Read())
        //        {
        //            liCollectionFollowUpViewModel.Add(new CollectionFollowUpViewModel
        //           {
        //               SelectedCollectionSts = Convert.ToString(execResult["Status"]),
        //               SelectedPriority = Convert.ToString(execResult["Priority"]),
        //               CreationDate = Convert.ToString(execResult["EventCreationDate"]),
        //               RecallDate = DateConverter(execResult["RecallDate"]),
        //               LastUpdate = Convert.ToString(execResult["LastUpdDate"]),
        //               UserId = Convert.ToString(execResult["CreatedBy"]),
        //               Remarks = Convert.ToString(execResult["Remarks"]),
        //               NoteCreationDate = Convert.ToString(execResult["DetailCreationDate"])
        //           });
        //        };
        //        if (liCollectionFollowUpViewModel.Count == 0)
        //        {
        //            liCollectionFollowUpViewModel.Add(new CollectionFollowUpViewModel());
        //        }
        //        return liCollectionFollowUpViewModel;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}

        //public async static Task<IEnumerable<SelectListItem>> GetCollFollwUpDropdown(string EventId)
        //{
        //    var objEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    try
        //    {
        //        await objEngine.InitiateConnectionAsync();
        //        SqlParameter[] Parameters = new SqlParameter[2];
        //        Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //        Parameters[1] = String.IsNullOrEmpty(EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", EventId);
        //      //  Parameters[2] = new SqlParameter("@Dropdown", 1);
        //      //  Parameters[3] = new SqlParameter("@Ind", DBNull.Value);

        //        var getObjData = await objEngine.ExecuteCommandAsync("WebDelinquentAcctCollFollowupList", CommandType.StoredProcedure, Parameters);
        //        var list = new List<SelectListItem>();

        //        while (getObjData.Read())
        //        {
        //            list.Add(new SelectListItem
        //            {
        //                Text = Convert.ToString(getObjData["RefCd"]),
        //                Value = Convert.ToString(getObjData["Descp"])
        //            });
        //        }
        //        return list;
        //    }
        //    finally
        //    {
        //        objEngine.CloseConnection();
        //    }
        //}

        //public async Task<MsgRetriever> SaveCollectionFollowUp(CollectionFollowUpViewModel collection)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

        //    try
        //    {
        //        objDataEngine.InitiateConnection();

        //        SqlParameter[] Parameters = new SqlParameter[8];
        //        Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //        Parameters[1] = String.IsNullOrEmpty(collection.EventId) ? new SqlParameter("@EventID", System.Data.SqlTypes.SqlInt64.Null) : new SqlParameter("@EventID", ConvertLong(collection.EventId));
        //        Parameters[2] = String.IsNullOrEmpty(collection.UserId) ? new SqlParameter("@UserID", DBNull.Value) : new SqlParameter("@UserID", collection.UserId);
        //        Parameters[3] = String.IsNullOrEmpty(collection.SelectedCollectionSts) ? new SqlParameter("@CollectionSts", DBNull.Value) : new SqlParameter("@CollectionSts", collection.SelectedCollectionSts);
        //        Parameters[4] = String.IsNullOrEmpty(collection.SelectedPriority) ? new SqlParameter("@Priority", DBNull.Value) : new SqlParameter("@Priority", collection.SelectedPriority);
        //        Parameters[5] = String.IsNullOrEmpty(collection.RecallDate) ? new SqlParameter("@RecallDate", DBNull.Value) : new SqlParameter("@RecallDate", DateConverterDB(collection.RecallDate));
        //        Parameters[6] = String.IsNullOrEmpty(collection.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", collection.Remarks);
        //        Parameters[7] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
        //        Parameters[7].Direction = ParameterDirection.ReturnValue;

        //        var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebDelinquentAcctCollFollowupMaint", CommandType.StoredProcedure, Parameters);
        //        var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);

        //        var Descp = await GetMessageCode(Result);
        //        return Descp;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}
    }
}
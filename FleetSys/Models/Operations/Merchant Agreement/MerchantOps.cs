using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using FleetOps.DAL;
using CCMS.ModelSector;
using FleetOps.ViewModel;
namespace FleetOps.Models
{
//    public class MerchantOps:IMerchantOps
//    {
//        //public int totalRecords { get; set; }
//        //public int displayRecods { get; set; }
//        //private CCMSEntities _context;
//        //public MerchantOps()
//        // {
//        //     _context = new CCMSEntities();
//        // }

        
//        //public  IEnumerable<SelectListItem> GetMerchContactType()
//        //{
//        //    var query = _context.iss_RefLib.Where(p => p.IssNo == 1 && p.RefType.Equals("Contact") && p.RefNo == 64).Select(q => new SelectListItem { Text = q.Descp, Value = q.RefCd });
//        //    return query;
//        //}
//        //public  IEnumerable<SelectListItem> GetMerchAddressType()
//        //{
//        //    var query = _context.iss_RefLib.Where(p => p.IssNo == 1 && p.RefType.Equals("Address") && p.RefNo == 64).Select(q => new SelectListItem { Text = q.Descp, Value = q.RefCd });
//        //    return query;
//        //}
//        //public  IEnumerable<SelectListItem> GetMerchState()
//        //{
//        //    var query = _context.iss_Issuer.Where(q => q.IssNo == 1).Select(y => y.CtryCd).SingleOrDefault();
//        //    var state = _context.iss_State.Where(q => q.CtryCd == query && q.IssNo == 1).Select(p => new SelectListItem { Text = p.Descp, Value = p.StateCd });
//        //    return state;
//        //}
//        ////public IEnumerable<CICnMCC> GetCICANDMCC(jQueryDataTableParamModel Model, string Type)
//        ////{
//        ////    IQueryable<CICnMCC> CiCnMcc;
//        ////        var query = _context.cmn_MerchantType.Where(x => x.Type == Type).OrderBy(x=>x.CategoryCd).Select(p => new CICnMCC
//        ////        {
//        ////            CategoryCd = p.CategoryCd,
//        ////            Descp = p.Descp,
//        ////            MerchCategoryCd = p.Code
//        ////        });
//        ////        this.totalRecords = query.Count();
//        ////        if (!string.IsNullOrEmpty(Model.sSearch))
//        ////        {
//        ////            Model.sSearch = Model.sSearch.ToLower();
//        ////            CiCnMcc = query.Where(p => p.CategoryCd.ToLower().Contains(Model.sSearch) || p.Descp.ToLower().Contains(Model.sSearch) || p.MerchCategoryCd.ToLower().Contains(Model.sSearch)).Skip(Model.iDisplayStart).Take(Model.iDisplayLength);
//        ////        }
//        ////        else
//        ////        {
//        ////            CiCnMcc = query.Skip(Model.iDisplayStart).Take(Model.iDisplayLength);
//        ////        }
//        ////        this.displayRecods = this.totalRecords;
//        ////    return CiCnMcc.ToList();
//        ////}
       
//        //public AddMerchantViewModel MerchantInfo(Int64 AcctNo)
//        //{
//        //    //3000000011
//        //    var query = from a in _context.aac_Account
//        //                from b in _context.iss_RefLib
//        //                where a.AcctNo.Equals(AcctNo) && a.AcqNo == b.IssNo && a.Sts == b.RefCd && b.RefType.Contains("MerchAcctSts")
//        //                select new AddMerchantViewModel
//        //                {
//        //                    AcctNo = a.AcctNo,
//        //                    Detail = new MerchantDtl
//        //                    {
//        //                        AccountName = a.BusnName,
//        //                        Ownership = new MerchOwnership
//        //                        {
//        //                            AgreementDate = a.AgreementDate ?? DateTime.MinValue,
//        //                            AgreementNo = a.AgreementNo,
//        //                            selectedCorporateCd = a.CorpCd,
//        //                            CoRegNo = a.CoRegNo,
//        //                            CoRegName = a.CoRegName,
//        //                            CancelDate = a.CancelDate ?? DateTime.MinValue,
//        //                            selectedOwnership = a.Ownership,
//        //                            PersonInCharge = a.PersonInCharge,
//        //                            MCC = a.Mcc,
//        //                            SIC = a.Sic,
//        //                            PayeeName = a.PayeeName
//        //                        },
//        //                        Status = new MerchStatus
//        //                        {
//        //                            CreatedBy = a.CreatedBy,
//        //                            CreationDate = a.CreationDate ?? DateTime.MinValue,
//        //                            selectedReasonCd = a.ReasonCd,
//        //                            selectedStatus = a.Sts
//        //                        },
//        //                        taxInfo = new MerchTaxInfo { TaxID = a.TaxId ?? 0 }

//        //                    },
//        //                    Entity = new MerchEntityDtl
//        //                    {
//        //                        EntityId = a.EntityId ?? 0,
//        //                        BankingDtl = new MerchpersonalBankingDtl
//        //                        {
//        //                            selectedBankAccountType = a.BankAcctType
//        //                        },
//        //                         PersonalParticulars=new PersonlParticulars(),
//        //                         EmploymentDetail=new EmploymentDtl()
                                  


//        //                    },
//        //                    Contacts=new MerchantContacts(),
//        //                    Address=new MerchAddress()
//        //                };

//        //    var quer2 = query.FirstOrDefault();
//        //   // quer2.Accessibility = BaseClass.Accessibility("CCMS", "Accounts/New");
//        //    quer2.Detail.Ownership.Ownership = BaseClass.WebGetRefLib("MerchOwnership");
//        //    quer2.Detail.Ownership.CorporateCd = BaseClass.WebGetRefLib("CorpCd");
//        //    quer2.Detail.Ownership.Establishment = BaseClass.WebGetRefLib("BusnSize");
//        //    quer2.Detail.Status.Status = BaseClass.WebGetRefLib("MerchAcctSts");
//        //    quer2.Detail.Status.ReasonCode = BaseClass.WebGetRefLib("MerchReasonCd");
//        //    quer2.Entity.PersonalParticulars.BloodGroup = BaseClass.WebGetRefLib("Blood");
//        //    quer2.Entity.PersonalParticulars.MaritalStatus = BaseClass.WebGetRefLib("Marital");
//        //    quer2.Entity.EmploymentDetail.Department = BaseClass.WebGetRefLib("Dept");
//        //    quer2.Entity.EmploymentDetail.Occupation = BaseClass.WebGetRefLib("Occupation");
//        //    quer2.Entity.BankingDtl.BankAcctType = BaseClass.WebGetRefLib("Bank");
//        //    quer2.Contacts.Position = BaseClass.WebGetRefLib("Occupation");
//        //    quer2.Contacts.ContactType = GetMerchContactType();
//        //    quer2.Contacts.ContactStatus = BaseClass.WebGetRefLib("ContactSts");
//        //    quer2.Address.AddressType = GetMerchAddressType();
//        //    quer2.Address.countryCd = BaseClass.WebGetRefLib("Country");
//        //    quer2.Address.State = GetMerchState();
//        //    return quer2;

//        //}

//        //public string MerchMaint(AddMerchantViewModel viewModel)
//        //{
//        //    FleetDataEngine Engine = new FleetDataEngine(AccessMode.Admin, DBType.Maint);
//        //    Engine.InitiateConnection();
//        //    SqlCommand Cmd = new SqlCommand();
//        //    string RtnCd = null;

//        //    SqlParameter[] ParamaterValues = new SqlParameter[30];
//        //    ParamaterValues[1] = new SqlParameter("@AcqNo", "1");
//        //    ParamaterValues[2] = new SqlParameter("@BusnName", viewModel.Detail.AccountName);
//        //    ParamaterValues[3] = new SqlParameter("@AgreeNo", viewModel.Detail.Ownership.AgreementNo);
//        //    ParamaterValues[4] = new SqlParameter("@AgreeDate", viewModel.Detail.Ownership.AgreementDate);
//        //    ParamaterValues[5] = new SqlParameter("@BankName", viewModel.Entity.BankingDtl.BankAcctType);
//        //    ParamaterValues[6] = new SqlParameter("@CorpCd", viewModel.Detail.Ownership.selectedCorporateCd);
//        //    ParamaterValues[7] = new SqlParameter("@ReasonCd", viewModel.Detail.Status.selectedReasonCd);
//        //    ParamaterValues[8] = new SqlParameter("@PersonInChrg", viewModel.Detail.Ownership.PersonInCharge);
//        //    ParamaterValues[9] = new SqlParameter("@Ownership", viewModel.Detail.Ownership.selectedOwnership);
//        //    ParamaterValues[10] = new SqlParameter("@Establishment", viewModel.Detail.Ownership.selectedEstablishment);
//        //    ParamaterValues[11] = new SqlParameter("@Sic", viewModel.Detail.Ownership.SIC);
//        //    ParamaterValues[12] = new SqlParameter("@MCC", viewModel.Detail.Ownership.MCC);
//        //    ParamaterValues[13] = new SqlParameter("@CreatedBy", viewModel.Detail.Status.CreatedBy);
//        //    ParamaterValues[14] = new SqlParameter("@CreateDate", viewModel.Detail.Status.CreationDate);
//        //    ParamaterValues[15] = new SqlParameter("@CoRegNo", viewModel.Detail.Ownership.CoRegNo);
//        //    ParamaterValues[16] = new SqlParameter("@BankAcctType", viewModel.Entity.BankingDtl.BankAcctType);
//        //    ParamaterValues[17] = new SqlParameter("@PayeeName", "");
//        //    ParamaterValues[18] = new SqlParameter("@AutoDebit", "");
//        //    ParamaterValues[19] = new SqlParameter("@CoRegName", viewModel.Detail.Ownership.CoRegName);
//        //    ParamaterValues[20] = new SqlParameter("@TaxId", viewModel.Detail.taxInfo.TaxID);
//        //    ParamaterValues[21] = new SqlParameter("@BranchCd", "");
//        //    ParamaterValues[22] = new SqlParameter("@WithholdInd", "");
//        //    ParamaterValues[23] = new SqlParameter("@WithholdRate", "");
//        //    ParamaterValues[24] = new SqlParameter("@AcctType","");
//        //    ParamaterValues[25] = new SqlParameter("@LastUpdDate",DateTime.Now.ToString("yyyyMMdd"));
//        //    ParamaterValues[26] = new SqlParameter("@AcctNo", SqlDbType.VarChar, 30);
//        //    ParamaterValues[27] = new SqlParameter("@Sts", SqlDbType.VarChar, 30);
//        //    ParamaterValues[28] = new SqlParameter("@EntityId", SqlDbType.VarChar, 30);
//        //    ParamaterValues[29] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4);
//        //    ParamaterValues[26].Direction = ParameterDirection.Output;
//        //    ParamaterValues[27].Direction = ParameterDirection.Output;
//        //    ParamaterValues[28].Direction = ParameterDirection.Output;
//        //    ParamaterValues[29].Direction = ParameterDirection.ReturnValue;
//        //   // Cmd = Engine.ExecuteCommand("MerchMaint", CommandType.StoredProcedure, ParamaterValues);
//        //    return "";
//        //}





//        //public IEnumerable<CICnMCC> GetCICANDMCC(jQueryDataTableParamModel Model, string Type)
//        //{
//        //    throw new NotImplementedException();
//        //}
//    }
}
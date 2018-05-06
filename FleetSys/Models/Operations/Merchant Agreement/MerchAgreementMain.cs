using System;
using System.Collections.Generic;
using System.Linq;
using CCMS.ModelSector;
using FleetOps.DAL;
using PagedList;
namespace FleetOps.Models
{
    public class MerchAgreementMain
    {
        public long AcctNo { get; set; }
        public string BusnName { get; set; }
        //public string MerchantName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
    }

  //  public partial class MerchAgreementOps:IMerchAgreementOps
  // { 
  ////      public int totalRecords { get; set; }
  ////      public int displayRecods { get; set; }
  ////      CCMSEntities context = new CCMSEntities();
  ////      public IEnumerable<MerchAgreementMain> GetAgreementBy()
  ////      {
  ////          var merch = new List<MerchAgreementMain>();
  ////          var query1 = from _Account in context.aac_Account
  ////                       from _Reflib in context.iss_RefLib
  ////                       where _Account.AcqNo == _Reflib.IssNo && _Account.Sts == _Reflib.RefCd && _Reflib.RefType.Equals("MerchAcctSts")
  ////                           && _Reflib.RefInd > 0 && _Account.AcqNo == 1
  ////                       select new
  ////                           {
  ////                               _Account.AcctNo,
  ////                               _Account.BusnName,
  ////                               _Account.CreatedBy,
  ////                               _Account.CreationDate,
  ////                               _Reflib.Descp
  ////                           };
  ////          var query = query1.AsEnumerable();
  ////          foreach (var result in query)
  ////          {
  ////              merch.Add(new MerchAgreementMain
  ////              {
  ////                  AcctNo = result.AcctNo,
  ////                  BusnName = result.BusnName,
  ////                  CreatedBy = result.CreatedBy,
  ////                  Status = result.Descp
  ////              });
  ////          }
  ////          return merch;
  ////      }
  ////      public  List<MerchAgreementMain> getAgreementbyAccountNo(jQueryDataTableParamModel model)
  ////      {
  ////          var entity = new CCMSEntities();
  ////          IEnumerable<MerchAgreementMain> ResultbyAccNo;
  ////          var query = (from _Account in entity.aac_Account
  ////                       from _Reflib in entity.iss_RefLib
  ////                       where _Account.AcqNo == _Reflib.IssNo && _Account.Sts == _Reflib.RefCd && _Reflib.RefType.Equals("MerchAcctSts")
  ////                           && _Reflib.RefInd > 0 && _Account.AcqNo == 1 orderby _Account.AcctNo
  ////                       select new MerchAgreementMain
  ////                       {
  ////                           AcctNo = _Account.AcctNo,
  ////                           BusnName = _Account.BusnName,
  ////                           CreatedBy = _Account.CreatedBy,
  ////                           CreationDate = _Account.CreationDate ?? DateTime.MinValue,
  ////                           Status = _Reflib.Descp
  ////                       });
  ////          this.totalRecords = query.Count();
  ////          if (!string.IsNullOrEmpty(model.sSearch))
  ////          {
  ////              model.sSearch = model.sSearch.ToLower();
  ////              ResultbyAccNo = query.AsEnumerable().Where(p => Convert.ToString(p.AcctNo).Contains(model.sSearch) || p.BusnName.ToLower().Contains(model.sSearch) || 
  ////                  p.CreatedBy.ToLower().Contains(model.sSearch) ||
  ////                  p.Status.ToLower().Contains(model.sSearch)).Skip(Convert.ToInt16(model.iDisplayStart)).Take(model.iDisplayLength);
  ////          }
  ////          else
  ////          {
  ////              var result = query.Skip(Convert.ToInt16(model.iDisplayStart)).Take(model.iDisplayLength);
  ////              ResultbyAccNo = result.AsEnumerable();
  ////          }
  ////          this.displayRecods = this.totalRecords;
  ////          return ResultbyAccNo.ToList();
  ////      }
  ////      public object AgreementByAccountNo(JqString collection)
  ////      {
  ////          var query = (from _Account in context.aac_Account
  ////                       from _Reflib in context.iss_RefLib
  ////                       where _Account.AcqNo == _Reflib.IssNo && _Account.Sts == _Reflib.RefCd && _Reflib.RefType.Equals("MerchAcctSts")
  ////                           && _Reflib.RefInd > 0 && _Account.AcqNo == 1
  ////                       orderby (collection.sidx + " " + collection.sord)
  ////                       select new MerchAgreementMain
  ////                       {
  ////                           AcctNo = _Account.AcctNo,
  ////                           BusnName = _Account.BusnName,
  ////                           CreatedBy = _Account.CreatedBy,
  ////                           CreationDate = _Account.CreationDate ?? DateTime.MinValue,
  ////                           Status = _Reflib.Descp
  ////                       });
  ////          var query1 =General.Filterfurther(collection, query).AsEnumerable();
  ////          var foo = query1.Select(p => new
  ////          {
  ////              id = Convert.ToString(p.AcctNo),
  ////              cell = new object[]
  ////          {
  ////           p.AcctNo,
  ////           p.BusnName,
  ////           p.CreatedBy,
  ////Convert.ToDateTime(p.CreationDate).ToString("dd/MM/yyyy"),
  ////           p.Status
  ////          }
  ////          }).ToArray();

  ////          totalRecords = query1.Count();
  ////          var result2 = foo.ToPagedList(collection.page, collection.rows);
  ////          return result2;
  ////      }
  ////      public object AgreementByMerchant()
  ////      {
  ////          var query = (from _busnLocation in context.aac_BusnLocation
  ////                       from _reflib in context.iss_RefLib
  ////                       from _account in context.aac_Account
  ////                       where _busnLocation.AcqNo == _reflib.IssNo && _busnLocation.Sts == _reflib.RefCd && _reflib.RefType.Equals("MerchAcctSts") && _reflib.RefInd > 0 && _busnLocation.AcqNo == _account.AcqNo
  ////                       && _busnLocation.AcctNo == _account.AcctNo && _busnLocation.AcqNo == 1
  ////                       select new
  ////                       {
  ////                           _busnLocation.BusnLocation,
  ////                           _busnLocation.AcctNo,
  ////                           _busnLocation.BusnName,
  ////                           _busnLocation.CreationDate,
  ////                           _busnLocation.CreatedBy,
  ////                           _reflib.Descp
  ////                       }).AsEnumerable();

  ////          var foo = query.Select(f => new
  ////          {
  ////              id = Convert.ToString(f.AcctNo),
  ////              cell = new object[]
  ////           {
  ////           f.AcctNo,
  ////                           f.BusnLocation,
  ////                       f.BusnName,
  ////                       f.CreatedBy,
  ////                       f.CreationDate.HasValue?Convert.ToDateTime(f.CreationDate).ToString("dd/MM/yyyy"):"Unknown",
  ////                       f.Descp
  ////           }
  ////          }).ToArray();
  ////          totalRecords = query.Count();
  ////          return foo;

  ////      }
  //  }
}
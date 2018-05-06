using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class DelinquentAcctInfoDTO
    {
       public Int64 AccountNumber { get; set; }
       public string CompanyName { get; set; }
       public string ClientType { get; set; }
       public string CorporateCode { get; set; }
       public string CorporateName { get; set; }
       public string SalesTerritory { get; set; }
       public DateTime? CreationDate { get; set; }
       public DateTime? BlockedDate { get; set; }
       public string TempReinstatementFrom { get; set; }
       public string TempReinstatementTo { get; set; }
       public string ContactPerson { get; set; }
       public string Occupation { get; set; }
       public string OfficePhone { get; set; }
       public string MobileNo { get; set; }
       public string EmailAddress { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.CardHolder
{
   public class LocationAcceptDTO
    {
       public LocationAcceptDTO()
       {
           SelectedStates = new List<string>();
       }
       public Int64 cardNo { get; set; }
       public string MerchantId { get; set; }
       public string BusnName { get; set; }
       public string SiteId { get; set; }
       public string DBA_Name { get; set; }
       public string DBA_City { get; set; }
       public string UserId { get; set; }
       public string CreationDate { get; set; }
       public List<string> SelectedBusnLocations { get; set; }
       public List<string> SelectedStates { get; set; }
    }
}

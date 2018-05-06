using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class WebAddressDTO
    {
       public string AddressType { get; set; }
       public string MainMailing { get; set; }
       public string Street1 { get; set; }
       public string Street2 { get; set; }
       public string Street3 { get; set; }
       public string Street4 { get; set; }
       public string Street5 { get; set; }
       public string City { get; set; }
       public string StateCd { get; set; }
       public string PostalCd { get; set; }
       public string Country { get; set; }
       public string RefCd { get; set; }
       public string Region { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto
{
   public class IssMessageDTO
    {
       public IssMessageDTO()
       {
           paraOut = new ReturnObject();
       }
       public int Flag { get; set; }
       public string Descp { get; set; }
       public ReturnObject paraOut;
    }
}

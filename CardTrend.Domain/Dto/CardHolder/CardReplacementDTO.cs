using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.CardHolder
{
   public class CardReplacementDTO
    {
       public Int64? NewCardNo { get; set; }
       public Int64? PrevCardNo { get; set; }
       public DateTime? CardExpiry { get; set; }
       public DateTime? TerminatedDate { get; set; }
       public string Sts { get; set; }
       public string Remarks { get; set; }
       public string FeeCd { get; set; }
       public string ReasonCd { get; set; }
       public string RsCode { get; set; }
       public int? CardMedia { get; set; }
    }
}

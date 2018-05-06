using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Application
{
   public class VeloctyLimitListMaintDTO
    {
       public string AccNo { get; set; }
       public string CardNo { get; set; }
       public string ApplId { get; set; }
       public string AppcId { get; set; }
       public string CostCentre { get; set; }
       public string SelectedCorpCd { get; set; }
       public Int64 CostCentreId { get; set; }
       public string CostCentreDescription { get; set; }
       public string VelocityIndicator { get; set; }
       public string CostCentreCode { get; set; }
       public string VelocityIndicatorDescription { get; set; }
       public decimal VelocityAmount { get; set; }
       public int Counter { get; set; }
       public decimal VelocityLitre { get; set; }
       public string ProductDescription { get; set; }
       public string ProductCode { get; set; }
       public decimal SpentAmount { get; set; }
       public int SpentCounter { get; set; }
       public decimal SpentLitre { get; set; }
       public string UserId { get; set; }
       public DateTime? CreationDate { get; set; }
       public DateTime? LastUpdateDate { get; set; }
    }
}

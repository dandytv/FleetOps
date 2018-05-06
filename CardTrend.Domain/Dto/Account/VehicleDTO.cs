using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class VehicleDTO
    {
       public string AppcId { get; set; }
       public string CardNo { get; set; }
       public string CardType { get; set; }
       public string SKDSIndicator { get; set; }
       public string PIN { get; set; }
       public string SKDSInd { get; set; }
       public string VRN { get; set; }
       public decimal? SKDSQuota { get; set; }
       public DateTime? RegisteredDate { get; set; }
       public string VehicleMaker { get; set; }
       public string Status { get; set; }
       public Int64? XrefCardNo { get; set; }
       public int? OdometerReading { get; set; }
       public DateTime? OdometerUpdate { get; set; }
       public string CardExpiry { get; set; }
       public DateTime? RoadTaxExpiry { get; set; }
       public string VehicleType { get; set; }
       public string VehicleColor { get; set; }
       public string VehicleModel { get; set; }
       public string CardTerminated { get; set; }
    }
}

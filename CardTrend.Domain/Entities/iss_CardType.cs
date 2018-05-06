using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Entities
{
   public class iss_CardType
    {
        public short IssNo { get; set; }
        public string CardLogo { get; set; }
        [Key]
        [Column(Order = 1)]
        public string PlasticType { get; set; }
        [Key]
        [Column(Order = 2)]
        public short CardType { get; set; }
        public string Descp { get; set; }
        public string ShortDescp { get; set; }
        public string CardRangeId { get; set; }
        public string CardCategory { get; set; }
        public string VehInd { get; set; }
        public Nullable<short> Attribute { get; set; }
        public Nullable<short> AuthCardType { get; set; }
        public Nullable<System.DateTime> LastUpdDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Entities
{
   public class iss_RefLib
    {
        [Key]
        [Column(Order = 1)]
        public short IssNo { get; set; }
        [Key]
        [Column(Order = 2)]
        public string RefType { get; set; }
        [Key]
        [Column(Order = 3)]
        public string RefCd { get; set; }
        public int RefNo { get; set; }
        public short RefInd { get; set; }
        public string RefId { get; set; }
        public Nullable<int> MapInd { get; set; }
        public string Descp { get; set; }
    }
}

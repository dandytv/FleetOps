using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.GlobalVariables
{
   public class LookupParameterDTO
    {
       public LookupParameterDTO()
       {
           ProductItems = new List<ProductListItemDTO>();
       }
        public string Country { get; set; }
        public string CtryCd { get; set; }
        public string StateName { get; set; }
        public string StateCd { get; set; }
        public string State { get; set; }
        public string ParamCd { get; set; }
        public string ParameterDescp { get; set; }
        public string City { get; set; }
        public string Descp { get; set; }
        public string Type { get; set; }

        public string RefId { get; set; }
        public int RefNo { get; set; }
        public string ProductGroup { get; set; }
        public string ProdCd { get; set; }
        public string ProdName { get; set; }
        public string ProdDescp { get; set; }
        public string ProdCategory { get; set; }
        public string ProdType { get; set; }
        public decimal? ProdUnitPrice { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public IEnumerable<ProductListItemDTO> ProductItems { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Domain.Dto.Applicant
{
   public class CardAppcInfoDTO
    {
       public CardAppcInfoDTO()
       {
           CardTypeLst = new List<SelectListItem>();
           BranchCdLst = new List<SelectListItem>();
           DivisionCodeLst = new List<SelectListItem>();
           DeptCdLst = new List<SelectListItem>();
       }
       public string CardType { get; set; }
       public string CardNo { get; set; }
       public string DriverName { get; set; }
       public string EmbName { get; set; }
       public string VehRegsNo { get; set; }
       public string Sts { get; set; }
       public string SKDSInd { get; set; }
       public decimal? SKDSQuota { get; set; }
       public string SKDSNo { get; set; }
       public string DialogueInd { get; set; }
       public string PushAlertInd { get; set; }
       public decimal? LitLimit { get; set; }
       public decimal? TxnLimit { get; set; }
       public byte? PINExceedCnt { get; set; }
       public string EntityId { get; set; }
       public string AcctNo { get; set; }
       public string PINInd { get; set; }
       public string OdometerInd { get; set; }
       public string JoiningFeeCd { get; set; }
       public string AnnlFeeCd { get; set; }
       public string PrimaryCard { get; set; }
       public int? PriAppcId { get; set; }
       public string ProdCd { get; set; }
       public int? AppcId { get; set; }
       public string Model { get; set; }
       public string CostCentre { get; set; }
       public string BranchCd { get; set; }
       public string DivisionCd { get; set; }
       public string DeptCd { get; set; }
       public string ProdGroup { get; set; }
       public int CardMedia { get; set; }
       public string CardMediaDescp { get; set; }
       public IEnumerable<SelectListItem> CardTypeLst { get; set; }
       public IEnumerable<SelectListItem> BranchCdLst { get; set; }
       public IEnumerable<SelectListItem> DivisionCodeLst { get; set; }
       public IEnumerable<SelectListItem> DeptCdLst { get; set; }
    }
}

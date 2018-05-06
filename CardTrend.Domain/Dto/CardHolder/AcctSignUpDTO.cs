using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.CardHolder
{
  public  class AcctSignUpDTO
    {
        public int IssNo { get; set; }
        public string PlasticType { get; set; }
        public Byte CycNo { get; set; }
        public string ApplId { get; set; }
        public string AcctNo { get; set; }
        public string CorpCd { get; set; }
        public string Reference { get; set; }
        public string CmpyLegalName { get; set; }
        public string CmpyName { get; set; }
        public string CmpyEmbName { get; set; }
        public string ContactPerson { get; set; }
        public string Position { get; set; }
        public string OfficePhone { get; set; }
        public string MobileNo { get; set; }
        public string OfficeFax { get; set; }
        public string SAPNo { get; set; }
        public string EmailAddr { get; set; }
        public string CmpyType { get; set; }
        public string CmpyRegsNo { get; set; }
        public DateTime? CmpyRegsDate { get; set; }
        public string NatureOfBusn { get; set; }
        public string BillMethod { get; set; }
        public string InvoicePref { get; set; }
        public string LoyaltyCardNo { get; set; }
        public string LoyaltyFullName { get; set; }
        public string LoyaltyIcNo { get; set; }
        public string LoyaltyContactNo { get; set; }
        public string LoyaltyeBusn { get; set; }
        public string BusnCategory { get; set; }
        public string EntityId { get; set; }
        public string DocPath { get; set; }
        public string InvBillInd { get; set; }
        public string PymtInd { get; set; }
        public string VehPerfRptInd { get; set; }
        public string TaxCategory { get; set; }
        public string WithHoldingTax { get; set; }
        public string LangId { get; set; }
        public string Website { get; set; }
        public string ClientClass { get; set; }
        public string ClientType { get; set; }
        public string AuthName { get; set; }
        public string UserId { get; set; }
        public string ReasonCd { get; set; }
    }
}

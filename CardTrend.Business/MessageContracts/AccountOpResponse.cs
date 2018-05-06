using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.Corporate;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class AccountOpResponse : ResponseBase
    {
        public AccountOpResponse()
        {
            financialInfo = new FinancialInfoModel();
            costCentre = new CostCentre();
            eventLogger = new EventLogger();
            creditAssesOperation = new CreditAssesOperation();
            tempCreditCtrl = new TempCreditCtrlModel();
            upToDateBal = new UpToDateBal();
            productDiscount = new ProductDiscount();
            PaymentTxn = new PaymentTxn();
            costCentres = new List<CostCentre>();
            WebSecDepRemarks = new List<RemarkHistory>();
            creditLimitHistories = new List<CreditLimitHistory>();
            financilInfoItems = new List<FinancilInfoItemsList>();
            eventLoggers = new List<EventLogger>();
            productDiscounts = new List<ProductDiscount>();
            onlineTransactions = new List<OnlineTransaction>();
            PaymentTxns = new List<PaymentTxn>();
            BillingItems = new List<BillingItem>();
        }
        public FinancialInfoModel financialInfo { get; set; }
        public CostCentre costCentre { get; set; }
        public UpToDateBal upToDateBal { get; set; }
        public CreditAssesOperation creditAssesOperation { get; set; }
        public TempCreditCtrlModel tempCreditCtrl { get; set; }
        public ProductDiscount productDiscount { get; set; }
        public EventLogger eventLogger { get; set; }
        public PaymentTxn PaymentTxn { get; set; }
        public List<CostCentre> costCentres { get; set; }
        public List<RemarkHistory> WebSecDepRemarks { get; set; }
        public List<CreditLimitHistory> creditLimitHistories { get; set; }
        public List<EventLogger> eventLoggers { get; set; }
        public List<FinancilInfoItemsList> financilInfoItems { get; set; }
        public List<OnlineTransaction> onlineTransactions { get; set; }
        public List<ProductDiscount> productDiscounts { get; set; }
        public List<PaymentTxn> PaymentTxns { get; set; }
        public List<BillingItem> BillingItems { get; set; }
    }
}

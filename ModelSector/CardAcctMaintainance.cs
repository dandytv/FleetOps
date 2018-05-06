﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CCMS.ModelSector;
namespace ModelSector
{

    public class GeneralInfoModel
    {
        [Required]
        [DisplayName(@"Account No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = @"Numbers only")]
        public string AcctNo { get; set; }

        [DisplayName("Plastic Type")]
        public string SelectedPlasticType { get; set; }
        public IEnumerable<SelectListItem> PlasticType { get; set; }
        [DisplayName("Account Name")]
        public string AccountName { get; set; }
        [DisplayName("Company Registration No")]
        public string CmpyRegsNo { get; set; }
        [DisplayName("Company Registration Date")]
        public string RegsDate { get; set; }
        [DisplayName("Standard Industry Code")]
        public string SelectedSIC { get; set; }
        public IEnumerable<SelectListItem> SIC { get; set; }

        [DisplayName("Customer No")]
        public string SapNo { get; set; }

        [DisplayName("Corporate Code")]
        public string SelectedCorpName { get; set; }
        public IEnumerable<SelectListItem> CorpName { get; set; }
        [DisplayName("Client Class")]
        public string SelectedClientClass { get; set; }
        public IEnumerable<SelectListItem> ClientClass { get; set; }

        [DisplayName("Customer Group")]
        public string SelectedCustomerGroup { get; set; }
        public IEnumerable<SelectListItem> CustomerGroup { get; set; }


        [DisplayName("Business Establishment")]
        public string SelectedBusnEstablishment { get; set; }
        public IEnumerable<SelectListItem> BusnEstablishment { get; set; }

        [DisplayName("Source Code")]
        public string SourceCode { get; set; }

        [DisplayName("Source Reference No")]
        public string SourceRefNo { get; set; }

        [DisplayName("Current Status")]
        public string SelectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }

        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }

        [DisplayName("Terminated Date")]
        public string TerminatedDate { get; set; }

        [DisplayName("Blocked Date")]
        public string BlockedDate { get; set; }

        [DisplayName("Reason Codes")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCode { get; set; }

        [DisplayName("Overrides Status Tagged By User Id ")]
        public string OvrStatusTaggedByUserId { get; set; }

        [DisplayName(@"Overrides Account Status ")]
        public string SelectedOvrStatus { get; set; }
        public IEnumerable<SelectListItem> OvrStatus { get; set; }


        [DisplayName("Overrides Account Expiry Date")]
        public string OvrExpDate { get; set; }
        //public IEnumerable<SelectListItem> OvrExpDate { get; set; }

        [DisplayName("Application Id")]
        public string ApplId { get; set; }

        [DisplayName("Application Reference")]
        public string ApplRef { get; set; }

        [DisplayName("Application Creation Date")]
        public string ApplCreationDate { get; set; }
        [DisplayName("Application Transffered Date")]
        public string ApplTransfferedDate { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("Web User Id")]
        public string WebUserId { get; set; }

        [DisplayName("Loyalty Card No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string LoyaltyCardNo { get; set; }
        [DisplayName("Entity Id")]
        public string EntityId { get; set; }

        [Display(Name = "Audit Id")]
        public string AuditId { get; set; }

        [DisplayName("User ID")]
        public string AdminEmail { get; set; }

        [DisplayName("Resend Activation")]
        public string ResendEmail { get; set; }

        public string CmpyName1 { get; set; }
        public IEnumerable<SelectListItem> CompanyType { get; set; }
        [Display(Name = "Company Type")]
        public string SelectedCompanyType { get; set; }

        public string CaptDate { get; set; }
        [Display(Name = "Web Password")]
        public string WebPassword { get; set; }
        public string CustSvcId { get; set; }
        public IEnumerable<SelectListItem> BusnCategory { get; set; }
        [Display(Name = "Business Category")]
        public string SelectedBusnCategory { get; set; }
        public string TaxId { get; set; }
        public IEnumerable<SelectListItem> SaleTerritory { get; set; }
        [Display(Name = "Sales Territory")]
        public string SelectedSaleTerritory { get; set; }
        [Display(Name = "Account Type")]
        public string SelectedAccountType { get; set; }
        public IEnumerable<SelectListItem> AccountType { get; set; }
        [Display(Name = "Cut Off")]
        public string CutOff { get; set; }
        [Display(Name = "Payment Terms")]
        public string PaymentTerms { get; set; }
    }
    public class FinancialInfoModel
    {

        public IEnumerable<SelectListItem> TaxCategory { get; set; }
        [Display(Name = "Tax Category")]
        public string SelectedTaxCategory { get; set; }

        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        [DisplayName("Tax Id")]
        public string TaxId { get; set; }

        [DisplayName("Late Payment Charge")]
        public bool LatePaymtCharge { get; set; }


        [DisplayName("Dunning Code")]
        [RegularExpression(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Dunning Code limit is 255")]
        public string SelectedDunningCd { get; set; }
        public IEnumerable<SelectListItem> DunningCd { get; set; }

        [DisplayName("Cr Allowance Factor (%)")]
        //decimalvalidationbug
        public string CredtAllowanceFact { get; set; }

        [DisplayName("Accrued Interest")]
        //decimalvalidationbug
        public string AccrdInterest { get; set; }

        [DisplayName("Accrued Credit Usage")]
        //decimalvalidationbug
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string AccrdCrdtUsg { get; set; }

        [DisplayName("Prompt Payment Rebate (%)")]
        //decimalvalidationbug
        public string PromPaymtRebate { get; set; }

        [DisplayName("PPR Grace Period")]
        [RegularExpression(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$", ErrorMessage = "PPR Grace Period limit is 255")]
        public int? PPRGracePeriod { get; set; }

        [DisplayName("PPR Expiry")]
        public string pprExpiry { get; set; }


        [DisplayName("Litre Limit Per Transaction")]
        //decimalvalidationbug
        public string LitreLimitPerTxn { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#####.##$}")]
        [DisplayName("Amount Limit Per Transaction")]
        //decimalvalidationbug
        public string AmtLimitPerTxn { get; set; }

        [DisplayName("Cycle No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string SelectedCycNo { get; set; }
        public IEnumerable<SelectListItem> CycNo { get; set; }

        [DisplayName("Statement Type")]
        public string SelectedStmtType { get; set; }
        public IEnumerable<SelectListItem> StmtType { get; set; }


        [DisplayName("Invoice Preference")]
        public string SelectedStmtInd { get; set; }
        public IEnumerable<SelectListItem> StmtInd { get; set; }

        [DisplayName("Last Statement Date")]
        public string StmtDate { get; set; }


        [DisplayName("Payment Mode")]
        public string SelectedPaymtMethod { get; set; }
        public IEnumerable<SelectListItem> PaymtMethod { get; set; }

        [DisplayName("Payment Term")]
        [RegularExpression(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Payment Term limit is 255")]
        public int? PaymtTerm { get; set; }

        [DisplayName("Grace Period")]
        [RegularExpression(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Grace Period limit is 255")]
        public int? GracePeriod { get; set; }

        [DisplayName("Direct Debit Indicator")]
        public bool DirectDebitInd { get; set; }



        [DisplayName("Bank Account Type")]
        public string SelectedBankAcctType { get; set; }
        public IEnumerable<SelectListItem> BankAcctType { get; set; }

        [DisplayName("Bank Name")]
        public string selectedBankName { get; set; }

        public IEnumerable<SelectListItem> BankName { get; set; }

        [DisplayName("Bank Account No")]
        public string BankAcctNo { get; set; }

        [DisplayName("Bank Branch Cd")]
        public string BankBranchCD { get; set; }



        [DisplayName("VAT Rate")]
        //decimalvalidationbug
        public string VATRate { get; set; }

        [DisplayName("Writeoff Date")]
        public string WriteoffDate { get; set; }

        [DisplayName("Last Payment Type")]
        public string LastPaymtType { get; set; }

        [DisplayName("Last Payment Received")]
        //decimalvalidationbug
        public string LastPaymtReceived { get; set; }

        [DisplayName("Last Payment Date")]
        public string LastPaymtDate { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        public SKDS _skds { get; set; }
        public CreditAssesOperation _creditAssesOperation { get; set; }
        public UpToDateBal _upToDateBal { get; set; }
        public MinRepaymtAgingList _minRepaymtAgingList { get; set; }
        public WithheldTxnList _withheldTxnList { get; set; }
        public UnsettleTxnList _unsettleTxnList
        {
            get;
            set;
        }

        [DisplayName("Invoice Billing Indicator")]
        public bool InvoiceBillingIndicator { get; set; }
        [DisplayName("Payment Advice Billing Indicator")]
        public bool PayAdviceBillingIndicator { get; set; }
        [DisplayName("Copy of Invoice Indicator")]
        public bool InvoiceIndCopy { get; set; }
        [DisplayName("Vehicle Performance Report")]
        public bool VehiclePerformanceReportInd { get; set; }
        [Display(Name = "Withholding Tax Indicator")]
        public bool WithholdingTaxInd { get; set; }

        public IEnumerable<SelectListItem> ProdRebateType { get; set; }
        public string SelectedProdRebateType { get; set; }
        public string ProdRebateRate { get; set; }
        public IEnumerable<SelectListItem> RiskCategory { get; set; }
        [DisplayName("Risk Category")]
        public string SelectedRiskCategory { get; set; }
        public IEnumerable<SelectListItem> AssessmentType { get; set; }
        [DisplayName("Assessment Type")]
        public string SelectedAssessmentType { get; set; }
        [DisplayName("Credit Limit")]
        public string CreditLimit { get; set; }
        [DisplayName("Payee Code")]
        public string PayeeCd { get; set; }
    }
    public class CreditAssesOperation
    {
        public string ApplId { get; set; }
        public string acctNo { get; set; }
        [DisplayName("Territory Code")]
        public string SelectedTerritoryCd { get; set; }
        public IEnumerable<SelectListItem> TerritoryCd { get; set; }
        [DisplayName("Risk Category")]
        public string SelectedRiskCategry { get; set; }
        public IEnumerable<SelectListItem> RiskCategory { get; set; }

        [DisplayName("Assessment Type")]
        public string SelectedAssesmtType { get; set; }
        public IEnumerable<SelectListItem> AssesmtType { get; set; }

        [DisplayName("Bank Account Type")]
        public string SelectedBankAcctType { get; set; }
        public IEnumerable<SelectListItem> BankAcctType { get; set; }

        [DisplayName("Bank Name")]
        public string SelectedBankName { get; set; }
        public IEnumerable<SelectListItem> BankName { get; set; }
        [DisplayName("Bank Account No")]
        public string BankAcctNo { get; set; }
        [DisplayName("BG Serial No")]
        public string BankBranchCode { get; set; }
        [DisplayName("Credit Limit")]
        //decimalvalidationbug
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string CreditLimit { get; set; }

        [DisplayName("Line Of Credit Provider")]
        public string LineOfCredtProvider { get; set; }
        [DisplayName("Deposit Exp")]
        public string DepositExp { get; set; }
        [DisplayName("Direct Debit Indicator")]
        public bool DirectDebitInd { get; set; }
        public string SelectedDirectDebitInd { get; set; }
        [DisplayName("Deposit Type")]
        public string SelectedDepositType { get; set; }
        public IEnumerable<SelectListItem> DepositType { get; set; }
        [DisplayName("Approval Code")]
        public string SelectedReasonCd { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        [DisplayName("Security Amount")]
        public string SecurityAmt { get; set; }
        [DisplayName("Deposit Amount")]
        //decimalvalidationbug
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string DepositAmt { get; set; }

        [DisplayName("Validity Date")]
        public string ValidityDate { get; set; }
        [DisplayName("Next Interval Review Date")]
        public string NRID { get; set; }

        [DisplayName("Payment Mode")]
        public string SelectedPaymentMode { get; set; }
        public IEnumerable<SelectListItem> PaymentMode { get; set; }
        public IEnumerable<SelectListItem> PaymentTerm { get; set; }
        [DisplayName("Payment Term")]
        [Range(0, 255)]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string SelectedPaymentTerm { get; set; }
        [Required]
        [DisplayName("GracePeriod")]
        [Range(0, 255)]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? GracePeriod { get; set; }

        [DisplayName("Deposit From Date")]
        public string DepositFromDate { get; set; }


        [DisplayName("Deposit To Date ")]
        public string DepositToDate { get; set; }

        [DisplayName("Deposit Bank Account No")]
        public string DepositBankAcctNo { get; set; }

        [DisplayName("Deposit Bank Branch Cd")]
        public string DepositBankBranchCD { get; set; }

        [DisplayName("Transaction Amount Limit")]
        //decimalvalidationbug
        public string TxnAmtLimit { get; set; }

        [DisplayName("Transaction Litre Limit")]
        //decimalvalidationbug
        public string TxnLitLimit { get; set; }

        [DisplayName("Approved By")]
        public string AppvUserIdEDP { get; set; }

        [DisplayName("Approved Status")]
        public string SelectedAppvStsEDP { get; set; }
        public IEnumerable<SelectListItem> AppvStsEDP { get; set; }

        [DisplayName("Approved Date")]
        public string AppvDateEDP { get; set; }

        [DisplayName("Approved By")]
        public string AppvUserIdCredOff { get; set; }

        [DisplayName("Approved Status ")]
        public string SelectedAppvStsCredOff { get; set; }
        public IEnumerable<SelectListItem> AppvStsCredOff { get; set; }

        [DisplayName("Approved Date")]
        public string AppvDateCredOff { get; set; }

        [DisplayName("Approved By")]
        public string AppvUserIdBackOff { get; set; }


        [DisplayName("Approved Status")]
        public string SelectedAppvStsBackOff { get; set; }
        public IEnumerable<SelectListItem> AppvStsBackOff { get; set; }

        [DisplayName("Approved Date ")]
        public string AppvDateBackOff { get; set; }


        [DisplayName("Approved By ")]
        public string AppvUserIdQAOff { get; set; }


        [DisplayName("Approved Status ")]
        public string SelectedAppvStsQAOff { get; set; }
        public IEnumerable<SelectListItem> AppvStsQAOff { get; set; }

        [DisplayName("Approved Date")]
        public string AppvDateQAOff { get; set; }
        public string DocPath { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Txn Id")]
        public string Txnid { get; set; }
        [DisplayName("Creation Date")]
        public string Creationdt { get; set; }
        [DisplayName("Remarks")]
        public string remarks { get; set; }
        public string BgSerialNo { get; set; }

    }

    public class UpToDateBal
    {
        [DisplayName("Acct Type")]
        public string SelectedAcctType { get; set; }
        public IEnumerable<SelectListItem> AcctType { get; set; }

        [DisplayName("CreditLimit")]
        //decimalvalidationbug
        public string CreditLimit { get; set; }

        [DisplayName("Account Balance")]
        //decimalvalidationbug
        public string OpeningBal { get; set; }

        [DisplayName("Instant Amount")]
        //decimalvalidationbug
        public string InstantAmt { get; set; }

        [DisplayName("Unposted Amount")]
        //decimalvalidationbug
        public string UnpostedAmt { get; set; }

        [DisplayName("Available Credit Limit")]
        //decimalvalidationbug
        public string ClosingBal { get; set; }

        [DisplayName("Opening Points Balance")]
        //decimalvalidationbug
        public string OpeningPtsBal { get; set; }

        [DisplayName("Instant Points")]
        //decimalvalidationbug
        public string InstantPts { get; set; }

        [DisplayName("Unposted Points")]
        //decimalvalidationbug
        public string UnpostedPts { get; set; }


    }

    public class MinRepaymtAgingList
    {
    }

    public class WithheldTxnList
    {
    }

    public class UnsettleTxnList
    {
    }

    public class SKDS
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayName("Application ID")]
        public string ApplId { get; set; }
        [DisplayName("Transaction ID")]
        public string TxnId { get; set; }
        [DisplayName("Diesel Subsidy No")]
        [Required]
        public string SKDSNo { get; set; }
        [DisplayName(@"Diesel Subsidy Description")]
        public string SksdDesp { get; set; }
        [DisplayName(@"Vehicle Register No")]
        public string VehRegsNo { get; set; }
        //[DisplayName(@"Skds Effective Date")]
        //public string SkdsEffDt { get; set; }
        [DisplayName(@"Account Subsidy Tag")]
        public string SelectedSkdsAcctSub { get; set; }
        public IEnumerable<SelectListItem> SkdsAcctSub { get; set; }
        //[DisplayName(@"Skds End Date")]
        //public string SkdsEndDt { get; set; }
        [DisplayName("Quota From Date")]
        public string QuotaFromDate { get; set; }
        [DisplayName("Quota To Date")]
        public string QuotaToDate { get; set; }
        [DisplayName("Last Subsidy Date")]
        public string LastSubsidyDate { get; set; }

        [DisplayName("Agreement From Date")]
        public string EffFromDate { get; set; }
        [DisplayName("Agreement To Date")]
        public string EffToDate { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Diesel Subsidy Litre Quota")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string SKDSLitreQuota { get; set; }
        //public string ShownSKDSLitreQuota { get; set; }
        [DisplayName("Refference/BRN")]
        public string Refference { get; set; }
        [DisplayName("Last Update Date")]
        public string LastUpdDate { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Diesel Subsidy Rate")]
        //decimalvalidationbug
        public string SKDSRate { get; set; }
        [DisplayName("Diesel Subsidy Quota")]
        public string SKDSQuota { get; set; }
        [DisplayName("Diesel Subsidy Doc Xref")]
        public string SKDSDocXref { get; set; }
        [DisplayName("Diesel Subsidy Ref")]
        public string SKDSRef { get; set; }
        [DisplayName("Tag Status")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        [DisplayName("Card Status")]
        public string CardSts { get; set; }

        [DisplayName("Card No")]
        public string CardNo { get; set; }
    }

    public class VeloctyLimitListMaintModel
    {
        public CardnAccNo _CardnAccNo { get; set; }

        [DisplayName("Reference Key")]
        public string RefKey { get; set; }

        //[DisplayName("Velocity Type")]
        //public string SelectedVelocityType { get; set; }
        //public IEnumerable<SelectListItem> VelocityType { get; set; }
        [DisplayName("Velocity Counter")]
        public string velocityCounter { get; set; }
        [DisplayName("Product Category")]
        public string SelectedProdCd { get; set; }
        public IEnumerable<SelectListItem> ProdCd { get; set; }

        public string ProdCdDescp { get; set; }

        [DisplayName("Velocity Litre")]
        //decimalvalidationbug
        public string VelocityLitre { get; set; }
        [DisplayName("Velocity Litre")]
        public string ddlVelocityLitre { get; set; }
        [DisplayName("Velocity Amount")]
        //decimalvalidationbug
        public string VelocityLimit { get; set; }
        [DisplayName("Velocity Limit")]
        public string ddlVelocityLimit { get; set; }


        [DisplayName("Counter Limit")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? CntrLimit { get; set; }

        [DisplayName("Spent Counter ")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? SpentCnt { get; set; }
        [DisplayName("Spent Amount")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string SpentLimit { get; set; }
        [DisplayName("Spent Litre")]
        //decimalvalidationbug
        public string SpentLitre { get; set; }

        [DisplayName("Last Update Date")]
        public string LastUpdateDate { get; set; }

        [DisplayName("User Id")]
        public string UserId { get; set; }

        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }

        [DisplayName("Application Id")]
        public string ApplId { get; set; }

        [DisplayName("Applicant Id")]
        public string AppcId { get; set; }



        [DisplayName("Velocity Indicator")]
        public string SelectedVelocityInd { get; set; }
        public IEnumerable<SelectListItem> VelocityInd { get; set; }

        public string VelocityIndDescp { get; set; }

        [DisplayName("Collateral Type")]
        public string SelectedCtrlType { get; set; }
        public IEnumerable<SelectListItem> CtrlType { get; set; }

        [DisplayName("Cost Centre")]
        public string CostCentre { get; set; }


    }



    public class CardHolderInfoModel
    {
        public string[] ExcelHeader
        {

            get
            {
                return new string[] { "CardNo", "Emboss Name", "Current Status", "Card Expiry", "XRef CardNo", "CardType", "Pin Indicator", "Vehicle Reg.No", "SKDS No", " Driver Code", "Full Name", "Blocked Date", "Terminated Date", "Cost Centre" };
            }
        }
        public string[] ExcelBody()
        {
            //  x.CardNo, x.EmbossName, x.SelectedCurrentStatus, x.CardExpiry, x.XRefCardNo, x.SelectedCardType, x.SelectedPINInd, x.vehRegNo, x.SKDSNo, x.DriverCd, x.FullName, x.BlockedDate, x.TerminatedDate, x.SelectedCostCentre
            return new string[] { CardNo, EmbossName, SelectedCurrentStatus, CardExpiry, XRefCardNo.ToString(), SelectedCardType, SelectedPINInd, vehRegNo, SelectedSKDSNo, DriverCd, FullName, BlockedDate, TerminatedDate, SelectedCostCentre };
        }
        [DisplayName("Card No")]
        [MinLength(16)]
        [MaxLength(19)]
        public string CardNo { get; set; }

        public string AcctNo { get; set; }
        [DisplayName("Member Since")]
        public string MemberSince { get; set; }

        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }

        [DisplayName("Next CardNo")]
        public Int64 XRefCardNo { get; set; }

        [DisplayName("Prev CardNo")]
        public string oldCardNo { get; set; }
        [DisplayName("Plastic Type")]
        public string SelectedPlasticType { get; set; }
        public IEnumerable<SelectListItem> PlasticType { get; set; }
        [DisplayName("Card Type")]
        public string SelectedCardType { get; set; }
        public IEnumerable<SelectListItem> CardType { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [DisplayName("Current Status")]
        public string SelectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }


        [DisplayName("Emboss Name")]
        public string EmbossName { get; set; }

        [DisplayName("Reason Code")]
        public string SelectedReasonCd { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        [DisplayName("PIN Indicator")]
        public string SelectedPINInd { get; set; }
        public IEnumerable<SelectListItem> PINInd { get; set; }

        [DisplayName("PVV")]
        public string PVV { get; set; }

        [DisplayName("PIN Offset")]
        public string PINOffset { get; set; }

        [DisplayName("Push Alert Indicator")]
        public bool SelectedPushAlertInd { get; set; }


        [DisplayName("Dialogue Indicator")]
        public string SelectedDialogueInd { get; set; }
        public IEnumerable<SelectListItem> DialogueInd { get; set; }


        [DisplayName("Vehicle Registration No")]
        public string vehRegNo { get; set; }


        public IEnumerable<SelectListItem> VehicleModel { get; set; }
        [Display(Name = "Vehicle Model")]
        public string SelectedVehicleModel { get; set; }

        [DisplayName("Driver Code")]
        public string DriverCd { get; set; }
        [DisplayName("Driver Name")]
        public string DriverName { get; set; }

        [DisplayName("Location Indicator")]
        public bool LocInd { get; set; }
        [DisplayName("Location Check Flag")]
        public bool SelectedLocCheckFlag { get; set; }

        [DisplayName("AnnualFee")]
        public string SelectedAnnualFee { get; set; }
        public IEnumerable<SelectListItem> AnnualFee { get; set; }

        [DisplayName("Joining Fee")]
        public string SelectedJonFee { get; set; }
        public IEnumerable<SelectListItem> JonFee { get; set; }


        [DisplayName("Max Count Limit")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int MaxCountLimit { get; set; }
        [DisplayName("Amount Limit ")]
        //decimalvalidationbug
        public string AmtLimit { get; set; }

        [DisplayName("Fuel Check Flag")]
        public bool SelectedFuelCheckFlag { get; set; }

        [DisplayName("Fuel Indicator")]
        public bool SelectedFuelInd { get; set; }


        [DisplayName("Renewal Indicator")]
        public string SelectedRenewalInd { get; set; }
        public IEnumerable<SelectListItem> RenewalInd { get; set; }


        [DisplayName("Fuel Litre/ per KM ")]
        //decimalvalidationbug
        public string FuelLitre { get; set; }

        [DisplayName("Max PIN Tries")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? PINExceedCnt { get; set; }

        [DisplayName("PIN Failed Attempted")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? PINAttempted { get; set; }

        [DisplayName("Entity Id")]
        public string EntityId { get; set; }
        [DisplayName("Card Expiry")]
        public string CardExpiry { get; set; }
        [DisplayName("Terminated Date")]
        public string TerminatedDate { get; set; }

        [DisplayName("Blocked Date")]
        public string BlockedDate { get; set; }

        //[DisplayName("Terminated Date")]
        //public string TerminatedDate { get; set; }

        //[DisplayName("Terminated Date")]
        //public string TerminatedDate { get; set; }

        [DisplayName("SKDS Quota")]
        //decimalvalidationbug
        public string SKDSQuota { get; set; }
        [DisplayName("SKDS Indicator")]
        public bool SelectedSKDSInd { get; set; }
        [DisplayName("SKDS No")]
        public string SelectedSKDSNo { get; set; }
        public IEnumerable<SelectListItem> SKDSNo { get; set; }
        [DisplayName("Odometer Indicator")]
        public bool OdometerIndicator { get; set; }
        public PersonInfoModel _PersoninfoModel { get; set; }
        [Display(Name = "Product Utilization")]
        public string SelectedProductUtilization { get; set; }
        public IEnumerable<SelectListItem> ProductUtilization { get; set; }
        [Display(Name = "Primary Card")]
        public bool PrimaryCard { get; set; }

        public string SelectedAnnualFeeCd { get; set; }

        [Display(Name = "Cost Centre")]
        public string SelectedCostCentre { get; set; }
        public IEnumerable<SelectListItem> CostCentre { get; set; }

        [Display(Name = "Branch Code")]
        public string SelectedBranchCd { get; set; }
        public IEnumerable<SelectListItem> BranchCd { get; set; }

        [Display(Name = "Division Code")]
        public string SelectedDivisionCode { get; set; }
        public IEnumerable<SelectListItem> DivisionCode { get; set; }

        [Display(Name = "Department Code")]
        public string SelectedDeptCd { get; set; }
        public IEnumerable<SelectListItem> DeptCd { get; set; }

        [Display(Name = "Product Group")]
        public string SelectedProductGroup { get; set; }
        public IEnumerable<SelectListItem> ProductGroup { get; set; }
    }

    public class PersonInfoModel
    {
        [DisplayName("Entity Id")]
        public string EntityId { get; set; }
        [DisplayName("Title")]
        public string SelectedTitle { get; set; }
        public IEnumerable<SelectListItem> title { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Identity No")]
        public string IdNo { get; set; }
        [DisplayName("National Identity Type")]
        public string SelectedIdType { get; set; }
        public IEnumerable<SelectListItem> IdType { get; set; }
        [DisplayName("Alternate Identity No")]
        public string AltIdNo { get; set; }
        [DisplayName("Alternate Identity Type")]
        public string SelectedAltIdType { get; set; }
        public IEnumerable<SelectListItem> AltIdType { get; set; }
        [DisplayName("Gender")]
        public string Selectedgender { get; set; }
        public IEnumerable<SelectListItem> gender { get; set; }
        [DisplayName("DOB")]
        public string DOB { get; set; }
        [DisplayName("Annual Income")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string AnnualIncome { get; set; }
        [DisplayName("Occupation")]
        public string SelectedOccupation { get; set; }
        public IEnumerable<SelectListItem> Occupation { get; set; }
        [DisplayName("Department")]
        public string SelectedDeptId { get; set; }
        public IEnumerable<SelectListItem> DeptId { get; set; }
        [DisplayName("Driving License")]
        public string DrivingLicense { get; set; }

    }

    public class VehiclesListModel
    {
        // x.CardNo, x.VehRegtNo, x.SelectedVehModel, x.VehRegDate, x.SelectedVehType, x.OdoMeterReading, x.OdoMeterUpdate, x.SelectedSts, x.PolicyExpDate, x.XrefCardNo, x.SelectedCardType, x.SkdsQuota
        public string[] ToCsv()
        {
            return new string[] { CardNo, VehRegtNo, SelectedVehModel, VehRegDate, SelectedVehType, OdoMeterReading, OdoMeterUpdate, SelectedSts, PolicyExpDate, XrefCardNo, SelectedCardType, SkdsQuota };
        }
        public string[] CsvHeader()
        {
            return new string[] { "Card No", "Vehicle Regn.No", "Vehicle Model", "Vehicle Regn.Date", "Vehicle Type", "Odometer Reading", "Odometer Update", "Status", "Policy Expiry Date", "XRef Card No", "Card Type", "SKDS Quota" };
        }

        [DisplayName("Card No")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "Card No Range = 16 to 19 digit")]
        public string CardNo { get; set; }
        [DisplayName("Card Type")]
        public string SelectedCardType { get; set; }
        public IEnumerable<SelectListItem> CardType { get; set; }
        [DisplayName("Card Terminated")]
        public string CardTerminated { get; set; }
        [DisplayName("Card Expiry")]
        public string CardExpiry { get; set; }
        [DisplayName("Vehicle Registration No")]
        public string VehRegtNo { get; set; }
        [DisplayName("Vehicle Maker")]
        public string SelectedVehMaker { get; set; }
        public IEnumerable<SelectListItem> VehMaker { get; set; }
        [DisplayName("Vehicle Model")]
        public string SelectedVehModel { get; set; }
        public IEnumerable<SelectListItem> VehModel { get; set; }
        [DisplayName("Vehicle Registration Date")]
        public string VehRegDate { get; set; }
        [DisplayName("Vehicle Year")]
        public string SelectedVehYr { get; set; }
        public IEnumerable<SelectListItem> VehYr { get; set; }
        [DisplayName("Vehicle Type")]
        public string SelectedVehType { get; set; }
        public IEnumerable<SelectListItem> VehType { get; set; }
        [DisplayName("Vehicle Color")]
        public string SelectedVehColor { get; set; }
        public IEnumerable<SelectListItem> VehColor { get; set; }
        [DisplayName("Odometer Reading")]
        public string OdoMeterReading { get; set; }
        [DisplayName("Odometer Update")]
        public string OdoMeterUpdate { get; set; }
        [DisplayName("Road Tax Expiry Date")]
        public string RoadTaxExpDate { get; set; }
        [DisplayName("Road Tax Amount")]
        //decimalvalidationbug
        public string RoadTaxAmt { get; set; }
        [DisplayName("Renewal Period")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? RenewalPeriod { get; set; }
        [DisplayName("Insurer Cd")]
        public string InsurerCd { get; set; }
        [DisplayName("Premium Amount")]
        public string PremiumAmt { get; set; }
        [DisplayName("PIN")]
        public string pin { get; set; }
        [DisplayName("Policy No")]
        public string PolicyNo { get; set; }
        [DisplayName("Policy Expiry Date")]
        public string PolicyExpDate { get; set; }
        [DisplayName("Policy Amount")]
        //decimalvalidationbug
        public string PolicyAmt { get; set; }
        [DisplayName("Status")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        [DisplayName("SKDS Indicator")]
        public bool SkdsInd { get; set; }
        public string RawSKDSInd { get; set; }
        [DisplayName("SKDS Quota")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string SkdsQuota { get; set; }
        [DisplayName("Vehicle Service Date")]
        public string VehicleServiceDate { get; set; }
        //[DisplayName("VRN")]
        //public string VRN { get; set; }
        [DisplayName("Xref CardNo")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "XrefCardNo Range = 16 to 19 digit")]
        public string XrefCardNo { get; set; }
        [DisplayName("Descpription")]
        public string Descp { get; set; }

        public string AppcId { get; set; }


    }

    public class ProdAcceptListModel
    {
        [DisplayName("Reference Key")]
        public string RefKey { get; set; }
        [DisplayName("Product Group")]
        public string ProdGroup { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }

    }

    public class LocationAcceptListModel
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayName("Dealer")]
        public string MerchantId { get; set; }


        [DisplayName("User Id")]
        public string UserId { get; set; }

        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }

        [DisplayName("Region")]
        public List<string> SelectedStates { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        public string s_state { get; set; }

        [DisplayName("Dealer")]
        public string BusnLoc { get; set; }

        [DisplayName("DBA Name")]
        public string DBAName { get; set; }

        [DisplayName("Site ID")]
        public string SiteId { get; set; }

    }

    public class AddrListMaintModel
    {
        [DisplayName("Reference Key")]
        public string SelectedRefCd { get; set; }

        [DisplayName("Address Type")]
        public string SelectedAddrType { get; set; }

        public IEnumerable<SelectListItem> addrtype { get; set; }

        [DisplayName("Main Mailing Indicator")]
        public bool MainMailingInd { get; set; }


        public string SelectedMailInd { get; set; }

        [DisplayName("Address")]
        [StringLength(100, ErrorMessage = "Maximum lengthis 100 characters")]
        public string Address { get; set; }

        [DisplayName("District")]
        [StringLength(100, ErrorMessage = "Maximum lengthis 100 characters")]
        public string District { get; set; }
        [DisplayName("City")]
        [StringLength(100, ErrorMessage = "Maximum lengthis 100 characters")]
        public string City { get; set; }
        [DisplayName("State")]
        public string Selectedstate { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [DisplayName("Region")]
        public string selectedregion { get; set; }
        public IEnumerable<SelectListItem> region { get; set; }
        [DisplayName("Country")]
        public string SelectedCountry { get; set; }
        public IEnumerable<SelectListItem> Country { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        public string RefTo { get; set; }
        public string RefKey { get; set; }
    }

    public class TempCreditCtrlModel
    {
        [DisplayName("Expiry Date")]
        public string ExpDate { get; set; }
        [DisplayName("Allowed Credit Limit")]
        public string AllowCreditLimit { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }


    }

    public class MiscellaneousInfoModel
    {
        [DisplayName("Application Id")]
        public string ApplId { get; set; }
        [DisplayName("Authoriser Name")]
        public string AuthName { get; set; }
        [DisplayName("Designation")]
        public string SelectedDesignation { get; set; }
        public IEnumerable<SelectListItem> Designation { get; set; }
        [DisplayName("Authorization Date")]
        public string AuthDate { get; set; }
    }


    public class ContactLstModel
    {
        [DisplayName("Reference Key")]
        public string RefKey { get; set; }

        [DisplayName("Contact Type")]
        public string SelectedContactType { get; set; }
        //[Required]
        public IEnumerable<SelectListItem> ContactType { get; set; }

        [DisplayName("Reference Code")]
        public string RefCd { get; set; }
        public string RefTo { get; set; }
        [DisplayName("Contact No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string ContactNo { get; set; }
        [DisplayName("Contact Name")]
        [StringLength(150, ErrorMessage = "Maximum lengthis 150 characters")]
        public string ContactName { get; set; }
        [DisplayName("Email Address")]
        [RegularExpression((@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$"), ErrorMessage = "Not a valid Email Address")]
        public string EmailAddr { get; set; }
        [DisplayName("Contact Status")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        public string RawStatus { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Function")]
        public string SelectedOccupation { get; set; }
        public IEnumerable<SelectListItem> Occupation { get; set; }
        public string RawOccupation { get; set; }
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string Fax { get; set; }
    }

    public class ChangeStatus
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayName("Current Status")]
        public string SelectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }
        [DisplayName("Change Status To")]
        public string SelectedChangeStatusTo { get; set; }
        public IEnumerable<SelectListItem> ChangeStatusTo { get; set; }
        [DisplayName("Reason Code")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCode { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("Reference Type")]
        public string SelectedRefType { get; set; }
        public IEnumerable<SelectListItem> RefType { get; set; }
        [DisplayName("Reference Id")]
        public string RefId { get; set; }

        [DisplayName("Merch Acct No")]
        public string MerchAcctNo { get; set; }
        [DisplayName("Dealer")]
        public string BusnLocation { get; set; }
        [DisplayName("Applicant Id")]
        public string AppcId { get; set; }

    }


    public class AcctGuaranteeContructorAcctGuarantee
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayName("Application Id")]
        public string ApplId { get; set; }
        [DisplayName("Page")]
        public char Page { get; set; }
        [DisplayName("Deposit Type")]
        public string DepositType { get; set; }
        [DisplayName("Deposit Amount")]
        //decimalvalidationbug
        public string DepositAmt { get; set; }
        [DisplayName("Bank Name")]
        public string SelectedBankCd { get; set; }
        public IEnumerable<SelectListItem> BankCd { get; set; }
        [DisplayName("Bank Account No")]
        public string BankAcctNo { get; set; }
        [DisplayName("Bank Branch Cd")]
        public string BankBranchCd { get; set; }
        [DisplayName("From Date")]
        public string EffFromDate { get; set; }
        [DisplayName("To Date")]
        public string EffToDate { get; set; }
        [DisplayName("Last Update Date")]
        public string LastUpdDate { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("Txn Id")]
        public string TxnId { get; set; }

    }

    public class AcctPostedTxnSearch
    {
        public string[] ExcelHeader
        {
            get
            {
                return new string[] { "Invoice Date", "Txn Date", "CardNo", "Txn Description", "Txn Amount", "Quantity", "Dealer", "Auth CardNo", "Process Date", "Txn Id", " Receipt Id", "Batch", " Vehicle Reg.No", " Driver Name", " SiteId" };
            }

        }
        public string[] ExcelBody()
        {
            //  x.CardNo, x.EmbossName, x.SelectedCurrentStatus, x.CardExpiry, x.XRefCardNo, x.SelectedCardType, x.SelectedPINInd, x.vehRegNo, x.SKDSNo, x.DriverCd, x.FullName, x.BlockedDate, x.TerminatedDate, x.SelectedCostCentre
            return new string[] { InvoicDt, TxnDate, SelectedCardNo, TxnDesp, TxnAmt, Quantity, Dealer, AuthCardNo, PrcsDate, TxnId, RecieptId, Batch, VehRegNo, DriverName, SiteId };
        }


        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        [DisplayName("Card No")]
        public string SelectedCardNo { get; set; }
        public IEnumerable<SelectListItem> CardNo { get; set; }
        [DisplayName("Transaction Category")]
        public string SelectedTxnCategory { get; set; }
        public IEnumerable<SelectListItem> TxnCategory { get; set; }
        [DisplayName("Transaction Code")]
        public string SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        [DisplayName("Transaction Date")]
        public string TxnDate { get; set; }
        [DisplayName("Transaction Description")]
        public string TxnDesp { get; set; }
        [DisplayName("Transaction Amount")]
        public string TxnAmt { get; set; }
        [DisplayName("Dealer")]
        public string Dealer { get; set; }
        [DisplayName("Term Id")]
        public string TermId { get; set; }
        [DisplayName("Approve Code")]
        public string ApproveCd { get; set; }
        [DisplayName("Authorised Card No")]
        public string AuthCardNo { get; set; }
        [DisplayName("Prcs Date")]
        public string PrcsDate { get; set; }
        [DisplayName("Transaction Id")]
        public string TxnId { get; set; }
        [DisplayName("From Date")]
        public string FromDate { get; set; }
        [DisplayName("To Date")]
        public string ToDate { get; set; }
        [DisplayName("Invoice Date")]
        public string InvoicDt { get; set; }
        [DisplayName("Receipt ID")]
        public string RecieptId { get; set; }
        public string Batch { get; set; }
        public string VehRegNo { get; set; }
        public string DriverName { get; set; }
        public string SiteId { get; set; }
        public string TotalTxnAmt { get; set; }
        public string Quantity { get; set; }


    }
    public class MerchPostedTxnSearch
    {

        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        [DisplayName("Business Location")]
        public string BusnLocation { get; set; }
        [DisplayName("Transaction Code")]
        public string SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        [DisplayName("Transaction Description")]
        public string TxnDesp { get; set; }
        [DisplayName("Transaction Date")]
        public string TxnDate { get; set; }
        [DisplayName("Dealer")]
        public string SelectedDealer { get; set; }
        public IEnumerable<SelectListItem> Dealer { get; set; }
        [DisplayName("Term Batch")]
        public string TermBatch { get; set; }
        [DisplayName("Card No")]
        public string cardNo { get; set; }
        [DisplayName("Transaction Amount")]
        public string TxnAmt { get; set; }

        [DisplayName("Term Id")]
        public string TermId { get; set; }
        [DisplayName("Authorised  No")]
        public string AuthNo { get; set; }
        [DisplayName("Authorised Card No")]
        public string AuthCardNo { get; set; }
        [DisplayName("Prcs Date")]
        public string PrcsDate { get; set; }
        [DisplayName("Transaction Id")]
        public string TxnId { get; set; }

    }

    public class PaymentTxn
    {
        [DisplayName("Txn Id")]
        public string PyTxnId { get; set; }

        public CardnAccNo _CardnAccNo { get; set; }

        [DisplayName("Txn Code")]
        public string SelectedPyTxnCd { get; set; }
        public IEnumerable<SelectListItem> PyTxnCd { get; set; }
        [DisplayName("Txn Type")]
        public string SelectedTxnType { get; set; }
        public IEnumerable<SelectListItem> TxnType { get; set; }
        [DisplayName("Debit Credit Indicator ")]
        public string DbCrInd { get; set; }
        [DisplayName("Txn Date")]
        public string TxnDate { get; set; }
        [DisplayName("Affiliate")]
        public string SelectedAffiliate { get; set; }
        public IEnumerable<SelectListItem> Affiliate { get; set; }
        [DisplayName("Txn Amount")]
        //decimalvalidationbug
        public string TotAmnt { get; set; }
        public string displayTotAmnt { get; set; }

        [DisplayName("Billing Amount")]
        //decimalvalidationbug
        public string BillingTxnAmt { get; set; }

        [DisplayName("Total Point")]
        //decimalvalidationbug
        public string Totpts { get; set; }

        [DisplayName("Account")]
        public string Account { get; set; }
        [DisplayName("Description")]
        public string Descp { get; set; }

        [DisplayName("Invoice No")]
        public string InvoiceNo { get; set; }

        [DisplayName("Collection Id")]
        public string CollectionId { get; set; }

        [DisplayName("Reference Type")]
        public string RefType { get; set; }

        [DisplayName("Reference Id")]
        public string RefId { get; set; }

        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        [DisplayName("Card No")]
        public string CardNo { get; set; }
        public string CreationDate { get; set; }

        [DisplayName("endorsedby")]
        public string EndorsedBy { get; set; }

        [DisplayName("endorseddate")]
        public string EndorsedDate { get; set; }


        [DisplayName("Booking date")]
        public string BookingDt { get; set; }


        [DisplayName("Movement date")]
        public string MovementDt { get; set; }

        [DisplayName("Due date")]
        public string DueDt { get; set; }

        [DisplayName("Balance date")]
        public string BalDate { get; set; }


        [DisplayName("Total O/S Balance")]
        public string TARBal { get; set; }


        [DisplayName("Withheld Unsettle Id")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public Int32 WithHeldUnsettleId { get; set; }

        [DisplayName("Dealer")]
        public string DeftBusnLocation { get; set; }

        [DisplayName("Deft Terminal Id")]
        public string DeftTermId { get; set; }


        [DisplayName("Cheque No")]
        public string CheqNo { get; set; }

        [DisplayName("Status")]
        public string SelectedSts { get; set; }

        public string StsDescp { get; set; }

        public IEnumerable<SelectListItem> Sts { get; set; }
        [DisplayName("Approval Code")]
        public string SelectedAppvCd { get; set; }
        public IEnumerable<SelectListItem> AppvCd { get; set; }


        [DisplayName("Remarks")]
        public string remarks { get; set; }
    }

    public class BillingItem
    {
        [DisplayName("Txn Id")]
        public Int64? TxnId { get; set; }
        public CardnAccNo _CardnAccNo { get; set; }
        public CreationDatenUserId _CreationDatenUserId { get; set; }
        [DisplayName("From Date")]
        public string FromDate { get; set; }
        [DisplayName("To Date")]
        public string ToDate { get; set; }
        [DisplayName("Txn Category")]
        public string SelectedTxnCategory { get; set; }
        public IEnumerable<SelectListItem> TxnCategory { get; set; }
        [DisplayName("Status")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        public string ClosedDate { get; set; }


        [DisplayName("Txn Date")]
        public string TxnDate { get; set; }
        [DisplayName("Booking Date")]
        public string BookingDate { get; set; }
        [DisplayName("Prcs Date")]
        public string PrcsDate { get; set; }
        [DisplayName("Billing Txn Amount")]
        public string BillingTxnAmt { get; set; }
        public string DisplayBillingTxnAmt { get; set; }
        [DisplayName("Description")]
        public string Descp { get; set; }
        [DisplayName("Dealer")]
        public string BusnLocation { get; set; }
        [DisplayName("Terminal Id")]
        public string TermId { get; set; }

        public string TarBalance { get; set; }

        [DisplayName("Settled Amount")]
        public string SettledAmt { get; set; }
        public string DisplaySettledAmt { get; set; }
        [DisplayName("Settled Date")]
        public string SettledDate { get; set; }
        [DisplayName("Document Ref No")]
        public string DocRefNo { get; set; }
        [DisplayName("Txn Code")]
        public string TxnCd { get; set; }
        [DisplayName("Ref Id")]
        public string RefId { get; set; }
        [DisplayName("Due Date")]
        public string DueDate { get; set; }
        public string Level { get; set; }
        public string TotalTxnAmount { get; set; }
        public string TotalSettledAmt { get; set; }
    }



    public class FinancilInfoItemsList
    {
        public string TxnId { get; set; }
        public string Lbe { get; set; }
        public string CardNo { get; set; }
        public string TxnAmt { get; set; }
        public string BookingDate { get; set; }
        public string TxnDate { get; set; }
        public string DueDate { get; set; }
        public string Descp { get; set; }
        public string UserId { get; set; }
        public string CreationDate { get; set; }
        public string RcptNo { get; set; }
        public string TxnCd { get; set; }
        public string ShortDescp { get; set; }
        public string SiteId { get; set; }
        public string DriverCardNo { get; set; }
    }

    public class AccountUser
    {
        public string Username { get; set; }
        public string PrivilegeCd { get; set; }
        public string Status { get; set; }
    }

    public class ResendAccountMail
    {
        public string AcctNo { get; set; }
        public string UserId { get; set; }
        public string ContentId { get; set; }
        public string Name { get; set; }
        public string Bcc { get; set; }
        public string Cc { get; set; }
    }
    public class ProductDiscount
    {
        public IEnumerable<SelectListItem> ProdCd { get; set; }
        [Display(Name = "Product Code")]
        public string SelectedProdCd { get; set; }
        public string ProdCdDescp { get; set; }
        [Display(Name = "Effective From Date")]
        public string EffDateFrom { get; set; }
        [Display(Name = "Effective To Date")]
        public string EffDateTo { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }
        [Display(Name = "Txn Id")]
        public string TxnId { get; set; }
        [Display(Name = "Txn Code")]
        public string TxnCd { get; set; }
        [Display(Name = "Discount Plan")]
        public string SelectedDiscPlan { get; set; }
        [Display(Name = "Amount Plan")]
        public string SelectedRebatePlan { get; set; }
        public IEnumerable<SelectListItem> DiscPlan { get; set; }
        public IEnumerable<SelectListItem> RebatePlan { get; set; }
        public IEnumerable<SelectListItem> ProdDiscType { get; set; }
        [Display(Name = "Discount Type")]
        public string SelectedProdDiscType { get; set; }
        public string ProdDiscDescp { get; set; }
        public IEnumerable<SelectListItem> PlanId { get; set; }//webgetplan
        [Display(Name = "Plan Id")]
        public string SelectedPlanId { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string Id { get; set; }

    }

    public class PointAdjustment
    {

        [Display(Name = "Txn Type")]
        public string TxnType { get; set; }
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }
        [Display(Name = "Card No")]
        public string CardNo { get; set; }
        public string Points { get; set; }
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Display(Name = "Txn Date")]
        public string TxnDate { get; set; }
        [Display(Name = "Due Date")]
        public string DueDate { get; set; }
        [Display(Name = "Booking Date")]
        public string BookingDate { get; set; }
        [Display(Name = "Txn Amount")]
        public string TxnAmt { get; set; }
        [Display(Name = "Cheque No")]
        public int ChequeNo { get; set; }
        [Display(Name = "Approval Code")]
        public string ApprvCd { get; set; }
        [Display(Name = "Receipt No")]
        public string ReceiptNo { get; set; }
        public string ReturnCd { get; set; }
        [Display(Name = "Withhold Unsettle Id")]
        public string WithHeldUnsettleId { get; set; }
        [Display(Name = "Txn Description")]
        public string TxnDescription { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        [Display(Name = "Status")]
        public string SelectedStatus { get; set; }
        [Display(Name = "Txn Code")]
        public string SelectedTxnCd { get; set; }
        [Display(Name = "Txn Id")]
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        public string TxnId { get; set; }
        public string WUId { get; set; }
        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }
    }
    public class CostCentre
    {
        public string[] XlsHeader()
        {
            return new string[] { "Cost Centre", "Description", "Person in Charge" };
        }
        public string[] XlsBody()
        {
            return new string[] { SelectedCostCentre, Descp, PersoninCharge };
        }
        [Display(Name = "Cost Centre")]
        public string SelectedCostCentre { get; set; }
        public IEnumerable<SelectListItem> CostCentreX { get; set; }
        [Display(Name = "Person in Charge")]
        public string PersoninCharge { get; set; }
        [Display(Name = "Description")]
        public string Descp { get; set; }
        public string RefKey { get; set; }
        public string RefTo { get; set; }
    }

}

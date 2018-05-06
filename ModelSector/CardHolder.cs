using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CCMS.ModelSector;
using ModelSector.Helpers;


namespace ModelSector
{

    public class CardAppcInfoModel
    {
        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "DriverNameLbl")]
        [StringLength(50, ErrorMessage = "Maximum lengthis 50 characters")]
        public string DriverName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LocMaxCntLbl")]
        [RegularExpression(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Location max counter limit is 255")]
        public int? LocMaxCnt { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LocMaxAmtLbl")]
        public string LocMaxAmt { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "FuelLitPerKMLbl")]
        public string FuelLitPerKM { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "FuelCheckFlagLbl")]
        public bool FuelCheckFlag { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "FuelIndLbl")]
        public bool FuelInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LocIndLbl")]
        public bool LocInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LocCheckFlagLbl")]
        public bool LocCheckFlag { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedDialogueIndDdl")]
        public string SelectedDialogueInd { get; set; }
        public IEnumerable<SelectListItem> DialogueInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "VehRegNoLbl")]
        public string vehRegNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedVehicleModelDdl")]
        public string SelectedVehicleModel { get; set; }
        public IEnumerable<SelectListItem> VehicleModel { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "PushAlertIndLbl")]
        public bool PushAlertInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SKDSIndLbl")]
        public bool SKDSInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedSKDSNoDdl")]
        public string SelectedSKDSNo { get; set; }
        public IEnumerable<SelectListItem> SKDSNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedCurrentStatusDdl")]
        public string SelectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "EmbossNameLbl")]
        [StringLength(26, ErrorMessage = "Maximum lengthis 26 characters")]
        public string EmbossName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "CardNoLbl")]
        [StringLength(19, MinimumLength = 16, ErrorMessage = "Minimum Length is 16,Maximum Length is 19")]
        public string CardNo { get; set; }

        [Required(ErrorMessage = "Please select card type")]
        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedCardTypeDdl")]
        public string SelectedCardType { get; set; }
        public IEnumerable<SelectListItem> CardType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LitreLimitLbl")]
        public string LitreLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "TxnLimitLbl")]
        public string TxnLimit { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SKDSQuotaLbl")]
        public string SKDSQuota { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "PINExceedCntLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? PINExceedCnt { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "AppcIdLbl")]
        public Int64 AppcId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "AcctNoLbl")]
        public string AcctNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "EntityIdLbl")]
        public string EntityId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "ManufacturerLbl")]
        public string Manufacturer { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "UserIdLbl")]
        public string UserId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedPinIndDdl")]
        public string SelectedPinInd { get; set; }
        public IEnumerable<SelectListItem> PinInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "OdometerReadingIndLbl")]
        public bool OdometerReadingInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedJoiningLbl")]
        public string SelectedJoining { get; set; }
        public IEnumerable<SelectListItem> JoiningFee { get; set;}
        public IEnumerable<SelectListItem> AnnualFee { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedAnnualFeeDdl")]
        public string SelectedAnnualFee { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "PrimaryCardLbl")]
        public string PrimaryCard { get; set; }

        public IEnumerable<SelectListItem> PrimaryApplicantId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedPrimaryApplicantIdDdl")]
        public string SelectedPrimaryApplicantId { get; set; }

        public int PriAppcId { get; set; }

        public string PriSec { get; set; }

        public string JoiningFeeCd { get; set; }

        public string AnnlFeeCd { get; set; }

        public IEnumerable<SelectListItem> ProductUtilization { get; set; }


        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedProductUtilizationDdl")]
        public String SelectedProductUtilization { get; set; }

        public string SelectedAnnualFeeCd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedCostCentreDdl")]
        public string SelectedCostCentre { get; set; }
        public IEnumerable<SelectListItem> CostCentre { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedBranchCdDdl")]
        public string SelectedBranchCd { get; set; }
        public IEnumerable<SelectListItem> BranchCd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedDivisionCodeDdl")]
        public string SelectedDivisionCode { get; set; }
        public IEnumerable<SelectListItem> DivisionCode { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedDeptCdDdl")]
        public string SelectedDeptCd { get; set; }
        public IEnumerable<SelectListItem> DeptCd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedProductGroupDdl")]
        public string SelectedProductGroup { get; set; }
        public IEnumerable<SelectListItem> ProductGroup { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "SelectedCardMediaDdl")]
        public string SelectedCardMedia { get; set; }
        public IEnumerable<SelectListItem> CardMedia { get; set; }
    }

    public class CardFinancialInfoModel
    {
        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "AmountLimitPerTransactionLbl")]
        public string txnLimit { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LitLimitLbl")]
        public string litLimit { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "PINExceedCntLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? PinExceedCnt { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "PushAlertIndLbl")]
        public bool PushAlertInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "PinAttemptedLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? PinAttempted { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "PinTriedUpdDateLbl")]
        public string PinTriedUpdDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LocationMaxCntLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? LocationMaxCnt { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LocationMaxAmtLbl")]
        public string LocationMaxAmt { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LocationCheckFlagLbl")]
        public bool LocationCheckFlag { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LocationIndLbl")]
        public bool LocationInd { get; set; }


        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LocCheckFlagLbl")]
        public bool FuelCheckFlag { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "FuelLitPerKMLbl")]
        public string FuelLitPerKM { get; set; }

        public string ChangePin { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendApplicantSignUp", "LastTxnDateLbl")]
        public string LastTxnDate { get; set; }

    }


}











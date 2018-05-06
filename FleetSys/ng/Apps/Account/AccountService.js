(function () {
    var injectParams = ['$http', 'Utils'];
    var accountFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/Account';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: ApiRef
            })
        };
        getFormData = function (obj, url) {
            var ApiRef = url || Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/FillData?' + params
            })
        };

        getPointBalance = function (obj) {
            return $http({
                method: 'GET',
                url: $rootScope.getRootUrl() + '/Account/LoadPointBalance?' + $.param(obj)
            })
        }
        postGeneralInfo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/GeneralInfoMaint',
                method: 'POST',
                data: obj
            });
        };
        FinancialInfoMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/FinancialInfoMaint',
                method: 'POST',
                data: obj
            });
        };

        //added by max, 20160714 - for collection
        SaveCollectionCaseInfo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/SaveCollectionCaseInfo',
                method: 'POST',
                data: obj
            });
        };

        SaveVelocityLimit = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveVelocityLimit',
                method: 'POST',
                data: obj
            });
        };
        ftVelocityDetail = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftVelocityDetail?' + $.param(obj), //addAPI
                method: 'GET',
            });
        }

        RemoveVelocity = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/DelVelocityLimit?' + $.param(obj),
                method: 'GET',
            })
        }//added

        SaveCreditAssessmentOperation = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/FtCreditApplAssessmentMaint',
                method: 'POST',
                data: obj
            });
        };
        AcctDepositInfoMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/AcctDepositInfoMaint',
                method: 'POST',
                data: obj
            });
        };
        ftGetGetAcctDepositInfoDetail = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/ftGetGetAcctDepositInfoDetail',
                method: 'POST',
                data: obj
            });
        }
        SaveSKDS = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveSKDS',
                method: 'POST',
                data: obj
            });
        }
        ftSKDSDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftSKDSDetail?' + Params,
                method: 'GET'
            });
        }
        ftAddressDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftAddressDetail?' + Params,
                method: 'GET'
            });
        }
        SaveAddress = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveAddress',
                method: 'POST',
                data: obj
            });
        }
        DelAddress = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/DelAddress?' + Params,
                method: 'GET'
            });
        }
        WebGetState = function (obj, url) {
            var ApiRef = url || Api;
            return $http({
                method: 'GET',
                url: $rootScope.getRootUrl() + '/Applications/WebGetState?' + $.param(obj)
            })
        };
        ftContactDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftContactDetail?' + Params,
                method: 'GET'
            });
        }
        SaveContact = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveContact',
                method: 'POST',
                data: obj
            });
        }
        RemoveContact = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/DelContact?' + Params,
                method: 'GET'
            });
        }
        SaveMiscellanious = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveMisecInfo',
                method: 'POST',
                data: obj
            });
        }
        ////////////////////////
        //Vehicles
        /////////////////////////
        ftVehicleDetail = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftVehicleDetail?' + $.param(obj),
                method: 'GET'
            })
        }
        SaveVehicleLimit = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveVehicleLimit',
                method: 'POST',
                data: obj
            });
        }
        //////////////////////
        //Cost Centre
        ///////////////////////
        CostCentreMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/CostCentreMaint',
                method: 'POST',
                data: obj
            });
        }
        FtCostCentreDetail = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/FtCostCentreDetail?' + $.param(obj),
                method: 'GET',
            });
        }
        ////////////////////////////
        //Txn Adjustment
        ///////////////////////////
        ftTxnAdjDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/ftTxnAdjDetail?' + Params,
                method: 'GET'
            });
        }
        SaveTxnAdj = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/SaveTxnAdj',
                method: 'POST',
                data: obj
            });
        }
        DelTxnAdj = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/DelTxnAdj?' + Params,
                method: 'GET'
            });
        }
        ////////////////////////////
        //Txn Adjustment
        ///////////////////////////
        ftPaymentTxnDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/ftPaymentTxnDetail?' + Params,
                method: 'GET'
            });
        }
        ftPaymentTxnMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/ftPaymentTxnMaint',
                method: 'POST',
                data: obj
            });
        }
        ftLocationDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/ftLocationDetail?' + Params,
                method: 'GET'
            });
        }
        SaveLocationAccept = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/SaveLocationAccept',
                method: 'POST',
                data: obj
            });
        }
        DelLocation = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/DelLocation?',
                method: 'POST',
                data: obj
            });
        },
        getBusnLocations = function (data) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/getBusnLocations?' + $.param(data),
                method: 'GET'
            });
        }
        //DelTxnAdj = function (obj) {
        //    var Params = $.param(obj);
        //    return $http({
        //        url: '/Account/DelTxnAdj?' + Params,
        //        method: 'GET'
        //    });
        //}
        ///////////////////////////////////
        //Temporary Credit Control
        //////////////////////////////////
        TempCreditCtrlMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/TempCreditCtrlMaint',
                method: 'POST',
                data: obj
            });
        }
        FtChangedStatusMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/FtChangedStatusMaint',
                method: 'POST',
                data: obj
            });
        }
        //////////////////////////////////////
        ////Account users
        //////////////////////////////////////
        ResendActivationEmail = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/ResendActivationEmail',
                method: 'POST',
                data: obj
            })
        };

        ResetPasswordCounter = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/ResetPasswordCounter',
                method: 'GET',
                params: obj
            })
        };

        ProductDiscountSelect = function (obj) {
            var Qstring = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/ProductDiscountSelect?' + Qstring,
                method: 'GET',
            })
        }
        ProductDiscountMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/ProductDiscountMaint',
                method: 'POST',
                data: obj
            })
        }
        DeleteProductDiscount = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/DeleteProductDiscount',
                method: 'POST',
                data: obj
            })
        }

        /////////////////////////////////////
        ///Points Adjustment
        /////////////////////////////////////
        WebPointAdjustmentSelect = function (obj) {
            var param = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/WebPointAdjustmentSelect?' + param,
                method: 'GET',
            })
        }
        WebPointAdjustmentMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/WebPointAdjustmentMaint',
                method: 'POST',
                data: obj
            })
        }

        PukalMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/SavePukalInfo',
                method: 'POST',
                data: obj
            })
        }

        SaveSKDS = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveSKDS',
                method: 'POST',
                data: obj
            });
        }
        ftSKDSDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftSKDSDetail?' + Params,
                method: 'GET'
            });
        }
        SaveAcctSubsidyInfoList = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/SaveAcctSubsidyInfoList',
                method: 'POST',
                data: obj
            });
        }
        GetPlanIdFromSubsidy = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/PlanIDProdDisc?' + Params,
                method: 'GET'
            });
        }

        WebAcctMilestoneListSelect = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/WebAcctMilestoneListSelect?' + Params,
                method: 'GET'
            });
        }
        //////////////////////////////////////////////////////////////
        //Event Configurations
        //////////////////////////////////////////////////////////////

        WebEventAcctConfSelect = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/WebEventAcctConfSelect?' + Params,
                method: 'GET'
            });
        }
        WebEventAcctRcptListSelect = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/WebEventAcctRcptListSelect?' + Params,
                method: 'GET'
            });
        }

        WebEventAcctConfMaint = function (obj, url) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/WebEventAcctConfMaint',
                method: 'POST',
                data: obj
            })
        },
          WebNtfEvtConfRcptDelete = function (obj) {
              var params = $.param(obj);
              return $http({
                  url: $rootScope.getRootUrl() + '/EventConfiguration/WebNtfEvtConfRcptDelete',
                  data: obj,
                  method: 'POST'
              });
          }


        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            getFormData: getFormData, // fill all the selects and radios
            //////////////////////////////////////////////////////////////
            //Application Index page
            //////////////////////////////////////////////////////////////
            //Application General Info
            /////////////////////////////////////////////////////////////
            getPointBalance: getPointBalance,
            postGeneralInfo: postGeneralInfo,
            FinancialInfoMaint: FinancialInfoMaint,
            SaveCreditAssessmentOperation: SaveCreditAssessmentOperation,
            SaveCollectionCaseInfo: SaveCollectionCaseInfo,
            ///////////////////////////////////////////////////////
            //Velocity Limit
            /////////////////////////////////////////////////////////
            SaveVelocityLimit: SaveVelocityLimit,
            ftVelocityDetail: ftVelocityDetail,
            RemoveVelocity: RemoveVelocity,//added
            ////////////////////////////////////////////////////////
            //Deposit Info
            ///////////////////////////////////////////////////////
            AcctDepositInfoMaint: AcctDepositInfoMaint,
            ftGetGetAcctDepositInfoDetail: ftGetGetAcctDepositInfoDetail,
            ///////////////////////
            // SKDS
            ////////////////////////////////
            ftSKDSDetail: ftSKDSDetail,
            SaveSKDS: SaveSKDS,
            /////////////////////////
            //Address
            ////////////////////////
            ftAddressDetail: ftAddressDetail,
            SaveAddress: SaveAddress,
            ftContactDetail: ftContactDetail,
            RemoveContact: RemoveContact,
            SaveContact: SaveContact,
            DelAddress: DelAddress,
            WebGetState: WebGetState,
            /////////////////////////
            /////Miscellanious
            //////////////////////////////
            SaveMiscellanious: SaveMiscellanious,
            /////////////////////////////
            //Vehicles
            //////////////////////////////
            SaveVehicleLimit: SaveVehicleLimit,
            ftVehicleDetail: ftVehicleDetail,
            ///////////////////////////
            //Cost Centre
            //////////////////////////
            CostCentreMaint: CostCentreMaint,
            FtCostCentreDetail: FtCostCentreDetail,
            //////////////////////////
            //Txn Adjustment
            /////////////////////////
            ftTxnAdjDetail: ftTxnAdjDetail,
            SaveTxnAdj: SaveTxnAdj,
            DelTxnAdj: DelTxnAdj,
            ftPaymentTxnDetail: ftPaymentTxnDetail,
            ftPaymentTxnMaint: ftPaymentTxnMaint,
            ////////////////////////
            //Location Acceptance
            ///////////////////////
            ftLocationDetail: ftLocationDetail,
            SaveLocationAccept: SaveLocationAccept,
            DelLocation: DelLocation,
            getBusnLocations: getBusnLocations,
            ////////////////////////////
            //Temporary Credit Control
            TempCreditCtrlMaint: TempCreditCtrlMaint,
            FtChangedStatusMaint: FtChangedStatusMaint,
            //////////////////////////////
            //Account users////////////////
            /////////////////////////////
            ResendActivationEmail: ResendActivationEmail,
            ResetPasswordCounter: ResetPasswordCounter,
            /////////////////////////////
            //Product Discount
            ////////////////////////////
            ProductDiscountSelect: ProductDiscountSelect,
            ProductDiscountMaint: ProductDiscountMaint,
            DeleteProductDiscount: DeleteProductDiscount,
            ////////////////////////////
            ///Points Adjustment////////
            ///////////////////////////
            WebPointAdjustmentSelect: WebPointAdjustmentSelect,
            WebPointAdjustmentMaint: WebPointAdjustmentMaint,
            PukalMaint: PukalMaint,
            //////////////////////////
            ///SKDS//////////////////
            ////////////////////////
            SaveSKDS: SaveSKDS,
            ftSKDSDetail: ftSKDSDetail,
            SaveAcctSubsidyInfoList: SaveAcctSubsidyInfoList,
            WebAcctMilestoneListSelect: WebAcctMilestoneListSelect,
            GetPlanIdFromSubsidy: GetPlanIdFromSubsidy,
            //////////////////////////
            ///Event Configurations//////////////////
            ////////////////////////
            WebEventAcctConfSelect: WebEventAcctConfSelect,
            WebEventAcctRcptListSelect: WebEventAcctRcptListSelect,
            WebEventAcctConfMaint: WebEventAcctConfMaint,
            WebNtfEvtConfRcptDelete: WebNtfEvtConfRcptDelete
        }
    };

    accountFactory.$inject = injectParams;
    angular.module('CardtrendApp').factory('Api', accountFactory);
}());
(function () {
    var injectParams = ['$http', 'Utils'];
    var CorporateCodeFactory = function ($http, $rootScope) {
        var defaultApi = $rootScope.getRootUrl() + '/CorporateCodes';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: Api
            })
        };
        getFormData = function (obj, Url) {
            var Api = Url || defaultApi;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/FillData?' + params
            })
        };
        postCorporateCode = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/CorporateCodes/SaveCorpAcct',
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
        SaveCreditAssessmentOperation = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveCreditAssessmentOperation',
                method: 'POST',
                data: obj
            });
        };
        SaveAcctDepositInfoOps = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveAcctDepositInfoOps',
                method: 'POST',
                data: obj
            });
        };
        ftGetGetAcctDepositInfoDetail = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftGetGetAcctDepositInfoDetail',
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
        //saveSecurityDeposit = function () {
        //    return $http({
        //        url: '/CorporateCodes/SaveSecurityDeposit',
        //        method: 'POST'
        //    });
        //}
        GetVelocityLimit = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftVelocityDetail?' + $.param(obj),
                method: 'GET'
            });
        }

        RemoveVelocity = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/DelVelocityLimit?' + $.param(obj),
                method: 'GET',
            });
        };

        //////////////////////
        //ProductDiscount
        ///////////////////////
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

        GetPlanIdFromSubsidy = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Account/PlanIDProdDisc?' + Params,
                method: 'GET'
            });
        }

        //////////////////////
        //AccountListing
        ///////////////////////
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


        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            getFormData: getFormData, // fill all the selects and radios
            postCorporateCode: postCorporateCode,
            SaveVelocityLimit: SaveVelocityLimit,
            ftAddressDetail: ftAddressDetail,
            SaveAddress: SaveAddress,
            WebGetState: WebGetState,
            ftContactDetail: ftContactDetail,
            RemoveContact: RemoveContact,
            SaveContact: SaveContact,
            DelAddress: DelAddress,
            GetVelocityLimit: GetVelocityLimit,
            RemoveVelocity: RemoveVelocity,
            SaveAcctDepositInfoOps: SaveAcctDepositInfoOps,
            ftGetGetAcctDepositInfoDetail: ftGetGetAcctDepositInfoDetail,
            ProductDiscountSelect: ProductDiscountSelect,
            ProductDiscountMaint: ProductDiscountMaint,
            DeleteProductDiscount: DeleteProductDiscount,
            GetPlanIdFromSubsidy: GetPlanIdFromSubsidy,
            ResendActivationEmail: ResendActivationEmail,
            ResetPasswordCounter: ResetPasswordCounter
        }
    };

    CorporateCodeFactory.$inject = injectParams;
    angular.module('CardtrendApp').factory('Api', CorporateCodeFactory);
}());
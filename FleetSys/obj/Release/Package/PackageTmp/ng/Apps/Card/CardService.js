(function () {
    var injectParams = ['$http','Utils'];
    var cardFactory = function ($http,$rootScope) {
        var rootUri = $rootScope.getRootUrl(), Api = rootUri + '/Card';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: Api
            })
        };
        getFormData = function (obj, Url) {
            Api = Url || Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/FillData?' + params
            })
        };
        postGeneralInfo = function (obj) {
            return $http({
                url: Api + '/WebCardGeneralInfoMaint',
                method: 'POST',
                data: obj
            });
        };
        postNewCard = function (obj) {
            return $http({
                url: rootUri + '/Applicant/SaveNewCardInfo',
                method: 'POST',
                data: obj
            });
        }
        SaveVelocityLimit = function (obj) {
            return $http({
                url: rootUri + '/Applications/SaveVelocityLimit',//Api + '/SaveVelocityLimit'
                method: 'POST',
                data: obj
            });
        };

        GetVelocityLimit = function (obj) {
            return $http({
                url: rootUri + '/Applications/ftVelocityDetail?' + $.param(obj),//Api + '/ftVelocityDetail?'
                method: 'GET'
            });

        };

        RemoveVelocity = function (obj) {
            return $http({
                url: rootUri + '/Applications/DelVelocityLimit?' + $.param(obj),
                method: 'GET',
            })
        }

        ftFinancialInfoSelect = function (obj) {
            return $http({
                url: Api + '/ftFinancialInfoSelect',
                method: 'GET',
                data: obj
            });
        }

        SaveFinancialInfo = function (obj) {
            return $http({
                url: Api + '/SaveFinancialInfo',
                method: 'POST',
                data: obj
            });
        };

        SaveWebPinReset = function (obj) {
            return $http({
                url: Api + '/SaveWebPinReset',
                method: 'POST',
                data: obj
            });
        };

        ftPersonInfoSelect = function (obj) {
            return $http({
                url: Api + '/ftPersonInfoSelect',
                method: 'GET',
                data: obj
            });
        };

        savePersonInfo = function (obj) {
            return $http({
                url: Api + '/SavePersonInfo',
                method: 'POST',
                data: obj
            });
        }
        ftAddressDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: Api + '/ftAddressDetail?' + Params,
                method: 'GET'
            });
        }
        SaveAddress = function (obj) {
            return $http({
                url: Api + '/SaveAddress',
                method: 'POST',
                data: obj
            });
        }

        DelAddress = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: rootUri + '/Applications/DelAddress?' + Params,
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
                url: Api + '/ftContactDetail?' + Params,
                method: 'GET'
            });
        }
        SaveContact = function (obj) {
            return $http({
                url: Api + '/SaveContact',
                method: 'POST',
                data: obj
            });
        }

        RemoveContact = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: rootUri + '/Applications/DelContact?' + Params,
                method: 'GET'
            });
        }

        SaveChangedStatus = function (obj) {
            return $http({
                url: rootUri + '/Applicant/SaveChangedStatus',
                method: 'POST',
                data: obj
            });
        }
        SaveCardReplacement = function (obj) {
            return $http({
                url: rootUri + '/Card/SaveCardReplacementInfo',
                method: 'POST',
                data: obj
            });
        }
        SaveLocationAccept = function (obj) {
            return $http({
                url: rootUri + '/Account/SaveLocationAccept',
                method: 'POST',
                data: obj
            });
        }
        getBusnLocations = function (data) {
            return $http({
                url: rootUri + '/Account/getBusnLocations?' + $.param(data),
                method: 'GET'
            });
        }

        DelLocation = function (obj) {
            return $http({
                url: rootUri + '/Account/DelLocation?',
                method: 'POST',
                data: obj
            });

        }
        //////////////////////
        //ProductDiscount
        ///////////////////////
        ProductDiscountSelect = function (obj) {
            var Qstring = $.param(obj);
            return $http({
                url: rootUri + '/Account/ProductDiscountSelect?' + Qstring,
                method: 'GET',
            })
        }
        ProductDiscountMaint = function (obj) {
            return $http({
                url: rootUri + '/Account/ProductDiscountMaint',
                method: 'POST',
                data: obj
            })
        }
        DeleteProductDiscount = function (obj) {
            return $http({
                url: rootUri + '/Account/DeleteProductDiscount',
                method: 'POST',
                data: obj
            })
        }

        GetPlanIdFromSubsidy = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: rootUri + '/Account/PlanIDProdDisc?' + Params,
                method: 'GET'
            });
        }

        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            getFormData: getFormData, // fill all the selects and radios
            //////////////////////////////////////////////////////////////
            //Application General Info
            /////////////////////////////////////////////////////////////
            postGeneralInfo: postGeneralInfo,
            postNewCard: postNewCard,
            ftFinancialInfoSelect: ftFinancialInfoSelect,
            SaveFinancialInfo: SaveFinancialInfo,
            ftPersonInfoSelect: ftPersonInfoSelect,
            savePersonInfo: savePersonInfo,
            SaveWebPinReset: SaveWebPinReset,
            ///////////////////////////////////////////////////////
            //Velocity Limit
            /////////////////////////////////////////////////////////
            SaveVelocityLimit: SaveVelocityLimit,
            GetVelocityLimit: GetVelocityLimit,
            RemoveVelocity: RemoveVelocity,
            ////////////////////////////////////////////////////////
            //Address
            ////////////////////////
            ftAddressDetail: ftAddressDetail,
            SaveAddress: SaveAddress,
            DelAddress: DelAddress,
            WebGetState: WebGetState,
            ftContactDetail: ftContactDetail,
            SaveContact: SaveContact,
            RemoveContact: RemoveContact,
            SaveChangedStatus: SaveChangedStatus,
            SaveCardReplacement: SaveCardReplacement,
            SaveLocationAccept: SaveLocationAccept,
            getBusnLocations: getBusnLocations,
            DelLocation: DelLocation,
            ProductDiscountSelect: ProductDiscountSelect,
            ProductDiscountMaint: ProductDiscountMaint,
            DeleteProductDiscount: DeleteProductDiscount,
            GetPlanIdFromSubsidy: GetPlanIdFromSubsidy,
        }
    };

    cardFactory.$inject = injectParams;
    angular.module('CardtrendApp').factory('CardApi', cardFactory);
}());
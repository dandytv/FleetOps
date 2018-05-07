(function () {
    var injectParams = ['$http', 'Utils'];
    var MerchantFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/Merchant';
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
        postGeneralInfo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Merchant/SaveMerchGeneralInfo',
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
        ///////////////////
        //Vehicles
        /////////////////////////
        ftVehicleDetail = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftVehicleDetail?' + $.param(obj),
                method: 'GET'
            });
        }
        SaveVehicleLimit = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveVehicleLimit',
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

        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            getFormData: getFormData, // fill all the selects and radios
            //////////////////////////////////////////////////////////////
            //Application Index page
            //////////////////////////////////////////////////////////////
            //Application General Info
            /////////////////////////////////////////////////////////////
            postGeneralInfo: postGeneralInfo,
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
            /////////////////////////
            //Address
            ////////////////////////
            ftAddressDetail: ftAddressDetail,
            SaveAddress: SaveAddress,
            WebGetState: WebGetState,
            ftContactDetail: ftContactDetail,
            RemoveContact: RemoveContact,
            SaveContact: SaveContact,
            DelAddress: DelAddress,
            FtChangedStatusMaint: FtChangedStatusMaint,
        }
    };

    MerchantFactory.$inject = injectParams;
    angular.module('MerchantApp').factory('Api', MerchantFactory);
}());
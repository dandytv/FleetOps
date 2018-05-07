(function () {
    var injectParams = ['$http', 'Utils'];
    var ApplicationFactory = function ($http, $rootScope) {

        var Api = $rootScope.getRootUrl() + '/Applications';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: Api
            })
        };
        getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/FillData?' + params
            })
        };
        postApplicationGeneralInfo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveApplicationGeneralInfo',
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
        ftVelocityDetail = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftVelocityDetail?' + $.param(obj),
                method: 'GET',
            });
        }

        RemoveVelocity = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/DelVelocityLimit?' + $.param(obj),
                method: 'GET',
            })
        }
        WebMilestoneHistorySelect = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/WebMilestoneHistorySelect?' + $.param(obj),
                method: 'GET',
            })
        }

        ftMilestoneInfo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftMilestoneInfo?' + $.param(obj),
                method: 'GET',
            })
        }

        GetTaskNo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/GetTaskNo?' + $.param(obj),
                method: 'GET',
            })
        }

        SaveMilestone = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveMilestone',
                method: 'POST',
                data: obj
            });
        }
        BindingData = function(tblName,scrollX,Url,func,array)
        {
            return {
                serverSide: true,
                processing: true,
                scrollX: scrollX,
                id: tblName,
                ajax: $rootScope.getRootUrl() + Url,
                edit: {
                    level: 'scope',
                    func: func,
                }
            };
        }
        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            getFormData: getFormData, // fill all the selects and radios
            //////////////////////////////////////////////////////////////
            //Application Index page
            //////////////////////////////////////////////////////////////
            //Application General Info
            /////////////////////////////////////////////////////////////
            postApplicationGeneralInfo: postApplicationGeneralInfo,
            SaveCreditAssessmentOperation: SaveCreditAssessmentOperation,
            ///////////////////////////////////////////////////////
            //Velocity Limit
            /////////////////////////////////////////////////////////
            SaveVelocityLimit: SaveVelocityLimit,
            ftVelocityDetail: ftVelocityDetail,
            RemoveVelocity: RemoveVelocity,
            ////////////////////////////////////////////////////////
            //Deposit Info
            ///////////////////////////////////////////////////////
            SaveAcctDepositInfoOps: SaveAcctDepositInfoOps,
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
            /////////////////////////
            //Milestone
            ////////////////////////
            WebMilestoneHistorySelect: WebMilestoneHistorySelect,
            ftMilestoneInfo: ftMilestoneInfo,
            GetTaskNo: GetTaskNo,
            SaveMilestone: SaveMilestone,
            BindingData: BindingData
        }
    };

    ApplicationFactory.$inject = injectParams;
    angular.module('CardtrendApp').factory('ApplicationApi', ApplicationFactory);
}());
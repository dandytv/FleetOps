(function () {
    var injectParams = ['$http', 'Utils'];
    var ApplicantFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/Applicant';
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
                url: Api + '/SaveApplicantInfo',
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

        GetVelocityLimit = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftVelocityDetail?' + $.param(obj),
                method: 'GET'
            });

        };

        RemoveVelocity = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/DelVelocityLimit?' + $.param(obj),
                method: 'GET',
            });
        };

        SaveFinancialInfo = function (obj) {
            return $http({
                url: Api + '/SaveFinancialInfo',
                method: 'POST',
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
                url: $rootScope.getRootUrl() + '/Applications/DelContact?' + Params,
                method: 'GET'
            });
        }
        SaveChangedStatus = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applicant/SaveChangedStatus',
                method: 'POST',
                data: obj
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
            postGeneralInfo: postGeneralInfo,
            SaveFinancialInfo: SaveFinancialInfo,
            savePersonInfo: savePersonInfo,
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


        }
    };
        ApplicantFactory.$inject = injectParams;
        angular.module('CardtrendApp').factory('Api', ApplicantFactory);
}());
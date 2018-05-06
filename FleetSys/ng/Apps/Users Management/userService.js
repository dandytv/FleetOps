(function () {
    var injectParams = ['$http', 'Utils'];
    var userFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/UserAccess';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: Api
            });
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

        GetUserAccessDetail = function (obj) {
            var params = $.param(obj);
            return $http({
                url: Api + '/GetUserAccessDetail?' + params,
                method: 'GET'
            });
        }
        getModules = function (obj) {
            var params = $.param(obj);
            return $http({
                url: Api + '/ftWebUserAccessLevelList?' + params,
                method: 'GET'
            });
        }

        getPageControls = function (obj) {
            var params = $.param(obj);
            return $http({
                url: Api + '/ftWebGetUserAccessPgNCtrlList',
                method: 'GET',
                params: obj
            });
        }
        SaveUserAccess = function (obj) {
            return $http({
                url: Api + '/SaveUserAccess',
                method: 'POST',
                data: obj
            });
        }


        postControlMap = function (obj) {
            return $http({
                url: Api + '/SaveWebUserAccessLevel',
                method: 'POST',
                data: obj
            });
        }

        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            getFormData: getFormData, // fill all the selects and radios
            GetUserAccessDetail: GetUserAccessDetail,
            getModules: getModules,
            getPageControls: getPageControls,
            //////////////////////////////////////////////////////////////
            //Application Index page
            //////////////////////////////////////////////////////////////
            //Application General Info
            /////////////////////////////////////////////////////////////
            postGeneralInfo: postGeneralInfo,
            postControlMap: postControlMap,
            SaveUserAccess: SaveUserAccess
        }
    };

    userFactory.$inject = injectParams;
    angular.module('userApp').factory('Api', userFactory);
}());
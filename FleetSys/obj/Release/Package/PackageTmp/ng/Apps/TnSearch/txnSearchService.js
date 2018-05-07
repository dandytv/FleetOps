(function () {
    var injectParams = ['$http', 'Utils'];
    var txnSearchFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/TxnSearch';

        var selectData = function () {
            return $http({
                method: 'GET',
                url: ApiRef
            })
        };

        getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/FillData?' + params
            })
        };

        getObjectDtl = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/WebGetObjectDetail?' + params
            })
        };

        return {
            getModel: getFormData,
            getObjectDtl: getObjectDtl
        }
    };

    txnSearchFactory.$inject = injectParams;
    angular.module('txnSearchApp').factory('Api', txnSearchFactory);
}());
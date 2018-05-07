(function () {
    var injectParams = ['$http', 'Utils'];
    var multipleAdjFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + "/MultipleTxn";
        return {
            SaveTxn: function (list, obj) {
                obj.MultipleTxnRecord = list;
                return $http({
                    url: Api + '/ftMultipleAdjMaint',
                    method: 'POST',
                    data: obj
                });
            },
            SaveTxnAdj: function (obj) {
                return $http({
                    url: $rootScope.getRootUrl() + '/Account/SaveTxnAdj',
                    method: 'POST',
                    data: obj
                });
            },
            getTxn: function (obj) {
                return $http({
                    url: Api + '/WebMultiTxnAdjustmentSelect?' + $.param(obj),
                    method: 'GET'
                })

            },

            WebGetGLCode: function (obj) {
                var params = $.param(obj);
                return $http({
                    method: 'GET',
                    url: Api + '/WebGetGLCode?' + params
                });
            }
        }
    };

    multipleAdjFactory.$inject = injectParams;
    angular.module('multipleAdjApp').factory('Invoice', multipleAdjFactory);
}());
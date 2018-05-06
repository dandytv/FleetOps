(function () {
    var injectParams = ['$http', 'Utils'];
    var MerchantMltAdjFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + "/MerchantMultiAdjustment";
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
                    url: Api + '/WebmMerchantMultiTxnAdjustmentSelect?' + $.param(obj),
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

    MerchantMltAdjFactory.$inject = injectParams;
    angular.module('multipleAdjApp').factory('Invoice', MerchantMltAdjFactory);

}());
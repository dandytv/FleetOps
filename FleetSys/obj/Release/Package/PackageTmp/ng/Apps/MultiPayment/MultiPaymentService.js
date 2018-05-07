(function () {
    var injectParams = ['$http', 'Utils'];
    var MultiPaymentFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + "/MultiPayment";
        return {

            SaveTxn: function (list, obj) {
                obj.MultipleTxnRecord = list;
                return $http({
                    url: Api + '/ftMultiplePaymentMaint',
                    method: 'POST',
                    data: obj
                });
            },
            SaveTxnAdj: function (obj) {
                return $http({
                    url: $rootScope.getRootUrl() + '/Account/ftPaymentTxnMaint',
                    method: 'POST',
                    data: obj
                });
            },
            getTxn: function (obj) {
                return $http({
                    url: Api + '/WebMultiPaymentSelect?' + $.param(obj),
                    method: 'GET'
                })

            },

            WebGetGLCode: function (obj) {
                var params = $.param(obj);
                return $http({
                    method: 'GET',
                    url: Api + '/WebGetGLCode?' + params
                });
            },

            PaymentTypeTxnCode: function (obj) {
                var Params = $.param(obj);
                return $http({
                    url: Api + '/PaymentTypeTxnCode?' + Params,
                    method: 'GET'
                });
            },
            WebGetPymtTxnCd: function (obj) {
                var Params = $.param(obj);
                return $http({
                    url: Api + '/WebGetAltPymtTxnCd?' + Params,
                    method: 'GET'
                });
            }

        };
    }

    MultiPaymentFactory.$inject = injectParams;
    angular.module('multiPaymentApp').factory('Invoice', MultiPaymentFactory);
}());
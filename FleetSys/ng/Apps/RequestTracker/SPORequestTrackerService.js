(function () {
    var injectParams = ['$http', 'Utils'];
    var spoRequestTrackerFactory = function ($http, $rootScope) {
        var defaultApi = $rootScope.getRootUrl() + '/RequestTracker';
        var getFormData = function () {
            return $http({
                method: 'GET',
                url: defaultApi
            });
        };
        ftManualSlipBatchDetail = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: defaultApi + '/ftManualSlipBatchDetail?' + params
            });
        }
        SaveManualSlipBatch = function (obj) {
            return $http({
                method: 'POST',
                data: obj,
                url: defaultApi + '/SaveManualSlipBatch'
            });
        }

        BusnLocSiteId = function (obj) {
            var Params = $.param(obj);
            return $http({
                method: 'GET',
                url: defaultApi + '/BusnLocSiteId?' + Params,
            });
        }

        SaveManualSlipEntryTxn = function (obj) {
            var params = $.param(obj);
            return $http({
                method: "POST",
                url: defaultApi + '/SaveManualSlipEntryTxn',
                data: obj
            });
        }

        ftManualSlipTxnDetail = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: defaultApi + '/ftManualSlipEntryTxn?' + params
            });
        }
        ftManualSlipProdDetail = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: defaultApi + '/ftManualSlipProdDetail?' + params
            });
        }
        SaveManualSlipProd = function (obj) {
            return $http({
                method: 'POST',
                data: obj,
                url: defaultApi + '/SaveManualSlipProd'
            });
        }
        WebGetManualTxn = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: defaultApi + '/WebGetManualTxn?' + params
            });
        }
        return {
            getFormData: getFormData, // fill all the selects and radios
            ftManualSlipBatchDetail: ftManualSlipBatchDetail,
            SaveManualSlipBatch: SaveManualSlipBatch,
            BusnLocSiteId: BusnLocSiteId,
            ftManualSlipTxnDetail: ftManualSlipTxnDetail,
            ftManualSlipProdDetail: ftManualSlipProdDetail,
            SaveManualSlipEntryTxn: SaveManualSlipEntryTxn,
            SaveManualSlipProd: SaveManualSlipProd,
            WebGetManualTxn: WebGetManualTxn
        }
    };

    spoRequestTrackerFactory.$inject = injectParams;
    angular.module('RequestTrackerApp').factory('Api', spoRequestTrackerFactory);
}());
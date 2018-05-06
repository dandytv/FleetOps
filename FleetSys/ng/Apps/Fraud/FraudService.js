(function () {
    var injectParams = ['$http', 'Utils'];
    var FraudFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/Fraud';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: ApiRef
            });
        };

        GetFormData = function (obj, url) {
            var ApiRef = url || Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/FillData?' + params
            });
        };

        GetCardNoList_ByAcctNo = function (obj) {
            var params = $.param(obj);
            return $http({
                url: Api + '/GetCardNoList_ByAcctNo?' + params,
                method: 'GET'
            });
        }

        GetFraudCustomerDetailsList = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/GetFraudCustomerDetailsList?' + params
            })
        };

        GetCardDetailsList_ByAcctNoCardNo = function (list, obj) {
            obj.FraudCards = list;
            if (list.length > 0) {
                obj.AcctNo = list[0].AcctNo;
            }
            return $http({
                // url: Api + '/GetCustomerDetailsList_ByAcctNoCardNo?' + params,
                url: Api + '/GetCardDetailsList_ByAcctNoCardNo',
                method: 'POST',
                data: JSON.stringify({ _FraudCardDetailsViewModel: obj })
            });
        }

        SaveFraud = function (custDetails, cardDetails, Incidents) {
            var obj = $.extend(custDetails, cardDetails);
            obj = $.extend(obj, Incidents);
            return $http({
                url: $rootScope.getRootUrl() + '/Fraud/SaveFraud',
                method: 'POST',
                data: obj// cardDetails
            });
        };

        SaveTxn = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Fraud/SaveTxn',
                method: 'POST',
                data: obj
            });
        };

        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            GetFormData: GetFormData, // fill all the selects and radios        
            GetCardNoList_ByAcctNo: GetCardNoList_ByAcctNo,
            GetFraudCustomerDetailsList: GetFraudCustomerDetailsList,
            GetCardDetailsList_ByAcctNoCardNo: GetCardDetailsList_ByAcctNoCardNo,
            SaveFraud: SaveFraud,
            SaveTxn: SaveTxn
        }
    };
    FraudFactory.$inject = injectParams;
    angular.module('FraudApp').factory('Api', FraudFactory);
}());
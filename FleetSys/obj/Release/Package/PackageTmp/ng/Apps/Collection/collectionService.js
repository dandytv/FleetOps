(function () {
    var injectParams = ['$http', 'Utils'];
    var collectionFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/Collection';

        var selectData = function () {
            return $http({
                method: 'GET',
                url: Api
            });
        };

        GetCollAcctInfo = function (obj) {
            var ApiRef = Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/GetCollAcctInfo?' + params
            });
        };

        GetCollFinancialInfo = function (obj) {
            var ApiRef = Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/GetCollFinancialInfo?' + params
            });
        };

        GetCollAgeingHistory = function (obj) {
            var ApiRef = Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/GetCollAgeingHistory?' + params
            });
        };
        GetCollFollowUp = function (obj) {
            var ApiRef = Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/GetCollFollowUp?' + params
            });
        };

        TaskListFillData = function () {
            var ApiRef = Api;
            return $http({
                method: 'GET',
                url: ApiRef + '/TaskListFillData'
            });
        };

        SaveCollectionFollowUp = function (obj) {
            var ApiRef = Api;
            return $http({
                url: ApiRef + '/SaveCollectionFollowUp',
                method: 'POST',
                data: obj
            });
        };
        return {
            selectData: selectData,
            GetCollAcctInfo: GetCollAcctInfo,
            GetCollAgeingHistory: GetCollAgeingHistory,
            GetCollFinancialInfo: GetCollFinancialInfo,
            GetCollFollowUp: GetCollFollowUp,
            TaskListFillData: TaskListFillData,
            SaveCollectionFollowUp: SaveCollectionFollowUp
        }
    };

    collectionFactory.$inject = injectParams;
    angular.module('CardtrendApp').factory('Api', collectionFactory);
}());
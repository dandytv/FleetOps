/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
/// <reference path="account.js" />
angular.module('MerchantApp', ['App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope) {
    $rootScope.tables = [];
})
    .factory('Api', function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/Merchant';
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
                url: Api + '/iFrameMerchGeneralInfoSelect?' + params
            })
        }
        return {
            getFormData: getFormData,
        }
    })
.controller('mainController', function ($scope, $rootScope, Utils, Api) {
    $scope.initController = function (busnLoc) {
        $scope._Object = null;
        $scope.searchOn = false;
        $scope.busnLocation = busnLoc;
        Api.getFormData({ MerchId: $scope.busnLocation }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });

        $scope.dtOptions = {
            serverSide: false,
            processing: true,
            checkBox: false,
            bRetrieve: true,
            "scrollX": true,
            id: 'tblIndex',
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
    }

    $scope.DownloadExcelReport = function () {
        if (!$scope._Object.SelectedTxnType)
            return Utils.PNotify({ flag: 1, Descp: 'Type is required' });

        var obj = {
            BusnLocation: $scope.busnLocation,
            SelectedTxnType: $scope._Object.SelectedTxnType,
            SelectedTxnDateRange: $scope._Object.SelectedTxnDateRange
        };

        location.href = $rootScope.getRootUrl() + "/Merchant/DownloadExcelReport?" + $.param(obj);
    }

    $scope.search = function () {
        $scope.searchOn = true;
        if (!$scope._Object.SelectedTxnType)
            return Utils.PNotify({ flag: 1, Descp: 'Type is required' });
        //        ajax: '/Merchant/iFrameMerchTxnListSelect?AcctNo=' + $.param(obj),
        var obj = {
            BusnLocation: $scope.busnLocation,
            SelectedTxnType: $scope._Object.SelectedTxnType,
            SelectedTxnDateRange: $scope._Object.SelectedTxnDateRange
        };

        var value = angular.extend($scope.dtOptions, {
            serverSide: true,
            ajax: $rootScope.getRootUrl() + '/Merchant/iFrameMerchTxnListSelect?' + $.param(obj),
            destroy: true,
        });

        if ($rootScope.tables[$scope.dtOptions.id]) {
            $rootScope.tables[$scope.dtOptions.id].fnDestroy();
        }

        $scope.$broadcast('updateDataTable', { options: value });
    }
});
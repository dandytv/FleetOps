(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$routeParams', '$timeout', 'Api'];

    var indexController = function ($scope, $rootScope, $location,$routeParams, $timeout, Api) {
        $scope.dtOptions = {
            serverSide: false,
            processing: true,
            scrollX: true,
            retrieve: true,
            id: 'tblSOA1',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            rowClick: function (aData) {
                $rootScope.obj.SelectedStmtDate = aData[1];
                $rootScope.obj.TxnCode = null;
                $scope.$apply();
            },
            "aoColumnDefs": [
                            { "sClass": "text-right", "aTargets": [2, 3, 4, 5, 6, 7, 8, 9, 10] },
            ],
        };
        $scope.EmptyPage = function () {
            $scope._Object = null;
            $rootScope.tables[$scope.dtOptions.id].fnDestroy();
        }

        $scope.SearchSOA = function () {
            var acctNo = $scope._Object.AcctNo;
            if (!acctNo)
                return;
            $rootScope.AcctNo = acctNo;
            Api.WebAcctSOASumm({ acctNo: acctNo }).success(function (data) {
                $scope._Object = data;
                $scope._Object.AcctNo = acctNo;
            });

            var $table = $rootScope.tables[$scope.dtOptions.id];
            var prefix = $rootScope.getRootUrl() + '/SOASummary/WebAcctSOASummList?';
            var queryString = $.param({
                AcctNo: $scope._Object.AcctNo
            });
            var value = angular.extend($scope.dtOptions, {
                serverSide: true, ajax: prefix + queryString, destroy: true,
            });
            $scope.$broadcast('updateDataTable', { options: value });

        }
        $rootScope.obj._type = 'index';
        if ($rootScope.AcctNo) {
            $scope._Object = {
                AcctNo: $rootScope.AcctNo
            }
            $scope.SearchSOA();
        }
        else {
            Api.getFormData({}).success(function (data) {
                $scope._Object = data.Model;
            });
        }

        $scope.$on('indexSelected', function (event, aData) {
            $location.path('/SOATxnCategoryList/' + $scope._Object.AcctNo + '/' + aData[1]);
            $scope.$apply();
        });
    };
    //statementDetail Controller
    var statementDetailController = function ($scope, $rootScope, $location, $routeParams, $timeout, Api) {
        var stmtDate = $routeParams.stmtDate;
        var acctNo = $routeParams.acctNo;
        Api.WebAcctSOASumm({ acctNo: acctNo, stmtDate: stmtDate }).success(function (data) {
            $scope._Object = data;
            $scope._Object.AcctNo = acctNo;
            $scope._Object.SelectedStmtDate = stmtDate;
        });

        $scope.dtSOAOptions2 = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblSOA2',
            edit: {
                level: 'scope',
                func: 'indexSelectedTxnCategory',
            },
            rowClick: function (aData) {
                $rootScope.obj.TxnCode = aData[0];
                $scope.$apply();
            },
            ajax: $rootScope.getRootUrl() + '/SOASummary/WebAcctSOATxnCategoryList?AcctNo=' + $routeParams.acctNo + '&SelectedStmtDate=' + $routeParams.stmtDate,
            "aoColumnDefs": [
                              { "sClass": "text-right", "aTargets": [4, 5, 6] },
            ],
        };

        $scope.$on('indexSelectedTxnCategory', function (event, aData) {
            $location.path('/SOATxnList/' + $routeParams.acctNo + '/' + $routeParams.stmtDate + '/' + aData[0]);
            $scope.$apply();
        });
    };
    //SOATxnList Controller
    var SOATxnListController = function ($scope, $rootScope, $location, $routeParams, $timeout, Api) {
        var stmtDate = $routeParams.stmtDate;
        var acctNo = $routeParams.acctNo;
        var txnCd = $routeParams.TxnCd;
        Api.WebAcctSOASumm({ acctNo: acctNo, stmtDate: stmtDate, txnCd: txnCd }).success(function (data) {
            $scope._Object = data;
            $scope._Object.AcctNo = acctNo;
            $scope._Object.SelectedStmtDate = stmtDate;
            $scope._Object.TxnCode = txnCd;
        });
        var url = $rootScope.getRootUrl() + '/SOASummary/WebAcctSOATxnList?AcctNo=' + $routeParams.acctNo + '&SelectedStmtDate=' + $routeParams.stmtDate + '&TxnCode=' + $routeParams.TxnCd;
        $scope.dtSOAOptions3 = {
            serverSide: true,
            processing: true,
            scrollX: true,
            id: 'tblSOA2',
            edit: {
                level: 'scope',
                func: 'indexTxnCategoryList',
            },
            ajax: url,
            "aoColumnDefs": [
                          { "sClass": "text-right", "aTargets": [11, 12] },
            ],
        };
    };

    //inject service
    indexController.$inject = injectParams;
    statementDetailController.$inject = injectParams;
    SOATxnListController.$inject = injectParams;

    angular.module('cardsApp').controller('indexController', indexController);
    angular.module('cardsApp').controller('statementDetailController', statementDetailController);
    angular.module('cardsApp').controller('SOATxnListController', SOATxnListController);

}());
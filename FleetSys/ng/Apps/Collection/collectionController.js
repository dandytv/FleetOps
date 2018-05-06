(function () {
    var injectParams = ['$scope', '$rootScope', '$location', 'Api', '$timeout', '$routeParams', 'Utils'];
    var collectionFollowUpController = function ($scope, $rootScope, $location, Api, $timeout, $routeParams, Utils) {
        $scope.obj._type = 'DelinqAcct';
        $scope.Prefix = "cfu";
        var eventId = $routeParams.eventId || $rootScope.obj.eventId;
        $scope.collectionNotes = [];
        $scope._Object = {};
        Api.GetCollFollowUp({ EventId: eventId }).success(function (data) {

            $scope._Object = data.Model[0];
            $scope._Selects = data.Selects;
            $scope.collectionNotes = data.Model;
        })

        var scrollDistance = 5;
        var count = 0;
        $scope.saveCollectionFollowUp = function () {
            Utils.InfoNotify();
            Api.SaveCollectionFollowUp({
                EventId: eventId, SelectedCollectionSts: $scope._Object.SelectedCollectionSts,
                SelectedPriority: $scope._Object.SelectedPriority, Remarks: $.trim($scope.NewNote), RecallDate: $scope.NewRecallDate
            }).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                $scope.NewNote = '';
                $scope.NewRecallDate = '';
                Api.GetCollFollowUp({ EventId: eventId }).success(function (data) {
                    $scope._Object = data.Model[0];
                    $scope.collectionNotes = data.Model;
                    //  $scope.collectionNotes1 = data.Model;                
                    //  $.extend($scope.collectionNotes, $scope.collectionNotes1);
                })

            })
        };
    };
    var accountInfoController = function ($scope, $rootScope, $location, Api, $timeout, $routeParams) {
        $scope.obj._type = 'DelinqAcct';
        $scope.Prefix = "cai";
        var acctNo = $routeParams.acctNo || $rootScope.obj.acctNo;
        Api.GetCollAcctInfo({ AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.aaData;
        })
    };
    var financialInfoController = function ($scope, $rootScope, $location, Api, $timeout, $routeParams) {
        $scope.obj._type = 'DelinqAcct';
        $scope.Prefix = "cfi";
        var acctNo = $routeParams.acctNo || $rootScope.obj.acctNo;

        Api.GetCollFinancialInfo({ AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.aaData;
        })

        $timeout(function () {
            $scope.dtOptionsAgeingHistory = {
                serverSide: true,
                processing: true,
                checkBox: false,
                bAutoWidth: false,
                "ordering": false,
                "searching": false,
                "scrollX": true,
                ajax: $rootScope.getRootUrl() + '/Collection/GetCollAgeingHistory?AcctNo=' + acctNo,
            }
        }, 300)

        $timeout(function () {
            $scope.dtOptionsPaymentHistory = {
                serverSide: true,
                processing: true,
                checkBox: false,
                autoWidth: false,
                "ordering": false,
                "searching": false,
                "scrollX": true,
                ajax: $rootScope.getRootUrl() + '/Collection/GetCollPaymentHistory?AcctNo=' + acctNo,
            }
        }, 300)
    };
    var collectionHistoryController = function ($scope, $rootScope, $location, Api, $timeout, $routeParams) {
        $scope.obj._type = 'DelinqAcct';
        var acctNo = $routeParams.acctNo || $rootScope.obj.acctNo;
        var indCloseCase = 'C';
        $timeout(function () {
            $scope.dtOptionsCollectionHist = {
                serverSide: true,
                processing: true,
                checkBox: false,
                autoWidth: false,
                "ordering": false,
                "scrollX": true,
                ajax: $rootScope.getRootUrl() + '/Collection/GetCollHistory?AcctNo=' + acctNo + '&CollectionCaseSts=' + indCloseCase
            }
        }, 300)
    };
    var collectionTaskListController = function ($scope, $rootScope, $location, Api, $timeout, Utils) {
        $scope.obj._type = 'index';
        $scope.modalOpen = {};
        $scope._Object = {};
        Api.TaskListFillData().success(function (data) {
            $scope._Object = data.aaData;
            $scope._Selects = data.Dropdown;
            $scope.currentUser = data.currentUser;
        })

        $scope.loadPendingTask = function () {
            Utils.makeObjectNull($scope._Object, {});
            $scope.dtOptionsPendingTask = {
                serverSide: true,
                id: 'tblPendingTask',
                processing: true,
                checkBox: false,
                destroy: true,
                autoWidth: false,
                "scrollX": true,
                ajax: $rootScope.getRootUrl() + '/Collection/GetPendingAcctCollection',
                edit: {
                    level: 'scope',
                    func: 'indexCollectionSelected'
                }
            }
        }
        $scope.loadPendingTask();

        $scope.SearchPendingTask = function () {
            var prefix = $rootScope.getRootUrl() + '/Collection/GetPendingAcctCollection?';
            var obj = $scope._Object;
            var queryString = $.param({
                AcctNo: obj.AcctNo, SelectedSalesTerritory: obj.SelectedSalesTerritory, SelectedCollectionSts: obj.SelectedCollectionSts,
                RecallFromDate: obj.RecallFromDate, RecallToDate: obj.RecallToDate, CreationFromDate: obj.CreationFromDate,
                CreationToDate: obj.CreationToDate, CororateCode: obj.CororateCode
            });

            var value = angular.extend($scope.dtOptionsPendingTask, {
                serverSide: true, ajax: prefix + queryString, checkBox: false, destroy: true, processing: true, autoWidth: false,
                "drawCallback": function (settings) {
                    $scope.modalOpen.PendingTask = false;
                    $scope.$apply();
                }
            });
            $scope.$broadcast('updateDataTable', { options: value });
        };

        $scope.refreshPendingTask = function () {
            $rootScope.tables[$scope.dtOptionsPendingTask.id].fnDraw();
        };
        $scope.refreshAllTask = function () {
            $rootScope.tables[$scope.dtOptionsAllTask.id].fnDraw();
        };

        $scope.$on("indexCollectionSelected", function (event, aData) {
            $rootScope.obj.acctNo = aData[1];
            $rootScope.obj.eventId = aData[0];
            $location.path('/CollectionFollowUp/' + aData[1] + '/' + aData[0]);
            $scope.$apply();
        });

        $scope.$on("indexThresholdSelected", function (event, aData) {
            window.location.href = $rootScope.urlPrefix + '/Account?id=' + aData[0] + '#/tempCreditControl/' + aData[0];
        });

        $scope.loadAllTask = function () {
            Utils.makeObjectNull($scope._Object, {});
            $timeout(function () {
                $scope.dtOptionsAllTask = {
                    id: 'tblAllTask',
                    serverSide: true,
                    processing: true,
                    checkBox: false,
                    destroy: true,
                    autoWidth: false,
                    "scrollX": true,
                    ajax: $rootScope.getRootUrl() + '/Collection/GetAllAcctCollection',
                    edit: {
                        level: 'scope',
                        func: 'indexCollectionSelected'
                    }
                }
            }, 300)
        }

        $scope.SearchAllTask = function () {
            var prefix = $rootScope.getRootUrl() + '/Collection/GetAllAcctCollection?';
            var obj = $scope._Object;
            var queryString = $.param({
                AcctNo: obj.AcctNo, SelectedSalesTerritory: obj.SelectedSalesTerritory, SelectedCollectionSts: obj.SelectedCollectionSts,
                SelectedOwner: obj.SelectedOwner, RecallFromDate: obj.RecallFromDate, RecallToDate: obj.RecallToDate, CreationFromDate: obj.CreationFromDate,
                CreationToDate: obj.CreationToDate, CororateCode: obj.CororateCode
            });

            var value = angular.extend($scope.dtOptionsAllTask, {
                serverSide: true, ajax: prefix + queryString, checkBox: false, destroy: true, processing: true, autoWidth: false,
                "drawCallback": function (settings) {

                    $scope.modalOpen.AllTask = false;
                    $scope.$apply();
                }
            });
            $scope.$broadcast('updateDataTable', { options: value });
        };

        $scope.loadThresholdLimit = function () {
            $timeout(function () {
                $scope.dtOptionsThresholdLimit = {
                    serverSide: true,
                    id: 'tbThresholdLimit',
                    processing: true,
                    checkBox: false,
                    autoWidth: false,
                    "scrollX": true,
                    ajax: $rootScope.getRootUrl() + '/Collection/GetThresholdLmtCollection',
                    edit: {
                        level: 'scope',
                        func: 'indexThresholdSelected'
                    }
                }
            }, 300)
        }

        $scope.modalClickPendingTask = function () {
            $scope.modalOpen.PendingTask = true;
        }
        $scope.modalClickAllTask = function () {
            $scope.modalOpen.AllTask = true;
        }
    };

    //inject service
    collectionFollowUpController.$inject = injectParams;
    accountInfoController.$inject = injectParams;
    financialInfoController.$inject = injectParams;
    collectionHistoryController.$inject = injectParams;
    collectionTaskListController.$inject = injectParams;

    angular.module('CardtrendApp').controller('collectionFollowUpController', collectionFollowUpController);
    angular.module('CardtrendApp').controller('accountInfoController', accountInfoController);
    angular.module('CardtrendApp').controller('financialInfoController', financialInfoController);
    angular.module('CardtrendApp').controller('collectionHistoryController', collectionHistoryController);
    angular.module('CardtrendApp').controller('collectionTaskListController', collectionTaskListController);

}());
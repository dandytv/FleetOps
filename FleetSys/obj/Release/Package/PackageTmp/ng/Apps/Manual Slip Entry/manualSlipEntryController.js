(function () {
    var injectParam = ['$scope', '$rootScope', '$window','$routeParams','Api','Utils','$http'];
    var mainController = function ($scope, $rootScope, $window, $routeParams, Api, Utils, $http) {
        $http.get($rootScope.getRootUrl() + '/ManualSlipEntry/GetModel').success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });
        $scope.uiState = {
            batchModalOpen: false,
            txnModalOpen: false,
            productModalOpen: false,
            txnTabVisible: true,
            prodTabVisible: false,
            batchEdit: false,
            txnEdit: false,
            productEdit: false
        };
        $scope.dtBatchOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblBatch',
            ajax: $rootScope.getRootUrl() + '/ManualSlipEntry/ftManualSlipBatchList',
            edit: {
                level: 'scope',
                func: 'BatchindexSelected',
            },
            rowClick: function (aData) {
                $rootScope.obj.SettleId = aData[0];
                $rootScope.obj.TxnId = null;
                $scope.$apply();
            }
        };
        $scope.dtTxnOptions = {
            serverSide: false,
            processing: true,
            "scrollX": true,
            id: 'tblTrans',
            destroy: true,
            retrieve: true
        };

        $scope.dtProdOptions = {
            serverSide: false,
            processing: true,
            "scrollX": true,
            id: 'tblProducts',
            destroy: true,
            retrieve: true
        };




        $scope.batchModalClick = function () {
            $scope.uiState.batchEdit = false;
            Utils.makeObjectNull($scope._Object, {});
            $scope.uiState.batchModalOpen = true;
        }
        $scope.$on('BatchindexSelected', function (event, obj) {
            $scope.uiState.batchModalOpen = true;
            $scope.uiState.batchEdit = true;
            Api.ftManualSlipBatchDetail({ SettleId: obj[0] }).success(function (data) {
                $scope._Object = data;
            });
        });
        $scope.SaveManualSlipBatch = function () {
            Utils.InfoNotify();
            Api.SaveManualSlipBatch($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $scope.uiState.batchModalOpen = false;
                    $window.location.reload();
                    $scope.$apply();
                }
            });
        }

        $scope.refreshBatch = function () {
            $rootScope.tables[$scope.dtBatchOptions.id].fnDraw();
            $scope.uiState.prodTabVisible = false;
            //$rootScope.tables[$scope.dtTxnOptions.id].fnDestroy();
            $rootScope.obj.SettleId = null;
            $rootScope.obj.TxnId = null;
        }

        $scope.loadTxn = function () {

            var value = angular.extend($scope.dtTxnOptions, {
                serverSide: true, ajax: $rootScope.obj.SettleId ? $rootScope.getRootUrl() + '/ManualSlipEntry/ftManualSlipEntryTxnList?SettleId=' + $rootScope.obj.SettleId : $rootScope.getRootUrl() + '/ManualSlipEntry/ftManualSlipEntryTxnList',
                edit: {
                    level: 'scope',
                    func: 'txnIndexSelected',
                },
                rowClick: function (aData) {
                    $rootScope.obj.TxnId = aData[19];
                    $scope.uiState.prodTabVisible = true;
                    $scope.$apply();
                }
            });
            $scope.$broadcast('updateDataTable', { options: value });
        }

        $scope.refreshTxn = function () {
            $scope.uiState.prodTabVisible = false;
            $rootScope.tables[$scope.dtTxnOptions.id].fnDraw();
        }

        $scope.$on('txnIndexSelected', function (event, obj) {
            //$scope.uiState.txnEdit = true;
            Api.ftManualSlipTxnDetail({ TxnId: obj[19] }).success(function (data) {
                $scope._Object = data.txn;
                $scope.uiState.txnModalOpen = true;

                Api.BusnLocSiteId({ BusnLocation: $scope._Object.BusnLocation }).success(function (data) {
                    console.log(data);

                    var id = _.map(data.list, function myfunction(item) {
                        return {
                            Text: item.Text,
                            Value: item.Text
                        }
                    });
                    $scope._Selects.TermId = id;
                    $scope._Object.SiteId = data.list[0].Value;
                })
            });
        });

        $scope.txnModalClick = function () {
            Api.WebGetManualTxn({ SettleId: $rootScope.obj.SettleId }).success(function (data) {
                $scope._Object = data;
                if ($scope._Object.InvoiceNo == null)
                    $scope.uiState.txnEdit = false;
                else
                    $scope.uiState.txnEdit = true;
                $scope.TerminalIdChanged();
            })

            $scope.uiState.txnModalOpen = true;
            $scope._Selects.TermId = null;
        }

        $scope.SaveManualSlipTxn = function () {
            Utils.InfoNotify();
            Api.SaveManualSlipEntryTxn({ _ManualSlipEntry: $scope._Object, SettleId: $rootScope.obj.SettleId }).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $scope.uiState.txnModalOpen = false;
                    $rootScope.tables[$scope.dtTxnOptions.id].fnDraw();
                    $rootScope.tables[$scope.dtBatchOptions.id].fnDraw();

                }

            });
        }
        $scope.TerminalIdChanged = function () {
            Api.BusnLocSiteId({ BusnLocation: $scope._Object.BusnLocation }).success(function (data) {
                console.log(data);

                var id = _.map(data.list, function myfunction(item) {
                    return {
                        Text: item.Text,
                        Value: item.Text
                    }
                });
                $scope._Selects.TermId = id;
                $scope._Object.SiteId = data.list[0].Value;
            })
        }

        $scope.loadProducts = function () {
            if (!$rootScope.obj.TxnId)
                return Utils.PNotify({ flag: 1, Descp: 'Transaction not selected' });
            var value = angular.extend($scope.dtProdOptions, {
                serverSide: true, ajax: $rootScope.getRootUrl() + '/ManualSlipEntry/ftManualSlipProdList?TxnId=' + $rootScope.obj.TxnId,
                edit: {
                    level: 'scope',
                    func: 'productIndexSelected',
                },
                aoColumnDefs: [
                       { "bVisible": false, "aTargets": [1] },
                ]
            });
            $scope.$broadcast('updateDataTable', { options: value });
        }

        $scope.refreshProd = function () {
            $rootScope.tables[$scope.dtProdOptions.id].fnDraw();
        }

        $scope.prodModalClick = function () {
            $scope.uiState.productEdit = false;
            Utils.makeObjectNull($scope._Object, {});
            $scope.uiState.productModalOpen = true;
            $scope._Object.SettleId = $rootScope.obj.SettleId;
            $scope._Object.TxnId = $rootScope.obj.TxnId;
            $scope._Object.TxnDetailId = $rootScope.obj.TxnDetailId;

        }

        $scope.$on('productIndexSelected', function (event, obj) {
            $scope.uiState.productModalOpen = true;
            Api.ftManualSlipProdDetail({ SettleId: obj[11], TxnId: obj[12], TxnDetailId: obj[13] }).success(function (data) {
                $scope._Object = data.resultCd;
                $scope._Object.SettleId = $rootScope.obj.SettleId;
                $scope._Object.TxnId = $rootScope.obj.TxnId;
                $scope._Object.TxnDetailId = obj[13];
                //$scope._Object.TxnDetailId = $rootScope.Obj.TxnDetailId;
            });
        });
        $scope.SaveManualSlipProduct = function () {
            Utils.InfoNotify();
            Api.SaveManualSlipProd($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $scope.uiState.productModalOpen = false;
                    $rootScope.tables[$scope.dtProdOptions.id].fnDraw();
                    $rootScope.tables[$scope.dtBatchOptions.id].fnDraw();
                }
            });
        }
    };

    //inject service
    mainController.$inject = injectParam;
    angular.module('manualEntryApp').controller('mainController', mainController);

}());
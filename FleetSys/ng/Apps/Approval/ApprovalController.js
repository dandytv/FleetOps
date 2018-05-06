(function () {
    var injectParamIndex = ['$scope', '$rootScope', '$location','$timeout', 'Api'];
    var injectParamApproval = ['$scope', '$rootScope', '$routeParams', '$location', 'Api', 'Utils'];

    var indexController = function ($scope, $rootScope, $location, $timeout, Api) {
        $rootScope.obj._type = 'index';
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblApplications',
            ajax: $rootScope.getRootUrl() + '/Applications/WebMilestoneListSelect?' + $.param({ Ind: 1 }),//workflowcd: "APPL"
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };

        $scope.loadAdj = function () {
            $timeout(function () {
                $scope.dtOptionsTxnAdj = {
                    serverSide: true,
                    processing: true,
                    checkBox: false,
                    autoWidth: false,
                    "scrollX": true,
                    id: 'tblTxnAdj',
                    ajax: $rootScope.getRootUrl() + '/Applications/WebMilestoneListSelect?' + $.param({ Ind: 1, workflowcd: "Adj" }),
                    edit: {
                        level: 'scope',
                        func: 'AdjindexSelected'
                    }
                }
            }, 200);
        }

        $scope.moveToUrl = function (rul) {
            alert(rul)
            $scope.modalOpen = false;
            $scope._Object.aprId = aprId;
            if (type === 'Adj' || type === 'Pymt') {
                // var url = $scope._Object.Url.replace(/{{0}}/g, AcctNo);
                var url = $scope._Object.Url.replace(/\{0}/g, AcctNo);
            }
            else {
                var url = $scope._Object.Url.replace(/\{0}/g, aprId);
            }
            alert(url);
            location.href = encodeURI(url);
        }

        $scope.loadPymt = function () {
            $timeout(function () {
                $scope.dtOptionsPymtTxn = {
                    serverSide: true,
                    processing: true,
                    checkBox: false,
                    autoWidth: false,
                    "scrollX": true,
                    id: 'tblPymtTxn',
                    ajax: $rootScope.getRootUrl() + '/Applications/WebMilestoneListSelect?' + $.param({ Ind: 1, workflowcd: "Pymt" }),
                    edit: {
                        level: 'scope',
                        func: 'PymtindexSelected'
                    }
                }
            }, 200);

        }

        $scope.$on("indexSelected", function (event, aData) {
            //console.log(aData);
            $location.path("/Approval/Appl/" + aData[0] + '/' + aData[0]);
            $scope.$apply();
        });

        $scope.$on("AdjindexSelected", function (event, aData) {
            //console.log(aData);
            $rootScope.obj.AcctNo = aData[1];
            $location.path("/Approval/Adj/" + aData[0] + '/' + aData[1]);
            $scope.$apply();
        });

        $scope.$on("PymtindexSelected", function (event, aData) {
            //alert('hello world');
            //console.log(aData);
            $rootScope.obj.AcctNo = aData[1];
            $location.path("/Approval/Pymt/" + aData[0] + '/' + aData[1]);
            $scope.$apply();
        });
    };
    var approvalController = function ($scope, $rootScope, $routeParams, $location, Api, Utils) {
        $scope.modalOpen = false;
        var aprId = $routeParams.aprId;
        var type = $routeParams.workflowcd;
        var AcctNo = $routeParams.acctNo;
        Api.getFormData({ Prefix: 'apr' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            // $scope._Object.ApplId = applId;
        });
        Api.WebMilestoneHistorySelect({ RefKey: aprId, workflowcd: type }).success(function (data) {
            $scope.approvals = data.result;
            $scope.currentUser = data.user.toLowerCase();
        });

        $scope.modalClick = function (item) {
            $scope.selectedItem = item;
            $scope._Object.SelectedTaskNo = null;
            $scope.modalOpen = true;
            $scope._Object.selectedOwner = item.selectedOwner;

            Api.GetTaskNo({ CurrentTaskNo: item.SelectedTaskNo, workflowcd: type }).success(function (data) {
                $scope._Selects.TaskNo = data.result;
            });

            Api.ftMilestoneInfo({ SelectedTaskNo: item.SelectedTaskNo, aprId: aprId, workflowcd: type }).success(function (data) {
                $scope._Object = data.result;
                $scope._Object.ID = item.ID;
                //$scope._Object.workflowcd = type;
                //$scope._Object.Url = $scope._Object.Url.replace("#/", "");
                $scope._Object.Url = $scope._Object.Url.replace('/{{0}}/g', aprId);

            });
        };

        $scope.moveToUrl = function (rul) {
            alert(rul)
            $scope.modalOpen = false;
            $scope._Object.aprId = aprId;
            if (type === 'Adj' || type === 'Pymt') {
                // var url = $scope._Object.Url.replace(/{{0}}/g, AcctNo);
                var url = $scope._Object.Url.replace(/\{0}/g, AcctNo);
            }
            else {
                var url = $scope._Object.Url.replace(/\{0}/g, aprId);
                //alert(url);
            }
            //alert(type);
            alert(url);
            location.href = encodeURI(url);
        }

        $scope.saveWorkflow = function () {
            Utils.InfoNotify();
            $scope._Object.aprId = aprId;//$scope._Object.ApplId = applId;
            console.log($scope._Object);
            Api.SaveMilestone($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    // find the item by index and update the status
                    var index = $scope.approvals.indexOf($scope.selectedItem);
                    $scope.selectedItem.selectedStatus = $scope._Object.selectedStatus;
                    $scope.approvals[index] = $scope.selectedItem;

                    Api.WebMilestoneHistorySelect({ RefKey: aprId, workflowcd: type }).success(function (data) {
                        $scope.approvals = data.result;
                        $scope.currentUser = data.user.toLowerCase();
                    });

                }
            });
        };

        $scope.saveWorkflowAdj = function () {
            Utils.InfoNotify();
            $scope._Object.aprId = aprId;//$scope._Object.ApplId = applId;
            console.log($scope._Object);
            Api.SaveMilestoneAdj($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    // find the item by index and update the status
                    var index = $scope.approvals.indexOf($scope.selectedItem);
                    $scope.selectedItem.selectedStatus = $scope._Object.selectedStatus;
                    $scope.approvals[index] = $scope.selectedItem;

                    Api.WebMilestoneHistorySelect({ RefKey: aprId, workflowcd: type }).success(function (data) {
                        $scope.approvals = data.result;
                        $scope.currentUser = data.user.toLowerCase();
                    });

                }
            });
        };

        $scope.saveWorkflowPymt = function () {
            Utils.InfoNotify();
            $scope._Object.aprId = aprId;//$scope._Object.ApplId = applId;
            console.log($scope._Object);
            Api.SaveMilestonePayment($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    // find the item by index and update the status
                    var index = $scope.approvals.indexOf($scope.selectedItem);
                    $scope.selectedItem.selectedStatus = $scope._Object.selectedStatus;
                    $scope.approvals[index] = $scope.selectedItem;

                    Api.WebMilestoneHistorySelect({ RefKey: aprId, workflowcd: type }).success(function (data) {
                        $scope.approvals = data.result;
                        $scope.currentUser = data.user.toLowerCase();
                    });

                }
            });
        };

        $scope.Save = function myfunction() {
            if (type == "Adj") {
                $scope.saveWorkflowAdj();

            } else if (type == "Pymt") {
                $scope.saveWorkflowPymt();
            }
            else {
                $scope.saveWorkflow();
            }
        }
    };
    //inject service
    indexController.$inject = injectParamIndex;
    approvalController.$inject = injectParamApproval;

    angular.module('CardtrendApp').controller('indexController', indexController);
    angular.module('CardtrendApp').controller('approvalController', approvalController);
}());
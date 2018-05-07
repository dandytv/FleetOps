(function () {
    var injectParams = ['$scope', '$routeParams', '$rootScope', '$location', 'Api', 'Utils', '$route', '$compile'];
    var generalInfoController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo || $rootScope.obj.acctNo;

        Api.getFormData({ Prefix: 'gen', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            // temporarily remove
            //if ($scope._Object.LoyaltyCardNo) {
            //    Api.getPointBalance({ AcctNo: $scope._Object.LoyaltyCardNo, RequestorId: "FLT", Token: "" }).success(function (data) {
            //        $scope._Object.PointBalance = data.PointBal;
            //    });
            //}

        });

        $scope.postGeneralInfo = function () {
            Utils.InfoNotify();
            Api.postGeneralInfo($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
            });
        }
    };
    //tempCredit Controller
    var tempCreditController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'tem', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.acctNo = acctNo;
            $scope._Selects = data.Selects;
        });
        $scope.Save = function () {
            Utils.InfoNotify();
            Api.TempCreditCtrlMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            });
        }

        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: true,
            id: 'tblCredCtrl',
            ajax: $rootScope.getRootUrl() + '/Account/WebEventSearchWithoutDate?AcctNo=' + acctNo
        };
    };
    //uptoDateBalace Controller
    var uptoDateBalaceController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.acctNo;
        $scope.instantTxn = 0;

        Api.getFormData({ Prefix: 'upd', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
        });

        $scope.ShowInstantTxn = function () {
            $scope.instantTxn = 1;
            $scope.dtOptions = {
                serverSide: true,
                processing: true,
                scrollX: true,
                id: 'tblInstantTxn',
                ajax: $rootScope.getRootUrl() + '/Account/TxnInstantListSelect?AcctNo=' + acctNo
            };
        }

        $scope.ShowUnpostedTxn = function () {
            $scope.instantTxn = 2;
            $scope.dtOptions2 = {
                serverSide: true,
                processing: true,
                scrollX: true,
                id: 'tblUnpostedTxn',
                ajax: $rootScope.getRootUrl() + '/Account/TxnUnpostedListSelect?AcctNo=' + acctNo
            };
        }

        $scope.ShowOnlineTxnLst = function () {
            $scope.instantTxn = 3;
            $scope.dtOptions3 = {
                serverSide: true,
                processing: true,
                scrollX: true,
                id: 'tblOnlineTxn',
                ajax: $rootScope.getRootUrl() + '/Account/GetOnlineTransactionList?AcctNo=' + acctNo + '&flag=1'
            };
        }

        $scope.ShowOfflineTxnLst = function () {
            $scope.instantTxn = 4;
            $scope.dtOptions4 = {
                serverSide: true,
                processing: true,
                scrollX: true,
                id: 'tblOfflineTxn',
                ajax: $rootScope.getRootUrl() + '/Account/GetOnlineTransactionList?AcctNo=' + acctNo+'&flag=2'
            };
        }

    };
    //statusMaint Controller
    var statusMaintController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'sta', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.AccNo = acctNo;
            $scope._Selects = data.Selects;
        });


        $scope.saveConfirmed = function () {
            $scope.confirmModalOpen = false;
            Utils.InfoNotify();
            Api.FtChangedStatusMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $scope._Object.SelectedCurrentStatus = $scope._Object.SelectedChangeStatusTo;
                }
            });
        }

        $scope.Save = function () {
            if ($scope._Object.SelectedChangeStatusTo == 'T') {
                $scope.confirmModalOpen = true;
            } else {
                $scope.saveConfirmed();
            }
        }
    };
    //finanacialInfo Controller
    var finanacialInfoController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var indOpenCase = 'O';

        Api.getFormData({ Prefix: 'fin', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });
        $scope.FinancialInfoMaint = function () {
            Utils.InfoNotify();
            Api.FinancialInfoMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
            });
        }

        $scope.SaveCollectionCaseInfo = function () {
            Utils.InfoNotify();
            Api.SaveCollectionCaseInfo($scope._Object).success(function (data) {

                $scope.$broadcast('updateDataTable', { options: $scope.dtOptionsCollectionHist });

                Utils.finalResultNotify(data.result);
            });
        }
    };
    //velocity Controller
    var velocityController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo; //$scope.acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var costCentre = $routeParams.cst;
        $scope.modalOpen = false;
        $scope.deleteModalOpen = false; //added
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblVelocity',
            ajax: $rootScope.getRootUrl() + '/Applications/ftVelocityList?' + $.param({ 'AccNo': acctNo, CostCentre: costCentre }), //$scope.acctNo
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            aoColumnDefs: [
            { "bVisible": false, "aTargets": [0] },
            ]
        };
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.ftVelocityDetail({ AccNo: acctNo, SelectedVelocityInd: obj[11], SelectedProdCd: obj[12], CostCentre: costCentre }).success(function (data) {
                $scope._Object = data.velocity;
                $scope._Object.Func = "Upd";
                $scope._Object.AccNo = acctNo;//$scope
                $scope._Object.CostCentre = costCentre;
            });
        });
        Api.getFormData({ Prefix: 'vel', AcctNo: acctNo }).success(function (data) {// AcctNo: $scope.acctNo
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.AccNo = acctNo; //added $scope.
            $scope._Selects = data.Selects;
        });
        $scope.newVelocity = function () {
            $scope.modalOpen = true;
            $scope._Object.Func = "Add";
            Utils.makeObjectNull($scope._Object, { AccNo: acctNo, CostCentre: costCentre });//added
        }
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.SaveVelocityLimit = function () {
            Utils.InfoNotify();
            $scope._Object.AccNo = acctNo; //$scope.
            //$scope._Object.costCentre = CostCentre;
            Api.SaveVelocityLimit($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }
        $scope.delete = function () {
            //delete velocity
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.RemoveVelocity({ AccNo: acctNo, VelInd: obj[11], CostCentre: costCentre }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    //TxnAdjustment Controller
    var TxnAdjustmentController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.acctNo = $routeParams.acctNo || $rootScope.obj.acctNo;
        $scope.modalOpen = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": false,
            id: 'tblftTxnAdjList',
            ajax: $rootScope.getRootUrl() + '/Account/ftTxnAdjList?AcctNo=' + $scope.acctNo,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };

        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { AcctNo: $scope.acctNo, SelectedSts: 'H' });// 
        };


        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.ftTxnAdjDetail({ TxnId: obj[7] }).success(function (data) {
                $scope._Object = data.txn;
            });
        });

        Api.getFormData({ Prefix: 'txn', AcctNo: $scope.acctNo }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.Func = true;
            $scope._Selects = data.Selects;
        });
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.SaveTxnAdjustment = function () {
            Utils.InfoNotify();
            $scope._Object.AccNo = $scope.acctNo;
            Api.SaveTxnAdj($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.txn);
                if (data.txn.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }
        $scope.deleteRecord = function () {
            //delete velocity
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.DelTxnAdj({ AcctNo: $scope.acctNo, CardNo: obj[1], TxnId: obj[7] }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    //paymentTxn Controller
    var paymentTxnController = function ($$scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.modalOpen = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": false,
            id: 'tblPaymentTxn',
            ajax: $rootScope.getRootUrl() + '/Account/ftPaymentTxnList?AcctNo=' + $scope.acctNo,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { AcctNo: $scope.acctNo, SelectedSts: 'H' });
        };

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.ftPaymentTxnDetail({ PyTxnId: obj[5] }).success(function (data) {
                $scope._Object = data.txn;
                $scope._Object.AcctNo = $scope.acctNo;//add
                //$scope._Object.CardNo = $scope.CardNo;//add
            });
        });

        Api.getFormData({ Prefix: 'pay', AcctNo: $scope.acctNo }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.Func = true;
            $scope._Selects = data.Selects;
        });
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.Save = function () {
            Utils.InfoNotify();
            $scope._Object.AccNo = $scope.acctNo;
            Api.ftPaymentTxnMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }
        $scope.deleteRecord = function () {
            //delete velocity
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.DelTxnAdj({ AcctNo: $scope.acctNo, CardNo: obj[1], TxnId: obj[7] }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }


        }
    };
    // locationAcceptance controller
    var locationAcceptanceController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.modalOpen = false;
        $scope.deleteModalOpen = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            checkBox: true,
            "ordering": false,
            id: 'tblLocationAcceptance',
            ajax: $rootScope.getRootUrl() + '/Account/ftLocationList?AcctNo=' + $scope.acctNo,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { AcctNo: $scope.acctNo });
        };

        $scope.RemoveSelectedState = function (state, obj) {
            //item.Value
            var repeats = _.filter($scope._Object.SelectedBusnLocations, function (item) {
                return item.indexOf(state.Value) != -1;
            });
            $scope._Object.SelectedBusnLocations = _.difference($scope._Object.SelectedBusnLocations, repeats);
            _.each($scope._Object.SelectedBusnLocations, function (item) {
                if (item) {
                    if (item.indexOf(state.Value) != -1) {
                        $scope._Object.SelectedBusnLocations.splice($scope._Object.SelectedBusnLocations.indexOf(item), 1);
                    }
                }
            });
            var x = $scope._Object.SelectedBusnLocations;
        }
        $scope.StateSelected = function (item, model) {
            var elements = model.split(/,/g);
            //  elements.push($scope._Object.SelectedStates);
            var element = elements[elements.length - 1];

            Api.getBusnLocations({ States: element, AcctNo: $scope.acctNo, }).success(function (data) {
                var temp = $scope._Object.SelectedBusnLocations;
                $scope._Object.SelectedBusnLocations = null;
                var values = _.map(data, function (dealer) {
                    return dealer.Value;
                });
                if (!temp) {
                    temp = [];
                    temp = values;
                } else {
                    _.each(values, function (str) {
                        temp.push(str);
                    });
                    // $scope._Object.SelectedBusnLocations.concat(values);
                }
                console.log(temp);
                $scope._Object.SelectedBusnLocations = temp;
                $scope._Selects.BusnLocations = data;
            });
        }

        Api.getFormData({ Prefix: 'loc', AcctNo: $scope.acctNo }).success(function (data) {
            //append all selects here...
            data.Model.SelectedStates = [];
            $scope._Object = data.Model;
            $scope._Object.Func = true;
            $scope._Selects = data.Selects;
        });
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.Save = function () {
            Utils.InfoNotify();
            $scope._Object.AccNo = $scope.acctNo;
            Api.SaveLocationAccept($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }

        $scope.deleteRecord = function () {
            //delete velocity
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRows($rootScope.tables[$scope.dtOptions.id]);
            var selectedStates = _.map(obj, function (item) {
                return item[1];
            });
            console.log(selectedStates);
            if (selectedStates) {
                Utils.InfoNotify();
                Api.DelLocation({ AcctNo: $scope.acctNo, BusnLoc: selectedStates }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    // cao controller
    var caoController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'cao', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            console.log(data.Model)
            $scope._Selects = data.Selects;
        });

        $scope.SaveCreditAssessmentOperation = function () {
            $scope._Object.acctNo = acctNo;
            Utils.InfoNotify();
            Api.SaveCreditAssessmentOperation($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                $rootScope.tables[$scope.dtOptions.id].fnDraw();
                $rootScope.tables[$scope.dtRemarksHistoryOptions.id].fnDraw();
                $scope._Object.remarks = '';
            });
        }

        $scope.dtRemarksHistoryOptions = {
            serverSide: true,
            processing: true,
            //searching: false,
            dom: 'lrtip',
            "scrollX": false,
            "scrollCollapse": false,
            id: 'tblRemarksHistory',
            ajax: $rootScope.getRootUrl() + '/Account/WebRemarksHistoryListSelect?acctNo=' + acctNo + '&&eventType=cao',
            aoColumnDefs: [
                  {
                      "aTargets": [0],
                      "mData": [0],
                      "mRender": function (data, type, full) {
                          var strContent = '<div style="float:left;background:#d1d4d7;width:100%;"> <textarea class="form-control ng-pristine ng-valid" style ="width:100%;" rows="3"  maxlength="1000" readonly>' + data + '</textarea></div> ';
                          strContent += '<label style="float:right;font-weight:normal;margin-bottom:-10px;"> Created by ' + full[1] + ' on ' + full[2] + ' </label> </div>'
                          return strContent;
                      }
                  }
            ],
            edit: {
                level: 'scope',
            }
        };

        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblCreditHistory',
            ajax: $rootScope.getRootUrl() + '/Account/WebAcctHistoryListSelect?acctNo=' + acctNo + '&&type=credit',
            edit: {
                level: 'scope',
            }
        };
    };
    // depositInfo controller
    var depositInfoController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.modalOpen = false;
        $scope.show = true;
        Api.getFormData({ Prefix: 'dep', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.acctNo = acctNo;
            $scope._Selects = data.Selects;
        });

        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblDepositInfo',
            ajax: $rootScope.getRootUrl() + '/Account/ftGetAcctDepositInfoList?AcctNo=' + acctNo,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            aoColumnDefs: [
          { "bVisible": false, "aTargets": [6] },
            ]
        };
        $scope.dtSecurityDepositOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblSecurityDeposit',
            ajax: $rootScope.getRootUrl() + '/Account/WebAcctHistoryListSelect?AcctNo=' + acctNo + '&&type=sec',
            edit: {
                level: 'scope',
                func: 'securitySelected'
            }
        };
        $scope.refresh = function myfunction() {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { acctNo: acctNo });
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.ftGetGetAcctDepositInfoDetail({ AccNo: acctNo, TxnId: obj[6] }).success(function (data) {
                $scope._Object = data.Adi;
                $scope._Object.acctNo = acctNo;
                //$scope._Object.remarkHistory[3] = data.Adi.remarkHistory[];
            });
            //console.log($scope._Object);
            //$scope.$apply();
        });
        $scope.SaveAcctDepositInfoOps = function () {
            Utils.InfoNotify();
            Api.AcctDepositInfoMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
                $rootScope.tables[$scope.dtSecurityDepositOptions.id].fnDraw();
            });
        }
    }
    // vehicles controller
    var vehiclesController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.modalOpen = false;
        Api.getFormData({ Prefix: 'veh', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.AccNo = acctNo;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": false,
            id: 'tblVehicles',
            ajax: $rootScope.getRootUrl() + '/Applications/ftVehicleList?AcctNo=' + acctNo,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };
        $scope.refresh = function myfunction() {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.ftVehicleDetail({ AcctNo: acctNo, CardNo: obj[0], VehRegtNo: obj[3] }).success(function (data) {
                $scope._Object = data.vehicle;
            });
        });
        //$scope.modalClick = function () {
        //    $scope.modalOpen = true;
        //    Utils.makeObjectNull($scope._Object, { ApplId: applId });
        //}
        $scope.Save = function () {
            Utils.InfoNotify();
            Api.SaveVehicleLimit($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }
    }
    // skds controller
    var skdsController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.modalOpen = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            ajax: $rootScope.getRootUrl() + '/Applications/ftSKDSList?AccNo=' + acctNo,
            id: 'tblSkds',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };

        Api.getFormData({ Prefix: 'skd', AcctNo: acctNo }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.AcctNo = acctNo;
            $scope._Selects = data.Selects;
        });

        $scope.refresh = function myfunction() {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }



        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            var param = obj;
            Api.ftSKDSDetail({ AccNo: acctNo, TxnId: obj[8] }).success(function (data) {
                //ApplId, TxnId
                $scope._Object = data.subs;
                $scope._Object.AccNo = acctNo;
            })

        });

        $scope.SaveSKDS = function () {
            Utils.InfoNotify();
            Api.SaveSKDS($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        };
        $scope.modalClick = function () {
            Utils.makeObjectNull($scope._Object, { AccNo: acctNo });
            $scope.modalOpen = true;
        };
    }
    // productDiscount controller
    var accountSubsidyController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo || $rootScope.obj.acctNo;

        $scope.dtOptions = {
            serverSide: false,
            processing: true,
            scrollX: true,
            retrieve: true,
            autoWidth: false,
            id: 'tblacctSubsidy'
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        Api.getFormData({ Prefix: 'ast', AcctNo: acctNo }, $rootScope.getRootUrl() + '/Account').success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.AccNo = acctNo;
            $scope._Selects = data.Selects;
        });

        $scope.SaveAcctSubsidyInfoList = function () {
            var list = [];
            Utils.InfoNotify();
            $('#tblAcctSubsidy').find('tbody tr').each(function (index, val) {
                var $row = $(val);
                var sts = $row.find('td:eq(5)').find('select').val();
                if (sts) {
                    list.push({
                        VehRegsNo: $row.find('td:first').text(),
                        CardNo: $row.find('td:eq(1)').text(),
                        SelectedSts: sts == "2" ? "A" : sts == "3" ? "C" : "",
                        SKDSNo: $row.find('td:eq(3)').find('select').val(),
                        SKDSQuota: $row.find('td:eq(4)').find('input').val(),
                    });
                }
            });
            console.log(list);
            if (list.length > 0) {
                Api.SaveAcctSubsidyInfoList({ skds: list, AcctNo: acctNo, SKDSNo: $scope._Object.SelectedSkdsAcctSub }).success(function (data) {
                    Utils.finalResultNotify(data.result);
                    $scope.SubsidyChanged(null, $scope._Object.SelectedSkdsAcctSub);
                })
            }
        }
        $scope.SubsidyChanged = function (obj, val) {
            var $select = $('select')
            //    "sAjaxSource": "@(Url.Action("ftAcctSubsidyInfoList", "CardAcctMaint"))?AcctNo=" + viewModel.acctInfo.acctNo + "&SkdsNo=" + currentVal,
            var obj = {
                AcctNo: acctNo,
                SkdsNo: obj ? obj.Value : val
            };
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var Url = $rootScope.getRootUrl() + '/Account/ftAcctSubsidyInfoList?' + $.param(obj);
            var value = angular.extend($scope.dtOptions, {
                serverSide: true,
                ajax: Url,
                destroy: true,

                "createdRow": function (row, data, index) {


                    var substs = $('#acctSubsidySts').clone();
                    substs.find('option').removeAttr('selected');

                    var selectedSts = data[5] == "A" ? "2" : data[5] == "C" ? "3" : "";

                    substs.find('option[value="' + selectedSts + '"]').attr('selected', 'selected');


                    var acctSub = $("<select class=\"form-control\" style=\"width:100%\" name=\"status\" />");
                    $("<option />", { value: "", text: "" }).appendTo(acctSub);
                    acctSub.find('option').removeAttr('selected');
                    $scope._Selects.SkdsAcctSub.forEach(function (item) {
                        $("<option />", { value: item.Value, text: item.Text }).appendTo(acctSub);
                    });

                    acctSub.find('option[value="' + $scope._Object.SelectedSkdsAcctSub + '"]').attr('selected', 'selected');
                    var elm = $('<input type="text" class="form-control" value="">');
                    elm.priceFormat({
                        prefix: '',
                        centsSeparator: '.',
                        thousandsSeparator: ','
                    }).val(data[4]);
                    $('td', row).eq(3).html(acctSub);
                    $('td', row).eq(4).html(elm);
                    //  xx.appendTo($('td', row).eq(5));
                    $('td', row).eq(5).html(substs);

                },
                columnDefs: [
                    {
                        type: 'html', targets: 3
                    },
                    {
                        type: 'html', targets: 4
                    },
                    {
                        type: 'html', targets: 5
                    }
                ]
            });
            $scope.$broadcast('updateDataTable', { options: value });
        }
    }
    // contact controller
    var contactsController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.modalOpen = false;
        $scope.deleteModalOpen = false;
        $scope.isUpdate = false;
        Api.getFormData({ Prefix: 'con', AcctNo: acctNo }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.RefTo = "ACCT";
            $scope._Object.RefKey = acctNo;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblContacts',
            ajax: $rootScope.getRootUrl() + '/Applications/ftContactList?RefTo=acct&RefKey=' + acctNo,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            rowClick: function (aData) {
            }
        },

    $scope.refresh = function () {
        $rootScope.tables[$scope.dtOptions.id].fnDraw();
    }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: acctNo, RefTo: 'acct', Func: "Add" });
            $scope.isUpdate = false;
        }

        $scope.SaveContact = function () {
            Utils.InfoNotify();
            Api.SaveContact($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }


        $scope.ResendActivationEmail = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.ResendActivationEmail({ AcctNo: acctNo, UserId: obj[5] }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                });
            }
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            $scope.isUpdate = true;
            Api.ftContactDetail({ RefCd: obj[0], RefKey: acctNo, RefTo: 'acct' }).success(function (data) {
                $scope._Object = data.contact;
                $scope._Object.RefKey = acctNo;
                $scope._Object.RefTo = 'acct';
                $scope._Object.RefCd = obj[0];
                $scope._Object.Func = "Upd";
            });
        });
        $scope.deleteRecord = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.RemoveContact({ RefCd: obj[0], RefKey: acctNo, RefTo: 'acct' }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    }
    // cardlist controller
    var cardsListController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: true,
            ajax: $rootScope.getRootUrl() + '/Account/ftCardHolderList?AcctNo=' + acctNo,
            id: 'tblCards',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };

        $scope.$on('indexSelected', function (event, obj) {
            location.href = $rootScope.getRootUrl() + '/Card#/' + acctNo + '/' + obj[0];
        });

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
    }
    // address controller
    var addressController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.modalOpen = false;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'add', AcctNo: acctNo }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "ACCT";
            $scope._Object.RefKey = acctNo;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            autoWidth: false,
            id: 'tblAddress',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            aoColumnDefs: [
          { "sWidth": '200px', "aTargets": [3, 4, 5, 6, 7] },
            ],
            ajax: $rootScope.getRootUrl() + '/Applications/ftAddressList?RefKey=' + acctNo + '&RefTo=acct'
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: acctNo, RefTo: 'acct', Func: 'Add' });
        }

        $scope.Save = function () {
            Utils.InfoNotify();
            Api.SaveAddress($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.ftAddressDetail({ RefCd: obj[0], RefKey: acctNo, RefTo: 'acct' }).success(function (data) {
                $scope._Object = data.address;
                angular.extend($scope._Object, { RefKey: acctNo, RefTo: 'acct', RefCd: obj[0], Func: 'Upd' });
                $scope.updateState(data.address.SelectedCountry);
            });
        });
        $scope.Remove = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.DelAddress({ RefCd: obj[0], RefKey: acctNo, RefTo: 'acct' }).success(function (data) {
                    Utils.finalResultNotify(data.result);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        };

        $scope.CountryChanged = function (item, value) {
            $scope.updateState(value);
        }
        $scope.updateState = function (value) {
            Api.WebGetState({ CountryCd: value }).success(function (item) {
                $scope._Selects.State = item;
            });
        }
    }
    // txnSearch controller
    var txnSearchController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.modalOpen = false;
        $scope.aaData = [];
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'aps', AcctNo: acctNo }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.AcctNo = acctNo;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: false,
            processing: true,
            scrollX: true,
            autoWidth: false,
            retrieve: true,
            //checkBox: true,
            id: 'tblTxnSearch',
            aoColumnDefs: [
                { "sClass": "detail-toggler", "aTargets": [0] },
            ],
            childTable: {
                format: function (nRows) {
                    var htm;
                    var rows = _.filter($scope.aaData, function (data) {
                        return data[13] == nRows[13];//txnid
                    });
                    var header = "<thead><tr><th>Product Descp</th><th>Quantity</th><th>Product Amount</th><th>VAT Amount</th><th>VAT Cd</th><th>VAT Rate</th></tr></thead>";
                    var rows2 = _.map(rows, function (Next) {
                        return '<tr>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[15] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[16] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[17] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[18] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[19] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[20] + '</td></tr>'
                    });
                    var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                    console.log(fullTable);
                    return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr>';
                },
                sum: function sum(numbers) {
                    return _.reduce(numbers, function (result, current) {
                        return result + parseFloat(current[9]);
                    }, 0);
                },
                fngroupOp: function (e, settings, json) {
                    //json.aaData = [];

                    if ($scope.date) {
                        if (new Date() - $scope.date <= 2000)
                            return;
                    }
                    $scope.$apply(function () {
                        $scope.date = new Date();
                        $scope.aaData = json.aaData;
                    });
                    var rows = [];
                    _.each(json.aaData, function (data) {
                        if (rows.length) {
                            var contains = _.find(rows, function (item) {
                                return item[13] == data[13];
                            })
                            if (!contains) {
                                rows.push(data);
                            }
                        } else {
                            rows.push(data);
                        }
                    });
                    json.aaData = rows;
                }
            }
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: acctNo, RefTo: 'acct' });
        }
        $scope.SearchTxn = function () {
            obj = $scope._Object;
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var prefix = $rootScope.getRootUrl() + '/Account/ftAcctPostedTxnSearch?';
            var queryString = $.param({
                SelectedCardNo: obj.SelectedCardNo, SelectedTxnCategory: obj.SelectedTxnCategory,
                SelectedTxnCd: obj.SelectedTxnCd, FromDate: obj.FromDate, ToDate: obj.ToDate, AcctNo: acctNo
            });
            var value = angular.extend($scope.dtOptions, {
                serverSide: true, ajax: prefix + queryString, destroy: true,
                "drawCallback": function (settings) {
                    $scope.modalOpen = false;
                    $scope.$apply();
                }
            });
            $scope.$broadcast('updateDataTable', { options: value });

            //Utils.updateDataTable($table, { serverSide: true, ajax: prefix + queryString });
            //var $newTable = $rootScope.tables[$scope.dtOptions.id];
            //var oprion = $newTable.fnSettings();
            //$newTable.fnUpdate();
            //$newTable.fnDraw();
        }
    }
    // miscellanious controller
    var MiscellaniousController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        Api.getFormData({ Prefix: 'mis' }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.ApplId = applId;
        });
        $scope.Save = function () {
            Utils.InfoNotify();
            Api.SaveMiscellanious($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            })
        }
    }
    // costcentre controller
    var costcentreController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.modalOpen = false;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.acctNo;
        $scope.isUpdate = false;

        Api.getFormData({ Prefix: 'csc', AcctNo: acctNo }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "ACCT";
            $scope._Object.RefKey = acctNo;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblCostCentre',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            ajax: $rootScope.getRootUrl() + '/Account/ftCostCentreList?RefKey=' + acctNo + '&RefTo=acct'
        };

        $scope.gotoVelocity = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            //console.log(obj[0])
            $location.path('/costcentre/' + acctNo + '/' + obj[0] + '/velocity');
        }

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
            $("span#tblCostCentre-options").css('display', "none");
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            $scope.isUpdate = false;
            Utils.makeObjectNull($scope._Object, { RefKey: acctNo, RefTo: 'acct' });
        }
        $scope.Save = function () {
            Utils.InfoNotify();
            Api.CostCentreMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            $scope.isUpdate = true;
            var obj = { SelectedCostCentre: obj[0], RefKey: acctNo, RefTo: 'acct' };
            Api.FtCostCentreDetail(obj).success(function (data) {
                angular.extend($scope._Object, data, obj);
            });
        });
    }
    // billingItem controller
    var billingItemController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.modalOpen = false;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.acctNo;

        Api.getFormData({ Prefix: 'bil', AcctNo: acctNo }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.AccNo = acctNo;
            $scope._Selects = data.Selects;
        });

        $scope.dtOptions = {
            serverSide: false,
            processing: true,
            scrollX: false,
            retrieve: true,
            autoWidth: false,
            id: 'tblbillingSearch',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            rowCallback: function (aData, nRow) {
                $scope.modalOpen = false;
                $scope.tarBalance = aData[9];
                $scope.totalBillingTxnAmt = aData[11];
                $scope.TotalSettledAmt = aData[12];
                $scope.$apply();
                if (aData[0] == "T") {
                    $(nRow).css('background-color', '#D5D5D5');
                }
            }
        };

        $scope.$on('indexSelected', function (obj) {
            var acctNo = $location.path(obj[0]);
            $location.path('#/generalInfo/' + acctNo);
        });

        $scope.navSettlementDtl = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            $rootScope.selectedBilling = obj;
            if (obj) {
                $location.path('/billingItemDetail/' + acctNo + "/settlement" + "/" + obj[9]);
            }
        }

        $scope.navTxnList = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            $rootScope.selectedBilling = obj;
            if (obj) {
                $location.path('/billingItemDetail/' + acctNo + "/txnlist" + "/" + obj[9]);
            }
        }


        $scope.modalClick = function () {
            $scope.modalOpen = true;
        }

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.SearchBillingItem = function () {
            obj = $scope._Object;
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var prefix = $rootScope.getRootUrl() + '/Account/ftSearchBillingItem?';
            var queryString = $.param({
                FromDate: obj.FromDate, ToDate: obj.ToDate,
                SelectedTxnCategory: obj.SelectedTxnCategory, SelectedSts: obj.SelectedSts, AccNo: acctNo
            });
            var value = angular.extend($scope.dtOptions, { serverSide: true, ajax: prefix + queryString, destroy: true });
            $scope.$broadcast('updateDataTable', { options: value });
            //Utils.updateDataTable($table, { serverSide: true, ajax: prefix + queryString });
            //var $newTable = $rootScope.tables[$scope.dtOptions.id];
            //var oprion = $newTable.fnSettings();
            //$newTable.fnUpdate();
            //$newTable.fnDraw();
        }
    }
    // billingDetail controller
    var billingDetailController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.modalOpen = false;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.acctNo;
        $scope.txnId = $routeParams.txnId;
        $scope.txnType = $routeParams.type;
        var selectedBilling = $rootScope.selectedBilling;

        $scope.Details = {
            Descp: selectedBilling[1],
            TxnDate: selectedBilling[2],
            DueDate: selectedBilling[3],
            BillingAmt: selectedBilling[4]
        };
        if ($scope.txnType == 'settlement') {
            $scope.dtOptions = {
                serverSide: true,
                processing: true,
                scrollX: false,
                id: 'tblBillingItemSettlement',
                ajax: $rootScope.getRootUrl() + '/Account/ftBillingItemSettlementList?Txn=' + $scope.txnId
            };
        } else if ($scope.txnType == 'txnlist') {
            $scope.dtOptions = {
                serverSide: true,
                processing: true,
                scrollX: false,
                id: 'tblBillingItemTxnList',
                ajax: $rootScope.getRootUrl() + '/Account/ftBillingItemTxnList?Txn=' + $scope.txnId
            };
        }
    }
    // userManagement controller
    var userManagementController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.acctNo;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblusers',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            ajax: $rootScope.getRootUrl() + '/Account/AccountUsersListSelect?AcctNo=' + acctNo
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.ResendActivationEmail = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.ResendActivationEmail({ AcctNo: acctNo, UserId: obj[0] }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                });
            }
        }

        $scope.ResetPasswordCounter = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.ResetPasswordCounter({ AcctNo: acctNo, UserId: obj[0] }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                });
            }
        }
    }
    // productDiscount controller
    var productDiscountController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.modalOpen = false;
        $scope.searchModalOpen = false;
        $scope.deleteModalOpen = false;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'pdc', AcctNo: acctNo }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "ACCT";
            $scope._Object.RefKey = acctNo;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblProductDiscount',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            ajax: $rootScope.getRootUrl() + '/Account/WebProductDiscountListSelect?RefTo=ACCT&AcctNo=' + acctNo,

            rowCallback: function (aData, nRow) {
                $scope.searchModalOpen = false;
            }
        };
        $scope.SubsidyChanged = function (obj, val) {
            console.log(val);
            if (val) {
                Api.GetPlanIdFromSubsidy({ SelectedProdDiscType: val }).success(function (data) {
                    console.log(data);
                    //if (!$scope._Selects) {
                    //    $scope._Selects = {
                    //        PlanId: data.PlanId
                    //    }
                    //} else {
                    //    $scope._Selects.PlanId = data.PlanId;
                    //    $scope._Object.SelectedPlanId = null;//SelectedPlanId
                    //}
                    $scope._Selects.PlanId = data.PlanId;
                })
            } else {
                $scope._Selects.PlanId = [];
            }
        }

        $scope.searchProductDiscount = function () {
            obj = $scope._Object;
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var prefix = $rootScope.getRootUrl() + '/Account/WebProductDiscountListSelect?';
            var queryString = $.param({
                DiscType: obj.SelectedProdDiscType, AcctNo: acctNo, Refto: "ACCT"
            });
            var value = angular.extend($scope.dtOptions, { serverSide: true, ajax: prefix + queryString, destroy: true });
            $scope.$broadcast('updateDataTable', { options: value });
        };

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { Func: "N", AcctNo: acctNo, RefTo: "ACCT" });
        }

        $scope.Save = function () {
            Utils.InfoNotify();
            console.log($scope._Object);
            Api.ProductDiscountMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag == 0) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.ProductDiscountSelect({ DiscType: obj[2], AcctNo: acctNo, Id: obj[10], RefTo: 'ACCT' }).success(function (data) {
                $scope._Object = data.Discount;
                $scope._Object.Func = "E";
                $scope._Object.AcctNo = acctNo;
                $scope._Object.RefTo = 'ACCT';

                $scope.SubsidyChanged(null, $scope._Object.SelectedProdDiscType);
                $scope.$apply();
            });
        })
        $scope.Remove = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.DeleteProductDiscount({ SelectedProdCd: obj[0], SelectedProdDiscType: obj[2], EffDateFrom: obj[5], RefKey: acctNo, AcctNo: acctNo, RefTo: "ACCT" }).success(function (data) { //'acct'
                    Utils.finalResultNotify(data.result);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    }
    // pointsAdjustment controller
    var pointsAdjustmentController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.modalOpen = false;
        $scope.searchModalOpen = false;
        $scope.deleteModalOpen = false;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'pad', AcctNo: acctNo }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "ACCT";
            $scope._Object.RefKey = acctNo;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblPointsAdjustment',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            ajax: $rootScope.getRootUrl() + '/Account/WebPointAdjustmentListSelect?AcctNo=' + acctNo
        };

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { Func: "N", AcctNo: acctNo });
        }

        $scope.Save = function () {
            Utils.InfoNotify();
            Api.WebPointAdjustmentMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag == 0) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.WebPointAdjustmentSelect({ AcctNo: acctNo, TxnId: obj[7] }).success(function (data) {
                $scope._Object = data.Adj;
                $scope._Object.Func = "E";
                $scope._Object.AcctNo = acctNo;
            });
        })
    }
    // pukal controller
    var pukalController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;

        Api.getFormData({ Prefix: 'puk', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.RefKey = acctNo;//add
        });
        $scope.Save = function () {
            Utils.InfoNotify();
            Api.PukalMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            });
        }
    }
    // eventConfigurationAcct controller
    var eventConfigurationAcct = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;

        Api.getFormData({ Prefix: 'puk', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.RefKey = acctNo;//add
        });
        $scope.Save = function () {
            Utils.InfoNotify();
            Api.PukalMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            });
        }
    }
    // eventConfiguration controller
    var eventConfigurationController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $route, $compile) {
        $scope.Items = [];
        var itemObjModel = null;
        var itemObjItem = [];
        var backupObj = {};
        var backupBitmapObj = {};
        $scope.Recipients = [];
        $scope.detailModalOpen = false;
        $scope.getClass = function (item) {
            var index = $scope.Items.indexOf(item);
            var item = item;
        };
        var periodType = [{ Value: "c", Text: "Current Cycle" }, { Value: 's', Text: "Set checking period" }];
        $scope.mapper = Utils.getEventMap();
        $scope.acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.isListView = $routeParams.id ? false : true;
        if ($scope.isListView) {
            $scope._Object = null;
            $scope.dtOptions = {
                serverSide: true,
                processing: true,
                scrollX: false,
                id: 'tblEventConf',
                edit: {
                    level: 'scope',
                    func: 'indexSelected',
                },
                ajax: $rootScope.getRootUrl() + '/Account/WebEventAcctConfListSelect?RefTo=ACCT&RefKey=' + $scope.acctNo
            };
        } else {
            $scope.EventId = $routeParams.id;
            $scope.scheduleId = $routeParams.scheduleId;
            Api.WebEventAcctConfSelect({ EventTypeId: $scope.EventId, EventScheduleId: $scope.scheduleId, AcctNo: $scope.acctNo }).success(function (data) {
                $scope.callBack(data);
            })
        }
        $scope.$on('indexSelected', function (event, obj) {
            $location.path('/eventConfDetail/' + $scope.acctNo + "/" + obj[0] + "/" + obj[1]);
            $scope.$apply();
        });


        $scope.callBack = function (data) {
            $scope.Items = [];
            $scope._Object = data.Model[0];
            $scope._Selects = data.Selects;
            $scope._Selects.PeriodType = periodType;
            if ($scope._Object.SelectedScope) {
                $scope._Object.SelectedOwner = $scope._Object.SelectedScope;
            }
            if ($scope._Object.MaxOccur > 0) {
                $scope._Object.SelectedOccur = 'S';
            } else {
                $scope._Object.SelectedOccur = 'E';
            }
            $scope.bitmapControls = [];

            _.each(data.Model, function (item, index) {

                var obj = {
                    MinIntVal: item.MinIntVal, MaxIntVal: item.MaxIntVal, MinMoneyVal: item.MinMoneyVal,
                    MaxMoneyVal: item.MaxMoneyVal, MinDateVal: item.MinDateVal, MaxDateVal: item.MaxDateVal,
                    MinTimeVal: item.MinTimeVal, MaxTimeVal: item.MaxTimeVal, VarCharVal: item.VarCharVal,
                    PeriodType: item.PeriodType, PeriodInterval: item.PeriodInterval, BitmapAmt: item.BitmapAmt,
                    id: index, EvtPlanDetailId: item.EvtPlanDetailId
                };


                if (obj.PeriodInterval > 0) {
                    obj.SelectedOccur = 'S';
                } else {
                    obj.SelectedOccur = 'C';
                }
                if (obj.BitmapAmt > 0) {
                    var __bitmap = Utils.findBinarySequence(obj.BitmapAmt).sort();
                    var __ = [];
                    _.each(__bitmap, function (item) {
                        var selectedMapper = _.find($scope.mapper, function (_x) {
                            return _x.id == item;
                        });
                        if (selectedMapper) {
                            selectedMapper.val = obj[selectedMapper.type];
                            __.push(selectedMapper);
                        }
                    });
                    $scope.bitmapControls.push(__);
                    itemObjItem = $scope.bitmapControls[0];
                }
                $scope.Items.push(obj);
                itemObjModel = obj;
            });
            $scope.WebEventAcctRcptListSelect({ ScheduleId: $scope.scheduleId });
        }
        $scope.selectedOccurChanged = function (newVal) {
            if (newVal == 'E') {
                $scope._Object.MaxOccur = "-1";
                $scope._Object.SelectedFrequency = '';
            } else {
                $scope._Object.MaxOccur = "";
            }
        }


        $scope.intervalChanged = function (val) {
            if (val == 'C') {
                $scope.Items[$scope.selectedItem].PeriodInterval = 0;
                $scope.Items[$scope.selectedItem].PeriodType = "C";
            }
        }

        $scope.WebEventAcctRcptListSelect = function (obj) {
            Api.WebEventAcctRcptListSelect(obj).success(function (data) {
                _.each(data, function (item) {
                    var obj = { ChannelInd: item.ChannelInd, ContactName: item.ContactName, ContactNo: item.ContactNo, Id: item.Id, LangInd: item.LangInd };
                    obj.NotifyIndEmail = false;
                    obj.NotifyIndSms = false;
                    if (item.ChannelInd == 1 || $scope._Object.ChannelInd == 3) {
                        obj.NotifyIndEmail = true;
                    }
                    if (item.ChannelInd == 2 || item.ChannelInd == 3) {
                        obj.NotifyIndSms = true;
                    }
                    $scope.Recipients.push(obj);
                });
            });
        }
        $scope.containsBitmap = function (index, bitmap) {
            var __ = $scope.bitmapControls[index];
            var contains = _.find(__, function (item) {
                return item.id == bitmap;
            });
            if (contains)
                return true;
            return false;
        };
        $scope.editItem = function (item) {
            $scope.isItemEdit = true;
            var index = $scope.Items.indexOf(item);
            $scope.selectedItem = index;
            backupObj = $.extend({}, $scope.Items[index]);
            backupBitmapObj = $.extend({}, $scope.bitmapControls[index]);
            $scope.detailModalOpen = true;
        }


        $scope.CreateNewItem = function (ctrl) {
            ctrl.currentTarget.blur();
            $scope.isItemEdit = false;
            var newObject = jQuery.extend({}, itemObjModel);
            Utils.makeObjectNull(newObject, { BitmapAmt: $scope.Items[$scope.Items.length - 1].BitmapAmt, id: $scope.Items.length });
            $scope.Items.push(newObject);
            $scope.bitmapControls.push($scope.bitmapControls[$scope.bitmapControls.length - 1]);
            $scope.selectedItem = $scope.Items.length - 1;
            $scope.detailModalOpen = true;
        };



        $scope.removeItem = function (item) {
            var index = $scope.Items.indexOf(item);
            $scope.itemtDeleteIndex = index;
            $scope.itemDeleteModalOpen = true;
        }

        $scope.confirmitemDelete = function () {
            $scope.Items.splice($scope.itemtDeleteIndex, 1);
            $scope.bitmapControls.splice($scope.itemtDeleteIndex, 1);
            $scope.itemDeleteModalOpen = false;
        }

        $scope.finishEdit = function (item) {
            $scope.detailModalOpen = false;
        };

        $scope.cancelEdit = function () {
            if ($scope.isItemEdit) {
                $scope.Items[$scope.selectedItem] = backupObj;
                $scope.bitmapControls[$scope.selectedItem] = backupBitmapObj;
            } else {
                $scope.Items.splice($scope.selectedItem, 1);
                $scope.bitmapControls.splice($scope.selectedItem, 1);
            }
            $scope.detailModalOpen = false;
        }


        $scope.toggleStatus = function (val) {
            $scope._Object.SelectedStatus = val;
        };

        $scope.updateCompanyName = function () {
            Api.WebGetRefCmpyName({ SelectedRefTo: $scope._Object.SelectedRefTo, RefKey: $scope._Object.RefKey }).success(function (data) {
                if (data)
                    $scope._Object.CompanyName = data.companyName;
            });
        }


        //Recipient info//


        $scope.CreateNewRecipient = function (ctrl) {
            ctrl.currentTarget.blur();
            var obj = { NotifyInd: null, ContactName: "", ContactNo: "", NotifyIndEmail: false, NotifyIndSms: false, LangInd: "", isEdit: true, isNew: true };
            $scope.Recipients.push(obj);
            $scope.selectedContact = $scope.Recipients.length - 1;
            $scope.contactModalOpen = true;

        }

        $scope.editRecipient = function (item) {
            item.ContactNoDuplicate = item.ContactNo;
            item.ContactNameDuplicate = item.ContactName;
            item.NotifyIndEmailDuplicate = item.NotifyIndEmail;
            item.NotifyIndSmsDuplicate = item.NotifyIndSms;
            $scope.selectedContact = $scope.Recipients.indexOf(item);
            $scope.contactModalOpen = true;
        }

        $scope.removeRecipient = function (item) {
            var index = $scope.Recipients.indexOf(item);
            $scope.recipientDeleteIndex = index;
            $scope.contactDeleteModalOpen = true;

        }

        $scope.confirmremoveRecipient = function () {

            $scope.confirmremoveRecipient = function () {
                if ($scope.Recipients[$scope.recipientDeleteIndex].isNew) {
                    $scope.Recipients.splice($scope.recipientDeleteIndex, 1);
                    $scope.contactDeleteModalOpen = false;
                } else {
                    Utils.InfoNotify();
                    Api.WebNtfEvtConfRcptDelete({ SchRcptId: $scope.Recipients[$scope.recipientDeleteIndex].Id }).success(function (data) {
                        Utils.finalResultNotify(data);
                        $scope.Recipients.splice($scope.recipientDeleteIndex, 1);
                        $scope.contactDeleteModalOpen = false;
                    })
                }
            }


        }

        $scope.switchMedium = function (index, type) {
            var item = $scope.Recipients[index];
            item.NotifyIndSms = false;
            item.NotifyIndEmail = false;
            if (type == "sms") {
                item.NotifyIndSms = true;
            }
            else if (type == "email") {
                item.NotifyIndEmail = true;
            }
        }

        $scope.cancelContact = function (item) {
            if (item.isNew) {
                var index = $scope.Recipients.indexOf(item);
                $scope.Recipients.splice(index, 1);
            } else {
                item.ContactNo = item.ContactNoDuplicate;
                item.ContactName = item.ContactNameDuplicate;
                item.NotifyIndEmail = item.NotifyIndEmailDuplicate;
                item.NotifyIndSms = item.NotifyIndSmsDuplicate;
            }
            $scope.contactModalOpen = false;
        }
        $scope.finishEditContact = function (item) {
            if (!item.ContactNo || !item.ContactName || !item.LangInd) {
                return Utils.PNotify({ flag: 1, Descp: "Please fill in all fields" });
            }
            item.isEdit = false;
            // item.isNew = false;
            $scope.Recipients.indexOf(item).ContactNoDuplicate = null;
            $scope.contactModalOpen = false;
        }
        $scope.finishEdit = function () {
            $scope.detailModalOpen = false;
        }

        $scope.saveAll = function () {
            var obj = $scope._Object;
            obj._EventRcptList = $scope.Recipients;
            obj.ProductItems = $scope.Items;
            var NtifiCount = 0;

            if (!$scope.Recipients.length) {
                return Utils.PNotify({ flag: 1, Descp: "Atleast one recipient details must be filled." });
            }

            _.each(obj._EventRcptList, function (item) {
                if (item.NotifyIndSms)
                    item.ChannelInd = 2;
                else if (item.NotifyIndEmail)
                    item.ChannelInd = 1;
            });
            Utils.InfoNotify();
            Api.WebEventAcctConfMaint(obj).success(function (data) {

                if (data.flag == 0) {
                    if ($scope._Object.EventScheduleId == '0') {
                        $location.path($location.$$url + data.Id);
                    } else {
                        // $location.path($location.$$url + data.Id);
                        $route.reload();
                    }
                }
                Utils.finalResultNotify(data);
            })
        }
    }
    //inject service
    generalInfoController.$inject = injectParams;
    tempCreditController.$inject = injectParams;
    uptoDateBalaceController.$inject = injectParams;
    statusMaintController.$inject = injectParams;
    finanacialInfoController.$inject = injectParams;

    velocityController.$inject = injectParams;
    TxnAdjustmentController.$inject = injectParams;
    paymentTxnController.$inject = injectParams;
    locationAcceptanceController.$inject = injectParams;
    caoController.$inject = injectParams;
    depositInfoController.$inject = injectParams;
    vehiclesController.$inject = injectParams;
    skdsController.$inject = injectParams;
    accountSubsidyController.$inject = injectParams;
    contactsController.$inject = injectParams;
    cardsListController.$inject = injectParams;

    addressController.$inject = injectParams;
    txnSearchController.$inject = injectParams;

    MiscellaniousController.$inject = injectParams;
    costcentreController.$inject = injectParams;
    billingItemController.$inject = injectParams;
    billingDetailController.$inject = injectParams;
    userManagementController.$inject = injectParams;

    productDiscountController.$inject = injectParams;
    pointsAdjustmentController.$inject = injectParams;
    pukalController.$inject = injectParams;
    eventConfigurationAcct.$inject = injectParams;
    eventConfigurationController.$inject = injectParams;

    angular.module('CardtrendApp').controller('generalInfoController', generalInfoController);
    angular.module('CardtrendApp').controller('tempCreditController', tempCreditController);
    angular.module('CardtrendApp').controller('uptoDateBalaceController', uptoDateBalaceController);
    angular.module('CardtrendApp').controller('statusMaintController', statusMaintController);
    angular.module('CardtrendApp').controller('finanacialInfoController', finanacialInfoController);

    angular.module('CardtrendApp').controller('velocityController', velocityController);
    angular.module('CardtrendApp').controller('TxnAdjustmentController', TxnAdjustmentController);
    angular.module('CardtrendApp').controller('paymentTxnController', paymentTxnController);
    angular.module('CardtrendApp').controller('locationAcceptanceController', locationAcceptanceController);
    angular.module('CardtrendApp').controller('caoController', caoController);
    angular.module('CardtrendApp').controller('depositInfoController', depositInfoController);

    angular.module('CardtrendApp').controller('vehiclesController', vehiclesController);
    angular.module('CardtrendApp').controller('skdsController', skdsController);
    angular.module('CardtrendApp').controller('accountSubsidyController', accountSubsidyController);
    angular.module('CardtrendApp').controller('contactsController', contactsController);
    angular.module('CardtrendApp').controller('cardsListController', cardsListController);

    angular.module('CardtrendApp').controller('addressController', addressController);
    angular.module('CardtrendApp').controller('txnSearchController', txnSearchController);
    angular.module('CardtrendApp').controller('MiscellaniousController', MiscellaniousController);
    angular.module('CardtrendApp').controller('costcentreController', costcentreController);
    angular.module('CardtrendApp').controller('billingItemController', billingItemController);

    angular.module('CardtrendApp').controller('billingDetailController', billingDetailController);
    angular.module('CardtrendApp').controller('userManagementController', userManagementController);
    angular.module('CardtrendApp').controller('productDiscountController', productDiscountController);
    angular.module('CardtrendApp').controller('pointsAdjustmentController', pointsAdjustmentController);
    angular.module('CardtrendApp').controller('pukalController', pukalController);

    angular.module('CardtrendApp').controller('eventConfigurationAcct', eventConfigurationAcct);
    angular.module('CardtrendApp').controller('eventConfigurationController', eventConfigurationController);

}());
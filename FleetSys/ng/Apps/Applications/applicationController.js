(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$routeParams', '$timeout', 'ApplicationApi', '$http', 'Utils'];

    var indexController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        $rootScope.obj._type = 'index';
        $scope.IsHidden = false;

        $scope.dtOptions = Utils.GetDataTable('tblmsApplications', true, '/Applications/WebMilestoneListSelect?' + $.param({ Ind: 1 }), 'gotoMilestones');
        $scope.loadMileStones = function () {
            $timeout(function () {
                $scope.dtOptionsmileStones = Utils.GetDataTable('tblApplications', true, '/Applications/ftAcctSignUpList', 'indexSelected');
            }, 300)
        }
        $scope.$on("gotoMilestones", function (event, aData) {
            location.href = $rootScope.getRootUrl() + "/Approval#/Approval/Appl/" + aData[0] + "/" + aData[0];
            //location.href = "/Approval#/Approval/Appl/" + aData[0] + '/' + aData[0];
            //$location.path("/approval/" + aData[0]);
            $scope.$apply();
        });

        $scope.$on("indexSelected", function (event, aData) {
            $rootScope.obj.applId = aData[0];
            $rootScope.obj._type = 'edit';
            $location.path('/generalInfo').search('applId', $rootScope.obj.applId);
            $scope.$apply();
        });
    };
    //statementDetail Controller
    var generalInfoController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        $scope._Object = null;
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.obj.applId;
        $scope.isNew = applId ? false : true;
        ApplicationApi.getFormData({ Prefix: 'gen', ApplId: applId }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.ApplicationId = applId;
            $scope._Selects = data.Selects;
        });
        $scope.postGeneralInfo = function () {
            Utils.InfoNotify();
            console.log($scope._Object);
           
            ApplicationApi.postApplicationGeneralInfo($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    if ($scope.isNew) {
                        $rootScope.obj.applId = data.ApplId;
                        $rootScope.obj._type = 'edit';
                        $scope.isNew = false;
                        $scope._Object.ApplicationId = data.ApplId;
                        $location.path('/cao/' + data.ApplId);
                    }
                }
            });
        }
    };
    //SOATxnList Controller
    var velocityController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        var CostCentre = $routeParams.cst;

        var obj = {
            applId: applId,
            CostCentre: CostCentre
        };

        $scope.modalOpen = false;
        $scope.deleteModalOpen = false;

        var columnDefs = { "bVisible": false, "aTargets": [0] };
        $scope.dtOptions = Utils.GetDataTableWithCollumnDefs('tblVelocity', true, '/Applications/ftVelocityList?' + $.param(obj), 'indexSelected', columnDefs);

        //$scope.dtOptions = {
        //    serverSide: true,
        //    processing: true,
        //    "scrollX": true,
        //    id: 'tblVelocity',
        //    ajax: $rootScope.getRootUrl() + '/Applications/ftVelocityList?' + $.param(obj),
        //    edit: {
        //        level: 'scope',
        //        func: 'indexSelected',
        //    },
        //    aoColumnDefs: [
        //  { "bVisible": false, "aTargets": [0] },
        //    ]
        //};

        ApplicationApi.getFormData({ Prefix: 'vel' }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.ApplId = applId;
            $scope._Selects = data.Selects;
        });
        $scope.newVelocity = function (event) {
            if (event.target.getAttribute("disabled"))
                return;
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { ApplId: applId, CostCentre: CostCentre, Func: "Add" });
        }
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.SaveVelocityLimit = function () {
            Utils.InfoNotify();
            $scope._Object.CostCentre = obj.CostCentre;
            ApplicationApi.SaveVelocityLimit($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            ApplicationApi.ftVelocityDetail({ ApplId: applId, SelectedVelocityInd: obj[11], SelectedProdCd: obj[12], CostCentre: CostCentre }).success(function (data) {
                $scope._Object = data.velocity;
                $scope._Object.Func = "Upd";
                $scope._Object.ApplId = applId;
            });
        });

        $scope.delete = function () {
            //delete velocity
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                ApplicationApi.RemoveVelocity({ ApplId: applId, VelInd: obj[11], ProdCd: obj[12], CostCentre: CostCentre }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    //SOATxnList Controller
    var caoController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        ApplicationApi.getFormData({ Prefix: 'cao', ApplId: applId }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });
        $scope.SaveCreditAssessmentOperation = function () {
            Utils.InfoNotify();
            $scope._Object.ApplId = applId;
            ApplicationApi.SaveCreditAssessmentOperation($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                //$location.path('/approval/' + data.ApplId);
            });
        }
    };
    //depositInfo Controller
    var depositInfoController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        $scope.show = false;
        $scope.modalOpen = false;
        ApplicationApi.getFormData({ Prefix: 'dep' }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.ApplId = applId;
            $scope._Selects = data.Selects;
        });
        var columnDefs = { "bVisible": false, "aTargets": [6] };
        $scope.dtOptions = Utils.GetDataTableWithCollumnDefs('tblDepositInfo', true, '/Applications/ftGetAcctDepositInfoList?ApplId=' + applId, 'indexSelected', columnDefs);

        //$scope.dtOptions = {
        //    serverSide: true,
        //    processing: true,
        //    "scrollX": true,
        //    id: 'tblDepositInfo',
        //    ajax: $rootScope.getRootUrl() + '/Applications/ftGetAcctDepositInfoList?ApplId=' + applId,
        //    edit: {
        //        level: 'scope',
        //        func: 'indexSelected',
        //    },
        //    aoColumnDefs: [
        //     { "bVisible": false, "aTargets": [6] },
        //     ]
        //};

        $scope.dtSecurityDepositOptions = Utils.GetDataTable('tblSecurityDepositOptions', true, '/Account/WebAcctHistoryListSelect?AcctNo=' + applId + '&&type=sec', 'securitySelected');

        $scope.refresh = function myfunction() {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;

            ApplicationApi.ftGetGetAcctDepositInfoDetail({ _applId: applId, TxnId: obj[6] }).success(function (data) {
                $scope._Object = data.Adi;
                $scope._Object.ApplId = applId;
            });
        });
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { ApplId: applId });
        }
        $scope.SaveAcctDepositInfoOps = function () {
            Utils.InfoNotify();
            ApplicationApi.SaveAcctDepositInfoOps($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $rootScope.tables[$scope.dtSecurityDepositOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }
    };
    //SOATxnList Controller
    var vehiclesController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        $scope.modalOpen = false;
        ApplicationApi.getFormData({ Prefix: 'veh' }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.ApplId = applId;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = Utils.GetDataTable('tblVehicles', false, '/Applications/ftVehicleList?ApplId=' + applId, 'indexSelected');
        $scope.refresh = function myfunction() {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            ApplicationApi.ftVehicleDetail({ ApplId: applId, AppcId: obj[0], VehRegtNo: obj[3] }).success(function (data) {
                $scope._Object = data.vehicle;
            });
        });
        //$scope.modalClick = function () {
        //    $scope.modalOpen = true;
        //    Utils.makeObjectNull($scope._Object, { ApplId: applId });
        //}
        $scope.Save = function () {
            Utils.InfoNotify();
            ApplicationApi.SaveVehicleLimit($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }
    };

    //SOATxnList Controller
    var skdsController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        $scope.modalOpen = false;
        $scope.dtOptions = Utils.GetDataTable('tblSkds', true, '/Applications/ftSKDSList?ApplId=' + applId, 'indexSelected');

        ApplicationApi.getFormData({ Prefix: 'skd' }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.ApplId = applId;
            $scope._Selects = data.Selects;
        });

        $scope.refresh = function myfunction() {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            var param = obj;
            ApplicationApi.ftSKDSDetail({ ApplId: applId, TxnId: obj[8] }).success(function (data) {
                //ApplId, TxnId
                $scope._Object = data.subs;
                $scope._Object.ApplId = applId;
            })
        });

        $scope.SaveSKDS = function () {
            Utils.InfoNotify();
            ApplicationApi.SaveSKDS($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        };

        $scope.modalClick = function () {
            Utils.makeObjectNull($scope._Object, { ApplId: applId });
            $scope.modalOpen = true;
        };
    };
    //SOATxnList Controller
    var contactsController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        $scope.modalOpen = false;
        $scope.deleteModalOpen = false;
        $scope.isUpdate = false;
        ApplicationApi.getFormData({ Prefix: 'con' }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.RefTo = "APPL";
            $scope._Object.RefKey = applId;
        });
        $scope.dtOptions = Utils.GetDataTable('tblContacts', false, '/Applications/ftContactList?RefTo=appl&RefKey=' + applId, 'indexSelected');

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: applId, RefTo: 'appl', Func: "Add" });
            $scope.isUpdate = false;
        }

        $scope.SaveContact = function () {
            Utils.InfoNotify();
            ApplicationApi.SaveContact($scope._Object).success(function (data) {
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
            ApplicationApi.ftContactDetail({ RefCd: obj[0], RefKey: applId, RefTo: 'appl' }).success(function (data) {
                $scope._Object = data.contact;
                $scope._Object.RefKey = applId;
                $scope._Object.RefTo = 'appl';
                $scope._Object.RefCd = obj[0];
                $scope._Object.Func = "Upd";
            });
        });
        $scope.deleteRecord = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                ApplicationApi.RemoveContact({ RefCd: obj[0], RefKey: applId, RefTo: 'appl' }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };

    //SOATxnList Controller
    var applicantsController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http,Utils) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        $scope.applId = applId;
        $scope.dtOptions = Utils.GetDataTable('tblApplicants', false, '/Applicant/ftCardHolderList?_ApplicationId=' + applId, 'indexSelected');
        $scope.$on('indexSelected', function (event, obj) {
            location.href = $rootScope.getRootUrl() + '/Applicant#/' + applId + '/' + obj[0];
        });

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
    };
    //SOATxnList Controller
    var addressController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        $scope.modalOpen = false;
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        ApplicationApi.getFormData({ Prefix: 'add' }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "APPL";
            $scope._Object.RefKey = applId;
            $scope._Selects = data.Selects;
        });

        var columnDefs = { "sWidth": '200px', "aTargets": [3, 4, 5, 6, 7] };
        $scope.dtOptions = Utils.GetDataTableWithCollumnDefs('tblAddress', false, '/Applications/ftAddressList?RefKey=' + applId + '&RefTo=appl', 'indexSelected', columnDefs);

        //$scope.dtOptions = {
        //    serverSide: true,
        //    processing: true,
        //    scrollX: false,
        //    id: 'tblAddress',
        //    edit: {
        //        level: 'scope',
        //        func: 'indexSelected',
        //    },
        //    aoColumnDefs: [
        //  { "sWidth": '200px', "aTargets": [3, 4, 5, 6, 7] },
        //    ],
        //    ajax: $rootScope.getRootUrl() + '/Applications/ftAddressList?RefKey=' + applId + '&RefTo=appl'
        //};

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: applId, RefTo: 'appl', Func: 'Add' });
        }

        $scope.Save = function () {
            Utils.InfoNotify();
            ApplicationApi.SaveAddress($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;

            ApplicationApi.ftAddressDetail({ RefCd: obj[0], RefKey: applId, RefTo: 'appl' }).success(function (data) {
                $scope._Object = data.address;
                angular.extend($scope._Object, { RefKey: applId, RefTo: 'appl', RefCd: obj[0], Func: 'Upd' });
                $scope.updateState(data.address.SelectedCountry);
            });


        })
        $scope.Remove = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                ApplicationApi.DelAddress({ RefCd: obj[0], RefKey: applId, RefTo: 'appl' }).success(function (data) {
                    Utils.finalResultNotify(data.result);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        };
        $scope.CountryChanged = function (item, value) {
            $scope.updateState(value);
        }
        $scope.updateState = function (value) {
            ApplicationApi.WebGetState({ CountryCd: value }).success(function (item) {
                $scope._Selects.State = item;
            });
        }

    };
    //SOATxnList Controller
    var MiscellaniousController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        ApplicationApi.getFormData({ Prefix: 'mis' }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.ApplId = applId;
        });
        $scope.Save = function () {
            Utils.InfoNotify();
            ApplicationApi.SaveMiscellanious($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            })
        }
    };

    //SOATxnList Controller
    var costcentreController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        $scope.modalOpen = false;
        $scope.isUpdate = false;
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;

        ApplicationApi.getFormData({ Prefix: 'csc' }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "APPL";
            $scope._Object.RefKey = applId;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = Utils.GetDataTable('tblCostCentre', false, '/Account/ftCostCentreList?RefKey=' + applId + '&RefTo=appl', 'indexSelected');
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
            $("span#tblCostCentre-options").css('display', "none");
        }
        $scope.modalClick = function () {
            $scope.isUpdate = false;
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: applId, RefTo: 'appl' });
        }
        $scope.gotoVelocity = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            $location.path('/costcentre/' + applId + '/' + obj[0] + '/velocity');
        }
        $scope.Save = function () {
            Utils.InfoNotify();
            ApplicationApi.CostCentreMaint($scope._Object).success(function (data) {
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
            var obj = { SelectedCostCentre: obj[0], RefKey: applId, RefTo: 'appl' };
            ApplicationApi.FtCostCentreDetail(obj).success(function (data) {
                angular.extend($scope._Object, data, obj);
            });
        });
    };
    //SOATxnList Controller
    var FileManagerController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        $scope.applId = $routeParams.applId || $rootScope.applId;
        $scope.uploadFiles = function () {
            if ($scope.dropZone.getQueuedFiles().length == 0) {
                return toastr.error("please select atleast one image for upload");
            }
            Utils.InfoNotify();
            $scope.dropZone.processQueue();
        }
        $http.get($rootScope.getRootUrl() + '/Applications/GetFiles?ApplId=' + $scope.applId).success(function (data) {
            $scope.files = data.files;
        });


        $scope.deleteFileConfirmation = function (file) {
            $scope.markedFile = file;
        }

        $scope.deleteFile = function (file) {
            var item = $scope.markedFile;
            $http.get($rootScope.getRootUrl() + '/Applications/RemoveFile?FileName=' + file.FileName + '&ApplId=' + $scope.applId).success(function (data) {
                var index = $scope.files.indexOf(item);
                $scope.files.splice(index, 1);
            })

        }
    };
    //SOATxnList Controller
    var approvalController = function ($scope, $rootScope, $location, $routeParams, $timeout, ApplicationApi, $http, Utils) {
        $scope.modalOpen = false;
        ApplicationApi.getFormData({ Prefix: 'apr' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.ApplId = applId;

        });

        ApplicationApi.WebMilestoneHistorySelect({ RefKey: applId }).success(function (data) {
            $scope.approvals = data.result;
            $scope.currentUser = data.user.toLowerCase();
        });

        $scope.modalClick = function (item) {
            $scope.selectedItem = item;
            $scope._Object.SelectedTaskNo = null;
            $scope.modalOpen = true;
            $scope._Object.selectedOwner = item.selectedOwner;
            ApplicationApi.GetTaskNo({ CurrentTaskNo: item.SelectedTaskNo }).success(function (data) {
                $scope._Selects.TaskNo = data.result;
            });

            ApplicationApi.ftMilestoneInfo({ SelectedTaskNo: item.SelectedTaskNo, ApplId: applId }).success(function (data) {
                $scope._Object = data.result;
                $scope._Object.ID = item.ID;
                $scope._Object.Url = $scope._Object.Url.replace("#/", "");
                $scope._Object.Url = $scope._Object.Url.replace('{{$root.obj.applId}}', applId);
            });
        };

        $scope.moveToUrl = function () {
            $scope.modalOpen = false;
            var url = $scope._Object.Url;
            $location.path(encodeURI(url));
        }

        $scope.saveWorkflow = function () {
            Utils.InfoNotify();
            $scope._Object.ApplId = applId;

            ApplicationApi.SaveMilestone($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    // find the item by index and update the status
                    var index = $scope.approvals.indexOf($scope.selectedItem);
                    $scope.selectedItem.selectedStatus = $scope._Object.selectedStatus;
                    $scope.approvals[index] = $scope.selectedItem;

                    ApplicationApi.WebMilestoneHistorySelect({ RefKey: applId }).success(function (data) {
                        $scope.approvals = data.result;
                        $scope.currentUser = data.user.toLowerCase();
                    });

                }
            });
        };
    };
    //inject service
    indexController.$inject = injectParams;
    generalInfoController.$inject = injectParams;
    velocityController.$inject = injectParams;
    caoController.$inject = injectParams;
    depositInfoController.$inject = injectParams;
    vehiclesController.$inject = injectParams;
    skdsController.$inject = injectParams;
    contactsController.$inject = injectParams;
    applicantsController.$inject = injectParams;
    addressController.$inject = injectParams;
    MiscellaniousController.$inject = injectParams;
    costcentreController.$inject = injectParams;
    FileManagerController.$inject = injectParams;
    approvalController.$inject = injectParams;

    angular.module('CardtrendApp').controller('indexController', indexController);
    angular.module('CardtrendApp').controller('generalInfoController', generalInfoController);
    angular.module('CardtrendApp').controller('velocityController', velocityController);
    angular.module('CardtrendApp').controller('caoController', caoController);
    angular.module('CardtrendApp').controller('depositInfoController', depositInfoController);
    angular.module('CardtrendApp').controller('vehiclesController', vehiclesController);
    angular.module('CardtrendApp').controller('skdsController', skdsController);
    angular.module('CardtrendApp').controller('contactsController', contactsController);
    angular.module('CardtrendApp').controller('applicantsController', applicantsController);
    angular.module('CardtrendApp').controller('addressController', addressController);
    angular.module('CardtrendApp').controller('MiscellaniousController', MiscellaniousController);
    angular.module('CardtrendApp').controller('costcentreController', costcentreController);
    angular.module('CardtrendApp').controller('FileManagerController', FileManagerController);
    angular.module('CardtrendApp').controller('approvalController', approvalController);
}());
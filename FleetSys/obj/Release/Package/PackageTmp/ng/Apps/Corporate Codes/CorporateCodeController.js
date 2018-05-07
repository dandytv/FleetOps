(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$routeParams', 'Api', 'Utils'];
    var indexController = function ($scope, $rootScope, $location, $routeParams, Api, Utils) {
        $rootScope.obj._type = 'index';
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblCorporateCodes',
            ajax: $rootScope.getRootUrl() + '/CorporateCodes/ftGetCorpAcctList',
            "aoColumnDefs": [
                              { "sClass": "text-right", "aTargets": [2] },
            ],
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
        $scope.$on("indexSelected", function (event, aData) {
            $rootScope.obj.corpCd = aData[0];
            $location.path('/details/' + aData[0]);
            $scope.$apply();
        });
    };
    var detailsController = function ($scope, $rootScope, $location, $routeParams, Api, Utils) {
        $scope._Object = null;
        var corpCd = $routeParams.corpCd || $rootScope.obj.corpCd;
        $scope.isUpdate = corpCd != null ? true : false;

        $scope.isNew = corpCd ? false : true;
        Api.getFormData({ Prefix: 'corp', CorpCd: corpCd }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $rootScope.obj.corpCd = corpCd;
            $scope._Object.func = "U";
        });

        $scope.PostCorporateCode = function () {
            Utils.InfoNotify();
            $scope._Object.func = $scope.isNew ? 'N' : 'U';
            Api.postCorporateCode($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    if ($scope.isNew) {
                        $rootScope.obj.corpCd = $scope._Object.CorpCd;
                        $rootScope.obj._type = 'edit';
                        $scope.isNew = false;
                        //$scope._Object.CorpCd = data.CorpCd;
                        $location.path('/details/' + $scope._Object.CorpCd); //data.CorpCd
                        $scope.$apply();
                    }
                }
            });
        }
    };
    var velocityController = function ($scope, $rootScope, $location, $routeParams, Api, Utils) {
        var CorpCd = $routeParams.corpCd || $rootScope.corpCd;
        $scope.modalOpen = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblVelocity',
            ajax: $rootScope.getRootUrl() + '/Applications/ftVelocityList?SelectedCorpCd=' + CorpCd,
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }, aoColumnDefs: [
            { "bVisible": false, "aTargets": [0] },
            ]
        };


        Api.getFormData({ Prefix: 'vel' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.Func = true;

            $scope._Selects = data.Selects;
        });
        $scope.newVelocity = function () {
            $scope.modalOpen = true;

            Utils.makeObjectNull($scope._Object, { SelectedCorpCd: CorpCd, Func: "Add" });


        }
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.GetVelocityLimit({ SelectedCorpCd: CorpCd, SelectedVelocityInd: obj[11], SelectedProdCd: obj[12] }).success(function (data) {
                $scope._Object = data.velocity;
                $scope._Object.Func = "Upd";
                $scope._Object.SelectedCorpCd = CorpCd;
            });
        });

        $scope.SaveVelocityLimit = function () {
            Utils.InfoNotify();
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
                Api.RemoveVelocity({ corpCd: CorpCd, VelInd: obj[11] }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    var contactsController = function ($scope, $rootScope, $location, $routeParams, Api, Utils) {
        var corpCd = $routeParams.corpCd || $rootScope.corpCd;
        $scope.modalOpen = false;
        $scope.deleteModalOpen = false;
        $scope.isUpdate = false;
        Api.getFormData({ Prefix: 'con' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.RefTo = "CORP";
            $scope._Object.RefKey = corpCd;

        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblContacts',
            ajax: $rootScope.getRootUrl() + '/Applications/ftContactList?RefTo=CORP&RefKey=' + corpCd,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            rowClick: function (aData) {
            }
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: corpCd, RefTo: 'CORP', Func: "Add" });
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
                Api.ResendActivationEmail({ CorpCd: corpCd, UserId: obj[5] }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                });
            }
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            $scope.isUpdate = true;
            Api.ftContactDetail({ RefCd: obj[0], RefKey: corpCd, RefTo: 'CORP' }).success(function (data) {
                $scope._Object = data.contact;
                $scope._Object.RefTo = "CORP";
                $scope._Object.Func = "Upd";
                $scope._Object.RefKey = corpCd;
            });
        });
        $scope.deleteRecord = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.RemoveContact({ RefCd: obj[0], RefKey: corpCd, RefTo: 'CORP' }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    var addressController = function ($scope, $rootScope, $location, $routeParams, Api, Utils) {
        $scope.modalOpen = false;
        var corpCd = $routeParams.corpCd || $rootScope.applId;
        Api.getFormData({ Prefix: 'add' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "CORP";
            $scope._Object.RefKey = corpCd;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblAddress',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            aoColumnDefs: [
          { "sWidth": '200px', "aTargets": [3, 4, 5, 6, 7] },
            ],
            ajax: $rootScope.getRootUrl() + '/Applications/ftAddressList?RefKey=' + corpCd + '&RefTo=corp'
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: corpCd, RefTo: 'corp', Func: 'Add' });
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
            Api.ftAddressDetail({ RefCd: obj[0], RefKey: corpCd, RefTo: 'corp' }).success(function (data) {
                $scope._Object = data.address;
                angular.extend($scope._Object, { RefKey: corpCd, RefTo: 'corp', RefCd: obj[0], Func: 'Upd' });
                $scope.updateState(data.address.SelectedCountry);
            });
        })


        $scope.Remove = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.DelAddress({ RefCd: obj[0], RefKey: corpCd, RefTo: 'corp' }).success(function (data) {
                    Utils.finalResultNotify(data.result);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }

        $scope.CountryChanged = function (item, value) {
            $scope.updateState(value);
        }
        $scope.updateState = function (value) {
            Api.WebGetState({ CountryCd: value }).success(function (item) {
                $scope._Selects.State = item;
            });
        }
    };
    var depositInfoController = function ($scope, $rootScope, $location, $routeParams, Api, Utils) {
        var corpCd = $routeParams.corpCd ? $routeParams.corpCd : $rootScope.corpCd;
        $scope.show = false;
        $scope.modalOpen = false;
        Api.getFormData({ Prefix: 'dep' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.CorpCd = corpCd;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblDepositInfo',
            ajax: $rootScope.getRootUrl() + '/Applications/ftGetAcctDepositInfoList?CorpCd=' + corpCd,
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
            id: 'tblSecurityDepositOptions',
            ajax: $rootScope.getRootUrl() + '/Account/WebAcctHistoryListSelect?AcctNo=' + corpCd + '&&type=sec',
            edit: {
                level: 'scope',
                func: 'securitySelected'
            }
        };

        $scope.refresh = function myfunction() {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.ftGetGetAcctDepositInfoDetail({ CorpCd: corpCd, TxnId: obj[6] }).success(function (data) {
                $scope._Object = data.Adi;
                $scope._Object.CorpCd = corpCd;
            });
        });
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { CorpCd: corpCd });
        }
        $scope.SaveAcctDepositInfoOps = function () {
            Utils.InfoNotify();
            Api.SaveAcctDepositInfoOps($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $rootScope.tables[$scope.dtSecurityDepositOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }
    };
    var productDiscountController = function ($scope, $rootScope, $location, $routeParams, Api, Utils) {
        $scope.modalOpen = false;
        $scope.searchModalOpen = false;
        $scope.deleteModalOpen = false;
        var acctNo = $routeParams.corpCd ? $routeParams.corpCd : $rootScope.obj.corpCd;
        //var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'pdc', AcctNo: acctNo }).success(function (data) {//, '/Account'
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "CORP";
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
            ajax: $rootScope.getRootUrl() + '/Account/WebProductDiscountListSelect?RefTo=CORP&AcctNo=' + acctNo,

            rowCallback: function (aData, nRow) {
                $scope.searchModalOpen = false;
            }
        };

        $scope.SubsidyChanged = function (obj, val) {
            //
            if (val) {
                Api.GetPlanIdFromSubsidy({ SelectedProdDiscType: val }).success(function (data) {
                    $scope._Selects.PlanId = data.PlanId;
                })
            }
        }

        $scope.searchProductDiscount = function () {
            obj = $scope._Object;
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var prefix = $rootScope.getRootUrl() + '/Account/WebProductDiscountListSelect?';
            var queryString = $.param({
                DiscType: obj.SelectedProdDiscType, AcctNo: acctNo, RefTo: "CORP"
            });
            var value = angular.extend($scope.dtOptions, { serverSide: true, ajax: prefix + queryString, destroy: true });
            $scope.$broadcast('updateDataTable', { options: value });
        };

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { Func: "N", AcctNo: acctNo, RefTo: "CORP" });
        }

        $scope.Save = function () {
            Utils.InfoNotify();
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
            Api.ProductDiscountSelect({ DiscType: obj[2], AcctNo: acctNo, Id: obj[10], RefTo: 'CORP' }).success(function (data) {
                $scope._Object = data.Discount;
                $scope._Object.Func = "E";
                $scope._Object.AcctNo = acctNo;
                $scope._Object.RefTo = 'CORP';
            });
        })
        $scope.Remove = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.DeleteProductDiscount({ SelectedProdCd: obj[0], SelectedProdDiscType: obj[2], EffDateFrom: obj[5], RefKey: acctNo, AcctNo: acctNo, RefTo: "CORP" }).success(function (data) { //'acct'
                    Utils.finalResultNotify(data.result);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    var AcctCorpListController = function ($scope, $rootScope, $location, $routeParams, Api, Utils) {
        var corpCd = $routeParams.corpCd ? $routeParams.corpCd : $rootScope.corpCd;
        $rootScope.obj._type = 'edit';
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblAcctCorp',
            ajax: $rootScope.getRootUrl() + '/CorporateCodes/ftAcctCorpList?CorpCd=' + corpCd,
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.$on("indexSelected", function (event, aData) {
            location.href = $rootScope.getRootUrl() + "/Account?id=" + aData[0];
            $scope.$apply();
        });
    };
    var userManagementController = function ($scope, $rootScope, $location, $routeParams, Api, Utils) {
        var CorpCd = $routeParams.corpCd ? $routeParams.corpCd : $rootScope.obj.corpCd;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblusers',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            ajax: $rootScope.getRootUrl() + '/Account/AccountUsersListSelect?CorpCd=' + CorpCd
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.ResendActivationEmail = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.ResendActivationEmail({ UserId: obj[0], CorpCd: CorpCd }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                });
            }
        }
        $scope.ResetPasswordCounter = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.ResetPasswordCounter({ AcctNo: obj[1], UserId: obj[0], CorpCd: CorpCd }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                });
            }
        }
    };


    //inject service
    indexController.$inject = injectParams;
    detailsController.$inject = injectParams;
    velocityController.$inject = injectParams;
    contactsController.$inject = injectParams;
    addressController.$inject = injectParams;

    depositInfoController.$inject = injectParams;
    productDiscountController.$inject = injectParams;
    AcctCorpListController.$inject = injectParams;
    userManagementController.$inject = injectParams;

    angular.module('CardtrendApp').controller('indexController', indexController);
    angular.module('CardtrendApp').controller('detailsController', detailsController);
    angular.module('CardtrendApp').controller('velocityController', velocityController);
    angular.module('CardtrendApp').controller('contactsController', contactsController);
    angular.module('CardtrendApp').controller('addressController', addressController);

    angular.module('CardtrendApp').controller('depositInfoController', depositInfoController);
    angular.module('CardtrendApp').controller('productDiscountController', productDiscountController);
    angular.module('CardtrendApp').controller('AcctCorpListController', AcctCorpListController);
    angular.module('CardtrendApp').controller('userManagementController', userManagementController);
}());
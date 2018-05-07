(function () {
    var injectParams = ['$scope', '$rootScope', '$location', '$routeParams', 'Utils', 'Api'];

    var generalInfoController = function ($scope, $rootScope, $location, $routeParams, Utils, Api) {
        $scope._Object = null;
        $rootScope.obj._type == 'new';
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.obj.applId;
        var appcId = $rootScope.obj.appcId;
        $scope.isNew = $rootScope.obj._type;

        Api.getFormData({ Prefix: 'gen', ApplId: applId, AppcId: appcId }, $rootScope.getRootUrl() + '/Applicant').success(function (data) {
            $scope._Object = data.Model;
            $rootScope.obj.entityId = data.Model.EntityId;
            $scope._Selects = data.Selects;
        });

        $scope.CardFee = function (obj, val) {
            console.log($scope._Object);
            if (val == 2) {
                $scope._Object.SelectedJoining = "201";
            }
            else {
                $scope._Object.SelectedJoining = null;
            }

        }

        $scope.postGeneralInfo = function () {
            Utils.InfoNotify();
            delete $scope._Object.AppcId;
            angular.extend($scope._Object, { _ApplID: applId, _AppcID: $rootScope.obj.appcId });
            Api.postGeneralInfo($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    if ($scope.isNew) {
                        $rootScope.obj.applId = applId;
                        $rootScope.obj._type = 'edit';
                        $rootScope.obj.appcId = data.AppcID;
                        $scope.isNew = false;
                        $scope._Object.AppcId = data.AppcID;
                        $location.path('/' + applId + '/' + data.AppcID + '');
                    } else {
                        $location.path('/' + applId + '/' + data.AppcID + '');
                    }
                }
            });
        }

    };
    //Index Controller
    var indexController = function ($scope, $rootScope, $location, $routeParams, Utils, Api) {
        $rootScope.obj._type = 'index';
        $scope._Object = null;
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.obj.applId;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": false,
            id: 'tblVelocity',
            ajax: $rootScope.getRootUrl() + '/Applicant/ftCardHolderList?_ApplicationId=' + applId,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };
        $scope.$on('indexSelected', function (event, obj) {
            //   $location.path('#/' + applId + '/' + obj[0]);

            $rootScope.obj.appcId = obj[0];
            $rootScope.obj._type = 'edit';
            $location.path('/' + $rootScope.obj.applId + '/' + obj[0]);
            $scope.$apply();
        });
    };
    //Financial Controller
    var financialInfoController = function ($scope, $rootScope, $location, $routeParams, Utils, Api) {
        $scope._Object = null;
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.obj.applId;
        var appcId = $routeParams.appcId ? $routeParams.appcId : $rootScope.obj.appcId;
        Api.getFormData({ Prefix: 'fin', ApplId: applId, AppcId: appcId }, $rootScope.getRootUrl() + '/Applicant').success(function (data) {
            $scope._Object = data.Model;
            $scope._Object._AppcId = appcId;
            $scope._Selects = data.Selects;
        });
        $scope.SaveFinancialInfo = function () {
            Utils.InfoNotify();
            Api.SaveFinancialInfo($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            });
        }
    };
    //Personal Information Controller
    var personInfoController = function ($scope, $rootScope, $location, $routeParams, Utils, Api) {
        $scope._Object = null;
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.obj.applId;
        var appcId = $routeParams.appcId ? $routeParams.appcId : $rootScope.obj.appcId;
        var entityId = $routeParams.entityId ? $routeParams.entityId : $rootScope.obj.entityId;

        Api.getFormData({ Prefix: 'per', ApplId: applId, AppcId: appcId, EntityId: entityId }, $rootScope.getRootUrl() + '/Applicant').success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.EntityId = entityId;
            $scope._Selects = data.Selects;
        });
        $scope.savePersonInfo = function () {
            Utils.InfoNotify();
            Api.savePersonInfo($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            });
        }
    };
    //Velocity Controller
    var velocityController = function ($scope, $rootScope, $location, $routeParams, Utils, Api) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        var appcId = $routeParams.appcId ? $routeParams.appcId : $rootScope.obj.appcId;
        $scope.modalOpen = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblVelocity',
            ajax: $rootScope.getRootUrl() + '/Applications/ftVelocityList?AppcId=' + appcId,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            aoColumnDefs: [
            { "bVisible": false, "aTargets": [0] },
            ]
            //  aoColumnDefs: [
            //{ "bVisible": false, "aTargets": [6] },
            //  { "bVisible": false, "aTargets": [7] }
            //  ]
        };


        Api.getFormData({ Prefix: 'vel' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefKey = appcId;
            $scope._Object.AppcId = appcId;
            $scope._Selects = data.Selects;
        });
        $scope.newVelocity = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { AppcId: appcId, Func: "Add" });
        }
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.GetVelocityLimit({ AppcId: appcId, SelectedVelocityInd: obj[11], SelectedProdCd: obj[12] }).success(function (data) {
                $scope._Object = data.velocity;
                $scope._Object.Func = "Upd";
                $scope._Object.RefKey = appcId;
                $scope._Object.AppcId = appcId;
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
                Api.RemoveVelocity({ AppcId: appcId, VelInd: obj[11], ProdCd: obj[12] }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    // Contact controller
    var contactsController = function ($scope, $rootScope, $location, $routeParams, Utils, Api) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        var appcId = $routeParams.appcId ? $routeParams.appcId : $rootScope.obj.appcId;
        $scope.modalOpen = false;
        $scope.isUpdate = false;

        Api.getFormData({ Prefix: 'con' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.RefTo = "APPC";
            $scope._Object.RefKey = appcId;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblContacts',
            ajax: $rootScope.getRootUrl() + '/Applications/ftContactList?RefTo=appc&RefKey=' + appcId,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: appcId, RefTo: 'appc', Func: "Add" });
        }

        $scope.deleteRecord = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.RemoveContact({ RefCd: obj[0], RefKey: appcId, RefTo: 'appc' }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
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

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            $scope.isUpdate = true;
            Api.ftContactDetail({ RefCd: obj[0], RefKey: appcId, RefTo: 'appc' }).success(function (data) {
                $scope._Object = data.contact;
                $scope._Object.RefKey = appcId;
                $scope._Object.RefTo = 'appc';
                $scope._Object.RefCd = obj[0];
                $scope._Object.Func = "Upd";
            });
        })

    };
    // Address controller
    var addressController = function ($scope, $rootScope, $location, $routeParams, Utils, Api) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        var appcId = $routeParams.appcId ? $routeParams.appcId : $rootScope.obj.appcId;
        $scope.modalOpen = false;
        Api.getFormData({ Prefix: 'add' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "appc";
            $scope._Object.RefKey = appcId;
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
            ajax: $rootScope.getRootUrl() + '/Applications/ftAddressList?RefKey=' + appcId + '&RefTo=appc'
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: appcId, RefTo: 'appc', Func: 'Add' });
        }

        $scope.Save = function () {
            Utils.InfoNotify();
            Api.SaveAddress($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                $rootScope.tables[$scope.dtOptions.id].fnDraw();
                if (data.resultCd.flag != 1) {
                    $scope.modalOpen = false;
                }
            });
        }

        $scope.Remove = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.DelAddress({ RefCd: obj[0], RefKey: appcId, RefTo: 'appc' }).success(function (data) {
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


        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            Api.ftAddressDetail({ RefCd: obj[0], RefKey: appcId, RefTo: 'appc' }).success(function (data) {
                $scope._Object = data.address;
                angular.extend($scope._Object, { RefKey: appcId, RefTo: 'appc', RefCd: obj[0], Func: 'Upd' });
                $scope.updateState(data.address.SelectedCountry);
            });
        })
    };
    //Status controller
    var statusMaintController = function ($scope, $rootScope, $location, $routeParams, Utils, Api) {
        var applId = $routeParams.applId ? $routeParams.applId : $rootScope.applId;
        var appcId = $routeParams.appcId ? $routeParams.appcId : $rootScope.obj.appcId;
        $scope.modalOpen = false;
        Api.getFormData({ Prefix: 'sts', AppcId: appcId }, $rootScope.getRootUrl() + '/Applicant').success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.AppcId = appcId;
            $scope._Selects = data.Selects;
        });
        $scope.Save = function () {
            Utils.InfoNotify();
            Api.SaveChangedStatus($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $scope._Object.SelectedCurrentStatus = $scope._Object.SelectedChangeStatusTo;
                }
            });
        }
    };
    //inject service
    generalInfoController.$inject = injectParams;
    indexController.$inject = injectParams;
    financialInfoController.$inject = injectParams;
    personInfoController.$inject = injectParams;
    velocityController.$inject = injectParams;
    contactsController.$inject = injectParams;
    addressController.$inject = injectParams;
    statusMaintController.$inject = injectParams;

    angular.module('CardtrendApp').controller('generalInfoController', generalInfoController);
    angular.module('CardtrendApp').controller('indexController', indexController);
    angular.module('CardtrendApp').controller('financialInfoController', financialInfoController);
    angular.module('CardtrendApp').controller('personInfoController', personInfoController);
    angular.module('CardtrendApp').controller('velocityController', velocityController);
    angular.module('CardtrendApp').controller('contactsController', contactsController);
    angular.module('CardtrendApp').controller('addressController', addressController);
    angular.module('CardtrendApp').controller('statusMaintController', statusMaintController);
}());
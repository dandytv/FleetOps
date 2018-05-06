(function () {
    var injectParams = ['$scope', '$routeParams', '$rootScope','$location','Api','Utils'];
    var injectParams2 = ['$scope', '$rootScope', '$routeParams', 'Api', 'Utils'];

    var indexController = function ($scope, $routeParams, $rootScope, $location, Api, Utils) {
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblIndex',
            ajax: $rootScope.getRootUrl() + '/Merchant/ftMechAggreementList?' + +$.param,
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };

        $scope.$on('indexSelected', function (e, obj) {
            $rootScope.obj.acctNo = obj[0];
            $location.path('/generalInfo/' + obj[0]);
            $scope.$apply();
        })
    };
    var generalInfoController = function ($scope, $routeParams, $rootScope, $location, Api, Utils) {

        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.isNew = acctNo ? false : true;//add
        Api.getFormData({ Prefix: 'gen', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });
        $scope.postGeneralInfo = function () {
            Utils.InfoNotify();
            $scope._Object.AcctNo = acctNo;
            Api.postGeneralInfo($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    if ($scope.isNew) {
                        $rootScope.obj.acctNo = data.AcctNo;
                        $rootScope.obj._type = 'edit';
                        $scope.isNew = false;
                        $scope._Object.AcctNo = data.AcctNo;
                        $scope._Object.SelectedCurrentStatus = "P";
                    }
                    $location.path('/generalInfo/' + data.AcctNo + '');
                }
            });
        }
    };
    var busnLocationController = function ($scope, $routeParams, $rootScope, $location, Api, Utils) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblBusnLocations',
            ajax: $rootScope.getRootUrl() + '/Dealer/ftBusinessLocationList?AcctNo=' + acctNo,
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.$on('indexSelected', function (e, obj) {
            location.href = encodeURI($rootScope.getRootUrl() + '/Dealer/Index/#/generalInfo/' + acctNo + '/' + obj[0]);
        })
        $scope.new = function () {
            location.href = encodeURI($rootScope.getRootUrl() + '/Dealer/Index/#/new/' + acctNo);
        }
    };

    var contactsController = function ($scope, $rootScope, $routeParams, Api, Utils) {
        var acctNo = $routeParams.acctNo || $rootScope.acctNo;
        $scope.modalOpen = false;
        $scope.deleteModalOpen = false;
        $scope.isUpdate = false;
        Api.getFormData({ Prefix: 'con' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.RefTo = "MERCH";
            $scope._Object.RefKey = acctNo;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblContacts',
            ajax: $rootScope.getRootUrl() + '/Applications/ftContactList?RefTo=merch&RefKey=' + acctNo,
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
            Utils.makeObjectNull($scope._Object, { RefKey: acctNo, RefTo: 'merch', Func: "Add" });
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
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            $scope.isUpdate = true;
            Api.ftContactDetail({ RefCd: obj[0], RefKey: acctNo, RefTo: 'merch' }).success(function (data) {
                $scope._Object = data.contact;
                $scope._Object.RefKey = acctNo;
                $scope._Object.RefTo = 'merch';
                $scope._Object.RefCd = obj[0];
                $scope._Object.Func = "Upd";
            });
        });
        $scope.deleteRecord = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.RemoveContact({ RefCd: obj[0], RefKey: acctNo, RefTo: 'merch' }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    var addressController = function ($scope, $rootScope, $routeParams, Api, Utils) {
        $scope.modalOpen = false;
        var acctNo = $routeParams.acctNo || $rootScope.acctNo;
        Api.getFormData({ Prefix: 'add' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "merch";
            $scope._Object.RefKey = acctNo;
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
            ajax: $rootScope.getRootUrl() + '/Applications/ftAddressList?RefKey=' + acctNo + '&RefTo=merch'
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: acctNo, RefTo: 'merch', Func: 'Add' });
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

            Api.ftAddressDetail({ RefCd: obj[0], RefKey: acctNo, RefTo: 'merch' }).success(function (data) {
                $scope._Object = data.address;
                angular.extend($scope._Object, { RefKey: acctNo, RefTo: 'merch', RefCd: obj[0], Func: 'Upd' });
                $scope.updateState(data.address.SelectedCountry);
            });
        })
        $scope.Remove = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.DelAddress({ RefCd: obj[0], RefKey: acctNo, RefTo: 'merch' }).success(function (data) {
                    Utils.finalResultNotify(data.result);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    };
    var statusMaintController = function ($scope, $rootScope, $routeParams, Api, Utils) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'sts', AcctNo: acctNo, refCd: 'MERCH' }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.AccNo = acctNo;
            $scope._Selects = data.Selects;
        });
        $scope.Save = function () {
            Utils.InfoNotify();
            $scope._Object.MerchAcctNo = acctNo;
            $scope._Object.AccNo = null;
            Api.FtChangedStatusMaint($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                var SelectedCurrentStatus = '';
                for (var i = 0; i < $scope._Object.ChangeStatusTo.length; i++) {
                    if ($scope._Object.ChangeStatusTo[i].Value == $scope._Object.SelectedChangeStatusTo) {
                        var arr = $scope._Object.ChangeStatusTo[i].Text.split(' ');
                        SelectedCurrentStatus = arr[1];
                    }

                }
                $scope._Object.SelectedCurrentStatus = SelectedCurrentStatus;
            });
        }
    };
    var txnSearchController = function ($scope, $rootScope, $routeParams, Api, Utils) {
        $scope._Object = null;
        $scope.dtOptions = {
            serverSide: false,
            processing: true,
            scrollX: true,
            autoWidth: false,
            retrieve: true,
            id: 'tblTxnSearch',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        Api.getFormData({ Prefix: 'mps', AcctNo: acctNo }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.AccNo = acctNo;
            $scope._Selects = data.Selects;
        });
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: acctNo, RefTo: 'acct' });
        }
        $scope.searchTxn = function () {
            obj = $scope._Object;
            obj.AcctNo = acctNo;
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var prefix = $rootScope.getRootUrl() + '/Merchant/ftGetMerchtPostedTxnSearchList?';
            var queryString = $.param(obj);
            var value = angular.extend($scope.dtOptions, {
                serverSide: true, ajax: prefix + queryString, destroy: true,
                "drawCallback": function (settings) {
                    $scope.modalOpen = false;
                    $scope.$apply();
                }
            });
            $scope.$broadcast('updateDataTable', { options: value });
        }
    };

    //inject service
    indexController.$inject = injectParams;
    generalInfoController.$inject = injectParams;
    busnLocationController.$inject = injectParams;

    contactsController.$inject = injectParams2;
    addressController.$inject = injectParams2;
    statusMaintController.$inject = injectParams2;
    txnSearchController.$inject = injectParams2;

    angular.module('MerchantApp').controller('indexController', indexController);
    angular.module('MerchantApp').controller('generalInfoController', generalInfoController);
    angular.module('MerchantApp').controller('busnLocationController', busnLocationController);

    angular.module('MerchantApp').controller('contactsController', contactsController);
    angular.module('MerchantApp').controller('addressController', addressController);
    angular.module('MerchantApp').controller('statusMaintController', statusMaintController);
    angular.module('MerchantApp').controller('txnSearchController', txnSearchController);
}());
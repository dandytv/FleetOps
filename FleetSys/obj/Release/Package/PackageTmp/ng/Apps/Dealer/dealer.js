/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
/// <reference path="account.js" />
angular.module('dealerApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};
    $rootScope.obj.merchantId = $('#hdMerchantId').val();


    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });


    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();
        if (_location.indexOf('/new') >= 0) {
            $rootScope.obj._type = 'new';
            $rootScope.obj.acctNo = $routeParams.acctNo;


        } else if (_location == '/') {
            $rootScope.obj._type = 'index';
        } else {
            $rootScope.obj.acctNo = $routeParams.acctNo;
            $rootScope.obj.dealerId = $routeParams.dealerId;
            $rootScope.obj._type = 'edit';
        }
        //$rootScope.$broadcast('routeChanged', _location);
    });
})

    .factory('Api', function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/Dealer';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: ApiRef
            });
        };
        getFormData = function (obj, url) {
            var ApiRef = url || Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/FillData?' + params
            })
        };
        postGeneralInfo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Dealer/SaveBusnLocationGeneralInfo',
                method: 'POST',
                data: obj
            });
        };


        ftAddressDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftAddressDetail?' + Params,
                method: 'GET'
            });
        }
        SaveAddress = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveAddress',
                method: 'POST',
                data: obj
            });
        }
        DelAddress = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/DelAddress?' + Params,
                method: 'GET'
            });
        }
        WebGetState = function (obj, url) {
            return $http({
                method: 'GET',
                url: $rootScope.getRootUrl() + '/Applications/WebGetState?' + $.param(obj)
            })
        };
        ftContactDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftContactDetail?' + Params,
                method: 'GET'
            });
        }
        SaveContact = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveContact',
                method: 'POST',
                data: obj
            });
        }
        RemoveContact = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/DelContact?' + Params,
                method: 'GET'
            });
        }

        ftBusnLocTermDetail = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Dealer/ftBusnLocTermDetail?' + Params,
                method: 'GET'
            });
        }
        SaveTermInventory = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Dealer/SaveTermInventory',
                method: 'POST',
                data: obj
            });
        }

        FtChangedStatusMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Account/FtChangedStatusMaint',
                method: 'POST',
                data: obj
            });
        }

        //////////////////////////////////////
        ////Account users
        //////////////////////////////////////


        ////////////////////////////////////
        /////Change Ownwership/////////////
        ///////////////////////////////////
        WebMerchChgOwnershipMaint = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Dealer/WebMerchChgOwnershipMaint',
                method: 'POST',
                data: obj
            });
        }


        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            getFormData: getFormData, // fill all the selects and radios
            //////////////////////////////////////////////////////////////
            //Application Index page
            //////////////////////////////////////////////////////////////
            //Application General Info
            /////////////////////////////////////////////////////////////
            postGeneralInfo: postGeneralInfo,
            ///////////////////////////////////////////////////////
            /////////////////////////
            //Address
            ////////////////////////
            ftAddressDetail: ftAddressDetail,
            SaveAddress: SaveAddress,
            WebGetState:WebGetState,
            ftContactDetail: ftContactDetail,
            RemoveContact: RemoveContact,
            SaveContact: SaveContact,
            DelAddress: DelAddress,
            FtChangedStatusMaint: FtChangedStatusMaint,
            SaveTermInventory: SaveTermInventory,
            ftBusnLocTermDetail: ftBusnLocTermDetail,
            ///////////////////////////////////////////////////
            ///Cahnge Ownership////////////////////////
            ////////////////////////////////////////////////
            WebMerchChgOwnershipMaint: WebMerchChgOwnershipMaint

        }
    })
    .config(function ($routeProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/:acctNo?', {
            templateUrl: 'index.html',
            controller: 'indexController'
        })
        .when('/new/:acctNo?', {
            templateUrl: rootUrl + '/Dealer/Tmpl?Prefix=gen&type=Index',
            controller: 'generalInfoController',
            pageKey: 'generalInfo'
        })
        .when('/generalInfo/:acctNo?/:dealerId?', {
            templateUrl: rootUrl + '/Dealer/Tmpl?Prefix=gen&type=Index',
            controller: 'generalInfoController',
            pageKey: 'generalInfo'
        })
        .when('/terminals/:acctNo?/:dealerId?', {
            templateUrl: rootUrl + '/Dealer/Tmpl?Prefix=ter&type=Index',
            controller: 'terminalController',
            pageKey: 'terminals'
        })
        .when('/contacts/:acctNo?/:dealerId?', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=con&overrideController=Dealer&type=Index',
            controller: 'contactsController',
            pageKey: 'contacts'
        })
         .when('/address/:acctNo?/:dealerId?', {
             templateUrl: rootUrl + '/Applications/Tmpl?Prefix=add&overrideController=Dealer&type=Index',
             controller: 'addressController',
             pageKey: 'address'
         })
        .when('/statusMaint/:acctNo?/:dealerId?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=sta&overrideController=Dealer&type=Index',
            controller: 'statusMaintController',
            pageKey: 'statusMaint'
        })
        .when('/prodprize/:acctNo?/:dealerId?', {
            templateUrl: rootUrl + '/Dealer/Tmpl?Prefix=prs&type=Index',
            controller: 'prodPrizeController',
            pageKey: 'prodprize'
        })
         .when('/ownershipChange/:acctNo?/:dealerId?', {
             templateUrl: rootUrl + '/Dealer/Tmpl?Prefix=moc&type=Index',
             controller: 'ownershipChangeController',
             pageKey: 'ownership'
         })
    })
    .controller('indexController', function ($scope, $routeParams, $rootScope, $location, Api, Utils) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblIndex',
            ajax: $rootScope.getRootUrl() + '/Dealer/ftBusinessLocationList?AcctNo=' + acctNo,
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
        $scope.$on('indexSelected', function (e, obj) {
            $rootScope.obj.acctNo = obj[0];
            $location.path('/generalInfo/' + acctNo + '/' + obj[0]);
            $scope.$apply();
        })
    })
.controller('generalInfoController', function ($scope, $routeParams, $rootScope, $location, Api, Utils) {
    $scope._Object = null;
    var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
    var dealerId = $routeParams.dealerId ? $routeParams.dealerId : $rootScope.obj.dealerId;
    $scope.isNew = dealerId ? false : true;//add
    Api.getFormData({ prefix: 'gen', id: dealerId }).success(function (data) {
        $scope._Object = data.Model;
        $scope._Selects = data.Selects;
    });

    $scope.postGeneralInfo = function () {
        Utils.InfoNotify();
        $scope._Object.AcctNo = acctNo;
        Api.postGeneralInfo($scope._Object).success(function (data) {
            Utils.finalResultNotify(data.result);
            if (data.result.flag == 0) {
                if ($scope.isNew) {
                    $rootScope.obj.dealerId = data.BusnLoc;
                    $scope._Object.BusnLoc = data.BusnLoc;
                    $rootScope.obj._type = 'edit';
                    $scope.isNew = false;
                }
                $location.path('/generalInfo/' + acctNo + '/' + data.BusnLoc);
            }
        });
    }
})
.controller('contactsController', function ($scope, $rootScope, $routeParams, Api, Utils) {
    var acctNo = $routeParams.acctNo || $rootScope.acctNo;
    var dealerId = $routeParams.dealerId ? $routeParams.dealerId : $rootScope.obj.dealerId;
    $scope.modalOpen = false;
    $scope.deleteModalOpen = false;
    Api.getFormData({ Prefix: 'con' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
        //append all selects here...
        $scope._Object = data.Model;
        $scope._Selects = data.Selects;
        $scope._Object.RefTo = "busn";
        $scope._Object.RefKey = acctNo;
    });
    $scope.dtOptions = {
        serverSide: true,
        processing: true,
        scrollX: false,
        id: 'tblContacts',
        ajax: $rootScope.getRootUrl() + '/Applications/ftContactList?RefTo=busn&RefKey=' + dealerId,
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
        Utils.makeObjectNull($scope._Object, { RefKey: dealerId, RefTo: 'busn', Func: "Add" });
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
        Api.ftContactDetail({ RefCd: obj[0], RefKey: dealerId, RefTo: 'busn' }).success(function (data) {
            $scope._Object = data.contact;
            $scope._Object.RefKey = dealerId;
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
            Api.RemoveContact({ RefCd: obj[0], RefKey: dealerId, RefTo: 'busn' }).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                $rootScope.tables[$scope.dtOptions.id].fnDraw();
            });
        }
    }
})
.controller('addressController', function ($scope, $rootScope, $routeParams, Api, Utils) {
    $scope.modalOpen = false;
    var acctNo = $routeParams.acctNo || $rootScope.acctNo;
    var dealerId = $routeParams.dealerId ? $routeParams.dealerId : $rootScope.obj.dealerId;
    Api.getFormData({ Prefix: 'add' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
        //append all selects here...
        $scope._Object = data.Model;
        $scope._Object.RefTo = "busn";
        $scope._Object.RefKey = dealerId;
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
        ajax: $rootScope.getRootUrl() + '/Applications/ftAddressList?RefKey=' + dealerId + '&RefTo=busn'
    };
    $scope.refresh = function () {
        $rootScope.tables[$scope.dtOptions.id].fnDraw();
    }
    $scope.modalClick = function () {
        $scope.modalOpen = true;
        Utils.makeObjectNull($scope._Object, { RefKey: dealerId, RefTo: 'busn', Func: 'Add' });
    }

    $scope.CountryChanged = function (item, value) {
        $scope.updateState(value);
    }
    $scope.updateState = function (value) {
        Api.WebGetState({ CountryCd: value }).success(function (item) {
            $scope._Selects.State = item;
        });
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

        Api.ftAddressDetail({ RefCd: obj[0], RefKey: dealerId, RefTo: 'busn' }).success(function (data) {
            $scope._Object = data.address;
            angular.extend($scope._Object, { RefKey: dealerId, RefTo: 'busn', RefCd: obj[0], Func: 'Upd' });
            $scope.updateState(data.address.SelectedCountry);
        });
    })
    $scope.Remove = function () {
        $scope.deleteModalOpen = false;
        var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
        if (obj) {
            Utils.InfoNotify();
            Api.DelAddress({ RefCd: obj[0], RefKey: dealerId, RefTo: 'busn' }).success(function (data) {
                Utils.finalResultNotify(data.result);
                $rootScope.tables[$scope.dtOptions.id].fnDraw();
            });
        }
    }
})
    .controller('terminalController', function ($scope, $rootScope, $routeParams, Api, Utils) {
        $scope.modalOpen = false;
        var acctNo = $routeParams.acctNo || $rootScope.acctNo;
        var dealerId = $routeParams.dealerId ? $routeParams.dealerId : $rootScope.obj.dealerId;
        Api.getFormData({ Prefix: 'ter' }, $rootScope.getRootUrl() + '/Dealer').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefKey = dealerId;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblTerminals',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            ajax: $rootScope.getRootUrl() + '/Dealer/ftBusnLocTermList?BusnLoc=' + dealerId
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { BusnLocation: dealerId });
        }

        $scope.Save = function () {
            Utils.InfoNotify();
            $scope._Object.BusnLocation = dealerId;
            Api.SaveTermInventory($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;

            Api.ftBusnLocTermDetail({ TermId: obj[0], BusnLocation: dealerId }).success(function (data) {
                $scope._Object = data.term;
                //angular.extend($scope._Object, { RefKey: dealerId, RefTo: 'merch', RefCd: obj[0], Func: 'Upd' });
            });
        })
        //$scope.Remove = function () {
        //    $scope.deleteModalOpen = false;
        //    var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
        //    if (obj) {
        //        Utils.InfoNotify();
        //        Api.DelAddress({ RefCd: obj[0], RefKey: dealerId, RefTo: 'merch' }).success(function (data) {
        //            Utils.finalResultNotify(data.result);
        //            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        //        });
        //    }
        //}
    })
.controller('statusMaintController', function ($scope, $routeParams, $rootScope, Api, Utils) {
    $scope._Object = null;
    var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
    var dealerId = $routeParams.dealerId || $rootScope.obj.dealerId;
    Api.getFormData({ Prefix: 'sts', id: dealerId }).success(function (data) {
        $scope._Object = data.Model;
        $scope._Object.BusnLocation = dealerId;
        $scope._Selects = data.Selects;
    });
    $scope.Save = function () {
        Utils.InfoNotify();
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
})
.controller('prodPrizeController', function ($scope, $routeParams, $rootScope, Api, Utils) {

    var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
    var dealerId = $routeParams.dealerId || $rootScope.obj.dealerId;
    $scope._Object = null;
    $scope.dtOptions = {
        serverSide: false,
        processing: true,
        scrollX: true,
        autoWidth: false,
        retrieve: true,
        id: 'tblProdPrize',
        edit: {
            level: 'scope',
            func: 'indexSelected',
        }
    };
    Api.getFormData({ Prefix: 'prs', AcctNo: acctNo }).success(function (data) {
        $scope._Object = data.Model;
        $scope._Object.AccNo = acctNo;
        $scope._Selects = data.Selects;
    });
    $scope.refresh = function () {
        $rootScope.tables[$scope.dtOptions.id].fnDraw();
    }
    $scope.modalClick = function () {
        $scope.modalOpen = true;
        Utils.makeObjectNull($scope._Object, { BusnLocation: dealerId, isList: false });
    }
    $scope.search = function () {
        obj = $scope._Object;
        obj.AcctNo = acctNo;
        var $table = $rootScope.tables[$scope.dtOptions.id];
        var prefix = $rootScope.getRootUrl() + '/Dealer/WebMerchProductPriceSearch?';
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
})
.controller('ownershipChangeController', function ($scope, $routeParams, $rootScope, $location, Api, Utils) {
    $scope._Object = null; $scope.disableModel = false;
    $scope.manualDisabled = true;
    var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
    var dealerId = $routeParams.dealerId ? $routeParams.dealerId : $rootScope.obj.dealerId;
    Api.getFormData({ prefix: 'own', id: dealerId }).success(function (data) {
        $scope._Object = data.Model;
        $scope._Selects = data.Selects;
       
        if ($scope._Object.FloatAcctInd && $scope._Object.MaskedFlag)
            $scope.disableModel = true;
       else if (!$scope._Object.FloatAcctInd && $scope._Object.MaskedFlag)
           $scope.disableModel = false;
       else if ($scope._Object.FloatAcctInd && !$scope._Object.MaskedFlag)
           $scope.disableModel = true;
       else
           $scope.disableModel = true;

    });

    $scope.dtOptions = {
        serverSide: true,
        processing: true,
        scrollX: false,
        id: 'tblChangeOwnership',
        edit: {
            level: 'scope',
            func: 'indexSelected',
        },
        ajax: $rootScope.getRootUrl() + '/Dealer/WebMerchOwnershipChangeListSelect?DealerId=' + dealerId,
    };

    $scope.changeOwnership = function () {
        Utils.InfoNotify();
        Api.WebMerchChgOwnershipMaint($scope._Object.FloatAcctInd ? {
            FromMerchantId: $scope._Object.FromMerchantId,CurrentSiteId: $scope._Object.CurrentSiteId,
            CutoffDate: $scope._Object.CutoffDate, CutOffTime: $scope._Object.CutOffTime, FloatAcctInd: $scope._Object.FloatAcctInd
        } : $scope._Object).success(function (data) {
            Utils.finalResultNotify(data.result);
            if (data.result.flag == 0) {
                $rootScope.tables[$scope.dtOptions.id].fnDraw();
            }
        });
    }
    $scope.updateModel = function () {
        $scope.manualDisabled = false;
        if ($scope._Object.FloatAcctInd) {
            $scope.disableModel = false;
        } else {
            $scope.disableModel = true;
        }
    }
})
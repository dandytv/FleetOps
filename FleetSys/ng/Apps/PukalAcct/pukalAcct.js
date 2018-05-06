/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
angular.module('PukalAcctApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};

    $rootScope.$on("updatePath", function (event, path) {
        $location.path(path);
    });

    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });

    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();
        if (_location == '/new') {
            $rootScope.obj._type = 'new';
            $rootScope.obj.corpCd = null;
        } else if (_location == '/') {
            $rootScope.obj._type = 'index';
        } else {
            $rootScope.obj.corpCd = $routeParams.corpCd;
            $rootScope.obj._type = 'edit';
        }
        //$rootScope.$broadcast('routeChanged', _location);
    });
})
    .factory('Api', function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/PukalAcct';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: ApiRef
            })
        };

        getFormData = function (obj, url) {
            var ApiRef = url || Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/FillData?' + params
            })
        };

        getMaintData = function (obj, url) {
            var ApiRef = url || Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/FillMaintData?' + params
            })
        };

        SaveChanges = function (list, obj) {
            obj.MultipleTxnRecord = list;

            return $http({
                method: 'POST',
                url: Api + '/FTWebTxnPukalPaymentMaint',
                data: obj
            })
        };
        SaveSedutChanges = function (obj) {
            return $http({
                method: 'POST',
                url: Api + '/WebPukalSedutMaint',
                data: obj
            })
        };
        getWebTxnPukalPaymentSelect = function (obj) {
            return $http({
                url: Api + '/GetWebTxnPukalPaymentSelect?' + $.param(obj),
                method: 'GET'
            })
        };
        getPukalPaymentSummSelect = function (obj, url) {
            var ApiRef = url || Api;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: ApiRef + '/GetPukalPaymentSummSelect?' + params
            })
        };
        return {
            getFormData: getFormData,
            getMaintData: getMaintData,
            getPukalPaymentSummSelect: getPukalPaymentSummSelect,
            getWebTxnPukalPaymentSelect: getWebTxnPukalPaymentSelect,
            SaveChanges: SaveChanges,
            SaveSedutChanges: SaveSedutChanges
        }
    })
    .config(function ($routeProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/', {
            templateUrl: 'index.html',
            controller: 'indexController',
        })
        .when('/viewBatch/:batchId?', {
            templateUrl: rootUrl + '/PukalAcct/Tmpl?Prefix=pkal&type=Index',
            controller: 'viewBatchController'
        })
    })

.controller('indexController', function ($scope, $rootScope, $timeout, $location, Utils, $http, Api) {
    //$scope.path = location.host;
    $scope.modalOpen = false;
    $rootScope.obj._type = 'index';
    $rootScope.PukalAcctInfo = {};
    $scope.WebPukalSedutList = {};
    var acctNo = 0;
    Api.getFormData({ Prefix: 'gen' }).success(function (data) {
        $scope._Object = data.Model;
        $scope._Selects = data.Selects;
    });
    $scope.dtOptions = {
        serverSide: true,
        processing: true,
        checkBox: false,
        "bAutoWidth": false,
        "scrollX": true,
        id: 'tblPukalAcct',
        ajax: $rootScope.getRootUrl() + '/PukalAcct/ftGetPukalAcctBatchList?refCd=&AcctOfficeCd=&cycStmtId=0',
        "aoColumnDefs": [
              { "sClass": "text-center", "aTargets": [1] },
              { "sClass": "text-center", "aTargets": [2] },
              { "sClass": "text-right", "aTargets": [3] },
              { "sClass": "text-center", "aTargets": [4] },
              { "sClass": "text-center", "aTargets": [5] }
        ],
        edit: {
            level: 'scope',
            func: 'indexSelected'
        }
    };
    $scope.GetPukaAccountBatchList = function () {
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "bAutoWidth": false,
            "destroy": true,
            "scrollX": true,
            id: 'tblPukalAcct',
            ajax: $rootScope.getRootUrl() + '/PukalAcct/ftGetPukalAcctBatchList?refCd=' + $scope._Object.RefCd + '&acctOfficeCd= ' + $scope._Object.AcctOfficeCd + '&cycStmtId=' + $scope._Object.CycStmtId,
            "aoColumnDefs": [
              { "sClass": "text-center", "aTargets": [1] },
              { "sClass": "text-center", "aTargets": [2] },
              { "sClass": "text-right", "aTargets": [3]},
              { "sClass": "text-center", "aTargets": [4]},
              { "sClass": "text-center", "aTargets": [5]}
            ],
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
    }
    $scope.loadPukalSedutBySelected = function () {
        $timeout(function () {
            $scope.dtPukalSedutOptions = {
                serverSide: true,
                processing: true,
                checkBox: false,
                autoWidth: false,
                "destroy": true,
                "scrollX": true,
                id: 'tblPukalSedutContent',
                ajax: $rootScope.getRootUrl() + '/PukalAcct/ftGetPukalSedutView?' + $.param({ refCd: $scope._Object.RefCd, acctOfficeCd: $scope._Object.AcctOfficeCd, Sts: 'P' }),
                edit: {
                    level: 'scope',
                    func: 'indexPukaSedutSelected'
                },
                "initComplete": function (settings, json) {
                }
            };
        }, 300);

        $timeout(function () {
            $scope.dtPukalSedutHistoryOptions = {
                serverSide: true,
                processing: true,
                checkBox: false,
                autoWidth: false,
                "destroy": true,
                "scrollX": true,
                id: 'tblPukalSedutHistory',
                ajax: $rootScope.getRootUrl() + '/PukalAcct/ftGetPukalSedutView?' + $.param({ refCd: $scope._Object.RefCd, acctOfficeCd: $scope._Object.AcctOfficeCd, Sts: 'C' }),
                edit: {
                    level: 'scope',
                    func: 'indexPukaSedutHistorySelected'
                },
                "initComplete": function (settings, json) {
                }
            };
        }, 300);
    }
    $scope.$on("indexSelected", function (event, aData) {
        $scope._Object.BatchId = aData[0];
        $scope._Object.RefCd = aData[1];
        $scope._Object.AcctOfficeCd = aData[2];
        $scope._Object.ChequeNo = aData[3];
        $scope._Object.ChequeAmt = aData[4];
        $scope._Object.CreationDate = aData[5];
        $scope._Object.StatementDate = aData[6];
        $scope._Object.SelectedOwner = aData[8];
        $scope._Object.SelectedTxnCd = aData[1];

        $rootScope.PukalAcctInfo = $scope._Object;
        $location.path('/viewBatch').search('batchId', $scope._Object.BatchId);
        $scope.$apply();
    });
    $scope.$on('indexPukaSedutSelected', function (event, obj) {
        $scope.WebPukalSedutList = {
            AcctNo: obj[0],
            CompanyName: obj[1],
            ActivationDate: obj[2],
            TerminationDate: obj[3],
            PukalAmt: obj[4],
            StmtDate: obj[5],
            Status: obj[6],
            UserId: obj[7]
        }
        $scope.modalOpen = true;
        $scope.$apply();
    });
    $scope.RefCdSelectionChanged = function (obj) {
        $scope._Object.RefCd = obj;
        $scope.GetPukaAccountBatchList();
    }
    $scope.StatementDateChanged = function (obj) {
        $scope._Object.CycStmtId = obj.split(':')[0];
        $scope._Object.StatementDate = obj.split(':')[1];
        $scope.GetPukaAccountBatchList();
    }
    $scope.CardSelectionChanged = function (obj) {
        $scope._Object.AcctOfficeCd = obj;
        $scope.GetPukaAccountBatchList();
    }
    $scope.LoadSedutContentList = function () {
        $scope._Object.SelectedRefCd = '';
        $scope._Object.RefCd = '';
        $scope._Object.SelectedAreaCode = '';
        $scope._Object.AcctOfficeCd = '';
        $scope.loadPukalSedutBySelected();
    }
    $scope.SedutRefCdSelectionChanged = function (obj) {
        $scope._Object.RefCd = obj;
        $scope.loadPukalSedutBySelected();
    }
    $scope.SedutCardSelectionChanged = function (obj) {
        $scope._Object.AcctOfficeCd = obj;
        $scope.loadPukalSedutBySelected();
    }
    $scope.refresh = function () {
        $rootScope.tables[$scope.dtOptions.id].fnDraw();
    }
    $scope.NewTxn = function () {
        if (!$scope._Object.RefCd)
        {
            new PNotify({
                text: 'Please select a Reference Code',
                addclass: "stack-bottomright",
                stack: { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 },
                type: 'error',
                styling: 'fontawesome'
            });
        } else if (!$scope._Object.AcctOfficeCd)
        {
            new PNotify({
                text: 'Please select a Area Code',
                addclass: "stack-bottomright",
                stack: { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 },
                type: 'error',
                styling: 'fontawesome'
            });
        } else if (!$scope._Object.CycStmtId)
        {
            new PNotify({
                text: 'Please select a statement date',
                addclass: "stack-bottomright",
                stack: { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 },
                type: 'error',
                styling: 'fontawesome'
            });
        }else if($scope._Object.AcctOfficeCd && $scope._Object.RefCd && $scope._Object.CycStmtId)
        {
            $scope._Object.BatchId = 0;
            $scope._Object.ChequeNo = 0;
            $scope._Object.ChequeAmt = 0;
            var d = new Date();
            $scope._Object.CreationDate = d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
            $scope._Object.StatementDate = $scope._Object.StatementDate;
            $scope._Object.SelectedOwner = "";
            $rootScope.PukalAcctInfo = $scope._Object;
            $location.path('/viewBatch').search('batchId', $scope._Object.BatchId);
          
        }
    }
    $scope.SaveSedutChanges = function () {
        Utils.InfoNotify();
        Api.SaveSedutChanges($scope.WebPukalSedutList).success(function (data) {
            Utils.finalResultNotify(data.result);
            $rootScope.tables[$scope.dtPukalSedutOptions.id].fnDraw();
        });
        $scope.modalOpen = false;
    }
})
.controller('viewBatchController', function ($scope, $rootScope, $routeParams, $location, Api, Utils) {
    $rootScope.obj._type = 'index';
    $scope.batchId = 0;
    $scope.Items = [];
    $scope.Total = 0.00;
    $scope.ChequeAmtDiffer = 0;
    $scope.ChequeNo = 0;
    $scope.ChequeAmt = 0;
    $scope.Owner = '';
    $scope.Difference = $scope.ChequeAmt - $scope.Total;
    $scope.StatementDate = '';
    // statementCode
    $scope.CycStmtId = '';

    if (angular.isUndefined($rootScope.PukalAcctInfo))
        $scope.RefCd = '';
    else
        $scope.RefCd = $rootScope.PukalAcctInfo.RefCd;

    if (angular.isUndefined($rootScope.PukalAcctInfo))
        $scope.AcctOfficeCd = '';
    else
        $scope.AcctOfficeCd = $rootScope.PukalAcctInfo.AcctOfficeCd;

    if (angular.isUndefined($rootScope.PukalAcctInfo)) {
        $scope.StatementDate = '';
        $scope.CycStmtId = 0
    }
    else {
        $scope.CycStmtId = $rootScope.PukalAcctInfo.CycStmtId;
        $scope.StatementDate = $rootScope.PukalAcctInfo.StatementDate.replace(/-/g, '/');
    }
    Api.getMaintData({ batchId: $routeParams.batchId, StatementDate: $scope.StatementDate }).success(function (data) {
        $scope._Object = data.Model;
        $scope._Selects = data.Selects;
        if ($routeParams.batchId != 0) {
            $scope.RefCd = data.Model.RefCd;
            $scope.AcctOfficeCd = data.Model.AcctOfficeCd;
            $scope.StatementDate = data.Model.StmtDate;
            $scope.CycStmtId = data.Model.CycStmtId;
        }
        else {
            $scope._Object.RefCd = $scope.RefCd;
            $scope._Object.CycStmtId = $scope.CycStmtId;
            $scope._Object.AcctOfficeCd = $scope.AcctOfficeCd;
        }
        $scope.batchId = $routeParams.batchId;
        $scope.ChequeNo = data.Model.ChequeNo;
        $scope.ChequeAmt = data.Model.ChequeAmt;
        $scope.ChequeAmtDiffer = data.Model.ChequeAmt;
        $scope.CreationDate = data.Model.TxnDate;
        $scope.Owner = data.Model.Owner;
        $scope.Difference = $scope.ChequeAmt - $scope.Total;
        // call 
        Api.getWebTxnPukalPaymentSelect({
            batchId: $routeParams.batchId,
            refCd: $scope.RefCd, acctOfficeCd: $scope.AcctOfficeCd,
            cycStmtId: $scope.CycStmtId
        }).success(function (data) {
            $scope.Items = data.list;
            itemsChanged();
            //_Object.ChequeAmt
            $scope.$watch('ChequeAmtDiffer', function (newValue) {
                if (newValue.toString().indexOf(',') != -1) {
                    $scope.ChequeAmtDiffer = newValue.replace(/,/, "");
                    itemsChanged(newValue);

                } else {
                    itemsChanged(newValue);
                }
            }, true);
            if ($scope.Items.length) {
                if (angular.isUndefined($scope._Object.SelectedTxnCd))
                    $scope._Object.SelectedTxnCd = $scope.Items[0].TxnCd;
            }
        });
    });

    $scope.isEdit = true;
    $scope.ChangeChequeAmt = function(value)
    {
        $scope.ChequeAmtDiffer = value;
        $scope.ChequeAmtDiffer = value.replace(/,/, "");
        itemsChanged($scope.ChequeAmtDiffer);
    }
    $scope.ChequeNoNumber = function (obj)
    {
        //var value = $input.val();
        //value = value.replace(/[^0-9]/g, '')
        if (obj.length > 11 || obj.length == 0)
            return false;
        if (event.which == 64 || event.which == 16) {
            // to allow numbers  
            return false;
        } else if (event.which >= 48 && event.which <= 57) {
            // to allow numbers  
            return true;
        } else if (event.which >= 96 && event.which <= 105) {
            // to allow numpad number  
            return true;
        } else if ([8, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
            // to allow backspace, enter, escape, arrows  
            return true;
        } else {
            event.preventDefault();
            // to stop others  
            //alert("Sorry Only Numbers Allowed");  
            return false;
        }
    }
    $rootScope.SaveChanges = function () {
        Utils.InfoNotify();
        if ($scope.Difference < 0)
            $scope._Object.MultipleTxnRecord = {};
        if (IsValidSubmit($scope._Object)) {
            Api.SaveChanges($scope.Items, $scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if(data.result.flag == 0)
                {
                    if ($scope._Object.BatchId == 0)
                    {
                        $location.path('/viewBatch').search('batchId', data.result.Id);
                        setTimeout(function () { window.location.reload() }, 1500);
                    } else
                    {
                        $location.path('/viewBatch').search('batchId', $scope._Object.BatchId);
                        setTimeout(function () { window.location.reload() }, 1500);
                    }

                }
            });
        }
    }
    $scope.ApplyChanges = function(acountNo,payment)
    {
        if ($scope.Items.length > 0)
        {
            for (var i = 0; i < $scope.Items.length; i++)
            {
                if ($scope.Items[i].AcctNo == acountNo )
                {
                    if (payment.trim() == '')
                    $scope.Items[i].PaymentAmt = 0;
                }
            }
        }
    }
    var IsValidSubmit = function (obj) {
        var result = true;
        if (obj.SelectedTxnCd == '') {
            $scope.result = { flag: 1, desp: 'Txt Code is a compulsory field', Id: null };
            Utils.finalResultNotify($scope.result);
            result = false;
        } else if (obj.CreationDate == '') {
            $scope.result = { flag: 1, desp: 'Txt Date is a compulsory field', Id: null };
            Utils.finalResultNotify($scope.result);
            result = false;
        }
        else if (obj.SelectedOwner == '' && obj.Func == 'Submit') {
            $scope.result = { flag: 1, desp: 'Owner is a compulsory field', Id: null };
            Utils.finalResultNotify($scope.result);
            result = false;
        }
        else if ((obj.ChequeNo + '').length >= 11 || obj.ChequeNo == '0') {
            $scope.result = { flag: 1, desp: 'Cheque No to only allow to accept 10 digits', Id: null };
            Utils.finalResultNotify($scope.result);
            result = false;
        } else if (obj.SelectedSettlement == '') {
            $scope.result = { flag: 1, desp: 'GL Settlementis a compulsory field', Id: null };
            Utils.finalResultNotify($scope.result);
            result = false;
        }
    return result;
    }
    var itemsChanged = function (newValue) {
        var arrAmt = getAsAmountArray();
        calculateTotal(arrAmt);
    }
    var getAsAmountArray = function () {
        var arrAmt = _.map($scope.Items, function (item) {
            var amt;
            if (item.PaymentAmt) {
                var amount = item.PaymentAmt.toString();
                var amt = Number(amount.replace(/[^0-9\.]+/g, ""))
                return parseFloat(amt);
            }
            else if (item.PaymentAmt == '') {
                item.PaymentAmt = "0.00";
                return 0;
            }
            else {
                return 0;
            }
        });
        return arrAmt;
    }
    var calculateTotal = function (arrAmt) {
        var totalAmt = arrAmt.reduce(function (prev, current) { return prev + current; }, 0);
        $scope.Total = parseFloat(totalAmt || 0.00).toFixed(2) || 0.00;

        if ($scope._Object) {
            $scope.Difference = (parseFloat($scope.ChequeAmtDiffer || 0) - ($scope.Total)).toFixed(2);
            //$scope.Difference = (parseFloat($scope._Object.ChequeAmt || 0) - ($scope.Total)).toFixed(2);
        }
        else {
            $scope.Difference = parseFloat(0 - ($scope.Total || 0)).toFixed(2);
        }
    }
    $scope.$watch('Items', function (newValue) {
        itemsChanged(newValue);
    }, true);
})
.directive("limitTo", [function () {
        return {
            restrict: "A",
            link: function (scope, elem, attrs) {
                var limit = parseInt(attrs.limitTo);
                angular.element(elem).on("keypress", function (e) {
                    if (this.value.length == limit) e.preventDefault();
                });
            }
        }
 }])
.directive('validInput', function () {
    return {
        require: '?ngModel',
        scope: {
            "inputPattern": '@'
        },
        link: function (scope, element, attrs, ngModelCtrl) {
            var regexp = null;
            if (scope.inputPattern !== undefined) {
                regexp = new RegExp(scope.inputPattern, "g");
            }
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (regexp) {
                    var clean = val.replace(regexp, '');
                    if (val !== clean) {
                        ngModelCtrl.$setViewValue(clean);
                        ngModelCtrl.$render();
                    }
                    return clean;
                }
                else {
                    return val;
                }
            });
            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    }
});

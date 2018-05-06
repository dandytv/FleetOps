/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
angular.module('PinMailerApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams,Api) {
    $rootScope.tables = {};
    $rootScope.obj = {};
    $rootScope.testPrintmodalOpen = false;
    $rootScope.$on("updatePath", function (event, path) {
        $location.path(path);
    });

    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });

    $rootScope.testPrint = function () {
        $rootScope.testPrintmodalOpen = true;
    };

    $rootScope.testPrintConfirm = function () {
        $rootScope.testPrintmodalOpen = false;
        Utils.InfoNotify();
        Api.doTestPrint().success(function (data) {
            Utils.finalResultNotify(data);
        })

    };

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
        //var defaultApi = '/PinMailer';
        //var selectData = function () {
        //    return $http({
        //        method: 'GET',
        //        url: Api
        //    })
        var Api = $rootScope.getRootUrl() + '/PinMailer';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: ApiRef
            })
        };

        PrintBatchList = function (batchid, obj) {

            //alert('PrintBatchList: ' + obj.length);

            // Create list of card numbers
            var list = new Array();
            for (i = 0; i < obj.length; i++) {
                list.push(obj[i][1]);
            }

            //alert('PrintBatchList: ' + list[0]);

            return $http({
                url: $rootScope.getRootUrl() + '/PinMailer/ftPinMailerPrintList',
                method: 'POST',
                data: { batchId: batchid, cardList: list }
            });
        };

        getFormData = function (obj, Url) {
            var Api = Url || defaultApi;
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/FillData?' + params
            })
        };

        doTestPrint = function () {
            return $http({
                url: $rootScope.getRootUrl() + '/PinMailer/DoTestPrint',
                method: 'POST'
            });
        }

        ftContactDetail = function (obj) {
            alert('ftContactDetail: ');
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftContactDetail?' + Params,
                method: 'GET'
            });
        }

        return {
            PrintBatchList: PrintBatchList,
            getFormData: getFormData,
            doTestPrint: doTestPrint
        }
    })

    .config(function ($routeProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/', {
            templateUrl: 'index.html',
            controller: 'indexController',
        })

        .when('/viewBatch/:applId?', {
            templateUrl: rootUrl + '/PINMailer/Tmpl?Prefix=NULL&Type=Index',
            controller: 'viewBatchController',
            //pageKey: 'BatchInfo',


        })
    })

.controller('indexController', function ($scope, $rootScope, $location, Api) {
    //alert('indexController');
    $rootScope.obj._type = 'index';
    $scope.dtOptions = {
        serverSide: true,
        processing: true,
        checkBox: false,
        "scrollX": true,
        id: 'tblPinMailer',
        //ajax: '/Applications/ftAcctSignUpList', 
        ajax: $rootScope.getRootUrl() + '/PinMailer/ftGetPinMailerBatchList',
        "aoColumnDefs": [
                        //  { "sClass": "text-right", "aTargets": [2] },
        ],
        edit: {
            level: 'scope',
            func: 'indexSelected'
        }
    };
    $scope.$on("indexSelected", function (event, aData) {
        //$rootScope.obj.corpCd = aData[0];
        //$location.path('/viewBatch/' + aData[0]);
        //$scope.$apply();

        $rootScope.obj.batchId = aData[0];
        //$rootScope.obj._type = 'edit';
        $location.path('/viewBatch').search('batchId', $rootScope.obj.batchId);
        $scope.$apply();

    });


})

.controller('viewBatchController', function ($scope, $rootScope, $location, $routeParams, $timeout, Api, Utils) {
    //$rootScope.obj._type = 'edit';
    if ($routeParams.batchId)
        $rootScope.obj = {
            batchId: $routeParams.batchId
        }
    $scope.dtOptionsToPrint = {
        serverSide: true,
        processing: false,
        checkBox: true,
        autoWidth: false,
        "scrollX": true,
        id: 'tblBatchView',
        ajax: $rootScope.getRootUrl() + '/PinMailer/ftGetPinMailerBatchView?batchid=' + $rootScope.obj.batchId + '&status=1',
        //edit: {
        //    level: 'scope',
        //    //func: 'indexSelected'
        //}
    };

    $scope.printBatchList = function () {
        //alert('printBatchList');
        var rows = Utils.getSelectedRows($rootScope.tables[$scope.dtOptionsToPrint.id]);
        if (rows) {
            //alert('rows>1');
            Utils.InfoNotify();
            PrintBatchList($rootScope.obj.batchId, rows).success(function (data) {
                //alert('success:' + data.result);
                Utils.finalResultNotify(data.result);
                $rootScope.tables[$scope.dtOptionsToPrint.id].fnDraw();
                $rootScope.tables[$scope.dtOptionsPending.id].fnDraw();
                $rootScope.tables[$scope.dtOptionsPrinted.id].fnDraw();
            });
        }
    }

    $scope.rePrintBatchList = function () {
        //alert('printBatchList');
        var rows = Utils.getSelectedRows($rootScope.tables[$scope.dtOptionsPrinted.id]);
        if (rows) {
            //alert('rows>1');
            Utils.InfoNotify();
            PrintBatchList($rootScope.obj.batchId, rows).success(function (data) {
                //alert('success:' + data.result);
                Utils.finalResultNotify(data.result);
                $rootScope.tables[$scope.dtOptionsToPrint.id].fnDraw();
                $rootScope.tables[$scope.dtOptionsPending.id].fnDraw();
                $rootScope.tables[$scope.dtOptionsPrinted.id].fnDraw();
            });
        }
    }

    $scope.selectAllToPrint = function () {
        //alert('selectAll: checked=' + checked);
        var checked = $('#selectall').is(':checked');
        var table = $rootScope.tables[$scope.dtOptionsToPrint.id];
        //var rows = $rootScope.tables[$scope.dtOptionsToPrint.id].fnGetData().length;
        rows = table.fnGetNodes();

        $(rows).each(function () {
            if (checked)
                $(rows).removeClass('active').addClass('active');
            else
                $(rows).removeClass('active');
        })

        $('input', table.fnGetNodes()).each(function () {
            $('input', table.fnGetNodes()).attr('checked', checked);
        });
        event.stopPropagation();
        return false; // prevent page refresh
    };

    $scope.loadPinMailerPending = function () {
        $timeout(function () {
            $scope.dtOptionsPending = {
                serverSide: true,
                processing: true,
                checkBox: false,
                autoWidth: false,
                "scrollX": true,
                id: 'tblPinMailerBatchView2',
                ajax: $rootScope.getRootUrl() + '/PinMailer/ftGetPinMailerBatchView?batchid=' + $rootScope.obj.batchId + '&status=2'
            };
        }, 300);
    }
    $scope.loadPinMailerPrinted = function () {
        $timeout(function () {
            $scope.dtOptionsPrinted = {
                serverSide: true,
                processing: true,
                checkBox: true,
                //"bAutoWidth": false,
                autoWidth: false,
                "scrollX": true,
                id: 'tblPinMailerBatchView3',
                ajax: $rootScope.getRootUrl() + '/PinMailer/ftGetPinMailerBatchView?batchid=' + $rootScope.obj.batchId + '&status=0'
            };
        }, 300);
    };
});

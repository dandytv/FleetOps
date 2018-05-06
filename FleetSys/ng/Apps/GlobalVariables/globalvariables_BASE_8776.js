/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
/// <reference path="account.js" />
angular.module('globalVariablesApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams, $timeout) {
    $rootScope.tables = {};
    $rootScope.variables = [
    { Key: "Language", Text: "Language", Selected: true }, { Key: "State", Text: "State" }, { Key: "City", Text: "City" }, { Key: "IcType", Text: "ID Number Type" }, { Key: "Title", Text: "Title" }, { Key: "VehType", Text: "Vehicle Type" }
    , { Key: "SaleTerritory", Text: "Sale Territory" }, { Key: "AcctOfficeCd", Text: "Pukal AG Code" }, { Key: "ProdCategory", Text: "Product Category" }, { Key: "ProdType", Text: "Product Type" }
    ];
    $rootScope.selectedObj = $rootScope.variables[0];
    $rootScope.subMenuSelected = function (item) {

        $rootScope.variables.forEach(function (item) {
            item.Selected = false;
        })
        var index = $rootScope.variables.indexOf(item);
        $rootScope.variables[index].Selected = true;
        $rootScope.selectedObj = $rootScope.variables[index];
        $rootScope.$broadcast('menuSelected', item);
    }
})
     .factory('Api', function ($http, $rootScope) {
         var Api = $rootScope.getRootUrl() + '/GlobalVariables';
         var selectData = function () {
             return $http({
                 method: 'GET',
                 url: ApiRef
             })
         };
         getFormData = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/FillData'
             })
         };
         getStates = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/GetStates?' + $.param(obj)
             })
         };

         WebRefSelect = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/WebRefSelect?' + $.param(obj)
             })
         };
         WebRefMaint = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'POST',
                 url: ApiRef + '/WebRefMaint',
                 data: obj
             })
         };
         WebProdGroupRefSelect = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/WebProdGroupRefSelect?' + $.param(obj)
             })
         }

         return {
             selectData: selectData,
             getFormData: getFormData,
             GetStates: getStates,
             WebRefSelect: WebRefSelect,
             WebRefMaint: WebRefMaint,
             WebProdGroupRefSelect: WebProdGroupRefSelect
         };
     })
    .config(function ($routeProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/', {
            templateUrl: 'tmplMain.html',
            controller: 'globalParamsController',
            pageKey: 'globalParams',

        })
        $routeProvider.when('/productGroup', {
            templateUrl: 'tmplProduct.html',
            controller: 'productGroupController',
            pageKey: 'productGroup'
        })
        .when('/productList', {
            templateUrl: 'tmplRebate.html',
            controller: 'productListController',
            pageKey: 'productList'
        });
    })
.controller('globalParamsController', function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
    $scope.modalOpen = false;
    $scope.editMode = false;
    $scope.dtOptions = {
        serverSide: true,
        processing: true,
        destroy: true,
        scrollX: true,
        id: 'tblMain',
        ajax: $rootScope.getRootUrl() + '/GlobalVariables/WebRefListSelect?refType=Language',
        edit: {
            level: 'scope',
            func: 'indexSelected'
        }
    };

    Api.getFormData({ Code: 'lp' }).success(function (data) {
        $scope._Object = data.Model;
        $scope._Selects = data.Selects;
    })
    $scope.modalClick = function () {
        var selectedObj = $rootScope.selectedObj;
        if (selectedObj.Key == "State") {

        }
        $scope.editMode = false;
        $scope.modalOpen = true;
        Utils.makeObjectNull($scope._Object, {});
    };
    $rootScope.$on("indexSelected", function (event, aData) {
        $scope.editMode = true;
        var selectedObj = $rootScope.selectedObj;
        var obj = {};
        obj.refType = selectedObj.Key;
        if (selectedObj.Key == "State") {
            obj.RefNo = aData[3];
            obj.RefCd = aData[1];
        } else if (selectedObj.Key == "City") {
            obj.RefCd = aData[2];
        }
        else {
            obj.RefCd = aData[0];
        }

        Api.WebRefSelect(obj).success(function (data) {
            console.log(data);
            $scope.modalOpen = true;
            angular.extend($scope._Object, data);
        })
        console.log(aData);
    });
    $scope.$on('menuSelected', function (data, obj) {
        var x = {};
        x.ajax = $rootScope.getRootUrl() + '/GlobalVariables/WebRefListSelect?refType=' + obj.Key;
        x.aoColumnDefs = [
            { "bVisible": obj.Key != "State", "aTargets": [3] }
        ]
        $timeout(function myfunction() {

            var value = angular.extend($scope.dtOptions, x);
            //   $scope.$broadcast('updateDataTable', { options: value });
        });
    });
    $scope.CountryChanged = function (item, value) {
        Api.GetStates({ CountryCd: value }).success(function (item) {
            $scope._Selects.States = item;
        });
    }
    $scope.refresh = function () {
        $rootScope.tables[$scope.dtOptions.id].fnDraw();
    }
    $scope.saveChanges = function () {
        var type = $rootScope.selectedObj.Key;
        var obj = { type: type };
        angular.extend($scope._Object, { type: type, flag: $scope.editMode ? 'U' : 'C' });
        Utils.InfoNotify();
        Api.WebRefMaint($scope._Object).success(function (data) {
            Utils.finalResultNotify(data);
            if (data.flag == 0) {
                $rootScope.tables[$scope.dtOptions.id].fnDraw();
                $scope.modalOpen = false;
            }
        });
    }
})
.controller('productGroupController', function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
    $scope.detailModalOpen = false;
    $scope.selectedProdGroup=null;
    $scope.dtOptions = {
        serverSide: true,
        processing: true,
        destroy: true,
        scrollX: true,
        pageLength: 3000,
        id: 'tblProductGroup',
        ajax: $rootScope.getRootUrl() + '/GlobalVariables/WebProdGroupRefListSelect',
        edit: {
            level: 'scope',
            func: 'indexSelected'
        },
        aoColumnDefs: [
                { "sClass": "detail-toggler", "aTargets": [0] },
        ],
        childTable: {
            format: function (nRows) {
                var htm;
                var rows = _.filter($scope.aaData, function (data) {
                    return data[1] == nRows[1];//txnid
                });
                var header = "<thead><tr><th>Product Code</th><th>Product Name</th><th>Product Category</th><th>Product Type</th></tr></thead>";
                var rows2 = _.map(rows, function (Next) {
                    return '<tr ng-click="detailModalOpen=true">' +
                      '<td style=\'background-color:#F8F9FA\'>' + Next[5] + '</td>' +
                      '<td style=\'background-color:#F8F9FA\'>' + Next[6] + '</td>' +
                      '<td style=\'background-color:#F8F9FA\'>' + Next[7] + '</td>' +
                      '<td style=\'background-color:#F8F9FA\'>' + Next[8] + '</td>' +
                      '</tr>'
                });
                var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                console.log(fullTable);
                $scope.selectedProdGroup=nRows[1];
                return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr>';
            },
            sum: function sum(numbers) {
                return _.reduce(numbers, function (result, current) {
                    return result + parseFloat(current[9]);
                }, 0);
            },
            edit: {
                level: 'format',
                func: function (cells) {
                    $scope.detailModalOpen = true;
                    $scope.$apply();
                    Api.WebProdGroupRefSelect({ ProdGroup: $scope.selectedProdGroup }).success(function (data) {
                        console.log(data);
                    })
                }
            },
            fngroupOp: function (e, settings, json) {
                //json.aaData = [];
                $scope.$apply(function () {
                    $scope.date = new Date();
                    $scope.aaData = json.aaData;
                });
                var rows = [];
                _.each(json.aaData, function (data) {
                    if (rows.length) {
                        var contains = _.find(rows, function (item) {
                            return item[1] == data[1];
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
    }

});
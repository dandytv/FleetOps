/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
/// <reference path="account.js" />
angular.module('globalVariablesApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams, $timeout) {
    $rootScope.tables = {};
    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });


    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();
        if (_location == '/new') {
            $rootScope.obj._type = 'new';
            $rootScope.obj.applId = null;


        } else if (_location == '/') {
            $rootScope.obj._type = 'index';
        } else {
            $rootScope.obj.applId = $routeParams.applId;
            $rootScope.obj._type = 'edit';
        }
        //$rootScope.$broadcast('routeChanged', _location);
    })




})
     .factory('Api', function ($http, $rootScope) {
         var Api = $rootScope.getRootUrl() + '/ProductParameter';
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
         WebRebateMaint = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'POST',
                 url: ApiRef + '/WebRebateMaint',
                 data: obj
             })
         };

         return {
             selectData: selectData,
             getFormData: getFormData,
             WebRefMaint: WebRefMaint
         };
     })
      .config(function ($routeProvider) {
          var rootUrl = $('#hdUrlPrefix').val();

          $routeProvider.when('/', {
              templateUrl: 'index.html',
              controller: 'mainController',
              pageKey: 'index',

          })
           .when('/productList', {
               templateUrl: rootUrl + '/Account/Tmpl?Prefix=fin&type=Index',
               controller: 'productListController',
               pageKey: 'product'
           })   
      })

.controller('mainController', function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
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

    Api.getFormData({}).success(function (data) {
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
});
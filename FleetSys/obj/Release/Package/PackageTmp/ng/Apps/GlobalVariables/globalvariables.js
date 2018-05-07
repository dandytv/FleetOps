/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
/// <reference path="account.js" />
angular.module('CardtrendApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select', 'datetime']).run(function (Utils, $rootScope, $location, $routeParams, $timeout) {
    $rootScope.tables = {};
    $rootScope.variables = [
    { Key: "Language", Text: "Language", Selected: true }, { Key: "State", Text: "State" }, { Key: "City", Text: "City" }, { Key: "IcType", Text: "ID Number Type" }, { Key: "Title", Text: "Title" }, { Key: "VehType", Text: "Vehicle Type" }
    , { Key: "SaleTerritory", Text: "Sale Territory" }, { Key: "ProdCategory", Text: "Product Category" }, { Key: "ProdType", Text: "Product Type" }
    ];
    $rootScope.selectedObj = $rootScope.variables[0];
    $rootScope.subMenuSelected = function (item) {

        $rootScope.variables.forEach(function (item) {
            item.Selected = false;
        });
        var index = $rootScope.variables.indexOf(item);
        $rootScope.variables[index].Selected = true;
        $rootScope.selectedObj = $rootScope.variables[index];
        $rootScope.$broadcast('menuSelected', item);
    }
    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });
})
.config(function ($routeProvider) {
    var rootUrl = $('#hdUrlPrefix').val();
    $routeProvider.when('/', {
        templateUrl: rootUrl + '/GlobalVariables/Tmpl?Prefix=lup&type=Index',
        controller: 'globalParamsController',
        pageKey: 'hash'
    })
    $routeProvider.when('/productGroup', {
        templateUrl: rootUrl + '/GlobalVariables/Tmpl?Prefix=pdt&type=Index',
        controller: 'productGroupController',
        pageKey: 'productGroup'
    })
    .when('/productList', {
        templateUrl: rootUrl + '/GlobalVariables/Tmpl?Prefix=pdt&type=Index&BehaveAsPrefix=pdl',
        controller: 'productListController',
        pageKey: 'productList'
    })
    .when('/Language', {
        templateUrl: rootUrl + '/GlobalVariables/Tmpl?Prefix=lag&type=Index',
        controller: 'globalParamsController',
        pageKey: 'Language'
    })
    .when('/City', {
        templateUrl: rootUrl + '/GlobalVariables/Tmpl?Prefix=city&type=Index',
        controller: 'globalParamsController',
        pageKey: 'City'
    })
    .when('/productList/:prdId', {
        templateUrl: rootUrl + '/GlobalVariables/Tmpl?Prefix=pdt&type=Index&BehaveAsPrefix=pdl',
        controller: 'productListController',
        pageKey: 'productList'
    })
    .when('/rebatePlan', {
        templateUrl: rootUrl + '/GlobalVariables/Tmpl?Prefix=rbp&type=Index',
        controller: 'rebatePlanController',
        pageKey: 'rebatePlan'
    })
    .when('/eventTypeList', {
        controller: 'eventTypeListController',
        templateUrl: rootUrl + '/GlobalVariables/Tmpl?Prefix=evt&type=Index',
        pageKey: 'eventList'
    })
    .when('/eventDetails/:id', {
        controller: 'eventDetailController',
        templateUrl: rootUrl + '/GlobalVariables/Tmpl?Prefix=evt&type=Index&BehaveAsPrefix=evt_dtl',
        pageKey: 'eventList'
    })
})
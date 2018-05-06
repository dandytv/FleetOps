/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
angular.module('manualEntryApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.Obj = {
        SettleId: null,
        TxnId: null,
        BatchId: null,
        ProductId: null,
        TxnDetailId: null
    };
    $rootScope.tables = {};
    $rootScope.obj = {};
});
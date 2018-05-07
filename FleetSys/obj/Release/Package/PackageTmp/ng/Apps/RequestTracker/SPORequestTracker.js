/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
angular.module('RequestTrackerApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.Obj = {
        ApplId: null
    };
    $rootScope.tables = {};
    $rootScope.obj = {};

    $rootScope.$on("updatePath", function (event, path) {
        $location.path(path);
    });

    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });
})
.config(function ($routeProvider) {
         var rootUrl = $('#hdUrlPrefix').val();
         $routeProvider.when('/', {
             templateUrl: 'index.html',
             controller: 'mainController',
             pageKey: 'index'
         })
          .when('/Approval/:workflowcd/:aprId/:acctNo', {
              templateUrl: rootUrl + '/Applications/Tmpl?Prefix=apr&type=Index&overrideController=Approval',
              controller: 'ApprovalController',
              pageKey: 'index'
          })
     })
;
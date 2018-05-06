angular.module('CollectionApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select', 'ngRouteAnimationManager']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};
    $rootScope.urlPrefix = $('#hdUrlPrefix').val();

    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });
    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();
        $rootScope.obj.acctNo = $routeParams.acctNo;
        $rootScope.obj.eventId = $routeParams.eventId;
    });
})
.config(function ($routeProvider, RouteAnimationManagerProvider) {

    var rootUrl = $('#hdUrlPrefix').val();
    $routeProvider.when('/', {
        templateUrl: rootUrl + '/Collection/Tmpl?Prefix=dal&type=Index',
        controller: 'collectionTaskListController',
        data: {
            animationConf: {
            }

        }
    })
      .when('/CollectionFollowUp/:acctNo?/:eventId?', {
          templateUrl: rootUrl + '/Collection/Tmpl?Prefix=cfu&type=Index',
          controller: 'collectionFollowUpController',
          pageKey: 'CollFollowUp',

          data: {
              animationConf: {
              }
          }
      })
     .when('/AccountInfo/:acctNo?/:eventId?', {
         templateUrl: rootUrl + '/Collection/Tmpl?Prefix=cai&type=Index',
         controller: 'accountInfoController',
         pageKey: 'AcctInfo',

         data: {
             animationConf: {
             }
         }
     })
     .when('/FinancialInfo/:acctNo?/:eventId?', {
         templateUrl: rootUrl + '/Collection/Tmpl?Prefix=cfi&type=Index',
         controller: 'financialInfoController',
         pageKey: 'FinInfo',

         data: {
             animationConf: {
             }
         }
     })
     .when('/CollectionHistory/:acctNo?/:eventId?', {
         templateUrl: rootUrl + '/Collection/Tmpl?Prefix=cch&type=Index',
         controller: 'collectionHistoryController',
         pageKey: 'CollHist',

         data: {
             animationConf: {
             }
         }
     })
    ;
    RouteAnimationManagerProvider.setDefaultAnimation('fade');
})



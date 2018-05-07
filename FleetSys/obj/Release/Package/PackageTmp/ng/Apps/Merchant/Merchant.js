
/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
/// <reference path="account.js" />
angular.module('MerchantApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};
    $rootScope.obj.merchantId = $('#hdMerchantId').val();
    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });
    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();
        if (_location == '/new') {
            $rootScope.obj._type = 'new';
            $rootScope.obj.acctNo = null;


        } else if (_location == '/') {
            $rootScope.obj._type = 'index';
            $rootScope.obj.acctNo = null;
        } else {
            $rootScope.obj.acctNo = $routeParams.acctNo;
            $rootScope.obj._type = 'edit';
        }
        //$rootScope.$broadcast('routeChanged', _location);
    });
})
.config(function ($routeProvider) {
    var rootUrl = $('#hdUrlPrefix').val();
    $routeProvider.when('/', {
        templateUrl: 'index.html',
        controller: 'indexController'
    })

        .when('/new', {
            templateUrl: rootUrl + '/Merchant/Tmpl?Prefix=gen&type=New',
            controller: 'generalInfoController',
            pageKey: 'generalInfo'
        })

    .when('/generalInfo/:acctNo?', {
        templateUrl: rootUrl + '/Merchant/Tmpl?Prefix=gen&type=Index',
        controller: 'generalInfoController',
        pageKey: 'generalInfo'
    })
    .when('/businessLocations/:acctNo?', {
        templateUrl: rootUrl + '/Merchant/Tmpl?Prefix=bus&type=Index',
        controller: 'busnLocationController',
        pageKey: 'businessLocations'
    })

    .when('/CardRange/:acctNo?', {
        templateUrl: rootUrl + '/Merchant/Tmpl?Prefix=car&type=Index',
        controller: 'cardRangeController',
        pageKey: 'cardRange'
    })
     .when('/Miscellanious/:acctNo?', {
         templateUrl: rootUrl + '/Applications/Tmpl?Prefix=mis&type=Index&overrideController=Merchant',
         controller: 'MiscellaniousController',
         pageKey: 'Miscellanious'
     })
    .when('/contacts/:acctNo?', {
        templateUrl: rootUrl + '/Applications/Tmpl?Prefix=con&type=Index&overrideController=Merchant',
        controller: 'contactsController',
        pageKey: 'contacts'
    })
     .when('/address/:acctNo?', {
         templateUrl: rootUrl + '/Applications/Tmpl?Prefix=add&type=Index&overrideController=Merchant',
         controller: 'addressController',
         pageKey: 'address'
     })

    .when('/terminals/:acctNo?', {
        templateUrl: rootUrl + '/Merchant/Tmpl?Prefix=txn&type=Index',
        controller: 'terminalsController',
        pageKey: 'terminals'
    })
    .when('/txnSearch/:acctNo?', {
        templateUrl: rootUrl + '/Merchant/Tmpl?Prefix=mps&type=Index',
        controller: 'txnSearchController',
        pageKey: 'txnSearch'
    })
    .when('/statusMaint/:acctNo?', {
        templateUrl: rootUrl + '/Account/Tmpl?Prefix=sta&type=Index&overrideController=Merchant',
        controller: 'statusMaintController',
        pageKey: 'statusMaint'
    })
});






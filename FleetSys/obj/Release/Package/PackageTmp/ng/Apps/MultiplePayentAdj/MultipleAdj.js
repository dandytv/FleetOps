/// <reference path="https://ajax.googleapis.com/ajax/libs/angularjs/1.3.0-beta.8/angular.min.js" />
angular.module('multipleAdjApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function ($rootScope) {
    $rootScope.tables = [];
})
 .config(function ($routeProvider) {
     $routeProvider.when('/', {
         templateUrl: 'index.html',
         controller: 'indexController',
     })
     .when('/new/', {
         templateUrl: 'main.html',
         controller: 'mainController',
         pageKey: 'generalInfo'
     })
          .when('/edit/:batchId/:ChequeNo?/:paymentSelected?', {
              templateUrl: 'main.html',
              controller: 'mainController',
              pageKey: 'generalInfo'
          })
 })
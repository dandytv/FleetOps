/// <reference path="https://ajax.googleapis.com/ajax/libs/angularjs/1.3.0-beta.8/angular.min.js" />
angular.module('txnSearchApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function ($rootScope) {
    $rootScope.tables = [];
});
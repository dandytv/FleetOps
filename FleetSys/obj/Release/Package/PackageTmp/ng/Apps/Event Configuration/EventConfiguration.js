angular.module('CardtrendApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select', 'datetime']).run(function (Utils, $rootScope, $location, $routeParams, $timeout) {
})
.run(function (Utils, $rootScope, $location, $routeParams, $timeout) {
        $rootScope.tables = {};
    })
.config(function ($routeProvider) {
    var rootUrl = $('#hdUrlPrefix').val();
    $routeProvider.when('/', {
        controller: 'eventConfigListController',
        templateUrl: rootUrl + '/EventConfiguration/Tmpl?Prefix=evc&type=Index'
    })
    .when('/eventDetails/:id', {
        controller: 'eventDetailController',
        templateUrl: rootUrl + '/EventConfiguration/Tmpl?Prefix=dtl&type=Index'
    })
    .when('/new', {
        controller: 'eventDetailController',
        templateUrl: rootUrl + '/EventConfiguration/Tmpl?Prefix=dtl&type=Index'
    })
 })

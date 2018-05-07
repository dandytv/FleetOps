angular.module('notificationSearchApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams, $timeout) {
})
.run(function (Utils, $rootScope, $location, $routeParams, $timeout) {
        $rootScope.tables = {};
    })
.config(function ($routeProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/', {
            controller: 'indexController',
            templateUrl: rootUrl + "/NotificationSearch/Tmpl?Prefix=lis"
        })
        .when('/eventDetails/:id', {
            controller: 'eventDetailController',
            templateUrl: rootUrl + "/NotificationSearch/Tmpl?Prefix=det"
        })
    });
/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
//angular.module('ApplicationsApp', ['ngRoute', 'App.Utils']).run(function (Utils, $rootScope, $location, $routeParams) {
angular.module('CardtrendApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};
    $rootScope.selectedStatus = null;

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
            controller: 'indexController',
            pageKey: 'index'
        })
        .when('/Approval/:workflowcd/:aprId/:acctNo', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=apr&type=Index&overrideController=Approval',
            controller: 'ApprovalController',
            pageKey: 'index'
        })
    })
.directive('bootstrapWizard', function ($compile) {
    return {
        restrict: 'A',
        link: function (scope, $element, attrs) {
            $element.bootstrapWizard({
                'nextSelector': '.button-next',
                'previousSelector': '.button-previous',
                onNext: function (tab, navigation, index) {

                },
                onTabShow: function (tab, navigation, index) {
                    var $total = navigation.find('li').length;
                    var $current = index + 1;
                    var $percent = ($current / $total) * 100;
                    $element.find('.progress-bar').css({
                        width: $percent + '%'
                    });

                    $element.find('.steps li').each(function (index) {
                        $(this).removeClass('complete');
                        index += 1;
                        if (index < $current) {
                            $(this).addClass('complete');
                        }
                    });

                    if ($current >= $total) {
                        $('#wizard2').find('.button-next').hide();
                        $('#wizard2').find('.button-finish').show();
                    } else {
                        $('#wizard2').find('.button-next').show();
                        $('#wizard2').find('.button-finish').hide();
                    }
                }
            });
        }
    }
})
.directive('accordionDirective', function ($compile) {
    return {
        restrict: 'A',
        link: function (scope, $element, attrs) {

        }
    }
});

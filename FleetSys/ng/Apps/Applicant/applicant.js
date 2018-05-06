/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
angular.module('CardtrendApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};
    //applicant?ApplId=23238&appcId=2387627
    $rootScope.$on("updatePath", function (event, path) {
        $location.path(path);
    });

    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
        if (current.pageType) {
            $rootScope.obj._type = 'new';
        } else {
            $rootScope.obj._type = 'edit';
        }
    });
    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();
        $rootScope.obj.applId = $routeParams.applId;
        $rootScope.obj.appcId = $routeParams.appcId;
        //$rootScope.$broadcast('routeChanged', _location);
    });
})
.config(function ($routeProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/finanacialInfo/:applId/:appcId', {
            templateUrl: rootUrl + '/Applicant/Tmpl?Prefix=fin',
            controller: 'financialInfoController',
            pageKey: 'finanacialInfo'
        })
        .when('/:applId/applicants', {
            templateUrl: rootUrl + '/ApplicantCard/Tmpl?Prefix=app',
            controller: 'indexController',
            pageKey: 'index',
            pageType: 'index'
        })
        .when('/:applId/new', {
            templateUrl: rootUrl + '/Applicant/Tmpl?Prefix=gen',
            controller: 'generalInfoController',
            pageKey: 'generalInfo',
            pageType: 'new'
        })
        .when('/:applId/:appcId', {
            templateUrl: rootUrl + '/Applicant/Tmpl?Prefix=gen',
            controller: 'generalInfoController',
            pageKey: 'generalInfo'
        })
        .when('/personInfo/:applId/:appcId', {
            templateUrl: rootUrl + '/Applicant/Tmpl?Prefix=per',
            controller: 'personInfoController',
            pageKey: 'personInfo'
        })
        .when('/velocity/:applId/:appcId', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=vel&type=Index&overrideController=Applicant',
            controller: 'velocityController',
            pageKey: 'velocity'
        })
        .when('/contacts/:applId/:appcId', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=con&type=Index&overrideController=Applicant',
            controller: 'contactsController',
            pageKey: 'contacts'
        })
        .when('/address/:applId/:appcId', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=add&type=Index&overrideController=Applicant',
            controller: 'addressController',
            pageKey: 'address'
        })
        .when('/statusMaint/:applId/:appcId', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=sta&type=Index&overrideController=Applicant',
            controller: 'statusMaintController',
            pageKey: 'statusMaint'
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

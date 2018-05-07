/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
angular.module('CardtrendApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};
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
        $rootScope.obj.acctNo = $routeParams.acctNo;
        $rootScope.obj.appcId = $routeParams.appcId;
        //$rootScope.$broadcast('routeChanged', _location);
    });
})
.config(function ($routeProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/finanacialInfo/select/:acctNo/:appcId', {
            templateUrl: rootUrl + '/Applicant/Tmpl?Prefix=fin&type=Index&overrideController=ApplicantCard',
            controller: 'financialInfoController',
            pageKey: 'finanacialInfo'
        })
            //#/{{$root.obj.applId}}/applicants
        .when('/:acctNo/applicants', {
            templateUrl: rootUrl + '/ApplicantCard/Tmpl?Prefix=app&type=Index&overrideController=ApplicantCard',
            controller: 'indexController',
            pageKey: 'index',
            pageType: 'index'
        })
        .when('/generalInfo/:acctNo/new', {
            templateUrl: rootUrl + '/Applicant/Tmpl?Prefix=gen&type=Index&overrideController=ApplicantCard',
            controller: 'generalInfoController',
            pageType: 'new',
            pageKey: 'generalInfo'
        })
        .when('/generalInfo/select/:acctNo/:appcId', {
            templateUrl: rootUrl + '/Applicant/Tmpl?Prefix=gen&type=Index&overrideController=ApplicantCard',
            controller: 'generalInfoController',
            pageKey: 'generalInfo'

        })
        .when('/personInfo/:acctNo/:appcId', {
            templateUrl: rootUrl + '/Applicant/Tmpl?Prefix=per&type=Index&overrideController=ApplicantCard',
            controller: 'personInfoController',
            pageKey: 'personInfo'
        })
        .when('/velocity/:acctNo/:appcId', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=vel&type=Index&overrideController=ApplicantCard',
            controller: 'velocityController',
            pageKey: 'velocity'
        })
        .when('/contacts/:acctNo/:appcId', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=con&type=Index&overrideController=ApplicantCard',
            controller: 'contactsController',
            pageKey: 'contacts'
        })
        .when('/address/:acctNo/:appcId', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=add&type=Index&overrideController=ApplicantCard',
            controller: 'addressController',
            pageKey: 'address'
        })
        .when('/statusMaint/:acctNo/:appcId', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=sta&type=Index&overrideController=ApplicantCard',
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

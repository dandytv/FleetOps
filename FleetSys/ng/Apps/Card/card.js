/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
angular.module('CardtrendApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select', 'ngRouteAnimationManager']).run(function (Utils, $rootScope, $location, $routeParams) {
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
        $rootScope.obj.cardNo = $routeParams.cardNo;
        //$rootScope.$broadcast('routeChanged', _location);
    });
})
 .config(function ($routeProvider, RouteAnimationManagerProvider) {
        var rooturl = $('#hdUrlPrefix').val();
        $routeProvider.when
            ('/cardsList/:acctNo?', {
                templateUrl: rooturl + '/Account/Tmpl?Prefix=car&type=Index&overrideController=Card',
                controller: 'indexController',
                pageKey: 'index',
                data: {
                    animationConf: {
                    }
                }
            })

        .when('/new/:acctNo', {
            templateUrl: rooturl + '/Card/Tmpl?Prefix=gen&type=Index',
            controller: 'generalInfoController',
            pageKey: 'generalInfo',
            pageType: 'new',
            data: {
                animationConf: {
                }
            }
        }).
        when('/:acctNo/:cardNo', {
            templateUrl: rooturl + '/Card/Tmpl?Prefix=gen&type=Index',
            controller: 'generalInfoController',
            pageKey: 'generalInfo',
            data: {
                animationConf: {
                }
            }
       }).when('/finanacialInfo/:acctNo/:cardNo', {
            templateUrl: rooturl + '/Card/Tmpl?Prefix=fin&type=Index',
            controller: 'financialInfoController',
            pageKey: 'finanacialInfo',
            data: {
                animationConf: {
                }
            }
        })
        .when('/personInfo/:acctNo/:cardNo/:entityId', {
            templateUrl: rooturl + '/Card/Tmpl?Prefix=per&type=Index',
            controller: 'personInfoController',
            pageKey: 'personInfo',
            data: {
                animationConf: {}
            }
        })
        .when('/velocity/:acctNo/:cardNo', {
            templateUrl: rooturl + '/Applications/Tmpl?Prefix=vel&type=Index&overrideController=Card',
            controller: 'velocityController',
            pageKey: 'velocity',
            data: {
                animationConf: {
                }
            }
        })
         .when('/contacts/:acctNo/:cardNo', {
             templateUrl: rooturl + '/Applications/Tmpl?Prefix=con&type=Index&overrideController=Card',
             controller: 'contactsController',
             pageKey: 'contacts',
             data: {
                 animationConf: {
                 }
             }
         })
         .when('/address/:acctNo/:cardNo', {
             templateUrl: rooturl + '/Applications/Tmpl?Prefix=add&type=Index&overrideController=Card',
             controller: 'addressController',
             pageKey: 'address',
             data: {
                 animationConf: {

                 }
             }
         })
         .when('/statusMaint/:acctNo/:cardNo', {
             templateUrl: rooturl + '/Account/Tmpl?Prefix=sta&type=Index&overrideController=Card',
             controller: 'statusMaintController',
             pageKey: 'statusMaint',
             data: {
                 animationConf: {

                 }
             }
         })
        .when('/cardReplacement/:acctNo/:cardNo', {
            templateUrl: rooturl + '/Card/Tmpl?Prefix=rep&type=Index',
            controller: 'cardReplacementController',
            pageKey: 'cardReplacement',
            data: {
                animationConf: {
                }
            }
        })
        .when('/locationAcceptance/:acctNo/:cardNo', {
            templateUrl: rooturl + '/Account/Tmpl?Prefix=loc&type=Index&overrideController=Card',
            controller: 'locationAcceptanceController',
            pageKey: 'locationAcceptance',
            data: {
                animationConf: {
                }
            }
        })
        .when('/productDiscount/:acctNo/:cardNo', {//acctNo
            templateUrl: rooturl + '/Account/Tmpl?Prefix=pdc&type=Index&overrideController=Card',
            controller: 'productDiscountController',
            pageKey: 'productDiscount',
            data: {
                animationConf: {
                }
            }
        })
        RouteAnimationManagerProvider.setDefaultAnimation('fade');
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

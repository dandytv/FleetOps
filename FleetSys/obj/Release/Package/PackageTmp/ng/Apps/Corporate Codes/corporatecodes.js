/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
angular.module('CardtrendApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select', 'ngRouteAnimationManager']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {}; 
    $rootScope.obj = {};

    $rootScope.$on("updatePath", function (event, path) {
        $location.path(path);
    });

    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });
    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();
        if (_location == '/new') {
            $rootScope.obj._type = 'new';
            $rootScope.obj.corpCd = null;
        } else if (_location == '/') {
            $rootScope.obj._type = 'index';
            $rootScope.obj.corpCd = null;
        } else {
            $rootScope.obj.corpCd = $routeParams.corpCd;
            $rootScope.obj._type = 'edit';
        }
        //$rootScope.$broadcast('routeChanged', _location);
    });
})
.config(function ($routeProvider, RouteAnimationManagerProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/', {
            templateUrl: 'index.html',
            controller: 'indexController',
            data: {
                animationConf: {
                }
            }
        })
        .when('/new', {
            templateUrl: rootUrl + '/CorporateCodes/Tmpl?Prefix=gen&type=Index',
            controller: 'detailsController',
            pageKey: 'generalInfo',
            data: {
                animationConf: {
                }
            }
        })
        .when('/details/:corpCd?', {//
            templateUrl: rootUrl + '/CorporateCodes/Tmpl?Prefix=gen&type=Index',
            controller: 'detailsController',
            pageKey: 'generalInfo',
            data: {
                animationConf: {
                }
            }
        })
        .when('/velocity/:corpCd?', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=vel&type=Index&overrideController=CorporateCodes',
            controller: 'velocityController',
            pageKey: 'velocity',
            data: {
                animationConf: {
                }
            }
        })
         .when('/contacts/:corpCd?', {
             templateUrl: rootUrl + '/Applications/Tmpl?Prefix=con&type=Index&overrideController=CorporateCodes',
             controller: 'contactsController',
             pageKey: 'contacts',
             data: {
                 animationConf: {
                 }
             }
         })
         .when('/address/:corpCd?', {
             templateUrl: rootUrl + '/Applications/Tmpl?Prefix=add&type=Index&overrideController=CorporateCodes',
             controller: 'addressController',
             pageKey: 'address',
             data: {
                 animationConf: {
                 }
             }
         })
        .when('/securityDeposit/:corpCd?', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=dep&type=Index&overrideController=CorporateCodes',
            controller: 'depositInfoController',
            pageKey: 'depositInfo',
            data: {
                animationConf: {
                }
            }
        })
        .when('/productDiscount/:corpCd?', {//acctNo
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=pdc&type=Index&overrideController=CorporateCodes',
            controller: 'productDiscountController',
            pageKey: 'productDiscount',
            data: {
                animationConf: {
                }
            }
        })
                    .when('/userManagement/:corpCd?', {
                        templateUrl: rootUrl + '/CorporateCodes/Tmpl?Prefix=usm&type=Index',
                        controller: 'userManagementController',
                        pageKey: 'userManagement'
                    })
        .when('/AccountList/:corpCd?', {
            templateUrl: rootUrl + '/CorporateCodes/Tmpl?Prefix=acc&type=Index',
            controller: 'AcctCorpListController',
            pageKey: 'AccountList',
            data: {
                animationConf: {
                }
            }
        });
        RouteAnimationManagerProvider.setDefaultAnimation('fade');
        //.when('/securityDeposit/:corpCd?', {
        //    templateUrl: '/CorporateCodes/Tmpl?Prefix=sec',
        //    controller: 'securityDepositController'
        //})
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

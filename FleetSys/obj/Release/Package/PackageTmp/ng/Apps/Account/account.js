/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
/// <reference path="account.js" />
angular.module('CardtrendApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select', 'datetime']).run(function (Utils, $rootScope, $location, $routeParams, $timeout) {
    $rootScope.tables = {};
    $rootScope.obj = {};
    $rootScope.obj.acctNo = $('#hdAcctNo').val();
    var acctNo = $rootScope.obj.acctNo;
    $rootScope.loadAdj = function myfunction() {
        $timeout(function myfunction() {
            $rootScope.dtOptionsTxnAdj = {
                serverSide: true,
                processing: true,
                checkBox: false,
                autoWidth: false,
                "scrollX": true,
                id: 'tblTxnAdj',
                ajax: $rootScope.getRootUrl() + '/Account/WebAcctMilestoneListSelect?' + $.param({ Ind: 1, workflowcd: "Adj", AcctNo: acctNo }),
                edit: {
                    level: 'scope',
                    func: 'AdjindexSelected'
                }
            }
        }, 200);
    }
    $rootScope.modalClick = function () {
        $rootScope.modalOpen = true;
        $rootScope.loadAdj();
        //Utils.makeObjectNull($scope._Object, { acctNo: acctNo });
    }
    $rootScope.loadPymt = function myfunction() {
        $timeout(function myfunction() {
            $rootScope.dtOptionsPymtTxn = {
                serverSide: true,
                processing: true,
                checkBox: false,
                autoWidth: false,
                "scrollX": true,
                id: 'tblPymtTxn',
                ajax: $rootScope.getRootUrl() + '/Account/WebAcctMilestoneListSelect?' + $.param({ Ind: 1, workflowcd: "Pymt", AcctNo: acctNo }),
                edit: {
                    level: 'scope',
                    func: 'PymtindexSelected'
                }
            }
        }, 500);
    }

    $rootScope.$on("AdjindexSelected", function (event, aData) {
        console.log(aData);
        $rootScope.obj.AcctNo = aData[1];
        location.href = $rootScope.getRootUrl() + "/Approval#/Approval/Adj/" + aData[0] + '/' + aData[1];
        //$location.path("/Approval/Adj/" + aData[0] + '/' + aData[1]);
        $rootScope.$apply();
    });

    $rootScope.$on("PymtindexSelected", function (event, aData) {
        //console.log(aData);
        $rootScope.obj.AcctNo = aData[1];
        location.href = $rootScope.getRootUrl() + "/Approval#/Approval/Pymt/" + aData[0] + '/' + aData[1];
        //$location.path("/Approval/Pymt/" + aData[0] + '/' + aData[1]);
        $rootScope.$apply();
    });
    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });

    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();
        if (_location == '/new') {
            $rootScope.obj._type = 'new';
            $rootScope.obj.applId = null;


        } else if (_location == '/') {
            $rootScope.obj._type = 'index';
        } else {
            $rootScope.obj.applId = $routeParams.applId;
            $rootScope.obj._type = 'edit';
        }
        //$rootScope.$broadcast('routeChanged', _location);
    })
})
.config(function ($routeProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=gen&type=Index',
            controller: 'generalInfoController',
            pageKey: 'generalInfo'
        })

        .when('/', {
            templateUrl: 'index.html',
            controller: 'indexController',
            pageKey: 'index',

        })
        .when('/generalInfo/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=gen&type=Index',
            controller: 'generalInfoController',
            pageKey: 'generalInfo'
        })
        .when('/finanacialInfo/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=fin&type=Index',
            controller: 'finanacialInfoController',
            pageKey: 'finanacialInfo'
        })
            // prefix = cao, parameter = account
        .when('/cao/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=cao&type=Index',
            controller: 'caoController',
            pageKey: 'cao'
        })       
        .when('/velocity/:acctNo?', {//applId
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=vel&type=Index&overrideController=Account',//Account
            controller: 'velocityController',
            pageKey: 'velocity'
        })
        .when('/vehicles/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=veh&type=Index',
            controller: 'vehiclesController',
            pageKey: 'vehicles'
        })
        .when('/depositInfo/:acctNo?', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=dep&type=Index&overrideController=Account',
            controller: 'depositInfoController',
            pageKey: 'depositInfo'
        })
        .when('/skds/:acctNo?', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=skd&type=Index&overrideController=Account',
            controller: 'skdsController',
            pageKey: 'skds'
        })
        .when('/accountSubsidy/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=ast&type=Index',
            controller: 'accountSubsidyController',
            pageKey: 'accountSubsidy'
        })
         .when('/cardsList/:acctNo?', {
             templateUrl: rootUrl + '/Account/Tmpl?Prefix=car&type=Index',
             controller: 'cardsListController',
             pageKey: 'cardsList'
         })
         .when('/contacts/:applId?', {
             templateUrl: rootUrl + '/Applications/Tmpl?Prefix=con&type=Index&overrideController=Account',
             controller: 'contactsController',
             pageKey: 'contacts'
         })
         .when('/address/:applId?', {
             templateUrl: rootUrl + '/Applications/Tmpl?Prefix=add&type=Index&overrideController=Account',
             controller: 'addressController',
             pageKey: 'address'
         })
         .when('/costcentre/:acctNo?', {
             templateUrl: rootUrl + '/Applications/Tmpl?Prefix=csc&type=Index&overrideController=Account',
             controller: 'costcentreController',
             pageKey: 'costcentre'
         })
        .when('/costcentre/:acctNo?/:cst/velocity', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=ccv&type=Index&overrideController=Account',
            controller: 'velocityController',
            pageKey: 'costCentre'
        })
         .when('/Miscellanious/:applId?', {
             templateUrl: rootUrl + '/Applications/Tmpl?Prefix=mis&type=Index&overrideController=Account',
             controller: 'MiscellaniousController',
             pageKey: 'Miscellanious'
         })
        .when('/TxnAdjustment/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=txn&type=Index',
            controller: 'TxnAdjustmentController',
            pageKey: 'TxnAdjustment'
        })
        .when('/paymentTxn/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=pay&type=Index',
            controller: 'paymentTxnController',
            pageKey: 'paymentTxn'
        })
        .when('/locationAcceptance/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=loc&type=Index',
            controller: 'locationAcceptanceController',
            pageKey: 'locationAcceptance'
        })
        .when('/tempCreditControl/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=tem&type=Index',
            controller: 'tempCreditController',
            pageKey: 'tempCreditControl'
        })
        .when('/statusMaint/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=sta&type=Index',
            controller: 'statusMaintController',
            pageKey: 'statusMaint'
        })
        .when('/txnSearch/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=aps&type=Index',
            controller: 'txnSearchController',
            pageKey: 'txnSearch'
        })
        .when('/billingItem/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=bil_Main&type=Index&OverridePrefix=bil',
            controller: 'billingItemController',
            pageKey: 'billingItem'
        })
        .when('/billingItemDetail/:acctNo?/:type/:txnId', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=bil_detail&type=Index&OverridePrefix=bil',
            controller: 'billingDetailController',
            pageKey: 'billingItem'
        })
        .when('/userManagement/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=usm&type=Index',
            controller: 'userManagementController',
            pageKey: 'userManagement'
        })
        .when('/productDiscount/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=pdc&type=Index',
            controller: 'productDiscountController',
            pageKey: 'productDiscount'
        })
        .when('/pointsAdjustment/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=pad&type=Index',
            controller: 'pointsAdjustmentController',
            pageKey: 'pointsAdjustment'
        })
        .when('/uptoDateBalance/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=upd&type=Index',
            controller: 'uptoDateBalaceController',
            pageKey: 'uptoDateBalance'
        })
        .when('/pukal/:acctNo?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=puk&type=Index',
            controller: 'pukalController',
            pageKey: 'pukal'
        })
         .when('/eventConf/:acctNo?', {
             templateUrl: rootUrl + '/Account/Tmpl?Prefix=evc&type=Index',
             controller: 'eventConfigurationController',
             pageKey: 'eventConf'
         })
        .when('/eventConfDetail/:acctNo?/:id?/:scheduleId?', {
            templateUrl: rootUrl + '/Account/Tmpl?Prefix=evc&type=Index',
            controller: 'eventConfigurationController',
            pageKey: 'eventConf'
        })
        //eventConfigurationAcct
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
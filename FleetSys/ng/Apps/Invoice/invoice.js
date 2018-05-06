/// <reference path="https://ajax.googleapis.com/ajax/libs/angularjs/1.3.0-beta.8/angular.min.js" />
angular.module('InvoiceApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select'])
.factory("Invoice", function ($http,$rootScope) {
        var Api = $rootScope.getRootUrl()+"/api/InvoiceSvc";
        var ClientsApi = $rootScope.getRootUrl()+"/api/ClientDetails";
        return {
            All: function () {
                return $http({
                    url: Api + '/GetAll',
                    method: 'GET'
                });
            },
            GetOptions: function () {
                return $http({
                    url: Api + '/GetOptions',
                    method: 'GET'
                });
            },
            GetClient: function (Id) {
                return $http({
                    url: ClientsApi + '/Get/' + Id,
                    method: 'GET'
                });
            },
            SaveInvoice: function (data, Id) {
                var Obj = {
                    ClientId: Id,
                    InvoiceItem: data
                };
                return $http({
                    url: Api + '/CreateInvoice',
                    method: 'POST',
                    data: Obj
                });
            },
            DeleteInvoice: function (Id) {
                return $http({
                    url: Api + '/DeleteInvoice/' + Id,
                    method: 'GET'
                });
            },



        }
    })
.service('InvoiceService', function () {
        var ClientList = [];
        _SelectedClient = null;
        var _Invoice = null;

        var Clients = function (op, val) {
            if (op == "get") {
                return ClientList;
            } else {
                ClientList = val;
            }
        };


        var getClientById = function (id) {
            var Client = _.find(ClientList, function (item) {
                return item.id == id;
            });
            return Client;
        };
        var SeletedClient = function (op, val) {
            if (op == "get") {
                return _SelectedClient;
            } else {
                _SelectedClient = val;
            }
        };
        var SelectedInvoice = function (op, val) {
            if (op == "get") {
                return _Invoice;
            } else {
                _Invoice = val;
            }
        };

        return {
            Clients: Clients,
            getClientById: getClientById,
            SeletedClient: SeletedClient,
            SelectedInvoice: SelectedInvoice
        };
    })
.config(function ($routeProvider) {
     $routeProvider.when('/', {
         templateUrl: 'index.html',
         controller: 'indexController',
     })
     .when('/new', {
         templateUrl: 'main.html',
         controller: 'mainController',
         pageKey: 'generalInfo'
     })
 })
.controller('indexController', function ($scope, $http, $location, Invoice, Utils, InvoiceService,$rootScope) {
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblCorporateCodes',
            ajax: $rootScope.getRootUrl()+'/CorporateCodes/ftGetCorpAcctList',
            "aoColumnDefs": [
                              { "sClass": "text-right", "aTargets": [2] },
            ],
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
    })
.controller('mainController', function ($scope, $http, $location, Invoice, Utils, InvoiceService, $rootScope) {
        $scope.Items = [];
        $scope.invoiceSaved = false;
        $scope.Total = 0.00;
        $scope.selectedClient = {
            Id: null
        };

        $http.get($rootScope.getRootUrl()+'/Multipayment/GetDropDown').success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope.$watch('_Object.ChequeAmt', function (newValue) {
                $scope._Object.ChequeAmt = newValue.replace(/,/, "");
                itemsChanged();
            }, true);
        });


        $scope.CreateNewItem = function () {
            var len = $scope.Items.length;
            if (len > 0) {
                $scope.Items[len - 1].isEdit = false;
            }

            $scope.Items.push({
                Descp: null,
                AcctNo: null,
                Amount: 0.00,
                isEdit: true,
                UnitNo: len + 1
            });
        }
        var itemsChanged = function (newValue) {
            var arrAmt = _.map($scope.Items, function (item) {
                if (item.Amount) {
                    amt = item.Amount.replace(/,/, '');
                    return parseFloat(amt);
                }
            });
            //   var totalAmt = _.reduce(arrAmt, function (prev, current) { return prev + current }, 0);
            var totalAmt = arrAmt.reduce(function (prev, current) { return prev + current; }, 0);

            $scope.Total = parseFloat(totalAmt || 0.00).toFixed(2) || 0.00;

            if ($scope._Object) {
                $scope.Difference = (parseFloat($scope._Object.ChequeAmt || 0) - ($scope.Total)).toFixed(2);
            }
            else {
                $scope.Difference = parseFloat(0 - ($scope.Total || 0)).toFixed(2);
            }
        }

        $scope.$watch('Items', function (newValue) {
            itemsChanged(newValue);
        }, true);

        $scope.removeItem = function (item) {
            var index = $scope.Items.indexOf(item);
            $scope.Items.splice(index, 1);
        }
        $scope.PrintInvoice = function () {
            if (!$scope.invoiceSaved) {
                $('#modal-4').modal('show', { backdrop: 'static' });
            }
        }
        $scope.SaveAndPrint = function () {
            Invoice.SaveInvoice($scope.Items, $scope.selectedClient.Id).success(function (data) {
                toastr.reaResponse(data);
                if (data.Flag == 0) {
                    //Now print the invoice

                }
            });
        }
        $scope.EditInvoice = function (item) {
            console.log(InvoiceService.Clients('get'));
            InvoiceService.SelectedInvoice('set', item);
            $location.path('/details').search({ InvoiceId: item.InvoiceId });
        };
        $scope.DeleteInvoice = function (id) {
            Invoice.DeleteInvoice(id).success(function (data) {
                toastr.reaResponse(data);
            });
        };


    })
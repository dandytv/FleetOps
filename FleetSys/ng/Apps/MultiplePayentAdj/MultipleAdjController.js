(function () {
    var injectParam1 = ['$scope', '$rootScope', '$http', '$location', 'Invoice', 'Utils', '$timeout'];
    var injectParam2 = ['$scope', '$route','$http','$timeout', '$rootScope','$location','$routeParams','Invoice','Utils'];

    var indexController = function ($scope, $rootScope, $http, $location, Invoice, Utils, $timeout) {
        $http.get($rootScope.getRootUrl() + '/Account/FillData?Prefix=txn&AcctNo=1').success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });

        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            "bDestroy": true,
            id: 'tblMultipleAdj',
            ajax: $rootScope.getRootUrl() + '/MultipleTxn/ftMultipleAdj',
            //"aoColumnDefs": [
            //                  { "sClass": "text-right", "aTargets": [2] }
            //],
            edit: {
                level: 'scope',
                func: 'indexSelected'
            },
            aoColumnDefs: [
                { "sClass": "detail-toggler", "aTargets": [0] },
            ],
            childTable: {
                format: function (nRows) {
                    var htm;
                    var that = this;
                    var rows = _.filter($scope.aaData, function (data) {
                        return data[1] == nRows[1];//batchid
                    });
                    var header = "<thead><tr><th>Txn Code</th><th>Ref No</th><th>Txn Cnt</th><th>Txn Amount</th><th>Owner</th><th>Status</th></tr></thead>";
                    var rows2 = _.map(rows, function (Next) {
                        return '<tr>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[3] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[4] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[5] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[6] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[7] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[8] + '</td></tr>'
                    });
                    var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                    return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr>';
                },

                edit: {
                    level: 'format',
                    func: function (cells) {
                        var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
                        //console.log(obj[1], cells[1]);
                        $location.path("edit/" + obj[1] + "/" + cells[1] + "/False");
                        $scope.$apply();
                    }
                },
                sum: function sum(numbers) {
                    return _.reduce(numbers, function (result, current) {
                        return result + parseFloat(current[9]);
                    }, 0);
                },
                fngroupOp: function (e, settings, json) {
                    //json.aaData = [];

                    if ($scope.date) {
                        if (new Date() - $scope.date <= 2000)
                            return;
                    }
                    $scope.$apply(function () {
                        $scope.date = new Date();
                        $scope.aaData = json.aaData;
                    });
                    var rows = [];
                    _.each(json.aaData, function (data) {
                        if (rows.length) {
                            var contains = _.find(rows, function (item) {
                                return item[1] == data[1];
                            })

                            if (!contains) {

                                rows.push(data);
                            }
                        } else {

                            rows.push(data);
                        }
                    });
                    json.aaData = rows;
                },
            },
        };

        $scope.modalOpen = false;
        $scope.newpayment = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            $location.path('/edit/' + obj[1] + '/1' + '/True');
        }

        $scope.$on('SubTableSelected', function (data, rows) {
            $location.path("edit/" + row[0]);
            $scope.$apply();
        });

        $scope.refresh = function () {
            setTimeout(function () {
                $rootScope.tables[$scope.dtOptions.id].fnReloadAjax($rootScope.getRootUrl() + "/MultipleTxn/ftMultipleAdj");
            }, 1500);
            $('#tblMultipleAdj-options').css('display', 'none')
        }
        $scope.SaveTxnAdjustment = function () {
            Utils.InfoNotify();
            Invoice.SaveTxnAdj($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.txn);
                if (data.txn.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }
        $scope.modalOpen = false;
    };

    var mainController = function ($scope, $route, $http, $timeout, $rootScope, $location, $routeParams, Invoice, Utils) {
        $scope.PaymentModalOpen = false;
        $scope.Items = [];
        $scope.invoiceSaved = false;
        $scope.Total = 0.00;
        $scope.processing = false;
        $scope.entryDisabled = false;
        $scope.selectedClient = {
            Id: null
        };

        var itemsChanged = function (newValue) {
            var arrAmt = _.map($scope.Items, function (item) {
                if (item.TxnAmt) {
                    amt = item.TxnAmt.replace(/,/g, '');
                    return Number(amt);
                } else {
                    return 0;
                }
            });
            var totalAmt = arrAmt.reduce(function (prev, current) { return prev + current; }, 0);

            $scope.Total = Number(totalAmt || 0.00).toFixed(2) || 0.00;

            if ($scope._Object) {
                $scope.Difference = (Number($scope._Object.ChequeAmt || 0) - ($scope.Total)).toFixed(2);
            }
            else {
                $scope.Difference = Number(0 - ($scope.Total || 0)).toFixed(2);
            }
        }


        if ($routeParams.batchId) {
            if ($routeParams.paymentSelected === 'True') {
                //alert();
                $scope.batchId = $routeParams.batchId;
                $http.get($rootScope.getRootUrl() + '/MultipleTxn/GetAdjDropDown').success(function (data) {
                    $scope._Selects = data.Selects;
                    $scope._Object = data.Model;
                    $scope.$watch('_Object.ChequeAmt', function (newValue) {
                        $scope._Object.ChequeAmt = newValue.replace(/,/g, "");
                        itemsChanged();
                    }, true);
                });
            }
            else if ($routeParams.paymentSelected === 'False') {
                //alert();
                $scope.batchId = $routeParams.batchId;
                $scope.ChequeNo = $routeParams.ChequeNo;
                $scope.isEdit = true;
                $scope.entryDisabled = true;
                Invoice.getTxn({ BatchId: $scope.batchId, ChequeNo: $scope.ChequeNo }).success(function (data) {
                    $scope.Items = data.list.MultipleTxnRecord;
                    $scope.$watch('_Object.ChequeAmt', function (newValue) {
                        $scope._Object.ChequeAmt = newValue.replace(/,/g, "");
                        itemsChanged();
                    }, true);
                    data.list.SelectedOwner = data.list.SelectedOwner;
                    data.list.SelectedPaymentType = data.list.SelectedPaymentType;
                    data.list.SelectedAdjTxnCode = data.list.SelectedAdjTxnCode;
                    $scope._Object = data.list;

                    $scope.getGlCode();

                    $http.get($rootScope.getRootUrl() + '/MultipleTxn/GetAdjDropDown').success(function (data) {
                        $scope._Selects = data.Selects;
                        // $scope._Object = data.Model;
                    });
                    //$scope.TxnCodeChanged(null, $scope._Object.SelectedPaymentType);
                    //$scope.$apply();

                });

            }
        }
        else {
            $http.get($rootScope.getRootUrl() + '/MultipleTxn/GetAdjDropDown').success(function (data) {
                $scope._Selects = data.Selects;
                $scope._Object = data.Model;
                $scope._Object.ChequeAmt = 0.00;
                $scope.$watch('_Object.ChequeAmt', function (newValue) {
                    $scope._Object.ChequeAmt = newValue.replace(/,/g, "");
                    itemsChanged();
                }, true);
            });
        }

        $scope.getGlCode = function () {
            var AcctNo = null;
            if ($scope.Items.length) {
                AcctNo = $scope.Items[0].AcctNo;
            }
            Invoice.WebGetGLCode({ SelectedAdjTxnCode: $scope._Object.SelectedAdjTxnCode, AcctNo: AcctNo }).success(function (data) {
                if ($scope.isEdit) {
                    //alert(item.TxnDescp);
                    $scope.array = data;
                }
                //$scope._Object.GLTxnCode = data;
                //$scope._Object.GLDescp = data;
                //$scope._Object.GLCodeDescp = data;
            })
        }

        $scope.acctNoChanged = function (item) {
            var acctNo = item.AcctNo;
            $http.get($rootScope.getRootUrl() + '/Repository/_Querymeta?name_startsWith=cac' + acctNo + '&AppendWildCard=false').success(function (data) {
                if (data.theResult.length) {
                    item.AcctName = data.theResult[0].Descp.split(':')[1];
                }
            })
        }

        $scope.CreateNewItem = function () {
            $scope.entryDisabled = false;
            console.log($scope.Items);
            var len = $scope.Items.length;
            if (len > 0) {
                $scope.Items[len - 1].isEdit = false;
            }

            $scope.Items.push({
                Descp: null,
                AcctNo: null,
                TxnAmt: 0.00,
                isEdit: true,
                UnitNo: len + 1
            });
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
        $scope.AddTxn = function () {
            $scope.PaymentModalOpen = false;
            $scope._Object.BatchId = $routeParams.batchId;
            if ($scope._Object.SelectedAdjTxnCode == '' || $scope._Object.SelectedAdjTxnCode == null) {
                return Utils.PNotify({ flag: 1, Descp: "The Transaction code dropdown is required " });
            }
            else if ($scope._Object.TxnDate == null || $scope._Object.TxnDate == '') {
                return Utils.PNotify({ flag: 1, Descp: "The Transaction Date field is required " });
            }
            else if ($scope._Object.ChequeAmt == 0 || $scope._Object.ChequeAmt == null) {
                return Utils.PNotify({ flag: 1, Descp: "The amount field is required " });
            }
            else if ($scope._Object.SelectedOwner == '' || $scope._Object.SelectedOwner == null) {
                return Utils.PNotify({ flag: 1, Descp: "The Owner dropdown is required " });
            }
            else if (!$scope.Total || $scope.Total == 0.00) {
                return Utils.PNotify({ flag: 1, Descp: "Empty transaction" });
            }
            $scope.Items.forEach(function (item) {
                item.SelectedOwner = $scope._Object.SelectedOwner;
                //if (item.AcctNo == null || item.AcctNo == '') {
                //    return Utils.PNotify({ flag: 1, Descp: "The Account No field is required " });
                //}
            });

            Utils.InfoNotify();
            Invoice.SaveTxn($scope.Items, $scope._Object).success(function (data) {
                $scope._Object.BatchId = data.batchId;
                $scope._Object.RetCd = data.RetCd;
                $scope._Object.ChequeNo = data.chequeNo;
                Utils.finalResultNotify(data.resultCd);
                // Utils.InfoNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $scope.PaymentModalOpen = true;
                    //$location.path('/');
                }
            });
        }

        $scope.ContinuePayment = function () {
            //alert($scope._Object.ChequeNo);
            // var count = 20;//$scope.Items.length;
            var random = Math.floor(Math.random() * (100 + 1) + 10);
            $scope.PaymentModalOpen = false;
            $scope.newpayment = null;
            var path = '/edit/' + $scope._Object.BatchId + '/' + $scope._Object.ChequeNo + '/True';
            $timeout(function () {
                $location.path('/edit/' + $scope._Object.BatchId + '/' + $scope._Object.ChequeNo + '/True');//random
                $scope.$apply();
            }, 400);

        }

        $scope.ClosePayment = function () {

            $scope.PaymentModalOpen = false;
            $timeout(function () {
                $location.path('/');
                $scope.$apply();
            }, 400);

        }

        $scope.insertRecord = function (item) {
            //alert();
            console.log($scope.isEdit);
            if ($scope.isEdit) {
                //alert(item.TxnDescp);
                item.processing = true;
                var obj = $scope._Object;
                $.extend(item, {
                    TotAmnt: item.TxnAmt,
                    SelectedTxnCd: obj.SelectedAdjTxnCode,
                    TxnDate: obj.TxnDate,
                    SelectedTxnCd: obj.SelectedAdjTxnCode,
                    ChequeAmt: obj.ChequeAmt,
                    //CheqNo: obj.CheqNo,
                    AccNo: item.AcctNo,
                    SelectedOwner: obj.SelectedOwner,
                    Descp: item.TxnDescp,
                    TxnId: item.TxnId,
                    InvoiceNo: item.InvoiceNo,
                    AppvCd: item.AppvCd,
                    DeftBusnLocation: item.DeftBusnLocation,
                    DeftTermId: item.DeftTermId,
                    //Descp: obj.TxnDescp,
                });
                item.isEdit = false;
            } else {
                item.isEdit = false;
            }
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
    };

    //inject service
    indexController.$inject = injectParam1;
    mainController.$inject = injectParam2;

    angular.module('multipleAdjApp').controller('indexController', indexController);
    angular.module('multipleAdjApp').controller('mainController', mainController);
}());
(function () {
    var injectParams = ['$scope', '$timeout', '$rootScope', '$http', '$location', '$routeParams', 'Invoice', 'Utils'];
    var injectParam2 = ['$scope', '$rootScope', '$http', '$location', 'Invoice','Utils'];

    var indexController = function ($scope, $rootScope, $http, $location, Invoice, Utils) {
        $http.get($rootScope.getRootUrl() + '/Account/FillData?Prefix=txn&AcctNo=1').success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });

        $scope.showOptions = false;
        $scope.modalOpen = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblMultipleAdj',
            ajax: $rootScope.getRootUrl() + '/MultiPayment/ftMultiplePayment',
            //"aoColumnDefs": [
            //                  { "sClass": "text-right", "aTargets": [2] },
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
                    var rows = _.filter($scope.aaData, function (data) {
                        return data[1] == nRows[1];//batchid
                    });
                    var header = "<thead><tr><th>Txn Code</th><th>Ref No</th><th>Cheque No</th><th>Txn Cnt</th><th>Txn Amount</th><th>Owner</th><th>Status</th></tr></thead>";
                    var rows2 = _.map(rows, function (Next) {
                        return '<tr>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[3] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[4] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[5] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[6] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[7] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[8] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[9] + '</td>' +
                           '</tr>'
                    });
                    var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                    return '<tr class=\"dynamic-created\"><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr>';
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
                }
            }
        };

        $scope.newpayment = function () {
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            $location.path('/edit/' + obj[1] + '/0' + '/True');
        }
        $scope.refresh = function () {
            setTimeout(function () {
                $rootScope.tables[$scope.dtOptions.id].fnReloadAjax($rootScope.getRootUrl() + '/MultiPayment/ftMultiplePayment');
            }, 1000);
            //$rootScope.tables[$scope.dtOptions.id].fnDraw();
            $('#tblMultipleAdj-options').css('display', 'none'); // i know, its bad, will change later
            $scope.showOptions = false;
        }


        $scope.SaveTxnAdjustment = function () {
            Utils.InfoNotify();
            Invoice.SaveTxnAdj($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.txn);
                if (data.txn.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }
        $scope.modalOpen = false;

    };
    //main Controller
    var mainController = function ($scope, $timeout, $rootScope, $http, $location, $routeParams, Invoice, Utils) {
        $scope.PaymentModalOpen = false;
        $scope.entryDisabled = false;
        $scope.Items = [];
        $scope.invoiceSaved = false;
        $scope.Total = 0.00;
        $scope.selectedClient = {
            Id: null
        };

        if ($routeParams.batchId) {
            if ($routeParams.paymentSelected === 'True') {
                $scope.batchId = $routeParams.batchId;
                $http.get($rootScope.getRootUrl() + '/MultipleTxn/GetDropDown').success(function (data) {
                    $scope._Selects = data.Selects;
                    $scope._Object = data.Model;
                    $scope.$watch('_Object.ChequeAmt', function (newValue) {
                        //if (newValue.toString().indexOf(',') !== -1)
                        // {
                        $scope._Object.ChequeAmt = ("" + newValue).replace(/,/g, "");
                        itemsChanged();
                        // }
                    }, true);
                });
            }
            else if ($routeParams.paymentSelected === 'False') {
                //if ($routeParams.batchId) {
                $scope.batchId = $routeParams.batchId;
                $scope.ChequeNo = $routeParams.ChequeNo;
                $scope.entryDisabled = true;
                $scope.isEdit = true;
                Invoice.getTxn({ BatchId: $scope.batchId, ChequeNo: $scope.ChequeNo }).success(function (data) {
                    $scope.Items = data.list.MultipleTxnRecord;
                    $scope.$watch('_Object.ChequeAmt', function (newValue) {
                        //if (newValue.toString().indexOf(',') !== -1) {
                        $scope._Object.ChequeAmt = ("" + newValue).replace(/,/g, "");
                        itemsChanged();
                        //}
                    }, true);
                    $scope._Object = data.list;
                    $http.get($rootScope.getRootUrl() + '/MultipleTxn/GetDropDown').success(function (data) {
                        $scope._Selects = data.Selects;
                    });
                    $scope.loadGlCodeInfo();
                    Invoice.WebGetPymtTxnCd({ GlSettlementCd: $scope._Object.SelectedGLSettlement }).then(function (obj) {
                        if(obj != null)
                        $scope._Selects.TxnCode = obj.data;
                    });
                   // $scope.$apply();
                });
            }

        } else {
            $http.get($rootScope.getRootUrl() + '/MultipleTxn/GetDropDown').success(function (data) {
                $scope._Selects = data.Selects;
                $scope._Object = data.Model;
                $scope._Object.ChequeAmt = 0.00;
                $scope.$watch('_Object.ChequeAmt', function (newValue) {
                    //if (newValue.toString().indexOf(',') !== -1) {
                    $scope._Object.ChequeAmt = ("" + newValue).replace(/,/g, "");
                    itemsChanged();
                    //}
                }, true);
            });
        }



        $scope.acctNoChanged = function (item) {
            var acctNo = item.AcctNo;

            $http.get($rootScope.getRootUrl() + '/Repository/_Querymeta?name_startsWith=cac' + acctNo + '&AppendWildCard=false').success(function (data) {

                if (data.theResult.length) {
                    item.AcctName = data.theResult[0].Descp.split(':')[1];

                }
            })
        };

        $scope.TxnCodeChanged = function (obj, txnCd) {
            $scope.loadGlCodeInfo(txnCd);
        }

        $scope.GlSettlementChanged = function (obj, Gl) {
            $scope.loadGlCodeInfo(null, Gl);
            Invoice.WebGetPymtTxnCd({ GlSettlementCd: Gl }).then(function (obj) {

                $scope._Selects.TxnCode = obj.data;
            });
        }

        $scope.loadGlCodeInfo = function (txnCd, glSettlement, AcctNo) {
            var AcctNo = $scope.Items.length ? $scope.Items[0].AcctNo : null;
            var GlSettlement = glSettlement || $scope._Object.SelectedGLSettlement;
            var TxnCd = txnCd || $scope._Object.SelectedTxnCode;


            if (AcctNo && GlSettlement && TxnCd) {
                Invoice.WebGetGLCode({ SelectedTxnCode: TxnCd, SelectedGLSettlement: GlSettlement, AcctNo: AcctNo }).success(function (data) {
                    //alert(item.TxnDescp);
                    $scope.array = data;
                    //$scope._Object.GLTxnCode = data;
                    //$scope._Object.GLDescp = data;
                    //$scope._Object.GLCodeDescp = data;
                });
            }
        }


        $scope.CreateNewItem = function () {
            $scope.entryDisabled = false;
            var len = $scope.Items.length;
            //console.log($scope.Items);
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
        var itemsChanged = function (newValue) {
            var arrAmt = _.map($scope.Items, function (item) {
                if (item.TxnAmt) {
                    amt = item.TxnAmt.replace(/,/g, '');
                    return Number(amt);
                } else {
                    return 0;
                }
            });
            //   var totalAmt = _.reduce(arrAmt, function (prev, current) { return prev + current }, 0);
            var totalAmt = arrAmt.reduce(function (prev, current) { return prev + current; }, 0);

            $scope.Total = Number(totalAmt || 0.00).toFixed(2) || 0.00;

            if ($scope._Object) {
                $scope.Difference = (Number($scope._Object.ChequeAmt || 0) - ($scope.Total)).toFixed(2);
            }
            else {
                $scope.Difference = Number(0 - ($scope.Total || 0)).toFixed(2);
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
        //$scope.ContinuePaymModalOpen = true;

        $scope.AddTxn = function () {
            $scope.PaymentModalOpen = false;
            $scope._Object.BatchId = $routeParams.batchId;

            if ($scope._Object.SelectedTxnCode == '' || $scope._Object.SelectedTxnCode == null) {
                return Utils.PNotify({ flag: 1, Descp: "The Txn Code dropdown is required " });
            }
            else if ($scope._Object.SelectedOwner == '' || $scope._Object.SelectedOwner == null) {
                return Utils.PNotify({ flag: 1, Descp: "The Owner dropdown is required " });
            }
            else if ($scope._Object.TxnDate == null || $scope._Object.TxnDate == '') {
                return Utils.PNotify({ flag: 1, Descp: "The Transaction Date is a mandatory field " });
            }
            else if ($scope._Object.DueDate == null || $scope._Object.DueDate == '') {
                return Utils.PNotify({ flag: 1, Descp: "The Due Date is a mandatory field " });
            }
            else if ($scope._Object.SelectedIssueingBank == null || $scope._Object.SelectedIssueingBank == '') {
                return Utils.PNotify({ flag: 1, Descp: "The Issuing Bank dropdown is required " });
            }
            else if ($scope._Object.ChequeAmt == 0 || $scope._Object.ChequeAmt == null) {
                return Utils.PNotify({ flag: 1, Descp: "The Amount field is required " });
            }
            else if ($scope._Object.ChequeNo == 0 || $scope._Object.ChequeNo == null) {
                return Utils.PNotify({ flag: 1, Descp: "The Cheque No field is required " });
            }
            else if ($scope._Object.SlipNo == 0 || $scope._Object.SlipNo == null) {
                return Utils.PNotify({ flag: 1, Descp: "The Slip No field is required " });
            }
            if (!$scope.Total || $scope.Total == 0.00) {
                return Utils.PNotify({ flag: 1, Descp: "Empty transaction" })
            }
            Utils.InfoNotify();
            $scope._Object.RefId = $scope._Object.RefNo;
            Invoice.SaveTxn($scope.Items, $scope._Object).success(function (data) {
                $scope._Object.BatchId = data.batchId;
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $scope.PaymentModalOpen = true;
                    $scope.$apply();
                } else {
                    itemsChanged();
                }
            });
        }
        $scope.ContinuePayment = function () {
            $scope.PaymentModalOpen = false;
            $timeout(function () {
                //window.open('/edit/' + $scope._Object.BatchId + '/0' + '/True', null,null, false);
                $location.path('/edit/' + $scope._Object.BatchId + '/' + $scope._Object.ChequeNo + '/True');
                $scope.$apply();
            }, 400)
        }

        $scope.ClosePayment = function () {

            $scope.PaymentModalOpen = false;
            $timeout(function () {
                $location.path('/');
                $scope.$apply();
            }, 400);
        }

        $scope.insertRecord = function (item) {
            if ($scope.isEdit) {
                //alert(item.TxnDescp);
                item.processing = true;
                var obj = $scope._Object;
                $.extend(item, {
                    TotAmnt: item.TxnAmt,
                    SelectedTxnCd: obj.SelectedTxnCode,
                    TxnDate: obj.TxnDate,
                    //SelectedTxnCd: obj.SelectedTxnCode,
                    ChequeAmt: obj.ChequeAmt,
                    ChequeNo: obj.ChequeNo,
                    AcctNo: item.AcctNo,
                    SelectedOwner: obj.SelectedOwner,
                    Descp: item.TxnDescp,
                    PyTxnId: item.TxnId,
                    GLTxnCode: item.GLTxnCode,
                    GLDescp: item.GLDescp,
                    GLCodeDescp: item.GLCodeDescp,
                    //Descp: obj.TxnDescp,
                });
                item.isEdit = false;
            } else {
                item.isEdit = false;
            }
            $scope.loadGlCodeInfo();
        }

        $scope.EditInvoice = function (item) {
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
    indexController.$inject = injectParam2;
    mainController.$inject = injectParams;

    angular.module('multiPaymentApp').controller('indexController', indexController);
    angular.module('multiPaymentApp').controller('mainController', mainController);

}());
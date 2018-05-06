(function () {
    var injectParams = ['$scope','$rootScope', '$http', '$location', '$timeout', 'Utils', 'Api'];
    var indexController = function ($scope, $rootScope, $http, $location, $timeout, Utils, Api) {
        Api.getModel({}).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });
        $scope.showOptions = false;
        $scope.acctTab = true;
        $scope.$on('SearchItemSelected', function (event, obj) {
            console.log(obj);
            Api.getObjectDtl({ Prefix: obj.item.desc.prefix, Value: obj.item.desc.match }).success(function (data) {
                $.extend($scope._Object, data.objectDetail);
            })

        });
        $scope.refresh = function () {
            $scope.acctTab ? $rootScope.tables[$scope.dtAcctOptions.id].fnDraw() : $rootScope.tables[$scope.dtMerchOptions.id].fnDraw();
        }
        $scope.dtAcctOptions = {
            serverSide: false,
            processing: true,
            scrollX: true,
            autoWidth: false,
            retrieve: true,
            //checkBox: true,
            id: 'tblTxnSearch',
            aoColumnDefs: [
        { "sClass": "detail-toggler", "aTargets": [0] },
            ],
            childTable: {
                format: function (nRows) {
                    var htm;
                    var rows = _.filter($scope.aaData, function (data) {
                        return data[14] == nRows[14];//txnid
                    });
                    var header = "<thead><tr><th>Product Descp</th><th>Quantity</th><th>Product Amount</th><th>VAT Amount</th><th>VAT Cd</th><th>VAT Rate</th></tr></thead>";
                    var rows2 = _.map(rows, function (Next) {
                        return '<tr>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[16] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[17] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[18] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[19] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[20] + '</td>' +
                          '<td style=\'background-color:#F8F9FA\'>' + Next[21] + '</td></tr>'
                    });
                    var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                    return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr>';
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
                                return item[14] == data[14];
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
        $scope.SearchTxn = function () {
            obj = $scope._Object;
            var $table = $rootScope.tables[$scope.dtAcctOptions.id];
            var uri = $scope.acctTab ? ($rootScope.getRootUrl() + '/TxnSearch/WebAcctTxnSearch?isDownload=false&' + $.param($scope._Object)) :
                ($rootScope.getRootUrl() + '/TxnSearch/WebMerchTxnSearch?isDownload=false&' + $.param($scope._Object));
            var value = angular.extend($scope.acctTab ? $scope.dtAcctOptions : $scope.dtMerchOptions, {
                serverSide: true, ajax: uri, destroy: true, "drawCallback": function (settings) {
                    $scope.modalOpen = false;
                    $scope.$apply();
                }
            });
            $scope.$broadcast('updateDataTable', { options: value });
        }
        $scope.loadMerchant = function () {
            Utils.makeObjectNull($scope._Object, {});//added
            $scope.acctTab = false;
            $timeout(function () {
                $scope.dtMerchOptions = {
                    serverSide: false,
                    processing: true,
                    scrollX: true,
                    autoWidth: false,
                    retrieve: true,
                    //checkBox: true,
                    id: 'tblMerchTxnSearch',
                    aoColumnDefs: [{ "sClass": "detail-toggler", "aTargets": [0] }, ],
                    childTable: {
                        format: function (nRows) {
                            var htm;
                            var rows = _.filter($scope.aaData, function (data) {
                                return data[11] == nRows[11];//txnid
                            });
                            //{null,x.InvoicDt, x.TxnDate,x.PrcsDate, x. AcctNo,x.SelectedCardNo,x.AuthCardNo , x.TxnDesp,x.VehRegNo,x.Stan,
                            // x.ApproveCd, x.RRn,x.VATNo, x.Dealer, x.TxnId, x.TxnAmt,x.ProductDescp,x.Quantity,x.ProductAmt,x.VATAmt,x.VATCd,x.VATRate,
                            var header = "<thead><tr><th>Product Descp</th><th>Quantity</th><th>Product Amount</th><th>VAT Amount</th><th>VAT Cd</th><th>VAT Rate</th></tr></thead>";
                            var rows2 = _.map(rows, function (Next) {
                                return '<tr>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[12] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[13] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[14] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[15] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[17] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[18] + '</td></tr>'
                            });
                            var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                            console.log(fullTable);
                            return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr>';
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
                                        return item[11] == data[11];
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
                }
            }, 300);
        }
        $scope.loadAcct = function () {
            Utils.makeObjectNull($scope._Object, {});//added
            $scope.acctTab = true;
        }
        $scope.downloadReport = function () {
            if ($scope.acctTab) {
                location.href = $rootScope.getRootUrl() + "/TxnSearch/WebAcctTxnSearch?isDownload=true&" + $.param($scope._Object);
            } else {
                location.href = $rootScope.getRootUrl() + "/TxnSearch/WebMerchTxnSearch?isDownload=true&" + $.param($scope._Object);
            }
        }
    };

    //inject service
    indexController.$inject = injectParams;
    angular.module('txnSearchApp').controller('indexController', indexController);
}());
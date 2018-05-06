(function () {
    var injectParamsIndex = ['$scope', '$rootScope', '$location', 'Api'];
    var injectParamsDetail = ['$scope', '$routeParams', '$rootScope', '$location', 'Api', 'Utils'];
    var injectParamsTxnDispute = ['$scope','$routeParams','$rootScope','$location','Api','Utils','$compile'];
    var injectParamsFileManager = ['$scope', '$rootScope', '$routeParams', '$location', 'Api', 'Utils', '$http'];

    var indexController = function ($scope, $rootScope, $location, Api) {
        $rootScope.obj._type = 'index';
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblFraudCastList',
            ajax: $rootScope.getRootUrl() + '/Fraud/FTGetFraudCaseList',
            "createdRow": function (row, data, dataIndex) {
                if (data[4].toString().toLowerCase() == "close") {
                    $(row).find('td').eq(4).addClass('label-success');
                }
                else if (data[4].toString().toLowerCase() == "open") {
                    $(row).find('td').eq(4).addClass('label-danger');
                }
                else if (data[4].toString().toLowerCase() == "in progress") {
                    $(row).find('td').eq(4).addClass('label-warning');
                }
            },
            edit: {
                level: 'scope',
                func: 'indexSelected'

            }
        };
        $scope.$on("indexSelected", function (event, aData) {
            $rootScope.obj.eventId = aData[0];
            $location.path('/events/' + aData[0]);
            $scope.$apply();
        });
    };
    var detailsController = function ($scope, $routeParams, $rootScope, $location, Api, Utils) {
        var eventId = $routeParams.eventId || $rootScope.obj.eventId;
        $scope.isNew = eventId ? false : true;
        $scope._Object = {};
        $scope._Selects = {};
        $scope.FraudCards = [];
        $scope._Selects.CardNo = [];
        //initial FraudIncidentsViewModel
        $scope._Object.FraudIncidentsViewModel = null;
        $scope._Selects.FraudIncidentsViewModel = null;

        //initial FraudCustomerDetailsViewModel
        $scope._Object.FraudCustomerDetailsViewModel = null;

        //initial FraudCardDetailsViewModel
        $scope._Object.FraudCardDetailsViewModel = null;

        Api.GetFormData({ Prefix: 'details', EventId: eventId }).success(function (data) {

            $scope._Object.FraudIncidentsViewModel = data._FraudIncidentsViewModel;
            $scope._Selects.FraudIncidentsViewModel = data.Selects;
            $scope.obj.acctNo = data._FraudCustomerDetailsViewModel.AcctNo;
            $scope._Object.FraudCustomerDetailsViewModel = data._FraudCustomerDetailsViewModel;
            $scope._Object.FraudCardDetailsViewModel = data._FraudCardDetailsViewModel;
            //  $scope._Object.FraudCardDetailsViewModel.FraudCards.SelectedCardNo
            $scope.FraudCards = $scope._Object.FraudCardDetailsViewModel.FraudCards;
            $scope._Selects.CardNo = data.CardNo.CardNo;
            $scope.Enum_NatureOfIncident = data.Enum_NatureOfIncident;
            if (data.aaData && data.model) {
                $scope.showCustTable = true;
                $scope.CustDetailsSales = data.aaData;
                $scope.FraudCards = data.liFraudCards;
                // $scope._Object.FraudCustomerDetailsViewModel.AcctNo = obj.AcctNo;
                // $scope._Object.FraudCustomerDetailsViewModel.EventId = eventId;
                $rootScope.obj.cmpyName = data.model[0];
                $scope._Object.FraudCustomerDetailsViewModel.CmpyName1 = data.model[0];
                $scope._Object.FraudCustomerDetailsViewModel.AccountType = data.model[1];
                $scope._Object.FraudCustomerDetailsViewModel.ClientType = data.model[2];
                $scope._Object.FraudCustomerDetailsViewModel.AgeingDays = data.model[3];
                $scope.Month1Date = data.model[4];
                $scope.Month2Date = data.model[5];
                $scope.Month3Date = data.model[6];
                $scope.Month4Date = data.model[7];
                $scope.Month5Date = data.model[8];
                $scope.Month6Date = data.model[9];
                $scope.AvgSalesDisplay = data.model[10];
            }
            else {
                $scope.showCustTable = false;
            }


            if (data.cardaaData && data.cardaaData.length > 0 && data.cardModel) {
                $scope.showCardTable = true;
                $scope.CardDetailsSales = data.cardaaData;
                $scope.CardMonth1Date = data.cardModel[0];
                $scope.CardMonth2Date = data.cardModel[1];
                $scope.CardMonth3Date = data.cardModel[2];
                $scope.CardMonth4Date = data.cardModel[3];
                $scope.CardMonth5Date = data.cardModel[4];
                $scope.CardMonth6Date = data.cardModel[5];
                $scope.CardAvgSalesDisplay = data.cardModel[6];
            }
            else {
                $scope.showCardTable = false;
                $scope.CardDetailsSales = null;
                $scope.CardMonth1Date = null;
                $scope.CardMonth2Date = null;
                $scope.CardMonth3Date = null;
                $scope.CardMonth4Date = null;
                $scope.CardMonth5Date = null;
                $scope.CardMonth6Date = null;
                $scope.CardAvgSalesDisplay = null;
            }
        })

        $scope.entryDisabled = false;


        $scope.$on('SearchItemSelected', function (event, obj) {

            obj = {
                AcctNo: obj ? obj.item.value : val
            };
            $scope.obj.acctNo = obj.AcctNo;

            Api.GetFraudCustomerDetailsList({ EventId: $scope.eventId, AcctNo: obj.AcctNo }).success(function (data) {

                $scope.CustDetailsSales = data.aaData;
                $scope._Object.FraudCardDetailsViewModel.AcctNo = obj.AcctNo;
                $scope._Object.FraudCustomerDetailsViewModel.AcctNo = obj.AcctNo;
                $rootScope.obj.cmpyName = data.model[0];
                $scope._Object.FraudCustomerDetailsViewModel.CmpyName1 = data.model[0];
                $scope._Object.FraudCustomerDetailsViewModel.AccountType = data.model[1];
                $scope._Object.FraudCustomerDetailsViewModel.ClientType = data.model[2];
                $scope._Object.FraudCustomerDetailsViewModel.AgeingDays = data.model[3];
                $scope.Month1Date = data.model[4];
                $scope.Month2Date = data.model[5];
                $scope.Month3Date = data.model[6];
                $scope.Month4Date = data.model[7];
                $scope.Month5Date = data.model[8];
                $scope.Month6Date = data.model[9];
                $scope.AvgSalesDisplay = data.model[10];

                if (data.model[10]) {
                    $scope.showCustTable = true;
                }
                else {
                    $scope.showCustTable = false;
                }

                Api.GetCardNoList_ByAcctNo({ AcctNo: obj.AcctNo, EventId: $scope.eventId }).success(function (data) {
                    $scope.FraudCards = [];
                    $scope._Selects.CardNo = data.cardList.CardNo;

                    $scope.CardDetailsSales = null;
                    $scope.CardMonth1Date = null;
                    $scope.CardMonth2Date = null;
                    $scope.CardMonth3Date = null;
                    $scope.CardMonth4Date = null;
                    $scope.CardMonth5Date = null;
                    $scope.CardMonth6Date = null;
                    $scope.CardAvgSalesDisplay = null;
                    $scope.showCardTable = false;
                })
            })
        });

        $scope.CreateNewItem = function () {

            if ($scope.obj.acctNo) {

                var hasEmpty = false;
                angular.forEach($scope.FraudCards, function (value, index) {
                    if (!value.SelectedCardNo) {
                        hasEmpty = true;
                    }
                });
                if (!hasEmpty) {
                    $scope.entryDisabled = false;
                    //var len = $scope.FraudCards.length;

                    // if (len > 0) {
                    //  $scope.FraudCards[len - 1].isEdit = false;
                    // }

                    $scope.FraudCards.push({
                        CardNo: null,
                        AcctNo: $scope.obj.acctNo,
                        SelectedCardNo: null,
                        isEdit: true,
                        //   UnitNo: len + 1

                    });
                }
            }
        }

        $scope.removeItem = function (item) {
            var index = $scope.FraudCards.indexOf(item);
            $scope.FraudCards.splice(index, 1);

            if (item.AcctNo) {
                Api.GetCardDetailsList_ByAcctNoCardNo($scope.FraudCards, $scope._Object.FraudCardDetailsViewModel).success(function (data) {
                    if (data.aaData && data.aaData.length > 0 && data.model) {
                        $scope.showCardTable = true;
                        $scope.CardDetailsSales = data.aaData;
                        $scope.CardMonth1Date = data.model[0];
                        $scope.CardMonth2Date = data.model[1];
                        $scope.CardMonth3Date = data.model[2];
                        $scope.CardMonth4Date = data.model[3];
                        $scope.CardMonth5Date = data.model[4];
                        $scope.CardMonth6Date = data.model[5];
                        $scope.CardAvgSalesDisplay = data.model[6];
                    }
                    else {
                        $scope.showCardTable = false;
                        $scope.CardDetailsSales = null;
                        $scope.CardMonth1Date = null;
                        $scope.CardMonth2Date = null;
                        $scope.CardMonth3Date = null;
                        $scope.CardMonth4Date = null;
                        $scope.CardMonth5Date = null;
                        $scope.CardMonth6Date = null;
                        $scope.CardAvgSalesDisplay = null;
                    }
                })
            }
        }

        $scope.insertRecord = function (item) {
            if (item.SelectedCardNo) {

                if ($scope.isEdit) {
                    item.processing = true;
                    var obj = $scope.MCardDetails;
                    $.extend(item, {

                        CardNo: item.SelectedCardNo,
                        AcctNo: $scope.obj.acctNo
                    });
                    item.isEdit = false;
                    $scope.entryDisabled = false;
                }
                else {
                    item.isEdit = false;
                }

                if ($scope.obj.acctNo) {
                    if ($scope.FraudCards.length > 0)
                        $scope._Object.FraudCardDetailsViewModel.FraudCards = $scope.FraudCards;

                    Api.GetCardDetailsList_ByAcctNoCardNo($scope.FraudCards, $scope._Object.FraudCardDetailsViewModel).success(function (data) {
                        $scope.showCardTable = true;
                        $scope.CardDetailsSales = data.aaData;
                        $scope.CardMonth1Date = data.model[0];
                        $scope.CardMonth2Date = data.model[1];
                        $scope.CardMonth3Date = data.model[2];
                        $scope.CardMonth4Date = data.model[3];
                        $scope.CardMonth5Date = data.model[4];
                        $scope.CardMonth6Date = data.model[5];
                        $scope.CardAvgSalesDisplay = data.model[6];

                    })
                }
            }

        }

        $scope.SaveFraud = function () {
            Utils.InfoNotify();
            $scope.FilteredFraudCards = $scope.FraudCards;
            angular.forEach($scope.FraudCards, function (value, key) {
                if (value.isEdit)
                    $scope.FilteredFraudCards.splice(key, 1)
            });
            $scope._Object.FraudCardDetailsViewModel.FraudCards = $scope.FilteredFraudCards;
            Api.SaveFraud($scope._Object.FraudCustomerDetailsViewModel, $scope._Object.FraudCardDetailsViewModel, $scope._Object.FraudIncidentsViewModel)
                .success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    if (data.resultCd.flag == 0) {
                        if ($scope.isNew) {
                            $rootScope.obj.eventId = data._eventId;
                            $scope._Object.FraudCustomerDetailsViewModel.EventId = data._eventId;
                            $scope.isNew = false;
                            $rootScope.obj._type = 'edit';
                            $location.path('/events/' + data._eventId); //data.CorpCd
                            $scope.$apply();
                        }
                    }
                })
        }


        $scope.refresh = function () {
            $scope.tables[$scope.dtOptions.id].fnDraw();

        }

        $scope.NatureOfIncidentChanged = function (obj, val) {
            if (val != $scope.Enum_NatureOfIncident) {
                $scope._Object.FraudIncidentsViewModel.OtherNatureOfIncident = '';
            }
        }
    };
    var txndisputeController = function ($scope, $routeParams, $rootScope, $location, Api, Utils, $compile) {
        var isPostedTxn = true;
        $rootScope.obj._type = 'txn';
        $scope.modalOpen = {};
        console.log('txn dispute.........');
        $scope.modalOpen.PostTxn = false;
        $scope.aaData = [];

        var eventId = $routeParams.eventId || $rootScope.obj.eventId;
        var acctNo = $rootScope.obj.acctNo || $routeParams.acctNo;
        var cardNo = $rootScope.obj.cardNo || $routeParams.cardNo;

        $scope._Object = {};
        $scope._Object.FraudTxnDisputeViewModel = {};
        $scope._Selects = [];
        Api.GetFormData({ Prefix: 'txn' }).success(function (data) {
            $scope._Selects.FraudTxnDisputeViewModel = data.Selects;
        });

        $scope.dtOptionsPostedTxn = {
            serverSide: false,
            processing: true,
            "scrollX": true,
            id: 'tblPostedTxn',
            retrieve: true,
            "ordering": false,
            searching: false,
            ajax: ''
        };

        $scope.dtOptionsDisputeTxn = {
            serverSide: false,
            processing: true,
            "scrollX": true,
            id: 'tblDisputeTxn',
            retrieve: true,
            "ordering": false,
            searching: false,
            ajax: ''
        };

        $scope.SearchTxn = function () {
            var prefix = $rootScope.getRootUrl() + '/Fraud/GetFraudTxnSearch?';

            var obj = $scope._Object.FraudTxnDisputeViewModel;
            var queryString = $.param({
                SelectedCardNo: $rootScope.obj.cardNo, SelectedTxnCategory: obj.SelectedTxnCategory, EventId: $rootScope.obj.eventId,
                SelectedTxnCd: obj.SelectedTxnCd, FromDate: obj.FromDate, ToDate: obj.ToDate, AcctNo: $rootScope.obj.acctNo, IsPostedDispute: (isPostedTxn == true ? '1' : '2')
            });

            if (isPostedTxn) {
                var value = angular.extend($scope.dtOptionsPostedTxn, {
                    serverSide: true, ajax: prefix + queryString, destroy: true, checkBox: true,
                    "drawCallback": function (settings) {
                        var $table = $rootScope.tables[$scope.dtOptionsPostedTxn.id];
                        if ($table) {
                            var chkall = $table.closest('.dataTables_scroll').find('.dataTables_scrollHead').find('input');
                            chkall[0].checked = false;
                        }

                        $scope.modalOpen.PostTxn = false;
                        $scope.$apply();

                    }
                });
            }
            else {
                var value = angular.extend($scope.dtOptionsDisputeTxn, {
                    serverSide: true, ajax: prefix + queryString, destroy: true, checkBox: true,
                    "drawCallback": function (settings) {
                        var $table = $rootScope.tables[$scope.dtOptionsDisputeTxn.id];
                        if ($table) {
                            var chkall = $table.closest('.dataTables_scroll').find('.dataTables_scrollHead').find('input');
                            chkall[0].checked = false;
                        }

                        $scope.modalOpen.PostTxn = false;
                        $scope.$apply();

                    }
                });
            }
            $scope.$broadcast('updateDataTable', { options: value });
        }

        function resetTableValue() {

            Utils.makeObjectNull($scope._Object.FraudTxnDisputeViewModel, { AcctNo: acctNo, EventId: eventId, SelectedCardNo: cardNo });

            var $tablePostedTxn = $rootScope.tables[$scope.dtOptionsPostedTxn.id];
            var $tableDisputeTxn = $rootScope.tables[$scope.dtOptionsDisputeTxn.id];

            $tablePostedTxn.fnClearTable(0);

            if ($tablePostedTxn) {
                var chkall = $tablePostedTxn.closest('.dataTables_scroll').find('.dataTables_scrollHead').find('input');
                chkall[0].checked = false;
            }

            var value = angular.extend($scope.dtOptionsPostedTxn, {
                serverSide: false, ajax: '', "drawCallback": '', destroy: true, checkBox: false
            });
            $scope.$broadcast('updateDataTable', { options: value });
            var prefix = $rootScope.getRootUrl() + '/Fraud/GetFraudTxnSearch?';
            var obj = $scope._Object.FraudTxnDisputeViewModel;
            var queryString = $.param({
                SelectedCardNo: $rootScope.obj.cardNo, SelectedTxnCategory: obj.SelectedTxnCategory, EventId: $rootScope.obj.eventId,
                SelectedTxnCd: obj.SelectedTxnCd, FromDate: obj.FromDate, ToDate: obj.ToDate, AcctNo: $rootScope.obj.acctNo, IsPostedDispute: (isPostedTxn == true ? '1' : '2')
            });
            var value2 = angular.extend($scope.dtOptionsDisputeTxn, {
                serverSide: true, ajax: prefix + queryString, destroy: true, checkBox: true,
                "drawCallback": function (settings) {
                    var $table = $rootScope.tables[$scope.dtOptionsDisputeTxn.id];
                    if ($table) {
                        var chkall = $table.closest('.dataTables_scroll').find('.dataTables_scrollHead').find('input');
                        chkall[0].checked = false;
                    }

                    $scope.modalOpen.PostTxn = false;
                    $scope.$apply();

                }
            });
            $scope.$broadcast('updateDataTable', { options: value2 });


        }

        $scope.loadPostedTxn = function () {
            isPostedTxn = true;
            resetTableValue();
        }

        $scope.loadDisputedTxn = function () {

            isPostedTxn = false;
            resetTableValue();

        }

        $scope.modalClick = function () {
            $scope.modalOpen.PostTxn = true;
            // Utils.makeObjectNull($scope._Object, { RefKey: acctNo, RefTo: 'acct' });
        }
        $scope.SaveDisputeTxn = function () {
            var obj = Utils.getSelectedRows($rootScope.tables[$scope.dtOptionsDisputeTxn.id]);
            var liTxnId = _.map(obj, function (item) {
                return item[1];
            });

            if (liTxnId && liTxnId.length > 0) {
                Utils.InfoNotify();
                Api.SaveTxn({ liTxnId: liTxnId, AcctNo: acctNo, CardNo: cardNo, EventId: eventId, IsPostedDispute: (isPostedTxn == true ? '1' : '2') }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptionsDisputeTxn.id].fnDraw();
                });
            }
        }

        $scope.SavePostedTxn = function () {
            var obj = Utils.getSelectedRows($rootScope.tables[$scope.dtOptionsPostedTxn.id]);
            var liTxnId = _.map(obj, function (item) {
                return item[1];
            });

            if (liTxnId && liTxnId.length > 0) {
                Utils.InfoNotify();
                Api.SaveTxn({ liTxnId: liTxnId, AcctNo: acctNo, CardNo: cardNo, EventId: eventId, IsPostedDispute: (isPostedTxn == true ? '1' : '2') }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptionsPostedTxn.id].fnDraw();
                });
            }

        }
    };
    var printController = function ($scope, $routeParams, $rootScope, $location, Api, Utils) {
        $rootScope.obj._type = 'txn';
        var eventId = $routeParams.eventId || $rootScope.obj.eventId;
        $scope.applId = eventId;
        Api.GetFormData({ Prefix: 'details', EventId: eventId }).success(function (data) {
            var obj = $.extend(data._FraudCustomerDetailsViewModel, data._FraudCardDetailsViewModel);
            obj = $.extend(obj, data._FraudIncidentsViewModel);
            $scope._Object = obj;
            $scope._Object.todayDate = new Date();
            $scope._Object.FraudCards = data.liFraudCards;
            $scope._Object.SubsidyNo = data.model[11];

            if ($scope._Object.FraudCards) {

                var text = '';
                angular.forEach($scope._Object.FraudCards, function (value, key) {
                    text = text + value.SelectedCardNo + ' , ';
                });

                $scope._Object.FraudCardsInLine = text.slice(0, -2);

                if (data.cardaaData && data.cardaaData.length > 0) {

                    $scope.CardDetailsSales = data.cardaaData;
                    angular.forEach($scope._Object.FraudCards, function (value, key) {
                        // CardAvgSales = data.cardaaData[key][6]
                        $.extend(value, {
                            CardAvgSales: data.cardaaData[key][7],
                            LitLimit: data.cardaaData[key][8],
                            SingleTxn: data.cardaaData[key][9],
                            DailyTxn: data.cardaaData[key][10],
                            MonthlyTxn: data.cardaaData[key][11],
                            DailyCnt: data.cardaaData[key][12],
                            MonthlyCnt: data.cardaaData[key][13],
                            DailyLitre: data.cardaaData[key][14],
                            MonthlyLitre: data.cardaaData[key][15]
                        });

                    });
                }

            }

            if (data.model) {
                $scope._Object.CmpyName1 = data.model[0];
                $scope._Object.AccountType = data.model[1];
                $scope._Object.ClientType = data.model[2];
                $scope._Object.AgeingDays = data.model[3];
            }
            if (data.aaData && data.aaData[0]) {
                $scope._Object.AvgSales = data.aaData[0][6];
            }

        });
    };
    var fileManagerController = function ($scope, $rootScope, $routeParams, $location, Api, Utils, $http) {
        $scope.FolderPath = '2';
        $scope.eventId = $routeParams.eventId || $rootScope.eventId;

        $scope.uploadFiles = function () {
            if ($scope.dropZone.getQueuedFiles().length == 0) {
                return toastr.error("please select atleast one image for upload");
            }
            Utils.InfoNotify();
            $scope.dropZone.processQueue();
        }
        $http.get($rootScope.getRootUrl() + '/Applications/GetFiles?ApplId=' + $scope.eventId + '&FolderPath=' + $scope.FolderPath).success(function (data) {
            $scope.files = data.files;
        });


        $scope.deleteFileConfirmation = function (file) {
            $scope.markedFile = file;
        }

        $scope.deleteFile = function (file) {
            $scope.markedFile = file;
            var item = $scope.markedFile;
            $http.get($rootScope.getRootUrl() + '/Applications/RemoveFile?FileName=' + item.FileName + '&ApplId=' + $scope.eventId + '&FolderPath=' + $scope.FolderPath).success(function (data) {
                var index = $scope.files.indexOf(item);
                $scope.files.splice(index, 1);
            })

        }
    };

    //inject service
    indexController.$inject = injectParamsIndex;
    detailsController.$inject = injectParamsDetail;
    txndisputeController.$inject = injectParamsTxnDispute;
    printController.$inject = injectParamsDetail;
    fileManagerController.$inject = injectParamsFileManager;

    angular.module('FraudApp').controller('indexController', indexController);
    angular.module('FraudApp').controller('detailsController', detailsController);
    angular.module('FraudApp').controller('txndisputeController', txndisputeController);
    angular.module('FraudApp').controller('printController', printController);
    angular.module('FraudApp').controller('fileManagerController', fileManagerController);
}());
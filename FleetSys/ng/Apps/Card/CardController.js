(function () {
    var injectParamsOne = ['$scope', '$rootScope', '$location', '$timeout', 'CardApi'];
    var injectParamsTwo = ['$scope', '$routeParams', '$rootScope', 'CardApi', 'Utils'];
    var indexController = function ($scope, $rootScope, $location, $timeout, CardApi) {
        $rootScope.obj._type = 'index';
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: true,
            ajax: $rootScope.getRootUrl() + '/Account/ftCardHolderList?AcctNo=' + acctNo,
            id: 'tblCards',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };
        $scope.$on('indexSelected', function (event, obj) {
            location.href = $rootScope.getRootUrl() + '/Card#/' + acctNo + '/' + obj[0];
        });

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

    };
    //GeneralInfo Controller
    var generalInfoController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;

        CardApi.getFormData({ Prefix: 'gen', AcctNo: acctNo, CardNo: cardNo }, $rootScope.getRootUrl() + '/Card').success(function (data) {
            $scope._Object = data.Model;
            //$scope._Object.AcctNo = acctNo;//add
            //$scope._Object.CardNo = cardNo;//add
            $rootScope.obj.entityId = data.Model.EntityId;
            $scope._Selects = data.Selects;
        });
        $scope.postGeneralInfo = function () {
            Utils.InfoNotify();
            if (!cardNo) {
                angular.extend($scope._Object, { AccNo: acctNo });
            } else {
                angular.extend($scope._Object, { CardNo: cardNo, AcctNo: acctNo });
            }
            if ($rootScope.obj._type == 'edit') {
                CardApi.postGeneralInfo($scope._Object).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    if (data.resultCd.flag == 0) {
                        $rootScope.obj.applId = applId;
                        $rootScope.obj._type = 'edit';
                        $scope.isNew = false;
                        $scope._Object.appcId = data.AppcId;
                    }
                });
            } else {
                CardApi.postNewCard($scope._Object).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    if (data.resultCd.flag == 0) {
                        $rootScope.obj.applId = applId;
                        $rootScope.obj._type = 'edit';
                        $scope.isNew = false;
                        $scope._Object.appcId = data.AppcId;
                    }
                })
            }

        }

    };
    //financialInfo Controller
    var financialInfoController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;
        CardApi.getFormData({ Prefix: 'fin', CardNo: cardNo, AcctNo: acctNo }, $rootScope.getRootUrl() + '/Card').success(function (data) {
            $scope._Object = data.Model;
            $scope._Object.CardNo = cardNo;
            $scope._Selects = data.Selects;
        });

        $scope.SaveWebPinReset = function () {
            Utils.InfoNotify();
            CardApi.SaveWebPinReset($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            });
        }

        $scope.SaveFinancialInfo = function () {
            Utils.InfoNotify();
            CardApi.SaveFinancialInfo($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            });
        }
    };
    //personInfo Controller
    var personInfoController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        $scope._Object = null;
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;
        var entityId = $routeParams.entityId ? $routeParams.entityId : $rootScope.obj.entityId;

        CardApi.getFormData({ Prefix: 'per', AcctNo: acctNo, CardNo: cardNo, EntityId: entityId }, $rootScope.getRootUrl() + '/Card').success(function (data) { // /Card
            $scope._Object = data.Model;
            $rootScope.obj.entityId = data.Model.EntityId;
            //$scope._Object.EntityId = entityId;
            $scope._Selects = data.Selects;
        });
        $scope.savePersonInfo = function () {
            $scope._Object._entityID = entityId;
            Utils.InfoNotify();
            CardApi.savePersonInfo($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
            });
        }
    }
    //velocity Controller
    var velocityController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;
        $scope.modalOpen = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblVelocity',
            ajax: $rootScope.getRootUrl() + '/Applications/ftVelocityList?CardNo=' + cardNo,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            aoColumnDefs: [
            { "bVisible": false, "aTargets": [0] },
            ]

        };


        CardApi.getFormData({ Prefix: 'vel' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefKey = cardNo;
            $scope._Object.CardNo = cardNo;
            $scope._Selects = data.Selects;
        });
        $scope.newVelocity = function () {
            $scope.modalOpen = true;
            $scope._Object._Func = "Add";
            Utils.makeObjectNull($scope._Object, { CardNo: cardNo });//add
        }
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            CardApi.GetVelocityLimit({ CardNo: cardNo, AccNo: acctNo, SelectedVelocityInd: obj[11], SelectedProdCd: obj[12] }).success(function (data) {
                $scope._Object = data.velocity;
                $scope._Object._Func = "Upd";
                $scope._Object.RefKey = cardNo;
                $scope._Object.CardNo = cardNo;
            });
        });

        $scope.SaveVelocityLimit = function () {
            Utils.InfoNotify();
            $scope._Object.AccNo = acctNo;
            CardApi.SaveVelocityLimit($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }

        $scope.delete = function () {
            //delete velocity
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                CardApi.RemoveVelocity({ CardNo: cardNo, VelInd: obj[0] }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    }
    //contact Controller
    var contactsController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;
        $scope.modalOpen = false;
        $scope.isUpdate = false;

        CardApi.getFormData({ Prefix: 'con' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope._Object.RefTo = "CARD";
            $scope._Object.RefKey = cardNo;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            "scrollX": true,
            id: 'tblContacts',
            ajax: $rootScope.getRootUrl() + '/Applications/ftContactList?RefTo=card&RefKey=' + cardNo,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.isUpdate = false;
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: cardNo, RefTo: 'card', Func: "Add" });
        }

        $scope.SaveContact = function () {
            Utils.InfoNotify();
            CardApi.SaveContact($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }


        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            $scope.isUpdate = true;
            CardApi.ftContactDetail({ RefCd: obj[0], RefKey: cardNo, RefTo: 'card' }).success(function (data) {
                $scope._Object = data.contact;
                $scope._Object.RefKey = cardNo;
                $scope._Object.RefTo = 'card';
                $scope._Object.RefCd = obj[0];
                $scope._Object.Func = "Upd";
            });
        })

        $scope.deleteRecord = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                CardApi.RemoveContact({ RefCd: obj[0], RefKey: cardNo, RefTo: 'card' }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    }
    //address Controller
    var addressController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;
        $scope.modalOpen = false;
        CardApi.getFormData({ Prefix: 'add' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "card";
            $scope._Object.RefKey = cardNo;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblAddress',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            aoColumnDefs: [
          { "sWidth": '200px', "aTargets": [3, 4, 5, 6, 7] },
            ],
            ajax: $rootScope.getRootUrl() + '/Applications/ftAddressList?RefKey=' + cardNo + '&RefTo=card'
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { RefKey: cardNo, RefTo: 'card', Func: 'Add' });
        }

        $scope.Save = function () {
            Utils.InfoNotify();
            CardApi.SaveAddress($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                $rootScope.tables[$scope.dtOptions.id].fnDraw();
                if (data.resultCd.flag != 1) {
                    $scope.modalOpen = false;
                }
            });
        }

        $scope.Remove = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                CardApi.DelAddress({ RefCd: obj[0], RefKey: cardNo, RefTo: 'card' }).success(function (data) {
                    Utils.finalResultNotify(data.result);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }

        $scope.CountryChanged = function (item, value) {
            $scope.updateState(value);
        }
        $scope.updateState = function (value) {
            CardApi.WebGetState({ CountryCd: value }).success(function (item) {
                $scope._Selects.State = item;
            });
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            CardApi.ftAddressDetail({ RefCd: obj[0], RefKey: cardNo, RefTo: 'card' }).success(function (data) {
                $scope._Object = data.address;
                angular.extend($scope._Object, { RefKey: cardNo, RefTo: 'card', RefCd: obj[0], Func: 'Upd' });
                $scope.updateState(data.address.SelectedCountry);
            });
        });


    }
    //statusMaint Controller
    var statusMaintController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;
        $scope.modalOpen = false;
        CardApi.getFormData({ Prefix: 'sts', CardNo: cardNo }, $rootScope.getRootUrl() + '/Card').success(function (data) { //add /Card
            $scope._Object = data.Model;
            $scope._Object.CardNo = cardNo;
            $scope._Selects = data.Selects;
        });
        $scope.Save = function () {
            Utils.InfoNotify();
            CardApi.SaveChangedStatus($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag == 0) {
                    $scope._Object.SelectedCurrentStatus = $scope._Object.SelectedChangeStatusTo;
                }
            });
        }
    }
    // CardReplacement controller
    var cardReplacementController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;
        $scope.modalOpen = false;
        CardApi.getFormData({ Prefix: 'rep', CardNo: cardNo }, $rootScope.getRootUrl() + '/Card').success(function (data) {//add /Card
            $scope._Object = data.Model;
            $scope._Object.CardNo = cardNo;
            $scope._Selects = data.Selects;
        });
        $scope.Save = function () {
            Utils.InfoNotify();
            CardApi.SaveCardReplacement($scope._Object).success(function (data) {
                if (data.resultCd.flag == 0) {
                    $scope._Object.NewCardNo = data.NewCard;
                }
                Utils.finalResultNotify(data.resultCd);
            });
        }
    }
    // locationAcceptance controller
    var locationAcceptanceController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        var acctNo = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;
        $scope.modalOpen = false;
        $scope.deleteModalOpen = false;
        $scope.dtOptions = {
            serverSide: true,
            checkBox: true,
            processing: true,
            checkBox: true,
            scrollX: true,
            ordering: false,
            id: 'tblLocationAcceptance',
            ajax: $rootScope.getRootUrl() + '/Account/ftLocationList?AcctNo=' + acctNo + '&CardNo=' + cardNo,
            edit: {
                level: 'scope',
                func: 'indexSelected',
            }
        };

        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { AcctNo: acctNo });
        };
        $scope.RemoveSelectedState = function (state, obj) {
            //item.Value
            var repeats = _.filter($scope._Object.SelectedBusnLocations, function (item) {
                return item.indexOf(state.Value) != -1;
            });
            var difference = _.difference($scope._Object.SelectedBusnLocations, repeats);

            $scope._Object.SelectedBusnLocations = difference;

            _.each($scope._Object.SelectedBusnLocations, function (item) {
                if (item) {
                    if (item.indexOf(state.Value) != -1) {
                        $scope._Object.SelectedBusnLocations.splice($scope._Object.SelectedBusnLocations.indexOf(item), 1);
                    }
                }
            });

            var x = $scope._Object.SelectedBusnLocations;

        }
        $scope.StateSelected = function (item, model) {
            var elements = model.split(/,/g);
            //elements.push($scope._Object.SelectedStates);
            var element = elements[elements.length - 1];

            CardApi.getBusnLocations({ States: element, AcctNo: acctNo, CardNo: cardNo }).success(function (data) {
                var temp = $scope._Object.SelectedBusnLocations;
                $scope._Object.SelectedBusnLocations = null;
                var values = _.map(data, function (dealer) {
                    return dealer.Value;
                });
                if (!temp) {
                    temp = [];
                    temp = values;
                } else {
                    _.each(values, function (str) {
                        temp.push(str);
                    });
                    // $scope._Object.SelectedBusnLocations.concat(values);
                }
                $scope._Object.SelectedBusnLocations = temp;
                $scope._Selects.BusnLocations = data;
            });
            //Api.getBusnLocations({ States: elements.join(',') }).success(function (data) {
            //    $scope._Selects.BusnLocations = data;
            //});
        }


        CardApi.getFormData({ Prefix: 'loc', AcctNo: acctNo, CardNo: cardNo }, $rootScope.getRootUrl() + '/Account').success(function (data) {
            //append all selects here...
            data.Model.SelectedStates = [];
            $scope._Object = data.Model;
            $scope._Object.Func = true;
            $scope._Selects = data.Selects;
        });
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.Save = function () {
            Utils.InfoNotify();
            $scope._Object.AccNo = acctNo;
            $scope._Object.CardNo = cardNo;
            CardApi.SaveLocationAccept($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                if (data.resultCd.flag != 1) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            })
        }


        $scope.deleteRecord = function () {
            //delete velocity
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRows($rootScope.tables[$scope.dtOptions.id]);
            var selectedStates = _.map(obj, function (item) {
                return item = item[1];
            });
            console.log(selectedStates);
            if (selectedStates) {
                Utils.InfoNotify();
                CardApi.DelLocation({ AcctNo: $scope.acctNo, BusnLoc: selectedStates, CardNo: cardNo }).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
        //$scope.deleteRecord = function () {
        //    //delete velocity
        //    $scope.deleteModalOpen = false;
        //    var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
        //    if (obj) {
        //        Utils.InfoNotify();
        //        Api.DelLocation({ AcctNo: acctNo, CardNo: cardNo, TxnId: obj[7] }).success(function (data) {
        //            Utils.finalResultNotify(data.resultCd);
        //            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        //        });
        //    }

        //}
    }
    // productDiscount controller
    var productDiscountController = function ($scope, $routeParams, $rootScope, CardApi, Utils) {
        $scope.modalOpen = false;
        $scope.searchModalOpen = false;
        $scope.deleteModalOpen = false;
        var acctNum = $routeParams.acctNo ? $routeParams.acctNo : $rootScope.obj.acctNo;
        var cardNo = $routeParams.cardNo ? $routeParams.cardNo : $rootScope.obj.cardNo;
        CardApi.getFormData({ Prefix: 'pdc', AcctNo: cardNo }).success(function (data) {//, '/Account'
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Object.RefTo = "CARD";
            $scope._Object.RefKey = cardNo;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            scrollX: false,
            id: 'tblProductDiscount',
            edit: {
                level: 'scope',
                func: 'indexSelected',
            },
            ajax: $rootScope.getRootUrl() + '/Account/WebProductDiscountListSelect?RefTo=CARD&AcctNo=' + cardNo,

            rowCallback: function (aData, nRow) {
                $scope.searchModalOpen = false;
            }
        };

        $scope.SubsidyChanged = function (obj, val) {
            //
            if (val) {
                CardApi.GetPlanIdFromSubsidy({ SelectedProdDiscType: val }).success(function (data) {
                    $scope._Selects.PlanId = data.PlanId;
                })
            }
        }

        $scope.searchProductDiscount = function () {
            obj = $scope._Object;
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var prefix = $rootScope.getRootUrl() + '/Account/WebProductDiscountListSelect?';
            var queryString = $.param({
                DiscType: obj.SelectedProdDiscType, AcctNo: cardNo, RefTo: "CARD"
            });
            var value = angular.extend($scope.dtOptions, { serverSide: true, ajax: prefix + queryString, destroy: true });
            $scope.$broadcast('updateDataTable', { options: value });
        };

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.modalClick = function () {
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, { Func: "N", AcctNo: cardNo, RefTo: "CARD" });
        }

        $scope.Save = function () {
            Utils.InfoNotify();
            CardApi.ProductDiscountMaint($scope._Object).success(function (data) {
                //$scope._Object.RefKey = cardNo;
                Utils.finalResultNotify(data.result);
                if (data.result.flag == 0) {
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                    $scope.modalOpen = false;
                }
            });
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.modalOpen = true;
            CardApi.ProductDiscountSelect({ DiscType: obj[2], AcctNo: cardNo, Id: obj[10], RefTo: 'CARD' }).success(function (data) {
                $scope._Object = data.Discount;
                $scope._Object.Func = "E";
                $scope._Object.AcctNo = cardNo;
                $scope._Object.RefTo = 'CARD';
            });
        })
        $scope.Remove = function () {
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                CardApi.DeleteProductDiscount({ SelectedProdCd: obj[0], SelectedProdDiscType: obj[2], EffDateFrom: obj[5], RefKey: cardNo, CardNo: cardNo, RefTo: "CARD" }).success(function (data) { //'acct'
                    Utils.finalResultNotify(data.result);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
    }
    //inject service
    indexController.$inject = injectParamsOne;
    generalInfoController.$inject = injectParamsTwo;
    financialInfoController.$inject = injectParamsTwo;
    personInfoController.$inject = injectParamsTwo;
    velocityController.$inject = injectParamsTwo;
    contactsController.$inject = injectParamsTwo;
    addressController.$inject = injectParamsTwo;
    statusMaintController.$inject = injectParamsTwo;
    cardReplacementController.$inject = injectParamsTwo;
    locationAcceptanceController.$inject = injectParamsTwo;
    productDiscountController.$inject = injectParamsTwo;

    angular.module('CardtrendApp').controller('indexController', indexController);
    angular.module('CardtrendApp').controller('generalInfoController', generalInfoController);
    angular.module('CardtrendApp').controller('financialInfoController', financialInfoController);
    angular.module('CardtrendApp').controller('personInfoController', personInfoController);
    angular.module('CardtrendApp').controller('velocityController', velocityController);
    angular.module('CardtrendApp').controller('contactsController', contactsController);
    angular.module('CardtrendApp').controller('addressController', addressController);
    angular.module('CardtrendApp').controller('statusMaintController', statusMaintController);
    angular.module('CardtrendApp').controller('cardReplacementController', cardReplacementController);
    angular.module('CardtrendApp').controller('locationAcceptanceController', locationAcceptanceController);
    angular.module('CardtrendApp').controller('productDiscountController', productDiscountController);
}());
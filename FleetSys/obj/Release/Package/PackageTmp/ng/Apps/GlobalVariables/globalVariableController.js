(function () {
    var injectParam1 = ['$scope', 'Api', '$timeout', '$routeParams', '$rootScope', 'Utils'];
    var injectParam2 = ['$scope', 'Api', '$timeout', '$location', '$routeParams', '$rootScope', 'Utils'];
    var injectParam3 = ['$scope', '$route', 'Api', '$timeout', '$routeParams','$rootScope','Utils'];

    var globalParamsController = function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
        $scope.modalOpen = false;
        $scope.editMode = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            destroy: true,
            scrollX: true,
            id: 'tblMain',
            ajax: $rootScope.getRootUrl() + '/GlobalVariables/WebRefListSelect?refType=Language',
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };

        Api.getFormData({ Code: 'lp' }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            $scope.buttonPermissions = data.buttonPermissions;
            _.each($rootScope.variables, function (item) {
            });
        });
        $scope.modalClick = function () {
            var selectedObj = $rootScope.selectedObj;
            if (selectedObj.Key == "State") {

            }
            $scope.editMode = false;
            $scope.modalOpen = true;
            Utils.makeObjectNull($scope._Object, {});
        };
        $rootScope.$on("indexSelected", function (event, aData) {
            $scope.editMode = true;
            var selectedObj = $rootScope.selectedObj;
            var obj = {};
            obj.refType = selectedObj.Key;
            if (selectedObj.Key == "State") {
                obj.RefNo = aData[3];
                obj.RefCd = aData[1];
            } else if (selectedObj.Key == "City") {
                obj.RefCd = aData[2];
            }
            else {
                obj.RefCd = aData[0];
            }

            Api.WebRefSelect(obj).success(function (data) {
                console.log(data);
                $scope.modalOpen = true;
                angular.extend($scope._Object, data);
                if (selectedObj.Key == "City") {
                    $scope.CountryChanged($scope._Object.Country, $scope._Object.Country);
                }
            });
            console.log(aData);
        });
        $scope.$on('menuSelected', function (data, obj) {
            var x = {};

            x.ajax = $rootScope.getRootUrl() + '/GlobalVariables/WebRefListSelect?refType=' + obj.Key;
            x.aoColumnDefs = [
                { "bVisible": obj.Key != "State", "aTargets": [3] }
            ];
            $timeout(function myfunction() {
                var value = angular.extend($scope.dtOptions, x);
            });

        });
        $scope.CountryChanged = function (item, value) {
            Api.GetStates({ CountryCd: value }).success(function (item) {
                $scope._Selects.States = item;
            });
        }
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.IsValidLookupParameter = function (type, obj) {
            var isToSave = true;
            if (type.toLowerCase() == 'city') {
                if (obj.Country == null || obj.Country == '') {
                    Utils.PNotify({ flag: 1, Descp: "Country is required." });
                    return isToSave = false;
                } else if (obj.StateCode == null || obj.StateCode == '') {
                    Utils.PNotify({ flag: 1, Descp: "State Code is required." });
                    return isToSave = false;
                } else if (obj.CityCode == null || obj.CityCode == '') {
                    Utils.PNotify({ flag: 1, Descp: "City code is a mandatory field." });
                    return isToSave = false;
                } else if (obj.CityName == null || obj.CityName == '') {
                    Utils.PNotify({ flag: 1, Descp: "City Name is a mandatory field." });
                    return isToSave = false;
                }

            } else if (type.toLowerCase() == 'state') {
                if (obj.Country == null || obj.Country == '') {
                    Utils.PNotify({ flag: 1, Descp: "Country is required." });
                    return isToSave = false;
                } else if (obj.StateCode == null || obj.StateCode == '') {
                    Utils.PNotify({ flag: 1, Descp: "State Code is a mandatory field." });
                    return isToSave = false;
                }
                else if (obj.StateName == null || obj.StateName == '') {
                    Utils.PNotify({ flag: 1, Descp: "State Name is a mandatory field." });
                    return isToSave = false;
                }
            } else {
                if (obj.ParameterCode == null || obj.ParameterCode == '') {
                    Utils.PNotify({ flag: 1, Descp: "Parameter code is a mandatory field." });
                    return isToSave = false;
                }
                else if (obj.ParameterDescp == null || obj.ParameterDescp == '') {
                    Utils.PNotify({ flag: 1, Descp: "Parameter description is a mandatory field." });
                    return isToSave = false;
                }
            }
            return isToSave = true;
        }
        $scope.saveChanges = function () {
            var type = $rootScope.selectedObj.Key;
            var obj = { type: type };

            angular.extend($scope._Object, { type: type, flag: $scope.editMode ? 'U' : 'C' });
            if ($scope.IsValidLookupParameter(type, $scope._Object)) {
                Utils.InfoNotify();
                Api.WebRefMaint($scope._Object).success(function (data) {
                    Utils.finalResultNotify(data);
                    if (data.flag == 0) {
                        $rootScope.tables[$scope.dtOptions.id].fnDraw();
                        $scope.modalOpen = false;
                    }
                });
            }
        }

    };
    var productGroupController = function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
        $scope.detailModalOpen = false;
        $scope.groupModalOpen = false;
        $scope.deleteModalOpen = false;
        $scope.selectedProdGroup = null;
        $scope.isEditMode = false;
        $scope.flag = null;
        $scope.Items = [];
        $scope.ProductGroupItems = [];
        $scope.selectedProdName = null;
        $scope.selectedItem = null;
        Api.getFormData({ Code: 'pg' }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });

        $scope.prodGroupItems = [];
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            destroy: true,
            scrollX: true,
            pageLength: 3000,
            id: 'tblProductGroup',
            ajax: $rootScope.getRootUrl() + '/GlobalVariables/WebProdGroupRefListSelect',
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
                        return data[1] == nRows[1];//txnid
                    });
                    $scope.selectedProdName = nRows[2];
                    var header = "<thead><tr><th>Product Code</th><th>Product Name</th><th>Product Category</th><th>Product Type</th></tr></thead>";
                    var rows2 = _.map(rows, function (Next) {
                        return '<tr ng-click="detailModalOpen=true">' +
                              '<td style=\'background-color:#F8F9FA\'>' + Next[5] + '</td>' +
                              '<td style=\'background-color:#F8F9FA\'>' + Next[6] + '</td>' +
                              '<td style=\'background-color:#F8F9FA\'>' + Next[7] + '</td>' +
                              '<td style=\'background-color:#F8F9FA\'>' + Next[8] + '</td>' +
                              '</tr>'
                    });
                    var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                    $scope.selectedProdGroup = nRows[1];
                    return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr>';
                },
                sum: function sum(numbers) {
                    return _.reduce(numbers, function (result, current) {
                        return result + parseFloat(current[9]);
                    }, 0);
                },
                edit: {
                    level: 'format',
                    func: function (cells) {
                        $scope.detailModalOpen = true;
                        $scope.$apply();
                        Api.WebProdRefSelect({ ProdCd: cells[0] }).success(function (data) {
                            $scope.Items = [];
                            _.each(data, function (item) {
                                $scope.Items.push({
                                    UnitPrice: item.UnitPrice, EffectiveFrom: item.EffectiveFrom, ExpiryDate: item.ExpiryDate,
                                    LastUpdated: item.LastUpdated, UserId: item.UserId, isEdit: false, ProdId: item.ProdId
                                });
                            });
                            $scope._Object = data[0];
                            $scope._Object.ProductName = $scope.selectedProdName;
                            $scope._Object.flag = 'U';
                            $scope.isEditMode = true;
                        });
                    }
                },
                fngroupOp: function (e, settings, json) {
                    //json.aaData = [];
                    $scope.$apply(function () {
                        $scope.date = new Date();
                        $scope.aaData = json.aaData;
                    });
                    var rows = [];
                    _.each(json.aaData, function (data) {
                        if (rows.length) {
                            var contains = _.find(rows, function (item) {
                                return item[1] == data[1];
                            });
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

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.groupModalOpen = true;
            $scope.isEditMode = false;
            $scope.Items = [];
            $scope.ProductGroupItems = [];
            $scope._Object = {};

            Api.WebProdGroupRefSelect({ ProdGroup: obj[1] }).success(function (data) {
                $scope._Object = data[0];
                _.each(data, function (item) {
                    $scope.ProductGroupItems.push({
                        ProductCode: item.ProductCode, ProductName: item.ProductName, SelectedProductCategory: item.SelectedProductCategory,
                        SelectedProductType: item.SelectedProductType, UnitPrice: item.UnitPrice, isEdit: false
                    });
                });
                $scope.isEditMode = true;
            });
        });

        $scope.loadWebProdRefListSelect = function () {
            $scope.activeMode = 'LIST';
            alert();
            $scope.dtWebProdRefListSelect = {
                serverSide: true,
                processing: true,
                destroy: true,
                scrollX: true,
                pageLength: 3000,
                id: 'tblProductGroup',
                ajax: $rootScope.getRootUrl() + '/GlobalVariables/WebProdRefListSelect',
                edit: {
                    level: 'scope',
                    func: 'indexSelected'
                },
            };
        }

        $scope.newProductGroup = function () {
            $scope.groupModalOpen = true;
            $scope.isEditMode = false;
            $scope.Items = [];
            $scope.ProductGroupItems = [];
            Utils.makeObjectNull($scope._Object, {});
        }


        $scope.finishEdit = function (item) {
            item.isEdit = false;
        }

        $scope.CreateNewItem = function () {
            $scope.Items.push({
                UnitPrice: null, EffectiveFrom: null, ExpiryDate: null,
                LastUpdated: null, UserId: null, isEdit: true, ProdId: null
            });
        };

        $scope.newProdGroupItem = function () {
            $scope.ProductGroupItems.push({ ProductCode: null, ProductName: null, SelectedProductCategory: null, SelectedProductType: null, UnitPrice: null, isEdit: true });
        }

        $scope.removePrompt = function (item) {
            $scope.deleteModalOpen = true;
            $scope.selectedItem = item;
        }

        $scope.removeItem = function () {
            var item = $scope.selectedItem;
            $scope.ProductGroupItems.splice($scope.ProductGroupItems.indexOf(item), 1);
            $scope.deleteModalOpen = false;
        };
        $scope.saveProductList = function () {
            var obj = $scope._Object;
            obj.ProductItems = $scope.Items;
            Utils.InfoNotify();
            Api.WebProdRefMaint({ _Param: obj }).success(function (data) {
                Utils.finalResultNotify(data);
            });
        }
        $scope.WebProdGroupRefMaint = function () {
            var isToSave = true;
            var params = $scope._Object;

            if (!$scope.ProductGroupItems.length) {
                isToSave = false;
                return Utils.PNotify({ flag: 1, Descp: "Atleast one item required to fill" });
            }

            var unsaved = _.find($scope.ProductGroupItems, function (item) {
                return item.isEdit;
            });
            if (unsaved) {
                isToSave = false;
                return Utils.PNotify({ flag: 1, Descp: "Please save all items on the list" });
            }

            var unfilled = _.find($scope.ProductGroupItems, function (item) {
                return item.ProductCode == null;
            });

            if (unfilled) {
                isToSave = false;
                return Utils.PNotify({ flag: 1, Descp: "Please make sure all product codes are filled" });
            }


            params.ProductItems = $scope.ProductGroupItems;
            Utils.InfoNotify();
            Api.WebProdGroupRefMaint({ param: params }).success(function (data) {
                Utils.finalResultNotify(data);
                if (data.flag == 0) {
                    $scope.groupModalOpen = false;
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                }
            });
        }


        $scope.WebProdRefSelect = function (obj, prodCd, item) {
            Api.WebProdRefSelect({ ProdCd: prodCd }).success(function (data) {
                item.ProductName = data[0].ProdDescp;
                item.SelectedProductCategory = data[0].SelectedProductCategory;
                var category = _.find($scope._Selects.ProductCategory, function (item) {
                    return item.Value == data[0].SelectedProductCategory;
                });
                item.SelectedProductCategory = category ? category.Text : data[0].SelectedProductCategory;
                item.SelectedProductType = data[0].SelectedProductType;
                item.UnitPrice = data[0].UnitPrice;
                item.isEdit = true;
            });
        }
    };
    var productListController = function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
        $scope.detailModalOpen = false;
        $scope.Items = [];
        $scope.isEditMode = false;
        $scope.flag = null;
        Api.getFormData({ Code: 'pg' }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });
        $scope.dtWebProdRefListSelect = {
            serverSide: true,
            processing: true,
            destroy: true,
            scrollX: true,
            pageLength: 3000,
            id: 'tblProductList',
            ajax: $rootScope.getRootUrl() + '/GlobalVariables/WebProdRefListSelect',
            edit: {
                level: 'scope',
                func: 'indexSelected'
            },
        };

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtWebProdRefListSelect.id].fnDraw();
        }


        $scope.newProduct = function () {
            $scope.detailModalOpen = true;
            $scope.isEditMode = false;
            $scope.flag = 'I';
            $scope.Items = [];
            Utils.makeObjectNull($scope._Object, {});
        }

        if ($routeParams.prdId) {
            $scope.detailModalOpen = true;
            $scope.flag = 'U';
            $scope.Items = [];
            Api.WebProdRefSelect({ ProdCd: $routeParams.prdId }).success(function (data) {
                _.each(data, function (item) {
                    $scope.Items.push({
                        UnitPrice: item.UnitPrice, EffectiveFrom: item.EffectiveFrom, ExpiryDate: item.ExpiryDate,
                        LastUpdated: item.LastUpdated, UserId: item.UserId, isEdit: false, ProdId: item.ProdId
                    });
                });
                $scope._Object = data[0];
                $scope.isEditMode = true;
            });
        }
        $scope.$on('indexSelected', function (event, obj) {
            $scope.detailModalOpen = true;
            $scope.flag = 'U';
            $scope.Items = [];
            Api.WebProdRefSelect({ ProdCd: obj[0] }).success(function (data) {
                // if (data[0].ProductItems.length > 0) {
                _.each(data, function (item) {
                    if (item.UnitPrice != '0.00')
                        $scope.Items.push({
                            UnitPrice: item.UnitPrice, EffectiveFrom: item.EffectiveFrom, ExpiryDate: item.ExpiryDate,
                            LastUpdated: item.LastUpdated, UserId: item.UserId, isEdit: false, ProdId: item.ProdId
                        });
                });
                // }

                $scope._Object = data[0];
                $scope.isEditMode = true;
            });
        });

        $scope.CreateNewItem = function () {
            $scope.Items.push({
                UnitPrice: null, EffectiveFrom: null, ExpiryDate: null,
                LastUpdated: null, UserId: null, isEdit: true, ProdId: null
            });
        };

        $scope.finishEdit = function (item) {
            item.isEdit = false;
        }

        $scope.saveProductList = function () {
            var isToSave = false;
            var obj = $scope._Object;
            obj.ProductItems = $scope.Items;
            obj.flag = $scope.flag;

            var unsaved = _.find(obj.ProductItems, function (item) {
                return item.isEdit;
            });
            if (unsaved) {
                isToSave = false;
                return Utils.PNotify({ flag: 1, Descp: "Please save all items on the list" });
            }

            var unfilled = _.find(obj.ProductItems, function (item) {
                return item.UnitPrice == null || item.EffectiveFrom == null || item.ExpiryDate == null;
            });

            Utils.InfoNotify();
            Api.WebProdRefMaint({ _Param: obj }).success(function (data) {
                Utils.finalResultNotify(data);
                if (data.flag == 0) {
                    $scope.detailModalOpen = false;
                    $rootScope.tables[$scope.dtWebProdRefListSelect.id].fnDraw();
                }
            });
        }
        $scope.removeItem = function (item) {
            $scope.Items.splice($scope.Items.indexOf(item), 1);
        };
    };
    var rebatePlanController = function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
        $scope.selectedItem = null;
        $scope.modalOpen = false;
        $scope.selectedProdGroup = null;
        $scope.deleteModalOpen = false;
        $scope.isEditMode = false;
        $scope.flag = null;
        $scope.Items = [];
        $scope.ProductGroupItems = [];
        Api.getFormData({ Code: 'rb' }).success(function (data) {
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });

        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            destroy: true,
            scrollX: true,
            pageLength: 3000,
            id: 'tblRebate',
            ajax: $rootScope.getRootUrl() + '/GlobalVariables/WebRebatePlanListSelect',
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
                        return data[1] == nRows[1];//txnid
                    });
                    var header = "<thead><tr><th>Tier</th><th>Tier Value</th><th>Basic Value</th><th>Billing Value</th><th>Update Date</th><th>User Id</th></tr></thead>";
                    var rows2 = _.map(rows, function (Next) {

                        var x = parseInt(rows.indexOf(Next));
                        return '<tr ng-click="detailModalOpen=true">' +
                              '<td style=\'background-color:#F8F9FA\'>' + (x + 1) + '</td>' +
                              '<td style=\'background-color:#F8F9FA\'>' + Next[8] + '</td>' +
                              '<td style=\'background-color:#F8F9FA\'>' + Next[9] + '</td>' +
                              '<td style=\'background-color:#F8F9FA\'>' + Next[10] + '</td>' +
                             '<td style=\'background-color:#F8F9FA\'>' + Next[11] + '</td>' +
                            '<td style=\'background-color:#F8F9FA\'>' + Next[12] + '</td>' +
                              '</tr>'
                    });
                    var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                    $scope.selectedProdGroup = nRows[1];
                    return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr>';
                },
                sum: function sum(numbers) {
                    return _.reduce(numbers, function (result, current) {
                        return result + parseFloat(current[9]);
                    }, 0);
                },
                edit: {
                    level: 'format',
                    func: function (cells) {
                        $scope.detailModalOpen = true;
                        $scope.Items = [];
                        $scope.$apply();
                        Api.WebRebatePlanSelect({ PlanId: cells[0] }).success(function (data) {
                            _.each(data, function (item) {
                                $scope.Items.push({
                                    UnitPrice: item.UnitPrice, EffectiveFrom: item.EffectiveFrom, ExpiryDate: item.ExpiryDate,
                                    LastUpdated: item.LastUpdated, UserId: item.UserId, isEdit: false, ProdId: item.ProdId
                                });
                            });
                            $scope._Object = data[0];
                            $scope._Object.flag = 'U';
                            $scope.isEditMode = true;
                        });
                    }
                },
                fngroupOp: function (e, settings, json) {
                    //json.aaData = [];
                    $scope.$apply(function () {
                        $scope.date = new Date();
                        $scope.aaData = json.aaData;
                    });
                    var rows = [];
                    _.each(json.aaData, function (data) {
                        if (rows.length) {
                            var contains = _.find(rows, function (item) {
                                return item[1] == data[1];
                            });
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
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.newPlan = function () {
            $scope.modalOpen = true;
            $scope.isEditMode = false;
            $scope._Object.flag = 'I';
            $scope.Items = [];
            Utils.makeObjectNull($scope._Object, {});
        }

        $rootScope.$on("indexSelected", function (event, aData) {
            $scope.editMode = true;
            Api.WebRebatePlanSelect({ PlanId: aData[1] }).success(function (data) {
                $scope.modalOpen = true;
                $scope.Items = [];
                $scope._Object = data[0];
                $scope._Object.flag = 'U';
                _.each(data, function (item) {
                    $scope.Items.push({
                        MinPurchaseAmt: item.MinPurchaseAmt, SubSeqPurchaseAmt: item.SubSeqPurchaseAmt, SubSeqBillingAmt: item.SubSeqBillingAmt,
                        LastUpdated: item.LastUpdated, isEdit: false
                    });
                });
                $scope.isEditMode = true;
            })
            console.log(aData);
        });
        $scope.CreateNewItem = function () {
            $scope.Items.push({
                MinPurchaseAmt: null, SubSeqPurchaseAmt: null, SubSeqBillingAmt: null,
                LastUpdated: null, isEdit: true
            });
        };

        $scope.finishEdit = function (item) {
            item.isEdit = false;
        }


        $scope.removePrompt = function (item) {
            $scope.deleteModalOpen = true;
            $scope.selectedItem = item;
        }

        $scope.removeItem = function () {
            var item = $scope.selectedItem;
            $scope.Items.splice($scope.Items.indexOf(item), 1);
            $scope.deleteModalOpen = false;
        };

        $scope.savePlan = function () {
            var obj = $scope._Object;
            obj.ProductItems = $scope.Items;
            var isToSave = true;
            var params = $scope._Object;
            if (!obj.ProductItems.length) {
                isToSave = false;
                return Utils.PNotify({ flag: 1, Descp: "Atleast one item required to fill" });
            }

            var unsaved = _.find(obj.ProductItems, function (item) {
                return item.isEdit;
            });
            if (unsaved) {
                isToSave = false;
                return Utils.PNotify({ flag: 1, Descp: "Please save all items on the list" });
            }

            var unfilled = _.find(obj.ProductItems, function (item) {
                return item.MinPurchaseAmt == null;
            });

            if (unfilled) {
                isToSave = false;
                return Utils.PNotify({ flag: 1, Descp: "Please make sure all product codes are filled" });
            }

            Utils.InfoNotify();
            Api.WebRebatePlanMaint({ Params: obj }).success(function (data) {
                Utils.finalResultNotify(data);
                if (data.flag == 0) {
                    $scope.modalOpen = false;
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                }
            });
        }
    };
    var eventTypeListController = function ($scope, Api, $timeout, $location, $routeParams, $rootScope, Utils) {
        $scope.Items = [];
        $scope.isItemEdit = false;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            destroy: true,
            scrollX: true,
            id: 'tblEventTypeList',
            ajax: $rootScope.getRootUrl() + '/GlobalVariables/WebEventTypeListSelect',
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
        $scope.detailModalOpen = false;
        $scope.$on('indexSelected', function (event, obj) {
            $location.path('/eventDetails/' + obj[0]);
            $scope.$apply();
        });
    };
    var eventDetailController = function ($scope, $route, Api, $timeout, $routeParams, $rootScope, Utils) {
        $scope.Items = [];
        var itemObjModel = null;
        var itemObjItem = [];
        var backupObj = {};
        var backupBitmapObj = {};
        $scope.getClass = function (item) {
            var index = $scope.Items.indexOf(item);
            var item = item;
        };
        var periodType = [{ Value: "c", Text: "Current Cycle" }, { Value: 's', Text: "Set checking period" }];
        $scope.mapper = Utils.getEventMap();
        Api.getFormData({ Code: 'ev', id: $routeParams.id }).success(function (data) {
            $scope._Object = data.Model[0];
            if ($scope._Object.NotifyInd == 1 || $scope._Object.NotifyInd == 3)
                $scope._Object.NotifyIndEmail = true;
            if ($scope._Object.NotifyInd == 2 || $scope._Object.NotifyInd == 3)
                $scope._Object.NotifyIndSms = true;
            $scope._Selects = data.Selects;
            $scope._Selects.PeriodType = periodType;
            if ($scope._Object.MaxOccur > 0) {
                $scope._Object.SelectedOccur = 'S';
            } else {
                $scope._Object.SelectedOccur = 'E';
            }
            $scope.bitmapControls = [];
            _.each(data.Model, function (item, index) {
                var obj = {
                    MinIntVal: item.MinIntVal, MaxIntVal: item.MaxIntVal, MinMoneyVal: item.MinMoneyVal,
                    MaxMoneyVal: item.MaxMoneyVal, MinDateVal: item.MinDateVal, MaxDateVal: item.MaxDateVal,
                    MinTimeVal: item.MinTimeVal, MaxTimeVal: item.MaxTimeVal, VarCharVal: item.VarCharVal,
                    PeriodType: item.PeriodType, PeriodInterval: item.PeriodInterval, BitmapAmt: item.BitmapAmt,
                    id: index, EvtPlanDetailId: item.EvtPlanDetailId
                };
                if (obj.PeriodInterval > 0) {
                    obj.SelectedOccur = 'S';
                } else {
                    obj.SelectedOccur = 'C';
                }

                if (obj.BitmapAmt > 0) {
                    var __bitmap = Utils.findBinarySequence(obj.BitmapAmt).sort();
                    var __ = [];
                    _.each(__bitmap, function (item, index) {

                        var selectedMapper = _.find($scope.mapper, function (_x) {
                            return _x.id == item;
                        });
                        if (selectedMapper) {
                            selectedMapper.key = index;
                            selectedMapper.val = obj[selectedMapper.type];
                            __.push(selectedMapper);
                        }
                    });
                    $scope.bitmapControls.push(__);
                    itemObjItem = $scope.bitmapControls[0];
                }
                $scope.Items.push(obj);
                itemObjModel = obj;
            });
            WebEventTypeTemplateSelect($routeParams.id)
        });
        WebEventTypeTemplateSelect = function (id) {
            Api.WebEventTypeTemplateSelect({ EventTypeId: id }).success(function (data) {
                $scope.templateDisplayer = data;
            })
        };


        $scope.selectedOccurChanged = function (newVal) {
            if (newVal == 'E') {
                $scope._Object.MaxOccur = "-1";
                $scope._Object.SelectedFrequency = "";
            } else {
                $scope._Object.MaxOccur = "";
            }
        }

        $scope.CreateNewItem = function (ctrl) {
            ctrl.currentTarget.blur();
            $scope.isItemEdit = false;
            var newObject = jQuery.extend({}, itemObjModel);
            Utils.makeObjectNull(newObject, { BitmapAmt: $scope.Items[$scope.Items.length - 1].BitmapAmt, id: $scope.Items.length });
            $scope.Items.push(newObject);
            $scope.bitmapControls.push(jQuery.extend({ key: $scope.bitmapControls.length - 1 }, $scope.bitmapControls[$scope.bitmapControls.length - 1]));
            $scope.selectedItem = $scope.Items.length - 1;
            $scope.detailModalOpen = true;
        };
        $scope.finishEdit = function () {
            $scope.detailModalOpen = false;
        }
        $scope.cancelEdit = function () {
            if ($scope.isItemEdit) {
                $scope.Items[$scope.selectedItem] = backupObj;
                $scope.bitmapControls[$scope.selectedItem] = backupBitmapObj;
            } else {
                $scope.Items.splice($scope.selectedItem, 1);
                $scope.bitmapControls.splice($scope.selectedItem, 1);
            }
            $scope.detailModalOpen = false;
        }
        $scope.toggleStatus = function (val) {
            $scope._Object.SelectedStatus = val;
        }
        $scope.editItem = function (item) {
            $scope.isItemEdit = true;
            var index = $scope.Items.indexOf(item);
            $scope.selectedItem = index;
            backupObj = $.extend({}, $scope.Items[index]);
            backupBitmapObj = $.extend({}, $scope.bitmapControls[index]);
            $scope.detailModalOpen = true;
        }

        $scope.intervalChanged = function (val) {
            if (val == 'C') {
                $scope.Items[$scope.selectedItem].PeriodInterval = 0;
                $scope.Items[$scope.selectedItem].PeriodType = "C";
            }
        }

        $scope.removeItem = function (item) {
            var index = $scope.Items.indexOf(item);
            $scope.itemtDeleteIndex = index;
            $scope.itemDeleteModalOpen = true;
        }

        $scope.confirmitemDelete = function () {
            $scope.Items.splice($scope.itemtDeleteIndex, 1);
            $scope.bitmapControls.splice($scope.itemtDeleteIndex, 1);
            $scope.itemDeleteModalOpen = false;
        }

        $scope.saveAll = function () {
            var obj = $scope._Object;
            obj.ProductItems = $scope.Items;
            var NtifiCount = 0;
            if (obj.NotifyIndEmail)
                NtifiCount = 1
            if (obj.NotifyIndSms)
                NtifiCount += 2;
            obj.NotifyInd = NtifiCount;
            Utils.InfoNotify();
            Api.WebEventTypeMaint(obj).success(function (data) {
                Utils.finalResultNotify(data);
                if (data.flag == 0) {
                    $route.reload();
                }
            })
        }
        $scope.containsBitmap = function (index, bitmap) {
            if (index === "undefined") {
                return false;
            } else {
                var __ = $scope.bitmapControls[index];
                var contains = _.find(__, function (item) {
                    return item.id == bitmap;
                });
                if (contains)
                    return true;
                return false;
            }

        }
    };

    //inject service
    globalParamsController.$inject = injectParam1;
    productGroupController.$inject = injectParam1;
    productListController.$inject = injectParam1;
    rebatePlanController.$inject = injectParam1;
    eventTypeListController.$inject = injectParam2;
    eventDetailController.$inject = injectParam3;

    angular.module('CardtrendApp').controller('globalParamsController', globalParamsController);
    angular.module('CardtrendApp').controller('productGroupController', productGroupController);
    angular.module('CardtrendApp').controller('productListController', productListController);
    angular.module('CardtrendApp').controller('rebatePlanController', rebatePlanController);
    angular.module('CardtrendApp').controller('eventTypeListController', eventTypeListController);
    angular.module('CardtrendApp').controller('eventDetailController', eventDetailController);
}());
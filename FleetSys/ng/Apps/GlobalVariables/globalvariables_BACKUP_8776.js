/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
/// <reference path="account.js" />
angular.module('globalVariablesApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams, $timeout) {
    $rootScope.tables = {};
    $rootScope.variables = [
    { Key: "Language", Text: "Language", Selected: true }, { Key: "State", Text: "State" }, { Key: "City", Text: "City" }, { Key: "IcType", Text: "ID Number Type" }, { Key: "Title", Text: "Title" }, { Key: "VehType", Text: "Vehicle Type" }
    , { Key: "SaleTerritory", Text: "Sale Territory" }, { Key: "AcctOfficeCd", Text: "Pukal AG Code" }, { Key: "ProdCategory", Text: "Product Category" }, { Key: "ProdType", Text: "Product Type" }
    ];
    $rootScope.selectedObj = $rootScope.variables[0];
    $rootScope.subMenuSelected = function (item) {

        $rootScope.variables.forEach(function (item) {
            item.Selected = false;
        });
        var index = $rootScope.variables.indexOf(item);
        $rootScope.variables[index].Selected = true;
        $rootScope.selectedObj = $rootScope.variables[index];
        $rootScope.$broadcast('menuSelected', item);
    }

    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });


})
     .factory('Api', function ($http, $rootScope) {
         var Api = $rootScope.getRootUrl() + '/GlobalVariables';
         var selectData = function () {
             return $http({
                 method: 'GET',
                 url: ApiRef
             })
         };
         getFormData = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/FillData?' + $.param(obj)
             })
         };
         getStates = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/GetStates?' + $.param(obj)
             })
         };

         WebRefSelect = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/WebRefSelect?' + $.param(obj)
             })
         };
         WebRefMaint = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'POST',
                 url: ApiRef + '/WebRefMaint',
                 data: obj
             })
         };
         WebProdGroupRefSelect = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/WebProdGroupRefSelect?' + $.param(obj)
             })
         };
<<<<<<< HEAD

         WebProdRefSelect = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/WebProdRefSelect?' + $.param(obj)
             })
         };
         WebProdRefMaint = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'POST',
                 url: ApiRef + '/WebProdRefMaint',
                 data: obj
             })
         };

=======

         WebProdRefSelect = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/WebProdRefSelect?' + $.param(obj)
             })
         };
         WebProdRefMaint = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'POST',
                 url: ApiRef + '/WebProdRefMaint',
                 data: obj
             })
         };

>>>>>>> 2c3dbb343f9c24652e6bf386112d7ea5bde7eb03
         WebProdGroupRefMaint = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'POST',
                 url: ApiRef + '/WebProdGroupRefMaint',
                 data: obj
             })
         };
         WebRebatePlanSelect = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'GET',
                 url: ApiRef + '/WebRebatePlanSelect?' + $.param(obj)
             })
         };
         WebRebatePlanMaint = function (obj, url) {
             var ApiRef = url || Api;
             return $http({
                 method: 'POST',
                 url: ApiRef + '/WebRebatePlanMaint',
                 data: obj
             })
         };
         return {
             selectData: selectData,
             getFormData: getFormData,
             GetStates: getStates,
             WebRefSelect: WebRefSelect,
             WebRefMaint: WebRefMaint,
             WebProdGroupRefSelect: WebProdGroupRefSelect,
             WebProdRefSelect: WebProdRefSelect,
             WebProdRefMaint: WebProdRefMaint,
             WebProdGroupRefMaint: WebProdGroupRefMaint,
             WebRebatePlanSelect: WebRebatePlanSelect,
             WebRebatePlanMaint: WebRebatePlanMaint
         };
     })
    .config(function ($routeProvider) {
        var rootUrl = $('#hdUrlPrefix').val();
        $routeProvider.when('/', {
            templateUrl: 'tmplMain.html',
            controller: 'globalParamsController',
            pageKey: 'hash'

        })
        $routeProvider.when('/productGroup', {
            templateUrl: 'tmplProductGroup.html',
            controller: 'productGroupController',
            pageKey: 'productGroup'
        })
        .when('/productList', {
            templateUrl: 'tmplProductList.html',
            controller: 'productListController',
            pageKey: 'productList'
        })
        .when('/rebatePlan', {
            templateUrl: 'tmplrebatePlan.html',
            controller: 'rebatePlanController',
            pageKey: 'rebatePlan'
        });
    })

.controller('globalParamsController', function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
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
        })
        console.log(aData);
    });
    $scope.$on('menuSelected', function (data, obj) {
        var x = {};
        x.ajax = $rootScope.getRootUrl() + '/GlobalVariables/WebRefListSelect?refType=' + obj.Key;
        x.aoColumnDefs = [
            { "bVisible": obj.Key != "State", "aTargets": [3] }
        ]
        $timeout(function myfunction() {

            var value = angular.extend($scope.dtOptions, x);
            //   $scope.$broadcast('updateDataTable', { options: value });
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
    $scope.saveChanges = function () {
        var type = $rootScope.selectedObj.Key;
        var obj = { type: type };
        angular.extend($scope._Object, { type: type, flag: $scope.editMode ? 'U' : 'C' });
        Utils.InfoNotify();
        Api.WebRefMaint($scope._Object).success(function (data) {
            Utils.finalResultNotify(data);
            if (data.flag == 0) {
                $rootScope.tables[$scope.dtOptions.id].fnDraw();
                $scope.modalOpen = false;
            }
        });
    }
})
.controller('productGroupController', function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
    $scope.detailModalOpen = false;
    $scope.groupModalOpen = false;
<<<<<<< HEAD
    $scope.deleteModalOpen = false;
=======
>>>>>>> 2c3dbb343f9c24652e6bf386112d7ea5bde7eb03
    $scope.selectedProdGroup = null;
    $scope.isEditMode = false;
    $scope.flag = null;
    $scope.Items = [];
    $scope.ProductGroupItems = [];
    $scope.selectedProdName = null;
<<<<<<< HEAD
    $scope.selectedItem = null;
=======
>>>>>>> 2c3dbb343f9c24652e6bf386112d7ea5bde7eb03
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

<<<<<<< HEAD
    $scope.removePrompt = function (item) {
        $scope.deleteModalOpen = true;
        $scope.selectedItem = item;
    }

    $scope.removeItem = function () {
        var item = $scope.selectedItem;
        $scope.ProductGroupItems.splice($scope.ProductGroupItems.indexOf(item), 1);
        $scope.deleteModalOpen = false;
=======
    $scope.removeItem = function (item) {
        $scope.ProductGroupItems.splice($scope.ProductGroupItems.indexOf(item), 1);
>>>>>>> 2c3dbb343f9c24652e6bf386112d7ea5bde7eb03
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
})
.controller('productListController', function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {
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

    $scope.$on('indexSelected', function (event, obj) {
        $scope.detailModalOpen = true;
        $scope.flag = 'U';
        $scope.Items = [];
        Api.WebProdRefSelect({ ProdCd: obj[0] }).success(function (data) {
            _.each(data, function (item) {
                $scope.Items.push({
                    UnitPrice: item.UnitPrice, EffectiveFrom: item.EffectiveFrom, ExpiryDate: item.ExpiryDate,
                    LastUpdated: item.LastUpdated, UserId: item.UserId, isEdit: false, ProdId: item.ProdId
                });
            });
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
            return item.UnitPrice == null || item.EffectiveFrom == null || item.ExpiryDate == null;
        });

        if (unfilled) {
            isToSave = false;
            return Utils.PNotify({ flag: 1, Descp: "Please make sure all product codes are filled" });
        }



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
})
.controller('rebatePlanController', function ($scope, Api, $timeout, $routeParams, $rootScope, Utils) {

<<<<<<< HEAD
    $scope.selectedItem = null;
    $scope.modalOpen = false;
    $scope.selectedProdGroup = null;
    $scope.deleteModalOpen = false;
=======

    $scope.modalOpen = false;
    $scope.selectedProdGroup = null;
>>>>>>> 2c3dbb343f9c24652e6bf386112d7ea5bde7eb03
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
        $scope.flag = 'I';
        $scope.Items = [];
        Utils.makeObjectNull($scope._Object, {});
    }

    $rootScope.$on("indexSelected", function (event, aData) {
        $scope.editMode = true;
        Api.WebRebatePlanSelect({ PlanId: aData[1] }).success(function (data) {
            $scope.modalOpen = true;
            $scope.Items = [];
            $scope._Object = data[0];
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
<<<<<<< HEAD


    $scope.removePrompt = function (item) {
        $scope.deleteModalOpen = true;
        $scope.selectedItem = item;
    }

    $scope.removeItem = function () {
        var item = $scope.selectedItem;
        $scope.Items.splice($scope.Items.indexOf(item), 1);
        $scope.deleteModalOpen = false;
    };




=======
    $scope.removeItem = function (item) {
        $scope.Items.splice($scope.Items.indexOf(item), 1);
    };

>>>>>>> 2c3dbb343f9c24652e6bf386112d7ea5bde7eb03
    $scope.savePlan = function () {
        var obj = $scope._Object;
        obj.ProductItems = $scope.Items;
        obj.flag = $scope.flag;


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
})
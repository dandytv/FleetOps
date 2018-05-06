(function () {
    var injectParams = ['$scope', 'Api', '$timeout','$location','$route','$routeParams','$rootScope', 'Utils'];
    var eventConfigListController = function ($scope, Api, $timeout, $location, $route,$routeParams,$rootScope, Utils) {
        $scope.Items = [];
        $scope.deleteModalOpen = false; //added
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            destroy: true,
            scrollX: true,
            id: 'tblEventConfig',
            ajax: $rootScope.getRootUrl() + '/EventConfiguration/WebNtfyEvtConfListSelect',
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
        $scope.$on('indexSelected', function (event, obj) {
            $location.path('/eventDetails/' + obj[0]);
            $scope.$apply();
        });

        $scope.WebNtfEvtConfDelete = function () {
            //delete velocity
            $scope.deleteModalOpen = false;
            var obj = Utils.getSelectedRow($rootScope.tables[$scope.dtOptions.id]);
            if (obj) {
                Utils.InfoNotify();
                Api.WebNtfEvtConfDelete({ ScheduleId: obj[0] }).success(function (data) {
                    Utils.finalResultNotify(data);
                    $rootScope.tables[$scope.dtOptions.id].fnDraw();
                });
            }
        }
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
    };
    var eventDetailController = function ($scope, Api, $timeout, $location, $route, $routeParams, $rootScope, Utils) {
        $scope.Items = [];
        var itemObjModel = null;
        var itemObjItem = [];
        var backupObj = {};
        var backupBitmapObj = {};
        $scope.Recipients = [];
        $scope.detailModalOpen = false;
        $scope.getClass = function (item) {
            var index = $scope.Items.indexOf(item);
            var item = item;
        };
        var periodType = [{ Value: "c", Text: "Current Cycle" }, { Value: 's', Text: "Set checking period" }];
        $scope.mapper = Utils.getEventMap();
        $scope.isNew = $routeParams.id ? false : true;

        Api.getFormData({ PlanId: $routeParams.id }).success(function (data) {
            $scope.callBack(data, true);
        });
        $scope.shortDescpChanged = function (item, obj) {
            Api.WebEventTypeSelect({ Code: "ev", id: obj }).success(function (data) {
                $scope.callBack(data, false);
            });
        }
        $scope.callBack = function (data, updateEventType) {
            $scope.Items = [];
            $scope._Object = data.Model[0];
            if (!updateEventType) {
                data.Selects.EventType = $scope._Selects.EventType;
            }
            $scope._Selects = data.Selects;
            $scope._Selects.PeriodType = periodType;

            if ($scope.isNew) {
                $scope._Object.type = data.Model[0].SelectedEventType;
            }

            if ($scope._Object.SelectedScope) {
                $scope._Object.SelectedOwner = $scope._Object.SelectedScope;
            }
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
                    var __bitmap = Utils.findBinarySequence(obj.BitmapAmt).sort();;
                    var __ = [];
                    _.each(__bitmap, function (item) {
                        var selectedMapper = _.find($scope.mapper, function (_x) {
                            return _x.id == item;
                        });
                        if (selectedMapper) {
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

            $scope.WebNtfyEventRcptListSelect({ PlanId: $routeParams.id });
        }

        $scope.selectedOccurChanged = function (newVal) {
            if (newVal == 'E') {
                $scope._Object.MaxOccur = "-1";
                $scope._Object.SelectedFrequency = '';
            } else {
                $scope._Object.MaxOccur = "";
            }
        }

        $scope.intervalChanged = function (val) {
            if (val == 'C') {
                $scope.Items[$scope.selectedItem].PeriodInterval = 0;
                $scope.Items[$scope.selectedItem].PeriodType = "C";
            }
        }
        $scope.WebNtfyEventRcptListSelect = function (obj) {
            Api.WebNtfyEventRcptListSelect(obj).success(function (data) {
                _.each(data, function (item) {
                    var obj = { ChannelInd: item.ChannelInd, ContactName: item.ContactName, LangInd: item.LangInd, ContactNo: item.ContactNo, Id: item.Id };
                    obj.NotifyIndEmail = false;
                    obj.NotifyIndSms = false;
                    if (item.ChannelInd == 1 || $scope._Object.ChannelInd == 3) {
                        obj.NotifyIndEmail = true;
                    }
                    if (item.ChannelInd == 2 || item.ChannelInd == 3) {
                        obj.NotifyIndSms = true;
                    }
                    $scope.Recipients.push(obj);
                })
            })
        }



        $scope.containsBitmap = function (index, bitmap) {
            var __ = $scope.bitmapControls[index];
            var contains = _.find(__, function (item) {
                return item.id == bitmap;
            });
            if (contains)
                return true;
            return false;
        };
        $scope.editItem = function (item) {
            $scope.isItemEdit = true;
            var index = $scope.Items.indexOf(item);
            $scope.selectedItem = index;
            backupObj = $.extend({}, $scope.Items[index]);
            backupBitmapObj = $.extend({}, $scope.bitmapControls[index]);
            $scope.detailModalOpen = true;
        }


        $scope.CreateNewItem = function (ctrl) {
            ctrl.currentTarget.blur();
            $scope.isItemEdit = false;
            var newObject = jQuery.extend({}, itemObjModel);
            Utils.makeObjectNull(newObject, { BitmapAmt: $scope.Items[$scope.Items.length - 1].BitmapAmt, id: $scope.Items.length });
            $scope.Items.push(newObject);
            $scope.bitmapControls.push($scope.bitmapControls[$scope.bitmapControls.length - 1]);
            $scope.selectedItem = $scope.Items.length - 1;
            $scope.detailModalOpen = true;


        };


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


        $scope.finishEdit = function () {
            $scope.detailModalOpen = false;
        };

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
        };

        $scope.updateCompanyName = function () {
            Api.WebGetRefCmpyName({ SelectedRefTo: $scope._Object.SelectedRefTo, RefKey: $scope._Object.RefKey }).success(function (data) {
                if (data)
                    $scope._Object.CompanyName = data.companyName;
            });
        }

        //////////////Belongs to Recipient tab////////////////
        $scope.CreateNewRecipient = function (ctrl) {
            ctrl.currentTarget.blur();
            var obj = { NotifyInd: null, ContactName: "", LangInd: null, ContactNo: "", NotifyIndEmail: false, NotifyIndSms: false, isEdit: true, isNew: true };
            $scope.Recipients.push(obj);
            $scope.selectedContact = $scope.Recipients.length - 1;
            $scope.contactModalOpen = true;

        }

        $scope.editRecipient = function (item) {
            item.ContactNoDuplicate = item.ContactNo;
            item.ContactNameDuplicate = item.ContactName;
            item.NotifyIndEmailDuplicate = item.NotifyIndEmail;
            item.NotifyIndSmsDuplicate = item.NotifyIndSms;
            $scope.selectedContact = $scope.Recipients.indexOf(item);
            $scope.contactModalOpen = true;
        }

        $scope.removeRecipient = function (item) {
            var index = $scope.Recipients.indexOf(item);
            $scope.recipientDeleteIndex = index;
            $scope.contactDeleteModalOpen = true;
        }

        $scope.confirmremoveRecipient = function () {
            if ($scope.Recipients[$scope.recipientDeleteIndex].isNew) {
                $scope.Recipients.splice($scope.recipientDeleteIndex, 1);
                $scope.contactDeleteModalOpen = false;
            } else {
                Utils.InfoNotify();
                Api.WebNtfEvtConfRcptDelete({ SchRcptId: $scope.Recipients[$scope.recipientDeleteIndex].Id }).success(function (data) {
                    Utils.finalResultNotify(data);
                    $scope.Recipients.splice($scope.recipientDeleteIndex, 1);
                    $scope.contactDeleteModalOpen = false;
                })
            }
        }

        $scope.switchMedium = function (index, type) {
            var item = $scope.Recipients[index];
            item.NotifyIndSms = false;
            item.NotifyIndEmail = false;
            if (type == "sms") {
                item.NotifyIndSms = true;
            }
            else if (type == "email") {
                item.NotifyIndEmail = true;
            }
        }

        $scope.cancelContact = function (item) {
            if (item.isNew) {
                var index = $scope.Recipients.indexOf(item);
                $scope.Recipients.splice(index, 1);
            } else {
                item.ContactNo = item.ContactNoDuplicate;
                item.ContactName = item.ContactNameDuplicate;
                item.NotifyIndEmail = item.NotifyIndEmailDuplicate;
                item.NotifyIndSms = item.NotifyIndSmsDuplicate;
            }
            $scope.contactModalOpen = false;
        }
        $scope.finishEditContact = function (item) {
            if (!item.ContactNo || !item.ContactName || !item.LangInd) {
                return Utils.PNotify({ flag: 1, Descp: "Please fill in all fields" });
            }
            item.isEdit = false;
            //  item.isNew = false;
            $scope.Recipients.indexOf(item).ContactNoDuplicate = null;
            $scope.contactModalOpen = false;
        }
        $scope.finishEdit = function () {
            $scope.detailModalOpen = false;
        }

        $scope.saveAll = function () {
            var obj = $scope._Object;
            obj._EventRcptList = $scope.Recipients;
            obj.ProductItems = $scope.Items;
            var NtifiCount = 0;

            if (!$scope.Recipients.length) {
                return Utils.PNotify({ flag: 1, Descp: "Atleast one recipient details must be filled." });
            }

            _.each(obj._EventRcptList, function (item) {
                if (item.NotifyIndSms)
                    item.ChannelInd = 2;
                else if (item.NotifyIndEmail)
                    item.ChannelInd = 1;
            });
            Utils.InfoNotify();
            Api.WebNtfyEventConfMaint(obj).success(function (data) {
                if (data.flag == 0) {
                    if ($scope.isNew) {
                        $location.path("/eventDetails/" + data.Id);
                    } else {
                        $route.reload();
                    }
                }
                Utils.finalResultNotify(data);
            });
        }

    };

    //inject service
    eventConfigListController.$inject = injectParams;
    eventDetailController.$inject = injectParams;

    angular.module('CardtrendApp').controller('eventConfigListController', eventConfigListController);
    angular.module('CardtrendApp').controller('eventDetailController', eventDetailController);
}());
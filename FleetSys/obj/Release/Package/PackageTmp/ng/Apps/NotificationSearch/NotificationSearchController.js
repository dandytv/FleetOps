(function () {
    var injectParams = ['$scope', 'Api', '$timeout', '$location', '$routeParams', '$rootScope', 'Utils'];
    var indexController = function ($scope, Api, $timeout, $location, $routeParams, $rootScope, Utils) {
        $scope.modalOpen = false;
        Api.getFormData({ Prefix: 'lis' }).success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
        });
        $scope.dtOptions = {
            serverSide: false,
            processing: true,
            scrollX: true,
            autoWidth: false,
            retrieve: true,
            //checkBox: true,
            id: 'tblIndex',
        };

        $scope.modalClick = function () {
            $scope.modalOpen = true;
        }

        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }

        $scope.WebNtfyEventSearch = function () {
            var obj = $scope._Object;
            var $table = $rootScope.tables[$scope.dtOptions.id];
            var prefix = $rootScope.getRootUrl() + '/NotificationSearch/WebNtfyEventSearch?';
            var queryString = $.param({
                SelectedRefTo: obj.SelectedRefTo, RefKey: obj.RefKey,
                SelectedEventType: obj.SelectedEventType, StartDate: obj.StartDate, EndDate: obj.EndDate, SeletedEventInd: obj.SeletedEventInd,
            });
            var value = angular.extend($scope.dtOptions, {
                serverSide: true,
                ajax: prefix + queryString,
                destroy: true,
                "drawCallback": function (settings) {
                    $scope.modalOpen = false;
                    $scope.$apply();
                },
                edit: {
                    level: 'scope',
                    func: 'indexSelected'
                },
                columnDefs: [{
                    targets: [1], "sClass": "text-cell-center-both", render: function (data) {
                        var x = "sd";
                        if (data.toLowerCase() == "alert") {
                            return "<i class=\"fa fa-exclamation-circle\" style=\"text-align: center;font-size: 18px;color: #63c2de;\"></i>";
                        } else {
                            return "<i class=\"fa fa-warning\" style=\"text-align: center;font-size: 18px;color: #d9534f;\"></i>";
                        }
                    }
                }]
            });
            $scope.$broadcast('updateDataTable', { options: value });
        }

        $scope.$on('indexSelected', function (event, obj) {
            $scope.$apply(function () {
                $location.path('/eventDetails/' + obj[0]);
            });
        });
    };
    var eventDetailController = function ($scope, Api, $timeout, $location, $routeParams, $rootScope, Utils) {
        var eventId = $routeParams.id;
        $scope.Items = [];
        var itemObjModel = null;
        var itemObjItem = [];
        var backupObj = {};
        var backupBitmapObj = {};
        $scope.Recipients = [];
        $scope.getClass = function (item) {
            var index = $scope.Items.indexOf(item);
            var item = item;
        };
        var periodType = [{ Value: "c", Text: "Current Cycle" }, { Value: 's', Text: "Set checking period" }];
        $scope.mapper = Utils.getEventMap();
        Api.getFormData({ Prefix: 'det', Id: eventId }).success(function (data) {
            $scope._Object = data.Model[0];
            $scope.WebNtfyEventRcptListSelect({ PlanId: $scope._Object.EventScheduleId });
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
                    id: index
                };

                if (obj.PeriodInterval > 0) {
                    obj.SelectedOccur = 'S';
                } else {
                    obj.SelectedOccur = 'C';
                }

                if (obj.BitmapAmt > 0) {
                    var __bitmap = Utils.findBinarySequence(obj.BitmapAmt);
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
        });

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
    };

    //inject service
    indexController.$inject = injectParams;
    eventDetailController.$inject = injectParams;

    angular.module('notificationSearchApp').controller('indexController', indexController);
    angular.module('notificationSearchApp').controller('eventDetailController', eventDetailController);
}());
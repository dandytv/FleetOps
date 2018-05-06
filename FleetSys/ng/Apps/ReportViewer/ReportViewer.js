/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
//angular.module('ApplicationsApp', ['ngRoute', 'App.Utils']).run(function (Utils, $rootScope, $location, $routeParams) {
angular.module('ReportViewerApp', ['App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope) {
    $rootScope.tables = {};
    $rootScope.obj = {};
})
 .factory('Api', function ($http, $rootScope) {
     var Api = $rootScope.getRootUrl() + '/Applications';
     var selectData = function () {
         return $http({
             method: 'GET',
             url: Api
         })
     };
     getFormData = function (obj) {
         return $http({
             method: 'GET',
             url:$rootScope.getRootUrl()+ '/ReportViewer/FillData'
         })
     };
     searchReport = function (obj) {
         return $http({
             method: 'GET',
             url:$rootScope.getRootUrl()+ '/ReportViewer/SearchReports?' + $.param(obj)
         })
     }

     return {
         selectData: selectData,   //  this fetch the partialviews and stuff
         getFormData: getFormData, // fill all the selects and radios,
         searchReport: searchReport

     }
 })
.controller('indexController', function ($scope, $rootScope, Api, Utils) {
    $scope.searchOn = false;
    Api.getFormData().success(function (data) {
        $scope._Object = data.Model;
        $scope._Selects = data.Selects;
    });

    $scope.searchReports = function () {
        $scope.RequestSent = true;
        if (!$scope._Object.SelectedRptType) {
            return Utils.PNotify({ type: 1, Descp: "Please fill in the report type" });
        }

        Api.searchReport($scope._Object).success(function (data) {
            $scope.searchOn = true;
            $scope.$broadcast('updateTable', data);
            $scope.RequestSent = false;
        })
    }
    $scope.exportReport = function (type) {
        if ($scope._Object.SelectedRptType) {
            var reportType = _.find($scope._Selects.RptType, function (item) {
                return item.Value == $scope._Object.SelectedRptType;
            });
            var param2 = {
                Extension: type,
                Title: reportType.Text
            };
            $.extend(param2, $scope._Object);
            location.href = (type == '.xlsx') ? $rootScope.getRootUrl()+'/ReportViewer/DownloadExcelReport?' + $.param(param2) : $rootScope.getRootUrl()+'/ReportViewer/downloadCSV?' + $.param(param2);
        }
    }
})
.directive('dynamicTable', function ($compile, $timeout, $rootScope) {

    return {
        restrict: 'A',
        link: function (scope, $element, attrs) {

            scope.$on('updateTable', function (e, value) {

                console.log(value);

                if ($rootScope.dTable) {
                    $rootScope.dTable.fnDestroy();
                }

                $element.find('thead').html('');
                $element.find('tbody').html("");

                var aaColumns = [];
                var aaRows = [];



                for (i = 0; i < value.Report.columns.length; i++) {
                    aaColumns.push({ title: value.Report.columns[i] });
                }

                if (value.Report.rows.length > 0) {
                    for (j = 0; j < value.Report.rows.length ; j++) {
                        aaRows.push(value.Report.rows[j]);
                    }
                }

                $rootScope.dTable = $element.dataTable({
                    "bServerSide": false,
                    columns: aaColumns,
                    data: aaRows,
                    "info": true,
                    "lengthChange": true,
                    "scrollX": true,
                    paging: true,
                    "searching": true,
                    searchable: true,
                    pageLength: 25,
                    "dom": 'C<"clear">lfrtip',
                    checkBox: false,
                    oLanguage: {
                        sEmptyTable: '<i style="font-size:140px;color:#eeeeee" class="fa fa-ban"></i>'
                    }
                });
            })


        }
    }
});
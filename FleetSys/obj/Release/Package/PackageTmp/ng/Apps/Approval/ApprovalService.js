(function () {
    var injectParams = ['$http', 'Utils'];
    var ApprovalFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/Applications';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: Api
            })
        };
        getFormData = function (obj) {
            var params = $.param(obj);
            return $http({
                method: 'GET',
                url: Api + '/FillData?' + params
            })
        };

        WebMilestoneHistorySelect = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/WebMilestoneHistorySelect?' + $.param(obj),
                method: 'GET',
            })
        }

        ftMilestoneInfo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/ftMilestoneInfo?' + $.param(obj),
                method: 'GET',
            })
        }

        GetTaskNo = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/GetTaskNo?' + $.param(obj),
                method: 'GET',
            })
        }

        SaveMilestone = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveMilestone',
                method: 'POST',
                data: obj
            });
        }

        SaveMilestoneAdj = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveMilestoneAdj',
                method: 'POST',
                data: obj
            });
        }
        SaveMilestonePayment = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/Applications/SaveMilestonePayment',
                method: 'POST',
                data: obj
            });
        }

        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            getFormData: getFormData, // fill all the selects and radios
            WebMilestoneHistorySelect: WebMilestoneHistorySelect,
            ftMilestoneInfo: ftMilestoneInfo,
            GetTaskNo: GetTaskNo,
            SaveMilestone: SaveMilestone,
            SaveMilestoneAdj: SaveMilestoneAdj,
            SaveMilestonePayment: SaveMilestonePayment,
        }
    };
    ApprovalFactory.$inject = injectParams;
    angular.module('CardtrendApp').factory('Api', ApprovalFactory);
}());
(function () {
    var injectParams = ['$http', 'Utils'];
    var notificationSearchFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/NotificationSearch';
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
        WebNtfyEventSearch = function (obj, url) {
            var ApiRef = url || Api;
            return $http({
                method: 'GET',
                url: ApiRef + '/WebNtfyEventSearch?' + $.param(obj)
            });
        }
        WebEvtSelect = function (ob, url) {
            var ApiRef = url || Api;
            return $http({
                method: 'POST',
                url: ApiRef + 'WebEvtSelect',
                data: data
            })
        };
        WebNtfyEventRcptListSelect = function (obj, url) {
            var ApiRef = url || Api;
            return $http({
                method: "GET",
                url: $rootScope.getRootUrl() + "/EventConfiguration/WebNtfyEventRcptListSelect?" + $.param(obj),
                data: obj
            })
        }
        return {
            selectData: selectData,
            getFormData: getFormData,
            WebNtfyEventSearch: WebNtfyEventSearch,
            WebEvtSelect: WebEvtSelect,
            WebNtfyEventRcptListSelect: WebNtfyEventRcptListSelect
        }
    };
    notificationSearchFactory.$inject = injectParams;
    angular.module('notificationSearchApp').factory('Api', notificationSearchFactory);
}());
(function () {
    var injectParams = ['$http', 'Utils'];
    var EventConfigFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/EventConfiguration';
        var globalVariablesApi = $rootScope.getRootUrl() + '/GlobalVariables';
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
        WebEventTypeSelect = function (obj, url) {
            var ApiRef = url || Api;
            return $http({
                method: 'GET',
                url: globalVariablesApi + '/FillData?' + $.param(obj)
            })
        }
        WebEventTypeMaint = function (obj, url) {
            var ApiRef = url || Api;
            return $http({
                method: "POST",
                url: ApiRef + "/WebEventTypeMaint",
                data: obj
            })
        }
        WebNtfyEventConfMaint = function (obj, url) {
            var ApiRef = url || Api;
            return $http({
                method: "POST",
                url: ApiRef + "/WebNtfyEventConfMaint",
                data: obj
            })
        }

        WebNtfyEventRcptListSelect = function (obj, url) {
            var ApiRef = url || Api;
            return $http({
                method: "GET",
                url: ApiRef + "/WebNtfyEventRcptListSelect?" + $.param(obj),
                data: obj
            })
        }
        WebNtfEvtConfDelete = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/EventConfiguration/WebNtfEvtConfDelete?' + Params,
                method: 'GET'
            });
        }
        WebNtfEvtConfDelete = function (obj) {
            var Params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/EventConfiguration/WebNtfEvtConfDelete?' + Params,
                method: 'GET'
            });
        }
        WebNtfEvtConfRcptDelete = function (obj) {
            var params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/EventConfiguration/WebNtfEvtConfRcptDelete',
                data: obj,
                method: 'POST'
            });
        }
        WebGetRefCmpyName = function (obj) {
            var params = $.param(obj);
            return $http({
                url: $rootScope.getRootUrl() + '/EventConfiguration/WebGetRefCmpyName?' + params,
                method: 'GET'
            });
        }
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
            WebRebatePlanMaint: WebRebatePlanMaint,
            WebEventTypeSelect: WebEventTypeSelect,
            WebEventTypeMaint: WebEventTypeMaint,
            WebNtfyEventConfMaint: WebNtfyEventConfMaint,
            WebNtfyEventRcptListSelect: WebNtfyEventRcptListSelect,
            WebNtfEvtConfRcptDelete: WebNtfEvtConfRcptDelete,
            WebNtfEvtConfDelete: WebNtfEvtConfDelete,
            WebGetRefCmpyName: WebGetRefCmpyName
        }
    };

    EventConfigFactory.$inject = injectParams;
    angular.module('CardtrendApp').factory('Api', EventConfigFactory);
}());
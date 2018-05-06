(function () {
    var injectParams = ['$http', 'Utils'];
    var globalVariableFactory = function ($http, $rootScope) {
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
                url: ApiRef + '/WebEventTypeSelect'
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
        WebEventTypeTemplateSelect = function (obj, url) {
            var ApiRef = url || Api;
            return $http({
                method: 'GET',
                url: ApiRef + '/WebEventTypeTemplateSelect?' + $.param(obj)
            })
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
            WebEventTypeTemplateSelect: WebEventTypeTemplateSelect
        }
    };
    globalVariableFactory.$inject = injectParams;
    angular.module('CardtrendApp').factory('Api', globalVariableFactory);
}());
(function () {
    var injectParams = ['$http', 'Utils'];
    var SOASummaryFactory = function ($http, $rootScope) {
        var Api = $rootScope.getRootUrl() + '/SOASummary';
        var selectData = function () {
            return $http({
                method: 'GET',
                url: Api
            })
        };
        getFormData = function () {
            return $http({
                method: 'GET',
                url: Api + '/FillData'
            })
        };
        WebAcctSOASumm = function (obj) {
            return $http({
                url: $rootScope.getRootUrl() + '/SOASummary/WebAcctSOASumm?' + $.param(obj), //addAPI
                method: 'GET',
            });
        }

        return {
            selectData: selectData,   //  this fetch the partialviews and stuff
            getFormData: getFormData, // fill all the selects and radios
            WebAcctSOASumm: WebAcctSOASumm,
        }
    };

    SOASummaryFactory.$inject = injectParams;
    angular.module('SOASummaryApp').factory('Api', SOASummaryFactory);
}());
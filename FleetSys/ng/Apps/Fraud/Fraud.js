angular.module('FraudApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select', 'ngRouteAnimationManager']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};
    $rootScope.urlPrefix= $('#hdUrlPrefix').val();

    $rootScope.$on("updatePath", function (event, path) {
        $location.path(path);
    });

    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });

    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();
        if (_location == '/new') {
            $rootScope.obj._type = 'edit';
            $rootScope.obj.eventId = null;
        } else if (_location == '/') {
            $rootScope.obj._type = 'index';
            $rootScope.obj.eventId = null;
        }
        else {
            $rootScope.obj.eventId = $routeParams.eventId;
            $rootScope.obj.acctNo = $routeParams.acctNo;
            $rootScope.obj.cardNo = $routeParams.cardNo;
            $rootScope.obj.cmpyName = $routeParams.cmpyName;
            $rootScope.obj._type = 'edit';
        }
    });
})


.config(function ($routeProvider, RouteAnimationManagerProvider) {
    var rootUrl = $('#hdUrlPrefix').val();
    $routeProvider.when('/', {
        templateUrl: 'index.html',
        controller: 'indexController',
        data: {
            animationConf: {
            }
         
        }
    })
      .when('/new', {
            templateUrl: rootUrl + '/Fraud/Tmpl?Prefix=fci&type=New',
            controller: 'detailsController',
            pageKey: 'fraudInfo',

            data: {
                animationConf: {
                }
            }
        })
     .when('/events/:eventId?', {
         templateUrl: rootUrl + '/Fraud/Tmpl?Prefix=fci&type=Index',
         controller: 'detailsController',
         pageKey: 'fraudInfo',

         data: {
             animationConf: {
             }
         }
     })
     .when('/txndispute/:eventId/:acctNo/:cardNo/:cmpyName*', {
         templateUrl: rootUrl + '/Fraud/Tmpl?Prefix=txn&type=Index',
         controller: 'txndisputeController',
         pageKey: 'txnDispute',

         data: {
             animationConf: {
             }
         }
     })
     .when('/print/:eventId', {
         templateUrl: rootUrl + '/Fraud/Tmpl?Prefix=prt&type=Index',
         controller: 'printController',
         pageKey: 'print',

         data: {
             animationConf: {
             }
         }
     })
     .when('/fileManager/:eventId?', {
         templateUrl: rootUrl + '/Fraud/Tmpl?Prefix=fil&type=Index',
         controller: 'fileManagerController',
         pageKey: 'fraudfileManager'
     })
    ;
    RouteAnimationManagerProvider.setDefaultAnimation('fade');
})


.controller('indexController', function ($scope, $rootScope, $location, Api) {


})

.controller('detailsController', function ($scope, $routeParams, $rootScope, $location, Api, Utils) {




})

.controller('txndisputeController', function ($scope, $routeParams, $rootScope, $location, Api, Utils, $compile) {

})

.controller('printController', function ($scope, $routeParams, $rootScope, $location, Api, Utils) {


})

.controller('fileManagerController', function ($scope, $rootScope, $routeParams, $location, Api, Utils, $http) {


})

.directive('fileDropzone', function ($compile, $timeout) {

        return {
            restrict: 'A',
            link: function (scope, $element, attrs) {
                if (scope.eventId){
                if (typeof Dropzone != 'undefined'  ) {
                    Dropzone.options.fileDropzone = {
                        autoProcessQueue: true,
                        parallelUploads: 10,
                        uploadMultiple: true,
                        maxFilesize: 4,
                        acceptedFiles: ".jpeg,.jpg,.png,.pdf,.doc,.docx,.xls,.xlsx,.txt",
                        init: function () {
                            this.on("addedfile", function (file) {
                                //on file added
                            });
                            this.on("sending", function (file, xhr, formData) {

                                xhr.setRequestHeader( "ApplId" , scope.eventId );
                          
                                //Max , 20160512 -FolderPath '2' is for Fraud (refer enums - PathFolder)
                                formData.append("FolderPath", scope.FolderPath);
                            });
                            this.on("success", function (file, xhr, formData) {
                                scope.files.push({ FileName: file.name, Size: parseInt(file.size / 1024, 10), Extension: file.type, CreatedDate: 'Just now', Class: 'success' });
                                //  scope.dropZone.removeFile(file);
                                scope.dropZone.removeAllFiles();
                                scope.$apply();
                            });
                            this.on("queuecomplete", function (file, xhr, formData) {
                                alert('queue complete');
                            });
                            this.on("processing", function () {

                            }); 
                         
                        }
                    };
                    Dropzone.autoDiscover = false;
                    Dropzone.autoProcessQueue = true;
                }
                scope.dropZone = new Dropzone("#file-dropzone"),
    dze_info = $("#dze_info"),
    status = {
        uploaded: 0,
        errors: 0
    };
                $timeout(function () {

                });
                }
               
            }
        }

    });





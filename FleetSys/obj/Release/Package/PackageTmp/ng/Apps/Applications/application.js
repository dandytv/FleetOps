
/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
//angular.module('ApplicationsApp', ['ngRoute', 'App.Utils']).run(function (Utils, $rootScope, $location, $routeParams) {
angular.module('CardtrendApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};

    $rootScope.$on("updatePath", function (event, path) {
        $location.path(path);
    });

    $rootScope.$on('$routeChangeStart', function (e, current, pre) {
        $rootScope.$broadcast('routeChanged', current.pageKey);
    });
    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
        var _location = $location.path();

        //$rootScope.$broadcast('routeChanged', current.pageKey);

        if (_location == '/new') {
            $rootScope.obj._type = 'new';
            $rootScope.obj.applId = null;
        } else if (_location == '/') {
            $rootScope.obj._type = 'index';
            $rootScope.obj.applId = null;
        } else {
            $rootScope.obj.applId = $routeParams.applId;
            $rootScope.obj._type = 'edit';
        }
        //$rootScope.$broadcast('routeChanged', _location);
    });
})
.config(function ($routeProvider) {
     var rootUrl = $('#hdUrlPrefix').val();
     $routeProvider.when('/', {
         templateUrl: 'index.html',
         controller: 'indexController',
         pageKey: 'index'
     })
    .when('/new', {
        templateUrl: rootUrl + '/Applications/Tmpl?Prefix=gen&type=Index',
        controller: 'generalInfoController',
        pageKey: 'generalInfo'
    })
     .when('/generalInfo/:applId?', {
         templateUrl: rootUrl + '/Applications/Tmpl?Prefix=gen&type=Index',
         controller: 'generalInfoController',
         pageKey: 'generalInfo'

     })
     .when('/cao/:applId?', {
         templateUrl: rootUrl + '/Applications/Tmpl?Prefix=cao&type=Index',
         controller: 'caoController',
         pageKey: 'cao'
     })
     .when('/velocity/:applId?', {
         templateUrl: rootUrl + '/Applications/Tmpl?Prefix=vel&type=Index',
         controller: 'velocityController',
         pageKey: 'velocity'
     })
     .when('/vehicles/:applId?', {
         templateUrl: rootUrl + '/Applications/Tmpl?Prefix=veh&type=Index',
         controller: 'vehiclesController',
         pageKey: 'vehicles'
     })
     .when('/depositInfo/:applId?', {
         templateUrl: rootUrl + '/Applications/Tmpl?Prefix=dep&type=Index',
         controller: 'depositInfoController',
         pageKey: 'depositInfo'
     })
     .when('/skds/:applId?', {
         templateUrl: rootUrl + '/Applications/Tmpl?Prefix=skd&type=Index',
         controller: 'skdsController',
         pageKey: 'skds'
     })
      .when('/applicants/:applId?', {
          templateUrl: rootUrl + '/Applications/Tmpl?Prefix=app&type=Index',
          controller: 'applicantsController',
          pageKey: 'applicants'
      })
      .when('/contacts/:applId?', {
          templateUrl: rootUrl + '/Applications/Tmpl?Prefix=con&type=Index',
          controller: 'contactsController',
          pageKey: 'contacts'
      })
      .when('/address/:applId?', {
          templateUrl: rootUrl + '/Applications/Tmpl?Prefix=add&type=Index',
          controller: 'addressController',
          pageKey: 'address'
      })
      .when('/costcentre/:applId?', {
          templateUrl: rootUrl + '/Applications/Tmpl?Prefix=csc&type=Index',
          controller: 'costcentreController',
          pageKey: 'costCentre'
      })
        .when('/costcentre/:applId?/:cst/velocity', {
            templateUrl: rootUrl + '/Applications/Tmpl?Prefix=vel&type=Index',
            controller: 'velocityController',
            pageKey: 'costCentre'
        })
     .when('/Miscellanious/:applId?', {
         templateUrl: rootUrl + '/Applications/Tmpl?Prefix=mis&type=Index',
         controller: 'MiscellaniousController',
         pageKey: 'Miscellanious'
     })
    .when('/fileManager/:applId?', {
        templateUrl: rootUrl + '/Applications/Tmpl?Prefix=fil&type=Index',
        controller: 'FileManagerController',
        pageKey: 'fileManager'
    })
    .when('/approval/:applId?', {
        templateUrl: rootUrl + '/Applications/Tmpl?Prefix=apr&type=Index&overrideController=Approval',
        controller: 'approvalController',
        pageKey: 'approval'
    })
 })
.directive('bootstrapWizard', function ($compile) {
    return {
        restrict: 'A',
        link: function (scope, $element, attrs) {
            $element.bootstrapWizard({
                'nextSelector': '.button-next',
                'previousSelector': '.button-previous',
                onNext: function (tab, navigation, index) {
                },
                onTabShow: function (tab, navigation, index) {
                    var $total = navigation.find('li').length;
                    var $current = index + 1;
                    var $percent = ($current / $total) * 100;
                    $element.find('.progress-bar').css({
                        width: $percent + '%'
                    });

                    $element.find('.steps li').each(function (index) {
                        $(this).removeClass('complete');
                        index += 1;
                        if (index < $current) {
                            $(this).addClass('complete');
                        }
                    });

                    if ($current >= $total) {
                        $('#wizard2').find('.button-next').hide();
                        $('#wizard2').find('.button-finish').show();
                    } else {
                        $('#wizard2').find('.button-next').show();
                        $('#wizard2').find('.button-finish').hide();
                    }
                }
            });
        }
    }
})
.directive('fileDropzone', function ($compile, $timeout) {

                return {
                    restrict: 'A',
                    link: function (scope, $element, attrs) {
                        if (typeof Dropzone != 'undefined') {
                            Dropzone.options.fileDropzone = {
                                autoProcessQueue: true,
                                parallelUploads: 10,
                                uploadMultiple: true,
                                init: function () {
                                    this.on("addedfile", function (file) {
                                        //on file added
                                    });
                                    this.on("sending", function (file, xhr, formData) {
                                        //formData.append("Id", scope.Album.Id); // Append all the additional input data of your form here!
                                        xhr.setRequestHeader("ApplId", scope.applId);
                                    });
                                    this.on("success", function (file, xhr, formData) {
                                        var filenames = '';
                                        if (file.name.indexOf('&') > -1)
                                        {
                                            filenames = file.name.replace('&', '_');
                                        } else
                                        {
                                            filenames = file.name;
                                        }
                                        scope.files.push({ FileName: filenames, Size: parseInt(file.size / 1024, 10), Extension: file.type, CreatedDate: 'Just now', Class: 'success' });
                                        scope.$apply();
                                    });
                                    this.on("queuecomplete", function (file, xhr, formData) {
                                        alert('queue complete');
                                    });
                                    this.on("processing", function () {
                                        //this.options.autoProcessQueue = true;
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
                            scope.$apply();
                        });
                       // scope.$apply();
                    }
                }

            });

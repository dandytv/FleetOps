
angular.module('SOASummaryApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {
        SelectedStmtDate: null,
        TxnCode: null,
        AcctNo: null,

    };

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
        } else {
            $rootScope.obj.applId = $routeParams.applId;
            $rootScope.obj._type = 'edit';
        }
        //$rootScope.$broadcast('routeChanged', _location);
    });
})
 .config(function ($routeProvider) {
     $routeProvider.when('/', {
         templateUrl: 'index.html',
         controller: 'indexController',
         pageKey: 'index'
     })

     .when('/SOATxnCategoryList/:acctNo/:stmtDate', {
         templateUrl: 'indexSOA1.html',
         controller: 'statementDetailController',
         pageKey: 'index'
     })
     .when('/SOATxnList/:acctNo/:stmtDate/:TxnCd', {
         templateUrl: 'indexSOA2.html',
         controller: 'SOATxnListController',
         pageKey: 'index'
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
                                        scope.files.push({ FileName: file.name, Size: parseInt(file.size / 1024, 10), Extension: file.type, CreatedDate: 'Just now', Class: 'success' });
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
                        scope.$apply();
                    }
                }

            });

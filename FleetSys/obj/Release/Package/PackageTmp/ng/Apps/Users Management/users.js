/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.js" />
angular.module('userApp', ['ngRoute', 'App.Utils', 'ngSanitize', 'ui.select']).run(function (Utils, $rootScope, $location, $routeParams) {
    $rootScope.tables = {};
    $rootScope.obj = {};
    //applicant?ApplId=23238&appcId=2387627
    $rootScope.$on("updatePath", function (event, path) {
        $location.path(path);
    });
    $rootScope.$on('$routeChangeSuccess', function (e, current, pre) {
    });
})
.config(function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: 'index.html',
            controller: 'indexController'
        }).when('/configure/:userId', {
            templateUrl: 'configure.html',
            controller: 'configureController'
        })
        .when('/new', {
            templateUrl: 'configure.html',
            controller: 'configureController'
        })
    })
.directive('dynaTree', function ($compile, $rootScope) {
        return {
            restrict: 'A',
            link: function (scope, $element, attrs) {
                scope.$on('indexTree', function (data, nodes) {
                    if ($rootScope.dynatree) {
                        $element.dynatree('destroy');
                    }
                    var tree = $element.dynatree({
                        onActivate: function (node) {
                            scope.$emit('listControls', node);
                            //if (node.data.pageLevel && !node.data.controlsLoaded) {
                            //////self.activateChildControls(node.data);
                            //    node.data.controlsLoaded = true;
                            //}
                        },
                        children: nodes,
                        checkbox: true,
                        onSelect: function (flag, node) {
                            var status = flag == true ? 1 : 0;
                            scope.$apply(function () {
                                //console.log("node is" + $scope.dbModules);
                                var pageInfo = _.find(scope.Pages, function (obj) {
                                    return obj.PageId == node.data.pageId;
                                });
                                if (!scope.dbSections.length) {
                                    scope.dbSections.push({ SectionId: node.data.key, SectionStatus: status, PageId: node.data.pageId, ModuleId: pageInfo.ModuleId });
                                }
                                else {
                                    var item = _.find(scope.dbSections, function (item) {
                                        return item.SectionId == node.data.key;
                                    });
                                    if (item) {
                                        scope.dbSections[scope.dbSections.indexOf(item)].SectionStatus = status;
                                    } else {
                                        scope.dbSections.push({ SectionId: node.data.key, SectionStatus: status, PageId: node.data.pageId, ModuleId: pageInfo.ModuleId });
                                    }
                                }
                            });

                        },
                    });
                    $rootScope.dynatree = tree;
                });

                scope.$on('destroyTree', function () {
                    if ($rootScope.dynatree) {
                        $element.dynatree('disable');
                    }
                })

            }
        }
    })
.directive('bootstrapWizard', function ($compile, $rootScope) {
    return {
        restrict: 'A',
        link: function (scope, $element, attrs) {
            $element.bootstrapWizard({
                'nextSelector': '.button-next',
                'previousSelector': '.button-previous',
                onTabClick: function (tab, navigation, index) {
                    $rootScope.prevTab = index;

                },
                onTabShow: function (tab, navigation, index) {
                    scope.$emit('Navigating', index);
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
.directive('bindSwitcher', function ($compile) {
    return {
        restrict: 'A',
        link: function (scope, $element, attrs) {
            $element.on('change', function () {
                var index = attrs.item;
                if (attrs.level == 'modules') {
                    var item = scope.Modules[index];
                    if (this.checked) {
                        item.Sts = 1;
                    } else {
                        item.Sts = 0;
                    }
                    item.Updated = true;
                    scope.Modules[index] = item;
                } else if (attrs.level == 'pages') {
                    var item = scope.Pages[index];
                    if (this.checked) {
                        item.Sts = 1;
                    } else {
                        item.Sts = 0;
                    }
                    item.Updated = true;
                    scope.Pages[index] = item;
                }
                scope.$apply();
            })
        }
    }
});
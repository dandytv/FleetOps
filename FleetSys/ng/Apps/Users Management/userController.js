(function () {
    var injectParams = ['$scope', '$routeParams', '$rootScope', '$location', 'Api', 'Utils'];
    var indexController = function ($scope, $routeParams, $rootScope, $location, Api, Utils) {
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblUsers',
            ajax: $rootScope.getRootUrl() + '/UserAccess/ftUserAccessList',
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
        $scope.refresh = function () {
            $rootScope.tables[$scope.dtOptions.id].fnDraw();
        }
        $scope.$on("indexSelected", function (event, aData) {
            $rootScope.obj._type = 'edit';
            $location.path('/configure/' + aData[0]);
            $scope.$apply();
        });
    };
    var configureController = function ($scope, $routeParams, $rootScope, $location, Api, Utils) {
        var UserId = $routeParams.userId;
        $scope.Modules = [];
        $scope.Pages = [];
        $scope.Sections = [];//added
        $scope.dbSections = [];
        $scope.Controls = [];
        $scope.ChildControls = [];
        $scope.treeLoading = false;
        $scope.isDisabled = false;
        $scope.State = {
            userId: UserId,
            userCreated: false,
            modulesLoaded: false,
            pagesLoaded: false,
            controlsLoaded: false,
            modulesUpdated: true,
            pagesUpdated: true,
            controlsUpdated: true,
            isModuleFirstLoad: true,
            isPagesFirstLoad: true,
            isControlsFirstLoad: true,
            masterSwitches: {
                Modules: null,
                Pages: null,
                controls: null
            }

        };


        getUpdatedSection = function (sectionId) {
            if ($scope.dbSections.length) {
                var section = _.find($scope.dbSections, function (item) {
                    return item.SectionId == sectionId;
                });
                if (section)
                    return section;
            } else {
                var section = _.find($scope.Controls, function (item2) {
                    return item2.SectionId == sectionId;
                });
                if (section) {
                    return section;
                }
            }
        }


        $scope.masterToggle = function (type, sts) {

            if (type == "ChildControls") {
                var sectionId = $scope[type][0].SectionId;
                var sectionInfo = getUpdatedSection(sectionId);
                if (sectionInfo.SectionStatus == 0)
                    return;
            }
            $scope.State.masterSwitches[type] = sts;
            _.each($scope[type], function (item) {
                item.Sts = sts;
                item.Updated = true;
            });
        }

        Api.GetUserAccessDetail({ accessInd: 'I', userId: UserId }).success(function (data) {
            $scope._Object = data.User;
            $scope._Selects = data._Selects;
            if (data.User.UserId != null)
                $scope.State.userCreated = true;
        });

        $scope.$on('Navigating', function (data, index) {
            var ModuleList = [];
            var PagesList = [];
            if ($rootScope.prevTab > index) {
                return;
            }

            if (index == 2) {
                if (!$scope.State.modulesLoaded) {
                    Utils.PNotify({ flag: 1, Descp: "Haven't configured modules yet" });
                    return false;
                }
            }
            else if (index == 3) {
                if (!$scope.State.pagesLoaded) {
                    Utils.PNotify({ flag: 1, Descp: "Haven't configured pages yet" });
                    return false;
                }
            }
            else if (index == 4) {
                if (!$scope.State.pagesLoaded) {
                    Utils.PNotify({ flag: 1, Descp: "Haven't configured pages yet" });
                    return false;
                }
            }


            if (index == 1) {      //user is going to modules
                if (!$scope.Modules.length) {
                    Api.getModules({ UserId: UserId }).success(function (data) {
                        $scope.Modules = data.aaData;
                        $scope.State.modulesLoaded = true;
                    });
                }
            }


            else if (index == 2) {   //user is going for pages

                $.each($scope.Modules, function (i, item) {
                    if ($scope.State.isModuleFirstLoad) {
                        if (item.Sts == 1 || item.Updated) {
                            ModuleList.push(item);
                        }
                    } else if (item.Sts == 1 || item.Updated) {
                        ModuleList.push(item);
                    }
                });
                $scope.dbModules = ModuleList;

                var filtered = _.filter(ModuleList, function (item) {
                    if (item.Sts == 1) {
                        return item;
                    }
                });

                var filteredList = _.pluck(filtered, 'ModuleId');
                var _obj = {
                    ModuleList: filteredList,
                    UserId: UserId,
                    AccessInd: 'I',
                    mode: "pages"
                };
                Api.getPageControls(_obj).success(function (data) {
                    $scope.Pages = data.aaData;
                    $scope.State.pagesLoaded = true;
                    $scope.State.modulesUpdated = false;
                    if ($scope.State.isModuleFirstLoad)
                        $scope.State.isModuleFirstLoad = false;
                    _.each($scope.Modules, function () {
                    })
                });
            }

            else if (index == 3) {     //user is going for controls
                $.each($scope.Pages, function (i, item) {
                    if ($scope.State.isPagesFirstLoad) {
                        if (item.Sts == 1 || item.Updated) {
                            PagesList.push(item);
                        }
                    } else if (item.Sts == 1 || item.Updated) {
                        PagesList.push(item);
                    }
                });
                $scope.dbPages = PagesList;
                var filtered = _.filter(PagesList, function (item) {
                    if (item.Sts == 1) {
                        return item;
                    }
                });
                var filteredModules = _.pluck(filtered, 'ModuleId');
                var filteredPages = _.pluck(filtered, 'PageId');
                var _obj = {
                    PageList: filteredPages,
                    ModuleList: filteredModules,
                    UserId: UserId,
                    AccessInd: 'I',
                    mode: "controls"
                };
                $scope.ChildControls = [];
                $scope.treeLoading = true; $scope.$apply();
                Api.getPageControls(_obj).success(function (data) {
                    $scope.Controls = data.aaData;
                    $scope.Nodes = [];
                    generateTree();
                    $scope.State.controlsLoaded = true;
                    if ($scope.State.isPagesFirstLoad)
                        $scope.State.isPagesFirstLoad = false;
                });

            }
        });
        $scope.IsSaveUserAccess = function (obj) {
            var isToSave = true;
            if (obj.UserId == null || obj.UserId == '') {
                Utils.PNotify({ flag: 1, Descp: "UserId is a mandatory field." });
                return isToSave = false;
            } else if (obj.Name == null || obj.Name == '') {
                Utils.PNotify({ flag: 1, Descp: "Name is a mandatory field." });
                return isToSave = false;
            } else if (obj.SelectedAccessInd == null || obj.SelectedAccessInd == '') {
                Utils.PNotify({ flag: 1, Descp: "Access Indicator is required." });
                return isToSave = false;
            } else if (obj.selectedSts == null || obj.selectedSts == '') {
                Utils.PNotify({ flag: 1, Descp: "Status is required." });
                return isToSave = false;
            }
            else if (obj.ContactNo == null || obj.ContactNo == '') {
                Utils.PNotify({ flag: 1, Descp: "Contact No is a mandatory field." });
                return isToSave = false;
            } else if (obj.EmailAddr == null || obj.EmailAddr == '') {
                Utils.PNotify({ flag: 1, Descp: "Email Address is required." });
                return isToSave = false;
            }
            else if (obj.EmailAddr != '') {
                var EMAIL_REGEXP = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                var isMatchRegex = EMAIL_REGEXP.test(obj.EmailAddr);
                if (!isMatchRegex) {
                    Utils.PNotify({ flag: 1, Descp: "Please input a valid email." });
                    return isToSave = false;
                }
            }

            return isToSave = true;
        }
        $scope.SaveUserAccess = function (event) {
            if ($scope.IsSaveUserAccess($scope._Object)) {
                Utils.InfoNotify();
                var model = { _userAccessPermission: $scope._Object, isUpdate: UserId ? true : false };
                Api.SaveUserAccess(model).success(function (data) {
                    Utils.finalResultNotify(data.resultCd);
                    if (data.resultCd.flag == 0) {
                        UserId = $scope._Object.UserId;
                        $scope.State.userCreated = true;
                    }

                });
            }
        };

        $scope.updateControlStatus = function (item, status) {
            var sectionStsAltered = $scope.dbSections.length ? true : false;

            var page = _.find($scope.Pages, function (page) {
                return page.PageId == item.PageId;
            });

            if (page && page.Sts == 0) {
                return;
            }
            if (sectionStsAltered) {
                var section = _.find($scope.dbSections, function (item2) {
                    return item2.SectionId == item.SectionId;
                });

                if (section && section.SectionStatus == 0) {
                    return;
                } else if (section && section.SectionStatus == 1) {
                    $.extend(item, { Sts: status, Updated: true });
                }
            }
            else {
                var section = _.find($scope.Controls, function (item2) {
                    return item.SectionId == item2.SectionId;
                });
                if (section && section.SectionStatus == 0) {

                } else {
                    $.extend(item, { Sts: status, Updated: true });
                }
            }
        }

        $scope.saveAll = function () {

            var self = this;
            var WebPageControl = [];
            if ($scope.Controls) {
                $.each($scope.Controls, function (i, val) {
                    $.each(val.CtrlList, function (i, val2) {
                        if (val2.Updated) {
                            //val.SectionStatus
                            WebPageControl.push({ CtrlId: val2.CtrlId, PageId: val2.PageId, ModuleId: val2.ModuleId, Sts: val2.Sts, SectionId: val2.SectionId });
                        }
                    });
                });
            }

            Utils.InfoNotify();
            Api.postControlMap({ ModuleList: $scope.dbModules, CtrlList: WebPageControl, SectionList: $scope.dbSections, PageList: $scope.dbPages, UserId: UserId }).success(function (data) {
                Utils.finalResultNotify(data.resultCd);
                $.each($scope.Modules, function (i, val) { delete val.Updated; });
                $.each($scope.Pages, function (i, val) { delete val.Updated; });
                $.each($scope.Controls, function (i, val) { delete val.Updated; });
                //$.each($scope.Sections, function (i, val) { delete val.Updated; });//added
            });
        }
        generateTree = function () {
            $.each($scope.dbModules, function (i, val) {
                if (val.Sts == 1) {
                    var contains = _.find($scope.dbPages, function (item) {
                        return item.ModuleId == val.ModuleId;
                    });
                    if (contains) {
                        $scope.Nodes.push({ title: val.Descp, isFolder: true, key: val.ModuleId, children: createPageNodes(val.ModuleId), hideCheckbox: true });
                    }
                }
            });
            $scope.$broadcast('indexTree', $scope.Nodes);
            $scope.treeLoading = false;
        }
        createPageNodes = function (ModuleId) {
            var pageNodes = [];
            $.each($scope.dbPages, function (i, val) {
                if (val.Sts == 1 && val.ModuleId == ModuleId) {
                    pageNodes.push({
                        title: val.Descp, isFolder: true, key: val.PageId, controlsLoaded: false, pageLevel: true,
                        children: self.createSectionNodes(val.PageId), hideCheckbox: true
                    });
                }
            });
            return pageNodes;
        }
        createSectionNodes = function (PageId) {
            var sectionNodes = [];
            $.each($scope.Controls, function (i, val) {
                if (val.PageId == PageId) {
                    sectionNodes.push({
                        title: val.Section, isFolder: true, key: val.SectionId, SectionStatus: val.SectionStatus,
                        pageId: PageId, controlsLoaded: false, pageLevel: true, select: val.SectionStatus == 1 ? true : false,
                    });
                }
            });
            return sectionNodes;
        }

        $scope.$on('ChangingTab', function (data, index) {
            return false;
        });

        $scope.$on('listControls', function (data, node) {

            $scope.State.masterSwitches = {
                Modules: null,
                Pages: null,
                controls: null
            };

            var self = this;
            var selectedControls = null;
            var pageId = node.data.pageId;
            var sectionId = node.data.key;
            $scope.ChildControls = [];

            var item = _.find($scope.dbSections, function (item) {
                return item.SectionId == sectionId;
            });

            if (typeof item == "undefined" || (item && item.SectionStatus == 1)) {
                $.each($scope.Controls, function (i, val) {
                    if (val.PageId == pageId && val.SectionId == sectionId) {
                        var xx = val.CtrlList.slice();
                        $scope.ChildControls.push.apply($scope.ChildControls, xx);
                    }
                });
            }
            $scope.$apply();
        })

    };

    //inject service
    indexController.$inject = injectParams;
    configureController.$inject = injectParams;

    angular.module('userApp').controller('indexController', indexController);
    angular.module('userApp').controller('configureController', configureController);
}());
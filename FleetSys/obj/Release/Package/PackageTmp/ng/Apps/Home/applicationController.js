(function () {
    var injectParam1 = ['$scope', '$rootScope', '$location', '$timeout', '$window', 'Utils', '$http', 'Api'];
    var injectParam2 = ['$scope', '$rootScope', '$routeParams', '$location', 'Api', 'Utils'];

    var indexController = function ($scope, $rootScope, $location, $timeout, $window, Utils, $http, Api) {
        $rootScope.obj._type = 'index';
        $scope.TxnAdjEnable = false;
        $scope.pymtTxnEnable = false;
        $scope.merchAdjEnable = false;
        $scope.modalOpen = false;
        $scope.approveObj = {};
        $scope.selectedRefIds = [];
        $scope.aaData = [];
        $scope.selectedStatus = null;
        $scope.dtOptions = {
            serverSide: true,
            processing: true,
            checkBox: false,
            "scrollX": true,
            id: 'tblApplications',
            ajax: $rootScope.getRootUrl() + '/Applications/WebMilestoneListSelect?' + $.param({ Ind: 1 }),
            edit: {
                level: 'scope',
                func: 'indexSelected'
            }
        };
        Api.getFormData({ Prefix: 'apr' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            // $scope._Object.ApplId = applId;
        });
        $scope.miller = function () {
        }
        $scope.loadTxnAdj = function () {
            $timeout(function () {
                $scope.dtOptionsTxnAdj = {
                    serverSide: true,
                    processing: true,
                    checkBox: false,
                    autoWidth: false,
                    "scrollX": true,
                    id: 'tbltxnAdj',
                    ajax: $rootScope.getRootUrl() + '/Approval/WebMilestoneListSelect?' + $.param({ Ind: 1, workflowcd: "Adj" }),
                    //edit: {
                    //    level: 'scope',
                    //    func: 'indexAdjSelected'
                    //},
                    rowClick: function (aData, nRow) {
                        $scope.$apply(function () {
                            $scope.approveObj = {
                                batchId: aData[0],
                                type: 'txnAdj',
                                Id: aData[7]
                            }
                        });
                    },
                    aoColumnDefs: [
                    { "sClass": "detail-toggler", "aTargets": [0] },
                    ],
                    childTable: {
                        format: function (nRows, options) {
                            var htm;
                            var rows = _.filter($scope.aaData[options.id], function (data) {
                                return data[0] == nRows[0];//txnid
                            });
                            var header = "<thead><tr><th>&nbsp;</th><th>Ref Key</th><th>Task No</th><th>Description</th><th>Priority</th><th>Creation Date</th><th>Status</th></tr></thead>";
                            var rows2 = _.map(rows, function (Next) {
                                return '<tr>' +
                                   '<td style=\'background-color:#F8F9FA\'><input type=\'checkbox\' checked /></td>' +
                                   '<td style=\'background-color:#F8F9FA\'>' + Next[1] + '</td>' +
                                   '<td style=\'background-color:#F8F9FA\'>' + Next[2] + '</td>' +
                                   '<td style=\'background-color:#F8F9FA\'>' + Next[3] + '</td>' +
                                   '<td style=\'background-color:#F8F9FA\'>' + Next[4] + '</td>' +
                                   '<td style=\'background-color:#F8F9FA\'>' + Next[5] + '</td>' +
                                   '<td style=\'background-color:#F8F9FA\'>' + Next[6] + '</td></tr>'
                            });
                            var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                            var btnGo = '<div class=\'panel\' ng-show=\'pymtTxnEnable\'>' +
                                            '<div class=\'panel-footer\'>' +
                                                '<div class=\'pull-right\'>{0}{1}' +
                                                '</div><div class=\'clearfix\'></div>' +
                                            '</div>' +
                                        '</div>';

                            if ($window.TxnAdj_BtnAppv_Sts == "0") {
                                btnGo = btnGo.replace(/\{0}/, '');
                            }
                            else {
                                btnGo = btnGo.replace(/\{0}/, '<button onClick=\"angular.element($(this)).scope().approveAll($(this),\'Adj\',false);\" class=\'btn btn-primary\'' +
                                    'style=\'margin-right:10px;\'' + ($window.TxnAdj_BtnAppv_Sts == '1' ? '' : ' disabled="disabled"') + '>Approve&nbsp;</button>');
                            }

                            if ($window.TxnAdj_BtnQuickAppv_Sts == "0") {
                                btnGo = btnGo.replace(/\{1}/, '');
                            }
                            else {
                                btnGo = btnGo.replace(/\{1}/, '<button onClick=\"angular.element($(this)).scope().approveAll($(this),\'Adj\',true);\" class=\'btn btn-success\'' +
                                    ($window.TxnAdj_BtnQuickAppv_Sts == '1' ? '' : ' disabled="disabled"') + '>Quick Approve&nbsp;<i class=\'fa fa-arrow-circle-o-right\'></i></button>');
                            }

                            return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr><tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + btnGo + '</td></tr>';
                        },
                        edit: {
                            level: 'scope',
                            func: function (cells) {
                                var self = $(this);
                                //$scope.approveObj = {
                                //    type: 'txnAdj',
                                //    cells: cells
                                //};
                            }
                        },
                        sum: function sum(numbers) {
                            return _.reduce(numbers, function (result, current) {
                                return result + parseFloat(current[9]);
                            }, 0);
                        },
                        fngroupOp: function (e, settings, json, options) {
                            //json.aaData = [];

                            if ($scope.date) {
                                if (new Date() - $scope.date <= 2000)
                                    return;
                            }
                            $scope.$apply(function () {
                                $scope.date = new Date();
                                $scope.aaData[options.id] = json.aaData;
                            });
                            var rows = [];
                            _.each(json.aaData, function (data) {
                                if (rows.length) {
                                    var contains = _.find(rows, function (item) {
                                        return item[0] == data[0];
                                    })
                                    if (!contains) {
                                        rows.push(data);
                                    }
                                } else {
                                    rows.push(data);
                                }
                            });
                            json.aaData = rows;
                        }
                    }
                }
            }, 300);
        }

        $scope.moreDetails = function (url) {
            location.href = url;
        }
        $scope.loadPymt = function myfunction() {
            $timeout(function () {
                $scope.dtOptionsPymtTxn = {
                    serverSide: true,
                    processing: true,
                    checkBox: false,
                    autoWidth: false,
                    "scrollX": true,
                    id: 'tblPymtTxn',
                    ajax: $rootScope.getRootUrl() + '/Approval/WebMilestoneListSelect?' + $.param({ Ind: 1, workflowcd: "Pymt" }),
                    //edit: {
                    //    level: 'scope',
                    //    func: 'PymtindexSelected'
                    //},
                    "initComplete": function (settings, json) {
                        //    $rootScope.tables[$scope.dtOptionsPymtTxn.id].fnAdjustColumnSizing();            }
                    },
                    rowClick: function (aData, nRow) {
                        $scope.$apply(function () {
                            $scope.approveObj = {
                                batchId: aData[0],
                                type: 'pymtTxn',
                                Id: aData[7]
                            }
                        });
                    },
                    aoColumnDefs: [
                    { "sClass": "detail-toggler", "aTargets": [0] },
                    ],
                    childTable: {
                        format: function (nRows, options) {
                            var htm;
                            var rows = _.filter($scope.aaData[options.id], function (data) {
                                return data[0] == nRows[0];//txnid
                            });
                            var header = "<thead><tr><th>&nbsp;</th><th>Ref Key</th><th>Task No</th><th>Description</th><th>Priority</th><th>Creation Date</th><th>Status</th></tr></thead>";
                            var rows2 = _.map(rows, function (Next) {
                                return '<tr>' +
                                     '<td style=\'background-color:#F8F9FA\'><input type=\'checkbox\' checked /></td>' +
                                  '<td style=\'background-color:#FFF8F9FA\'>' + Next[1] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[2] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[3] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[4] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[5] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[6] + '</td></tr>'
                            });
                            //to the future web developer,i know this is an anti-pattern and totally not allowed, but due to the time constraints, i'm just gonna do it anyway and if u gonna judge me on this, i suggest you go fuck yourself...
                            var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                            var btnGo = '<div class=\'panel\' ng-show=\'pymtTxnEnable\'>' +
                                            '<div class=\'panel-footer\'>' +
                                                '<div class=\'pull-right\'>{0}{1}' +
                                                '</div><div class=\'clearfix\'></div>' +
                                            '</div>' +
                                        '</div>';
                            if ($window.PymtTxn_BtnAppv_Sts == "0") {
                                btnGo = btnGo.replace(/\{0}/, '');
                            }
                            else {
                                btnGo = btnGo.replace(/\{0}/, '<button onClick=\"angular.element($(this)).scope().approveAll($(this),\'Pymt\',false);\" class=\'btn btn-primary\'' +
                                    'style=\'margin-right:10px;\'' + ($window.PymtTxn_BtnAppv_Sts == '1' ? '' : ' disabled="disabled"') + '>Approve&nbsp;</button>');
                            }

                            if ($window.PymtTxn_BtnQuickAppv_Sts == "0") {
                                btnGo = btnGo.replace(/\{1}/, '');
                            }
                            else {
                                btnGo = btnGo.replace(/\{1}/, '<button onClick=\"angular.element($(this)).scope().approveAll($(this),\'Pymt\',true);\" class=\'btn btn-success\'' +
                                    ($window.PymtTxn_BtnQuickAppv_Sts == '1' ? '' : ' disabled="disabled"') + '>Quick Approve&nbsp;<i class=\'fa fa-arrow-circle-o-right\'></i></button>');
                            }
                            return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr><tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + btnGo + '</td></tr>';
                        },
                        edit: {
                            level: 'scope',
                            func: function (cells) {
                                var self = $(this);
                                //$scope.approveObj = {
                                //    type: 'pymtTxn',
                                //    cells: cells
                                //};
                            }
                        },
                        sum: function sum(numbers) {
                            return _.reduce(numbers, function (result, current) {
                                return result + parseFloat(current[9]);
                            }, 0);
                        },
                        fngroupOp: function (e, settings, json, options) {
                            //json.aaData = [];

                            if ($scope.date) {
                                if (new Date() - $scope.date <= 2000)
                                    return;
                            }
                            $scope.$apply(function () {
                                $scope.date = new Date();
                                $scope.aaData[options.id] = json.aaData;
                            });
                            var rows = [];
                            _.each(json.aaData, function (data) {
                                if (rows.length) {
                                    var contains = _.find(rows, function (item) {
                                        return item[0] == data[0];
                                    })
                                    if (!contains) {
                                        rows.push(data);
                                    }
                                } else {
                                    rows.push(data);
                                }
                            });
                            json.aaData = rows;
                        }
                    }
                }
            }, 300);
        }

        $scope.loadMerchantAdjustment = function () {
            $timeout(function () {
                $scope.dtOptionsMerchant = {
                    serverSide: true,
                    processing: true,
                    checkBox: false,
                    autoWidth: false,
                    "scrollX": true,
                    id: 'tblMerchantTxnAdjustment',
                    ajax: $rootScope.getRootUrl() + '/Approval/WebMilestoneListSelect?' + $.param({ Ind: 1, workflowcd: "MERCHADJ" }),
                    //edit: {
                    //    level: 'scope',
                    //    func: 'PymtindexSelected'
                    //},
                    "initComplete": function (settings, json) {
                        //    $rootScope.tables[$scope.dtOptionsPymtTxn.id].fnAdjustColumnSizing();            }
                    },
                    rowClick: function (aData, nRow) {
                        $scope.$apply(function () {
                            $scope.approveObj = {
                                batchId: aData[0],
                                type: 'MerchAdj',
                                Id: aData[7]
                            };
                        });
                    },
                    aoColumnDefs: [
                    { "sClass": "detail-toggler", "aTargets": [0] },
                    ],
                    childTable: {
                        format: function (nRows, options) {
                            var htm;
                            var rows = _.filter($scope.aaData[options.id], function (data) {
                                return data[0] == nRows[0];//txnid
                            });
                            var header = "<thead><tr><th>&nbsp;</th><th>Ref Key</th><th>Task No</th><th>Description</th><th>Priority</th><th>Creation Date</th><th>Status</th></tr></thead>";
                            var rows2 = _.map(rows, function (Next) {
                                return '<tr>' +
                                     '<td style=\'background-color:#F8F9FA\'><input type=\'checkbox\' checked /></td>' +
                                  '<td style=\'background-color:#FFF8F9FA\'>' + Next[1] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[2] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[3] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[4] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[5] + '</td>' +
                                  '<td style=\'background-color:#F8F9FA\'>' + Next[6] + '</td></tr>'
                            });
                            //to the future web developer,i know this is an anti-pattern and totally not allowed, but due to the time constraints, i'm just gonna do it anyway and if u gonna judge me on this, i suggest you go fuck yourself...
                            var fullTable = "<table class=\"table childtable\">" + header + "<tbody>" + rows2.join('') + '</tbody></table>';
                            var btnGo = '<div class=\'panel\' ng-show=\'pymtTxnEnable\'>' +
                                            '<div class=\'panel-footer\'>' +
                                                '<div class=\'pull-right\'>{0}{1}' +
                                                '</div><div class=\'clearfix\'></div>' +
                                            '</div>' +
                                        '</div>';
                            if ($window.PymtTxn_BtnAppv_Sts == "0") {
                                btnGo = btnGo.replace(/\{0}/, '');
                            }
                            else {
                                btnGo = btnGo.replace(/\{0}/, '<button onClick=\"angular.element($(this)).scope().approveAll($(this),\'MerchAdj\',false);\" class=\'btn btn-primary\'' +
                                    'style=\'margin-right:10px;\'' + ($window.PymtTxn_BtnAppv_Sts == '1' ? '' : ' disabled="disabled"') + '>Approve&nbsp;</button>');
                            }

                            if ($window.PymtTxn_BtnQuickAppv_Sts == "0") {
                                btnGo = btnGo.replace(/\{1}/, '');
                            }
                            else {
                                btnGo = btnGo.replace(/\{1}/, '<button onClick=\"angular.element($(this)).scope().approveAll($(this),\'MerchAdj\',true);\" class=\'btn btn-success\'' +
                                    ($window.PymtTxn_BtnQuickAppv_Sts == '1' ? '' : ' disabled="disabled"') + '>Quick Approve&nbsp;<i class=\'fa fa-arrow-circle-o-right\'></i></button>');
                            }
                            return '<tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + fullTable + '</td></tr><tr class=\'dynamic-created\'><td style=\'border-top:0\' colspan=\'16\'>' + btnGo + '</td></tr>';
                        },
                        edit: {
                            level: 'scope',
                            func: function (cells) {
                                var self = $(this);
                                //$scope.approveObj = {
                                //    type: 'pymtTxn',
                                //    cells: cells
                                //};
                            }
                        },
                        sum: function sum(numbers) {
                            return _.reduce(numbers, function (result, current) {
                                return result + parseFloat(current[9]);
                            }, 0);
                        },
                        fngroupOp: function (e, settings, json, options) {
                            //json.aaData = [];

                            if ($scope.date) {
                                if (new Date() - $scope.date <= 2000)
                                    return;
                            }
                            $scope.$apply(function () {
                                $scope.date = new Date();
                                $scope.aaData[options.id] = json.aaData;
                            });
                            var rows = [];
                            _.each(json.aaData, function (data) {
                                if (rows.length) {
                                    var contains = _.find(rows, function (item) {
                                        return item[0] == data[0];
                                    })
                                    if (!contains) {
                                        rows.push(data);
                                    }
                                } else {
                                    rows.push(data);
                                }
                            });
                            json.aaData = rows;
                        }
                    }
                }
            }, 300);
        }

        $scope.loadSOPRequestTracker = function () {
            $timeout(function () {
                $scope.dtOptionsSOPRequestTracker = {
                    serverSide: true,
                    processing: true,
                    checkBox: false,
                    autoWidth: false,
                    "scrollX": true,
                    id: 'tblSPORequestTracker',
                    ajax: $rootScope.getRootUrl() + '/Approval/WebMilestoneListSelect?' + $.param({ Ind: 1, workflowcd: "SPOREQTRCKR" }),
                    edit: {
                        level: 'scope',
                        func: 'indexSOPRequestSelected'
                    },
                    "initComplete": function (settings, json) {
                    }
                }
            }, 300);
        }

        $scope.loadPukalApproval = function () {
            $timeout(function () {
                $scope.dtOptionsPukalApproval = {
                    serverSide: true,
                    processing: true,
                    checkBox: false,
                    autoWidth: false,
                    "scrollX": true,
                    id: 'tblPukalApproval',
                    ajax: $rootScope.getRootUrl() + '/Approval/WebPukalApprovalMilestoneList?' + $.param({ Ind: 1, workflowcd: "PKLPYMT" }),
                    edit: {
                        level: 'scope',
                        func: 'indexPukalApprovalSelected'
                    },
                    "initComplete": function (settings, json) {
                    }
                }
            }, 300);
        }

        $scope.$on("indexPukalApprovalSelected", function (event, aData) {
            location.href = $rootScope.getRootUrl() + "/Approval#/Approval/PKLPYMT/" + aData[0] + '/' + aData[0];
            $scope.$apply();
        });
        $scope.$on("indexSelected", function (event, aData) {
            // location.href = "/Approval#/Approval/Appl/" + aData[0];
            location.href = $rootScope.getRootUrl() + "/Approval#/Approval/Appl/" + aData[0] + '/' + aData[0];
            $scope.$apply();
        });

        $scope.$on("indexAdjSelected", function (event, aData) {

            //$rootScope.obj.AcctNo = aData[1];
            //document.cookie = aData[1];
            location.href = $rootScope.getRootUrl() + "/Approval#/Approval/Adj/" + aData[0] + '/' + aData[1];
            //$scope.$apply();
            //location.href = "/Applications/Index#/approval/" + aData[0];
        });

        $scope.$on("PymtindexSelected", function (event, aData) {
            //console.log(aData);
            $rootScope.obj.AcctNo = aData[1];
            // document.cookie = aData[1];
            location.href = $rootScope.getRootUrl() + "/Approval#/Approval/Pymt/" + aData[0] + '/' + aData[1];
            $scope.$apply();
        });

        $scope.$on("indexSOPRequestSelected", function (event, aData) {
            location.href = $rootScope.getRootUrl() + "/Approval#/Approval/" + aData[12] + '/' + aData[0] + '/' + aData[1];
            $scope.$apply();
        });
        $scope.approveAll = function (element, workFlowCd, quickApproval) {
            var rows;
            $scope.workFlowCd = workFlowCd;
            $rootScope.selectedStatus = null;
            $scope.isQuickApproval = quickApproval;
            var tbl = element.closest('table').find('.childtable');
            $scope.selectedRefIds = [];
            if (workFlowCd == 'Adj') {
                rows = $scope.aaData[$scope.dtOptionsTxnAdj.id];
            } else if (workFlowCd == 'Pymt') {
                rows = $scope.aaData[$scope.dtOptionsPymtTxn.id];
            } else {
                rows = $scope.aaData[$scope.dtOptionsMerchant.id];
            }
            $scope.$apply(function () {
                tbl.find('tbody tr').each(function (_index) {
                    var self = $(this);
                    if ($(this).find('td:first').find('input').attr('checked')) {
                        var row = _.filter(rows, function (item) {
                            return item[0] == $scope.approveObj.batchId
                        });
                        $scope.selectedRefIds.push({ refId: self.find('td').eq(1).text(), ID: row[_index][7], currentTaskNo: self.find('td').eq(2).text() });
                    }
                });
                if ($scope.selectedRefIds.length) {
                    var _taskNos = _.map($scope.selectedRefIds, function (_, index) {
                        return _.currentTaskNo;
                    });
                    console.log($scope.selectedRefIds);
                    var _milestones = _.map($scope.selectedRefIds, function (_, index) {
                        return { workflowcd: $scope.workFlowCd, SelectedTaskNo: _.currentTaskNo }
                    });
                    if (!quickApproval) {
                        $scope.modalOpen = true;
                        $http({ method: 'POST', url: $rootScope.getRootUrl() + '/Applications/GetTaskNos?WorkFlowCd=Appl', data: _taskNos }).success(function (data) {
                            _.each(data.list, function (item, index) {
                                $.extend($scope.selectedRefIds[index], { TaskNo: item });
                            });
                            $http({ method: 'POST', url: $rootScope.getRootUrl() + '/Applications/FtMileStoneInfoList', data: _milestones }).success(function (data) {
                                _.each(data.lsItems, function (item, index) {
                                    $.extend($scope.selectedRefIds[index],
                                        {
                                            SelectedTaskNo: item.SelectedTaskNo,
                                            Descp: item.Descp,
                                            selectedOwner: item.selectedOwner,
                                            Remarks: item.Remarks,
                                            Url: $scope.workFlowCd == "Adj" ? item.Url.replace(/\{0}/g, $scope.approveObj.batchId).replace(/\{1}/g, $scope.selectedRefIds[index].refId) :
                                               $scope.workFlowCd == "Pymt" ? item.Url.replace(/\{0}/g, $scope.approveObj.batchId).replace(/\{1}/g, $scope.selectedRefIds[index].refId) :
                                               item.Url.replace(/\{0}/g, $scope.approveObj.batchId).replace(/\{1}/g, $scope.selectedRefIds[index].refId),
                                            SLADay: item.SLADay,
                                            CreditLimit: item.CreditLimit,
                                            SecurityAmt: item.SecurityAmt,
                                            WorkFlowCd: $scope.workFlowCd
                                        });
                                    console.log($scope.selectedRefIds[index]);
                                });
                            })
                        });
                    } else {
                        $scope.quickApprovalModal = true;
                    }
                }
            })
        };
        $scope.postApprove = function (i, type) {
            i = i || 0;
            type = type || $scope.workFlowCd;
            if (type == 'Adj') {
                SaveMilestoneAdj(i);
            } else if (type == 'Pymt') {
                SaveMilestonePymt(i);
            } else if (type == "MerchAdj") {
                SaveMerchantAdjustment(i);
            }
        }
        var SaveMilestoneAdj = function (index) {
            //$scope.approveObj = {
            //    batchId: aData[0],
            //    type: 'pymtTxn',
            //    Id: aData[7]
            //}
            var _ = $scope.selectedRefIds[index];
            if (!_) {
                $rootScope.tables[$scope.dtOptionsTxnAdj.id].fnDraw();
                return;
            }
            _.RefNo = $scope.approveObj.batchId, _.aprId = _.refId;
            if ($scope.isQuickApproval) {
                _.selectedStatus = $rootScope.selectedStatus;
            }
            //refkey is the cheque no.
            Utils.InfoNotify("Posting " + _.refId);
            $http({ method: 'POST', url: Utils.getRootUrl() + '/Applications/SaveMilestoneAdj', data: _ }).success(function (data) {
                Utils.finalResultNotify(data.result);
                $scope.postApprove(index + 1, 'Adj');
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    $scope.quickApprovalModal = false;
                }
                //$scope.modalOpen = false;
            })
        }
        var SaveMilestonePymt = function (index) {
            var _ = $scope.selectedRefIds[index];
            if (!_) {
                $rootScope.tables[$scope.dtOptionsPymtTxn.id].fnDraw();
                return;
            }
            _.RefNo = $scope.approveObj.batchId, _.aprId = _.refId;
            if ($scope.isQuickApproval) {
                _.selectedStatus = $rootScope.selectedStatus;
            }
            Utils.InfoNotify("Posting " + _.refId);
            $http({ method: 'POST', url: Utils.getRootUrl() + '/Applications/SaveMilestonePayment', data: _ }).success(function (data) {
                Utils.finalResultNotify(data.result);
                $scope.postApprove(index + 1, 'Pymt');
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    $scope.quickApprovalModal = false;
                }
            });

        }
        var SaveMerchantAdjustment = function (index) {
            var _ = $scope.selectedRefIds[index];
            if (!_) {
                $rootScope.tables[$scope.dtOptionsMerchant.id].fnDraw();
                return;
            }
            _.RefNo = $scope.approveObj.batchId, _.aprId = _.refId;
            if ($scope.isQuickApproval) {
                _.selectedStatus = $rootScope.selectedStatus;
            }
            Utils.InfoNotify("Posting " + _.refId);
            $http({ method: 'POST', url: Utils.getRootUrl() + '/Applications/SaveMilestoneMerchant', data: _ }).success(function (data) {
                Utils.finalResultNotify(data.result);
                $scope.postApprove(index + 1, 'MerchAdj');
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    $scope.quickApprovalModal = false;
                }

            })

        }
        $scope.loadDelinqAcct = function () {
            $timeout(function () {
                $scope.dtOptionsDelinqAcct = {
                    serverSide: true,
                    processing: true,
                    checkBox: false,
                    autoWidth: false,
                    "scrollX": true,
                    id: 'tblDelinqAcct',
                    ajax: $rootScope.getRootUrl() + '/Collection/GetPendingAcctCollection',
                    edit: {
                        level: 'scope',
                        func: 'indexDelinqAcctSelected'
                    },
                    "initComplete": function (settings, json) {
                    }
                }
            }, 300);
        }

        $scope.$on("indexDelinqAcctSelected", function (event, aData) {
            location.href = $rootScope.getRootUrl() + '/Collection#/CollectionFollowUp/' + aData[1] + '/' + aData[0];
        });



        $timeout(function () {
            $scope.dtWebProdRefListSelect = {
                serverSide: true,
                processing: true,
                "ordering": false,
                "searching": false,
                checkBox: false,
                autoWidth: false,
                "scrollX": true,
                ajax: $rootScope.getRootUrl() + '/GlobalVariables/WebUndefinedProdType',
                edit: {
                    level: 'scope',
                    func: 'indexPrdInfoSelected'
                },

            }
        }, 300);


        $scope.$on("indexPrdInfoSelected", function (event, aData) {
            var url;
            if ($rootScope.getRootUrl() == '') {
                url = location.origin + '/' + $rootScope.getRootUrl() + 'GlobalVariables#/productList/' + aData[0];
            } else {
                url = location.origin + $rootScope.getRootUrl() + '/' + 'GlobalVariables#/productList/' + aData[0];
            }
            window.open(url, '_blank');
            //  location.href = $rootScope.getRootUrl() + 'GlobalVariables#/productList/' + aData[0];
        });
    };
    var approvalController = function ($scope, $rootScope, $routeParams, $location, Api, Utils) {
        $scope.modalOpen = false;
        var aprId = $routeParams.aprId;
        var type = $routeParams.workflowcd;
        var AcctNo = $routeParams.acctNo;


        Api.getFormData({ Prefix: 'apr' }, $rootScope.getRootUrl() + '/Applications').success(function (data) {
            //append all selects here...
            $scope._Object = data.Model;
            $scope._Selects = data.Selects;
            console.log($scope._Object);
        });
        Api.WebMilestoneHistorySelect({ RefKey: aprId, workflowcd: type }).success(function (data) {
            $scope.approvals = data.result;
            $scope.currentUser = data.user.toLowerCase();
        });

        $scope.modalClick = function (item) {
            //var taskNo;
            $scope.selectedItem = item;
            $scope._Object.SelectedTaskNo = null;
            $scope.modalOpen = true;
            $scope._Object.selectedOwner = item.selectedOwner;

            Api.GetTaskNo({ CurrentTaskNo: item.SelectedTaskNo, workflowcd: type }).success(function (data) {
                $scope._Selects.TaskNo = data.result;
            });

            Api.ftMilestoneInfo({ SelectedTaskNo: item.SelectedTaskNo, aprId: aprId, workflowcd: type }).success(function (data) {
                $scope._Object = data.result;
                $scope._Object.ID = item.ID;
                console.log($scope._Object);
                //$scope._Object.workflowcd = type;
                //$scope._Object.Url = $scope._Object.Url.replace("#/", "");
                $scope._Object.Url = $scope._Object.Url.replace('/{{0}}/g', aprId);
                //console.log($scope._Object.Url.replace('{{$root.obj.applId}}', aprId));
            });
        };
        $scope.moreDetails = function (url) {
            alert(url);
        }
        $scope.moveToUrl = function () {
            $scope.modalOpen = false;
            var url;
            $scope._Object.aprId = aprId;
            if (type === 'Adj' || type === 'Pymt' || type == "MerchAdj") {
                // var url = $scope._Object.Url.replace(/{{0}}/g, AcctNo);
                url = $scope._Object.Url.replace(/\{0}/g, AcctNo);
            }
            else if (type === 'PKLPYMT') {
                var url = $location.absUrl().split("#")[0].replace('Approval', 'PukalAcct') + '#/viewBatch?batchId=' + AcctNo;
            }
            else {
                url = $scope._Object.Url.replace(/\{0}/g, aprId);
            }
            location.href = encodeURI(url);
        }
        $scope.saveWorkflow = function () {
            Utils.InfoNotify();
            $scope._Object.aprId = aprId;//$scope._Object.ApplId = applId;
            console.log($scope._Object);
            Api.SaveMilestone($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    // find the item by index and update the status
                    var index = $scope.approvals.indexOf($scope.selectedItem);
                    $scope.selectedItem.selectedStatus = $scope._Object.selectedStatus;
                    $scope.approvals[index] = $scope.selectedItem;

                    Api.WebMilestoneHistorySelect({ RefKey: aprId, workflowcd: type }).success(function (data) {
                        $scope.approvals = data.result;
                        $scope.currentUser = data.user.toLowerCase();
                    });

                }
            });
        };

        $scope.saveWorkflowAdj = function () {
            Utils.InfoNotify();
            $scope._Object.aprId = aprId;//$scope._Object.ApplId = applId;
            console.log($scope._Object);
            Api.SaveMilestoneAdj($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    // find the item by index and update the status
                    var index = $scope.approvals.indexOf($scope.selectedItem);
                    $scope.selectedItem.selectedStatus = $scope._Object.selectedStatus;
                    $scope.approvals[index] = $scope.selectedItem;

                    Api.WebMilestoneHistorySelect({ RefKey: aprId, workflowcd: type }).success(function (data) {
                        $scope.approvals = data.result;
                        $scope.currentUser = data.user.toLowerCase();
                    });
                }
            });
        };

        $scope.saveWorkflowPymt = function () {
            Utils.InfoNotify();
            $scope._Object.aprId = aprId;//$scope._Object.ApplId = applId;
            console.log($scope._Object);
            Api.SaveMilestonePayment($scope._Object).success(function (data) {
                Utils.finalResultNotify(data.result);
                if (data.result.flag != 1) {
                    $scope.modalOpen = false;
                    // find the item by index and update the status
                    var index = $scope.approvals.indexOf($scope.selectedItem);
                    $scope.selectedItem.selectedStatus = $scope._Object.selectedStatus;
                    $scope.approvals[index] = $scope.selectedItem;

                    Api.WebMilestoneHistorySelect({ RefKey: aprId, workflowcd: type }).success(function (data) {
                        $scope.approvals = data.result;
                        $scope.currentUser = data.user.toLowerCase();
                    });

                }
            });
        };

        $scope.Save = function myfunction() {
            if (type == "Adj") {
                $scope.saveWorkflowAdj();

            } else if (type == "Pymt") {
                $scope.saveWorkflowPymt();
            }
            else {
                $scope.saveWorkflow();
            }
        }
    };

    //inject service
    indexController.$inject = injectParam1;
    approvalController.$inject = injectParam2;

    angular.module('CardtrendApp').controller('indexController', indexController);
    angular.module('CardtrendApp').controller('approvalController', approvalController);
}());
﻿/// <reference path="D:\Petronas\FleetSys\FleetSys\FleetSys\Scripts/angular.min.js" />
angular.module('App.Utils', []).run(function ($rootScope, Utils) {
    $rootScope.getRootUrl = function () {
        return Utils.getRootUrl();
    }
}).factory('Utils', function ($http, $rootScope) {
    var stack_bottomright = { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 };
    return {
        getRootUrl: function () {
            var url = $('#hdUrlPrefix').val();
            return url;
        },
        InfoNotify: function () {
            $('button,input[type="button]"').not('.close').attr('disabled', 'disabled');
            $rootScope.notice = new PNotify({
                text: 'Processing action, please wait...',
                addclass: "stack-bottomright",
                type: 'info',
                stack: stack_bottomright,
                hide: false,
                styling: 'fontawesome'
            });
        },
        GetDataTable: function (tblName, scrollX, Url, func)
        {
            return {
                serverSide: true,
                processing: true,
                scrollX: scrollX,
                id: tblName,
                ajax: $rootScope.getRootUrl() + Url,
                edit: {
                    level: 'scope',
                    func: func,
                }
            };
        },
        GetDataTableWithCollumnDefs: function (tblName, scrollX, Url, func,columnDefs) {
            return {
                serverSide: true,
                processing: true,
                scrollX: scrollX,
                id: tblName,
                ajax: $rootScope.getRootUrl() + Url,
                edit: {
                    level: 'scope',
                    func: func,
                },
                aoColumnDefs: [columnDefs]
            };
        },
        finalResultNotify: function (obj) {
            $('button,input[type="button]"').removeAttr('disabled');
            if ($rootScope.notice) {
                var title = $rootScope.notice.options.isLongRunning ? " " : false;
                $rootScope.notice.update({
                    type: obj.flag == 0 ? 'success' : 'error',
                    text: obj.desp,
                    hide: true,
                    title: title
                });
            } else {
                return this.PNotify(obj);
            }
        },
        PNotify: function (obj, releaseDisabled) {
            new PNotify({
                type: obj.flag == 0 ? 'success' : obj.flag == 2 ? 'warning' : 'error',
                text: obj.Descp,
                addclass: "stack-bottomright",
                stack: stack_bottomright,
                styling: 'fontawesome'
            });
        },
        makeObjectNull: function (obj, overrides) {
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    if (overrides[key]) {
                        obj[key] = overrides[key];
                    } else {
                        obj[key] = null;
                    }
                }
            }
            for (var key in overrides) {
                if (!obj.hasOwnProperty(key)) {
                    obj[key] = overrides[key];
                }
            }
            return obj;
        },
        getSelectedRow: function (dt) {
            var obj, self = this;
            dt.find('tr').each(function (index, $row) {
                if ($(this).hasClass('active')) {
                    obj = dt.fnGetData($row);

                }
            });
            if (obj)
                return obj;
            self.PNotify({ flag: 1, Descp: "0 row selected" });
            return null;
        },
        getSelectedRows: function (dt) {
            var obj, self = this;
            var rows = [];
            dt.find('tr').each(function (index, $row) {
                if ($(this).hasClass('active')) {
                    rows.push(dt.fnGetData($row));
                }
            });
            if (rows.length)
                return rows;
            self.PNotify({ flag: 1, Descp: "0 row selected" });
            return null;
        },
        updateDataTable: function (dt, newOptions) {
            var objSettings = dt.fnSettings();
            angular.extend(objSettings, newOptions);
        },
        isDateGreaterthanToday: function (_dt) {
            var x = __dt.split('/');
        },
        generateBinSequence: function (sum) {
            var count = 20;
            var bin = [1];
            for (i = 1; i < count; i++) {
                num = bin[bin.length - 1] * 2;
                if (num > sum)
                    break;
                bin.push(num);
            }
            return bin;
        },
        Decimal2Bin: function (dec) {
            return (dec >>> 0).toString(2);
        },
        findBinarySequence: function (val) {
            var bin = this.generateBinSequence(val).reverse();
            var num = 0;
            var nums = [];
            var binArray = this.Decimal2Bin(val).split("");
            _.each(binArray, function (item, index) {
                if (item == "1") {
                    nums.push(bin[index]);
                }
            });
            return nums;
        },
        getEventMap: function () {
            return [
            { id: 1, type: "PeriodType", placeholder: "", val: null, class: 'col-sm-6', dataType: 'x' },
            { id: 2, type: "PeriodInterval", placeholder: "Interval", val: null, class: 'col-sm-6', dataType: 'x' },
            { id: 4, type: "MinIntVal", placeholder: "Min Value", val: null, class: 'col-sm-6', dataType: 'int' },
            { id: 8, type: "MaxIntVal", placeholder: "Max Value", val: null, class: 'col-sm-6', dataType: 'int' },
            { id: 16, type: "MinMoneyVal", placeholder: "Min Money", val: null, class: 'col-sm-6', dataType: 'money' },
            { id: 32, type: "MaxMoneyVal", placeholder: "Max Money", val: null, class: 'col-sm-6', dataType: 'money' },
            { id: 64, type: "MinDateVal", placeholder: "Min Date", val: null, class: 'col-sm-6', dataType: 'datePicker' },
            { id: 128, type: "MaxDateVal", placeholder: "Max Date", val: null, class: 'col-sm-6', dataType: 'datePicker' },
            { id: 256, type: "MinTimeVal", placeholder: "Min Time", val: null, class: 'col-sm-6', dataType: 'timePicker' },
            { id: 512, type: "MaxTimeVal", placeholder: "Max Time", val: null, class: 'col-sm-6', dataType: 'timePicker' },
            { id: 1024, type: "VarCharVal", placeholder: "Char Value", val: null, class: 'col-sm-12', dataType: 'string' }
            ]
        }
    }
})
.directive('routingMenu', function ($compile, $timeout) {
        return {
            restrict: 'A',
            link: function (scope, $element, attrs) {

                scope.$on('routeChanged', function (data, val) {
                    $(".pagekey").removeClass("active");
                    $(".pagekey_" + val).addClass('active');
                })
                $link = $element.find('.action > ul > li > a');
                $element.find('ul li a').on('click', function () {
                    var self = $(this);
                    $element.find('ul li a').each(function () {
                        $(this).removeClass('active');
                    });
                    self.addClass('active');
                });
            }
        }
    })
.directive("onlyNumber", function () {
         return {
             restrict: "A",
             link: function (scope, element, attr) {
                 element.bind('input', function () {
                     var position = this.selectionStart - 1;

                     //remove all but number and .
                     var fixed = this.value.replace(/[^0-9\.]/g, '');  
                     if (fixed.charAt(0) === '.')                  //can't start with .
                         fixed = fixed.slice(1);

                     var pos = fixed.indexOf(".") + 1;
                     if (pos >= 0)               //avoid more than one .
                         fixed = fixed.substr(0, pos) + fixed.slice(pos).replace('.', '');  

                     if (this.value !== fixed) {
                         this.value = fixed;
                         this.selectionStart = position;
                         this.selectionEnd = position;
                     }
                 });
             }
         };
     })
.directive('datePicker', function ($compile, $timeout) {
        return {
            restrict: 'A',
            scope: { element: '=element' },
            link: function (scope, $element, attrs) {
                //$element.datetimepicker({
                //    format: 'd/m/Y',
                //    timepicker: false,
                //});
                $element.datepicker({
                    format: "dd/mm/yyyy",
                    todayBtn: 'linked',
                    autoclose: true,
                    todayHighlight: true
                });
            }
        }
    })
.directive('onlyDigits', function () {
         return {
             require: 'ngModel',
             restrict: 'A',
             link: function (scope, element, attr, ctrl) {
                 function inputValue(val) {
                     if (val) {
                         var digits = val.replace(/[^0-9]/g, '');

                         if (digits !== val) {
                             ctrl.$setViewValue(digits);
                             ctrl.$render();
                         }
                         return parseInt(digits,10);
                     }
                     return undefined;
                 }            
                 ctrl.$parsers.push(inputValue);
             }
         };
     })
.directive("limitTo", [function() {
            return {
                restrict: "A",
                link: function(scope, elem, attrs) {
                    var limit = parseInt(attrs.limitTo);
                    angular.element(elem).on("keypress", function(e) {
                        if (this.value.length == limit) e.preventDefault();
                    });
                }
            }
        }])
.directive('onlyTime', function () {
            return {
                require: 'ngModel',
                restrict: 'A',
                link: function (scope, element, attr, ctrl) {
                    function inputValue(val) {
                        if (val) {
                            var digits = val.replace(/^(?:2[0-3]|[01]?[0-9]):[0-5][0-9]:[0-5][0-9]$/, '');  //!@#$%^&*()_+\-=\[\]{};'"\\|,.<>\/?

                            if (digits !== val) {
                                ctrl.$setViewValue(digits);
                                ctrl.$render();
                            }
                            return digits
                        }
                        return undefined;
                    }
                    ctrl.$parsers.push(inputValue);
                }
            };
        })
.directive('appModal', function ($compile, $timeout) {
    return {
        scope: { trigger: '=trigger' },
        restrict: 'AE',
        link: function (scope, $element, attrs) {
            $element.on('hidden.bs.modal', function (e) {
                scope.trigger = false;
            })
            scope.$watch('trigger', function (newVal) {
                if (newVal) {
                    $element.modal({ 'show': true, keyboard: true, backdrop: 'static' });
                } else {
                    $element.modal('hide');
                }
            });
            $element.draggable({ handle: ".modal-head" });
        }
    }
})
.directive('confirmAction', function ($compile, $timeout) {
    return {
        scope: { trigger: '=trigger' },
        restrict: 'AE',
        link: function (scope, $element, attrs) {
            $element.on('hidden.bs.modal', function (e) {
                scope.trigger = false;
            })
            scope.$watch('trigger', function (newVal) {
                if (newVal) {
                    $element.modal({ 'show': true, keyboard: true, backdrop: 'static' });
                } else {
                    $element.modal('hide');
                }
            });
            $element.draggable({ handle: ".modal-head" });
            $element.find('btnConfirm').on('click', function () {
                scope.$eval(attrs.execute);
            })
        }
    }
})
.directive('validationForm', function ($compile, $timeout, $rootScope) {
    return {
        restrict: 'EA',
        link: function (scope, $element, attrs) {
            var $form = $element.closest('form');
            $element.on('click', function (e) {
                e.preventDefault();
                $.validator.unobtrusive.parse($form);
                if ($form.valid()) {
                    scope.$eval(attrs.customsubmit);
                    scope.$apply();
                } else {
                    new PNotify({
                        text: 'Validation errors, please verify your inputs',
                        addclass: "stack-bottomright",
                        stack: { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 },
                        type: 'error',
                        styling: 'fontawesome'
                    });
                }
            });
        }
    }
})
.filter('doubleQuote', function () {
    return function (input) {
        input = '"' + input.replace(/^"*|"*$/, '') + '"';
        return input;
    }
})
.directive('dtable', function ($compile, $timeout, $rootScope) {
    return {
        restrict: 'AE',
        scope: { options: '=' },
        link: function (scope, $element, attrs) {
            $.fn.dataTable.ext.legacy.ajax = true;
            /* Set the defaults for DataTables initialisation */
            $.extend(!0, $.fn.dataTable.defaults, {
                sDom: "<'row'<'col-xs-6'l><'col-xs-6'f>r>t<'row'<'col-xs-6'i><'col-xs-6'p>>",
                oLanguage: {
                    sLengthMenu: "_MENU_ records per page"
                }
            });
            $.extend($.fn.dataTableExt.oStdClasses, {
                sWrapper: "dataTables_wrapper form-inline",
                sFilterInput: "form-control input-sm",
                sLengthSelect: "form-control input-sm"
            });
            if ($.fn.dataTable.Api) {
                $.fn.dataTable.defaults.renderer = "bootstrap";
                $.fn.dataTable.ext.renderer.pageButton.bootstrap = function (e, t, n, r, i, s) {
                    var o = new $.fn.dataTable.Api(e),
                        u = e.oClasses,
                        a = e.oLanguage.oPaginate,
                        f, l, c = function (t, r) {
                            var h, p, d, v, m = function (e) {
                                e.preventDefault();
                                e.data.action !== "ellipsis" && o.page(e.data.action).draw(!1)
                            };
                            for (h = 0, p = r.length; h < p; h++) {
                                v = r[h];
                                if ($.isArray(v)) c(t, v);
                                else {
                                    f = "";
                                    l = "";
                                    switch (v) {
                                        case "ellipsis":
                                            f = "&hellip;";
                                            l = "disabled";
                                            break;
                                        case "first":
                                            f = a.sFirst;
                                            l = v + (i > 0 ? "" : " disabled");
                                            break;
                                        case "previous":
                                            f = a.sPrevious;
                                            l = v + (i > 0 ? "" : " disabled");
                                            break;
                                        case "next":
                                            f = a.sNext;
                                            l = v + (i < s - 1 ? "" : " disabled");
                                            break;
                                        case "last":
                                            f = a.sLast;
                                            l = v + (i < s - 1 ? "" : " disabled");
                                            break;
                                        default:
                                            f = v + 1;
                                            l = i === v ? "active" : ""
                                    }
                                    if (f) {
                                        d = $("<li>", {
                                            "class": u.sPageButton + " " + l,
                                            "aria-controls": e.sTableId,
                                            tabindex: e.iTabIndex,
                                            id: n === 0 && typeof v == "string" ? e.sTableId + "_" + v : null
                                        }).append($("<a>", {
                                            href: "#"
                                        }).html(f)).appendTo(t);
                                        e.oApi._fnBindAction(d, {
                                            action: v
                                        }, m)
                                    }
                                }
                            }
                        };
                    c($(t).empty().html('<ul class="pagination"/>').children("ul"), r)
                }
            } else {
                $.fn.dataTable.defaults.sPaginationType = "bootstrap";
                $.fn.dataTableExt.oApi.fnPagingInfo = function (e) {
                    return {
                        iStart: e._iDisplayStart,
                        iEnd: e.fnDisplayEnd(),
                        iLength: e._iDisplayLength,
                        iTotal: e.fnRecordsTotal(),
                        iFilteredTotal: e.fnRecordsDisplay(),
                        iPage: e._iDisplayLength === -1 ? 0 : Math.ceil(e._iDisplayStart / e._iDisplayLength),
                        iTotalPages: e._iDisplayLength === -1 ? 0 : Math.ceil(e.fnRecordsDisplay() / e._iDisplayLength)
                    }
                };
                $.extend($.fn.dataTableExt.oPagination, {
                    bootstrap: {
                        fnInit: function (e, t, n) {
                            var r = e.oLanguage.oPaginate,
                                i = function (t) {
                                    t.preventDefault();
                                    e.oApi._fnPageChange(e, t.data.action) && n(e)
                                };
                            $(t).append('<ul class="pagination"><li class="prev disabled"><a href="#">&larr; ' + r.sPrevious + "</a></li>" + '<li class="next disabled"><a href="#">' + r.sNext + " &rarr; </a></li>" + "</ul>");
                            var s = $("a", t);
                            $(s[0]).bind("click.DT", {
                                action: "previous"
                            }, i);
                            $(s[1]).bind("click.DT", {
                                action: "next"
                            }, i)
                        },
                        fnUpdate: function (e, t) {
                            var n = 5,
                                r = e.oInstance.fnPagingInfo(),
                                i = e.aanFeatures.p,
                                s, o, u, a, f, l, c = Math.floor(n / 2);
                            if (r.iTotalPages < n) {
                                f = 1;
                                l = r.iTotalPages
                            } else if (r.iPage <= c) {
                                f = 1;
                                l = n
                            } else if (r.iPage >= r.iTotalPages - c) {
                                f = r.iTotalPages - n + 1;
                                l = r.iTotalPages
                            } else {
                                f = r.iPage - c + 1;
                                l = f + n - 1
                            }
                            for (s = 0, o = i.length; s < o; s++) {
                                $("li:gt(0)", i[s]).filter(":not(:last)").remove();
                                for (u = f; u <= l; u++) {
                                    a = u == r.iPage + 1 ? 'class="active"' : "";
                                    $("<li " + a + '><a href="#">' + u + "</a></li>").insertBefore($("li:last", i[s])[0]).bind("click", function (n) {
                                        n.preventDefault();
                                        e._iDisplayStart = (parseInt($("a", this).text(), 10) - 1) * r.iLength;
                                        t(e)
                                    })
                                }
                                r.iPage === 0 ? $("li:first", i[s]).addClass("disabled") : $("li:first", i[s]).removeClass("disabled");
                                r.iPage === r.iTotalPages - 1 || r.iTotalPages === 0 ? $("li:last", i[s]).addClass("disabled") : $("li:last", i[s]).removeClass("disabled")
                            }
                        }
                    }
                })
            }
            if ($.fn.DataTable.TableTools) {
                $.extend(!0, $.fn.DataTable.TableTools.classes, {
                    container: "DTTT btn-group",
                    buttons: {
                        normal: "btn btn-default",
                        disabled: "disabled"
                    },
                    collection: {
                        container: "DTTT_dropdown dropdown-menu",
                        buttons: {
                            normal: "",
                            disabled: "disabled"
                        }
                    },
                    print: {
                        info: "DTTT_print_info modal"
                    },
                    select: {
                        row: "active"
                    }
                });
                $.extend(!0, $.fn.DataTable.TableTools.DEFAULTS.oTags, {
                    collection: {
                        container: "ul",
                        button: "li",
                        liner: "a"
                    }
                })
            };

            function mixIt(Scopeoptions) {

                var defaults = {
                    "info": true,
                    "lengthChange": true,
                    "scrollX": true,
                    paging: true,
                    "searching": true,
                    searchable: true,
                    pageLength: 25,
                    "dom": 'C<"clear">lfrtip',
                    checkBox: false,
                    oLanguage: {
                        sEmptyTable: '<i style="font-size:140px;color:#eeeeee" class="fa fa-ban"></i>'
                    }
                };
                var options = {};
                $.extend(options, $.extend({}, defaults, Scopeoptions));
                scope.options.id = options.id;
                options.fnRowCallback = function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    var $nRow = $(nRow);
                    var self = this;

                    $nRow.on('click', function () {
                        if (options.edit) {
                            $nRow.closest('tbody').find('tr').removeClass('active');
                            $('#' + options.id + '-options').show();
                            if (options.checkBox) {
                                $nRow.closest('tbody').find('tr').each(function (index, $elm) {
                                    var x = $($elm).find('td:first-child').find('input').prop('checked', false);
                                });
                            }
                            $nRow.removeClass('active').addClass('active');
                        }
                        if (options.rowClick)
                            options.rowClick.call(this, aData, nRow);
                    });

                    $nRow.on('dblclick', function () {
                        if (Scopeoptions.edit)
                            scope.$emit(Scopeoptions.edit.func, aData);
                    });

                    if (options.rowCallback) {
                        options.rowCallback.call(self, aData, nRow);
                    }
                    if (options.childTable) {
                        $nRow.find('td:first').on('click', function () {
                            $nRow.closest('tbody').find('tr').not($nRow).each(function () {
                                var $row = $(this);
                                if ($row.hasClass('shown')) {
                                    $row.removeClass('shown');
                                    $row.next().remove();
                                    $row.closest('tbody').find('tr.dynamic-created').remove();
                                }
                            });
                            var $tbl = $rootScope.tables[options.id];
                            var row = $nRow.next().find('.childtable').length;
                            if (!row) {
                                $nRow.addClass('shown');
                                var $tbl = $(options.childTable.format(aData, options));//aaData options
                                if (options.childTable.edit) {
                                    $tbl.find('tbody tr').on('click', function () {
                                        var self = this, $row = $(self), cells = [];
                                        $tbl.find('tr td').each(function () {
                                            $(this).removeClass('active');
                                        });
                                        $row.find('td').each(function (index, item) {
                                            $(this).removeClass().addClass('active');
                                        });
                                        $row.find('td').each(function (index, item) {
                                            cells.push(item.innerText);
                                        });
                                        options.childTable.edit.func.call(self, cells);
                                    });
                                }
                                $nRow.after($tbl);
                            } else {
                                $nRow.removeClass('shown');
                                var __ex = false;
                                if ($nRow.closest('tbody').find('tr.dynamic-created').length)
                                    $nRow.closest('tbody').find('tr.dynamic-created').remove();
                            }
                        });
                    }
                };
                options.fnCreatedRow = function (nRow, aData, iDataIndex) {
                    if (options.checkBox) {
                        var $input = $('<input type="checkbox"/>');
                        $input.on('click', function (event) {
                            var checked = $input.prop('checked');
                            $(nRow).closest('tbody').find('tr').each(function (index, $elm) {
                                //  var x = $($elm).find('td:first-child').find('input').prop('checked', false);
                            });
                            $input.prop('checked', checked);
                            if (checked) {
                                $('#' + options.id + '-options').show();
                                // $(nRow).closest('tbody').find('tr').removeClass('active');
                                event.stopPropagation();
                                $(nRow).removeClass('active').addClass('active');
                            } else {
                                $(nRow).removeClass('active');
                                event.stopPropagation();
                            }
                        });
                        $('td:first', nRow).html($input);
                    }
                    if (options.createdRow) {
                        options.createdRow.call(this, nRow, aData, iDataIndex);
                    }
                }
                options.fnInitComplete = function (oSettings, json) {
                    if (options.checkBox) {
                        var $table = $rootScope.tables[options.id];
                        var $input = $table.closest('.dataTables_scroll').find('.dataTables_scrollHead').find('input');
                        $input.on('click', function (event) {
                            if ($input.is(':checked')) {
                                $table.find('tbody tr').each(function () {
                                    $(this).find('input').trigger('click');
                                    if (!$(this).find('input').is(':checked'))
                                        $(this).find('input').trigger('click');
                                })
                            }
                            else {
                                $table.find('tbody tr').each(function () {
                                    $(this).find('input').trigger('click');
                                    if ($(this).find('input').is(':checked'))
                                        $(this).find('input').trigger('click');
                                })
                            }
                        });
                    }
                }
                return options;
            }
            scope.$watch('options', function (newVal) {
                if (newVal) {
                    $timeout(function () {
                        var options = mixIt(scope.options);
                        $rootScope.tables[options.id] = $element.on('xhr.dt', function (e, settings, json) {
                            if (options.childTable) {
                                options.childTable.fngroupOp.call(this, e, settings, json, options);
                            }
                            if (options.xhrDone) {
                                options.xhrDone.call(this, e, settings, json);
                            }
                        }).dataTable(options);
                    });
                }
            }, true);
            scope.$on('updateDataTable', function (data, val) {
                if (scope.options && val.options.id == scope.options.id) {
                    var tbl = $rootScope.tables[val.options.id];
                    tbl.fnDestroy();
                    var options = mixIt(val.options);
                    $rootScope.tables[options.id] = $element.on('xhr.dt', function (e, settings, json) {
                        if (options.childTable) {
                            options.childTable.fngroupOp.call(this, e, settings, json);
                        }
                        if (options.xhrDone) {
                            options.xhrDone.call(this, e, settings, json);
                        }
                    }).dataTable(options);
                    data.preventDefault();
                }
            });
        }
    }
})    
.directive('amount',['$filter', function ($filter) {
        return {
            require: '?ngModel',
            link: function (scope, elem, attrs, ctrl) {
                if (!ctrl) return;
              
                ctrl.$parsers.unshift(function (viewValue) {
                  
                    elem.priceFormat({
                                        prefix: '',
                                        centsSeparator: '.',
                                        thousandsSeparator: ','
                    });

                    var removeCommaResult = elem[0].value.replace(/,/g, '');
                    return removeCommaResult;

                });
            }
   };
}])
.directive('amount3decimal', ['$filter', function ($filter) {
        return {
            require: '?ngModel',
            link: function (scope, elem, attrs, ctrl) {
                if (!ctrl) return;

                ctrl.$parsers.unshift(function (viewValue) {

                    elem.priceFormat({
                        prefix: '',
                        centsSeparator: '.',
                        centsLimit:3,
                        thousandsSeparator: ','
                    });

                    var removeCommaResult = elem[0].value.replace(/,/g, '');
                    return removeCommaResult;

                });
            }
        };
    }])
.directive('dataTableInputX', function ($compile) {
    return {
        restrict: 'AE',
        scope: { options: '@' },
        link: function (scope, $element, attrs) {
            scope.options.fnCreatedRow = function (nRow, aData, iDataIndex) {
                // Bold the grade for all 'A' grade browsers
                var $input = $('<input type="checkbox"/>');
                $input.on('click', function (event) {
                    alert();
                    var checked = $input.prop('checked');
                    $(nRow).closest('tbody').find('tr').each(function (index, $elm) {
                        var x = $($elm).find('td:first-child').find('input').prop('checked', false);
                    });
                    $input.prop('checked', checked);
                    if (checked) {
                        $(nRow).closest('tbody').find('tr').removeClass('active');
                        event.stopPropagation();
                        $(nRow).removeClass('active').addClass('active');
                    } else {
                        $(nRow).removeClass('active');
                        event.stopPropagation();
                    }
                });
                $('td:first', nRow).html($input);
            }
            $element.dataTable($scope.options);
        }
    }

})
.directive("searchForm", function ($compile) {
    return {
        restrict: 'AE',
        scope: {},
        link: function (scope, $element, attrs) {

            $.widget("custom.catcomplete", $.ui.autocomplete, {
                _renderMenu: function (ul, items) {
                    var self = this,
                        currentCategory = "";
                    $.each(items, function (index, item) {
                        self._renderItemData(ul, item);
                    });
                }
            });
            var CurrentValue;
            var autoCompleteModel = function () {
                this.minLength = 4;
                this.currentPrefix = null;
                this.determinePost = function (term) {
                    var self = this;
                    if (term.length < 3)
                        return false;
                    var prefix = term.substring(0, 3).toLowerCase();
                    var qstr = term.substr(3, term.length - 1);
                    for (i = 0; i < this.prefixes.length; i++) {
                        if (this.prefixes[i].shortCode == prefix) {
                            if (qstr.length >= this.prefixes[i].minChars) {
                                self.currentPrefix = prefix;
                                return true;
                            } else {
                                return false;
                            }
                        }
                    }
                }
                this.prefixes = [
                    { shortCode: "cac", minChars: 1 }, { shortCode: "crd", minChars: 1 }, { shortCode: "nam", minChars: 1 }, { shortCode: "nic", minChars: 1 }, { shortCode: "oic", minChars: 1 },
                    { shortCode: "pas", minChars: 1 }, { shortCode: "pye", minChars: 1 }, { shortCode: "tax", minChars: 1 }, { shortCode: "apl", minChars: 1 }, { shortCode: "apc", minChars: 1 },
                { shortCode: "apr", minChars: 1 }, { shortCode: "co1", minChars: 1 }, { shortCode: "co2", minChars: 1 }, { shortCode: "cor", minChars: 1 }, { shortCode: "vrn", minChars: 1 }
                , { shortCode: "psn", minChars: 1 }, { shortCode: "mid", minChars: 1 }, { shortCode: "mac", minChars: 1 }, { shortCode: "bsn", minChars: 1 }, { shortCode: "sub", minChars: 1 },
                { shortCode: "mdt", minChars: 1 }, { shortCode: "sid", minChars: 1 }, { shortCode: "apn", minChars: 1 }, { shortCode: "crn", minChars: 1 }],
                this.source = function (request, response) {
                    var x = $('#hdUrlPrefix').val();
                    if (!objAutoComplete.determinePost(request.term))
                        return;
                    $.ajax({
                        url: $('#hdUrlPrefix').val() + '/Repository/_Querymeta',
                        dataType: "json",
                        cache: false,
                        data: {
                            name_startsWith: request.term
                        },
                        success: function (data) {
                            response($.map(data.theResult, function (item) {
                                return {
                                    value: item.Link,
                                    desc: { match: item.MatchedValue, more: item.Descp, dest: item.Dest, prefix: request.term }
                                }
                            }));
                        }
                    });
                },
                this.open = function () {
                    $('.ui-autocomplete').css('z-index', '3000');
                    $('.ui-autocomplete').css('width', '450');
                    $('.ui-autocomplete').css('max-height', '500');
                    return false;
                },
                this.focus = function (event, ui) {
                    //   $(this).val(objAutoComplete.currentPrefix + ui.item.desc.match);
                    $('.autocomplete-match-desc').css('color', '#797979');
                    $('.ui-menu-item').find('.ui-state-focus').find('.autocomplete-match-desc').css('color', 'white');
                    return false;
                },
                this.select = function (event, ui) {
                    var prefix = $('#hdUrlPrefix').val();
                    $(this).val(ui.item.desc.match);

                    scope.$emit('SearchItemSelected', ui);

                    return false;
                },
                this.renderItemData = function (ul, item) {
                    //if (item.desc == "Show More") {
                    //    return $("<li></li>").data("item.autocomplete", item).append(
                    //        '<a style="padding-left:70px;background-color:#DFDFDF"><span>Show More results for  <strong>"' + CurrentValue + '"</strong></span></a>')
                    //        .appendTo(ul);owa.edenowa
                    //}
                    //else {
                    return $("<li></li>").data("item.autocomplete", item).append(
                                        '<a style="border-bottom:1px solid #f3f3f3"><span class="autocomplete-match">' + item.desc.match + '</span> <span class="autocomplete-match-desc">' + item.desc.more + '</span></a>')
                                        .appendTo(ul);
                    //}

                    //$('.ui-autocomplete').css('z-index', '3000');
                };

            }
            var objAutoComplete = new autoCompleteModel();

            $element.find('input').val('').catcomplete({
                minLength: objAutoComplete.minLength,
                source: objAutoComplete.source,
                open: objAutoComplete.open,
                focus: objAutoComplete.focus,
                autoFocus: true,
                select: objAutoComplete.select
            }).data("custom-catcomplete")._renderItemData = objAutoComplete.renderItemData;

            $element.find('.selector li a').on('click', function () {
                $element.find('input').val($(this).find('span').text()).focus();
            });



        }
    }

})
.directive("autocompleteForm", function ($compile) {
    return {
        restrict: 'AE',
        scope: {},
        link: function (scope, $element, attrs) {
            alert(attrs.autocompleteForm);
            $.widget("custom.catcomplete", $.ui.autocomplete, {
                _renderMenu: function (ul, items) {
                    var self = this,
                        currentCategory = "";
                    $.each(items, function (index, item) {
                        self._renderItemData(ul, item);
                    });
                }
            });
            var CurrentValue;
            var autoCompleteModel = function () {
                this.minLength = attrs.minLength;
                this.currentPrefix = null;

                this.source = function (request, response) {
                    var x = $('#hdUrlPrefix').val();
                    $.ajax({
                        url: $('#hdUrlPrefix').val() + attrs.autocompleteForm,
                        dataType: "json",
                        cache: false,
                        data: {
                            name_startsWith: request.term
                        },
                        success: function (data) {
                            response($.map(data.theResult, function (item) {
                                return {
                                    value: item.Text,
                                    desc: { match: item.MatchedValue, more: item.Descp, dest: item.Dest, prefix: request.term }
                                }
                            }));
                        }
                    });
                },
                this.open = function () {
                    $('.ui-autocomplete').css('z-index', '3000');
                    $('.ui-autocomplete').css('width', '450');
                    $('.ui-autocomplete').css('max-height', '500');
                    return false;
                },
                this.focus = function (event, ui) {
                    //   $(this).val(objAutoComplete.currentPrefix + ui.item.desc.match);
                    $('.autocomplete-match-desc').css('color', '#797979');
                    $('.ui-menu-item').find('.ui-state-focus').find('.autocomplete-match-desc').css('color', 'white');
                    return false;
                },
                this.select = function (event, ui) {
                    var prefix = $('#hdUrlPrefix').val();
                    $(this).val(ui.item.desc.match);

                    scope.$emit('SearchItemSelected', ui);

                    return false;
                },
                this.renderItemData = function (ul, item) {
                    //if (item.desc == "Show More") {
                    //    return $("<li></li>").data("item.autocomplete", item).append(
                    //        '<a style="padding-left:70px;background-color:#DFDFDF"><span>Show More results for  <strong>"' + CurrentValue + '"</strong></span></a>')
                    //        .appendTo(ul);owa.edenowa
                    //}
                    //else {
                    return $("<li></li>").data("item.autocomplete", item).append(
                                        '<a style="border-bottom:1px solid #f3f3f3"><span class="autocomplete-match">' + item.desc.match + '</span> <span class="autocomplete-match-desc">' + item.desc.more + '</span></a>')
                                        .appendTo(ul);
                    //}

                    //$('.ui-autocomplete').css('z-index', '3000');
                };

            }
            var objAutoComplete = new autoCompleteModel();

            $element.catcomplete({
                minLength: objAutoComplete.minLength,
                source: objAutoComplete.source,
                open: objAutoComplete.open,
                focus: objAutoComplete.focus,
                autoFocus: true,
                select: objAutoComplete.select
            }).data("custom-catcomplete")._renderItemData = objAutoComplete.renderItemData;

            $element.find('.selector li a').on('click', function () {
                $element.find('input').val($(this).find('span').text()).focus();
            });



        }
    }

})
.directive("autocompleteAccno", function ($compile) {
    return {
        restrict: 'AE',
        scope: {},
        link: function (scope, $element, attrs) {

            $.widget("custom.catcomplete", $.ui.autocomplete, {
                _renderMenu: function (ul, items) {
                    var self = this,
                        currentCategory = "";
                    $.each(items, function (index, item) {
                        self._renderItemData(ul, item);
                    });
                }
            });
            var CurrentValue;
            var autoCompleteModel = function () {
               // this.minLength = 4;
                this.currentPrefix = null;
                this.determinePost = function (term) {
                    var self = this;
                    //if (term.length < 3)
                    //    return false;
                    var prefix = 'cac';// term.substring(0, 3).toLowerCase();
                    var qstr = term;//.substr(3, term.length - 1);
                    for (i = 0; i < this.prefixes.length; i++) {
                        if (this.prefixes[i].shortCode == prefix) {
                            if (qstr.length >= this.prefixes[i].minChars) {
                                self.currentPrefix = prefix;
                                return true;
                            } else {
                                return false;
                            }
                        }
                    }
                }
                this.prefixes = [
                    { shortCode: "cac", minChars: 1 }, { shortCode: "crd", minChars: 1 }, { shortCode: "nam", minChars: 1 }, { shortCode: "nic", minChars: 1 }, { shortCode: "oic", minChars: 1 },
                    { shortCode: "pas", minChars: 1 }, { shortCode: "pye", minChars: 1 }, { shortCode: "tax", minChars: 1 }, { shortCode: "apl", minChars: 1 }, { shortCode: "apc", minChars: 1 },
                { shortCode: "apr", minChars: 1 }, { shortCode: "co1", minChars: 1 }, { shortCode: "co2", minChars: 1 }, { shortCode: "cor", minChars: 1 }, { shortCode: "vrn", minChars: 1 }
                , { shortCode: "psn", minChars: 1 }, { shortCode: "mid", minChars: 1 }, { shortCode: "mac", minChars: 1 }, { shortCode: "bsn", minChars: 1 }, { shortCode: "sub", minChars: 1 },
                { shortCode: "mdt", minChars: 1 }, { shortCode: "sid", minChars: 1 }, { shortCode: "apn", minChars: 1 },{ shortCode: "crn", minChars: 1 }],
                this.source = function (request, response) {
                    var x = $('#hdUrlPrefix').val();
                    if (!objAutoComplete.determinePost(request.term))
                        return;
                    $.ajax({
                        url: $('#hdUrlPrefix').val() + '/Repository/_Querymeta',
                        dataType: "json",
                        cache: false,
                        data: {
                            name_startsWith:'cac'+ request.term
                        },
                        success: function (data) {
                            response($.map(data.theResult, function (item) {
                                return {
                                    value: item.Link,
                                    desc: { match: item.MatchedValue, more: item.Descp, dest: item.Dest, prefix: request.term }
                                }
                            }));
                        }
                    });
                },
                this.open = function () {
                    $('.ui-autocomplete').css('z-index', '3000');
                    $('.ui-autocomplete').css('width', '450');
                    $('.ui-autocomplete').css('max-height', '500');
                    return false;
                },
                this.focus = function (event, ui) {
                    //   $(this).val(objAutoComplete.currentPrefix + ui.item.desc.match);
                    $('.autocomplete-match-desc').css('color', '#797979');
                    $('.ui-menu-item').find('.ui-state-focus').find('.autocomplete-match-desc').css('color', 'white');
                    return false;
                },
                this.select = function (event, ui) {
                    var prefix = $('#hdUrlPrefix').val();
                    $(this).val(ui.item.desc.match);

                    scope.$emit('SearchItemSelected', ui);

                    return false;
                },
                this.renderItemData = function (ul, item) {
                    //if (item.desc == "Show More") {
                    //    return $("<li></li>").data("item.autocomplete", item).append(
                    //        '<a style="padding-left:70px;background-color:#DFDFDF"><span>Show More results for  <strong>"' + CurrentValue + '"</strong></span></a>')
                    //        .appendTo(ul);owa.edenowa
                    //}
                    //else {
                    return $("<li></li>").data("item.autocomplete", item).append(
                                        '<a style="border-bottom:1px solid #f3f3f3"><span class="autocomplete-match">' + item.desc.match + '</span> <span class="autocomplete-match-desc">' + item.desc.more + '</span></a>')
                                        .appendTo(ul);
                    //}

                    //$('.ui-autocomplete').css('z-index', '3000');
                };

            }
            var objAutoComplete = new autoCompleteModel();

            $element.find('input').val('').catcomplete({
                minLength: objAutoComplete.minLength,
                source: objAutoComplete.source,
                open: objAutoComplete.open,
                focus: objAutoComplete.focus,
                autoFocus: true,
                select: objAutoComplete.select
            }).data("custom-catcomplete")._renderItemData = objAutoComplete.renderItemData;

            $element.find('.selector li a').on('click', function () {
                $element.find('input').val($(this).find('span').text()).focus();
            });



        }
    }

})
.directive('ngConfirmClick', [function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                var message = attrs.ngConfirmMessage;
                if (message && confirm(message)) {
                    scope.$apply(attrs.ngConfirmClick);
                }
            });
        }
    }
}])
.config(function ($provide, $httpProvider) {
    var rootUrl = $('#hdUrlPrefix').val();

    $provide.factory('CustomHttpInterceptor', function ($q, $rootScope, $window) {
        return {
            timer: null,
            request: function (config) {
                if (config.method == 'POST' || config.method == 'PUT') {
                    this.timer = setTimeout(function () {
                        if ($rootScope.notice) {
                            $rootScope.notice.update({
                                type: 'info',
                                text: "<p>Server is taking longer than usual to <strong>respond</strong>. You can ignore this message and continue to wait for the response or refresh this page again.</p><p class='text-center'><button onclick='location.reload()' class='btn btn-white btn-sm'><i class='fa fa-refresh'></i>&nbsp;Refresh this page</button></p>",
                                title: 'Uh oh',
                                isLongRunning: true
                            });
                        }
                    }, 60000);
                }
                return config || $q.when(config);
            },
            requestError: function (rejection) {
                return $q.reject(rejection);
            },
            response: function (response) {
                return response || $q.when(response);
            },
            responseError: function (rejection) {
                var error;
                type = 'error';
                var stack_bottomright = { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 };
                switch (rejection.status) {
                    case 500:
                        error = 'Internal server error';
                        break;
                    case 404:
                        error = 'location not found';
                        break;
                    case 403:
                        $window.location = rootUrl + '/InternalError/Error403';
                        break;
                    case 402:
                        error = 'bad gateway';
                        break;
                    case 0:
                        error = ""
                        type = "info";
                        break;
                    default:
                        error = "Internal server error";
                        break;

                }
                $('button').removeAttr('disabled');


                if ($rootScope.notice) {
                    $rootScope.notice.update({
                        type: type,
                        isResponse: true,
                        text: error,
                        title: '500',
                        hide: true
                    });
                } else {
                    new PNotify({
                        type: type,
                        isResponse: true,
                        text: error,
                        addclass: "stack-bottomright",
                        stack: stack_bottomright
                    });
                }
                return $q.reject(rejection);
            }
        };
    });
    $httpProvider.interceptors.push('CustomHttpInterceptor');
})



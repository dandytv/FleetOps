$(function () {

    $('#ul-searchParams').find('li a').on('click', function () {
        var value = $(this).find('span').text();
        $('input#txtSearchUniversal').val(value).focus();
    });

    $("#open-close").addClass("closed");
    $("#open-close").click(function () {
        if ($(this).hasClass("closed")) {
            $(this).parent().animate({
                right: "+=250"
            }, 500);
            $(this).removeClass("closed");
        } else {
            $(this).parent().animate({
                right: "-=250"
            }, 500);
            $(this).addClass("closed");
        }
    });

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
        { shortCode: "apr", minChars: 1 }, { shortCode: "co1", minChars: 1 }, { shortCode: "co2", minChars:1 }, { shortCode: "cor", minChars: 1 }, { shortCode: "vrn", minChars: 1 }
        , { shortCode: "psn", minChars: 1 }, { shortCode: "mid", minChars: 1 }, { shortCode: "mac", minChars: 1 }, { shortCode: "bsn", minChars: 1 }, { shortCode: "sub", minChars: 1 },
        { shortCode: "mdt", minChars: 1 }, { shortCode: "sid", minChars: 1 }, { shortCode: "apn", minChars: 1 },{ shortCode: "crn", minChars: 1 }],
        this.source = function (request, response) {

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
                            desc: { match: item.MatchedValue, more: item.Descp, dest: item.Dest }
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
            $(this).val('Redirecting (' + ui.item.desc.match + ')...');
            console.log(ui);
            var target, value;
            if (ui.item.desc.dest == "ACCT") {
                target = prefix + '/Account';
                //  window.location.href = target + "?id=" + ui.item.value;
                //  window.location.href = window.location.pathname + "?" + $.param({ 'id': ui.item.value });
                (target == window.location.pathname) ? window.location.href = window.location.pathname + "?" + $.param({ 'id': ui.item.value }) : window.location.href = target + "?id=" + ui.item.value;
            } else if (ui.item.desc.dest == "APPL") {
                target = prefix + '/Applications#/generalInfo';
                window.location.href = target + "?applId=" + ui.item.value;
            }
            else if (ui.item.desc.dest == "MERCH") {
                window.location.href = prefix + '/Merchant#/generalInfo/' + ui.item.value;
            } else if (ui.item.desc.dest == "CARD") {
                window.location.href = prefix + '/Card#/' + ui.item.value + '/' + ui.item.desc.match;
            }
            else if (ui.item.desc.dest == 'CORP') {
                window.location.href = prefix + '/CorporateCodes#/details/' + ui.item.value;
            }
            else if (ui.item.desc.dest == 'BUSN') {
                window.location.href = prefix + '/Dealer/Index/#/generalInfo/' + ui.item.value + '/' + ui.item.desc.match;
            }
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

    $('#txtSearchUniversal').val('').catcomplete({
        minLength: objAutoComplete.minLength,
        source: objAutoComplete.source,
        open: objAutoComplete.open,
        focus: objAutoComplete.focus,
        autoFocus: true,
        select: objAutoComplete.select
    }).data("custom-catcomplete")._renderItemData = objAutoComplete.renderItemData;




    if ($.validator) {
        $.validator.setDefaults({
            highlight: function (element) {
                $(element).closest(".form-group").addClass("has-error");
            },
            unhighlight: function (element) {
                $(element).closest(".form-group").removeClass("has-error");
            }
        });
    }

    $.fn.setReadOnly = function (flag) {
        if (flag) {
            $(this).find('option').not(":selected").attr("disabled", "disabled");
        } else {
            $(this).find('option').not(":selected").removeAttr("disabled");
        }
    };


    $('span.field-validation-valid, span.field-validation-error').each(function () {
        $(this).addClass('help-inline');
    });

    $('form').submit(function () {
        if ($(this).valid()) {
            $(this).find('div.form-group').each(function () {
                if ($(this).find('span.field-validation-error').length == 0) {
                    $(this).removeClass('error');
                }
            });
        }
        else {
            $(this).find('div.form-group').each(function () {
                if ($(this).find('span.field-validation-error').length > 0) {
                    $(this).addClass('error');
                }
            });
        }
    });

    $('button#btnUpdatePassword').on('click', function () {
        var stack_bottomright = { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 };
        var notice = new PNotify({
            text: 'Processing action, please wait...',
            addclass: "stack-bottomright",
            stack: stack_bottomright,
            hide: false,
            styling: 'fontawesome'
        });

        $.ajax({
            url: $('#hdUrlPrefix').val() + '/Auth/UpdatePassword',
            data: $('#frmUpdPw').serialize(),
            type: 'POST',
            success: function (obj) {
                notice.update({
                    type: obj.flag == 0 ? 'success' : 'error',
                    text: obj.desp,
                    hide: true
                });
            }
        });
    });
});
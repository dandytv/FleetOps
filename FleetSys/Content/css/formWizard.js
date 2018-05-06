function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

$(document).ready(function () {

    //$('#wizard1').bootstrapWizard({
    //    'nextSelector': '.button-next',
    //    'previousSelector': '.button-previous',
    //    onNext: function (tab, navigation, index) {
    //        alert("Next clicked");
    //    },
    //    onTabShow: function (tab, navigation, index) {
    //        var $total = navigation.find('li').length;
    //        var $current = index + 1;
    //        var $percent = ($current / $total) * 100;
    //        $('#wizard1').find('.progress-bar').css({
    //            width: $percent + '%'
    //        });

    //        $('#wizard1 > .steps li').each(function (index) {
    //            $(this).removeClass('complete');
    //            index += 1;
    //            if (index < $current) {
    //                $(this).addClass('complete');
    //            }
    //        });

    //        if ($current >= $total) {
    //            $('#wizard1').find('.button-next').hide();
    //            $('#wizard1').find('.button-finish').show();
    //        } else {
    //            $('#wizard1').find('.button-next').show();
    //            $('#wizard1').find('.button-finish').hide();
    //        }
    //    },
    //    onPrev: function (tab,navigation,index) {
    //        alert("previous clicked");
    //    }
    //});
});
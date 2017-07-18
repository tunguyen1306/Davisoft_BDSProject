function AjaxSubmit(formID, url, type, successFunction, data) {
    var formObject = $("#" + formID);
    var idLoader = ('Loader' + Math.random() + 1).replace('.', '');
    var loader = '<div id="' + idLoader + '" class="ajax-loader i-loading-ajax"></div>';

    if (data == null)
        data = formObject.serialize();
    
    formObject.css('position', 'relative');
    formObject.append(loader);

    $.ajax({
        type: type,
        url: url,
        data: data,
        success: function (result) {
            formObject.find("#" + idLoader).remove();
            successFunction(result);
        }
    });
}
function getHashParams() {

    var hashParams = {};
    var e,
        a = /\+/g, // Regex for replacing addition symbol with a space
        r = /([^&;=]+)=?([^&;]*)/g,
        d = function(s) { return decodeURIComponent(s.replace(a, " ")); },
        q = window.location.hash.substring(1);

    while (e = r.exec(q))
        hashParams[d(e[1])] = d(e[2]);

    return hashParams;
}


//**WK - add define to Edit Numeric
var numericInput = $('.edit-numberic');
//if (numericInput.attr('data-v-max') == null || numericInput.attr('data-v-max') == '')
//    numericInput.attr('data-v-max', '999999999999999');

if (numericInput.attr('precision') != null)
    numericInput.attr('data-m-dec', numericInput.attr('precision'));

var tempNumericInput;
$('.edit-numberic').focus(function () {
    var val = $(this).val();
    tempNumericInput = val;
    if (val == 0) {
        $(this).val('');
    }
});
$('.edit-numberic').blur(function () {
    var val = $(this).val();
    if (val == '') {
        if (tempNumericInput != '' && tempNumericInput != 0)
            $(this).val(0);
        else
            $(this).val(tempNumericInput);
    }
});


/// fix unobtrusive date format
jQuery(function ($) {
    $.validator.addMethod('date',
    function (value, element) {
        if (this.optional(element)) {
            return true;
        }

        var ok = true;
        try {
            $.datepicker.parseDate('dd/mm/yy', value);
        } catch (err) {
            ok = false;
        }

        return ok;
    });
});

/// disable changing of checkbox state when click on label
$(function() {
    $(document).on("click", "label:has(:checkbox[readonly])", function(evt) {
        evt.preventDefault();
    });
});

//function restartControlJquery() {
//    //-------Numberic---
//    initEditNumberic();

//    //------select------
//    $("select[data-display=select2]").select2({
//        placeholder: "Select...",
//        width: 'resolve',
//        allowClear: true,
//    });
//    //$("select[optional=true] option:first").before("<option></option>");
//    //$("select[optional=true]").before("<option value='0'>sss</option>");
//    //
//    //$("select[optional=true] option:first") = new Option("1990","1990");

//    //---datetime piker
//    $('.date-picker').datepicker({
//        autoclose: true,
//        todayHighlight: true,
//        placeholder: "mm/dd/yyyy"
//    });

//    $('[rel=popover]').popover();


//    //** WK **
//    // Add attr "Checked" to html code to perfect
//    //----Check box----------
//    $("input[type=checkbox]").click(function () {
//        if (this.checked)
//            this.setAttribute('checked', 'checked');
//        else
//            $(this).removeAttr("checked");
//    });


//}

//initEditNumberic();
//function initEditNumberic() {
//    //**WK - add define to Edit Numeric
//    var numericInput = $('.edit-numberic');
//    //if (numericInput.attr('data-v-max') == null || numericInput.attr('data-v-max') == '')
//    //    numericInput.attr('data-v-max', '999999999999999');

//    if (numericInput.attr('precision') != null)
//        numericInput.attr('data-m-dec', numericInput.attr('precision'));
//    else
//        numericInput.attr('data-m-dec', 0);

//    numericInput.autoNumeric('init');

//    var tempNumericInput;
//    $('.edit-numberic').focus(function () {
//        var val = $(this).val();
//        tempNumericInput = val;
//        if (val == 0) {
//            $(this).val('');
//        }
//    });
//    $('.edit-numberic').blur(function () {
//        var val = $(this).val();
//        if (val == '') {
//            if ($(this).attr('allowNull') == 'true') {
//                $(this).val('');
//                return;
//            }
//            if (tempNumericInput != '' && tempNumericInput != 0)
//                $(this).val(o);
//            else {
//                $(this).val(tempNumericInput);
//            }
//        }
//    });
//}
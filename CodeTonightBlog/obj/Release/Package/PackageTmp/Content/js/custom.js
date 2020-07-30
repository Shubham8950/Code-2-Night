/* Theme Name:Code2night - Clean & Powerful Bootstrap Theme
 * Author:HtmlCoder
 * Author URI:http://www.htmlcoder.me
 * Author e-mail:htmlcoder.me@gmail.com
 * Version: 1.5
 * Created:October 2014
 * License URI:http://support.wrapbootstrap.com/
 * File Description: Place here your custom scripts
 */

function loadDropDownWithSelected(url, elem, postData, selected, option) {

    var abc = $.post(url, postData, function (result, status) {
        console.log(result);

        var element = document.getElementById(elem);
        $(element).empty();
        $(element).append("<option value='0'>" + option + "</option>");
        var obj = JSON.parse(result);
        $(element).select2();
        $.each(obj, function (i, data) {

            if (selected == data.Key) {
                $(element).append("<option value=" + data.Key + " selected>" + data.Value + "</option>");
            }
            else {
                console.log($("#" + elem));
                $(element).append("<option value=" + data.Key + ">" + data.Value + "</option>");
            }
        });
    });


}
function loadDropDownWithName(url, elem, postData, selected, option) {

    var abc = $.post(url, postData, function (result, status) {
        console.log(status);

        var ctrl = document.getElementsByName(elem)[0];
        $(ctrl).empty();
        $(ctrl).append("<option value='0'>" + option + "</option>");
        var obj = JSON.parse(result);
        $(ctrl).select2();
        $.each(obj, function (i, data) {

            if (selected == data.Key) {
                $(ctrl).append("<option value=" + data.Key + " selected>" + data.Value + "</option>");
            }
            else {
                console.log($("#" + elem));
                $(ctrl).append("<option value=" + data.Key + ">" + data.Value + "</option>");
            }
        });
    });


}

function GetAjaxData(url, postData) {
    $.post(url, postData, function (result, status) {
        return result;
    });
}
function GetAjaxDataPromise(url, postData) {
   
    var promise = $.post(url, postData, function (promise, status) {
    console.log(status);
    console.log(promise);

    });
    return promise;
}
function AjaxDeleteRecordById(url, postData) {

    $.post(url, postData, function (result) {
        new PNotify({
            title: 'Notification',
            text: result,
            type: 'success',
            hide: true,
            styling: 'bootstrap3'
        });

    });
}


function Reset(form) {
    $(form).trigger('reset');

}

function LoadPartialView(url, elem, postData) {
    ShowLoader();
    $.post(url, postData, function (result, status) {
        $("#" + elem).html('');
        $("#" + elem).html(result);
        HideLoader();

    });

}
function LoadPartialViewPromise(url, elem, postData) {
    ShowLoader();
    var promise = $.post(url, postData, function (result, status) {
        $("#" + elem).html('');
        $("#" + elem).html(result);
        HideLoader();

    });
    return promise;
}

function ShowNotification(key) {
    if (sessionStorage.getItem(key) != null && sessionStorage.getItem(key) != undefined && sessionStorage.getItem(key) !== "") {
        new PNotify({
            title: 'Success',
            text: sessionStorage.getItem(key),
            type: 'success',
            hide: true,
            styling: 'bootstrap3'
        });
        sessionStorage.removeItem(key);
    }
}
function Notify(Message) {

    new PNotify({
        title: 'Notification',
        text: Message,
        type: 'success',
        hide: true,
        styling: 'bootstrap3'
    });


}
function NotifyError(Message) {

    new PNotify({
        title: 'Error',
        text: Message,
        type: 'error',
        hide: true,
        styling: 'bootstrap3'
    });


}

function ShowLoader() {
    $('#DivLoader_SMS').show();
    return true;
}
function HideLoader() {
    $('#DivLoader_SMS').hide();
    return true;
}
$(".numeric").on('keypress', function (event) {

    var evt = (event) ? event : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;

});
$(".numeric").bind("paste", function (e) {
    e.preventDefault();
});
$(".numeric").bind('mouseenter', function (e) {
    var val = $(this).val();
    if (val != '0') {
        val = val.replace(/[^0-9]+/g, "")
        $(this).val(val);
    }
});
$('input').attr('autocomplete', 'off');
//$('.datetimepicker').datetimepicker({ format: "L" });


//$(".summernote").summernote();

function Setindex(elem) {

    var regex = new RegExp("\\[\\d+\\]\\.");
    var bodyRows = $(elem).find("[data-content='section']");
    console.log(bodyRows + "jhii");

    for (var i = 0; i < bodyRows.length; i++) {
        var elements = $(bodyRows[i]).find("input,select,textarea");
        $(elements).each(function () {
            var el = $(this);
            var name = el.attr("name");
            name = name.replace(regex, "[" + i + "].");
            el.attr("name", name);
            el.removeAttr("id");
            if (el.is(':checkbox')) {
                el.attr("id", name);
                $(this).siblings("label").attr("for", name);
            }
        });
    }

}


function format(data) {
    if (!data.id) return data.text; // optgroup
    return "<img class='img-circle' style='width:20px;height:20px;margin-right:5px;' src='" + data.ImageUrl + "'/>" + data.text;
}
function BindImageSelect2(elem, Ajaxurl) {
    $("#" + elem).select2({
        templateResult: format,
        templateSelection: format,
        escapeMarkup: function (m) { return m; },
        processResults: function (data, params) {
            return {
                results: JSON.parse(data),
            };
        },
        ajax: {
            url: Ajaxurl,
            dataType: 'json',
            delay: 250,
            processResults: function (data, params) {
                return {
                    results: JSON.parse(data),
                };
            },
        },
    });
}
$(".datetimepicker").on("keypress", function () {
    return false;
});
function BindImageSelect2WithDataSource(elem, ajaxurl, params) {
    var dataajax = GetAjaxDataPromise(ajaxurl, params);
    dataajax.done(function (datasource) {
        $("#" + elem).select2({
            templateResult: format,
            templateSelection: format,
            data: JSON.parse(datasource),
            escapeMarkup: function (m) { return m; },
            processResults: function (data) {
                return {
                    results: JSON.parse(data),
                };
            },

        });
    });

}
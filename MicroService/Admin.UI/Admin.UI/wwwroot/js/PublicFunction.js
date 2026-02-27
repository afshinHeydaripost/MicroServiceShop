
function isNullOrEmpty(string) {
    return (string == null || string === "");
}

function objisNull(fildId) {
    $(fildId).focus();
    $(fildId).css("border", "1px red solid");
}
function DestroyTable() {
    $(selector).destroy();
}
function objisFull(fildId) {
    $(fildId).css("border", "1px red #dbdbdb");
}
function ToDataTable(selector) {
    $(selector).DataTable({
        processing: false,
        serverSide: false,
        filter: true,
        language: {
            url: "/lib/DataTable/Persian.json"
        },
        searching: true,
        responsive: true,
        lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "همه موارد"]],
        pagingType: "full_numbers",
        order: [[1, "desc"]]
    });
}
function GetCheckBoxChecked(selector) {
    return $(selector).is(":checked");
}
function loadFile(event, tag) {
    var output = document.getElementById(tag);
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};
function SubString(text, startIndex, length) {
    if (isNullOrEmpty(text))
        return "...";
    else {
        if (text.length > length)
            return text.substring(startIndex, length) + "...";
        else
            return text;
    }
}
function CopyURL(Url = "") {
    var url = window.location.href + Url;
    if (isNullOrEmpty(Url))
        url = window.location.href;
    navigator.clipboard.writeText(url).then(function () {
        ToastMessageSuccess("لینک با موفقیت کپی شد")
    }, function (err) {
        ToastMessageError('خطا در کپی کردن لینک ', err);
    });
}
function DisabledTag(tag) {
    $(tag).attr("disabled", 'disabled');
    $(tag).addClass('disabled');
}
function toEnglishDigits(number) {
    var res = number.replaceAll('۰', '0');
    res = res.replaceAll('۱', '1');
    res = res.replaceAll('۲', '2');
    res = res.replaceAll('۳', '3');
    res = res.replaceAll('۴', '4');
    res = res.replaceAll('۵', '5');
    res = res.replaceAll('۶', '6');
    res = res.replaceAll('۷', '7');
    res = res.replaceAll('۸', '8');
    res = res.replaceAll('۹', '9');
    return res;
}
String.prototype.toEnglishDigits = function () {
    var num_dic = {
        '۰': '0',
        '۱': '1',
        '۲': '2',
        '۳': '3',
        '۴': '4',
        '۵': '5',
        '۶': '6',
        '۷': '7',
        '۸': '8',
        '۹': '9',
    }
    return parseInt(this.replace(/[۰-۹]/g, function (w) {
        return num_dic[w]
    }));
}

function separate(number) { //جدا کردن سه رقم سه رقم اعداد
    number += '';
    number = number.replace(',', '');
    x = number.split('.');
    y = x[0];
    z = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(y))
        y = y.replace(rgx, '$1' + ',' + '$2');
    return y + z;
}
function just_persian(str) {
    if (str != "") {
        var asciiCode = GetAscii(str);
        if (isNullOrEmpty(str) || str == " " || asciiCode == 66 || asciiCode == 69)
            return true;
        var text = ToPersianNumber(str);
        var p = /^[\u0600-\u06FF\s]+$/;
        if (!p.test(text)) {
            return false
        }
    }
    return true;
}

function ChangeUrl(url) {
    location.replace(url);
}
function loadPage() {
    location.reload();
}

$(".JustDigit").keypress(function (event) {
    var controlKeys = [8, 9, 13, 35, 36, 37, 39];
    var isControlKey = controlKeys.join(",").match(new RegExp(event.which));
    if (!event.which || (45 <= event.which && event.which <= 57) || (48 == event.which && $(this).attr("value")) || isControlKey) {
        return
    } else {
        event.preventDefault()
    }
});

$(".Number").keyup(function (event) {
    var Num = $(this).val();
    Num += '';
    Num = Num.replace(',', '');
    Num = Num.replace(',', '');
    Num = Num.replace(',', '');
    Num = Num.replace(',', '');
    Num = Num.replace(',', '');
    Num = Num.replace(',', '');
    x = Num.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    $(this).val(x1 + x2)
});


function ToSafeUrl(strUrl) {
    var res = "";
    var text = String(strUrl);
    res = text.replaceAll(" ", "-");
    res = res.replaceAll(":", "-");
    res = res.replaceAll("/./", "-");
    res = res.replaceAll("///", "-");
    res = res.replaceAll("/\\/", "-");
    res = res.replaceAll('/', "-");
    res = res.replaceAll("/?/", "-");
    res = res.replaceAll("!", "-");
    res = res.replaceAll("@", "");
    res = res.replaceAll("#", "");
    res = res.replaceAll("%", "");
    res = res.replaceAll("^", "");
    res = res.replaceAll("&", "");
    res = res.replaceAll("/*/", "");
    res = res.replaceAll("/+/", "");
    res = res.replaceAll("~", "");
    res = res.replaceAll(",", "");
    res = res.replaceAll("$", "");
    res = res.replaceAll("insert", "");
    res = res.replaceAll("select", "");
    res = res.replaceAll("update", "");
    return res;
}

String.prototype.replaceAll = String.prototype.replaceAll || function (string, replaced) {
    return this.replace(new RegExp(string, 'g'), replaced);
};

function GetAscii(str) {
    return str.charCodeAt(0);
}

function ToPersianNumber(num) {
    var i = 0,

        dontTrim = dontTrim || false,

        num = dontTrim ? String(num) : String(num).trim(),
        len = num.length,

        res = '',
        pos,

        persianNumbers = typeof persianNumber == 'undefined' ? ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'] :
            persianNumbers;
    for (; i < len; i++)
        if ((pos = persianNumbers[num.charAt(i)]))
            res += pos;
        else
            res += num.charAt(i);

    return res;
}


function Any(data, item) {
    return data.includes(item);
}

function SetMaxLength(id, length) {
    $("#" + id).attr('maxlength', length);
}
function ShowTag(selector) {
    $(selector).show();
}
function HideTag(selector) {
    $(selector).hide();
}
function TriggerClick(selector) {
    $(selector).trigger("click");
}

function ShowModal(selector) {
    $(selector).modal('show');
}
function HideModal(selector) {
    $(selector).modal('hide');
}
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function isPhoneNumber(number) {
    var regex = new RegExp('^(\\+98|0)?9\\d{9}$');
    var result = regex.test(number);

    return result;
}
function ShowLoaderGif() {
    var html = '<div id="gifLoader" class="align-items-center d-flex justify-content-center"><div class="d-flex"><img src="/spinning-loading.gif" /></div></div>';
    $("body").append(html);
}
function HidLoaderGif() {
    $("#gifLoader").remove();
}
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);

}
function isIncludes(strText, strSearchText) {
    return strText.includes(strSearchText);
}

function GetFromServer(url, data, callbackFunction, showLoader = false) {
    if (showLoader)
        ShowLoaderGif();
    $.ajax({
        type: 'get',
        url: url,
        data: data,
        success: function (res) {
            HidLoaderGif();
            if (typeof callbackFunction == 'function') {
                callbackFunction.call(this, res);
            }
        },
        error: function (e) {
            HidLoaderGif();
            ToastMessageError("خطا در ارتباط با سرور.");
            console.log("url:" + url);
            console.log(e);
        }
    })
}
function PostToServer(url, data, callbackFunction, showLoader = false) {
    if (showLoader)
        ShowLoaderGif();
    $.ajax({
        url: url,
        method: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (response) {
            HidLoaderGif();
            if (typeof callbackFunction == 'function') {
                callbackFunction.call(this, response);
            }
        },
        error: function (e) {
            HidLoaderGif();
            ToastMessageError("خطا در ارتباط با سرور.");
            console.log("url:" + url);
            console.log(e);
        }
    });
}
function PostToServerByAntiForgeryToken(url, data, antiForgeryToken, callbackFunction, showLoader = false) {
    if (showLoader)
        ShowLoaderGif();
    $.ajax({
        url: url,
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        headers: {
            "RequestVerificationToken": antiForgeryToken
        },
        success: function (response) {
            HidLoaderGif();
            if (typeof callbackFunction == 'function') {
                callbackFunction.call(this, response);
            }
        },
        error: function (e) {
            HidLoaderGif();
            ToastMessageError("خطا در ارتباط با سرور.");
            console.log("url:" + url);
            console.log(e);
        }
    });
}
function AddOrEditForm(Url, frm, callbackFunction, BeforeFunction = null, showLoader = false) {

    if (BeforeFunction != null && typeof BeforeFunction == 'function') {
        BeforeFunction.call(this, res);
    }
    $.validator.unobtrusive.parse(frm);
    if ($(frm).valid()) {
        if (showLoader)
            ShowLoaderGif();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: Url,
            data: new FormData($(frm)[0]),
            contentType: false,
            processData: false,
            success: function (response) {
                HidLoaderGif();
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call(this, response);
                }

            },
            error: function (xhr, status, error) {
                HidLoaderGif();
                ToastMessageError("خطا در ارتباط با سرور.");
                console.log("url:" + Url);
                console.log(error);
            }
        });
    }
}

function ValidationSummaryErrorsClear(selector = ".validation-summary-errors") {
    $(selector).find("ul").html("");
}
function ToSelect2(selector) {
    $(selector).select2();
}
function DestroySelect2(selector) {
    $(selector).select2('destroy');
}
function SetCheckBoxChecked(selector, checked = false) {
    $(selector).prop("checked", checked);
}

function SetSelect2Val(selector, val = null) {
    $(selector).select2().val(val).trigger("change");
}

function Confirmed(title, message, callbackFunction) {
    
    Swal.fire({
        title: title,
        text: message,
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "بله",
        cancelButtonText:"انصراف"

    }).then(function (result) {
        if (result.value) {
            if (typeof callbackFunction == 'function') {
                callbackFunction.call(this, result);
            }
        }
    });
}

function getCheckedValues(selector) {
    return $(selector).map(function () {
        return this.value;
    }).get();
}
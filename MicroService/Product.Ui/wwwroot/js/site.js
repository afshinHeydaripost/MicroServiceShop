function ShowLoader() {

} function HideLoader() {

}
function GetFromServer(url, data, callbackFunction, showLoader = false) {
    if (showLoader) {
        ShowLoader();
    }
    $.ajax({
        type: 'get',
        url: url,
        data: data,
        success: function (res) {
            HideLoader()
            if (typeof callbackFunction == 'function') {
                callbackFunction.call(this, res);
            }

        },
        error: function (res) {
            HideLoader();
            console.log('error');
            console.log("url=>" + url);
            console.log("data=>" + data);
        }
    })
}

function PostFormToServerByFile(Url, frm, callbackFunction) {
    $.validator.unobtrusive.parse(frm);
    if ($(frm).valid()) {
        var token = $(frm).find('input[name="__RequestVerificationToken"]').val();
        var formData = new FormData($(frm)[0]);
        formData.append('__RequestVerificationToken', token);
        console.log(formData);
        console.log($(frm)[0]);
        $.ajax({
            type: "POST",
            dataType: "json",
            url: Url,
            data: formData,
            contentType: false,
            processData: false,
            success: function (result) {
                if (typeof callbackFunction == 'function') {
                    callbackFunction.call(this, res);
                }
            },
            error: function (xhr, status, error) {
                ToastMessageError(error);
            }
        });
    }
}
function PostToServer(Url, data, callbackFunction) {
    $.ajax({
        url: Url,
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (result) {
            if (typeof callbackFunction == 'function') {
                callbackFunction.call(this, result);
            }
        },
        error: function (xhr, status, error) {
            ToastMessageError(error);
            return false;
        }
    });
};

function ToastMessageError(message) {
    $.toast({
        text: "<p class='toastFont'>" + message + "</p>",
        hideAfter: false,
        hideAfter: 5000,
        bgColor: '#F64E60'
    })
}
function ToastMessageSuccess(message) {
    $.toast({
        text: "<p class='toastFont'>" + message + "</p>",
        hideAfter: false,
        hideAfter: 5000,
        bgColor: '#155724'
    })
}


function ShowModal(modal) {
    $(modal).modal('show');
}
function HideModal(modal) {
    $(modal).modal('hide');
}

function loadFile(event, tag) {
    var output = document.getElementById(tag);
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};


function ValidationSummary(tagErrors, tagValid) {
    $(tagErrors).html("<ul></ul>");
    $(tagErrors).addClass("validation-summary-valid");
    $(tagValid).removeClass("validation-summary-errors");
    $(tagValid).html("<ul></ul>");
}
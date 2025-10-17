

function GetFromServer(url, data, callbackFunction) {
    $.ajax({
        type: 'get',
        url: url,
        data: data,
        success: function (res) {
            if (typeof callbackFunction == 'function') {
                callbackFunction.call(this, res);
            }

        },
        error: function (res) {
            console.log('error')
        }
    })
}

function PostFormToServerByFile(Url, frm, callbackFunction) {
    $.validator.unobtrusive.parse(frm);
    if ($(frm).valid()) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: Url,
            data: new FormData(document.forms[0]),
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
                callbackFunction.call(this, res);
            }
        },
        error: function (xhr, status, error) {
            ToastMessageError(error);
            return false;
        }
    });
};
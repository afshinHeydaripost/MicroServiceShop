$(document).ready(function () {

    $(".JustDigit").keypress(function (event) {
        var controlKeys = [8, 9, 13, 35, 36, 37, 39];
        var isControlKey = controlKeys.join(",").match(new RegExp(event.which));
        if (!event.which || (45 <= event.which && event.which <= 57) || (48 == event.which && $(this).attr("value")) || isControlKey) {
            return
        } else {
            event.preventDefault()
        }
    });
})


function AddEdit(
    frm,
    Url,
    callbackFunction = null,
    errorCallbackFunction = null
) {
    let valdata = $(frm).serialize();
    $.validator.unobtrusive.parse(frm);
    if ($(frm).valid()) {
        $.ajax({
            url: Url,
            type: "POST",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: valdata,
            success: function (result) {
                if (typeof callbackFunction == "function")
                    callbackFunction.call(this, result);
            },
            error: function (xhr, status, error) {
                ToastMessageError(error);
                if (typeof errorCallbackFunction == "function")
                    errorCallbackFunction.call(this, result);
            },
        });
    }
}
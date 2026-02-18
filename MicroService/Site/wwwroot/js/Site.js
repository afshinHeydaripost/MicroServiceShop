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
function loadPage() {
    location.reload();
}
function ShowLoaderGif() {
    //var html = '<div id="gifLoader" class="align-items-center d-flex justify-content-center"><div class="d-flex"><img src="/spinning-loading.gif" /></div></div>';
    //  $("body").append(html);
}
function HidLoaderGif() {
    //    $("#gifLoader").remove();
}
function AddEdit(
    frm,
    Url,
    callbackFunction = null,
    showGifLoader = false,
    errorCallbackFunction = null
) {
    let valdata = $(frm).serialize();
    $.validator.unobtrusive.parse(frm);
    if ($(frm).valid()) {
        if (showGifLoader)
            ShowLoaderGif()
        $.ajax({
            url: Url,
            type: "POST",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: valdata,
            success: function (result) {
                HidLoaderGif()
                if (typeof callbackFunction == "function")
                    callbackFunction.call(this, result);
            },
            error: function (xhr, status, error) {
                HidLoaderGif();
                if (typeof errorCallbackFunction == "function")
                    errorCallbackFunction.call(this, result);
            },
        });
    }
}
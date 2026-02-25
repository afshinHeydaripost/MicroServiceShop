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

function isNullOrEmpty(string) {
    return (string == null || string === "");
}

function GenerateUuid() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}
function loadPage() {
    location.reload();
} function ChangeUrl(url = "/") {
    window.location.href = url
}
function ShowLoaderGif(id = "") {
    let html = "";
    if (isNullOrEmpty(id))
        html = '<div id="gifLoader" class="Custome-loader align-items-center d-flex justify-content-center"><div class="loader-spinner"></div></div>';
    else
        html = '<div id="' + id + '" class="Custome-loader align-items-center d-flex justify-content-center"><div class="loader-spinner"></div></div>';

    $("body").append(html);
}
function HidLoaderGif(id = "") {
    if (isNullOrEmpty(id))
        $("#gifLoader").remove();
    else
        $("#" + id).remove();
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
        let id = GenerateUuid();
        if (showGifLoader)
            ShowLoaderGif(id);
        $.ajax({
            url: Url,
            type: "POST",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: valdata,
            success: function (result) {
                HidLoaderGif(id);
                if (typeof callbackFunction == "function")
                    callbackFunction.call(this, result);
            },
            error: function (xhr, status, error) {
                HidLoaderGif(id);
                if (typeof errorCallbackFunction == "function")
                    errorCallbackFunction.call(this, result);
            },
        });
    }
}
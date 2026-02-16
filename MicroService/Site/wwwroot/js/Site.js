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
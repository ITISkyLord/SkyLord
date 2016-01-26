/// <reference path="jquery.d.ts" />
$(document).ready(function () {
    setInterval(function () {
        $(".time").each(function (key, value) {
            console.log("update");
            var node = $(value);
            DecresingTime(node);
            VerboseTime(node);
        });
    }, 1000);
});
function DecresingTime(node) {
    var secondes = +$(node).attr("secondes");
    var minutes = +$(node).attr("minutes");
    var hours = +$(node).attr("hours");
    if (secondes == 0 && minutes == 0 && hours == 0) {
        $(node).removeClass("time");
        return;
    }
    secondes--;
    if (secondes < 0) {
        secondes = 59;
        minutes--;
    }
    if (minutes < 0) {
        minutes = 59;
        hours--;
    }
    if (hours < 0) {
        hours = 0;
    }
    $(node).attr("secondes", secondes);
    $(node).attr("minutes", minutes);
    $(node).attr("hours", hours);
}
function VerboseTime(node) {
    var secondes = +$(node).attr("secondes");
    var minutes = +$(node).attr("minutes");
    var hours = +$(node).attr("hours");
    $(node).text((hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (secondes < 10 ? "0" + secondes : secondes));
}

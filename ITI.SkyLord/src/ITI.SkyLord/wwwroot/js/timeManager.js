/// <reference path="jquery.d.ts" />
$(document).ready(function () {
    setInterval(function () {
        $(".time").each(function (key, value) {
            var node = $(value);
            if (DecresingTime(node)) {
                VerboseTime(node);
            }
        });
    }, 1000);
    setInterval(function () {
        $(".hour").each(function (key, value) {
            DecresingHour($(value));
        });
    }, 1000);
});
function DecresingHour(node) {
    var second = +$(node).attr("second");
    var minute = +$(node).attr("minute");
    var hour = +$(node).attr("hour");
    second++;
    if (second == 60) {
        second = 0;
        minute++;
    }
    if (minute == 60) {
        minute = 0;
        hour++;
    }
    if (hour == 24) {
        hour = 0;
    }
    $(node).attr("second", second);
    $(node).attr("minute", minute);
    $(node).attr("hour", hour);
    $(node).text((hour < 10 ? "0" + hour : hour) + "h" + (minute < 10 ? "0" + minute : minute) + "m" + (second < 10 ? "0" + second : second) + "s");
}
function DecresingTime(node) {
    var secondes = +$(node).attr("secondes");
    var minutes = +$(node).attr("minutes");
    var hours = +$(node).attr("hours");
    if (secondes == 0 && minutes == 0 && hours == 0) {
        $(node).html($(node).html() + "<br><button class='button' onclick='location.reload();'>Cliquez pour rafraichir !</button>");
        $(node).removeClass("time");
        return false;
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
    return true;
}
function VerboseTime(node) {
    var secondes = +$(node).attr("secondes");
    var minutes = +$(node).attr("minutes");
    var hours = +$(node).attr("hours");
    $(node).text((hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (secondes < 10 ? "0" + secondes : secondes));
}

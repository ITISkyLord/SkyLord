/// <reference path="jquery.d.ts" />

$(document).ready(function () {
    setInterval(function () {
        $(".time").each(function (key: number, value: Element) {
            var node = $(value);

            if (DecresingTime(node)) {
                VerboseTime(node);
            }
        });
    }, 1000);

    setInterval(function () {
        $(".hour").each(function (key: number, value: Element) {
            DecresingHour($(value));
        });
    }, 1000);

});
 
function DecresingHour(node: JQuery) {
    var second: number = +$(node).attr("second");
    var minute: number = +$(node).attr("minute");
    var hour: number = +$(node).attr("hour");

    second++;
    if (second == 60) {
        second = 0;
        minute++;
    }
    if (minute == 60) {
        minute= 0;
        hour++;
    }
    if (hour == 24) {
        hour = 0;
    }

    $(node).attr("second", second);
    $(node).attr("minute", minute);
    $(node).attr("hour", hour);

    $(node).text((hour < 10 ? "0" + hour : hour) + "h" + (minute < 10 ? "0" + minute : minute) + "m" + (second < 10 ? "0" + second : second)+"s");

}

function DecresingTime(node: JQuery) {

    var secondes: number = +$(node).attr("secondes");
    var minutes: number = +$(node).attr("minutes");
    var hours: number = +$(node).attr("hours");
    var days: number = +$(node).attr("days");

    if (secondes == 0 && minutes == 0 && hours == 0 && days == 0) {
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
        hours = 23;
        days--;
    }
    if (days < 0) {
        days = 0;
    }

    $(node).attr("secondes", secondes);
    $(node).attr("minutes", minutes);
    $(node).attr("hours", hours);
    $(node).attr("days", days);
    return true;
}

function VerboseTime(node: JQuery) {
    var secondes: number = +$(node).attr("secondes");
    var minutes: number = +$(node).attr("minutes");
    var hours: number = +$(node).attr("hours");
    var days: number = +$(node).attr("days");

    $(node).text((days == 0 ? "" : days+"j et ")+(hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (secondes < 10 ? "0" + secondes : secondes));
}
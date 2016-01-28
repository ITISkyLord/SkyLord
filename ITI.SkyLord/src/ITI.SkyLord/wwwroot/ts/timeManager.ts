/// <reference path="jquery.d.ts" />

$(document).ready(function () {

    $(".time").each(function () {
        if ( +$(this).attr("secondes") == 0 && +$(this).attr("minutes") == 0 && +$(this).attr("hours") == 0)
        {
            $(this).removeClass(".time");
            $(this).text("Tâche finie actualisez pour appliquer.");
        }
    });

    setInterval(function () {
        $(".time").each(function (key: number, value: Element) {
            console.log("update");

            var node = $(value);

            DecresingTime(node);
            VerboseTime(node);
        });
    }, 1000);

});
 
function DecresingTime(node: JQuery) {

    var secondes: number = +$(node).attr("secondes");
    var minutes: number = +$(node).attr("minutes");
    var hours: number = +$(node).attr("hours");

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

function VerboseTime(node: JQuery) {
    var secondes: number = +$(node).attr("secondes");
    var minutes: number = +$(node).attr("minutes");
    var hours: number = +$(node).attr("hours");

    $(node).text((hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (secondes < 10 ? "0" + secondes : secondes));
}
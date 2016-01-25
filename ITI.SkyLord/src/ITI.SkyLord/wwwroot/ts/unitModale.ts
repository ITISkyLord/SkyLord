/// <reference path="jquery.d.ts" />
/// <reference path="jquery-ui.d.ts" />

$(document).ready(function () {

    $(".UnitInfo").click(function (e) {
        $("#StatisticModale").fadeIn();
        var unit = $(this).attr("id");
        $("#StatisticModale > div#" + unit).fadeIn();
    });


    $(".CloseModale").click(function (e) {
        $("#StatisticModale").fadeOut(function () {
            $("#StatisticModale .Stat").hide();
        });
    });


});
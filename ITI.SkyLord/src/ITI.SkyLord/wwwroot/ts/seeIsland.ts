﻿/// <reference path="jquery.d.ts" />
/// <reference path="jquery-ui.d.ts" />

$(document).ready(function () {

    // Quand on selectionne un building => On affiche l'interface qui va bien
    $(".Building").click(function (event) {

        var buildingname: string = $(this).attr("buildingname");
        var viewselector: string = "#ui_"+$(this).attr("templateid");

        $(".templatebuilding").fadeOut("fast",function () {
            setTimeout(function () {
                $(viewselector).fadeIn();
            }, 200);
        });

    });

});









































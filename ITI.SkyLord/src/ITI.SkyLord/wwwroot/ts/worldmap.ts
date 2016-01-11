/// <reference path="jquery.d.ts" />
/// <reference path="jquery-ui.d.ts" />

$(document).ready(function () {

    // On rend l'élément draggable
    $("#draggable").draggable();

    var taillecase: number = 64;
    var taillemap: number = 500;
    var x: number = + $("#MyIsland").attr("x");
    var y: number = + $("#MyIsland").attr("y");

    var pos_x: number = -1 * x * taillecase - taillecase + (taillemap / 2) - (taillecase / 2);
    var pos_y: number  = -1 * y * taillecase - taillecase + (taillemap / 2) - (taillecase / 2);

    $("#draggable").css("top", pos_x + "px");
    $("#draggable").css("left", pos_y + "px");

});
 
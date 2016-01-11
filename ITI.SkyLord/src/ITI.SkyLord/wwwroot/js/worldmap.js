/// <reference path="jquery.d.ts" />
/// <reference path="jquery-ui.d.ts" />
$(document).ready(function () {
    // On rend l'élément draggable
    $("#draggable").draggable();
    var taillecase = 64;
    var taillemap = 500;
    var x = +$("#MyIsland").attr("x");
    var y = +$("#MyIsland").attr("y");
    var pos_x = -1 * x * taillecase - taillecase + (taillemap / 2) - (taillecase / 2);
    var pos_y = -1 * y * taillecase - taillecase + (taillemap / 2) - (taillecase / 2);
    $("#draggable").css("top", pos_x + "px");
    $("#draggable").css("left", pos_y + "px");
});

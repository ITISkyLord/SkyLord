/// <reference path="jquery.d.ts" />
/// <reference path="jquery-ui.d.ts" />
var taillecase = 64;
var taillemap = 500;
$(document).ready(function () {
    // On rend l'élément draggable
    $("#draggable").draggable();
    var pos = GetCoordinate($("#MyIsland"));
    $("#draggable").css("top", pos.X + "px");
    $("#draggable").css("left", pos.Y + "px");
    var menu = $("#Menu");
    $(".Island").click(function (event) {
        // Met la bonne information dans le lien
        var islandId = +$(this).attr("islandid");
        $("#Menu").attr("islandid", islandId);
        // Met l'affichage
        if ($(menu).css("display") == "none") {
            menu.css("top", event.clientX + "px");
            menu.css("left", event.clientY - 100 + "px");
            menu.fadeIn();
        }
        event.stopPropagation();
    });
    $(".map").mousedown(function (event) {
        menu.fadeOut();
    });
    // Quand on click sur le menu, on prend le lien et on lui ajoute le island id correspondant
    $("#Menu a").click(function (event) {
        var islandId = $("#Menu").attr("islandid");
        var lien = $(this).attr("href");
        var nouveaulien = lien.replace("{{islandId}}", islandId);
        window.open(nouveaulien, "_self");
        return false;
    });
});
var Coordinate = (function () {
    function Coordinate(x, y) {
        this.X = x;
        this.Y = y;
    }
    return Coordinate;
})();
function GetCoordinate(islandNode) {
    var x = +$(islandNode).attr("x");
    var y = +$(islandNode).attr("y");
    var pos_x = -1 * x * taillecase - taillecase + (taillemap / 2) - (taillecase / 2);
    var pos_y = -1 * y * taillecase - taillecase + (taillemap / 2) - (taillecase / 2);
    return new Coordinate(pos_x, pos_y);
}

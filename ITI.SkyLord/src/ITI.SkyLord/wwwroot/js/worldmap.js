/// <reference path="jquery.d.ts" />
/// <reference path="jquery-ui.d.ts" />
/*

WorldMap :
Gère la world map pour la vue /World

*/
// Variables à définir en cas de changement de tailles des elements
var taillecase = 64;
var taillemap = 500;
// Main
$(document).ready(function () {
    // Centre la map sur l'island de la personne
    var pos = GetCoordinate($("#MyIsland"));
    $("#draggable").css("top", pos.X + "px");
    $("#draggable").css("left", pos.Y + "px");
    TestConstraint($("#draggable"));
    // On rend l'élément draggable
    $("#draggable").draggable({
        drag: function (event) {
            return TestConstraint($(this));
        }
    });
    function TestConstraint(node) {
        var pos_x = parseInt($(node).css("top"));
        var pos_y = parseInt($(node).css("left"));
        if (pos_x >= 0 && pos_y >= 0) {
            $(node).css("top", "-1px");
            $(node).css("left", "-1px");
            return false;
        }
        else if (pos_x < -5900 || pos_y < -5900) {
            $(node).css("top", "-5899px");
            $(node).css("left", "-5899px");
            return false;
        }
        else if (pos_x < -5900) {
            $(node).css("top", "-5899px");
            return false;
        }
        else if (pos_y < -5900) {
            $(node).css("left", "-5899px");
            return false;
        }
        else if (pos_x >= 0) {
            $(node).css("top", "-1px");
            return false;
        }
        else if (pos_y >= 0) {
            $(node).css("left", "-1px");
            return false;
        }
    }
    // Récupère le node du menu quand on clique sur une island
    var menu = $("#Menu");
    // Charge l'island dans le menu et place le menu et l'affiche avec petite animation
    $(".Island").click(function (event) {
        // Met la bonne information dans le lien
        var islandId = +$(this).attr("islandid");
        $("#Menu").attr("islandid", islandId);
        // Met l'affichage
        if ($(menu).css("display") == "none") {
            var click_x = event.clientX;
            var click_y = event.clientY;
            menu.css("top", click_y + "px");
            menu.css("left", click_x - 100 + "px");
            menu.fadeIn();
        }
        event.stopPropagation();
    });
    // Fais disparaitre le menu quand on clique ailleurs que sur l'island
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
// Struct pour gérer les coordonnées X,Y
var Coordinate = (function () {
    function Coordinate(x, y) {
        this.X = x;
        this.Y = y;
    }
    return Coordinate;
})();
// Helper pour le centrage sur l'island courrante
function GetCoordinate(islandNode) {
    var x = +$(islandNode).attr("x");
    var y = +$(islandNode).attr("y");
    var pos_x = -1 * x * taillecase - taillecase + (taillemap / 2) - (taillecase / 2);
    var pos_y = -1 * y * taillecase - taillecase + (taillemap / 2) - (taillecase / 2);
    return new Coordinate(pos_x, pos_y);
}

/// <reference path="jquery.d.ts" />
$(document).ready(function () {
    // Dès que l'on a un changement sur les coordonnées => on change l'island cible
    $("input[name=pos-x], input[name=pos-y]").change(function () {
        UpdateSelection();
    });
    // Dès que l'on selectionne une île, on change les coordonnées ciblées
    $("#EnnemyToAttack").change(function () {
        SelectGoodCoordinates();
    });
    function UpdateSelection() {
        var pos_x = $("input[name=pos-x]").val();
        var pos_y = $("input[name=pos-y]").val();
        SelectGoodIsland(pos_x, pos_y);
    }
    function SelectGoodCoordinates() {
        $("input[name=pos-x]").val($("option:selected").attr("pos-x"));
        $("input[name=pos-y]").val($("option:selected").attr("pos-y"));
    }
    function SelectGoodIsland(pos_x, pos_y) {
        var optionNode = $("option[pos-x=" + pos_x + "][pos-y=" + pos_y + "]");
        // Si l'on a rien trouvé, on se casse
        if (optionNode.length == 0)
            return;
        // Sinon on change du coup notre select
        $(optionNode).prop("selected", true);
    }
});

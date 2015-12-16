using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel
{
    public class StandardViewModel
    {
        public StandardViewModel(IStandardContext context, int playerId, int currentIslandId)
        {
                Layout.CurrentPlayer = context.Players.Where(p => p.PlayerId == playerId).Single();
                Layout.AllIslands = context.Islands.Where(i => i.Owner == Layout.CurrentPlayer).ToList();
                Layout.CurrentIsland = context.Islands.Where(i => i.IslandId == currentIslandId).Single();

                // Cette ligne ne fonctionne pas car l'on n'a pas de "public Island Destination {get;set;}" dans la class Army.
                //Layout.AttackerArmy = armyContext.Armies.Where(a => a.ArmyState == ArmyState.movement && a.Destination == Layout.CurrentIsland).ToList();
    
        }

        public LayoutViewModel Layout { get; set; }

    }
}

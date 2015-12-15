using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel
{
    public class StandardViewModel
    {
        public StandardViewModel(int playerId, int currentIslandId)
        {

            using (var armyContext = new ArmyContext())
            {
                Layout.CurrentPlayer = armyContext.Players.Where(p => p.PlayerId == playerId).Single();
                Layout.AllIslands = armyContext.Islands.Where(i => i.Owner == Layout.CurrentPlayer).ToList();
                Layout.CurrentIsland = armyContext.Islands.Where(i => i.IslandId == currentIslandId).Single();
                //Layout.AttackerArmy = armyContext.Armies.Where(a => a.ArmyState == ArmyState.movement && a.Destination == Layout.CurrentIsland).ToList();
            }

        }

        public LayoutVM Layout { get; set; }

    }
}

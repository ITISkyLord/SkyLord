using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public class EventPackManager
    {
        public ArmyManager ArmyManager { get; }
        public BuildingManager BuildingManager { get; }
        // public LevelManager LevelManager { get; }
        // public TechnologyManager Technology { get; }

        public EventPackManager(SetupContext ctx)
        {
            ArmyManager = new ArmyManager(ctx);
            var LevelManager = new LevelManager(ctx);
            BuildingManager = new BuildingManager(ctx,LevelManager);

            //LevelManager = new LevelManager(ctx);
            //TechnologyManager = new TechnologyManager(ctx);
        }



    }
}

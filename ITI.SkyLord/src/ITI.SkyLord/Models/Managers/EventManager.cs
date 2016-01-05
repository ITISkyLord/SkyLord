using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using ITI.SkyLord.Models.ObjectModel;

namespace ITI.SkyLord.Models.Managers
{
    public class EventManager
    {
        public void AddUnitEvent(IUnitEventContext ctx, Unit unit, Island island )
        {
            ctx.UnitEvents.Add( new UnitEvent() { eventType = EventType.Unit, unit = unit, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds( unit.UnitStatistics.TimeToBuild ), island = island, done = false } );
            ctx.SaveChanges();
        }

        public void AddArmyEvent(IArmyEventContext ctx, Army army, Island island, ArmyMovement am, int timeToDistance, Island destination )
        {
            int secondsToGo = TimeToGoHereFromHere( island, destination, army);
            ctx.ArmyEvents.Add( new ArmyEvent() { eventType = EventType.Army, army = army, armyMovement = am, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds( secondsToGo ), destination = destination, done = false, island = island } );
            ctx.SaveChanges();
        }

        private int TimeToGoHereFromHere( Island island, Island destination, Army army )
        {
            int numberOfSeconds;
            Unit slowerUnit = army.Regiments.Select(r=> r.Unit).OrderBy(u => u.UnitStatistics.Speed).FirstOrDefault();

            double distance = IslandManager.DistanceBeetweenTwoIslands( island, destination );
            numberOfSeconds = (int)distance * slowerUnit.UnitStatistics.Speed; // Voir si on modifie le ratio
            return numberOfSeconds;
        }

        public void AddBuildingEvent( IBuildingEventContext ctx, Building building, Island island )
        {
            ctx.BuildingEvents.Add( new BuildingEvent() { eventType = EventType.Building, building = building, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds(/*TIME TO BUILD BUILDING */ 100 ), done = false, island = island } );
            ctx.SaveChanges();
        }

        public void AddUpgradeEvent( IBuildingEventContext ctx, Building building, Island island )
        {
            ctx.UpgradeEvents.Add( new UpgradeEvent() { eventType = EventType.Upgrade, building = building, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds(/*TIME TO BUILD BUILDING */ 100 ), done = false, island = island } );
            ctx.SaveChanges();
        }
        public void AddTechnologyEvent(ITechnologyEventContext ctx, Technology technology, Island island )
        {
            ctx.TechnologyEvents.Add( new TechnologyEvent() { eventType = EventType.Technology, technology = technology, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds( 100 /* PAREIL QUE AU DESSUS */), done = false, island = island } );
            ctx.SaveChanges();
        }
    }
}

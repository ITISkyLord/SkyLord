﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;

namespace ITI.SkyLord
{
    public class EventManager
    {
        private SetupContext _generalContext;

        public EventManager(SetupContext ctx)
        {
            _generalContext = ctx;
        }

        public void AddUnitEvent(IUnitEventContext ctx, Unit unit, Island island )
        {
            ctx.UnitEvents.Add( new UnitEvent() { EventType = EventDiscrimator.UnitEvent, unit = unit, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds( unit.UnitStatistics.TimeToBuild ), island = island, done = false } );
            ctx.SaveChanges();
        }

        public void AddArmyEvent(IArmyEventContext ctx, Army army, Island island, ArmyMovement am, int timeToDistance, Island destination )
        {
            int secondsToGo = TimeToGoHereFromHere( island, destination, army);
            ctx.ArmyEvents.Add( new ArmyEvent() { EventType = EventDiscrimator.ArmyEvent, army = army, armyMovement = am, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds( secondsToGo ), destination = destination, done = false, island = island } );
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
            ctx.BuildingEvents.Add( new BuildingEvent() { EventType = EventDiscrimator.BuildingEvent, BuildingToBuild = building, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds(/*TIME TO BUILD BUILDING */ 100 ), done = false, island = island } );
            ctx.SaveChanges();
        }

        public void AddUpgradeEvent( IBuildingEventContext ctx, Building building, Island island )
        {
            ctx.UpgradeEvents.Add( new UpgradeEvent() { EventType = EventDiscrimator.UpgradeEvent, buildingToUpgrade = building, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds(/*TIME TO BUILD BUILDING */ 100 ), done = false, island = island } );
            ctx.SaveChanges();
        }

        public void AddTechnologyEvent(ITechnologyEventContext ctx, Technology technology, Island island )
        {
            ctx.TechnologyEvents.Add( new TechnologyEvent() { EventType = EventDiscrimator.TechnologyEvent, technology = technology, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds( 100 /* PAREIL QUE AU DESSUS */), done = false, island = island } );
            ctx.SaveChanges();
        }
         
        public List<Event> Get(EventType et, IEventContext ctx, int IslandId)
        {
            return ctx.Events.Include(e => e.island).Where(e => e.island.IslandId == IslandId).ToList();
        }

        public void ResolveAll(int islandId, IEventContext ctx)
        {
            // Selectionne les éléments pas encore fait et qui doivent être résolu, dans l'ordre de finission-
            List<Event> t = ctx.Events.Where( e => e.done==false && e.endingDate > DateTime.Now).OrderBy(e=>e.endingDate).ToList();

            foreach(var a in t)
            {
                a.Accept(this);
                a.done = true;
            }

        }

        #region Resolve
        public void Resolve(UnitEvent ue)
        {
            //ArmyManager armyManager = new ArmyManager(_setupContext);
            throw new NotImplementedException();
        }

        public void Resolve(TechnologyEvent te)
        {
            //TechnologyManager technologyManager = new TechnologyManager(_setupContext);
            throw new NotImplementedException();
        }

        public void Resolve(BuildingEvent be)
        {
            //BuildingManager buildingManager = new BuildingManager(_setupContext);
            throw new NotImplementedException();
        }

        public void Resolve(UpgradeEvent ue)
        {
            //BuildingManager buildingManager = new BuildingManager(_setupContext);
            throw new NotImplementedException();
        }

        public void Resolve(ArmyEvent ae)
        {
            //ArmyManager armyManager = new ArmyManager(_setupContext);
            throw new NotImplementedException();
        }
        #endregion

    }
}

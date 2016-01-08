using System;
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
        SetupContext _ctx;
        EventPackManager _allManager;

        public EventManager(SetupContext ctx, EventPackManager allManager)
        {
            _ctx = ctx;
            _allManager = allManager;
        }

        public void AddUnitEvent(IUnitEventContext ctx, Unit unit, Island island )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.UnitEvent, island );
            ctx.UnitEvents.Add( new UnitEvent() { EventType = EventDiscrimator.UnitEvent, unit = unit, begginningDate = begginningDate, endingDate = begginningDate.AddSeconds( unit.UnitStatistics.TimeToBuild ), island = island, done = false } );
        }

        public void AddArmyEvent(IArmyEventContext ctx, Army army, Island island, ArmyMovement am, int timeToDistance, Island destination )
        {
            int secondsToGo = TimeToGoHereFromHere( island, destination, army);
            ctx.ArmyEvents.Add( new ArmyEvent() { EventType = EventDiscrimator.ArmyEvent, army = army, armyMovement = am, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds( secondsToGo ), destination = destination, done = false, island = island } );
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
        }

        public void AddUpgradeEvent( IBuildingEventContext ctx, Building building, Island island )
        {
            ctx.UpgradeEvents.Add( new UpgradeEvent() { EventType = EventDiscrimator.UpgradeEvent, buildingToUpgrade = building, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds(/*TIME TO BUILD BUILDING */ 100 ), done = false, island = island } );
        }

        public void AddTechnologyEvent(ITechnologyEventContext ctx, Technology technology, Island island )
        {
            ctx.TechnologyEvents.Add( new TechnologyEvent() { EventType = EventDiscrimator.TechnologyEvent, technology = technology, begginningDate = DateTime.Now, endingDate = DateTime.Now.AddSeconds( 100 /* PAREIL QUE AU DESSUS */), done = false, island = island } );
        }
         
        public List<Event> Get(EventType et, IEventContext ctx, int IslandId)
        {
            return ctx.Events.Include(e => e.island).Where(e => e.island.IslandId == IslandId).ToList();
        }

        private DateTime FindLastEndingDateInQueue( string eventType, Island island )
        {
            DateTime lastEndingDate = DateTime.Now;
            if( eventType == EventDiscrimator.UnitEvent )
            {
                lastEndingDate = _ctx.UnitEvents.Where( u => u.island.Equals( island ) && u.done == false).OrderByDescending( d=> d.endingDate ).Select(d => d.endingDate).FirstOrDefault();
            } else if( eventType == EventDiscrimator.ArmyEvent )
            {
                lastEndingDate = _ctx.ArmyEvents.Where( u => u.island.Equals( island ) && u.done == false ).OrderByDescending( d => d.endingDate ).Select( d => d.endingDate ).FirstOrDefault();
            }
            else if( eventType == EventDiscrimator.BuildingEvent )
            {
                lastEndingDate = _ctx.BuildingEvents.Where( u => u.island.Equals( island ) && u.done == false ).OrderByDescending( d => d.endingDate ).Select( d => d.endingDate ).FirstOrDefault();
            }
            else if( eventType == EventDiscrimator.UpgradeEvent )
            {
                lastEndingDate = _ctx.UpgradeEvents.Where( u => u.island.Equals( island ) && u.done == false ).OrderByDescending( d => d.endingDate ).Select( d => d.endingDate ).FirstOrDefault();
            }
            else if( eventType == EventDiscrimator.TechnologyEvent )
            {
                lastEndingDate = _ctx.TechnologyEvents.Where( u => u.island.Equals( island ) && u.done == false ).OrderByDescending( d => d.endingDate ).Select( d => d.endingDate ).FirstOrDefault();
            } else
            {
                throw new ArgumentException( "Le Event Discrimator n'est pas valide." );
            }

            if( lastEndingDate == null ) return DateTime.Now;
            else return lastEndingDate;
        }


        #region Resolve

        public void ResolveAllForIsland(long islandId)
        {
            // Selectionne les éléments pas encore fait et qui doivent être résolu, dans l'ordre de finission-
            List<Event> t = _ctx.Events.Where( e => e.done==false && e.endingDate > DateTime.Now).OrderBy(e=>e.endingDate).ToList();
            foreach(var a in t)
            {
                a.Accept(this);
                a.done = true;
            }
        }
        public void ResolveAllForPlayer(long playerId)
        {
            var islands = _ctx.Islands.Include(i => i.Owner).Where(i => i.Owner.PlayerId == playerId).ToList();
            foreach (var island in islands)
            {
                ResolveAllForIsland(island.IslandId);
            }
            return;
        }

        
        public void Resolve(UnitEvent ue)
        {
            throw new NotImplementedException();
        }

        public void Resolve(TechnologyEvent te)
        {
            throw new NotImplementedException();
        }

        public void Resolve(BuildingEvent be)
        {
            throw new NotImplementedException();
        }

        public void Resolve(UpgradeEvent ue)
        {
            throw new NotImplementedException();
        }

        public void Resolve(ArmyEvent ae)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}

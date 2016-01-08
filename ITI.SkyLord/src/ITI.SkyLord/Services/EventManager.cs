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
        List<UnitEvent> _unitEvents;

        public EventManager( SetupContext ctx, EventPackManager allManager )
        {
            _ctx = ctx;
            _allManager = allManager;
            _unitEvents = new List<UnitEvent>();
        }

        public void AddUnitEvent( IUnitEventContext ctx, Unit unit, int number, Island island )
        {
            
            for( int j = 0; j < number; j++ )
            {
                DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.UnitEvent, island );
                _unitEvents.Add( new UnitEvent()
                {
                    EventType = EventDiscrimator.UnitEvent,
                    Unit = unit,
                    begginningDate = begginningDate,
                    endingDate = begginningDate.AddSeconds( unit.Duration ),
                    island = island,
                    done = false
                } );

            }
            foreach( UnitEvent ue in _unitEvents )
            {
                ctx.UnitEvents.Add( ue );
            }

        }

        public void AddArmyEvent( IArmyEventContext ctx, Army army, Island island, ArmyMovement am, int timeToDistance, Island destination )
        {
            // NE PAS OUBLIER D'AJOUTER L'ÉVÈNEMENT DE RETOUR
            int secondsToGo = TimeToGoHereFromHere( island, destination, army);
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.ArmyEvent, island );

            ctx.ArmyEvents.Add( new ArmyEvent() { EventType = EventDiscrimator.ArmyEvent, army = army, armyMovement = am, begginningDate = begginningDate, endingDate = DateTime.Now.AddSeconds( secondsToGo ), destination = destination, done = false, island = island } );
        }

        private int TimeToGoHereFromHere( Island island, Island destination, Army army )
        {
            int numberOfSeconds;
            Unit slowerUnit = army.Regiments.Select( r => r.Unit ).OrderBy( u => u.UnitStatistics.Speed ).FirstOrDefault();
            double distance = IslandManager.DistanceBeetweenTwoIslands( island, destination );
            numberOfSeconds = (int)distance * slowerUnit.UnitStatistics.Speed; // Voir si on modifie le ratio
            return numberOfSeconds;
        }

        public void AddBuildingEvent( IBuildingEventContext ctx, Building building, Island island )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.BuildingEvent, island );
            ctx.BuildingEvents.Add( new BuildingEvent() { EventType = EventDiscrimator.BuildingEvent, BuildingToBuild = building, begginningDate = begginningDate, endingDate = DateTime.Now.AddSeconds(/*TIME TO BUILD BUILDING */ 100 ), done = false, island = island } );
        }

        public void AddUpgradeEvent( IBuildingEventContext ctx, Building building, Island island )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.UpgradeEvent, island );
            ctx.UpgradeEvents.Add( new UpgradeEvent() { EventType = EventDiscrimator.UpgradeEvent, buildingToUpgrade = building, begginningDate = begginningDate, endingDate = DateTime.Now.AddSeconds(/*TIME TO BUILD BUILDING */ 100 ), done = false, island = island } );
        }

        public void AddTechnologyEvent( ITechnologyEventContext ctx, Technology technology, Island island )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.TechnologyEvent, island );
            ctx.TechnologyEvents.Add( new TechnologyEvent() { EventType = EventDiscrimator.TechnologyEvent, technology = technology, begginningDate = begginningDate, endingDate = DateTime.Now.AddSeconds( 100 /* PAREIL QUE AU DESSUS */), done = false, island = island } );
        }

        public List<Event> Get( EventType et, IEventContext ctx, int islandId )
        {
            return ctx.Events.Include( e => e.island ).Where( e => e.island.IslandId == islandId ).ToList();
        }

        private DateTime FindLastEndingDateInQueue( string eventType, Island island )
        {
            DateTime lastEndingDate = DateTime.Now;
            if( eventType == EventDiscrimator.UnitEvent )
            {
                lastEndingDate = _unitEvents.Where( u => u.island.Equals( island ) && u.done == false )
                                                .OrderByDescending( d => d.endingDate ).Select( d => d.endingDate )
                                                .FirstOrDefault();
            }
            else if( eventType == EventDiscrimator.ArmyEvent )
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
            }
            else
            {
                throw new ArgumentException( "Le Event Discrimator n'est pas valide." );
            }

            if( lastEndingDate == null ||lastEndingDate == new DateTime(01,01,01)) return DateTime.Now;
            else return lastEndingDate;
        }


        #region Resolve

        public void ResolveAllForIsland( long islandId )
        {
            // Sélectionne les éléments pas encore fait et qui doivent être résolus, dans l'ordre de finission-
            //List<Event> listEvent = _ctx.Events.Where( e => e.done == false && e.endingDate > DateTime.Now ).OrderBy( e => e.endingDate ).ToList();

            List<UnitEvent> listUnitEvent = _ctx.UnitEvents.Include(u => u.Unit).ThenInclude( u => u.UnitStatistics).Where( e => e.done == false && e.endingDate < DateTime.Now ).OrderBy( e => e.endingDate ).ToList();
            foreach( UnitEvent unitEvent in listUnitEvent )
            {
                unitEvent.Accept( this );
                unitEvent.done = true;
            }
            //foreach( Event even in listEvent )
            //{

            //    even.Accept( this );
            //    even.done = true;
            //}
        }
        public void ResolveAllForPlayer( long playerId )
        {
            List<Island> islands = _ctx.Islands.Include (i => i.Owner ).Where( i => i.Owner.PlayerId == playerId ).ToList();
            foreach( Island island in islands )
            {
                ResolveAllForIsland( island.IslandId );
            }
        }

        public void Resolve( UnitEvent ue )
        {
            // Ajouter une unité dans l'armée, voir avec Army Controller
            // Gérer les listes d'unités à ajouter, à l'instar de UnitsToAdd dans la model relié à ArmyController.
            // Gérer le temps d'attente entre les unités et vérifier que ça ne pose pas de problème d'ajouter une unité à une armée qui est partie en attaque.
            ArmyManager am = new ArmyManager( _ctx );
            am.AddUnit( ue.Unit, 1, ue.island );
            _ctx.SaveChanges();
            // TODO : Si plusieurs lignes sont finies en même temps, on peut les cumuler avec ArmyManager.AddUnit

            //UnitName uN = (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true );
            //am.AddUnit
            //    (
            //        _ctx.Units.Where( u => u.UnitName == uN ).Single(),
            //        kvp.Value,
            //        ue.island
            //    );
        }


        public void Resolve( ArmyEvent ae )
        {
            throw new NotImplementedException();
        }
        public void Resolve( TechnologyEvent te )
        {
            throw new NotImplementedException();
        }

        public void Resolve( BuildingEvent be )
        {
            throw new NotImplementedException();
        }

        public void Resolve( UpgradeEvent ue )
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

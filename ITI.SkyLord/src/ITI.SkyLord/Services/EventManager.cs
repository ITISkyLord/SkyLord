using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using ITI.SkyLord.Services;

namespace ITI.SkyLord
{
    public class EventManager
    {
        SetupContext _context;
        EventPackManager _allManager;
        List<UnitEvent> _unitEvents;

        public EventManager( SetupContext ctx, EventPackManager allManager )
        {
            _context = ctx;
            _allManager = allManager;
            _unitEvents = new List<UnitEvent>();
        }

        internal List<BuildingEvent> GetBuildingEventsOnThisBuildingPosition( long islandId, int position )
        {
            List<BuildingEvent> events = new List<BuildingEvent>();
            // Récupérer les évènements liés à une position
            events = _context.BuildingEvents
                .Include( e => e.Island ).ThenInclude( e => e.Buildings )
                .Where( i => i.Island.IslandId == islandId && i.PositionToBuild == position && i.Done == false)
                .ToList();

            return events;
        }
        public void AddUnitEvent( IUnitEventContext ctx, Unit unit, int number, Island island )
        {

            for ( int j = 0; j < number; j++ )
            {
                DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.UnitEvent, island );
                _unitEvents.Add( new UnitEvent()
                {
                    EventType = EventDiscrimator.UnitEvent,
                    Unit = unit,
                    UnitIdd = unit.UnitId,
                    BegginningDate = begginningDate,
                    EndingDate = begginningDate.AddSeconds( unit.Duration ),
                    Island = island,
                    Done = false
                } );

            }
            foreach ( UnitEvent ue in _unitEvents )
            {
                ctx.UnitEvents.Add( ue );
            }
        }

        public void AddArmyEvent( IArmyEventContext ctx, Army army, Island island, ArmyMovement am, Island destination, Ressource pillagedRessources = null )
        {
            // NE PAS OUBLIER D'AJOUTER L'ÉVÈNEMENT DE RETOUR
            int secondsToGo = TimeToGoHereFromHere( island, destination, army );
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.ArmyEvent, island );
            long pillagedRessourceId;
            if ( pillagedRessources == null ) pillagedRessourceId = 0;
            else pillagedRessourceId = pillagedRessources.RessourceId;

            ctx.ArmyEvents.Add( new ArmyEvent()
            {
                EventType = EventDiscrimator.ArmyEvent,
                Army = army,
                ArmyIdd = army.ArmyId,
                ArmyMovement = am,
                BegginningDate = begginningDate,
                EndingDate = DateTime.Now.AddSeconds( secondsToGo ),
                Destination = destination,
                DestinationIdd = destination.IslandId,
                Done = false,
                PillagedRessources = pillagedRessources,
                PillagedRessourcesIdd = pillagedRessourceId,
                Island = island
            } );
        }

        private int TimeToGoHereFromHere( Island island, Island destination, Army army )
        {
            int numberOfSeconds;
            Unit slowerUnit = army.Regiments.Select( r => r.Unit ).OrderBy( u => u.UnitStatistics.Speed ).FirstOrDefault();
            double distance = IslandManager.DistanceBeetweenTwoIslands( island, destination );
            numberOfSeconds = (int)distance * slowerUnit.UnitStatistics.Speed; // Voir si on modifie le ratio
            return numberOfSeconds;
        }

        public void AddBuildingEvent( IBuildingEventContext ctx, BuildingName building, Island island, int position )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.BuildingEvent, island );

            int duration = _allManager.BuildingManager.GetAvailableBuildings().Where(b => b.BuildingName == building).First().Level.Duration;

            ctx.BuildingEvents.Add( new BuildingEvent()
            {
                EventType = EventDiscrimator.BuildingEvent,
                BuildingToBuild = building,
                BegginningDate = begginningDate,
                EndingDate = DateTime.Now.AddSeconds(duration),
                Done = false,
                Island = island,
                PositionToBuild = position
            } );
            ctx.SaveChanges();
        }

        public void AddUpgradeEvent( IBuildingEventContext ctx, Building building, Island island )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.UpgradeEvent, island );
            ctx.UpgradeEvents.Add( new UpgradeEvent() { EventType = EventDiscrimator.UpgradeEvent, BuildingToUpgrade = building, BegginningDate = begginningDate, EndingDate = DateTime.Now.AddSeconds(/*TIME TO BUILD BUILDING */ 100 ), Done = false, Island = island } );
        }

        public void AddTechnologyEvent( ITechnologyEventContext ctx, TechnologyName technologyName, int duration, Island island )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.TechnologyEvent, island );
            ctx.TechnologyEvents.Add( new TechnologyEvent()
            {
                EventType = EventDiscrimator.TechnologyEvent,
                TechnologyName = technologyName,
                BegginningDate = begginningDate,
                EndingDate = DateTime.Now.AddSeconds( duration ),
                Done = false,
                Island = island
            } );
        }

        public List<Event> Get( EventType et, IEventContext ctx, int islandId )
        {
            return ctx.Events.Include( e => e.Island ).Where( e => e.Island.IslandId == islandId ).ToList();
        }

        private DateTime FindLastEndingDateInQueue( string eventType, Island island )
        {
            DateTime lastEndingDate = DateTime.Now;
            if ( eventType == EventDiscrimator.UnitEvent )
            {
                if ( _unitEvents.Count == 0 )
                {
                    lastEndingDate = _context.UnitEvents.Where( u => u.Island.Equals( island ) && u.Done == false )
                                .OrderByDescending( d => d.EndingDate ).Select( d => d.EndingDate )
                                .FirstOrDefault();
                }
                else
                {
                    lastEndingDate = _unitEvents.Where( u => u.Island.Equals( island ) && u.Done == false )
                                                .OrderByDescending( d => d.EndingDate ).Select( d => d.EndingDate )
                                                .FirstOrDefault();
                }
            }
            else if ( eventType == EventDiscrimator.ArmyEvent )
            {
                lastEndingDate = _context.ArmyEvents.Where( u => u.Island.Equals( island ) && u.Done == false ).OrderByDescending( d => d.EndingDate ).Select( d => d.EndingDate ).FirstOrDefault();
            }
            else if ( eventType == EventDiscrimator.BuildingEvent )
            {
                lastEndingDate = _context.BuildingEvents.Where( u => u.Island.Equals( island ) && u.Done == false ).OrderByDescending( d => d.EndingDate ).Select( d => d.EndingDate ).FirstOrDefault();
            }
            else if ( eventType == EventDiscrimator.UpgradeEvent )
            {
                lastEndingDate = _context.UpgradeEvents.Where( u => u.Island.Equals( island ) && u.Done == false ).OrderByDescending( d => d.EndingDate ).Select( d => d.EndingDate ).FirstOrDefault();
            }
            else if ( eventType == EventDiscrimator.TechnologyEvent )
            {
                // Sur le player plut^to que sur l'ile? 
                lastEndingDate = _context.TechnologyEvents.Where( u => u.Island.Equals( island ) && u.Done == false ).OrderByDescending( d => d.EndingDate ).Select( d => d.EndingDate ).FirstOrDefault();
            }
            else
            {
                throw new ArgumentException( "Le Event Discrimator n'est pas valide." );
            }

            if ( lastEndingDate == null || lastEndingDate == new DateTime( 01, 01, 01 ) ) return DateTime.Now;
            else return lastEndingDate;
        }


        #region Resolve

        /// <summary>
        /// Selectionne les éléments pas encore fait et qui doivent être résolu, dans l'ordre de finission
        /// </summary>
        /// <param name="islandId"></param>
        private void ResolveAllForIsland( long islandId, bool useEventsWhereTarget = true )
        {
            // All events of the player
            List<Event> allEvent = _context.Events.Where( e => e.Done == false && e.EndingDate < DateTime.Now && e.Island.IslandId == islandId ).ToList();

            // All army movements where player is the target
            if ( useEventsWhereTarget )
            {
                List<ArmyEvent> eventsWhereTarget = _context.ArmyEvents.Include( u => u.Army ).ThenInclude( j => j.Regiments ).Where( e => e.Done == false && e.EndingDate < DateTime.Now && e.DestinationIdd == islandId ).ToList();
                allEvent = allEvent.Union( eventsWhereTarget ).OrderBy( e => e.EndingDate ).ToList();
            }

            // Merge the two lists order them by date of attack

            // So, we execute all events ( with a super visitor pattern OTFD )
            foreach ( Event @event in allEvent )
            {
                @event.Accept( this );
                @event.Done = true;
                _context.SaveChanges(); // À voir si on peut pas le mettre ailleurs que dans le manager
            }
        }

        /// <summary>
        /// Resolve all event on all island of the current player
        /// </summary>
        /// <param name="playerId"></param>
        public void ResolveAllForPlayer( long playerId )
        {
            List<Island> islands = _context.Islands.Include( i => i.Owner ).Where( i => i.Owner.PlayerId == playerId ).ToList();
            foreach ( Island island in islands )
            {
                ResolveAllForIsland( island.IslandId );
            }

        }

        public void Resolve( UnitEvent ue )
        {
            // À remettre en place quand on aura la solutions pour Include

            //  /!\ HERE IS THE PROBLEM with Include. /!\
            // UnitEvent unitEvent = _context.UnitEvents.Include(a=>a.Unit).ThenInclude( b => b.UnitStatistics ).Where( e => e.EventId == ue.EventId ).First();

            Unit unit = _context.Units.Include( u => u.Requirements ).Include( u => u.UnitCost ).Include( u => u.UnitStatistics )
                .Single( u => u.UnitId == ue.UnitIdd );
            ArmyManager am = _allManager.ArmyManager;
            am.AddUnit( unit, 1, ue.Island );
            // TODO : Si plusieurs lignes sont finies en même temps, on peut les cumuler avec ArmyManager.AddUnit

        }

        public void Resolve( ArmyEvent armyEvent )
        {
            // Résolution de Army Event
            // Le combat doit se passer et/ou l'armée doit rejoindre une autre armée.
            // Attention aux ressources transférées.
            //ArmyEvent armyEvent = _context.ArmyEvents
            //                              .Include( a => a.Destination)
            //                              .Where( e => e.EventId == ae.EventId ).First();
            ArmyManager armyManager = _allManager.ArmyManager;
            Army attackingArmy = _context.Armies
                                        .Include( i => i.Island ).ThenInclude( i => i.Coordinates )
                                        .Include( i => i.Island )
                                        .ThenInclude( i => i.Armies ).ThenInclude( i => i.Regiments )
                                        .Include( y => y.Regiments )
                                        .ThenInclude( z => z.Unit )
                                        .ThenInclude( z => z.UnitStatistics )
                                        .Include( i => i.Island ).ThenInclude( i => i.AllRessources )
                                        .Where( u => u.ArmyId == armyEvent.ArmyIdd )
                                        .Single();

            if ( armyEvent.ArmyMovement == ArmyMovement.attacking )
            {
                Island destination = _context.Islands
                                    .Include( c => c.Coordinates )
                                    .Include( p => p.Owner )
                                    .Include( i => i.Armies )
                                    .ThenInclude( r => r.Regiments )
                                    .ThenInclude( r => r.Unit ).ThenInclude( r => r.UnitStatistics )
                                    .Include( i => i.AllRessources )
                                    .Where( i => i.IslandId == armyEvent.DestinationIdd )
                                    .Single();

                ResolveAllForIsland( destination.IslandId, false );
                Army defendingArmy = destination.Armies.Where( a => a.ArmyState == ArmyState.defense ).SingleOrDefault();
                //   defendingArmy.Island = destination;
                if ( defendingArmy == null )
                    defendingArmy = new Army { Island = destination, Regiments = new List<Regiment>(), ArmyState = ArmyState.defense };

                CombatResult combatResult = armyManager.ResolveCombat( attackingArmy, defendingArmy, armyEvent, _context );
                _context.Messages.Add( combatResult.CombatReportWinner );
                _context.Messages.Add( combatResult.CombatReportLooser );
                _context.SaveChanges();
                this.AddArmyEvent( _context, attackingArmy, attackingArmy.Island, ArmyMovement.returning, destination, combatResult.PillagedRessources );
            }
            else if ( armyEvent.ArmyMovement == ArmyMovement.returning )
            {
                if ( attackingArmy != null )
                {
                    Ressource pillagedRessource = _context.Ressources.Where( r => r.RessourceId == armyEvent.PillagedRessourcesIdd ).SingleOrDefault();

                    RessourceManager.AddRessource( attackingArmy.Island.AllRessources, pillagedRessource );
                    // L'armée est déjà présente à l'aller
                    //attackingArmy.Island.AllRessources.ChangeRessources( pillagedRessources );
                    attackingArmy = armyManager.JoinArmies( armyEvent.Island.Armies.SingleOrDefault( a => a.ArmyState == ArmyState.defense ), attackingArmy );
                    _context.SaveChanges();
                }
            }


            //    CombatReportViewModel combatReportViewModel = new CombatReportViewModel { CombatResult = combatResult };

        }
        public void Resolve( TechnologyEvent te )
        {
            TechnologyManager tm = new TechnologyManager( _context, new LevelManager( _context ), new BonusManager( _context ) );
            TechnologyEvent technoEvent = _context.TechnologyEvents.Single( e => e.EventId == te.EventId );
            if ( tm.GetPlayersTechnologies( technoEvent.Island.Owner.PlayerId ).Any( t => t.TechnologyName == technoEvent.TechnologyName ) )
            {
                tm.LevelUpTechnology( technoEvent.TechnologyName, technoEvent.Island.Owner.PlayerId, technoEvent.Island.IslandId );
            }
            else
            {
                tm.AddTechnology( technoEvent.TechnologyName, technoEvent.Island.Owner.PlayerId, technoEvent.Island.IslandId );
            }

            _context.SaveChanges();

            // Update all the units with the newly added bonus
            tm.BonusManager.ResolvePlayersArmies( technoEvent.Island.Owner.PlayerId, technoEvent.Island.IslandId );
        }

        internal void Resolve( BuildingEvent be )
        {
            _allManager.BuildingManager.AddBuildingToIsland(be.BuildingToBuild, be.Island.IslandId, be.PositionToBuild);
        }

        internal void Resolve( UpgradeEvent ue )
        {
            ue = _context.UpgradeEvents.Where( e => e.EventId == ue.EventId ).Single();
            _allManager.BuildingManager.LevelUpBuilding( ue.BuildingToUpgrade.BuildingName, ue.Island.IslandId, ue.Island.Owner.PlayerId, ue.BuildingToUpgrade.Position );
        }


        #endregion
    }
}

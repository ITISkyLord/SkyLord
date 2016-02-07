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
            // Get the buildingEvent on this position
            events = _context.BuildingEvents
                .Include( e => e.Island ).ThenInclude( e => e.Buildings )
                .Where( i => i.Island.IslandId == islandId && i.PositionToBuild == position && i.Done == false )
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
                    // Get the duration affected by bonuses
                    EndingDate = begginningDate.AddSeconds( _allManager.BonusManager.GetModifiedDuration( unit, island.Owner.PlayerId, island.IslandId ) ),
                    Island = island,
                    Done = false
                } );

            }
            foreach ( UnitEvent ue in _unitEvents )
            {
                ctx.UnitEvents.Add( ue );
            }
        }

        public void AddArmyEvent( IArmyEventContext ctx, Army army, Island island, ArmyMovement am, Island destination, Ressource pillagedRessources = null, bool isReturning = false )
        {
            // NE PAS OUBLIER D'AJOUTER L'ÉVÈNEMENT DE RETOUR
            int secondsToGo = TimeToGoHereFromHere( island, destination, army );
            DateTime begginningDate;
            if( isReturning )
                begginningDate = FindLastEndingDateInQueue( EventDiscrimator.ArmyEvent, island, army.ArmyId );
            else
                begginningDate = DateTime.Now;

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
                EndingDate = begginningDate.AddSeconds( secondsToGo ),
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
            numberOfSeconds = (int)((distance / slowerUnit.UnitStatistics.Speed ) * 3000 ); // Voir si on modifie le ratio
            Console.WriteLine( "Nombre de secondes de transport : " + numberOfSeconds );
            return numberOfSeconds;
        }

        public void AddBuildingEvent( IBuildingEventContext ctx, BuildingName buildingName, Island island, int position, BuildingLevel buildingLevel )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.BuildingEvent, island );

            ctx.BuildingEvents.Add( new BuildingEvent()
            {
                EventType = EventDiscrimator.BuildingEvent,
                BuildingToBuild = buildingName,
                BegginningDate = DateTime.Now,
                // Get the duration affected by bonuses
                EndingDate = DateTime.Now.AddSeconds( _allManager.BonusManager.GetModifiedDuration( buildingLevel, island.Owner.PlayerId, island.IslandId ) ),
                Done = false,
                Island = island,
                PositionToBuild = position
            } );

            ctx.SaveChanges();
        }

        public void AddUpgradeEvent( IBuildingEventContext ctx, Building building, Island island )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.UpgradeEvent, island );
            ctx.UpgradeEvents.Add( new UpgradeEvent()
            {
                EventType = EventDiscrimator.UpgradeEvent,
                BuildingToUpgrade = building,
                BegginningDate = DateTime.Now,
                EndingDate = DateTime.Now.AddSeconds( _allManager.BonusManager.GetModifiedDuration( building.Level, island.Owner.PlayerId, island.IslandId ) ),
                Done = false,
                Island = island
            } );
        }

        public void AddTechnologyEvent( ITechnologyEventContext ctx, TechnologyName technologyName, int duration, Island island, TechnologyLevel technologyLevel )
        {
            DateTime begginningDate = FindLastEndingDateInQueue( EventDiscrimator.TechnologyEvent, island );
            ctx.TechnologyEvents.Add( new TechnologyEvent()
            {
                EventType = EventDiscrimator.TechnologyEvent,
                TechnologyName = technologyName,
                BegginningDate = begginningDate,
                EndingDate = DateTime.Now.AddSeconds( _allManager.BonusManager.GetModifiedDuration( technologyLevel, island.Owner.PlayerId, island.IslandId ) ),
                Done = false,
                Island = island
            } );
        }

        public List<Event> Get( EventType et, IEventContext ctx, int islandId )
        {
            return ctx.Events.Include( e => e.Island ).Where( e => e.Island.IslandId == islandId ).ToList();
        }

        private DateTime FindLastEndingDateInQueue( string eventType, Island island, long armyId = 0 )
        {
            DateTime lastEndingDate = DateTime.Now;
            if ( eventType == EventDiscrimator.UnitEvent )
            {
                if ( _unitEvents.Count == 0 )
                {
                    UnitEvent unitEvent = _context.UnitEvents.Where( u => u.Island.Equals( island ) && u.Done == false )
                               .OrderByDescending( d => d.EndingDate ).FirstOrDefault();

                    if( unitEvent != null )
                    {
                        lastEndingDate = unitEvent.EndingDate;
                    }

                    //lastEndingDate = _context.UnitEvents.Where( u => u.Island.Equals( island ) && u.Done == false )
                    //            .OrderByDescending( d => d.EndingDate ).Select( d => d.EndingDate )
                    //            .FirstOrDefault();

                    //lastEndingDate = DateTime.Now;
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
                lastEndingDate = _context.ArmyEvents.Where( u => u.Island.Equals( island ) && u.Done == false && u.ArmyIdd == armyId ).OrderByDescending( d => d.EndingDate ).Select( d => d.EndingDate ).FirstOrDefault();
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

        public List<UnitQueue> GetCurrentUnitQueue( long currentIslandId )
        {
            // Get all the unitEvents from the island that are no done yet
            List<UnitEvent> unitEventsOnCurrentIsland = _context.UnitEvents.Include( ue => ue.Island ).Include( ue => ue.Unit )
                .Where( ue => !ue.Done && ue.Island.IslandId == currentIslandId ).ToList();

            // Create a KeyValPair list from the events to separate the key to filter from
            List<KeyValPair> listKvP = new List<KeyValPair>();
            foreach ( UnitEvent unitEvent in unitEventsOnCurrentIsland )
            {
                if ( !unitEvent.Done )
                {
                    listKvP.Add( new KeyValPair { Key = unitEvent.Unit.UnitName, Value = unitEvent } );
                }
            }

            // Create a collection of groups with the ChunkBy class
            IEnumerable<IGrouping<UnitName, KeyValPair>> groups = listKvP.ChunkBy( p => p.Key );

            // Create the UnitQueue list from the groups
            List<UnitQueue> queue = new List<UnitQueue>();
            foreach ( IGrouping<UnitName, KeyValPair> group in groups )
            {
                queue.Add( new UnitQueue
                {
                    Number = group.Count(),
                    EndingDate = group.Last().Value.EndingDate,
                    Unit = group.First().Value.Unit
                } );
            }

            return queue;
        }

        public TechnologyEvent GetCurrentResearch( long playerId )
        {
            Player currentPlayer = _context.Players.Include( p => p.Islands ).First( p => p.PlayerId == playerId );
            return _context.TechnologyEvents.SingleOrDefault( e => !e.Done && currentPlayer.Islands.Any( i => i.IslandId == e.Island.IslandId ) );
        }

        public BuildingEvent GetCurrentConstruction( long islandId )
        {
            return _context.BuildingEvents
               .Include( e => e.Island ).ThenInclude( e => e.Buildings )
               .Where( i => i.Done == false && i.Island.IslandId == islandId ).FirstOrDefault();
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
            Army armyOnMovement = _context.Armies
                                        .Include( i => i.Island ).ThenInclude( i => i.Coordinates )
                                        .Include( i => i.Island )
                                        .ThenInclude( i => i.Armies ).ThenInclude( i => i.Regiments )
                                        .Include( y => y.Regiments )
                                        .ThenInclude( z => z.Unit )
                                        .ThenInclude( z => z.UnitStatistics )
                                        .Include( i => i.Island ).ThenInclude( i => i.AllRessources )
                                        .Include( i => i.Island.Buildings )
                                        .Include( i => i.Island).ThenInclude(i => i.Owner)
                                        .Where( u => u.ArmyId == armyEvent.ArmyIdd )
                                        .Single();

            Island destination = _context.Islands
                                    .Include( c => c.Coordinates )
                                    .Include( p => p.Owner )
                                    .Include( i => i.Armies )
                                    .ThenInclude( r => r.Regiments )
                                    .ThenInclude( r => r.Unit ).ThenInclude( r => r.UnitStatistics )
                                    .Include( i => i.AllRessources )
                                    .Include( i => i.Buildings ).ThenInclude( i => i.Level )
                                    .Where( i => i.IslandId == armyEvent.DestinationIdd )
                                    .Single();

            if ( armyEvent.ArmyMovement == ArmyMovement.attacking )
            {
                ResolveAllForIsland( destination.IslandId, false );
                Army defendingArmy = destination.Armies.Where( a => a.ArmyState == ArmyState.defense ).SingleOrDefault();
                if ( defendingArmy == null )
                    defendingArmy = new Army { Island = destination, Regiments = new List<Regiment>(), ArmyState = ArmyState.defense };

                CombatResult combatResult = armyManager.ResolveCombat( armyOnMovement, defendingArmy, armyEvent, _context );
                _context.Messages.Add( combatResult.CombatReportWinner );
                _context.Messages.Add( combatResult.CombatReportLooser );
                _context.SaveChanges();
                if ( combatResult.CombatReportWinner.Receiver == armyOnMovement.Island.Owner ) // S'il n'y a plus de troupes dans l'armées attaquante, il n'y a pas de retour.
                    this.AddArmyEvent( _context, armyOnMovement, armyOnMovement.Island, ArmyMovement.returning, destination, combatResult.PillagedRessources, true );
            }
            else if ( armyEvent.ArmyMovement == ArmyMovement.sendingRessources )
            {
                armyEvent.Destination = destination;
                Ressource pillagedRessource = _context.Ressources.Where( r => r.RessourceId == armyEvent.PillagedRessourcesIdd ).SingleOrDefault();
                armyEvent.PillagedRessources = pillagedRessource;
                CombatResult combatResult = armyManager.ResolveSendingRessources( armyOnMovement, armyEvent, _context );
                _context.Messages.Add( combatResult.CombatReportWinner );
                _context.SaveChanges();
                this.AddArmyEvent( _context, armyOnMovement, armyOnMovement.Island, ArmyMovement.returning, destination, null, true);
            }
            else if ( armyEvent.ArmyMovement == ArmyMovement.returning )
            {
                if ( armyOnMovement != null )
                {
                    Ressource pillagedRessource = _context.Ressources.Where( r => r.RessourceId == armyEvent.PillagedRessourcesIdd ).SingleOrDefault();
                    if ( pillagedRessource != null )
                        RessourceManager.AddRessource( armyOnMovement.Island.AllRessources, pillagedRessource );

                    armyEvent.Island.Armies.SingleOrDefault( a => a.ArmyState == ArmyState.defense );
                    Army islandArmy = _context.Armies
                                    .Include( a => a.Island )
                                    .Include( a => a.Regiments )
                                    .ThenInclude( r => r.Unit )
                                    .Where( a => a.Island.IslandId == armyEvent.Island.IslandId && a.ArmyState == ArmyState.defense )
                                    .Single();
                    armyOnMovement = armyManager.JoinArmies( islandArmy, armyOnMovement, destination.IslandId );
                    _context.SaveChanges();
                }
            }
            else if ( armyEvent.ArmyMovement == ArmyMovement.moving )
            {
                Army defendingArmy = destination.Armies.SingleOrDefault( a => a.ArmyState == ArmyState.defense );
                armyManager.JoinArmies( defendingArmy, armyOnMovement, destination.IslandId );
            }
            else if ( armyEvent.ArmyMovement == ArmyMovement.colonising )
            {
                // Check if the destination is still a free island, if not, program a return
                Ressource transportedRessource = _context.Ressources.Where( r => r.RessourceId == armyEvent.PillagedRessourcesIdd ).SingleOrDefault();
                if ( destination.Owner != null )
                {
                    this.AddArmyEvent( _context, armyOnMovement, armyOnMovement.Island, ArmyMovement.returning, destination, transportedRessource, true );
                }
                else
                {
                    // Change the ownership of the island to the player
                    destination.Owner = armyOnMovement.Island.Owner;
                    _context.SaveChanges();
                    // Add a tower, a portal and a shield

                    _allManager.BuildingManager.AddBuildingToIsland( BuildingName.tower, destination.IslandId, 1 );
                    _allManager.BuildingManager.AddBuildingToIsland( BuildingName.invocation, destination.IslandId, 2 );
                    _allManager.BuildingManager.AddBuildingToIsland( BuildingName.shield, destination.IslandId, 10 );

                    destination.Name = "Nouvelle île";

                    // Add the ressources the apprentice brings on the island
                    if ( transportedRessource != null )
                        destination.AllRessources = transportedRessource;

                    armyManager.RemoveArmy( armyOnMovement );
                }
            }



            //    CombatReportViewModel combatReportViewModel = new CombatReportViewModel { CombatResult = combatResult };

        }

        public void Resolve( TechnologyEvent te )
        {
            TechnologyManager tm = new TechnologyManager( _context, new LevelManager( _context ), new BonusManager( _context ) );
            TechnologyEvent technoEvent = _context.TechnologyEvents.Single( e => e.EventId == te.EventId );

            List<Technology> playersTechnologies = tm.GetPlayersTechnologies( technoEvent.Island.Owner.PlayerId );
            if ( playersTechnologies.Any( t => t.TechnologyName == technoEvent.TechnologyName ) )
            {
                tm.LevelUpTechnology( technoEvent.TechnologyName, technoEvent.Island.Owner.PlayerId, technoEvent.Island.IslandId );
            }
            else
            {
                tm.AddTechnology( technoEvent.TechnologyName, technoEvent.Island.Owner.PlayerId, technoEvent.Island.IslandId );
            }

            _context.SaveChanges();

            playersTechnologies = tm.GetPlayersTechnologies( technoEvent.Island.Owner.PlayerId );

            // Update all the units with the newly added bonus
            tm.BonusManager.ResolvePlayersArmies( technoEvent.Island.Owner.PlayerId, technoEvent.Island.IslandId );

            // If the conquest technology has been upgraded, upgrade the max island of the player
            if ( te.TechnologyName == TechnologyName.conquest )
            {
                Technology conquest = playersTechnologies.Single( t => t.TechnologyName == TechnologyName.conquest );
                if ( conquest.Level.Number >= 2 )
                {
                    _context.Players.Single( p => p.PlayerId == technoEvent.Island.Owner.PlayerId ).MaxIsland = 1 + ( conquest.Level.Number / 2 );
                }
            }
        }

        internal void Resolve( BuildingEvent be )
        {
            List<Building> buildingsOnIsland = _allManager.BuildingManager.GetBuildingsOnCurrentIsland( be.Island.IslandId, be.Island.Owner.PlayerId );
            if ( buildingsOnIsland.Any( b => b.BuildingName == be.BuildingToBuild && b.Position== be.PositionToBuild) )
            {
                _allManager.BuildingManager.LevelUpBuilding( be.BuildingToBuild, be.Island.IslandId, be.Island.Owner.PlayerId, be.PositionToBuild );
            }
            else
            {
                _allManager.BuildingManager.AddBuildingToIsland( be.BuildingToBuild, be.Island.IslandId, be.PositionToBuild );
            }
            _context.SaveChanges();

            // Update all the units production in case that the new building reduces unit production speed
            //List<UnitEvent> currentUnitEvents = _context.UnitEvents.Include( e => e.Island )
            //    .Where( e => e.Island.IslandId == be.Island.IslandId ).ToList();
            //if ( currentUnitEvents != null )
            //{
            //    foreach ( UnitEvent unitEvent in currentUnitEvents )
            //    {
            //          /!\ LINQ Exception here /!\
            //        unitEvent.EndingDate = unitEvent.BegginningDate.AddSeconds( _allManager.BonusManager.GetModifiedDuration( unitEvent.Unit, be.Island.Owner.PlayerId, be.Island.IslandId ) );
            //    }
            //}
        }

        internal void Resolve( UpgradeEvent ue )
        {
            ue = _context.UpgradeEvents.Where( e => e.EventId == ue.EventId ).Single();
            _allManager.BuildingManager.LevelUpBuilding( ue.BuildingToUpgrade.BuildingName, ue.Island.IslandId, ue.Island.Owner.PlayerId, ue.BuildingToUpgrade.Position );
        }


        #endregion
    }

    internal class KeyValPair
    {
        public UnitName Key { get; set; }
        public UnitEvent Value { get; set; }
    }
}

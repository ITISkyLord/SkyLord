using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel.Armies;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITI.SkyLord.ViewModel;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using ITI.SkyLord.Services;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Mvc.Filters;
using ITI.SkyLord.ViewModel.Partial;

namespace ITI.SkyLord.Controllers
{
    public class ArmyController : GenericController
    {
        public IActionResult Index( long islandId = 0 )
        {
            return View( CreateArmyViewModel( islandId ) );
        }

        public IActionResult AddUnit( BuildingPartialViewModel model, long islandId = 0 )
        {
            ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );
            EventManager em = new EventManager( SetupContext, new EventPackManager( SetupContext ) );

            if( model.UnitAmount <= 0 )
            {
                ModelState.AddModelError( "UnitsToAdd", "Les unités ne peuvent pas être négatives." );
            }
            else
            {
                long playerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;

                Unit unit = SetupContext.Units.Include( u => u.UnitCost ).Include( u => u.UnitStatistics )
                    .Single( u => u.UnitName == model.UnitTarget && u.IsModel );
                Island island = SetupContext.GetIsland( islandId, playerId );
                if( unit.UnitCost.Wood * model.UnitAmount > island.AllRessources.Wood || unit.UnitCost.Metal * model.UnitAmount > island.AllRessources.Metal * model.UnitAmount || unit.UnitCost.Cristal * model.UnitAmount > island.AllRessources.Cristal || unit.UnitCost.Magic * model.UnitAmount > island.AllRessources.Magic )
                {
                    ModelState.AddModelError( "UnitsToAdd", "Vous n'avez pas assez de ressources." );
                }
                else
                {
                    BonusManager bonusManager = new BonusManager( SetupContext );
                    RessourceManager.RemoveRessource( island.AllRessources, unit.UnitCost.Wood * model.UnitAmount, unit.UnitCost.Metal * model.UnitAmount, unit.UnitCost.Cristal * model.UnitAmount, unit.UnitCost.Magic * model.UnitAmount );
                    em.AddUnitEvent( SetupContext, unit, model.UnitAmount, island );
                    //SetupContext.Ressources.Update( island.AllRessources );
                    SetupContext.SaveChanges();
                }
            }


            return RedirectToAction( "SeeMyIsland", "Island", new
            {
                islandId = islandId
            } );

        }
        public IActionResult SetAttackingArmy( SetAttackingArmyViewModel model, long islandId = 0, long EnnemyIslandId = 0 )
        {
            if( EnnemyIslandId != 0 ) model.EnnemyIslandId = EnnemyIslandId;

            // Tout les mouvements d'armée de l'ile qui ne sont pas encore fait et qui ne sont pas obsolètes
            model.AllPlayerArmiesEvent = SetupContext
                .ArmyEvents
                    .Include( i => i.Island )
                    .Include( i => i.Army )
                    .Include( i => i.Destination )
                        .ThenInclude( i => i.Owner )
                .Where( ae => ae.Island.IslandId == islandId && ae.Done == false && ae.Army.ArmyState != ArmyState.obsolete )
                .ToList();


            return View( CreateSetAttackingArmyViewModel( model, islandId ) );
        }

        public IActionResult Fight( SetAttackingArmyViewModel model, long islandId = 0 )
        {
            Army defendingArmyFromAttacker = SetupContext.Armies
                                    .Include( a => a.Island )
                                    .Include( a => a.Regiments )
                                    .ThenInclude( r => r.Unit )
                                    .Where( a => a.Island.IslandId == islandId && a.ArmyState == ArmyState.defense )
                                    .SingleOrDefault();

            if( model.UnitsToSend.Count( kvp => kvp.Value == 0 ) == model.UnitsToSend.Count() || model.UnitsToSend.Any( kvp => kvp.Value < 0 ) )
            {
                if( model.UnitsToSend.Any( kvp => kvp.Value < 0 ) )
                {
                    ModelState.AddModelError( "UnitsToSend", "Les unités ne peuvent pas être négatives." );
                    return RedirectToAction( "SetAttackingArmy", new { islandId = islandId } );
                }
                else if( model.UnitsToSend.Count( kvp => kvp.Value == 0 ) == model.UnitsToSend.Count() )
                {
                    ModelState.AddModelError( "UnitsToSend", "Aucune unité sélectionnée." );
                }
            }
            if( model.UnitsToSend.Any( kvp => kvp.Value >
                defendingArmyFromAttacker.Regiments.Single( r => r.Unit.UnitName == (UnitName)Enum.Parse( typeof( UnitName ), kvp.Key, true ) ).Number ) )
            {
                ModelState.AddModelError( "UnitsToSend", "Vous ne pouvez pas envoyer plus d'unités que vous n'en possédez." );
                return RedirectToAction( "SetAttackingArmy", new { islandId = islandId } );
            }
            if( model.Target == 0 )
            {
                ModelState.AddModelError( "UnitsToSend", "Aucune cible sélectionnée." );
                return RedirectToAction( "SetAttackingArmy", new { islandId = islandId } );
            }

            EventManager eventManager = new EventManager( SetupContext, new EventPackManager( SetupContext ) );

            Island island = SetupContext.Islands.Include( i => i.Owner )
                                                .Include( i => i.Coordinates )
                                                .Include( i => i.AllRessources )
                                                .Include( i => i.Armies ).ThenInclude( a => a.Regiments )
                                                .ThenInclude( r => r.Unit ).ThenInclude( r => r.UnitStatistics )
                                                .Where( i => i.IslandId == model.Target ).FirstOrDefault();
            ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );
            long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;

            Island attackingIsland = SetupContext.GetIsland( islandId, activePlayerId );
            Army attackingArmy = am.CreateArmy( model.UnitsToSend, attackingIsland );
            SetupContext.Armies.Add( attackingArmy );
            SetupContext.SaveChanges();
            eventManager.AddArmyEvent( SetupContext, attackingArmy, attackingIsland, ArmyMovement.attacking, island );
            SetupContext.SaveChanges();

            return RedirectToAction( "SetAttackingArmy", new { islandId = islandId } );
        }

        /// <summary>
        /// À utiliser lorsqu'on ne dispose pas du Layout du StandartViewModel
        /// </summary>
        /// <param name="islandId"></param>
        /// <returns></returns>
        private Island GetIsland( long islandId )
        {
            if( islandId == 0 )
            {
                long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;
                return SetupContext.Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IsCapital && i.Owner.PlayerId == activePlayerId );
            }
            else
            {
                return SetupContext.Islands
                    .Include( i => i.Armies )
                    .ThenInclude( a => a.Regiments )
                    .ThenInclude( r => r.Unit )
                    .Include( i => i.AllRessources )
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                    .SingleOrDefault( i => i.IslandId == islandId );
            }

        }

        private SetAttackingArmyViewModel CreateSetAttackingArmyViewModel( SetAttackingArmyViewModel model, long islandId )
        {
            long activePlayerId = SetupContext.GetPlayer( User.GetUserId() ).PlayerId;

            Island currentIsland = SetupContext.GetIsland( islandId, activePlayerId );

            model.CurrentDefenseArmy = SetupContext.Armies
                                    .Include( a => a.Island )
                                    .Include( a => a.Regiments )
                                    .ThenInclude( r => r.Unit )
                                    .ThenInclude( r => r.UnitStatistics )
                                    .Where( a => a.Island.IslandId == currentIsland.IslandId && a.ArmyState == ArmyState.defense )
                                    .SingleOrDefault();

            model.EnnemyIslands =
                SetupContext.Islands
                    .Include( i => i.Owner )
                    .Include( i => i.Coordinates )
                .Where( i => i.Owner.PlayerId != activePlayerId )
                .ToList();

            SetupContext.FillStandardVM( model, activePlayerId, islandId );
            return model;
        }

        /// <summary>
        /// Creates an ArmyViewModel containing all available units and the armies contained on the island
        /// </summary>
        /// <param name="islandId">The id of the Island examined</param>
        /// <returns>An ArmyViewModel containing all available units and the armies contained on the island</returns>
        private ArmyViewModel CreateArmyViewModel( long islandId )
        {
            return CreateArmyViewModel( islandId, new ArmyViewModel() );
        }

        private ArmyViewModel CreateArmyViewModel( long islandId, ArmyViewModel model )
        {
            List<Army> currentIslandArmies = SetupContext.Islands.Include( i => i.Armies ).ThenInclude( a => a.Regiments )
                .ThenInclude( r => r.Unit ).ThenInclude( u => u.UnitStatistics )
                .Single( i => i.IslandId == islandId ).Armies.Where( a => a.ArmyState != ArmyState.obsolete ).ToList();
            LevelManager levelManager = new LevelManager( SetupContext );


            model.AvailableUnits = new List<Unit>();
            //model.AvailableUnits = SetupContext.Units.Include( u => u.UnitCost ).Include( u => u.UnitStatistics ).
            //    Where( u => levelManager.IsUnitAvailable( u, islandId) ).ToList();
            foreach( Unit unit in SetupContext.Units.Include( u => u.UnitCost ).Include( u => u.UnitStatistics ).Include( u => u.Requirements ).ToList() )
            {
                if( levelManager.GetAvailablility( unit, islandId ).IsItemAvailable )
                    model.AvailableUnits.Add( unit );
            }
            model.CurrentIslandArmies = currentIslandArmies;

            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return model;
        }
        public IActionResult SendRessources( SetSendRessourcesViewModel model, long islandId = 0 )
        {
            Island senderIsland = GetIsland( islandId );
            Ressource sendingRessource = new Ressource() { Wood = model.Wood, Metal = model.Metal, Cristal = model.Cristal, Magic = model.Magic };

            if( model.NumberOfTransportorToSend > 0 && RessourceManager.IsEnough( senderIsland.AllRessources, sendingRessource ) && ((model.Wood + model.Cristal + model.Metal + model.Magic) < model.CapacityOfCarrier) )
            {
                EventManager eventManager = new EventManager( SetupContext, new EventPackManager( SetupContext ) );

                Island island = SetupContext.Islands.Include( i => i.Owner )
                                                    .Include( i => i.Coordinates )
                                                    .Where( i => i.IslandId == model.Target ).FirstOrDefault();
                ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );

                Dictionary<string, int> unitsToSend = new Dictionary<string, int>();
                unitsToSend.Add( "carrier", model.NumberOfTransportorToSend );
                Army SenderRessourceArmy = am.CreateArmy( unitsToSend, senderIsland );
                SetupContext.Armies.Add( SenderRessourceArmy );
                SetupContext.Ressources.Add( sendingRessource );
                RessourceManager.RemoveRessource( senderIsland.AllRessources, sendingRessource );
                SetupContext.SaveChanges();
                eventManager.AddArmyEvent( SetupContext, SenderRessourceArmy, senderIsland, ArmyMovement.sendingRessources, island, sendingRessource );
                SetupContext.SaveChanges();

                return RedirectToAction( "SetSendRessources", new { islandId = islandId } );
            }
            else
            {
                if( model.NumberOfTransportorToSend < 0 )
                    ModelState.AddModelError( "UnitsToSend", "Les unités ne peuvent pas être négatives." );
                else if( model.NumberOfTransportorToSend == 0 )
                    ModelState.AddModelError( "UnitsToSend", "Aucune unité sélectionnée." );
                else if( model.Target == 0 )
                {
                    ModelState.AddModelError( "UnitsToSend", "Aucune cible sélectionnée." );
                }
                else if( RessourceManager.IsEnough( senderIsland.AllRessources, sendingRessource ) )
                {
                    ModelState.AddModelError( "UnitsToSend", "Vous n'avez pas assez de ressources à envoyer." );

                }
                else if( (model.Wood + model.Cristal + model.Metal + model.Magic) > model.CapacityOfCarrier )
                {

                    ModelState.AddModelError( "UnitsToSend", "Vous ne pouvez pas envoyer pus de ressources que ne peuvent transporter vos transporteurs soit : " + model.CapacityOfCarrier );
                }
                else
                    ModelState.AddModelError( "UnitsToSend", "Vous ne pouvez pas envoyer plus d'unités que vous n'en possédez." );

                return RedirectToAction( "SetSendRessources", new { islandId = islandId } );
            }
        }
        private SetSendRessourcesViewModel CreateSetSendRessourceViewModel( long islandId )
        {
            return CreateSetSendRessourceViewModel( islandId, new SetSendRessourcesViewModel() );
        }
        private SetSendRessourcesViewModel CreateSetSendRessourceViewModel( long islandId, SetSendRessourcesViewModel model )
        {
            model.SendableIslands = SetupContext.Islands.Include( i => i.Coordinates ).Include( i => i.Owner ).Where( i => i.Owner != null ).ToList();

            //model.CurrentTransportorArmy = SetupContext.Islands.Include( i => i.Armies ).ThenInclude( a => a.Regiments )
            //    .ThenInclude( r => r.Unit ).ThenInclude( u => u.UnitStatistics )
            //    .Single( i => i.IslandId == islandId ).Armies.Single( a => a.ArmyState != ArmyState.obsolete );
            ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );
            Army defenseArmy = am.GetCurrentDefenseArmy( islandId );
            if( defenseArmy == null )
            {
                model.NumberOfTransportorToSend = 0;
            }
            else
            {
                model.NumberOfTransportorToSend = SetupContext.Islands
                                            .Single( i => i.IslandId == islandId )
                                            .Armies.Single( a => a.ArmyState == ArmyState.defense )
                                            .Regiments.Where( r => r.Unit.UnitName == UnitName.carrier )
                                            .Select( r => r.Number ).SingleOrDefault();
            }

            if( model.NumberOfTransportorToSend > 0 )
            {
                model.CapacityOfCarrier = SetupContext.Islands.Include( i => i.Armies ).ThenInclude( i => i.Regiments ).ThenInclude( i => i.Unit ).ThenInclude( i => i.UnitStatistics )
                                 .Single( i => i.IslandId == islandId )
                                 .Armies.Single( a => a.ArmyState == ArmyState.defense )
                                 .Regiments.Where( r => r.Unit.UnitName == UnitName.carrier )
                                 .Select( r => r.Unit.UnitStatistics.Capacity ).First();
            }
            else
            {
                model.CapacityOfCarrier = 0;
            }


            model.SenderIsland = GetIsland( islandId );
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return model;
        }
        public IActionResult SetSendRessources( long islandId )
        {
            return View( CreateSetSendRessourceViewModel( islandId ) );
        }
        public IActionResult SetColonisation( long islandId = 0 )
        {
            SetColonisationViewModel model = new SetColonisationViewModel();
            model.PossibleColonisableIslands = SetupContext.Islands.Include( i => i.Coordinates ).Include( i => i.Owner ).Where( i => i.Owner == null ).ToList();
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );
            Army defenseArmy = am.GetCurrentDefenseArmy( islandId );
            if( defenseArmy == null )
            {
                model.HasApprentice = false;
            }
            else
            {
                model.HasApprentice = SetupContext.Islands
                                    .Single( i => i.IslandId == islandId )
                                    .Armies.Single( a => a.ArmyState == ArmyState.defense )
                                    .Regiments.Any( r => r.Unit.UnitName == UnitName.apprentice );

                model.CapacityOfCarrier = SetupContext.Islands.Include( i => i.Armies ).ThenInclude( i => i.Regiments ).ThenInclude( i => i.Unit ).ThenInclude( i => i.UnitStatistics )
                  .Single( i => i.IslandId == islandId )
                  .Armies.Single( a => a.ArmyState == ArmyState.defense )
                  .Regiments.Where( r => r.Unit.UnitName == UnitName.apprentice )
                  .Select( r => r.Unit.UnitStatistics.Capacity ).First();
            }

            model.SenderIsland = GetIsland( islandId );
            return View( model );
        }
        public IActionResult SendColonisation( SetColonisationViewModel model, long islandId = 0 )
        {
            Island senderIsland = GetIsland( islandId );
            Ressource sendingRessource = new Ressource() { Wood = model.Wood, Metal = model.Metal, Cristal = model.Cristal, Magic = model.Magic };

            if( RessourceManager.IsEnough( senderIsland.AllRessources, sendingRessource ) && ((model.Wood + model.Cristal + model.Metal + model.Magic) < model.CapacityOfCarrier) )
            {
                EventManager eventManager = new EventManager( SetupContext, new EventPackManager( SetupContext ) );
                ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );

                Island island = SetupContext.Islands.Include( i => i.Owner )
                                                    .Include( i => i.Coordinates )
                                                    .Where( i => i.IslandId == model.Target ).FirstOrDefault();

                Dictionary<string, int> unitsToSend = new Dictionary<string, int>();
                unitsToSend.Add( "apprentice", 1 );
                Army SenderApprenticeArmy = am.CreateArmy( unitsToSend, senderIsland );
                SetupContext.Armies.Add( SenderApprenticeArmy );
                SetupContext.Ressources.Add( sendingRessource );
                RessourceManager.RemoveRessource( senderIsland.AllRessources, sendingRessource );
                SetupContext.SaveChanges();
                eventManager.AddArmyEvent( SetupContext, SenderApprenticeArmy, senderIsland, ArmyMovement.colonising, island, sendingRessource );
                SetupContext.SaveChanges();

                return RedirectToAction( "SetAttackingArmy", new { islandId = islandId } );
            }
            else
            {
                if( model.Target == 0 )
                {
                    ModelState.AddModelError( "UnitsToSend", "Aucune cible sélectionnée." );
                }
                else if( RessourceManager.IsEnough( senderIsland.AllRessources, sendingRessource ) )
                {
                    ModelState.AddModelError( "UnitsToSend", "Vous n'avez pas assez de ressources à envoyer." );

                }
                else if( (model.Wood + model.Cristal + model.Metal + model.Magic) > model.CapacityOfCarrier )
                {

                    ModelState.AddModelError( "UnitsToSend", "Vous ne pouvez pas envoyer pus de ressources que ne peuvent transporter votre apprenti soit : " + model.CapacityOfCarrier );
                }
                else
                    ModelState.AddModelError( "UnitsToSend", "Vous ne pouvez pas envoyer plus d'unités que vous n'en possédez." );

                return RedirectToAction( "SetAttackingArmy", new { islandId = islandId } );
            }
        }
        public IActionResult SetMovement( long islandId )
        {
            long playerId = SetupContext.GetPlayer( User );
            SetMovementArmyViewModel movementViewModel = new SetMovementArmyViewModel();

            SetupContext.FillStandardVM( movementViewModel, playerId, islandId );
            movementViewModel.CurrentDefenseArmy = new ArmyManager( SetupContext, new BonusManager( SetupContext ) ).GetCurrentDefenseArmy( islandId );

            return View( movementViewModel );

        }

        public IActionResult MoveArmy( SetMovementArmyViewModel model, long islandId )
        {
            Island senderIsland = GetIsland( islandId );

            if( model.UnitsToSend.Count( kvp => kvp.Value == 0 ) != model.UnitsToSend.Count() || model.UnitsToSend.Any( kvp => kvp.Value < 0 ) )
            {
                EventManager eventManager = new EventManager( SetupContext, new EventPackManager( SetupContext ) );

                Island islandTarget = SetupContext.Islands.Include( i => i.Owner )
                                                    .Include( i => i.Coordinates )
                                                    .Where( i => i.IslandId == model.TargetIslandId ).FirstOrDefault();

                ArmyManager am = new ArmyManager( SetupContext, new BonusManager( SetupContext ) );

                Army MovingArmy = am.CreateArmy( model.UnitsToSend, senderIsland );
                SetupContext.Armies.Add( MovingArmy );
                SetupContext.SaveChanges();

                eventManager.AddArmyEvent( SetupContext, MovingArmy, senderIsland, ArmyMovement.moving, islandTarget );
                SetupContext.SaveChanges();

                return RedirectToAction( "SetMovement", new { islandId = islandId } );
            }
            else
            {
                return RedirectToAction( "SetMovement", new { islandId = islandId } );
            }
        }

    }


}

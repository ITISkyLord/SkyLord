using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Services
{
    public class TechnologyManager
    {
        public ILevelContext CurrentContext { get; set; }
        public LevelManager LevelManager { get; set; }
        public BonusManager BonusManager { get; set; }

        public TechnologyManager( ILevelContext currentContext, LevelManager levelManager, BonusManager bonusManager )
        {
            CurrentContext = currentContext;
            BonusManager = bonusManager;
            LevelManager = levelManager;
        }

        public bool AddTechnology( TechnologyName technologyName, long playerId, long currentIslandId )
        {
            Player currentPlayer = CurrentContext.Players.Include( p => p.Technologies ).Single( p => p.PlayerId == playerId );
            // If the technology already exists don't add it and return false
            if ( GetPlayersTechnologies( playerId ).Any( t => t.TechnologyName == technologyName ) )
            {
                return false;
            }

            // Create and add technology on the player
            Technology technologyToAdd;
            technologyToAdd = new Technology
            {
                Name = TechnologyNameToName( technologyName ),
                TechnologyName = technologyName,
                Level = CurrentContext.TechnologyLevels.Include( tl => tl.Cost ).First( tl => tl.TechnologyName == technologyName && tl.Number == 1 )
            };
            currentPlayer.Technologies.Add( technologyToAdd );

            return true;
        }

        public bool LevelUpTechnology( TechnologyName technologyNameToLevelUp, long playerId, long currentIslandId )
        {
            Technology technologyToLevelUp = GetPlayersTechnologies( playerId ).Single( t => t.TechnologyName == technologyNameToLevelUp );

            if ( LevelManager.GetNextLevelAvailablility( technologyToLevelUp, currentIslandId ).IsItemAvailable )
            {
                return LevelManager.LevelUp( technologyToLevelUp );
            }
            return false;
        }

        public bool IsEnoughForNextLevel( TechnologyName technologyNameToLevelUp, long islandId, long playerId )
        {
            Technology technologyToLevelUp = GetPlayersTechnologies( playerId ).Single( t => t.TechnologyName == technologyNameToLevelUp );

            Level nextLevel = LevelManager.FindNextLevel( technologyToLevelUp.Level );
            if ( nextLevel != null )
            {
                return RessourceManager.IsEnough( CurrentContext.GetIsland( islandId, playerId ).AllRessources, nextLevel.Cost );
            }
            return false;
        }

        public bool IsEnoughForFirstLevel( TechnologyName technologyNameToLevelUp, long islandId, long playerId )
        {
            TechnologyLevel technologyLevel = CurrentContext.TechnologyLevels.Include( tl => tl.Cost )
                .Single( tl => tl.TechnologyName == technologyNameToLevelUp && tl.Number == 1 );

            return RessourceManager.IsEnough( CurrentContext.GetIsland( islandId, playerId ).AllRessources, technologyLevel.Cost );
        }

        public string TechnologyNameToName( TechnologyName technologyName )
        {
            string name;
            switch ( technologyName )
            {
                case TechnologyName.armor:
                    name = "Armure";
                    break;
                case TechnologyName.claws:
                    name = "Griffes";
                    break;
                case TechnologyName.hold:
                    name = "Cale";
                    break;
                case TechnologyName.levitation:
                    name = "Lévitation";
                    break;
                case TechnologyName.magicalProtection:
                    name = "Robe de mage";
                    break;
                case TechnologyName.magicalStaff:
                    name = "Bâton de mage";
                    break;
                case TechnologyName.magicBoots:
                    name = "Bottes magiques";
                    break;
                case TechnologyName.plating:
                    name = "Blindage";
                    break;
                case TechnologyName.propulsion:
                    name = "Propulsion";
                    break;
                case TechnologyName.shell:
                    name = "Carapace";
                    break;
                case TechnologyName.weapons:
                    name = "Armes";
                    break;
                case TechnologyName.wings:
                    name = "Ailes";
                    break;
                case TechnologyName.conquest:
                    name = "Conquête";
                    break;
                default:
                    name = "Error";
                    break;
            }
            return name;
        }


        internal List<Technology> GetPlayersTechnologies( long playerId )
        {
            List<Technology> technologyList = CurrentContext.Players.Include( p => p.Technologies ).ThenInclude( t => t.Level ).ThenInclude( tl => tl.Requirements )
                .Single( p => p.PlayerId == playerId ).Technologies.ToList();
            foreach ( Technology technology in technologyList )
            {
                technology.Level.Cost = CurrentContext.Technologies.Include( t => t.Level ).ThenInclude( tl => tl.Cost )
                    .Single( t => t.TechnologyId == technology.TechnologyId ).Level.Cost;
            }

            return technologyList;
        }

        public List<TechnologyLevel> GetAvailableTechnologies()
        {
            List<TechnologyLevel> availableTechnologies = new List<TechnologyLevel>();
            foreach ( TechnologyName technologyName in Enum.GetValues( typeof( TechnologyName ) ) )
            {
                if ( technologyName != TechnologyName.none )
                {
                    TechnologyLevel firstLevel = CurrentContext.TechnologyLevels.Include( l => l.Cost ).Include( l => l.Requirements)
                        .Where( l => l.TechnologyName == technologyName && l.Number == 1 ).Single();

                    availableTechnologies.Add( firstLevel );
                }
            }
            return availableTechnologies;
        }
    }
}

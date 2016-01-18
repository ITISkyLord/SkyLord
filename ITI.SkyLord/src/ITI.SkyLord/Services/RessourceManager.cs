using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITI.SkyLord.Services
{
    public static class RessourceManager
    {

        /// <summary>
        /// Resolve ressources accumulated in the given island
        /// </summary>
        /// <param name="island">Current Island</param>
        public static void ResolveResources( Island island, SetupContext context )
        {
            Ressource ressourcesGathered = new Ressource();
            List<Building> ressourcesBuildings = GetRessourcesBuildings(island.Buildings);
            DateTime checkTime = GetLastCheckTime(island.IslandId, context);

            if( checkTime.Equals( new DateTime() ) && ressourcesBuildings != null ) // If null, checkTime will be set after the if{} anyway for initialisation
            {
                // Calculate gap between last time and now
                int gap = (int)(DateTime.Now - checkTime).TotalSeconds;

                // Get all fields
                Building cristalField = ressourcesBuildings.Where( b => b.BuildingName == BuildingName.cristalField ).Single();
                Building woodField = ressourcesBuildings.Where( b => b.BuildingName == BuildingName.woodField ).Single();
                Building metalField = ressourcesBuildings.Where( b => b.BuildingName == BuildingName.metalField ).Single();
                Building magicField = ressourcesBuildings.Where( b => b.BuildingName == BuildingName.magicField ).Single();

                // Create an object Ressource that contain the amount earned since last check
                ressourcesGathered.Cristal = gap * GetProductionEachSecond( (FieldLevel)cristalField.Level );
                ressourcesGathered.Wood = gap * GetProductionEachSecond( (FieldLevel)woodField.Level );
                ressourcesGathered.Metal = gap * GetProductionEachSecond( (FieldLevel)metalField.Level );
                ressourcesGathered.Magic = gap * GetProductionEachSecond( (FieldLevel)magicField.Level );

                AddRessource( island.AllRessources, ressourcesGathered );
                // case if you got robbed, remove ressources ?
            }

            SetLastCheckTime( island.IslandId, context );
            context.SaveChanges();
        }

        #region Resolve Resources Helpers
        private static int GetProductionEachSecond( FieldLevel field )
        {
            return field.Production / 3600;
        }
        private static void SetLastCheckTime( long islandId, SetupContext context )
        {
            var lastCheck = context.ResourceLastTimeCheck.Where( r => r.IslandId == islandId ).SingleOrDefault();
            if( lastCheck == null )
            {
                lastCheck = new RessourceLastTimeCheck { IslandId = islandId };
            }
            lastCheck.CheckTime = DateTime.Now;
        }
        private static DateTime GetLastCheckTime( long islandId, SetupContext context )
        {
            var lastCheck = context.ResourceLastTimeCheck.Where( r => r.IslandId == islandId ).SingleOrDefault();
            if( lastCheck != null )
            {
                return lastCheck.CheckTime;
            }
            return new DateTime();
        }
        private static List<Building> GetRessourcesBuildings( IList<Building> buildings )
        {
            if( buildings == null )
                return null;
            return buildings
                .Where( b => b.BuildingName == BuildingName.cristalField ||
                                         b.BuildingName == BuildingName.woodField ||
                                         b.BuildingName == BuildingName.magicField ||
                                         b.BuildingName == BuildingName.metalField
                                         ).ToList();
        }
        #endregion

        public static bool IsEnough( Ressource ressourceToCheck, Ressource cost )
        {
            return IsEnough
                (
                    ressourceToCheck,
                    cost.Wood,
                    cost.Metal,
                    cost.Cristal,
                    cost.Magic
                );
        }

        public static bool IsEnough( Ressource ressourceToCheck, int wood, int metal, int cristal, int magic )
        {
            Ressource clone = CloneRessource( ressourceToCheck );

            if( (clone.Wood - wood) < 0 ) return false;
            if( (clone.Metal - metal) < 0 ) return false;
            if( (clone.Cristal - cristal) < 0 ) return false;
            if( (clone.Magic - magic) < 0 ) return false;

            return true;
        }

        public static bool RemoveRessource( Ressource ressourceToChange, Ressource cost )
        {
            return RemoveRessource
                (
                    ressourceToChange,
                    cost.Wood,
                    cost.Metal,
                    cost.Cristal,
                    cost.Magic
                );
        }

        public static bool AddRessource( Ressource ressourceToChange, Ressource cost )
        {
            return AddRessource
                (
                    ressourceToChange,
                    cost.Wood,
                    cost.Metal,
                    cost.Cristal,
                    cost.Magic
                );
        }

        public static bool AddRessource( Ressource ressourceToChange, int wood, int metal, int cristal, int magic )
        {
            if( wood < 0 || metal < 0 || cristal < 0 || magic < 0 ) throw new ArgumentException( "The parameters must me positive" );
            if( ressourceToChange == null ) throw new ArgumentException( "The ressource to change cannot be null" );

            ressourceToChange.Wood += wood;
            ressourceToChange.Metal += metal;
            ressourceToChange.Cristal += cristal;
            ressourceToChange.Magic += magic;

            return true;
        }

        public static bool RemoveRessource( Ressource ressourceToChange, int wood, int metal, int cristal, int magic )
        {
            if( wood < 0 || metal < 0 || cristal < 0 || magic < 0 ) throw new ArgumentException( "The parameters must me positive" );
            if( ressourceToChange == null ) throw new ArgumentException( "The ressource to change cannot be null" );

            if( !IsEnough( ressourceToChange, wood, metal, cristal, magic ) )
            {
                return false;
            }

            ressourceToChange.Wood -= wood;
            ressourceToChange.Metal -= metal;
            ressourceToChange.Cristal -= cristal;
            ressourceToChange.Magic -= magic;

            return true;
        }

        public static Ressource CloneRessource( Ressource source )
        {
            return new Ressource
            {
                Wood = source.Wood,
                Metal = source.Metal,
                Cristal = source.Cristal,
                Magic = source.Magic
            };
        }
    }
}

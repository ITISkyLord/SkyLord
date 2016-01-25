using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

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
            List<Building> ressourcesBuildings = GetRessourcesBuildings(island.Buildings.ToList());
            DateTime checkTime = GetLastCheckTime(island);

            if( !checkTime.Equals( new DateTime() ) && ressourcesBuildings != null ) // If null, checkTime will be set after the if{} anyway for initialisation
            {
                // Calculate gap between last time and now
                int gap = (int)(DateTime.Now - checkTime).TotalSeconds;

                // Get all fields
                List<Building>  woodFields = ressourcesBuildings.Where( b => b.BuildingName == BuildingName.woodField ).ToList();
                List<Building>  metalFields = ressourcesBuildings.Where( b => b.BuildingName == BuildingName.metalField ).ToList();
                List<Building> cristalFields = ressourcesBuildings.Where( b => b.BuildingName == BuildingName.cristalField ).ToList();
                List<Building>  magicFields = ressourcesBuildings.Where( b => b.BuildingName == BuildingName.magicField ).ToList();

                // Create an object Ressource that contain the amount earned since last check
                foreach( Building woodField in woodFields )
                    ressourcesGathered.Wood += (int)(gap * GetProductionEachSecond( (FieldLevel)woodField.Level ));
                foreach( Building metalField in metalFields )
                    ressourcesGathered.Metal += (int)(gap * GetProductionEachSecond( (FieldLevel)metalField.Level ));
                foreach( Building cristalField in cristalFields )
                    ressourcesGathered.Cristal += (int)(gap * GetProductionEachSecond( (FieldLevel)cristalField.Level ));
                foreach( Building magicField in magicFields )
                    ressourcesGathered.Magic += (int)(gap * GetProductionEachSecond( (FieldLevel)magicField.Level ));

                // Base resources production
                double baseProduction = 0.002; // 10 each hour
                ressourcesGathered.Wood += (int)(gap * baseProduction);
                ressourcesGathered.Metal += (int)(gap * baseProduction);
                ressourcesGathered.Cristal += (int)(gap * baseProduction);
                ressourcesGathered.Magic += (int)(gap * baseProduction);

                // Add ressources only if all ressources that exist are able to be added
                if( (ressourcesGathered.Metal >= 1 || metalFields.Count < 1) && (ressourcesGathered.Magic >= 1 || magicFields.Count < 1) && (ressourcesGathered.Wood >= 1 || woodFields.Count < 1) && (ressourcesGathered.Cristal >= 1 || cristalFields.Count < 1) )
                {
                    AddRessource( island.AllRessources, ressourcesGathered );
                    SetLastCheckTime( island );
                }
                // In case you got robbed, remove ressources ?
            }

            // set checktime for initialisation
            if( checkTime.Equals( new DateTime() ) && ressourcesBuildings != null )
            {
                SetLastCheckTime( island );
            }

            context.SaveChanges();
        }

        #region Resolve Resources Helpers
        private static double GetProductionEachSecond( FieldLevel field )
        {
            return (double)field.Production / 3600f;
        }
        private static void SetLastCheckTime( Island island )
        {
            island.CheckTime = DateTime.Now;
        }
        private static DateTime GetLastCheckTime( Island island )
        {
            if( island.CheckTime != null )
            {
                return island.CheckTime;
            }
            return new DateTime();
        }
        private static List<Building> GetRessourcesBuildings( List<Building> buildings )
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
        //
        // Créer une méthode qui rend le nombre max de Cost payable avec les fonds courants de l'ile
        //

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

using ITI.SkyLord.Models.Entity_Framework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITI.SkyLord.Services
{
    public class RessourceManager
    {
        public LevelContext CurrentContext { get; }

        public RessourceManager( LevelContext levelContext )
        {
            CurrentContext = levelContext;
        }

        public bool IsEnough( Ressource ressourceToCheck, Ressource cost )
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

        bool IsEnough( Ressource ressourceToCheck, int wood, int metal, int cristal, int magic )
        {
            Ressource clone = CloneRessource( ressourceToCheck );

            if ( ( clone.Wood - wood ) < 0 ) return false;
            if ( ( clone.Metal - metal ) < 0 ) return false;
            if ( ( clone.Cristal - cristal ) < 0 ) return false;
            if ( ( clone.Magic - magic ) < 0 ) return false;

            return true;
        }
        public bool RemoveRessource( Ressource ressourceToChange, Ressource cost )
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

        public bool AddRessource( Ressource ressourceToChange, Ressource cost )
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

        public bool AddRessource( Ressource ressourceToChange, int wood, int metal, int cristal, int magic )
        {
            if ( wood < 0 || metal < 0 || cristal < 0 || magic < 0 ) throw new ArgumentException( "The parameters must me positive" );
            if ( ressourceToChange == null ) throw new ArgumentException( "The ressource to change cannot be null" );

            ressourceToChange.Wood += wood;
            ressourceToChange.Metal += metal;
            ressourceToChange.Cristal += cristal;
            ressourceToChange.Magic += magic;

            return true;
        }

        public bool RemoveRessource( Ressource ressourceToChange, int wood, int metal, int cristal, int magic )
        {
            if ( wood < 0 || metal < 0 || cristal < 0 || magic < 0 ) throw new ArgumentException( "The parameters must me positive" );
            if ( ressourceToChange == null ) throw new ArgumentException( "The ressource to change cannot be null" );

            if ( !IsEnough( ressourceToChange, wood, metal, cristal, magic ) )
            {
                return false;
            }

            ressourceToChange.Wood -= wood;
            ressourceToChange.Metal -= metal;
            ressourceToChange.Cristal -= cristal;
            ressourceToChange.Magic -= magic;

            return true;
        }

        private Ressource CloneRessource( Ressource source )
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

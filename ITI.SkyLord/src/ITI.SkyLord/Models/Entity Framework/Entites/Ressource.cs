using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Ressource
    {
        private int _wood;
        private int _metal;
        private int _cristal;
        private int _magic;

        /// <summary>
        /// Create a Ressource object with quantities
        /// </summary>
        /// <param name="wood">The wood the ressource contains.</param>
        /// <param name="metal">The metal the ressource contains.</param>
        /// <param name="cristal">The cristal the ressource contains.</param>
        /// <param name="magic">The magic the ressource contains.</param>
        public Ressource( int wood, int metal, int cristal, int magic )
        {
            if( wood < 0 || metal < 0 || cristal < 0 || magic < 0) throw new ArgumentOutOfRangeException( "Ressource values cannot be under 0." );
            this.Wood = wood;
            this.Metal = metal;
            this.Cristal = cristal;
            this.Magic = magic;
        }

        /// <summary>
        /// Creates a Ressource object with every quantity at 0
        /// </summary>
        public Ressource()
        {
            this.Wood = 0;
            this.Metal = 0;
            this.Cristal = 0;
            this.Magic = 0;
        }

        #region Properties

        [Key]
        public long RessourceId { get; set; }

        public int Wood
        {
            get
            {
                return _wood;
            }

            set
            {
                _wood = value;
            }
        }

        public int Metal
        {
            get
            {
                return _metal;
            }

            set
            {
                _metal = value;
            }
        }

        public int Cristal
        {
            get
            {
                return _cristal;
            }

            set
            {
                _cristal = value;
            }
        }

        public int Magic
        {
            get
            {
                return _magic;
            }

            set
            {
                _magic = value;
            }
        }

        #endregion

        /// <summary>
        /// Adds to ressources if positive, removes from ressources if negative.
        /// </summary>
        /// <param name="woodQuantity">The wood quantity to add or remove.</param>
        /// <param name="metalQuantity">The metal quantity to add or remove.</param>
        /// <param name="cristalQuantity">The cristal quantity to add or remove.</param>
        /// <param name="magicQuantity">The magic quantity to add or remove.</param>
        public void ChangeRessources(int woodQuantity, int metalQuantity, int cristalQuantity, int magicQuantity )
        {
            // Remplacer par un bool, demander à Spi !
            if ( Wood + woodQuantity < 0 ) throw new ArgumentException( "Wood cannot be negative." );
            if ( Metal + metalQuantity < 0 ) throw new ArgumentException( "Metal cannot be negative." );
            if ( Cristal + cristalQuantity < 0 ) throw new ArgumentException( "Cristal cannot be negative." );
            if ( Magic + magicQuantity < 0 ) throw new ArgumentException( "Magic cannot be negative." );

            Wood += woodQuantity;
            Metal += metalQuantity;
            Cristal += cristalQuantity;
            Magic += magicQuantity;
        }
        /// <summary>
        /// Only for add ressources.
        /// </summary>
        /// <param name="ressource"></param>
        public void ChangeRessources( Ressource ressource, bool b = true )
        {
            if(b)
            {
                Wood += ressource.Wood;
                Metal += ressource.Metal;
                Cristal += ressource.Cristal;
                Magic += ressource.Magic;
            } else
            {
                if( Wood - ressource.Wood < 0 ) throw new ArgumentException( "Wood cannot be negative." );
                if( Metal - ressource.Metal < 0 ) throw new ArgumentException( "Metal cannot be negative." );
                if( Cristal - ressource.Cristal < 0 ) throw new ArgumentException( "Cristal cannot be negative." );
                if( Magic - ressource.Magic < 0 ) throw new ArgumentException( "Magic cannot be negative." );
                Wood -= ressource.Wood;
                Metal -= ressource.Metal;
                Cristal -= ressource.Cristal;
                Magic -= ressource.Magic;
            }


        }

        /// <summary>
        /// Adds or removes wood from ressource.
        /// </summary>
        /// <param name="woodQuantity">The wood quantity to add or remove.</param>
        public void ChangeWood(int woodQuantity )
        {
            if ( Wood + woodQuantity < 0 ) throw new ArgumentException( "Wood cannot be negative." );

            Wood += woodQuantity;
        }
        /// <summary>
        /// Adds or removes metal form ressource.
        /// </summary>
        /// <param name="metalQuantity">The metal quantity to add or remove.</param>
        public void ChangeMetal( int metalQuantity )
        {
            if ( Metal + metalQuantity < 0 ) throw new ArgumentException( "Metal cannot be negative." );

            Metal += metalQuantity;
        }
        /// <summary>
        /// Adds or removes cristal form ressource.
        /// </summary>
        /// <param name="metalQuantity">The cristal quantity to add or remove.</param>
        public void ChangeCristal( int cristalQuantity)
        {
            if ( Cristal + cristalQuantity < 0 ) throw new ArgumentException( "Cristal cannot be negative." );

            Cristal += cristalQuantity;
        }
        /// <summary>
        /// Adds or removes magic form ressource.
        /// </summary>
        /// <param name="metalQuantity">The magic quantity to add or remove.</param>
        public void ChangeMagic( int magicQuantity )
        {
            if ( Magic + magicQuantity < 0 ) throw new ArgumentException( "Magic cannot be negative." );

            Magic += magicQuantity;
        }

        public override bool Equals( object obj )
        {
            Ressource other = obj as Ressource;
            return other != null && (other.Wood == this.Wood && other.Metal == this.Metal && other.Cristal == this.Cristal && other.Magic == this.Magic);
        }

        public override int GetHashCode()
        {
            if( Wood == -1 ) return 0;
            return Wood.GetHashCode();
        }
    }
}
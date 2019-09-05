using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Ressource
    {
        #region Properties

        [Key]
        public long RessourceId { get; set; }

        public int Wood { get; set; }

        public int Metal { get; set; }

        public int Cristal { get; set; }

        public int Magic { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Adds to ressources if positive, removes from ressources if negative.
        /// </summary>
        /// <param name="woodQuantity">The wood quantity to add or remove.</param>
        /// <param name="metalQuantity">The metal quantity to add or remove.</param>
        /// <param name="cristalQuantity">The cristal quantity to add or remove.</param>
        /// <param name="magicQuantity">The magic quantity to add or remove.</param>
        [Obsolete]
        public void ChangeRessources( int woodQuantity, int metalQuantity, int cristalQuantity, int magicQuantity )
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
        [Obsolete]
        public void ChangeRessources( Ressource ressource, bool b = true )
        {
            if ( b )
            {
                Wood += ressource.Wood;
                Metal += ressource.Metal;
                Cristal += ressource.Cristal;
                Magic += ressource.Magic;
            }
            else
            {
                if ( Wood - ressource.Wood < 0 ) throw new ArgumentException( "Wood cannot be negative." );
                if ( Metal - ressource.Metal < 0 ) throw new ArgumentException( "Metal cannot be negative." );
                if ( Cristal - ressource.Cristal < 0 ) throw new ArgumentException( "Cristal cannot be negative." );
                if ( Magic - ressource.Magic < 0 ) throw new ArgumentException( "Magic cannot be negative." );
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
        public void AddWood( int woodQuantity )
        {
            if ( Wood + woodQuantity < 0 ) throw new ArgumentException( "Wood cannot be negative." );

            Wood += woodQuantity;
        }
        /// <summary>
        /// Adds or removes metal form ressource.
        /// </summary>
        /// <param name="metalQuantity">The metal quantity to add or remove.</param>
        public void AddMetal( int metalQuantity )
        {
            if ( Metal + metalQuantity < 0 ) throw new ArgumentException( "Metal cannot be negative." );

            Metal += metalQuantity;
        }
        /// <summary>
        /// Adds or removes cristal form ressource.
        /// </summary>
        /// <param name="metalQuantity">The cristal quantity to add or remove.</param>
        public void AddCristal( int cristalQuantity )
        {
            if ( Cristal + cristalQuantity < 0 ) throw new ArgumentException( "Cristal cannot be negative." );

            Cristal += cristalQuantity;
        }
        /// <summary>
        /// Adds or removes magic form ressource.
        /// </summary>
        /// <param name="metalQuantity">The magic quantity to add or remove.</param>
        public void AddMagic( int magicQuantity )
        {
            if ( Magic + magicQuantity < 0 ) throw new ArgumentException( "Magic cannot be negative." );

            Magic += magicQuantity;
        }

        public void WithdrawMagic(int magicQuantity)
        {
            if (Magic - magicQuantity < 0) throw new ArgumentException("Magic cannot be negative.");

            Magic -= magicQuantity;
        }
        public void WithdrawWood(int woodQuantity)
        {
            if (Wood - woodQuantity < 0) throw new ArgumentException("Wood cannot be negative.");

            Wood -= woodQuantity;
        }
        public void WithdrawCristal(int cristalQuantity)
        {
            if (Cristal - cristalQuantity < 0) throw new ArgumentException("Cristal cannot be negative.");

            Cristal -= cristalQuantity;
        }
        public void WithdrawMetal(int metalQuantity)
        {
            if (Metal - metalQuantity < 0) throw new ArgumentException("Metal cannot be negative.");

            Metal -= metalQuantity;
        }
        #endregion

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
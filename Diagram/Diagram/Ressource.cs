﻿using System;
using System.Collections.Generic;
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
        /// Create ressource
        /// </summary>
        /// <param name="wood">The wood the ressource contains.</param>
        /// <param name="metal">The metal the ressource contains.</param>
        /// <param name="cristal">The cristal the ressource contains.</param>
        /// <param name="magic">The magic the ressource contains.</param>
        public Ressource( int wood, int metal, int cristal, int magic )
        {
            if( wood < 0 || metal < 0 || cristal < 0 || magic < 0) throw new ArgumentOutOfRangeException( "Ressources cannot be under 0." );
            this._wood = wood;
            this._metal = metal;
            this._cristal = cristal;
            this._magic = magic;
        }

        #region Properties
        /// <summary>
        /// Gets the wood of this ressource.
        /// </summary>
        public int Wood
        {
            get
            {
                return _wood;
            }
        }

        /// <summary>
        /// Gets the metal of this ressource.
        /// </summary>
        public int Metal
        {
            get
            {
                return _metal;
            }
        }

        /// <summary>
        /// Gets the cristal of this ressource.
        /// </summary>
        public int Cristal
        {
            get
            {
                return _cristal;
            }
        }

        /// <summary>
        /// Gets the magic of this ressource.
        /// </summary>
        public int Magic
        {
            get
            {
                return _magic;
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
            if ( _wood + woodQuantity < 0 ) throw new ArgumentException( "Wood cannot be negative." );
            if ( _metal + metalQuantity < 0 ) throw new ArgumentException( "Metal cannot be negative." );
            if ( _cristal + cristalQuantity < 0 ) throw new ArgumentException( "Cristal cannot be negative." );
            if ( _magic + magicQuantity < 0 ) throw new ArgumentException( "Magic cannot be negative." );

            _wood += woodQuantity;
            _metal += metalQuantity;
            _cristal += cristalQuantity;
            _magic += magicQuantity;
        }

        /// <summary>
        /// Adds or removes wood from ressource.
        /// </summary>
        /// <param name="woodQuantity">The wood quantity to add or remove.</param>
        public void ChangeWood(int woodQuantity )
        {
            if ( _wood + woodQuantity < 0 ) throw new ArgumentException( "Wood cannot be negative." );

            _wood += woodQuantity;
        }
        /// <summary>
        /// Adds or removes metal form ressource.
        /// </summary>
        /// <param name="metalQuantity">The metal quantity to add or remove.</param>
        public void ChangeMetal( int metalQuantity )
        {
            if ( _metal + metalQuantity < 0 ) throw new ArgumentException( "Metal cannot be negative." );

            _metal += metalQuantity;
        }
        /// <summary>
        /// Adds or removes cristal form ressource.
        /// </summary>
        /// <param name="metalQuantity">The cristal quantity to add or remove.</param>
        public void ChangeCristal( int cristalQuantity)
        {
            if ( _cristal + cristalQuantity < 0 ) throw new ArgumentException( "Cristal cannot be negative." );

            _cristal += cristalQuantity;
        }
        /// <summary>
        /// Adds or removes magic form ressource.
        /// </summary>
        /// <param name="metalQuantity">The magic quantity to add or remove.</param>
        public void ChangeMagic( int magicQuantity )
        {
            if ( _magic + magicQuantity < 0 ) throw new ArgumentException( "Magic cannot be negative." );

            _magic += magicQuantity;
        }
    }
}
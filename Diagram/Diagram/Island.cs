using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Island
    {
      //  private readonly int _id;
        private string _name;
        private readonly List<Army> _armies; // Une île n'a qu'une armée en défense, peut en avoir plusieurs en mouvement et en suppport.
        private Player _owner;
        private readonly List<Building> _buildings;
        private Ressource _allRessources;
        private readonly Coordinate _coordinates;
        private int _loyalty;
        private bool _isCapital;

        /// <summary>
        /// Create a new island.
        /// </summary>
        /// <param name="name">The name of island.</param>
        /// <param name="coordinates">Coordinates X and Y of the island.</param>
        /// <param name="capital">True if it's the capital or false if not.</param>
        /// <param name="owner">The owner of the island.</param>
        internal Island( string name, Coordinate coordinates, bool capital, Player owner = null )
        {
            if( name.Length > 50 ) throw new ArgumentOutOfRangeException( " Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment." );
            this._name = name;
            this._owner = owner;
            this._armies = new List<Army>();
            this._buildings = new List<Building>();
            this._allRessources = new Ressource(10000, 1000, 5000, 1000);
            this._coordinates = coordinates;
            this._loyalty = 100;
            this._isCapital = capital;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the name of island.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the owner of the island.
        /// </summary>
        public Player Owner
        {
            get
            {
                return _owner;
            }

            set
            {
                _owner = value;
            }
        }

        /// <summary>
        /// Gets the list of armies of the island.
        /// </summary>
        public List<Army> Armies
        {
            get
            {
                return _armies;
            }
        }

        /// <summary>
        /// Gets the list of buildlings of the island.
        /// </summary>
        public List<Building> Buildings
        {
            get
            {
                return _buildings;
            }
        }

        /// <summary>
        /// Gets ressources of the island. Use methods of Ressources to update ressources.
        /// </summary>
        public Ressource AllRessources
        {
            get
            {
                return _allRessources;
            }

        }

        /// <summary>
        /// Gets the coordinates X and Y of the island.
        /// </summary>
        public Coordinate Coordinates
        {
            get
            {
                return _coordinates;
            }
        }

        /// <summary>
        /// Gets or sets the Loyalty of the island.
        /// </summary>
        public int Loyalty
        {
            get
            {
                return _loyalty;
            }

            set
            {
                _loyalty = value;
            }
        }

        /// <summary>
        /// Gets or sets if this island is the capital of the island.
        /// </summary>
        public bool IsCapital
        {
            get
            {
                return _isCapital;
            }

            set
            {
                _isCapital = value;
            }
        } 
        #endregion
    }
}

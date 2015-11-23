using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Island
    {
        private string _name;
        private List<Army> _armies; // Une île n'a qu'une armée en défense, peut en avoir plusieurs en mouvement et en suppport.
        private Player _owner;
        private List<Building> _buildings;
        private Ressource _allRessources;
        private Coordinate _coordinates;
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
            this.Name = name;
            this.Owner = owner;
            this.Armies = new List<Army>();
            this.Buildings = new List<Building>();
            this.AllRessources = new Ressource(10000, 1000, 5000, 1000);
            this.Coordinates = coordinates;
            this.Loyalty = 100;
            this.IsCapital = capital;
        }

        #region Properties
        [Key]
        public long IslandId { get; set; }

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

        public List<Army> Armies
        {
            get
            {
                return _armies;
            }

            set
            {
                _armies = value;
            }
        }

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

        public List<Building> Buildings
        {
            get
            {
                return _buildings;
            }

            set
            {
                _buildings = value;
            }
        }

        public Ressource AllRessources
        {
            get
            {
                return _allRessources;
            }

            set
            {
                _allRessources = value;
            }
        }

        public Coordinate Coordinates
        {
            get
            {
                return _coordinates;
            }

            set
            {
                _coordinates = value;
            }
        }

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

        /// <summary>
        /// Gets or sets the name of island.
        /// </summary>
        #endregion
    }
}

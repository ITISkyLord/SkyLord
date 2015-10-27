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
        private List<Army> _armies; // Une île n'a qu'une armée en défense, peut en avoir plusieurs en mouvement et en suppport.
        private Player _owner;
        private List<Building> _buildings;
        private Ressource _allRessources;
        private readonly Coordinate _coordinates;
        private int _loyalty;
        private bool _capital;

        internal Island( string name, Coordinate coordinates, bool capital, Player owner = null )
        {
            if( name.Length > 50 ) throw new ArgumentOutOfRangeException( " Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment." );
            this._name = name;
            this._owner = owner;
            this._armies = new List<Army>();
            this._buildings = new List<Building>();
            this._allRessources = new Ressource();
            this._coordinates = coordinates;
            this._loyalty = 100;
            this._capital = capital;
        }

        #region Properties
        //public int Id
        //{
        //    get
        //    {
        //        return _id;
        //    }
        //}
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
        public bool Capital
        {
            get
            {
                return _capital;
            }

            set
            {
                _capital = value;
            }
        } 
        #endregion
    }
}

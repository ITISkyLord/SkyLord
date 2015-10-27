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
        private Mage _mage;
        private Player _owner;
        private List<Building> _buildings;
        private Ressource _allRessources;
        private readonly Coordinate _coordinates;
        private int _loyalty;
        private bool _capital;

        public Island( string name, Player ower, List<Army> armies, Mage mage, List<Building> buildings, Coordinate coordinates, bool capital )
        {
            this._name = name;
            this._owner = ower;
            this._armies = armies;
            this._mage = mage;
            this._buildings = buildings;
            this._allRessources = new Ressource(1000, 1000, 1000, 1000);
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
        public Mage Mage
        {
            get
            {
                return _mage;
            }

            set
            {
                _mage = value;
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

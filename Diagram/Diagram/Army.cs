using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Army
    {
      //  private int _id;
       // private Regiment _regiment;
        private ArmyState _armyState;
        private Island _island;
        private readonly Dictionary<Unit, int> _regiments;

        public Army( ArmyState armyState, Island island )
        {
            this._island = island;
            //this._regiment = new Regiment();
            this._armyState = armyState;
            this._regiments = new Dictionary<Unit, int>();
        }


        //public Regiment Regiment
        //{
        //    get
        //    {
        //        return _regiment;
        //    }

        //    set
        //    {
        //        _regiment = value;
        //    }
        //}

        #region Properties
        public ArmyState ArmyState
        {
            get
            {
                return _armyState;
            }

            set
            {
                _armyState = value;
            }
        }

        public Dictionary<Unit, int> Regiments
        {
            get
            {
                return _regiments;
            }
        }

        public Island Island
        {
            get
            {
                return _island;
            }

            set
            {
                _island = value;
            }
        } 
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Army
    {
      //  private int _id;
        private List<Regiment> _regiments;
        private ArmyState _armyState;

        public Army( List<Regiment> regiments, ArmyState armyState )
        {
            this._regiments = regiments;
            this._armyState = armyState;
        }
        public List<Regiment> Regiments
        {
            get
            {
                return _regiments;
            }

            set
            {
                _regiments = value;
            }
        }

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
    }
}
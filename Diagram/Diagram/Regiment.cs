using ITI.SkyLord.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class Regiment
    {
        private int _regimentId;
        private Unit _unit;
        private int _number;

        public Regiment( Unit unit, int number )
        {
            if ( number < 1 ) throw new ArgumentOutOfRangeException( "number", "The number of units cannot be under 1." );

            _unit = unit;
            _number = number;
        }

        #region Properties
        public int RegimentId
        {
            get
            {
                return _regimentId;
            }

            set
            {
                _regimentId = value;
            }
        }

        public Unit Unit
        {
            get
            {
                return _unit;
            }

            set
            {
                _unit = value;
            }
        }

        public int Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = value;
            }
        }

        internal string Name
        {
            get
            {
                return _unit.Name;
            }
        }
        #endregion
    }
}

using ITI.SkyLord.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ITI.SkyLord
{
    public class Regiment
    {
        private Unit _unit;
        private int _number;

        public Regiment( Unit unit, int number )
        {
            if ( number < 1 ) throw new ArgumentOutOfRangeException( "number", "The number of units cannot be under 1." );

            Unit = unit;
            Number = number;
        }

        #region Properties
        [Key]
        public long RegimentId { get; set; }


        internal string Name { get; set; }

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
        #endregion
    }
}

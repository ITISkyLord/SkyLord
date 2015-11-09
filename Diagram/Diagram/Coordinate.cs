using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ITI.SkyLord
{
    // TODO : passer en class
    public struct Coordinate
    {
        private readonly int _x;
        private readonly int _y;
        
        /// <summary>
        /// Create pair of coordinate x and y
        /// </summary>
        /// <param name="x">No limit</param>
        /// <param name="y">No limit</param>
        public Coordinate( int x, int y )
        {
            this._x = x;
            this._y = y;
        }

        #region Properties
        /// <summary>
        /// Gets the coordinate X
        /// </summary>
        public int X
        {
            get
            {
                return _x;
            }
        }
        /// <summary>
        /// Gets the coordinate Y
        /// </summary>
        public int Y
        {
            get
            {
                return _y;
            }
        } 
        #endregion
    }
}
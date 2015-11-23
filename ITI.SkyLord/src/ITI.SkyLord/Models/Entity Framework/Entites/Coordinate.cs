using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace ITI.SkyLord
{
    // TODO : passer en class
    public class Coordinate
    {
        private long _coordinateId;
        private  int _x;
        private  int _y;

        /// <summary>
        /// Create pair of coordinate x and y
        /// </summary>
        /// <param name="x">No limit</param>
        /// <param name="y">No limit</param>
        public Coordinate()
        {
            this._x = 0;
            this._y = 0;
        }

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
        [Key]
        public long CoordinateId { get; set; }
        /// <summary>
        /// Gets the coordinate X
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Gets the coordinate Y
        /// </summary>
        public int Y { get; set; }
    #endregion
    }
}
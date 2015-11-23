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
        private  int _x;
        private  int _y;

        /// <summary>
        /// Create pair of coordinate x and y
        /// </summary>
        /// <param name="x">No limit</param>
        /// <param name="y">No limit</param>
        public Coordinate()
        {
            this.X = 0;
            this.Y = 0;
        }

        /// <summary>
        /// Create pair of coordinate x and y
        /// </summary>
        /// <param name="x">No limit</param>
        /// <param name="y">No limit</param>
        public Coordinate( int x, int y )
        {
            this.X = x;
            this.Y = y;
        }

        #region Properties
        [Key]
        public long CoordinateId { get; set; }

        public int X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y =  value ;
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace ITI.SkyLord
{
    public class Coordinate
    {
        #region Properties
        [Key]
        public long CoordinateId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
        #endregion
    }
}
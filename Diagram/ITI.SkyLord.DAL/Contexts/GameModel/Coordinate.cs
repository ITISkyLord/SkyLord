using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
   public partial class Coordinate
    {
        public long CoordinateId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}

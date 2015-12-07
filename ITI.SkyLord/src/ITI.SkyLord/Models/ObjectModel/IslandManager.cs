using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.ObjectModel
{
    public static class IslandManager
    {
        public static double DistanceFromCenter( Island island )
        {
            if( Math.Sqrt( Math.Pow( island.Coordinates.X, 2 ) + Math.Pow( island.Coordinates.Y, 2 ) ) == 0 )
                return 0.01;
            else
                return Math.Sqrt( Math.Pow( island.Coordinates.X, 2 ) + Math.Pow( island.Coordinates.Y, 2 ) );
            // Formule : RACINE(coord.X² + coord.Y²)
        }
    }
}

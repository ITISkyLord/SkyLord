using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
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

        public static double DistanceBeetweenTwoIslands( Island island1, Island island2 )
        {
            // Formule : RACINE [ (Xb - Xa)² + (Yb - Ya)² ]
            if( island1 == island2 ) throw new ArgumentException( "Island 1 et island 2 c'est la même île !" );
            return Math.Sqrt( (Math.Pow( island2.Coordinates.X - island1.Coordinates.X, 2 ) + Math.Pow( island2.Coordinates.Y - island1.Coordinates.Y, 2 )) );
        }
    }
}

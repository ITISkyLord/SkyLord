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
            Coordinate center = new Coordinate() { X = 50, Y = 50 };
            var distance = DistanceBeetweenTwoCoordinates(island.Coordinates, center);

            return (distance == 0 ? 0.01 : distance);
        }

        public static double DistanceBeetweenTwoIslands( Island island1, Island island2 )
        {
            if( island1 == island2 ) throw new ArgumentException( "Island 1 et island 2 c'est la même île !" );
            if( island1.IslandId == island2.IslandId ) throw new ArgumentException( "Island 1 et island 2 c'est la même île !" );

            return DistanceBeetweenTwoCoordinates(island1.Coordinates, island2.Coordinates);
        }

        private static double DistanceBeetweenTwoCoordinates(Coordinate c1, Coordinate c2)
        {
            // Formule : Distance = RACINE [ (Xb - Xa)² + (Yb - Ya)² ]
            return Math.Sqrt((Math.Pow(c2.X - c1.X, 2) + Math.Pow(c2.Y - c1.Y, 2)));
        }
    }
}

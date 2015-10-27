using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Map
    {
        private readonly Dictionary<Coordinate,Island> _islands;
        World _world;

        public Map(World world)
        {
            _world = world;
            _islands = new Dictionary<Coordinate, Island>();
        }

        public Dictionary<Coordinate, Island> Islands
        {
            get
            {
                return _islands;
            }
        }
    }
}
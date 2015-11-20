using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Map
    {
        private readonly List<Island> _islands;
        World _world;

        /// <summary>
        /// Create a new map. Only one map on a world.
        /// </summary>
        /// <param name="world"></param>
        public Map( World world )
        {
            if( world == null ) throw new ArgumentNullException( "world is null." );
            _world = world;
            _islands = new List<Island>();
        }

        #region Properties
        /// <summary>
        /// Gets the dictionary of islands presents on the map.
        /// </summary>
        public List<Island> Islands
        {
            get
            {
                return _islands;
            }
        } 
        #endregion
    }
}
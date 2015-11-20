using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Map
    {
        private long _mapId;
        private List<Island> _islands;
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
        /// 
        [Key]
        public long MapId { get; set; }
        public List<Island> Islands { get; set; }
        public World World { get; set; }
        #endregion
    }
}
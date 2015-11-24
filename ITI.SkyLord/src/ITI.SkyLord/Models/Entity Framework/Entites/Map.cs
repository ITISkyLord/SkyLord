using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Map
    {
        private List<Island> _islands;
        // World _world;

        /// <summary>
        /// Create a new map. Only one map on a world.
        /// </summary>
        /// <param name="world"></param>
        public Map()
        {
            Islands = new List<Island>();
        }
        #region Properties
        /// <summary>
        /// Gets the dictionary of islands presents on the map.
        /// </summary>
        /// 
        [Key]
        public long MapId { get; set; }
        public List<Island> Islands
        {
            get
            {
                return _islands;
            }

            set
            {
                _islands = value;
            }
        }

        #endregion
    }
}
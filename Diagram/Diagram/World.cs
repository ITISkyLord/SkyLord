using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class World
    {
        private readonly List<Player> _players;
        private readonly Map _map;

        /// <summary>
        /// Creates a world.
        /// </summary>
        public World()
        {
            _players = new List<Player>();
            _map = new Map(this);
        }

        #region Properties
        public List<Player> Players
        {
            get
            {
                return _players;
            }

        }

        public Map Map
        {
            get
            {
                return _map;
            }
        }
        #endregion

        /// <summary>
        /// Adds a new player to the wolrd.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="mail">The mail of the player.</param>
        /// <param name="password">The password of the player.</param>
        /// <returns>The player added.</returns>
        public Player addNewPlayer( string name, string mail = "toto@gmail.com", string password = "password" )
        {
            Player player = new Player( this, name, mail, password );
            _players.Add( player );
            return player;
        }

        /// <summary>
        /// Adds an Island to the world.
        /// </summary>
        /// <param name="coordinates">The coordinates of the world on the Map.</param>
        /// <param name="name">The name of the world.</param>
        /// <param name="capital">Is the Island the first Island of the owner.</param>
        /// <param name="owner">The player who is the owner of the island.</param>
        /// <returns></returns>
        public Island addNewIsland ( Coordinate coordinates, string name = "Île inhabitée.", bool capital = false, Player owner = null )
        {
            if( name.Length > 50 ) throw new ArgumentOutOfRangeException( " Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment." );
            if( _map.Islands.Any( i => i.Coordinates.X == coordinates.X && i.Coordinates.Y == coordinates.Y ) ) throw new ArgumentException( " There is already an island on these coordinates." );
            Island island = new Island( name, coordinates, capital, owner = null );
            _map.Islands.Add(island );
            return island;
        }
    }
}

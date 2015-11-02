using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class World
    {
        private readonly List<Player> _players;
        private readonly Map _map;

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

        public World()
        {
            _players = new List<Player>();
            _map = new Map(this);
        }

        public Player addNewPlayer( string name, string mail = "toto@gmail.com", string password = "password" )
        {
            Player player = new Player( this, name, mail, password );
            _players.Add( player );
            return player;
        }

        public Island addNewIsland ( Coordinate coordinates, string name = "Île inhabitée.", bool capital = false, Player owner = null )
        {
            if( name.Length > 50 ) throw new ArgumentOutOfRangeException( " Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment." );
            if( _map.Islands.ContainsKey( coordinates ) ) throw new ArgumentException( " There is already an island on these coordinates." );
            Island island = new Island( name, coordinates, capital, owner = null );
            _map.Islands.Add(coordinates, island );
            return island;
        }
    }
}

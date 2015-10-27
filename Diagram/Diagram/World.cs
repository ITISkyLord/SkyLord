using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class World
    {
        List<Player> players;
        Map map;
        public World()
        {
            players = new List<Player>();
            map = new Map(this);
        }

        public void addNewPlayer( string name, string mail = "toto@gmail.com", string password = "password" )
        {
            players.Add( new Player( name, mail, password ) );
        }

        public void addNewIsland ( string name, Coordinate coordinates, bool capital, Player owner = null )
        {
            map.Islands.Add( coordinates, new Island( name, coordinates, capital, owner = null ) );
        }
    }
}

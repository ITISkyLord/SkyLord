using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using NUnit.Framework;
namespace ITI.SkyLord.Tests.EfTests
{
    [TestFixture]
    public class ConnectionTest
    {
        World _world;
        public ConnectionTest()
        {
            _world = new World();
        }
        [Test]
        public void Create_a_new_player_and_save_it()
        {

            Player p = new Player(_world, "Thanur","toto@intechinfo.fr", "toto");
            PlayerContext pc = new PlayerContext();
            pc.AddPlayer( p );
        }
    }
}

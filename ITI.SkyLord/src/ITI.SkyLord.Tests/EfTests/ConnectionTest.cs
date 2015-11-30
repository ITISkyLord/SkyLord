//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ITI.SkyLord.Models.Entity_Framework.Contexts;
//using NUnit.Framework;
//namespace ITI.SkyLord.Tests.EfTests
//{
//    [TestFixture]
//    public class ConnectionTest
//    {
//        World _world;
//        public ConnectionTest()
//        {
//            _world = new World();

//        }

//        [Test]
//        public void Create_a_new_player_and_save_it()
//        {

//            Player p = new Player(_world, "Marvin", "cdk@tft.fr", "thought");
//            _world.Players.Add(p);
//            PlayerContext pc = new PlayerContext();
//            pc.AddPlayer(p);
//        }

//        [Test]
//        public void Validate_a_player_with_IsPlayerValid_method()
//        {
//            Player p = new Player(_world, "Thanur", "toto@intechinfo.fr", "toto");
//            Player p2 = new Player(_world, "Thanur", "toto@intechinfo.fr", "tata");
//            Player p3 = new Player(_world, "Thanur", "tata@bla.fr", "tata");
//            Player p4 = new Player(_world, "Nathos", "tata@bla.fr", "tata");

//            PlayerContext pc = new PlayerContext();

//            Assert.IsTrue(pc.IsPlayerValid(p.Name, p.Profil.Password));
//            Assert.IsFalse(pc.IsPlayerValid(p2.Name, p2.Profil.Password));
//            Assert.IsFalse(pc.IsPlayerValid(p3.Name, p3.Profil.Password));
//            Assert.IsFalse(pc.IsPlayerValid(p4.Name, p4.Profil.Password));
//        }
//    }
//}

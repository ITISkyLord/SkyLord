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
//            //using ( PlayerContext context = new PlayerContext() )
//            //{
//            //    context.Add( _world );
//            //    context.SaveChanges();
//            //}

//            using ( PlayerContext context = new PlayerContext() )
//            {
//                _world = context.GetWorld();
//            }
//        }

//        //[Test]
//        //public void Validate_a_player_with_IsPlayerValid_method()
//        //{
//        //    Player p = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
//        //    Player p2 = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "tata" };
//        //    Player p3 = new Player { World = _world, Name = "Thanur", Mail = "tata@bla.fr", Password = "tata" };
//        //    Player p4 = new Player { World = _world, Name = "Nathos", Mail = "tata@bla.fr", Password = "tata" };

//        //    try
//        //    {
//        //        using ( PlayerContext context = new PlayerContext() )
//        //        {
//        //            context.AddPlayer( p );

//        //            Assert.IsTrue( context.IsPlayerValid( p.Mail, p.Password ) );
//        //            Assert.IsFalse( context.IsPlayerValid( p2.Mail, p2.Password ) );
//        //            Assert.IsFalse( context.IsPlayerValid( p3.Mail, p3.Password ) );
//        //            Assert.IsFalse( context.IsPlayerValid( p4.Mail, p4.Password ) );
//        //        }
//        //    }
//        //    finally
//        //    {
//        //        using ( PlayerContext context = new PlayerContext() )
//        //        {
//        //            context.RemovePlayer( p.PlayerId );
//        //        }
//        //    }
//        //}
//    }
//}

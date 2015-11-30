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
            //using ( PlayerContext context = new PlayerContext() )
            //{
            //    context.Add( _world );
            //    context.SaveChanges();
            //}

            using ( PlayerContext context = new PlayerContext() )
            {
                _world = context.GetWorld();
            }
        }

        [Test]
        public void Validate_a_player_with_IsPlayerValid_method()
        {
            Player p = new Player( _world, "Thanur", "toto@intechinfo.fr", "toto" );
            Player p2 = new Player( _world, "Thanur", "toto@intechinfo.fr", "tata" );
            Player p3 = new Player( _world, "Thanur", "tata@bla.fr", "tata" );
            Player p4 = new Player( _world, "Nathos", "tata@bla.fr", "tata" );

            using ( PlayerContext context = new PlayerContext() )
            {
                context.AddPlayer( p );

                Assert.IsTrue( context.IsPlayerValid( p.Profil.Mail, p.Profil.Password ) );
                Assert.IsFalse( context.IsPlayerValid( p2.Profil.Mail, p2.Profil.Password ) );
                Assert.IsFalse( context.IsPlayerValid( p3.Profil.Mail, p3.Profil.Password ) );
                Assert.IsFalse( context.IsPlayerValid( p4.Profil.Mail, p4.Profil.Password ) );

                context.RemovePlayer( p.PlayerId );
            }


        }
    }
}

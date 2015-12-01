using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.Data.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Tests.EfTests
{
    [TestFixture]
    public class PlayerTests
    {
        World _world;
        public PlayerTests()
        {
            using ( PlayerContext context = new PlayerContext() )
            {
                _world = context.GetWorld();
            }

            //using ( PlayerContext context = new PlayerContext() )
            //{
            //    context.Add( _world );
            //    context.SaveChanges();
            //}
        }

        [Test]
        public void Create_a_new_player_and_save_it()
        {
            Player p = new Player( _world, "Thanur", "toto@intechinfo.fr", "toto" );
            Player playerFromDatabase;

            using ( PlayerContext context = new PlayerContext() )
            {
                context.AddPlayer( p );

                playerFromDatabase = context.FindPlayer( p.Profil.Mail );
            }

            Assert.That( playerFromDatabase.Name == p.Name && playerFromDatabase.Profil.Mail == p.Profil.Mail );

            using ( PlayerContext context = new PlayerContext() )
            {
                context.RemovePlayer( p.PlayerId );
            }


        }

        [Test]
        public void DisplayPlayers()
        {
            Player p1 = new Player( _world, "Thanur", "toto@intechinfo.fr", "toto" );
            Player p2 = new Player( _world, "MachinTruc", "machin@intechinfo.fr", "toto" );

            using ( PlayerContext context = new PlayerContext() )
            {
                
                context.AddPlayer( p1 );
                context.AddPlayer( p2 );

                foreach ( Player player in context.Players.Include( p => p.Profil ) )
                {
                    Console.WriteLine( "Name: {0}, Mail : {1}", player.Name, player.Profil.Mail );
                }

                context.RemovePlayer( p1.PlayerId );
                context.RemovePlayer( p2.PlayerId );
            }
        }
    }
}

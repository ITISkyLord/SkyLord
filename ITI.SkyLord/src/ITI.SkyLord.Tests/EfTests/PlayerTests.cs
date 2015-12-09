using ITI.SkyLord.Models.Entity_Framework;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Identity.EntityFramework;
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
        User_Player _user_player;
        public PlayerTests()
        {
            using ( PlayerContext context = new PlayerContext() )
            {
                _world = context.GetWorld();
            }
        }

        void AddPlayerAndUser( Player p )
        {
            ApplicationUser appUser = new ApplicationUser();
            User_Player userPlayer = new User_Player( p, appUser );

            using ( var context = new PlayerContext() )
            {
                context.Users.Add( appUser );
                context.Players.Add( p );
                context.User_Players.Add( userPlayer );
                context.SaveChanges();
            }
        }

        void RemovePlayerAndUser( Player p )
        {
            using ( var context = new PlayerContext() )
            {
                context.Remove( p );
                context.User_Players.Remove( p.UserPlayer );
                context.SaveChanges();

                context.Users.Remove( p.UserPlayer.User );
                context.SaveChanges();
                Console.WriteLine("C'est bon ! ");
            }
        }

        [Test]
        public void AddPlayerAndUser_then_RemovePlayerAndUser()
        {
            Player player = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
            AddPlayerAndUser( player );

            using ( var context = new PlayerContext() )
            {
                Assert.IsNotNull( context.Players.SingleOrDefault( p => p.PlayerId == player.PlayerId ) );
                RemovePlayerAndUser( player );
                Assert.IsNull( context.Players.SingleOrDefault( p => p.PlayerId == player.PlayerId ) );
            }
        }

        [Test]
        public void Create_a_new_player_and_save_it()
        {
            Player defaultPlayer = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
            Player playerFromDatabase;

            try
            {
                using ( PlayerContext context = new PlayerContext() )
                {
                    AddPlayerAndUser( defaultPlayer );

                    playerFromDatabase = context.FindPlayer( defaultPlayer.Mail );
                }
                Assert.That( playerFromDatabase.Name == defaultPlayer.Name && playerFromDatabase.Mail == defaultPlayer.Mail );
            }
            finally
            {
                using ( PlayerContext context = new PlayerContext() )
                {
                    RemovePlayerAndUser( defaultPlayer );
                }
            }
        }
    }
}

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

        //[Test]
        //public void Create_a_new_player_and_save_it()
        //{
        //    Player p = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
        //    ApplicationUser appUser = new ApplicationUser();
        //    _user_player = new User_Player(p, appUser);

        //    Player playerFromDatabase;

        //    try
        //    {
        //        using ( PlayerContext context = new PlayerContext() )
        //        {
        //            context.AddPlayer( p );
        //            context.User_Players.Add( _user_player );
        //            context.SaveChanges();

        //            playerFromDatabase = context.FindPlayer( p.Mail );
        //        }
        //        Assert.That( playerFromDatabase.Name == p.Name && playerFromDatabase.Mail == p.Mail );
        //    }
        //    finally
        //    {
        //        using ( PlayerContext context = new PlayerContext() )
        //        {
        //            playerFromDatabase = context.FindPlayer(p.Mail);
        //            context.RemovePlayer(p.PlayerId);
        //            context.Remove( _user_player );
        //            context.Remove( appUser );
        //        }
        //    }
        //}

        //[Test]
        //public void DisplayPlayers()
        //{
        //    Player p1 = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
        //    Player p2 = new Player { World = _world, Name = "MachinTruc", Mail = "machin@intechinfo.fr", Password = "toto" };

        //    try
        //    {
        //        using ( PlayerContext context = new PlayerContext() )
        //        {
        //            context.AddPlayer( p1 );
        //            context.AddPlayer( p2 );

        //            foreach ( Player player in context.Players.Include( p => p.Profil ) )
        //            {
        //                Console.WriteLine( "Name: {0}, Mail : {1}", player.Name, player.Mail );
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        using ( PlayerContext context = new PlayerContext() )
        //        {
        //            context.RemovePlayer(p1.PlayerId);
        //            context.RemovePlayer(p2.PlayerId);
        //        }
        //    }
        //}

        //[Test]
        //public void cascade_delete()
        //{
        //    Player p = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };

        //    try
        //    {
        //        using (PlayerContext context = new PlayerContext())
        //        {
        //            context.AddPlayer(p);
        //        }
        //    }
        //    finally
        //    {
        //        using (PlayerContext context = new PlayerContext())
        //        {
        //            Profil profil = context.FindPlayer(p.PlayerId).Profil;

        //            context.RemovePlayer(p.PlayerId);
        //            context.SaveChanges();

        //            Assert.IsNull(context.FindPlayer(p.PlayerId));
        //            Assert.IsFalse(context.Profils.Any(pr => pr.ProfilId == profil.ProfilId));
        //        }
        //    }

        //}

        //[Test]
        //public void CreateAPlayerCreateAlsoAIsland()
        //{

        //    Player p1 = new Player { World = _world, Name = "Kevin", Mail = "tdr@cdk.fr", Password = "levelIII" };
        //    Coordinate c = new Coordinate { X = 0, Y = 1 };        

        //    using (PlayerContext context =  new PlayerContext())
        //    {
        //        if (context.Players.Where((p) => p.Name == p1.Name).FirstOrDefault() != null)
        //        {
        //            Player x = context.Players.Include(p => p.Profil).Include(z => z.World).First(o => o.Name == "Marvin");

        //            Console.WriteLine("Name : {0}, Password : {1}, Description {2}, Monde : {3}", x.Name, x.Password, x.Profil.Description, x.World.WorldId);
        //        }
        //        else
        //        {
        //            context.AddPlayer(p1);
        //            context.SaveChanges();
        //        }
        //    }

        //    using (IslandContext context = new IslandContext())
        //    {
        //        context.Coordinates.Add(c);
        //        Island i1 = new Island { Name = "Parc des princes", IsCapital = true, Loyalty = 0, Owner = p1, Coordinates = c };
        //        context.Islands.Add(i1);
        //        context.SaveChanges();
        //    }      
        //}
    }
        }

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
        }

        [Test]
        public void Create_a_new_player_and_save_it()
        {
            Player p = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
            Player playerFromDatabase;

            try
            {
                using ( PlayerContext context = new PlayerContext() )
                {
                    context.AddPlayer( p );
                    playerFromDatabase = context.FindPlayer( p.Mail );
                }
                Assert.That( playerFromDatabase.Name == p.Name && playerFromDatabase.Mail == p.Mail );
            }
            finally
            {
                using ( PlayerContext context = new PlayerContext() )
                {
                    playerFromDatabase = context.FindPlayer(p.Mail);
                    context.RemovePlayer(p.PlayerId);
                }
            }
        }

        [Test]
        public void DisplayPlayers()
        {
            Player p1 = new Player { World = _world, Name = "Thanur", Mail = "toto@intechinfo.fr", Password = "toto" };
            Player p2 = new Player { World = _world, Name = "MachinTruc", Mail = "machin@intechinfo.fr", Password = "toto" };

            try
            {
                using ( PlayerContext context = new PlayerContext() )
                {
                    context.AddPlayer( p1 );
                    context.AddPlayer( p2 );

                    foreach ( Player player in context.Players.Include( p => p.Profil ) )
                    {
                        Console.WriteLine( "Name: {0}, Mail : {1}", player.Name, player.Mail );
                    }
                }
            }
            finally
            {
                using ( PlayerContext context = new PlayerContext() )
                {
                    context.RemovePlayer(p1.PlayerId);
                    context.RemovePlayer(p2.PlayerId);
                }
            }
        }

        [Test]
        public void CreateAPlayerCreateAlsoAIsland()
        {

            Player p1 = new Player { World = _world, Name = "Kevin", Mail = "tdr@cdk.fr", Password = "levelIII" };
            Coordinate c = new Coordinate { X = 0, Y = 1 };        

            using (PlayerContext context =  new PlayerContext())
            {
                if (context.Players.Where((p) => p.Name == p1.Name).FirstOrDefault() != null)
                {
                    Player x = context.Players.Include(p => p.Profil).Include(z => z.World).First(o => o.Name == "Marvin");

                    Console.WriteLine("Name : {0}, Password : {1}, Description {2}, Monde : {3}", x.Name, x.Password, x.Profil.Description, x.World.WorldId);
                }
                else
                {
                    context.AddPlayer(p1);
                    context.SaveChanges();
                }
            }

            using (IslandContext context = new IslandContext())
            {
                context.Coordinates.Add(c);
                Island i1 = new Island { Name = "Parc des princes", IsCapital = true, Loyalty = 0, Owner = p1, Coordinates = c };
                context.Islands.Add(i1);
                context.SaveChanges();
            }      
    }
}
        }

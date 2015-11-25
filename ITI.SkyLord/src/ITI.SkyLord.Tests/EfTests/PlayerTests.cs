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

        [Test]
        public void Add2Players()
        {
            PlayerContext context = new PlayerContext();
            _world = new World();
            Player testPlayer = new Player(_world, "Bitch");
            Player testPlayer2 = new Player(_world, "Biatch");
            _world.Players.Add(testPlayer);
            _world.Players.Add(testPlayer2);

            context.AddPlayer(testPlayer); context.AddPlayer(testPlayer2);
            Console.WriteLine("Player saved");
        }

        [Test]
        public void DisplayPlayers()
        {
            using( PlayerContext context = new PlayerContext())
            {
                foreach( Player p in context.Players)
                {
                    Console.WriteLine("Name: {0}", p.Name);
                }
            }
        }
    }
}

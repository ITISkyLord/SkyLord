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
        //[Test]
        //public void Add2Players()
        //{
        //    World _world = new World();
        //    Player testPlayer = new Player(_world, "Thanur");
        //    Player testPlayer2 = new Player(_world, "Nathos");
        //    using (PlayerContext context = new PlayerContext())
        //    {
        //        context.Players.Add(testPlayer);
        //        context.Players.Add(testPlayer2);
        //        context.SaveChanges();

        //    }
        //    Console.WriteLine("Player saved");
        //}

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

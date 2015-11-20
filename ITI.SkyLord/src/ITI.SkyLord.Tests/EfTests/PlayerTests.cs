using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework.Entites;
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
        [Test]
        public void Add2Players()
        {
            EPlayer testPlayer = new EPlayer { Name = "Lolo" };
            EPlayer testPlayer2 = new EPlayer { Name = "Lili" };
            using (PlayerContext context = new PlayerContext())
            {
                context.Players.Add(testPlayer);
                context.Players.Add(testPlayer2);
                context.SaveChanges();
            }
            Console.WriteLine("Player saved");
        }

        [Test]
        public void DisplayPlayers()
        {
            using (PlayerContext context = new PlayerContext())
            {
                foreach (EPlayer p in context.Players.Include( p => p.Profil))
                {
                    Console.WriteLine("Name: {0}", p.Name);
                }
            }
        }
    }
}

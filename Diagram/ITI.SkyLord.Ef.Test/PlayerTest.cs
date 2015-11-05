using ITI.SkyLord.DAL.Contexts.GameModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.Ef.Test
{
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void Add2Players()
        {
            Player testPlayer = new Player { Name = "Lolo" };
            Player testPlayer2 = new Player { Name = "Lili" };
            using (GameEntity context = new GameEntity())
            {
                context.Players.Add(testPlayer);
                context.Players.Add(testPlayer2);
                context.SaveChanges();
            }
            Console.WriteLine("Player saved");
            Console.ReadLine();
        }

        [Test]
        public void DisplayPlayers()
        {
            using (GameEntity context = new GameEntity())
            {
                foreach(Player p in context.Players)
                {
                    Console.WriteLine("Name: {0}", p.Name);
                }
            }
        }
    }
}

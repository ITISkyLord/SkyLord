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
    public class Player
    {
        [Test]
        public void CreatePlayer()
        {
            using (GameEntity context = new GameEntity())
            {
                context.Players.Add(new Players { Name = "Boss" });
                context.SaveChanges();
            }
        }

        [Test]
        public void DisplayPlayers()
        {
            using (GameEntity context = new GameEntity())
            {
                foreach(Players P in context.Players)
                {
                    Console.WriteLine("Name: {0}", P.Name);
                }
            }
        }
    }
}

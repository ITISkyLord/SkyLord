using ITI.SkyLord.DAL.Contexts.GameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.ConsoleApplication
{
     public class Program
    {
        static void Main(string[] args)
        {
            Players testPlayer = new Players {Name = "Lolo" };
            Players testPlayer2 = new Players { Name = "Lili" };
            using (GameEntity context = new GameEntity())
            {
                context.Players.Add(testPlayer);
                context.Players.Add(testPlayer2);
                context.SaveChanges();
            }
            Console.WriteLine("Player saved");
            Console.ReadLine();
        }
    }
}

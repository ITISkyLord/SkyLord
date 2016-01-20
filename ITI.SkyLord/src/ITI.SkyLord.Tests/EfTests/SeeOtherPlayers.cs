using ITI.SkyLord.Models.Entity_Framework.Contexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace ITI.SkyLord.Tests
{
    [TestFixture]
    public class SeeOtherPlayers
    {
        World _world = new World();

        public SeeOtherPlayers()
        {
            using (SetupContext context = new SetupContext())
            {
               _world = context.GetWorld();
            }
        }

        //[Test]
        //public void See_information_of_a_player()
        //{
        //    Player p1 = new Player { World = _world, Name = "Marvin", Mail = "marvin@cdk.fr", Password = "tought" };
        //    using (PlayerContext pc = new PlayerContext())
        //    {
        //        try
        //        {
        //            pc.AddPlayer(p1);
        //            pc.SaveChanges();

        //            Player x = pc.Players.Include(p => p.Profil).First(o => o.Name == "Marvin");

        //            Console.WriteLine("Name : {0}, Password : {1}, Description {2}", x.Name, x.Password, x.Profil.Description);
        //        }
        //        finally
        //        {
        //            pc.RemovePlayer(p1.PlayerId);
        //            pc.SaveChanges();
        //        }
  
        //    }
        //}
    }
}

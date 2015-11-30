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

        //[Test]
        //public void Add2Players()
        //{
        //    PlayerContext context = new PlayerContext();
        //    _world = new World();
        //    Player testPlayer = new Player(_world, "Loic");
        //    Player testPlayer2 = new Player(_world, "Kevin");

        //    context.AddPlayer(testPlayer); context.AddPlayer(testPlayer2);
        //    Console.WriteLine("Player saved");
        //}


        //[Test]
        //public void DeletePlayers()
        //{
        //    using (PlayerContext context = new PlayerContext())
        //    {
        //        foreach (Player p in context.Players)
        //        {
        //            context.Players.Remove(p);
        //        }
        //        Console.WriteLine("Voici le nombre d'entrée maintenant dans la bdd : " + context.Players.Count());
        //    }

        //}
    }
}

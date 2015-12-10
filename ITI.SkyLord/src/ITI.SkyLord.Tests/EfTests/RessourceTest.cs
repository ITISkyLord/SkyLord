using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Tests.EfTests
{
    [TestFixture]
    public class RessourceTest
    {
        [Test]
        public void SeeRessources()
        {
            Ressource testRessource = null;
            try
            {               
                using (IslandContext context = new IslandContext())
                {
                    testRessource = new Ressource { Magic = 10, Cristal = 10, Metal = 10, Wood = 10 };
                    context.Add(testRessource);
                    context.SaveChanges();

                    foreach (Ressource r in context.Ressources)
                    {
                        Console.WriteLine("Cristal : {0}, Magic : {1},Metal : {2}, Wood : {3}", r.Cristal, r.Magic, r.Metal, r.Wood);
                    }

                    Console.WriteLine("ok");
                }
            }
            finally
            {
                using (IslandContext context = new IslandContext())
                {
                    context.Remove(testRessource);
                    context.SaveChanges();
                }
            }          
        }

        [Test]
        public void AddRessources()
        {
            using (PlayerContext context = new PlayerContext())
            {
                Player playerActive = context.Players.Where(p => p.Name == "LoicD").SingleOrDefault();
                using (IslandContext islandContext = new IslandContext())
                {
                    Ressource ressource = islandContext.Islands.Include(r => r.AllRessources).Where(i => i.Owner.PlayerId == playerActive.PlayerId).Select(res => res.AllRessources).SingleOrDefault();
                }     
            }               
        }
    }
}

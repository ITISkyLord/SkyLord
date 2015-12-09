using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
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

        }
    }
}

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
           using (IslandContext context = new IslandContext())
            {
                foreach(Ressource r in context.Ressources)
                {
                    Console.WriteLine("Cristal : {0}, Magic : {1},Metal : {2}, Wood : {3}", r.Cristal,r.Magic,r.Metal,r.Wood);
                }
            }
        }
    }
}

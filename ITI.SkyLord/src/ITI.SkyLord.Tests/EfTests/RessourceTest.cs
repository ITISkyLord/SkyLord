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
                using ( IslandContext context = new IslandContext() )
                {
                    testRessource = new Ressource { Cristal = 10, Wood = 10, Magic = 10, Metal = 10 };
                    context.Add( testRessource );
                    context.SaveChanges();

                    foreach ( Ressource r in context.Ressources )
                    {
                        Console.WriteLine( "Cristal : {0}, Magic : {1},Metal : {2}, Wood : {3}", r.Cristal, r.Magic, r.Metal, r.Wood );
                    }

                    Assert.That( context.Ressources.Count() != 0 );
                }
            }
            finally
            {
                using ( IslandContext context = new IslandContext() )
                {
                    context.Remove( testRessource );
                    context.SaveChanges();
                }
            }
           
           
        }
    }
}

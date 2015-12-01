using ITI.SkyLord.Models.Entity_Framework.Contexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Tests.EfTests
{
    [TestFixture]
    public class CreateWorld
    {
        [Test]
        public void Create_world()
        {
            World world = new World();
            try
            {
                using ( WorldContext context = new WorldContext() )
                {
                    context.Add( world );
                    context.SaveChanges();
                    Assert.That( context.Worlds.Count() != 0 );
                }
                Console.WriteLine( "New world created !" );
            }
            finally
            {
                using ( WorldContext context = new WorldContext() )
                {
                    context.Remove( world );
                    context.SaveChanges();
                }
            }
        }

        [Test]
        public void Remove_world()
        {
            World world = new World();
            int count = 0;
            using ( WorldContext context = new WorldContext() )
            {
                // Get the current number of worlds in the database
                count = context.Worlds.Count();

                context.Add( world );
                context.SaveChanges();
            }

            using ( WorldContext context = new WorldContext() )
            {
                context.Remove( world );
                context.SaveChanges();

                // Assert that the original count was not changed, meaning the newly created world was deleted
                Assert.That( context.Worlds.Count() == count );
            }

            Console.WriteLine( "World removed !" );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.ObjectModel;
using Microsoft.Data.Entity;
using NUnit.Framework;

namespace ITI.SkyLord.Tests
{
    [TestFixture]
    public class IslandManagerTest
    {
        [Test]
        public void Calculate_the_distance_and_return_the_first_island_where_there_is_no_owner()
        {
            Island island;
            using( var context = new PlayerContext() )
            {
                island = context.Islands.Include(i => i.Coordinates).Where( i => i.Owner == null ).OrderBy( i => IslandManager.DistanceFromCenter( i ) ).First();

            }
            Assert.That( island != null );
        }
    }
}

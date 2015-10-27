using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diagram;
using NUnit.Framework;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class IslandTests
    {

        Player _defaultPlayer = new Player("Thanur");
        [Test]
        public void Create_new_island()
        {
            Island island = new Island("Ivry", new Coordinate(), true);
            Assert.IsNotNull( island.Name );
        }
        [Test]
        public void Create_new_island_with_name_more_than_50characters_throws_outOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => new Island( "IvrycestUneSuperVilleMaisIlFautQuelleSoitDePlusDeCinquanteCaracteresIci", new Coordinate(), true ));
        }

        //[Test]
        //public void Create_an_island_on_an_other_island_throws_ArgumentException()
        //{

        //    Island island = new Island("Ivry", new Coordinate(), true);
        //    Assert.Throws<ArgumentException>( () => new Island( "Ivry", new Coordinate(), true ));

        //}

    }
}

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
        World _world;

        public IslandTests()
        {
            _world = new World();
        }

        [Test]
        public void Create_new_island()
        {
            _world.Map.Islands.Clear();
            Island island = _world.addNewIsland( new Coordinate());

            Assert.IsNotNull( island.Name );
        }

        [Test]
        public void Create_multpiple_new_island()
        {
            _world.Map.Islands.Clear();
            Island island = _world.addNewIsland( new Coordinate() );
            Island island2 = _world.addNewIsland( new Coordinate( 1, 1 ) );

            Assert.IsNotNull( island.Name );
            Assert.IsNotNull( island2.Name );
        }

        [Test]
        public void Create_new_island_with_name_more_than_50characters_throws_outOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => _world.addNewIsland( new Coordinate(), "IvrycestUneSuperVilleMaisIlFautQuelleSoitDePlusDeCinquanteCaracteresIci" ) );
        }

        [Test]
        public void Create_an_island_on_an_other_island_throws_ArgumentException()
        {
            Island island = _world.addNewIsland( new Coordinate(1,1) );
            Assert.Throws<ArgumentException>( () => _world.addNewIsland( new Coordinate(1,1) ) );
        }
    }
}

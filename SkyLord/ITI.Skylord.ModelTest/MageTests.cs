using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ITI.SkyLord;
using ITI.SkyLord.Model;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class MageTests
    {
        Island _defaultIsland;
        World _world;

        public MageTests()
        {
            _world = new World();
            _world.addNewIsland( new Coordinate() );
            _defaultIsland = _world.Map.Islands.First();
        }

        [Test]
        public void Create_new_Mage()
        {
            Mage mage = new Mage( _defaultIsland,"Mage" );
            Assert.IsNotNull( mage.Apprentices );
        }

        [Test]
        public void Mage_begin_level1()
        {
            Mage mage = new Mage( _defaultIsland,"Mage" );
            Assert.That( mage.MageLevel.Number == 1 );
        }

        [Test]
        public void Create_mage_with_w_name()
        {
            Mage mage = new Mage(_defaultIsland, "Mage");
            Assert.That(mage.Name == "Mage");
        }

        [Test]
        public void Create_mage_with_null_island_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>( () => new Mage( null,"Mage" ));
        }

        [Test]
        public void Create_mage_with_null_name_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Mage(_defaultIsland, null));
        }

        [Test]
        public void Create_mage_with_a_name_with_3_caracters_throw_InvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => new Mage(_defaultIsland, "mag"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Diagram;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class MageTests
    {
        Island _defaultIsland = new Island("Ivry", new Coordinate(), true);
        [Test]
        public void Create_new_Mage()
        {
            Mage mage = new Mage( _defaultIsland );
            Assert.IsNotNull( mage.Apprentices );
        }

        [Test]
        public void Mage_begin_level1()
        {
            Mage mage = new Mage( _defaultIsland );
            Assert.That( mage.MageLevel.Number == 1 );
        }

        [Test]
        public void Create_mage_with_null_island_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>( () => new Mage( null ));
        }
    }
}

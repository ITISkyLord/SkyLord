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
    public class UnitTests
    {

        Player _defaultPlayer = new Player();
        Island _defaultIsland = new Island("Ivry", new Player(), new List<Army>(), new Mage(), new List<Building>(), new Coordinate(), true);
        [Test]
        public void Create_new_soldier()
        {
            Diagram.Guard guard = new Diagram.Guard( _defaultIsland );
            Assert.IsNotNull( guard );
        }

        [Test]
        public void Create_new_soldier_with_good_stats()
        {
            Diagram.Guard guard = new Diagram.Guard( _defaultIsland );
            Assert.That( guard.UnitStatistics.Attack == 70 );
        }

        [Test]
        public void Create_stats_works()
        {
            UnitStatisitcs unitStat = new UnitStatisitcs( 70, 50, 20, 15, 15, 5 );
            Assert.IsNotNull( unitStat );
        }

        [Test]
        public void unitStats_out_of_range_constructor_throws_ArgumentOutOfRangeException()
        {
            // Attack over 100
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatisitcs( 101, 100, 99, 80, 50, 50 ) );
            // Attack under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatisitcs( -1, 100, 99, 80, 50, 50 ) );
            // Armor over 100
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatisitcs( 100, 101, 99, 80, 50, 50 ) );
            // Armor under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatisitcs( 100, -1, 99, 80, 50, 50 ) );
            // Shield over 100
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatisitcs( 100, 100, 101, 80, 50, 50 ) );
            // Shield under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatisitcs( 100, 100, -1, 80, 50, 50 ) );
            // Speed under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatisitcs( 100, 100, 100, -1, 50, 50 ) );
            // Capacity under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatisitcs( 100, 100, 100, 100, -1, 50 ) );
            // Cunsumption under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatisitcs( 100, 100, 100, 100, 100, -1 ) );
        }
    }
}

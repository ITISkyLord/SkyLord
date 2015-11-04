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

        Player _defaultPlayer;
        Island _defaultIsland;
        World _world;

        public UnitTests()
        {
            _world = new World();
            _defaultPlayer = new Player( _world, "Thanur" );
            _world.addNewIsland(  new Coordinate() );
            _defaultIsland = _world.Map.Islands.Values.First();

        }
        [Test]
        public void Create_new_unit()
        {
            Unit guard = new Unit( UnitName.guard, UnitDamageType.physical, UnitType.soldier, new UnitStatistics( 50, 70, 30, 15, 15, 5 ), new Ressource( 10, 10, 10, 10 ) );
            Assert.IsNotNull( guard );
        }
        [Test]
        public void Create_new_warrior_has_the_good_unitType()
        {
            Unit warrior = new Unit( UnitName.warrior, UnitDamageType.physical, UnitType.soldier, new UnitStatistics( 70, 50, 25, 25, 15, 5 ), new Ressource( 10, 10, 10, 10 ) );
            Assert.That( warrior.UnitDamageType == UnitDamageType.physical );
        }
        [Test]
        public void Create_new_goblins_has_the_good_unitType()
        {
            Unit goblin = new Unit( UnitName.gobelin, UnitDamageType.physical, UnitType.monster, new UnitStatistics( 30, 15, 15, 15, 15, 5 ), new Ressource( 10, 10, 10, 10 ) );
            Assert.That( goblin.UnitDamageType == UnitDamageType.physical );
        }
        [Test]
        public void Create_new_troll_has_the_good_unitType()
        {
            Unit troll = new Unit( UnitName.troll, UnitDamageType.physical, UnitType.monster, new UnitStatistics( 75, 15, 30, 10, 40, 10 ), new Ressource( 10, 10, 10, 10 ) );
            Assert.That( troll.UnitDamageType == UnitDamageType.physical );
        }

        [Test]
        public void Create_new_necromancer_has_the_good_unitType()
        {
            Unit necromancer = new Unit( UnitName.necromancer, UnitDamageType.physical, UnitType.magic, new UnitStatistics( 70, 30, 15, 10, 10, 5 ), new Ressource( 10, 10, 10, 10 ) );
            Assert.That( necromancer.UnitDamageType == UnitDamageType.magical );
        }
        [Test]
        public void Create_new_cyclop_has_the_good_unitType()
        {
            Unit cyclop = new Unit( UnitName.necromancer, UnitDamageType.physical, UnitType.magic, new UnitStatistics( 50, 40, 20, 5, 10, 10 ), new Ressource( 10, 10, 10, 10 ) );
            Assert.That( cyclop.UnitDamageType == UnitDamageType.magical );
        }
        [Test]
        public void Create_stats_works()
        {
            UnitStatistics unitStat = new UnitStatistics( 70, 50, 20, 15, 15, 5 );
            Assert.IsNotNull( unitStat );
        }

        [Test]
        public void unitStats_out_of_range_constructor_throws_ArgumentOutOfRangeException()
        {
            // Attack over 100
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatistics( 101, 100, 99, 80, 50, 50 ) );
            // Attack under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatistics( -1, 100, 99, 80, 50, 50 ) );
            // Armor over 100
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatistics( 100, 101, 99, 80, 50, 50 ) );
            // Armor under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatistics( 100, -1, 99, 80, 50, 50 ) );
            // Shield over 100
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatistics( 100, 100, 101, 80, 50, 50 ) );
            // Shield under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatistics( 100, 100, -1, 80, 50, 50 ) );
            // Speed under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatistics( 100, 100, 100, -1, 50, 50 ) );
            // Capacity under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatistics( 100, 100, 100, 100, -1, 50 ) );
            // Cunsumption under 0
            Assert.Throws<ArgumentOutOfRangeException>( () => new UnitStatistics( 100, 100, 100, 100, 100, -1 ) );
        }
    }
}

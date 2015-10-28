﻿using System;
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
            _defaultPlayer = new Player( "Thanur" );
            _world.addNewIsland( "Ivry", new Coordinate(), true );
            _defaultIsland = _world.Map.Islands.Values.First();

        }
        [Test]
        public void Create_new_guard()
        {
            Diagram.Guard guard = new Diagram.Guard( _defaultIsland );
            Assert.IsNotNull( guard );
        }

        [Test]
        public void Create_new_guard_has_an_island()
        {
            Diagram.Guard guard = new Diagram.Guard( _defaultIsland );
            Assert.That( guard.Island != null );
        }

        [Test]
        public void Create_new_warrior_has_the_good_unitType()
        {
            Warrior warrior = new Warrior( _defaultIsland );
            Assert.That( warrior.UnitType == UnitType.soldier );
        }
        [Test]
        public void Create_new_goblins_has_the_good_unitType()
        {
            Goblins goblins = new Goblins( _defaultIsland );
            Assert.That( goblins.UnitType == UnitType.monster );
        }
        [Test]
        public void Create_new_troll_has_the_good_unitType()
        {
            Troll troll = new Troll( _defaultIsland );
            Assert.That( troll.UnitType == UnitType.monster );
        }

        [Test]
        public void Create_new_necromancer_has_the_good_unitType()
        {
            Necromancer necromancer = new Necromancer( _defaultIsland );
            Assert.That( necromancer.UnitType == UnitType.magic );
        }
        [Test]
        public void Create_new_cyclop_has_the_good_unitType()
        {
            Cyclop cyclop = new Cyclop( _defaultIsland );
            Assert.That( cyclop.UnitType == UnitType.magic );
        }

        [Test]
        public void Create_new_unit_without_island_or_nullIsland_throw_ArgumentNulException()
        {
            Assert.Throws<ArgumentNullException>( () => new Warrior( null ) );
        }

        [Test]
        public void Create_stats_works()
        {
            UnitStat unitStat = new UnitStat( 70, 50, 20, 15, 15, 5 );
            Assert.IsNotNull( unitStat );
        }
    }
}

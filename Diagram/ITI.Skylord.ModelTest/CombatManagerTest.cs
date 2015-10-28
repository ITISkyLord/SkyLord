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
    class CombatManagerTest
    {
        Player _defaultPlayer;
        Island _defenseIsland;
        Island _attackIsland;
        World _world;
        Army _armyAttack;
        Army _armyDefense;
        Warrior _warrior;


        public CombatManagerTest()
        {
            _world = new World();
            _defaultPlayer = new Player( "Thanur" );
            _world.addNewIsland( "Ivry", new Coordinate(), true );
            _world.addNewIsland( "Paris", new Coordinate(1,1), true );
            _defenseIsland = _world.Map.Islands.Values.First();
            _attackIsland = _world.Map.Islands.Values.Where( i => i.Coordinates.X == 1 && i.Coordinates.Y == 1  ).Single();
            _warrior = new Warrior();
            _armyAttack = new Army( ArmyState.movement, _attackIsland );
            _armyDefense = new Army( ArmyState.defense, _defenseIsland );
            _armyAttack.Regiments.Add( _warrior, 50 );
            _armyDefense.Regiments.Add( _warrior, 50 );

        }

        [Test]
        public void Two_armies_fights_simple_version_army_attack_wins()
        {
            CombatManager combatManager = new CombatManager();
            Assert.That( combatManager.Resolve( _armyAttack, _armyDefense ) == _attackIsland );
        }
    }
}

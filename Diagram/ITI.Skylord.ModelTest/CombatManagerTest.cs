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
        Island _defenseIsland;
        Island _attackIsland;
        World _world;
        Army _armyAttack;
        Army _armyDefense;


        public CombatManagerTest()
        {
            _world = new World();
            _world.addNewIsland( new Coordinate() );
            _world.addNewIsland( new Coordinate(1,1) );
            _defenseIsland = _world.Map.Islands.Values.First();
            _attackIsland = _world.Map.Islands.Values.Where( i => i.Coordinates.X == 1 && i.Coordinates.Y == 1  ).Single();
            _armyAttack = new Army( ArmyState.movement, _attackIsland );
            _armyDefense = new Army( ArmyState.defense, _defenseIsland );
        }

        [Test]
        public void Two_armies_fights_simple_version_army_attack_wins_50warriors_vs_15warriors()
        {
            // Ajouter plusieurs armées en même temps ?
            _armyAttack.Regiments.Add( new Warrior(), 50 );
            _armyDefense.Regiments.Add( new Warrior(), 15 );
            CombatManager combatManager = new CombatManager();
            Assert.That( combatManager.Resolve( _armyAttack, _armyDefense ) == _attackIsland );
        }
    }
}

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


        #region CombatManager
        public CombatManagerTest()
        {
            _world = new World();
            _world.addNewIsland( new Coordinate() );
            _world.addNewIsland( new Coordinate( 1, 1 ) );
            _defenseIsland = _world.Map.Islands.Values.First();
            _attackIsland = _world.Map.Islands.Values.Where( i => i.Coordinates.X == 1 && i.Coordinates.Y == 1 ).Single();
            _armyAttack = new Army( ArmyState.movement, _attackIsland );
            _armyDefense = new Army( ArmyState.defense, _defenseIsland );
        }

        [Test]
        public void Two_armies_physics_fights_armyattack_wins_50warriors_vs_15warriors_return_CombatResult_with_the_winner()
        {
            _armyAttack.Regiments.Clear();
            _armyDefense.Regiments.Clear();
            _armyAttack.Regiments.Add( new Warrior(), 50 );
            _armyDefense.Regiments.Add( new Warrior(), 15 );
            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve(_armyAttack, _armyDefense);
            Assert.That( combatResult.WinningArmy == _armyAttack );
        }

        [Test]
        public void Two_armies_physics_fights_armyattack_loose_15warriors_vs_50warriors_return_CombatResult_with_the_winner()
        {
            _armyAttack.Regiments.Clear();
            _armyDefense.Regiments.Clear();
            _armyAttack.Regiments.Add( new Warrior(), 15 );
            _armyDefense.Regiments.Add( new Warrior(), 50 );
            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve(_armyAttack, _armyDefense);
            Assert.That( combatResult.WinningArmy == _armyDefense );
        }

        [Test]
        public void Two_armies_magics_fights_armyattack_wins_50necromancer_vs_15necromancer_return_CombatResult_with_the_winner()
        {
            _armyAttack.Regiments.Clear();
            _armyDefense.Regiments.Clear();
            _armyAttack.Regiments.Add( new Necromancer(), 50 );
            _armyDefense.Regiments.Add( new Necromancer(), 15 );
            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve(_armyAttack, _armyDefense);
            Assert.That( combatResult.WinningArmy == _armyAttack );
        }
        [Test]
        public void Two_armies_magics_fights_armyattack_looses_15necromancer_vs_100necromancer_return_CombatResult_with_the_winner()
        {
            _armyAttack.Regiments.Clear();
            _armyDefense.Regiments.Clear();
            _armyAttack.Regiments.Add( new Necromancer(), 15 );
            _armyDefense.Regiments.Add( new Necromancer(), 100 );
            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve(_armyAttack, _armyDefense);
            Assert.That( combatResult.WinningArmy == _armyDefense );
        }

        [Test]
        public void Two_armies_physics_fights_armyattack_wins_50Warriors_and_20guards_vs_15warrior_and_10_guard_return_CombatResult_with_the_winner()
        {
            // Ajouter plusieurs armées en même temps ?
            _armyAttack.Regiments.Clear();
            _armyDefense.Regiments.Clear();
            _armyAttack.Regiments.Add( new Warrior(), 50 );
            _armyAttack.Regiments.Add( new Diagram.Guard(), 20 );
            _armyDefense.Regiments.Add( new Warrior(), 15 );
            _armyDefense.Regiments.Add( new Diagram.Guard(), 10 );

            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve(_armyAttack, _armyDefense);
            Assert.That( combatResult.WinningArmy == _armyAttack );
        }
        #endregion

    }
}

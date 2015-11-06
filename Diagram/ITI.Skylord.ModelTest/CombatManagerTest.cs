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
        public void Two_armies_physics_fights_armyattack_wins_50Warriors_and_20guards_vs_15warrior_and_10_guard_return_CombatResult_with_the_winner()
        {
            // Ajouter plusieurs armées en même temps ?
            _armyAttack.Regiments.Clear();
            _armyDefense.Regiments.Clear();
            _armyAttack.Regiments.Add(new Unit(UnitName.warrior, UnitDamageType.physical, UnitType.soldier,new UnitStatistics(70,0,0,0,0,0), new Ressource(1,1,1,1)), 130 );
            _armyAttack.Regiments.Add(new Unit(UnitName.necromancer, UnitDamageType.magical, UnitType.magic,new UnitStatistics(70,0,0,0,0,0), new Ressource(1,1,1,1)), 50 );
            _armyDefense.Regiments.Add( new Unit( UnitName.guard, UnitDamageType.physical, UnitType.soldier, new UnitStatistics( 70, 70, 70, 0, 0, 0 ), new Ressource( 1, 1, 1, 1 ) ), 100 );
            _armyDefense.Regiments.Add( new Unit( UnitName.cyclop, UnitDamageType.magical, UnitType.magic, new UnitStatistics( 70, 70, 70, 0, 0, 0 ), new Ressource( 1, 1, 1, 1 ) ), 80 );

            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve( _armyAttack, _armyDefense );
            Assert.That( combatResult.WinningArmy.Island == _armyAttack.Island );
        }

        #endregion

    }
}

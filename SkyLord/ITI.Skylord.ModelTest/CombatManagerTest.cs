using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ITI.SkyLord;
using ITI.SkyLord.Model;
using ITI.SkyLord.Model.Units;

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
        Unit _warrior;
        Unit _necromancer;
        Unit _guard;
        Unit _cyclop;

        #region CombatManager
        public CombatManagerTest()
        {
            _world = new World();
            _world.addNewIsland( new Coordinate() );
            _world.addNewIsland( new Coordinate( 1, 1 ) );
            _defenseIsland = _world.Map.Islands.First();
            _attackIsland = _world.Map.Islands.Where( i => i.Coordinates.X == 1 && i.Coordinates.Y == 1 ).Single();
            _armyAttack = new Army( ArmyState.movement, _attackIsland );
            _armyDefense = new Army( ArmyState.defense, _defenseIsland );
            _warrior = new Unit(UnitName.warrior, UnitDamageType.physical, UnitType.soldier,new UnitStatistics(70,50,20,0,40,0), new Ressource(1,1,1,1));
            _necromancer = new Unit( UnitName.necromancer, UnitDamageType.magical, UnitType.magic, new UnitStatistics( 70,20, 50, 0, 30, 0 ), new Ressource( 1, 1, 1, 1 ) );
            _guard = new Unit( UnitName.guard, UnitDamageType.physical, UnitType.soldier, new UnitStatistics( 50, 80, 70, 0, 0, 0 ), new Ressource( 1, 1, 1, 1 ) );
            _cyclop = new Unit( UnitName.cyclop, UnitDamageType.magical, UnitType.magic, new UnitStatistics( 50, 70, 30, 0, 0, 0 ), new Ressource( 1, 1, 1, 1 ) );
        }

        [Test]
        public void Two_armies_physics_fights_armyattack_wins_130Warriors_and_50guards_vs_80warrior_and_10_guard_return_CombatResult_with_the_winner()
        {
            _armyAttack.ClearRegiments();
            _armyDefense.ClearRegiments();
            _armyAttack.AddRegiment(_warrior, 130 );
            _armyAttack.AddRegiment(_guard, 50 );
            _armyDefense.AddRegiment( _warrior, 80 );
            _armyDefense.AddRegiment( _guard, 10 );

            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve( _armyAttack, _armyDefense );
            Assert.That( combatResult.WinningArmy.Island == _armyAttack.Island );
        }

        [Test]
        public void OneArmy_attack_an_empty_army_return_CombatResult_with_the_winner()
        {
            _armyAttack.ClearRegiments();
            _armyDefense.ClearRegiments();
            _armyAttack.AddRegiment( _warrior, 130 );
            _armyAttack.AddRegiment( _necromancer, 50 );

            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve( _armyAttack, _armyDefense );
            Assert.That( combatResult.WinningArmy.Island == _armyAttack.Island );
        }
        [Test]
        public void OneArmy_attack_1warrior_army_return_CombatResult_with_the_winner()
        {
            _armyAttack.ClearRegiments();
            _armyDefense.ClearRegiments();
            _armyAttack.AddRegiment( _warrior, 80 );
            _armyAttack.AddRegiment( _necromancer, 50 );

            _armyDefense.AddRegiment( _warrior, 1 );

            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve( _armyAttack, _armyDefense );
            Assert.That( combatResult.WinningArmy.Island == _armyAttack.Island );
        }

        [Test]
        public void Two_armies_mixte_fights_armyattack_wins_130Warriors_and_50necromancers_vs_80warrior_and_10_cyclop_return_CombatResult_with_the_winner()
        {
            _armyAttack.ClearRegiments();
            _armyDefense.ClearRegiments();
            _armyAttack.AddRegiment( _warrior, 130 );
            _armyAttack.AddRegiment( _necromancer, 50 );
            _armyDefense.AddRegiment( _guard, 80 );
            _armyDefense.AddRegiment( _cyclop, 10 );

            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve( _armyAttack, _armyDefense );
            Assert.That( combatResult.WinningArmy.Island == _armyAttack.Island );
        }


        [Test]
        public void OneArmy_attack_with_only_1_warrior_against_50warriors_and_20cyclops_return_CombatResult_with_the_winner()
        {
            _armyAttack.ClearRegiments();
            _armyDefense.ClearRegiments();
            _armyAttack.AddRegiment( _warrior, 1 );
            _armyDefense.AddRegiment( _warrior, 50 );
            _armyDefense.AddRegiment( _cyclop, 10 );

            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve( _armyAttack, _armyDefense );
            Assert.That( combatResult.WinningArmy.Island == _armyDefense.Island );
        }

        [Test]
        public void Two_armies_mixte_fights_armyattack_wins_1300Warriors_and_500necromancers_vs_800warrior_and_100_cyclop_return_CombatResult_with_the_winner()
        {
            _armyAttack.ClearRegiments();
            _armyDefense.ClearRegiments();
            _armyAttack.AddRegiment( _warrior, 1300 );
            _armyAttack.AddRegiment( _necromancer, 500 );
            _armyDefense.AddRegiment( _guard, 800 );
            _armyDefense.AddRegiment( _cyclop, 100 );

            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve( _armyAttack, _armyDefense );
            Assert.That( combatResult.WinningArmy.Island == _armyAttack.Island );
        }

        [Test]
        public void One_army_physics_attacks_fights_armyattack_wins_1300Warriorsvs_500_guards_and_100_cyclop_return_CombatResult_with_the_winner()
        {
            _armyAttack.ClearRegiments();
            _armyDefense.ClearRegiments();
            _armyAttack.AddRegiment( _warrior, 1300 );
            _armyDefense.AddRegiment( _guard, 500 );
            _armyDefense.AddRegiment( _cyclop, 100 );

            CombatManager combatManager = new CombatManager();
            CombatResult combatResult = combatManager.Resolve( _armyAttack, _armyDefense );
            Assert.That( combatResult.WinningArmy.Island == _armyAttack.Island );
        }

        //[Test]
        //public void Attacker_if_win_fill_their_bags_withRessources()
        //{
        //    _armyAttack.ClearRegiments();
        //    _armyDefense.ClearRegiments();
        //    _armyAttack.AddRegiment( _warrior, 1300 );
        //    _armyDefense.AddRegiment( _guard, 500 );
        //    _armyDefense.AddRegiment( _cyclop, 100 );

        //    CombatManager combatManager = new CombatManager();
        //    CombatResult combatResult = combatManager.Resolve( _armyAttack, _armyDefense );
        //    Console.WriteLine( "Ressources pillées : " + combatResult.PillagedRessources.Total );
        //    Assert.That( combatResult.PillagedRessources.Total > 0 );
        //}
        #endregion

    }
}

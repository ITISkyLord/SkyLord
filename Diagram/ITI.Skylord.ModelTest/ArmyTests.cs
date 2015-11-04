﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Diagram;

namespace ITI.Skylord.ModelTest
{
    [TestFixture]
    class ArmyTests
    {
        World _world;
        Player _defaultPlayer;
        Island _defaultIsland;
        Unit _guard;
        Unit _warrior;
        Unit _necromancer;
        Unit _cyclop;

        public ArmyTests()
        {
            _world = new World();
            _defaultPlayer = new Player( _world, "Thanur" );
            _world.addNewIsland( new Coordinate() );
            _defaultIsland = _world.Map.Islands.Values.First();
            _warrior = new Unit( UnitName.warrior, UnitDamageType.physical, UnitType.soldier, new UnitStatistics( 70, 50, 25, 25, 15, 5 ), new Ressource( 10, 10, 10, 10 ) );
            _guard = new Unit( UnitName.guard, UnitDamageType.physical, UnitType.soldier, new UnitStatistics( 50, 70, 30, 15, 15, 5 ), new Ressource( 10, 10, 10, 10 ) );
            _necromancer = new Unit( UnitName.necromancer, UnitDamageType.physical, UnitType.magic, new UnitStatistics( 70, 30, 15, 10, 10, 5 ), new Ressource( 10, 10, 10, 10 ) );
            _cyclop = new Unit( UnitName.necromancer, UnitDamageType.physical, UnitType.magic, new UnitStatistics( 50, 40, 20, 5, 10, 10 ), new Ressource( 10, 10, 10, 10 ) );
        }
        [Test]
        public void Create_new_army()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland);
            Assert.IsNotNull( army.Regiments );
        }

        [Test]
        public void Add_unit_in_army()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( _guard, 1 );
            Assert.That( army.Regiments.Count == 1 );
        }
        [Test]
        public void Add_different_units_in_army()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( _guard, 1 );
            army.Regiments.Add( _necromancer, 10 );
            army.Regiments.Add( _warrior, 15 );
            Assert.That( army.Regiments.Count == 3 );
        }
        [Test]
        public void Find_regiment_in_army_with_FindRegiment_method()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( _warrior, 50 );
            KeyValuePair<Unit, int> KvP = army.FindRegiment( _warrior );

            Assert.That( KvP.Key.Name == _warrior.Name && KvP.Value == 50 );
        }

        [Test]
        public void FindRegiment_returns_a_null_key_if_unit_is_not_in_army()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( _guard, 1 );
            army.Regiments.Add( _necromancer, 10 );
            army.Regiments.Add( _warrior, 15 );

            KeyValuePair<Unit, int> KvP = army.FindRegiment( _cyclop );
            Assert.IsNull( KvP.Key );
        }

        [Test]
        public void Get_physical_regiments_with_GetRegimentsByDamagetype_method()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( _warrior, 50 );
            army.Regiments.Add( _cyclop, 10 );

            Dictionary<Unit, int> dic = army.GetRegimentsByDamagetype(UnitDamageType.physical);

            Assert.That( dic.Count == 1 );
            Assert.That( dic.ContainsKey( _warrior ) && !dic.ContainsKey( _cyclop ) );
        }

        [Test]
        public void Get_magical_regiments_with_GetRegimentsByDamagetype_method()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( _warrior, 50 );
            army.Regiments.Add( _cyclop, 10 );

            Dictionary<Unit, int> dic = army.GetRegimentsByDamagetype( UnitDamageType.magical );

            Assert.That( dic.Count == 1 );
            Assert.That( dic.ContainsKey( _cyclop ) && !dic.ContainsKey( _warrior ) );
        }

        [Test]
        public void Get_an_army_s_ratio_of_physical_damage_with_GetPhysicalAttackRatio_method()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( _necromancer, 10 );
            army.Regiments.Add( _warrior, 10 );

            double ratio = army.GetPhysicalAttackRatio();
            Assert.That( ratio == 0.5);
        }

        [Test]
        public void Subtract_units_from_regiment_with_SubtractFromRegiment_method()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( _guard, 50 );
            army.Regiments.Add( _necromancer, 50 );

            army.SubstractFromRegiment( _guard, 10 );
            army.SubstractFromRegiment( _necromancer, 20 );

            Assert.That( army.Regiments[ _necromancer ] == 30 && army.Regiments[ _guard ] == 40 );
        }

        [Test]
        public void If_unit_number_in_regiment_is_under_0_the_regiment_is_removed()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );
            army.Regiments.Add( _guard, 50 );

            army.SubstractFromRegiment( _guard, 60 );

            Assert.IsEmpty( army.Regiments );
        }

        [Test]
        public void Add_units_to_regiment_with_AddToRegiment_method()
        {
            Army army = new Army( ArmyState.defense, _defaultIsland );

            army.Regiments.Add( _guard, 40 );
            army.Regiments.Add( _necromancer, 30 );

            army.AddToRegiment( _guard, 10 );
            army.AddToRegiment( _necromancer, 20 );

            Assert.That( army.Regiments[ _necromancer ] == 50 && army.Regiments[ _guard ] == 50 );
        }
    }
}

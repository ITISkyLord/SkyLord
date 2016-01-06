
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Army
    {

        #region Properties
        [Key]
        public long ArmyId { get; set; }

        public ArmyState ArmyState { get; set; }

        public Island Island { get; set; }

        public IList<Regiment> Regiments { get; set; }

        #endregion

        #region Methods
        //internal void AddRegiment( Regiment regiment )
        //{
        //    if ( Regiments.Any( r => r.Unit == regiment.Unit ) )
        //    {
        //        int initialUnitNumber = Regiments.Where( r => r.Unit == regiment.Unit ).Select( r => r.Number ).SingleOrDefault();
        //        int finalUnitNumber = initialUnitNumber + regiment.Number;

        //        Regiments.Remove( FindRegiment( regiment ) );
        //        Regiments.Add( regiment.Unit, finalUnitNumber );
        //    }
        //    else
        //        Regiments.Add( regiment.Unit, regiment.Number );
        //}

        //internal void AddRegiment( Unit unit, int number )
        //{
        //    if ( Regiments.Any( r => r.Unit == unit ) )
        //    {
        //        int initialUnitNumber = Regiments.Where( r => r.Unit == unit ).Select( r => r.Number ).SingleOrDefault();
        //        int finalUnitNumber = initialUnitNumber + number;

        //        Regiments.Remove( FindRegiment( unit ) );
        //        Regiments.Add( unit, finalUnitNumber );
        //    }
        //    else
        //        Regiments.Add( unit, number );
        //}

        //internal void RemoveRegiment( Regiment regiment )
        //{
        //    Regiments.Remove( regiment );
        //}
        //internal void RemoveRegiment( Unit unit )
        //{
        //    Regiments.Remove( FindRegiment( unit ) );
        //}

        //internal void ClearRegiments()
        //{
        //    Regiments.Clear();
        //}

        ///// <summary>
        ///// Gets all the regiments containing units that deal physical damage
        ///// </summary>
        ///// <param name="unitDamageType"></param>
        ///// <returns></returns>
        //internal RegimentList GetRegimentsByDamagetype( UnitDamageType unitDamageType )
        //{
        //    IEnumerable<Regiment> regs = Regiments.Where( r => r.Unit.UnitDamageType == unitDamageType );
        //    RegimentList newRegiments = new RegimentList();
        //    foreach ( Regiment r in regs )
        //    {
        //        newRegiments.Add( r );
        //    }

        //    return newRegiments;
        //}

        ///// <summary>
        ///// Gets the army's ratio of physical attack.
        ///// </summary>
        ///// <returns>The ratio.</returns>
        //internal double GetPhysicalAttackRatio()
        //{
        //    double totalAttack = 0;
        //    double physicalAttack = 0;
        //    foreach ( Unit u in Regiments.Select( r => r.Unit ) )
        //    {
        //        totalAttack += u.UnitStatistics.Attack;
        //        if ( u.UnitDamageType == UnitDamageType.physical )
        //            physicalAttack += u.UnitStatistics.Attack;
        //    }

        //    return physicalAttack / totalAttack;
        //}

        ///// <summary>
        ///// Adds a number of units to a regiment in the army.
        ///// </summary>
        ///// <param name="unit">The type of unit to add</param>
        ///// <param name="numberToAdd">The number of units to add.</param>
        //internal void AddToRegiment( Unit unit, int numberToAdd )
        //{
        //    Regiment r = FindRegiment( unit );

        //    if ( r == null ) throw new ArgumentException( "The unit you are trying to handle is not present in the army." );

        //    int initialUnitNumber = r.Number;
        //    int finalUnitNumber = initialUnitNumber + numberToAdd;
        //    Regiments.Remove( r );
        //    Regiments.Add( new Regiment( unit, finalUnitNumber ) );
        //}

        ///// <summary>
        ///// Return a new army
        ///// </summary>
        ///// <param name="ratio"></param>
        ///// <returns></returns>
        //internal Army GetArmyByRatio( double ratio )
        //{
        //    Army army = new Army( this.ArmyState, this.Island );
        //    int newValue;

        //    foreach ( Regiment r in this.Regiments )
        //    {
        //        newValue = (int)Math.Round( (double)r.Number * ratio );
        //        army.AddRegiment( r.Unit, newValue );
        //    }

        //    return army;
        //}

        ///// <summary>
        ///// Join armies to Army.this. Used in round combat. Obsolete.
        ///// </summary>
        ///// <param name="armyToJoin"></param>
        //internal void JoinArmies( Army armyToJoin )
        //{
        //    Army joinedArmy = this.Copy();

        //    foreach ( Regiment r in armyToJoin.Regiments )
        //    {
        //        if ( joinedArmy.Regiments.Any( reg => reg.Unit == r.Unit ) )
        //        {
        //            Regiment reg = armyToJoin.FindRegiment( r.Unit );
        //            joinedArmy.AddToRegiment( reg.Unit, reg.Number );
        //        }
        //        else
        //        {
        //            joinedArmy.AddRegiment( r.Unit, r.Number );
        //        }
        //    }

        //    Regiments.Clear();
        //    foreach ( Regiment r in joinedArmy.Regiments )
        //    {
        //        Regiments.Add( new Regiment( r.Unit, r.Number ) );
        //    }
        //}
        #endregion
    }
}
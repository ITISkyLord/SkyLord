
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Army
    {
        private ArmyState _armyState;
        private Island _island;
        private RegimentList _regiments;

        //private  Dictionary<Unit, int> _regiments;

        /// <summary>
        /// Create new army.
        /// </summary>
        /// <param name="armyState">Army state</param>
        /// <param name="island">This is the island where is from the army.</param>
        public Army( ArmyState armyState, Island island )
        {
            this.Island = island;
            //this._regiment = new Regiment();
            this.ArmyState = armyState;
            this.Regiments = new RegimentList();
        }

        #region Properties
        [Key]
        public long ArmyId { get; set; }

        public ArmyState ArmyState
        {
            get
            {
                return _armyState;
            }

            set
            {
                _armyState = value;
            }
        }

        public Island Island
        {
            get
            {
                return _island;
            }

            set
            {
                _island = value;
            }
        }

        public RegimentList Regiments
        {
            get
            {
                return _regiments;
            }

            set
            {
                _regiments =  value ;
            }
        }
        #endregion

        internal Regiment FindRegiment( Regiment regiment )
        {
            return Regiments.Where( r => r.Unit == regiment.Unit ).FirstOrDefault();
        }

        /// <summary>
        /// Find a regiment corresponding to a Unit in an army.
        /// </summary>
        /// <param name="unit">The unit to look for in the army.</param>
        /// <returns>The regiment found. If the regiment is not found it returns null.</returns>
        internal Regiment FindRegiment( Unit unit )
        {
            return Regiments.Where( u => u.Name == unit.Name ).FirstOrDefault();
        }

        internal void AddRegiment( Regiment regiment )
        {
            if( Regiments.Any( r => r.Unit == regiment.Unit ) )
            {
                int initialUnitNumber = Regiments.Where( r => r.Unit == regiment.Unit ).Select( r => r.Number ).SingleOrDefault();
                int finalUnitNumber = initialUnitNumber + regiment.Number;

                Regiments.Remove( FindRegiment(regiment) );
                Regiments.Add( regiment.Unit, finalUnitNumber );
            }
            else
                Regiments.Add( regiment.Unit, regiment.Number );
        }

        internal void AddRegiment( Unit unit, int number )
        {
            if ( Regiments.Any( r => r.Unit == unit ) )
            {
                int initialUnitNumber = Regiments.Where( r => r.Unit == unit ).Select( r => r.Number ).SingleOrDefault();
                int finalUnitNumber = initialUnitNumber + number;

                Regiments.Remove( FindRegiment(unit) );
                Regiments.Add( unit, finalUnitNumber );
            }
            else
                Regiments.Add( unit, number );
        }

        internal void RemoveRegiment( Regiment regiment )
        {
            Regiments.Remove( regiment );
        }
        internal void RemoveRegiment( Unit unit )
        {
            Regiments.Remove( FindRegiment( unit ) );
        }

        internal void ClearRegiments()
        {
            Regiments.Clear();
        }

        /// <summary>
        /// Gets all the regiments containing units that deal physical damage
        /// </summary>
        /// <param name="unitDamageType"></param>
        /// <returns></returns>
        internal RegimentList GetRegimentsByDamagetype( UnitDamageType unitDamageType)
        {
            IEnumerable<Regiment> regs = Regiments.Where( r => r.Unit.UnitDamageType == unitDamageType );
            RegimentList newRegiments = new RegimentList();
            foreach(Regiment r in regs )
            {
                newRegiments.Add( r );
            }

            return newRegiments;
        }

        /// <summary>
        /// Gets the army's ratio of physical attack.
        /// </summary>
        /// <returns>The ratio.</returns>
        internal double GetPhysicalAttackRatio()
        {
            double totalAttack = 0;
            double physicalAttack = 0;
            foreach(Unit u in Regiments.Select( r => r.Unit) )
            {
                totalAttack += u.UnitStatistics.Attack;
                if ( u.UnitDamageType == UnitDamageType.physical )
                    physicalAttack += u.UnitStatistics.Attack;
            }

            return physicalAttack / totalAttack;
        }

        /// <summary>
        /// Substracts a number of units from a regiment in a army, if the final number is under 0, it removes the regiment.
        /// </summary>
        /// <param name="unit">The unit type to remove.</param>
        /// <param name="numberToRemove">The number of units to substract.</param>
        internal void SubstractFromRegiment( Unit unit, int numberToRemove )
        {
            Regiment r = FindRegiment( unit );

            if ( r == null ) throw new ArgumentException( "The unit you are trying to handle is not present in the army." );

            int initialUnitNumber = r.Number;
            int finalUnitNumber = initialUnitNumber - numberToRemove;
            Regiments.Remove( r );

            if ( finalUnitNumber > 0 )
                Regiments.Add( new Regiment(unit, finalUnitNumber ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ratio"></param>
        internal void SubstractFromArmy( double ratio )
        {
            Army tmpArmy = this.Copy();

            foreach( Regiment r in tmpArmy.Regiments )
            {
                int number = (int)(r.Number * ratio);
                Console.WriteLine( "number in the unit : " + r.Name + " = " + r.Number );
                Console.WriteLine( "loss = " + number );
                SubstractFromRegiment( r.Unit, number );

            }
        }

        /// <summary>
        /// Adds a number of units to a regiment in the army.
        /// </summary>
        /// <param name="unit">The type of unit to add</param>
        /// <param name="numberToAdd">The number of units to add.</param>
        internal void AddToRegiment(Unit unit, int numberToAdd )
        {
            Regiment r = FindRegiment( unit );

            if ( r == null ) throw new ArgumentException( "The unit you are trying to handle is not present in the army." );

            int initialUnitNumber = r.Number;
            int finalUnitNumber = initialUnitNumber + numberToAdd;
            Regiments.Remove( r );
            Regiments.Add( new Regiment( unit, finalUnitNumber ) );
        }

        /// <summary>
        /// Return a new army copied.
        /// </summary>
        /// <returns></returns>
        internal Army Copy()
        {
            Army army = new Army(this.ArmyState, this.Island);
            foreach( Regiment r in this.Regiments )
            {
                army.AddRegiment( r );
            }

            return army;
        }

        /// <summary>
        /// Return a new army
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        internal Army GetArmyByRatio( double ratio )
        {
            Army army = new Army( this.ArmyState, this.Island );
            int newValue;

            foreach( Regiment r in this.Regiments )
            {
                newValue = (int)Math.Round( (double)r.Number * ratio );
                army.AddRegiment( r.Unit, newValue );
            }

            return army;
        }

        /// <summary>
        /// Join armies to Army.this. Used in round combat. Obsolete.
        /// </summary>
        /// <param name="armyToJoin"></param>
        internal void JoinArmies( Army armyToJoin )
        {
            Army joinedArmy = this.Copy();

            foreach(Regiment r in armyToJoin.Regiments )
            {
                if ( joinedArmy.Regiments.Any( reg => reg.Unit == r.Unit ) )
                {
                    Regiment reg = armyToJoin.FindRegiment( r.Unit );
                    joinedArmy.AddToRegiment( reg.Unit, reg.Number );
                }
                else
                {
                    joinedArmy.AddRegiment( r.Unit, r.Number );
                }
            }

            Regiments.Clear();
            foreach( Regiment r in joinedArmy.Regiments )
            {
                Regiments.Add( new Regiment(r.Unit, r.Number ) );
            }
        }

        /// <summary>
        /// Count the number of unit in a army.
        /// </summary>
        /// <returns></returns>
        internal double Count()
        {
            double number = 0;

            foreach( Regiment r in this.Regiments )
            {
                number += r.Number;
            }
            return number;
        }
    }
}
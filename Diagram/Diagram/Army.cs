using ITI.SkyLord.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Army
    {
        private ArmyState _armyState;
        private Island _island;
        private readonly RegimentList _regiments;

        //private readonly Dictionary<Unit, int> _regiments;

        /// <summary>
        /// Create new army.
        /// </summary>
        /// <param name="armyState">Army state</param>
        /// <param name="island">This is the island where is from the army.</param>
        public Army( ArmyState armyState, Island island )
        {
            this._island = island;
            //this._regiment = new Regiment();
            this._armyState = armyState;
            this._regiments = new RegimentList();
        }
        #region Properties
        /// <summary>
        /// Gets or sets ArmyState
        /// </summary>
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

        /// <summary>
        /// Gets dictionnary Unit,int Regiments
        /// </summary>
        public RegimentList Regiments
        {
            get
            {
                return _regiments;
            }
        }

        /// <summary>
        /// Gets or sets the Island where is from the Army.
        /// </summary>
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
        #endregion

        /// <summary>
        /// Find a regiment corresponding to a Unit in an army.
        /// </summary>
        /// <param name="unit">The unit to look for in the army.</param>
        /// <returns>The regiment found. If the regiment is not found it returns null.</returns>
        internal Regiment FindRegiment( Unit unit )
        {
            return _regiments.Where( u => u.Name == unit.Name ).FirstOrDefault();
        }

        /// <summary>
        /// Gets all the regiments containing units that deal physical damage
        /// </summary>
        /// <param name="unitDamageType"></param>
        /// <returns></returns>
        internal RegimentList GetRegimentsByDamagetype( UnitDamageType unitDamageType)
        {
            IEnumerable<Regiment> regs = _regiments.Where( r => r.Unit.UnitDamageType == unitDamageType );
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
            foreach(Unit u in _regiments.Select( r => r.Unit) )
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
            _regiments.Remove( r );

            if ( finalUnitNumber > 0 )
                _regiments.Add( new Regiment(unit, finalUnitNumber ) );
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
            _regiments.Remove( r );
            _regiments.Add( new Regiment( unit, finalUnitNumber ) );
        }

        internal Army Copy()
        {
            Army army = new Army(this.ArmyState, this.Island);
            foreach( Regiment r in this.Regiments )
            {
                army.Regiments.Add( r );
            }

            return army;
        }

        internal Army GetArmyByRatio( double ratio )
        {
            Army army = new Army( this.ArmyState, this.Island );
            int newValue;

            foreach( Regiment r in this.Regiments )
            {
                newValue = (int)Math.Round( (double)r.Number * ratio );
                army.Regiments.Add( new Regiment(r.Unit, newValue ) );
            }

            return army;
        }

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
                    joinedArmy.Regiments.Add( new Regiment( r.Unit, r.Number ) );
                }
            }

            _regiments.Clear();
            foreach( Regiment r in joinedArmy.Regiments )
            {
                _regiments.Add( new Regiment(r.Unit, r.Number ) );
            }
        }
    }
}
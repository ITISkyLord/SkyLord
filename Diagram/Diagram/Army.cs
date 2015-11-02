using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Army
    {
        private ArmyState _armyState;
        private Island _island;
        private readonly Dictionary<Unit, int> _regiments;

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
            this._regiments = new Dictionary<Unit, int>();
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
        public Dictionary<Unit, int> Regiments
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
        /// Find a regiment corresponding to a Unit in an army
        /// </summary>
        /// <param name="unit">The unit to look for in the army</param>
        /// <returns>A keyValuePair that represents the regiment found. If the regiment is not found the key will be null</returns>
        internal KeyValuePair<Unit, int> FindRegiment( Unit unit )
        {
            return _regiments.Where( u => u.Key.Name == unit.Name ).FirstOrDefault();
        }

        internal Dictionary<Unit, int> GetRegimentsByDamagetype( UnitDamageType unitDamageType)
        {
            return _regiments.Where( kvp => kvp.Key.UnitDamageType == unitDamageType ).ToDictionary( kvp => kvp.Key, kvp => kvp.Value);
        }

        internal double GetPhysicalAttackRatio()
        {
            double totalAttack = 0;
            double physicalAttack = 0;
            foreach(Unit u in _regiments.Keys )
            {
                totalAttack += u.UnitStatistics.Attack;
                if ( u.UnitDamageType == UnitDamageType.physical )
                    physicalAttack += u.UnitStatistics.Attack;
            }

            return physicalAttack / totalAttack;
        }
    }
}
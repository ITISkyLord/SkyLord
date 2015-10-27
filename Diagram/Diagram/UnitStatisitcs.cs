using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public struct UnitStatisitcs
    {
        int _attack;
        int _armor;
        int _shield;
        int _speed;
        int _capacity;
        int _consumption; // Magic consumption by the hour

        public UnitStatisitcs(int attack, int armor, int shield, int speed, int capacity, int consumption)
        {
            if ( attack > 100 || attack < 0) throw new ArgumentOutOfRangeException( "The attack value of a unit cannot be over 100 or under 0." );
            if ( armor > 100 || shield > 100 || armor < 0 || shield < 0) throw new ArgumentOutOfRangeException( "The defense (shield or armor) value of a unit cannot be over 100 or under 0." );
            if ( speed < 0 ) throw new ArgumentOutOfRangeException( "The speed of a unit cannot be under 0." );
            if ( capacity < 0 ) throw new ArgumentOutOfRangeException( "The capacity of a unit cannot be under 0." );
            if ( consumption < 0 ) throw new ArgumentOutOfRangeException( "The speed of a unit cannot be under 0." );

            _attack = attack;
            _armor = armor;
            _shield = shield;
            _speed = speed;
            _capacity = capacity;
            _consumption = consumption;
        }

        #region Properties
        public int Attack
        {
            get
            {
                return _attack;
            }
        }

        public int Armor
        {
            get
            {
                return _armor;
            }

        }

        public int Shield
        {
            get
            {
                return _shield;
            }
        }

        public int Speed
        {
            get
            {
                return _speed;
            }
        }

        public int Capacity
        {
            get
            {
                return _capacity;
            }
        }

        public int Consumption
        {
            get
            {
                return _consumption;
            }
        }
        #endregion
    }
}
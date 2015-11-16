using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord.Model.Units
{
    public struct UnitStatistics
    {
        int _attack;
        int _physicResist;
        int _magicResist;
        int _speed;
        int _capacity;
        int _consumption; // Magic consumption by the hour

        public UnitStatistics(int attack, int physicResist, int magicResist, int speed, int capacity, int consumption)
        {
            if ( attack > 100 || attack < 0) throw new ArgumentOutOfRangeException( "The attack value of a unit cannot be over 100 or under 0." );
            if ( physicResist > 100 || magicResist > 100 || physicResist < 0 || magicResist < 0) throw new ArgumentOutOfRangeException( "The defense (shield or armor) value of a unit cannot be over 100 or under 0." );
            if ( speed < 0 ) throw new ArgumentOutOfRangeException( "The speed of a unit cannot be under 0." );
            if ( capacity < 0 ) throw new ArgumentOutOfRangeException( "The capacity of a unit cannot be under 0." );
            if ( consumption < 0 ) throw new ArgumentOutOfRangeException( "The speed of a unit cannot be under 0." );

            _attack = attack;
            _physicResist = physicResist;
            _magicResist = magicResist;
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

        public int PhysicResist
        {
            get
            {
                return _physicResist;
            }

        }

        public int MagicResist
        {
            get
            {
                return _magicResist;
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
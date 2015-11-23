using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class UnitStatistics
    {
        int _attack;
        int _physicResist;
        int _magicResist;
        int _speed;
        int _capacity;
        int _consumption; // Magic consumption by the hour

        public long UnitStatisticsId { get; set; }

        public int Attack
        {
            get
            {
                return _attack;
            }

            set
            {
                _attack = value;
            }
        }

        public int PhysicResist
        {
            get
            {
                return _physicResist;
            }

            set
            {
                _physicResist = value;
            }
        }

        public int MagicResist
        {
            get
            {
                return _magicResist;
            }

            set
            {
                _magicResist = value;
            }
        }

        public int Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }

        public int Capacity
        {
            get
            {
                return _capacity;
            }

            set
            {
                _capacity = value;
            }
        }

        public int Consumption
        {
            get
            {
                return _consumption;
            }

            set
            {
                _consumption =  value ;
            }
        }

        public UnitStatistics(int attack, int physicResist, int magicResist, int speed, int capacity, int consumption)
        {
            if ( attack > 100 || attack < 0) throw new ArgumentOutOfRangeException( "The attack value of a unit cannot be over 100 or under 0." );
            if ( physicResist > 100 || magicResist > 100 || physicResist < 0 || magicResist < 0) throw new ArgumentOutOfRangeException( "The defense (shield or armor) value of a unit cannot be over 100 or under 0." );
            if ( speed < 0 ) throw new ArgumentOutOfRangeException( "The speed of a unit cannot be under 0." );
            if ( capacity < 0 ) throw new ArgumentOutOfRangeException( "The capacity of a unit cannot be under 0." );
            if ( consumption < 0 ) throw new ArgumentOutOfRangeException( "The speed of a unit cannot be under 0." );

            Attack = attack;
            PhysicResist = physicResist;
            MagicResist = magicResist;
            Speed = speed;
            Capacity = capacity;
            Consumption = consumption;
        }

        #region Properties
        
        #endregion
    }
}
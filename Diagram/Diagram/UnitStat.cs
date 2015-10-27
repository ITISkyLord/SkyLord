using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public struct UnitStat
    {
        int _attack;
        int _armor;
        int _shield;
        int _speed;
        int _capacity;
        int _consumption; // Consommation de magie par heure.

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

        public UnitStat(int attack, int armor, int shield, int speed, int capacity, int consumption)
        {
            _attack = attack;
            _armor = armor;
            _shield = shield;
            _speed = speed;
            _capacity = capacity;
            _consumption = consumption;
        }
    }
}
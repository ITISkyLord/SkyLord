using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Unit
    {
        protected int _id;
        private UnitName _unitName;
        protected UnitDamageType _unitDamageType;
        protected UnitType _unitType;
        protected UnitStatistics _unitStatistics;
        protected Ressource _unitCost;

        #region Properties

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public UnitName UnitName
        {
            get
            {
                return _unitName;
            }
        }

        public string Name
        {
            get
            {
                return _unitName.ToString();
            }
        }

        public UnitDamageType UnitDamageType
        {
            get
            {
                return _unitDamageType;
            }
        }
        public UnitType UnitType
        {
            get
            {
                return _unitType;
            }
        }
        public UnitStatistics UnitStatistics
        {
            get
            {
                return _unitStatistics;
            }
        }

        public Ressource UnitCost
        {
            get
            {
                return _unitCost;
            }
        }

        //protected Regiment Regiment
        //{
        //    get
        //    {
        //        return _regiment;
        //    }

        //    set
        //    {
        //        _regiment = value;
        //    }
        //} 
        #endregion

        internal Unit( UnitName unitName, UnitDamageType unitDamageType, UnitType unitType, UnitStatistics unitStatistics, Ressource unitCost )
        {
            _unitName = unitName;
            _unitDamageType = unitDamageType;
            _unitType = unitType;
            _unitStatistics = unitStatistics;
            _unitCost = unitCost;
        }
        public override bool Equals( object obj )
        {
            Unit other = obj as Unit;
            return other != null && other.Name == this.Name;
        }

        public override int GetHashCode()
        {
            if ( Name == null ) return 0;
            return Name.GetHashCode();
        }
    }
}

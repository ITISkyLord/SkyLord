using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class Unit
    {
        private UnitName _unitName;
        private UnitDamageType unitDamageType;
        private UnitType unitType;
        private UnitStatistics unitStatistics;
        private Ressource unitCost;

        #region Properties

        public int UnitId { get; set; }


        public string Name
        {
            get
            {
                return _unitName.ToString();
            }
        }

        public UnitName UnitName
        {
            get
            {
                return _unitName;
            }

            set
            {
                _unitName = value;
            }
        }

        public UnitDamageType UnitDamageType
        {
            get
            {
                return unitDamageType;
            }

            set
            {
                unitDamageType = value;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return unitType;
            }

            set
            {
                unitType = value;
            }
        }

        public UnitStatistics UnitStatistics
        {
            get
            {
                return unitStatistics;
            }

            set
            {
                unitStatistics = value;
            }
        }

        public Ressource UnitCost
        {
            get
            {
                return unitCost;
            }

            set
            {
                unitCost =  value ;
            }
        }


        #endregion

        internal Unit( UnitName unitName, UnitDamageType unitDamageType, UnitType unitType, UnitStatistics unitStatistics, Ressource unitCost )
        {
            UnitName = unitName;
            UnitDamageType = unitDamageType;
            UnitType = unitType;
            UnitStatistics = unitStatistics;
            UnitCost = unitCost;
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

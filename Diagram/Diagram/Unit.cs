using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public abstract class Unit
    {
        protected int _id;
        protected UnitType _unitType;
        protected UnitStatisitcs _unitStatistics;
        protected Island _island;
        protected Regiment _regiment;

        #region Properties
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }
        }
        public UnitType UnitType
        {
            get
            {
                return _unitType;
            }
        }
        public UnitStatisitcs UnitStatistics
        {
            get
            {
                return _unitStatistics;
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
        protected Regiment Regiment
        {
            get
            {
                return _regiment;
            }

            set
            {
                _regiment = value;
            }
        } 
        #endregion
    }
}

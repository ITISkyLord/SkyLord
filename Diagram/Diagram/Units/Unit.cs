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
        protected UnitDamageType _unitDamageType;
        protected UnitType _unitType;
        protected UnitStatistics _unitStatistics;
     //   protected Regiment _regiment;

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

        public override bool Equals( object obj )
        {
            if( obj == null ) throw new ArgumentNullException( "obj == null" );
            Unit other = obj as Unit;
            if( other == null ) throw new ArgumentException( "obj != Unit" );
            return (this.Name == other.Name);
          
        }
    }
}

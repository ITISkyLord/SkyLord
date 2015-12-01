using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class Unit
    {
        #region Properties
        public int UnitId { get; set; }


        public string Name { get; set; }

        public UnitName UnitName { get; set; }

        public UnitDamageType UnitDamageType { get; set; }

        public UnitType UnitType { get; set; }

        public UnitStatistics UnitStatistics { get; set; }

        public Ressource UnitCost { get; set; }
        #endregion

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

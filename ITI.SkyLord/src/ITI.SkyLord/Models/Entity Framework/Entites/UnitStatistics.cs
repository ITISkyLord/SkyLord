using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class UnitStatistics
    {

        #region Properties
        public long UnitStatisticsId { get; set; }

        public int Attack { get; set; }

        public int PhysicResist { get; set; }

        public int MagicResist { get; set; }

        public int Speed { get; set; }

        public int Capacity { get; set; }

        public int Consumption { get; set; }
        #endregion
    }
}
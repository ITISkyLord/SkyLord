using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
   public class UnitStatistic
    {
        public int UnitStatisticId { get; set; }
        public int Attack { get; set; }
        public int Capacity { get; set; }
        public int Consumption { get; set; }
        public int MagicResist { get; set; }
        public int PhysicResist { get; set; }
        public int Speed { get; set; }
    }
}

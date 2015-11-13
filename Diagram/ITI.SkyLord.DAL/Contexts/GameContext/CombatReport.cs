using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
   public  class CombatReport
    {
        public long CombatReportId { get; set; }
        public string ObjectReport { get; set; }
        public string Report { get; set; }

        public Player Receiver { get; set; }
    }
}
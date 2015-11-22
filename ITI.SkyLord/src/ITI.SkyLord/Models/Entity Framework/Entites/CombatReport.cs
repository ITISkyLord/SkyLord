using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class CombatReport
    {
        private long _combatReportId;
        private Player _receiver;
        private string _objectReport;
        private string _report;

        [Key]
        public long CombatreportId { get; set; }
        public Player Receiver { get; set; }

        public string ObjectReport { get; set; }

        public string Report { get; set; }
    }
}
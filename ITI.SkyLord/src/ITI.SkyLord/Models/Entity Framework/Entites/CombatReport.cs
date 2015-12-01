using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class CombatReport
    {
        [Key]
        public long CombatreportId { get; set; }

        public Player Receiver { get; set; }

        public string ObjectReport { get; set; }

        public string Report { get; set; }
    }
}
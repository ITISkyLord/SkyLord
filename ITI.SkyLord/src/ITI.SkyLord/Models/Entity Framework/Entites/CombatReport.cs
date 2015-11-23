using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class CombatReport
    {
        private Player _receiver;
        private string _objectReport;
        private string _report;

        [Key]
        public long CombatreportId { get; set; }

        public Player Receiver
        {
            get
            {
                return _receiver;
            }

            set
            {
                _receiver = value;
            }
        }

        public string ObjectReport
        {
            get
            {
                return _objectReport;
            }

            set
            {
                _objectReport = value;
            }
        }

        public string Report
        {
            get
            {
                return _report;
            }

            set
            {
                _report =  value ;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class CombatReport
    {
        private int _id;
        private Player _receiver;
        private string _objectReport;
        private string _report;

        public Player Receiver
        {
            get
            {
                return _receiver;
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
                _report = value;
            }
        }
    }
}
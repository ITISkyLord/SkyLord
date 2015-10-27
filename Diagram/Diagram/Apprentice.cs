using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Apprentice
    {
        //private int _id;
        private ApprenticeLevel _level;

        public Apprentice()
        {
            this._level = new ApprenticeLevel();
        }

        public ApprenticeLevel Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
            }
        }
    }
}
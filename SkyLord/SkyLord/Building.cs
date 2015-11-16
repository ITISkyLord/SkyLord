using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord.Model
{
    public abstract class Building
    {
        private int _id;
        private string _name;
        private BuildingLevel _level;

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public BuildingLevel Level
        {
            get
            {
                return _level;
            }
        }
    }
}
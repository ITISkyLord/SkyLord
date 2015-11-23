using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public abstract class Building
    {
        private string _name;
        private BuildingLevel _level;

        [Key]
        public int BuildingId { get; set; }

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

            set
            {
                _level =  value ;
            }
        }
    }
}
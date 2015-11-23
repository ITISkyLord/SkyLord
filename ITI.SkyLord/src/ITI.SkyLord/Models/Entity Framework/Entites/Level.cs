using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public abstract class Level
    {
        private int _number;
        private Ressource _cost;

        public Level( int number = 1 )
        {
            Number = number;
        }

        [Key]
        public long LevelId { get; set; }

        public int Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = value;
            }
        }

        public Ressource Cost
        {
            get
            {
                return _cost;
            }

            set
            {
                _cost = value;
            }
        }
    }
}
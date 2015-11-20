using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public abstract class Level
    {
        private long _levelId;
        private int _number;
        private Ressource _cost;

        public Level( int number = 1 )
        {
            _number = number;
        }
        [Key]
        public long LevelId { get; set; }
        public int Number { get; set; }
        // Coût en ressources à voir avec le XML et la base de données.
        // Le reste ce sera des méthodes.
        public Ressource Cost { get; set; }
    }
}
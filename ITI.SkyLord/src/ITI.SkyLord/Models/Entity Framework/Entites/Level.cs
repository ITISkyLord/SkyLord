using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public abstract class Level
    {
        [Key]
        public long LevelId { get; set; }

        public int Number { get; set; }

        public Ressource Cost { get; set; }
    }
}
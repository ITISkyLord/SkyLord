using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
    public class Level
    {
        public long LevelId { get; set; }
        public int Number { get; set; }
        public Ressource Cost { get; set; }
    }
}

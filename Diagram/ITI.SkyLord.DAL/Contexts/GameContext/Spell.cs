using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
    public class Spell
    {
        public long SpellId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}

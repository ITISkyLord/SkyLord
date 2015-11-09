using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
    public class Regiment
    {
        public long RegimentId { get; set; }
        public int Number { get; set; }
        public Unit Unit { get; set; }
    }
}

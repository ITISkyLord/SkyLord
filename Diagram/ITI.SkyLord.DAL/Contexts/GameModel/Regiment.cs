using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
    public class Regiment
    {
        public long RegimentId { get; set; }
        public int Number { get; set; }
        public Unit Unit { get; set; }
    }
}

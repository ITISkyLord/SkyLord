using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{
   public class Ressource
    {
        public long RessourceId { get; set; }
        public int Cristal { get; set; }
        public int Magic { get; set; }
        public int Metal { get; set; }
        public int Wood { get; set; }
    }
}

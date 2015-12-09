using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.SeeRessources
{
    public class SeeRessources
    {
        public Ressource Ressources { get; set; }
        public List<Ressource> AllRessources { get; set; }

        public Island Island { get; set; }
        public int Cristal { get; set; }
        public int Wood { get; set; }
        public int Magic { get; set; }
        public int Metal { get; set; }
    }
}

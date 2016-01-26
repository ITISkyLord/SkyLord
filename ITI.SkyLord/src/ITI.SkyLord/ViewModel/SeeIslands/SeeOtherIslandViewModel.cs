using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.SeeIslands
{
    public class SeeOtherIslandViewModel : StandardViewModel
    {
        List<Army> AttacksOnThisIslands { get; set; }
        List<Army> AttacksFromThisIslands { get; set; }
        Ressource RessourceProduction { get; set; }
    }
}

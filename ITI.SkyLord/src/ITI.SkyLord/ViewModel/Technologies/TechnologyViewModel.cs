using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;

namespace ITI.SkyLord.ViewModel.Technologies
{
    public class TechnologyViewModel : StandardViewModel
    {
        public long IslandId { get; set; }
        public List<TechnologyLevel> AllTechnologies { get; set; }
        public List<TechnologyLevel> AvailableTechnologies { get; set; }
     //   public SelectList AvailableSearchingTechnologies{ get; set; }

        public Dictionary<TechnologyName, Ressource> NextLevelCosts { get; set; }
        public TechnologyName TargetTechnology { get; set; }
        public List<Technology> OwnTechnologies { get; set; }

    }
}

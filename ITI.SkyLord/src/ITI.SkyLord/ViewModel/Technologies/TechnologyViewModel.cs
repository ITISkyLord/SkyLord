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
        public List<Technology> ExistingTechnologies { get; set; }
        public List<Technology> AvailableTechnologies { get; set; }
        public SelectList AvailableSearchingTechnologies{ get; set; }

        public Dictionary<TechnologyName, Ressource> NextLevelCosts { get; set; }
        public TechnologyName TargetTechnology { get; set; }
        public List<Technology> Technologies { get; set; }

    }
}

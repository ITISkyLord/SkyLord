﻿using Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.AccountsModel
{
    public partial class Island
    {
        public long IslandId { get; set; }
        public bool IsCapital { get; set; }
        public int Loyalty { get; set; }
        public string Name { get; set; }


        public Ressource Ressources { get; set; }
        public ICollection<Army> Army { get; set; }
        public ICollection<Building> Building { get; set; }
        public Coordinate Coordinate { get; set; }    
        public Player Owner { get; set; }

    }
}

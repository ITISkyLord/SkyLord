﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameContext
{ 
   public class Map
    {
        public long WorldId { get; set; }
        public World World { get; set; }

       public ICollection<Island> Islands { get; set; }
    }
}

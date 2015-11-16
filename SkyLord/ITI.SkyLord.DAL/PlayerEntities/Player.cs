using ITI.SkyLord.DAL.GuildEntities;
using ITI.SkyLord.DAL.IslandEntities;
using ITI.SkyLord.DAL.WorldEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.PlayerEntities
{
    public class Player
    {
        public long PlayerId { get; set; }
        public string Name { get; set; }
        public Guild Guild { get; set; }
        public IList<Island> Islands { get; set; }
        public IList<Technology> Technologies { get; set; }
        public World World { get; set; }
        public virtual Profile Profil { get; set; }

    }
}

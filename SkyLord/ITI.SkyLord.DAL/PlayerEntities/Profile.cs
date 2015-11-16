using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.PlayerEntities
{
   public class Profile
    {
        public long PlayerId { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public virtual Player Owner { get; set; }

    }
}

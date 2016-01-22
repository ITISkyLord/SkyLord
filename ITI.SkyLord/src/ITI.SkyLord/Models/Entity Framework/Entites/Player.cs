using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ITI.SkyLord
{
    public class Player
    {
        [Key]
        public long PlayerId { get; set; }

        [StringLength( 20, MinimumLength = 3 ) ]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        public Profil Profil { get; set; }

        public IList<Island> Islands { get; set; }

        public Guild Guild { get; set; }

        public IList<Technology> Technologies { get; set; }

        public World World { get; set; }
        public int MaxIsland { get; set; }
        public User_Player UserPlayer { get; set; }
    }
}
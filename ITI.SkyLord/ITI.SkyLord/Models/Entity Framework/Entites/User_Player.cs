using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework;

namespace ITI.SkyLord
{
    public class User_Player
    {
        public User_Player(Player player, ApplicationUser user)
        {
            Player = player;
            User = user;
        }
        [Key]
        public long User_PlayerId { get; set; }
        
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        [ForeignKey( "UserId" )]
        public ApplicationUser User { get; set; }

      
    }
}

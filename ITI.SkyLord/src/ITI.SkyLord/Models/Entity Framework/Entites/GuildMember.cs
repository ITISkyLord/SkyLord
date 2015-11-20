using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Entites
{
    public class GuildMember
    {
        private long _guildMemberId;
        private Guild _guild;
        private GuildRole _guildRole;

        [Key]
        public long GuildMemberId { get; set; }
        public Guild Guild { get; set; }
        public GuildRole GuildRole { get; set; }
    }
}

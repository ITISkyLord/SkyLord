using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class GuildMember
    {
        private Guild _guild;
        private GuildRole _guildRole;

        [Key]
        public long GuildMemberId { get; set; }

        public Guild Guild
        {
            get
            {
                return _guild;
            }

            set
            {
                _guild = value;
            }
        }

        public GuildRole GuildRole
        {
            get
            {
                return _guildRole;
            }

            set
            {
                _guildRole =  value ;
            }
        }
    }
}

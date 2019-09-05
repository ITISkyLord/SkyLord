using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord
{
    public class GuildMember
    {

        [Key]
        public long GuildMemberId { get; set; }

        public Guild Guild { get; set; }

        public GuildRole GuildRole { get; set; }
    }
}

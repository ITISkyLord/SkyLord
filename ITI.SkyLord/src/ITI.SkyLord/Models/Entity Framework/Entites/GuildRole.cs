using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class GuildRole
    {

        [Key]
        public long GuildRoleId { get; set; }

        public string Name { get; set; }

        public MemberRight Rights { get; set; }
    }
}
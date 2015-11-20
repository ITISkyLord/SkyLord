using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class GuildRole
    {
        private long _guildRoleId;
        private string _name;
        private List<MemberRight> _rights; //Rajouter des rôles dans MemberRight si nécessaire.

        [Key]
        public long GuildRoleId { get; set; }
        public string Name { get; set; }
        public List<MemberRight> Rights { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.GuildEntities
{
    public class GuildRole
    {
        public long GuildRoleId { get; set; }
        public string Name { get; set; }
        public MemberRight Rights { get; set; }
    }
}

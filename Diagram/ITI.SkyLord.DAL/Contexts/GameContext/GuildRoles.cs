using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
    public class GuildRoles
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public MemberRight MemberRight { get; set; }
    }
}

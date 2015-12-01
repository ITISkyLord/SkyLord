using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Guild
    {
        #region Properties
        [Key]
        public long GuildId { get; set; }

        public string Name { get; set; }

        public IList<GuildMember> Members { get; set; }

        public string Description { get; set; }

        public string InternalMessage { get; set; }

        public string PublicMessage { get; set; }

        public bool Recrutement { get; set; }
        #endregion
    }
}
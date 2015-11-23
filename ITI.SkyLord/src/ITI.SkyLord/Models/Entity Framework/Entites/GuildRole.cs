using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class GuildRole
    {
        private string _name;
        private List<MemberRight> _rights; //Rajouter des rôles dans MemberRight si nécessaire.

        [Key]
        public long GuildRoleId { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public List<MemberRight> Rights
        {
            get
            {
                return _rights;
            }

            set
            {
                _rights = value;
            }
        }
    }
}
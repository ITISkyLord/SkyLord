using Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.AccountsModel
{
   public partial class AccountProfil
    {
        public long ProfilId { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }
        public long Owner { get; set; }
        public string Password { get; set; }

        public virtual AccountPlayer Player { get; set; }

    }
}

using Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.AccountsModel
{
    public partial class AccountPlayer
    {
        public AccountPlayer()
        {
            //this.AccountProfil = new HashSet<Profil>();
        }
        public long PlayerId { get; set; }
        public long Guild { get; set; }
        public Island Island { get; set; }
        public string Name { get; set; }
        public Technology Technology { get; set; }
        public long World { get; set; }
        
        
        

        //public virtual ICollection<Profil> AccountProfil { get; set; }
        //public virtual Guild Guild { get; set; }
        //public virtual List<Island> Island { get; set; }
        //public virtual World AccountWorld { get; set; }
        //public virtual List<Technology> Technology { get; set; }

    }
}

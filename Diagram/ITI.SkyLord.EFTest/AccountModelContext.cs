using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.AccountsModel
{
    public partial class AccountsEntity : DbContext
    {
        public AccountsEntity()
        {
            
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<AccountProfil> Profils { get; set; }
        public virtual DbSet<AccountPlayer> Players { get; set; }
    }
}

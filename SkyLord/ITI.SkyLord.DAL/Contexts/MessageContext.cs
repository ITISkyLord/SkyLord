using ITI.SkyLord.DAL.MessageEntities;
using ITI.SkyLord.DAL.PlayerEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.MessageContext
{
    public partial class MessageContext : DbContext
    {
        public MessageContext()
            :base("SkyLordDB")
        {
            Database.SetInitializer<MessageContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure PlayerId as PK for Profile
            modelBuilder.Entity<Profile>()
               .HasKey(e => e.PlayerId);

           // Configure PlayerId as FK for Profile
           modelBuilder.Entity<Player>()
                       .HasOptional(s => s.Profil) // Mark Profile is optional for Player
                       .WithRequired(ad => ad.Owner); // Create inverse relationship

            base.OnModelCreating(modelBuilder);
        }
       
        public DbSet<Message> Messages { get; set; }
        
    }
}

using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity.EntityFramework;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;
using ITI.SkyLord.ViewModel;
using System;
using System.Linq;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public class SetupContext : IdentityDbContext, ISuperInterfaceContext
    {

        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Event>()
                .HasDiscriminator<string>("EventType")
                    .HasValue<ArmyEvent>(EventDiscrimator.ArmyEvent)
                    .HasValue<UnitEvent>(EventDiscrimator.UnitEvent)
                    .HasValue<BuildingEvent>(EventDiscrimator.BuildingEvent)
                    .HasValue<UpgradeEvent>(EventDiscrimator.UpgradeEvent)
                    .HasValue<TechnologyEvent>(EventDiscrimator.TechnologyEvent);
        }

        public IConfigurationRoot Configuration { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile( "appsettings.json" );

            Configuration = builder.Build();
            var appEnv = CallContextServiceLocator.Locator.ServiceProvider
                            .GetRequiredService<IApplicationEnvironment>();
            optionsBuilder.UseSqlServer( Configuration[ "Data:DefaultConnection:ConnectionString" ] );
        }

        #region DbSet
        public DbSet<Profil> Profils { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Mage> Mages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Island> Islands { get; set; }
        public DbSet<Apprentice> Apprentices { get; set; }
        public DbSet<Army> Armies { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<CombatReport> CombatReports { get; set; }
        public DbSet<GuildMember> GuildMembers { get; set; }
        public DbSet<GuildRole> GuildRoles { get; set; }
        public DbSet<Regiment> Regiments { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitStatistics> UnitStatistics { get; set; }
        public DbSet<World> Worlds { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UnitEvent> UnitEvents { get; set; }
        public DbSet<ArmyEvent> ArmyEvents { get; set; }
        public DbSet<UpgradeEvent> UpgradeEvents { get; set; }
        public DbSet<TechnologyEvent> TechnologyEvents { get; set; }
        public DbSet<User_Player> UserPlayers { get; set; }
        public DbSet<BuildingEvent> BuildingEvents { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<User_Player> User_Players { get; set; }
        #endregion DbSet

        #region Enumerations
        public UnitDamageType UnitDamageTypes { get;set;}
        public UnitName UnitName { get; set; }
        public UnitType UnitType { get; set; }
        public ArmyMovement ArmyMovements { get; set; }
        public EventType EventTypes { get; set; }
        public BuildingName BuildingName { get; set; }
        public TechnologyName TechnologyName { get; set; }
        #endregion Enumerations

        #region Helper

        public Player GetPlayer(string userId)
        {
            return Players.Where(pl => pl.UserPlayer.User.Id == userId).First();
        }
        public World GetWorld()
        {
            return this.Worlds.FirstOrDefault();
        }
        public Player FindPlayer(long playerId)
        {
            return Players.Include( p => p.Profil).FirstOrDefault( p => p.PlayerId == playerId );
        }
        public Player FindPlayer(string playerMail)
        {
            return Players.FirstOrDefault( p => p.Mail == playerMail );
        }
        public void FillStandardVM(StandardViewModel svm, long playerId, long islandId)
        {
            svm.Layout = new LayoutViewModel();

            svm.Layout.AllIslands = Islands.Include(i => i.Owner).Where(i => i.Owner.PlayerId == playerId).ToList();
            svm.Layout.CurrentIsland = GetIsland(islandId, playerId);
            svm.Layout.CurrentPlayer = Players.Single(p => p.PlayerId == playerId);
            svm.Layout.IslandId = islandId;
        }
        public Island GetIsland(long islandId, long playerId)
        {
            if (islandId == 0)
            {
                long activePlayerId = playerId;
                return Islands
                    .Include(i => i.Armies)
                    .ThenInclude(a => a.Regiments)
                    .ThenInclude(r => r.Unit)
                    .Include(i => i.AllRessources)
                    .Include(i => i.Owner)
                    .Include(i => i.Coordinates)
                    .SingleOrDefault(i => i.IsCapital && i.Owner.PlayerId == activePlayerId);
            }
            else
            {
                long activePlayerId = playerId;
                return Islands
                    .Include(i => i.Armies)
                    .ThenInclude(a => a.Regiments)
                    .ThenInclude(r => r.Unit)
                    .Include(i => i.AllRessources)
                    .Include(i => i.Owner)
                    .Include(i => i.Coordinates)
                    .SingleOrDefault(i => i.IslandId == islandId && i.Owner.PlayerId == activePlayerId);
            }
        }
        void IDbContext.OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            OnConfiguring(optionsBuilder);
        }
        void IDbContext.OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreating(modelBuilder);
        }

        #endregion Helper

        //public DbSet<MageLevel> MageLevels { get; set; }

    }
}

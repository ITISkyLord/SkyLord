using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public class SetupContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public IConfigurationRoot Configuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            var appEnv = CallContextServiceLocator.Locator.ServiceProvider
                            .GetRequiredService<IApplicationEnvironment>();
            optionsBuilder.UseSqlServer( Configuration["Data:DefaultConnection:ConnectionString"] );
        }

        public DbSet<Profil> Profils { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Mage> Mages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Island> Islands { get; set; }
        public DbSet<Apprentice> Apprentices { get; set; }
        //public DbSet<ApprenticeLevel> ApprenticesLevel { get; set; }
        public DbSet<Army> Armies { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<CombatReport> CombatReports { get; set; }
        public DbSet<GuildMember> GuildMembers { get; set; }
        public DbSet<GuildRole> GuildRoles { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Regiment> Regiments { get; set; }
        public DbSet<Ressource> Ressources { get; set; }
        public DbSet<Spell> Spelles { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitStatistics> UnitStatistics { get; set; }
        public DbSet<World> Worlds { get; set; }
        //public DbSet<BuildingLevel> BuildingLevels { get; set; }
        //public DbSet<MageLevel> MageLevels { get; set; }
        //public DbSet<TechnologyLevel> TechnologiesLevel { get; set; }

    }
}

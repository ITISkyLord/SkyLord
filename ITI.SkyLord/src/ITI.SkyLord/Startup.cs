using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using ITI.SkyLord.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework;
using Microsoft.AspNet.Diagnostics.Entity;
using System.Linq;
using ITI.SkyLord.Controllers;
using System;
using Microsoft.AspNet.Cryptography.KeyDerivation;
using Microsoft.Data.Entity.ChangeTracking;
using ITI.SkyLord.Services;

namespace ITI.SkyLord
{
    public class Startup
    {
        public Startup( IHostingEnvironment env, IApplicationEnvironment appEnv )
        {
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            if( env.IsDevelopment() )
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            // Add Entity Framework services to the services container.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<SetupContext>( options =>
                     options.UseSqlServer( Configuration["Data:DefaultConnection:ConnectionString"] ) );

            // Add Identity services to the services container.
            services.AddIdentity<ApplicationUser, IdentityRole>( options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonLetterOrDigit = false;
                options.Password.RequireUppercase = false;
            } )
                .AddEntityFrameworkStores<SetupContext>()
                .AddDefaultTokenProviders();

            // Add MVC services to the services container.
            services.AddMvc();


            services.AddScoped<PlayerContext>();
            services.AddScoped<IslandContext>();
            services.AddScoped<ArmyContext>();
            services.AddScoped<ArmyManager>();
            services.AddScoped<LevelContext>();
            services.AddScoped < MessageContext>();
            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();

            // Register application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddDebug();

            // Configure the HTTP request pipeline.

            // Add the following to the request pipeline only in development environment.
            if( env.IsDevelopment() )
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // sends the request to the following path or controller action.
                app.UseExceptionHandler( "/Home/Error" );
            }


            // If you want sessions to expire 30 minutes after the last activity, you would do so like this:
            //  app.UseInMemorySession( configure: s => s.IdleTimeout = TimeSpan.FromMinutes( 30 ) );


            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add authentication middleware to the request pipeline. You can configure options such as Id and Secret in the ConfigureServices method.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715

            // Add MVC to the request pipeline.
            app.UseMvc( routes =>
             {
                 routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}" );
             } );

            #region Seeding
            World defaultWorld = null;

            Unit cyclop = null;
            Unit gobelin = null;
            Unit guard = null;
            Unit necromancer = null;
            Unit troll = null;
            Unit warrior = null;

            if ( env.IsDevelopment() )
            {
                // Add defaultWorld
                using ( WorldContext context = new WorldContext() )
                {
                    defaultWorld = context.Worlds.FirstOrDefault();
                    if ( defaultWorld == null )
                    {
                        defaultWorld = new World();
                        context.Add( defaultWorld );
                        context.SaveChanges();
                    }
                }

                // Add Islands
                using ( IslandContext context = new IslandContext() )
                {
                    if( context.Islands.Count() < 99 )
                    {
                        for ( int i = 0; i < 100; i++ )
                        {
                            Ressource ressource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
                            Coordinate coord = new Coordinate();
                            coord.X = i;
                            coord.Y = i;
                            context.Ressources.Add(ressource);
                            context.Coordinates.Add(coord);
                            context.SaveChanges();

                            Island island = new Island();
                            island.Loyalty = 100;
                            island.Coordinates = coord;
                            island.AllRessources = ressource;
                                                  
                            context.Islands.Add( island );
                            context.SaveChanges();
                        }
                    }
                }

                //Add defaultUnits
                using ( ArmyContext context = new ArmyContext() )
                {

                    cyclop = context.Units.Where( u => u.UnitName == UnitName.cyclop ).SingleOrDefault();
                    gobelin = context.Units.Where( u => u.UnitName == UnitName.gobelin ).SingleOrDefault();
                    guard = context.Units.Where( u => u.UnitName == UnitName.guard ).SingleOrDefault();
                    necromancer = context.Units.Where( u => u.UnitName == UnitName.necromancer ).SingleOrDefault();
                    troll = context.Units.Where( u => u.UnitName == UnitName.troll ).SingleOrDefault();
                    warrior = context.Units.Where( u => u.UnitName == UnitName.warrior ).SingleOrDefault();

                    if ( cyclop == null )
                    {
                        Ressource cyclopCost = new Ressource { Wood = 400, Metal = 200, Cristal = 100, Magic = 100 };
                        context.Ressources.Add( cyclopCost );
                        UnitStatistics cyclopStatistics = new UnitStatistics { Attack = 150, PhysicResist = 80, MagicResist = 50, Capacity = 200, Speed = 15, Consumption = 20 };
                        context.UnitStatistics.Add( cyclopStatistics );
                        context.SaveChanges();

                        cyclop = new Unit
                        {
                            Name = "Cyclope",
                            UnitType = UnitType.monster,
                            UnitName = UnitName.cyclop,
                            UnitDamageType = UnitDamageType.magical,
                            UnitCost = cyclopCost,
                            UnitStatistics = cyclopStatistics
                        };
                        context.Units.Add( cyclop );
                        context.SaveChanges();
                    }
                    if ( gobelin == null )
                    {
                        Ressource gobelinCost = new Ressource { Wood = 100, Metal = 100, Magic = 20 };
                        context.Ressources.Add( gobelinCost );
                        UnitStatistics gobelinStatistics = new UnitStatistics { Attack = 40, PhysicResist = 30, MagicResist = 30, Capacity = 50, Speed = 30, Consumption = 5 };
                        context.UnitStatistics.Add( gobelinStatistics );
                        context.SaveChanges();

                        gobelin = new Unit
                        {
                            Name = "Gobelin",
                            UnitType = UnitType.monster,
                            UnitName = UnitName.gobelin,
                            UnitDamageType = UnitDamageType.physical,
                            UnitCost = gobelinCost,
                            UnitStatistics = gobelinStatistics
                        };
                        context.Units.Add( gobelin );
                        context.SaveChanges();
                    }
                    if ( guard == null )
                    {
                        Ressource guardCost = new Ressource { Wood = 200, Metal = 100 };
                        context.Ressources.Add( guardCost );
                        UnitStatistics guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
                        context.UnitStatistics.Add( guardStatistics );
                        context.SaveChanges();

                        guard = new Unit
                        {
                            Name = "Garde",
                            UnitType = UnitType.soldier,
                            UnitName = UnitName.guard,
                            UnitDamageType = UnitDamageType.physical,
                            UnitCost = guardCost,
                            UnitStatistics = guardStatistics
                        };
                        context.Units.Add( guard );
                        context.SaveChanges();
                    }
                    if ( necromancer == null )
                    {
                        Ressource necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
                        context.Ressources.Add( necromancerCost );
                        UnitStatistics necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
                        context.UnitStatistics.Add( necromancerStatistics );
                        context.SaveChanges();

                        necromancer = new Unit
                        {
                            Name = "Nécromancien",
                            UnitType = UnitType.magic,
                            UnitName = UnitName.necromancer,
                            UnitDamageType = UnitDamageType.magical,
                            UnitCost = necromancerCost,
                            UnitStatistics = necromancerStatistics,
                        };
                        context.Units.Add( necromancer );
                        context.SaveChanges();
                    }
                    if ( troll == null )
                    {
                        Ressource trollCost = new Ressource { Wood = 300, Metal = 200, Cristal = 150, Magic = 50 };
                        context.Ressources.Add( trollCost );
                        UnitStatistics trollStatistics = new UnitStatistics { Attack = 180, PhysicResist = 60, MagicResist = 100, Capacity = 200, Speed = 15, Consumption = 20 };
                        context.UnitStatistics.Add( trollStatistics );
                        context.SaveChanges();

                        troll = new Unit
                        {
                            Name = "Troll",
                            UnitType = UnitType.monster,
                            UnitName = UnitName.troll,
                            UnitDamageType = UnitDamageType.physical,
                            UnitCost = trollCost,
                            UnitStatistics = trollStatistics,
                        };
                        context.Units.Add( troll );
                        context.SaveChanges();
                    }
                    if ( warrior == null )
                    {
                        Ressource warriorCost = new Ressource { Wood = 200, Metal = 100, Cristal = 50 };
                        context.Ressources.Add( warriorCost );
                        UnitStatistics warriorStatistics = new UnitStatistics { Attack = 80, PhysicResist = 40, MagicResist = 40, Capacity = 125, Speed = 20, Consumption = 15 };
                        context.UnitStatistics.Add( warriorStatistics );
                        context.SaveChanges();

                        warrior = new Unit
                        {
                            Name = "Guerrier",
                            UnitType = UnitType.soldier,
                            UnitName = UnitName.warrior,
                            UnitDamageType = UnitDamageType.physical,
                            UnitCost = warriorCost,
                            UnitStatistics = warriorStatistics,
                        };
                        context.Units.Add( warrior );
                        context.SaveChanges();
                    }
                }
            }
            #endregion
        }

        private string ProtectPassword( string clearpassword )
        {
            // generate a 128-bit salt using a secure PRNG
            byte[ ] salt = new byte[ 128 / 8 ];
            //using ( var rng = RandomNumberGenerator.Create() )
            //{
            //    rng.GetBytes( salt );
            //}

            // TODO : garded le salt dans la table Player

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return Convert.ToBase64String( KeyDerivation.Pbkdf2(
                password: clearpassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8 ) );
        }
    }
}

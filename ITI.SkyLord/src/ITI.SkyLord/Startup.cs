using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
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
using System.Collections.Generic;

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

            // TODO : Doit être supprimer
            services.AddScoped<PlayerContext>();
            services.AddScoped<IslandContext>();
            services.AddScoped<ArmyContext>();
            services.AddScoped<ArmyManager>();
            services.AddScoped<LevelContext>();
            services.AddScoped<MessageContext>();
            // Fin Doit être supprimer


            services.AddScoped<SetupContext>();
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
           // loggerFactory.MinimumLevel = LogLevel.Information;
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

            if( env.IsDevelopment() )
            {
                // Add defaultWorld
                using( WorldContext context = new WorldContext() )
                {
                    defaultWorld = context.Worlds.FirstOrDefault();
                    if( defaultWorld == null )
                    {
                        defaultWorld = new World();
                        context.Add( defaultWorld );
                        context.SaveChanges();
                    }
                }

                // Add Islands
                using( IslandContext context = new IslandContext() )
                {
                    if( context.Islands.Count() < 99 )
                    {
                        for( int i = 0; i < 100; i++ )
                        {
                            Ressource ressource = new Ressource { Wood = 1000, Metal = 1000, Cristal = 1000, Magic = 1000 };
                            Coordinate coord = new Coordinate();
                            coord.X = i;
                            coord.Y = i;
                            context.Ressources.Add( ressource );
                            context.Coordinates.Add( coord );
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
                using( ArmyContext context = new ArmyContext() )
                {

                    cyclop = context.Units.Where( u => u.UnitName == UnitName.cyclop ).SingleOrDefault();
                    gobelin = context.Units.Where( u => u.UnitName == UnitName.gobelin ).SingleOrDefault();
                    guard = context.Units.Where( u => u.UnitName == UnitName.guard ).SingleOrDefault();
                    necromancer = context.Units.Where( u => u.UnitName == UnitName.necromancer ).SingleOrDefault();
                    troll = context.Units.Where( u => u.UnitName == UnitName.troll ).SingleOrDefault();
                    warrior = context.Units.Where( u => u.UnitName == UnitName.warrior ).SingleOrDefault();

                    if( cyclop == null )
                    {
                        Ressource cyclopCost = new Ressource { Wood = 400, Metal = 200, Cristal = 100, Magic = 100 };
                        context.Ressources.Add( cyclopCost );
                        UnitStatistics cyclopStatistics = new UnitStatistics { Attack = 150, PhysicResist = 80, MagicResist = 50, Capacity = 200, Speed = 15, Consumption = 20 };
                        context.UnitStatistics.Add( cyclopStatistics );

                        cyclop = new Unit
                        {
                            Name = "Cyclope",
                            UnitType = UnitType.monster,
                            UnitName = UnitName.cyclop,
                            UnitDamageType = UnitDamageType.magical,
                            UnitCost = cyclopCost,
                            UnitStatistics = cyclopStatistics,
                            Duration = 120
                        };
                        context.Units.Add( cyclop );
                        //context.SaveChanges();
                    }
                    if( gobelin == null )
                    {
                        Ressource gobelinCost = new Ressource { Wood = 100, Metal = 100, Magic = 20 };
                        context.Ressources.Add( gobelinCost );
                        UnitStatistics gobelinStatistics = new UnitStatistics { Attack = 40, PhysicResist = 30, MagicResist = 30, Capacity = 50, Speed = 30, Consumption = 5 };
                        context.UnitStatistics.Add( gobelinStatistics );

                        gobelin = new Unit
                        {
                            Name = "Gobelin",
                            UnitType = UnitType.monster,
                            UnitName = UnitName.gobelin,
                            UnitDamageType = UnitDamageType.physical,
                            UnitCost = gobelinCost,
                            UnitStatistics = gobelinStatistics,
                            Duration = 10
                        };
                        context.Units.Add( gobelin );
                        //context.SaveChanges();
                    }
                    if( guard == null )
                    {
                        Ressource guardCost = new Ressource { Wood = 200, Metal = 100 };
                        context.Ressources.Add( guardCost );
                        UnitStatistics guardStatistics = new UnitStatistics { Attack = 70, PhysicResist = 70, MagicResist = 40, Capacity = 100, Speed = 20, Consumption = 10 };
                        context.UnitStatistics.Add( guardStatistics );

                        guard = new Unit
                        {
                            Name = "Garde",
                            UnitType = UnitType.soldier,
                            UnitName = UnitName.guard,
                            UnitDamageType = UnitDamageType.physical,
                            UnitCost = guardCost,
                            UnitStatistics = guardStatistics,
                            Duration = 90
                        };
                        context.Units.Add( guard );
                        //context.SaveChanges();
                    }
                    if( necromancer == null )
                    {
                        Ressource necromancerCost = new Ressource { Wood = 100, Metal = 100, Cristal = 200, Magic = 50 };
                        context.Ressources.Add( necromancerCost );
                        UnitStatistics necromancerStatistics = new UnitStatistics { Attack = 70, PhysicResist = 40, MagicResist = 70, Capacity = 50, Speed = 30, Consumption = 15 };
                        context.UnitStatistics.Add( necromancerStatistics );

                        necromancer = new Unit
                        {
                            Name = "Nécromancien",
                            UnitType = UnitType.magic,
                            UnitName = UnitName.necromancer,
                            UnitDamageType = UnitDamageType.magical,
                            UnitCost = necromancerCost,
                            UnitStatistics = necromancerStatistics,
                            Duration = 90
                        };
                        context.Units.Add( necromancer );
                        //context.SaveChanges();
                    }
                    if( troll == null )
                    {
                        Ressource trollCost = new Ressource { Wood = 300, Metal = 200, Cristal = 150, Magic = 50 };
                        context.Ressources.Add( trollCost );
                        UnitStatistics trollStatistics = new UnitStatistics { Attack = 180, PhysicResist = 60, MagicResist = 100, Capacity = 200, Speed = 15, Consumption = 20 };
                        context.UnitStatistics.Add( trollStatistics );

                        troll = new Unit
                        {
                            Name = "Troll",
                            UnitType = UnitType.monster,
                            UnitName = UnitName.troll,
                            UnitDamageType = UnitDamageType.physical,
                            UnitCost = trollCost,
                            UnitStatistics = trollStatistics,
                            Duration = 150
                        };
                        context.Units.Add( troll );
                        //context.SaveChanges();
                    }
                    if( warrior == null )
                    {
                        Ressource warriorCost = new Ressource { Wood = 200, Metal = 100, Cristal = 50 };
                        context.Ressources.Add( warriorCost );
                        UnitStatistics warriorStatistics = new UnitStatistics { Attack = 80, PhysicResist = 40, MagicResist = 40, Capacity = 125, Speed = 20, Consumption = 15 };
                        context.UnitStatistics.Add( warriorStatistics );

                        warrior = new Unit
                        {
                            Name = "Guerrier",
                            UnitType = UnitType.soldier,
                            UnitName = UnitName.warrior,
                            UnitDamageType = UnitDamageType.physical,
                            UnitCost = warriorCost,
                            UnitStatistics = warriorStatistics,
                            Duration = 70
                        };
                        context.Units.Add( warrior );
                        context.SaveChanges();
                    }
                }
            }
            #endregion

            #region Temp seed level
            //using ( LevelContext context = new LevelContext() )
            //{
            //    Ressource barrackLevel1Cost = null;
            //    Ressource barrackLevel2Cost = null;
            //    Ressource barrackLevel3Cost = null;

            //    Requirement barrackLevel2Requirement = null;
            //    Requirement barrackLevel3Requirement = null;
            //    Requirement barrackLevel3Requirement2 = null;

            //    Level barrackLevel1 = null;
            //    Level barrackLevel2 = null;
            //    Level barrackLevel3 = null;
            //    Level towerLevel2 = null;



            //    // Set up requirements
            //    barrackLevel2Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 1 };
            //    barrackLevel3Requirement = new Requirement { BuildingName = BuildingName.barrack, Number = 2 };
            //    barrackLevel3Requirement2 = new Requirement { BuildingName = BuildingName.tower, Number = 2 };
            //    context.Add( barrackLevel2Requirement );
            //    context.Add( barrackLevel3Requirement );
            //    context.Add( barrackLevel3Requirement2 );

            //    // Set up LevelCosts
            //    barrackLevel1Cost = new Ressource { Wood = 100, Metal = 50 };
            //    barrackLevel2Cost = Multiplyressource( barrackLevel1Cost, 2 );
            //    barrackLevel3Cost = Multiplyressource( barrackLevel2Cost, 2 );
            //    context.Add( barrackLevel1Cost );
            //    context.Add( barrackLevel2Cost );
            //    context.Add( barrackLevel3Cost );

            //    // Set up Levels
            //    barrackLevel1 = new BuildingLevel
            //    {
            //        Number = 1,
            //        BuildingName = BuildingName.barrack,
            //        Cost = barrackLevel1Cost
            //    };
            //    barrackLevel2 = new BuildingLevel
            //    {
            //        Number = 2,
            //        BuildingName = BuildingName.barrack,
            //        Cost = barrackLevel2Cost,
            //        Requirements = new List<Requirement> { barrackLevel2Requirement }
            //    };
            //    barrackLevel3 = new BuildingLevel
            //    {
            //        Number = 3,
            //        BuildingName = BuildingName.barrack,
            //        Cost = barrackLevel3Cost,
            //        Requirements = new List<Requirement> { barrackLevel3Requirement, barrackLevel3Requirement2 }
            //    };
            //    towerLevel2 = new BuildingLevel
            //    {
            //        Number = 2,
            //        BuildingName = BuildingName.tower
            //    };
            //    context.Add( barrackLevel1 );
            //    context.Add( barrackLevel2 );
            //    context.Add( barrackLevel3 );
            //    context.Add( towerLevel2 );

            //    context.SaveChanges();
            //}
            #endregion

        }

        private Ressource Multiplyressource( Ressource initialRessource, int factor )
        {
            return new Ressource
            {
                Wood = initialRessource.Wood * factor,
                Metal = initialRessource.Metal * factor,
                Cristal = initialRessource.Cristal * factor,
                Magic = initialRessource.Magic * factor
            };
        }
    }
}


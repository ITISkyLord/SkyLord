using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using System.Linq;
using ITI.SkyLord.Controllers;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Data.Entity.ChangeTracking;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ITI.SkyLord
{
    public class Startup
    {
        public Startup( IHostingEnvironment env )
        {
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath( env.ContentRootPath )
                .AddJsonFile( "appsettings.json" )
                .AddJsonFile( $"config.{env.EnvironmentName}.json", optional: true );

            if ( env.IsDevelopment() )
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            // Add Entity Framework services to the services container.
            services.AddDbContext<SetupContext>( options =>
                     options.UseSqlServer( Configuration[ "Data:DefaultConnection:ConnectionString" ] ) );

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
            if ( env.IsDevelopment() )
            {
                // Ces lignes sont les bonnes, mais je décommente pour un test
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // sends the request to the following path or controller action.
                app.UseExceptionHandler("/Home/Index");
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


using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITI.SkyLord.ASP4.Startup))]
namespace ITI.SkyLord.ASP4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

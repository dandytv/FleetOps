using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FleetSys.Startup))]
namespace FleetSys
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

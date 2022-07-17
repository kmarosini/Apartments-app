using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RWA.Startup))]
namespace RWA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zebra.Startup))]
namespace Zebra
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

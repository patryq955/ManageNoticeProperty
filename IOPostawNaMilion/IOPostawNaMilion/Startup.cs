using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IOPostawNaMilion.Startup))]
namespace IOPostawNaMilion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

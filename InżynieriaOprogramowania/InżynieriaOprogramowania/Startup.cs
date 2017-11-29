using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InżynieriaOprogramowania.Startup))]
namespace InżynieriaOprogramowania
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

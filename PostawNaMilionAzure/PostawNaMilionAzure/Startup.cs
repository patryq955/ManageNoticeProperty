using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PostawNaMilionAzure.Startup))]
namespace PostawNaMilionAzure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

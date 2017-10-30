using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManageNoticeProperty.Startup))]
namespace ManageNoticeProperty
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

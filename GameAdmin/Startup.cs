using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameAdmin.Startup))]
namespace GameAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

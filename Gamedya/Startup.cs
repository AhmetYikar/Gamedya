using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gamedya.Startup))]
namespace Gamedya
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

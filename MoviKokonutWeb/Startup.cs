using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoviKokonutWeb.Startup))]
namespace MoviKokonutWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

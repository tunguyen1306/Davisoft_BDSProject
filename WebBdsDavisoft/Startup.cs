using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBdsDavisoft.Startup))]
namespace WebBdsDavisoft
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

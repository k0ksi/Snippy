using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Snippy.Web.Startup))]
namespace Snippy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

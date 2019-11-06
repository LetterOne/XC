using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XC.WebApp.Startup))]
namespace XC.WebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

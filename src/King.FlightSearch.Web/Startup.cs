using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(King.FlightSearch.Web.Startup))]
namespace King.FlightSearch.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

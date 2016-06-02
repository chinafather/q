using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(q.Startup))]
namespace q
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

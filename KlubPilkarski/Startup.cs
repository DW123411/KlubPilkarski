using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KlubPilkarski.Startup))]
namespace KlubPilkarski
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(proyectoProgramaciónAvanzada.Startup))]
namespace proyectoProgramaciónAvanzada
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

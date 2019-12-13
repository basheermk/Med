using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Medacademy.Startup))]
namespace Medacademy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

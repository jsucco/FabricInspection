using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inspection_mvc.Startup))]
namespace Inspection_mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

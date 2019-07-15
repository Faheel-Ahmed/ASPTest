using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPTestWT.Startup))]
namespace ASPTestWT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FuckThisNumber.Startup))]
namespace FuckThisNumber
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

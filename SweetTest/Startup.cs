using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SweetTest.Startup))]
namespace SweetTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

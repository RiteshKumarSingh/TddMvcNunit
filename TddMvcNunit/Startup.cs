using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TddMvcNunit.Startup))]
namespace TddMvcNunit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

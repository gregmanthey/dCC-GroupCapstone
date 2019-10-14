using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dCC_GroupCapstone.Startup))]
namespace dCC_GroupCapstone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

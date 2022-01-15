using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(medical.Startup))]
namespace medical
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhoneDirectory.Startup))]
namespace PhoneDirectory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NorthernLights.CRM.Startup))]

namespace NorthernLights.CRM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
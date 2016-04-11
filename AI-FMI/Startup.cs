using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AI_FMI.Startup))]
namespace AI_FMI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
     
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AI.Startup))]
namespace AI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

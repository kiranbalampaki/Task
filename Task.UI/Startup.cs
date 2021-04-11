using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Task.UI.Startup))]
namespace Task.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

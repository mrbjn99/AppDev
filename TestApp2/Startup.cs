using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestApp2.Startup))]
namespace TestApp2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

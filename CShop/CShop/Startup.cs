using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CShop.Startup))]
namespace CShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

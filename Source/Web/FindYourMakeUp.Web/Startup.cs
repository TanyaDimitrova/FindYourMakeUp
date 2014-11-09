using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FindYourMakeUp.Web.Startup))]
namespace FindYourMakeUp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

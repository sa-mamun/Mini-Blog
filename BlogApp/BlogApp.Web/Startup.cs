using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogApp.Web.Startup))]
namespace BlogApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

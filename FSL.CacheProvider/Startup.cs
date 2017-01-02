using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FSL.CacheProvider.Startup))]
namespace FSL.CacheProvider
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

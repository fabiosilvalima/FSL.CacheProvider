using FSL.CacheProvider.Caching;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FSL.CacheProvider
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();

            container.Register<ICache, HttpRuntimeCache>(Lifestyle.Singleton);
            container.Register<ICacheProvider, FslCacheProvider>(Lifestyle.Singleton);

            container.Verify();

            DependencyResolver.SetResolver(
               new SimpleInjectorDependencyResolver(container));
        }
    }
}

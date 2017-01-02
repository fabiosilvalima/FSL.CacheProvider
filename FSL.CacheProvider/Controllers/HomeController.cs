using FSL.CacheProvider.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FSL.CacheProvider.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAsyncCacheProvider _cacheProvider;

        public HomeController(IAsyncCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}